# Quiet Interaction In Mixed Reality - Designing an Accessible Home Environment for DHH Individuals through AR, AI, and IoT Technologies
![IMG_0520](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/3fbdb608-a333-4490-aa2c-479c3c80d0f3)


### *Link to Full Video: [Youtube Link](https://youtu.be/vcVAcftTQxQ)*
### *Link to System Interaction Tutorial: [Youtube Link](https://youtu.be/z4Dv3mkz-7o)*
### *Test the APK (Basic interaction without Raspy integration): [Sample APK](https://drive.google.com/file/d/1nOGyeBYFsGYZFEVWwdgokjDOVLC8749r/view?usp=sharing)*
---
## I've been thinking about...
*How might the integration of AR, AI, and IoT technologies in HMD platforms potentially open up new avenues for improving the home living experience of Deaf and Hard of Hearing (DHH) individuals?*

## Project Introduction (Why do I choose this topic?)
As technology rapidly evolves, voice-command-based smart assistants are becoming integral to our daily lives. However, this advancement overlooks the needs of the Deaf and Hard of Hearing (DHH) community, creating a technological gap in current systems. To address this technological oversight, this study develops a Mixed-Reality (MR) application that integrates Augmented Reality (AR), Artificial Intelligence (AI), and the Internet of Things (IoT) to fill the gaps in safety, communication, and accessibility for DHH individuals at home.

## Research Methodologies 
![image](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/945a1f3a-695a-4d8d-884f-187f5034954d)

+ **Literature Review:**
Gain insights into the daily challenges that DHH individuals encounter at home, as well as the advantages and limitations of current solutions.

+ **Online Survey with DHH Individuals:**
A more detailed understanding of their specific challenges and needs.

## Required Package
### Embedded
+ [Meta Interaction SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-interaction-sdk-264559) 
+ [M2MqttUnity by gpvigano](https://github.com/gpvigano/M2MqttUnity) 
+ [Azure Speech SDK for Unity](https://aka.ms/csspeech/unitypackage)
  
### Need to purchase
+ [FlatKit](https://assetstore.unity.com/packages/vfx/shaders/flat-kit-toon-shading-and-water-143368)
  
  (FlatKit is only for the outline effects, feel free to purchase.)
  
+ [3D WebView for Android (Web Browser) by Vuplex](https://assetstore.unity.com/packages/tools/gui/3d-webview-for-android-web-browser-137030)
  
  UnityOculusAndroidVRBrowser by IanPhilips [https://github.com/IanPhilips/UnityOculusAndroidVRBrowser] (This one should also work and it's free ;), which you might wanna give a try.)

## Required Hardware
+ Meta Quest 2+. *(I used the Quest 3 for testing; however, the Quest 2, and Quest Pro should also work.)*
+ Zigbee devices: [List of supported Zigbee devices](https://www.zigbee2mqtt.io/supported-devices/)
+ Respeaker 4-Mic Array for Raspberry Pi Module. *(This could be any microphone, as long as it is capable of capturing audio for analysis by the Raspberry Pi.)*

+ SONOFF Zigbee USB Dongle and RaspberryPi 4B for smart device communication: [SONOFF Zigbee dongle](https://www.zigbee2mqtt.io/devices/ZBDongle-E.html)
+ Surveillance System: A webcam with microphone + RaspberryPi 4B + PWM Servo Driver
  
  ![223](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/27523272-952e-4c54-965c-40d30f562bb0)

## API Used
Microsoft Azure for Real-Time Speech Diarization *(note that Python is not currently supported. However, remember to refer to the official documentation for any updates.)*, Speech-To-Text, and Text-To-Speech
   
## System architecture diagram
![image](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/cb0d8dea-dade-450c-b72e-0cecd65f83a3)

Example of MQTT workflow

![image](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/7a127443-1c5c-4b4a-8a1e-05f178f55db9)

---
# Important Note

As this project involves communication between hardware and APIs, it requires a somewhat complex setup and a basic understanding of Raspberry Pi.
If you're interested in experimenting with the application, please ensure you have the following setup:

## RaspberryPi Setup
1. Set up your **Zigbee Dongle** and **MQTT broker** ([you can refer to this tutorial](https://randomnerdtutorials.com/how-to-install-mosquitto-broker-on-raspberry-pi/](https://www.youtube.com/watch?v=efmsed9Aj-o&t=605s))) on your Raspberry Pi, and enter your API keys for Azure Speech Service (a free tier is available).
2. Run the Python scripts on Raspberry Pi, remember to change the IP address and note the port numbers (*feel free to change it*) used in these scripts.
   *For the speech services script, you can choose between Livecaption.py and Translation.py. There's no need to run both.*

![image](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/8a92a078-78ec-4dd4-89e9-090628332508)

## Unity Setup
### After downloading the project:

1. Import FlatKit & 3D WebView for Android
2. Enter your Azure Speech key and Live Caption port in the Inspector:
   
   ![image](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/b82b3eb8-1541-473a-81ed-d8ecee15a535)

4. Configure MQTT settings in the Inspector:
   
   ![image](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/4eb0ae35-8f9c-443c-b08b-28931ec96723)
     
5. Surveillance System: 
   
   ![image](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/19136e8d-8805-4c1e-948c-141e523369fe)
   
## Support
Given the complexity of the setup, if you encounter any issues during setup or testing, please do not hesitate to contact me. Let's work together to build a more accessible home living environment for the DHH community.

---
## Usage Preview
### 1. 3D User Interface

+ *User Menu Mechanism*

![UserMenuMecha-ezgif com-video-to-gif-converter](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/ea2514a0-6879-4ec4-a3ac-361a1e7b06c1)

+ *3D Representative User Home Model and Room Selection*

![RoomSelection-ezgif com-video-to-gif-converter](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/3f64d55f-7710-4c85-92b5-1769a6ac2962)

+ *Tutorial for first time user*

![Tutorial-ezgif com-video-to-gif-converter](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/e11e0218-9726-4c7e-b5e3-b986a3efed32)

### 2. Smart Device Control
+ *Lamp Control*

![Lamp-ezgif com-video-to-gif-converter](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/15cdd1b9-ad8e-481c-80c8-51a0a7be45d3)

+ *Blinds Control*

![Blinds-ezgif com-video-to-gif-converter](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/5e7b30be-f4a8-41f3-b140-3220db8ce096)

### 3. Understand Appliances Status
+ *Example 1: Microwvae Status*

![Microwave-ezgif com-video-to-gif-converter](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/ef7edc8d-0f6d-4947-b367-cfb02cd478be)

### 4. Urgent Events
+ *Water Leaking*

![WaterLeaking-ezgif com-video-to-gif-converter](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/0e035bd2-b7eb-4daa-a664-acf0378747c6)

+ *Fridge Door Opened*

![Fridge-ezgif com-video-to-gif-converter](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/a029b13a-778c-4d0b-97fe-aec6347694f7)

### 5. Visitors

![DoorSensor-ezgif com-video-to-gif-converter](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/f0fcb4a5-6abf-4b60-8069-df01b14d2575)

### 6. Speech Services
+ *Active Keyboard*

![Keyboard-ezgif com-video-to-gif-converter](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/c7ffc1fe-cbcd-45f2-b1d2-0801a7ca89d0)

+ *Speech-to-Text and Text-to-Speech*

![TexttoSpeech-ezgif com-optimize](https://github.com/NarrowSpace/Quiet-Interaction-For-Deaf-and-Hard-of-Hearing-Community/assets/105491905/f3238677-1b75-47fe-91b1-12e0de826f76)

*(Note that the participant’s face is blurred to protect privacy and maintain anonymity.)*
