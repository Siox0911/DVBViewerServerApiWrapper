# Changelog

v0.0.2.4
 - added class RecordingChannel, will present a channel in the RecordingItem class
   - added methode to give back a List of RecordingChannel to get all channels in the recordings
 - RecordingItem
   - renamed field SDuration to Duration2 and changed type from string to TimeSpan
   - note: Duration gives the unformated string back, thats the media server sends
   - the field RecDate gives back a DateTime from the unformated field StartDate
   - the field Channel is now a object of type RecordingChannel
 - added class VideoFilePath
 - added class VideoFilePathItem
   - every PathItem presents a Path from the videofolder
   - every PathItem contains his own Parent and his Childfolders
   - every PathItem gives back his owned videofiles or all videos in all subdirectories

v0.0.2.3
- changed access modifiers of getter methods in some classes
- renamed Recording to RecordingList
- added field RecordingSeries as seperate object to RecordingItem
- Recordings can now be filtered by Series
- added functions to generate M3U Files for video and recordings
- some improvements and bugfixes

v0.0.2.2
- Changed Type of Password from String to SecureString
  - Added Class to encrypt and decrypt a password to store it in a file
    - The decryption and encryption methods are bound to the Windows account
    - For more details, see the readme.md

v0.0.2.1
- DVBViewerServerApi.cs
  - renamed Function CreateUri to CreateApiUri for Api Uris
  - added Function CreateUri for Non-Api Uris
  - added Property ServerTasks, present the Tasks of MediaServer
  - added Getter GetVideoList to filter video names
  - added Function GetFileAsync to save and return a file that is coming with a response from the server
- Added Integration of the Servertasks with Run functionality
- VideoFileList and Item
  - added some Properties for UPnP
  - added Function to filter the videos
  - added Functions to cleanup/recreate the database
  - added Function to get a Uri for UPnP streaming
  - added Function to create a m3u file for a client-applikation

v0.0.2.0
- added VideoFileList 
- added Version
- added DVBViewerClients
- updating Clients to play videos
- renamed ETypes to Enums
- renames SUriParam to UriParam
- 

v0.0.1.0
- initial Version
- base functionality
  - Serverstatus
  - Recordings
  - Clients