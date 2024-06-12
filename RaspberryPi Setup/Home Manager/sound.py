import tensorflow as tf
import numpy as np
import csv
import pyaudio
import io
import socket

#CSV Reader helper function for YAMNet
def class_names_from_csv(class_map_csv_text):
  """Returns list of class names corresponding to score vector."""
  class_map_csv = io.StringIO(class_map_csv_text)
  class_names = [display_name for (class_index, mid, display_name) in csv.reader(class_map_csv)]
  class_names = class_names[1:]  # Skip CSV header
  return class_names

#Audio Constants
#Sample Rate
RATE=16000
#Length of each Window
RECORD_SECONDS = 2
#Buffer Size
CHUNKSIZE = 1024

# initialize portaudio object
audio = pyaudio.PyAudio()

#Set up YAMNet variables
interpreter = tf.lite.Interpreter('lite-model_yamnet_tflite_1.tflite')
input_details = interpreter.get_input_details()
waveform_input_index = input_details[0]['index']
output_details = interpreter.get_output_details()
scores_output_index = output_details[0]['index']
embeddings_output_index = output_details[1]['index']
spectrogram_output_index = output_details[2]['index']

# Download the YAMNet class map to yamnet_class_map.csv
class_names = class_names_from_csv(open('yamnet_class_map.csv').read())

# Highly recommend to run the MicCheck.py first to get the desired microphone input index and check if it is activated
mic_index = 2

# UDP SOCKET COMMUNICATION
server = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# Quest IP = 10.0.0.57 in home WIFI
serverAddPort = ("10.0.0.57", 5060)

while True:
    # Open Stream
    stream = audio.open(format=pyaudio.paInt16,
                        channels=1,
                        rate=RATE,
                        input=True,
                        frames_per_buffer=CHUNKSIZE,
                        input_device_index=mic_index)

    # Divide audio into frames and store all the frames in the array list
    frames = []
    for _ in range(0, int(RATE / CHUNKSIZE * RECORD_SECONDS)):
        # Step 1: The stream.read(CHUNK_SIZE) operation retrieves the audio data as a binary (1 or 0) string
        data = stream.read(CHUNKSIZE)
        # Step 2: Convert the binary string data to an array of 16-bit integers
        frames.append(np.frombuffer(data, dtype=np.int16))

    # Convert the frame numpy array to 1D-32 float array as it required by YAMNet:
    # Horizontally stack the frames stored in the frames list
    convert_data = np.hstack(frames)

    # Normalize audio data
    # The maximum value of a 16-bit signed integer is 32767,
    # so dividing by 32768.0 scales the values between -1.0 and 1.0
    convert_data = convert_data.astype(np.float32, order='C') / 32768.0

    # YAMNet interpreter
    waveform = convert_data

    # close stream
    stream.stop_stream()
    stream.close()

    # Interpreter setup
    interpreter.resize_tensor_input(waveform_input_index, [len(waveform)], strict=True)
    interpreter.allocate_tensors()
    interpreter.set_tensor(waveform_input_index, waveform)
    interpreter.invoke()
    scores, embeddings, spectrogram = (
        interpreter.get_tensor(scores_output_index),
        interpreter.get_tensor(embeddings_output_index),
        interpreter.get_tensor(spectrogram_output_index))

    # Sort 'scores' array
    prediction = np.mean(scores, axis=0)
    # Choose top 3 values from sorted array
    top3 = np.argsort(prediction)[::-1][:3]

    # print 'class names' and 'confidence value' according top3
    ## print('\n'.join('  {:12s}: {:.3f}'.format(class_names[i], prediction[i]) for i in top3))
    ## print(' ')

    ## UDP Version
    prediction_str = '\n'.join('  {:12s}: {:.3f}'.format(class_names[i], prediction[i]) for i in top3)
    print(prediction_str)
    print(' ')
    server.sendto(str.encode(str(prediction_str)), serverAddPort)

audio.terminate()
