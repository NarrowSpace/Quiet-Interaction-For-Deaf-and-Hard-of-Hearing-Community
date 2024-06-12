import azure.cognitiveservices.speech as speechsdk
import time
import socket

subscription="TYPE YOUR AZURE SPEECH KEY HERE"
region="eastus"

# You can get a list of the support languages on Azure Speech SDK website
target_language = "en"
source_language = "zh-CN" 

# Setup the speech translation configuration
speech_translation_config = speechsdk.translation.SpeechTranslationConfig(subscription=subscription, region=region)
speech_translation_config.speech_recognition_language = source_language
speech_translation_config.add_target_language(target_language)

# Check the ip address on your Quest WIFI
ip = "10.0.0.57"
port_num = 8990
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

def main():
    audio_config = speechsdk.audio.AudioConfig(use_default_microphone=True)
    translation_recognizer = speechsdk.translation.TranslationRecognizer(translation_config=speech_translation_config, audio_config=audio_config)
    done = False  # Variable to control the recognition loop

    def stop_cb(evt):
        ## When evt is received, the evt message is printed.
        ## After evt is received, stop_continuous_recognition() is called to stop recognition.
        ## The recognition state is changed to True.
        print('CLOSING on {}'.format(evt))
        translation_recognizer.stop_continuous_recognition()
        nonlocal done
        done = True

    def recognized_cb(evt):
        # Check if the event has translations for the target language
        if target_language in evt.result.translations:
            translated_text = evt.result.translations[target_language]
            # print(f"Recognized: {evt.result.text}")
            # print(f"Translated into '{target_language}': {translated_text}")
            final = translated_text
            # Send the translated text over UDP
            print(final)
            sock.sendto(translated_text.encode(), (ip, port_num))

    # Connect callbacks to the events fired by the translation recognizer
    translation_recognizer.recognized.connect(recognized_cb)
    translation_recognizer.session_started.connect(lambda evt: print('SESSION STARTED: {}'.format(evt)))
    translation_recognizer.session_stopped.connect(lambda evt: print('SESSION STOPPED {}'.format(evt)))
    translation_recognizer.canceled.connect(lambda evt: print('CANCELED {}'.format(evt)))

    # Connect the callbacks to stop continuous recognition
    translation_recognizer.session_stopped.connect(stop_cb)
    translation_recognizer.canceled.connect(stop_cb)

    # Start continuous speech recognition
    print("Start speaking...")
    translation_recognizer.start_continuous_recognition()

    # Keep the program running until recognition is stopped
    while not done:
        time.sleep(.5)

    # sock.close()

if __name__ == '__main__':
    main()
