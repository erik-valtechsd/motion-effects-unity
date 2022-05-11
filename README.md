# Interactive Particle Drawing Display
## Valtech CXP Hackathon Demo


### About

This demo uses immersive 3D particle to transform body motion into an immersive interactive experience.

The technology behind the demo has two main components: 1. an interactive 3D particle environment powered by Unity3d software, and 2. a web-app which uses rotation data from a mobile device to remotely interact with the interactive 3D display. The system also utilizes a Firebase realtime database to transmit the data from the web app to the interactive display. The interactive display has a QR code which links to the web app. 

The applications for the demo could be a game or experience for a museum, theme-park, or in-store display. 


### Unity
The 3D environment and particle system is created in Unity3D 2020.3. The particle system is created using Unity Effects Graph system. For the real time control, Firebase Unity plugin is installed which allows the 3D app to communicate with the Firebase realtime database to update the particle system remotely.


### Remote Web App

The web app opens when scanning the QR code on the display. The web app takes realtime orientation data from a user's phone and updates a Firebase realtime database with that data. The 

### Unity Installation

Download the Unity files from this repo. Open the project in Unity3D 2020.3, build the project to your desired platform. 

### Remote web app installation 

Download the web app from the repo here. No need to install, the app uses plain HTML / javascript / CSS.  

