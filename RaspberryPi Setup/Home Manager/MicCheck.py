import pyaudio

# Function to get desired microphone index when you have multi microphone inputs:
def list_microphones():
    audio = pyaudio.PyAudio()
    mic_indices = []
    num_devices = audio.get_device_count()
    for i in range(num_devices):
        info = audio.get_device_info_by_index(i)
        if info["maxInputChannels"] > 0:  # This ensures the device is an input device/microphone
            print(f"Index {i}: {info['name']}")
            mic_indices.append(i)

    audio.terminate()
    return mic_indices

# Check if microphone index 1 is active
def check_microphone_active(index):
    audio = pyaudio.PyAudio()
    try:
        info = audio.get_device_info_by_index(index)
        return info and info["maxInputChannels"] > 0
    except Exception as e:
        print(f"Error: {e}")
        return False
    finally:
        audio.terminate()

# Get Mic indices and info:
if __name__ == "__main__":
    mic_indices = list_microphones()
    is_desired_mic_active = check_microphone_active(1)
    print('\n'f"Desired Microphone is {'active' if is_desired_mic_active else 'not active'}.")
