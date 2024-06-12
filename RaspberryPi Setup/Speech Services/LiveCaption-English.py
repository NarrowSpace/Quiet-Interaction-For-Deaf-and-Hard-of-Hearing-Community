import azure.cognitiveservices.speech as speechsdk
import time
import socket

subscription="TYPE YOUR AZURE SPEECH KEY HERE"
region="eastus"
speech_config = speechsdk.SpeechConfig(subscription, region)

# Check the ip address on your Quest WIFI
ip = "10.0.0.57"
port_num = 9900
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

def main():
    # Create a speech recognizer with the given settings
    speech_recognizer = speechsdk.SpeechRecognizer(speech_config=speech_config)
    done = False  # Variable to control the recognition loop
    # Define a callback function to handle the end of a session
    def stop_cb(evt):
        """Callback that stops continuous recognition upon receiving an event `evt`."""
        print(f'CLOSING on {evt}')
        speech_recognizer.stop_continuous_recognition()
        nonlocal done
        done = True

    def recognized_cb(evt):
        text = evt.result.text
        print(text)
        sock.sendto(text.encode(), (ip, port_num))

    ## Connect callbacks to the events fired by the speech recognizer
    # speech_recognizer.recognizing.connect(lambda evt: print(f'RECOGNIZING: {evt.result.text}'))
    # speech_recognizer.recognized.connect(lambda evt: print(evt.result.text))
    speech_recognizer.recognized.connect(recognized_cb)

    speech_recognizer.session_started.connect(lambda evt: print('SESSION STARTED: {}'.format(evt)))
    speech_recognizer.session_stopped.connect(lambda evt: print('SESSION STOPPED {}'.format(evt)))
    speech_recognizer.canceled.connect(lambda evt: print('CANCELED {}'.format(evt)))

    # Connect the callbacks to stop continuous recognition
    speech_recognizer.session_stopped.connect(stop_cb)
    speech_recognizer.canceled.connect(stop_cb)

    # Start continuous speech recognition
    print("Start speaking...")
    speech_recognizer.start_continuous_recognition()

    # Keep the program running until recognition is stopped
    while not done:
        time.sleep(.5)

if __name__ == '__main__':
    main()
