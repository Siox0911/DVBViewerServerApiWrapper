# Changelog

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