# Changelog

v0.3.5.1
 - added TrustedDevice Property. 
   - The DMS has an option for trusted devices, with this username and password are not required.
 - added BypassLocalhost Property
   - If the DMS and an APP are running on the same machine, a bypass can be set that points directly to the media file. This ensures that the wrapper, in Play, does not create playlists as m3u for the UPnP streams. It will directly return the media file.

v0.3.5.0
 - added additional async functions and properties
   - now all can be used on a async manner
 - added english documentation

v0.3.0.0
 - changed version to a realistic value

v0.0.3.0
 - changed access modifier in RecordingList to public
 - added a Function to send POST data to the media server
 - added a Function to update the data of an RecordingItem easy
   - call item.Update() after you changed the title, description etc.
 - added a Function to delete a RecordingItem
 - adapting of the recordings.html is now completed

v0.0.2.4
 - added a class RecordingChannel, will present a channel in the RecordingItem class
   - added methode to give back a List of RecordingChannel to get all channels in the recordings
 - RecordingItem
   - renamed field SDuration to Duration2 and changed type from string to TimeSpan
   - note: Duration gives the unformated string back, thats the media server sends
   - the field RecDate gives back a DateTime from the unformated field StartDate
   - the field Channel is now a object of type RecordingChannel
 - added a class VideoFilePath
 - added a class VideoFilePathItem
   - every PathItem presents a Path from the videofolder
   - every PathItem contains his own Parent and his Childfolders
   - every PathItem gives back his owned videofiles or all videos in all subdirectories

v0.0.2.3
- changed access modifiers of getter methods in some classes
- renamed Recording to RecordingList
- added a field RecordingSeries as seperate object to RecordingItem
- Recordings can now be filtered by Series
- added a functions to generate M3U Files for video and recordings
- some improvements and bugfixes

v0.0.2.2
- Changed Type of Password from String to SecureString
  - Added a Class to encrypt and decrypt a password to store it in a file
    - The decryption and encryption methods are bound to the Windows account
    - For more details, see the readme.md

v0.0.2.1
- DVBViewerServerApi.cs
  - renamed Function CreateUri to CreateApiUri for Api Uris
  - added a Function CreateUri for Non-Api Uris
  - added a Property ServerTasks, present the Tasks of MediaServer
  - added a Getter GetVideoList to filter video names
  - added a Function GetFileAsync to save and return a file that is coming with a response from the server
- Added Integration of the Servertasks with Run functionality
- VideoFileList and Item
  - added some Properties for UPnP
  - added a Function to filter the videos
  - added a Functions to cleanup/recreate the database
  - added a Function to get a Uri for UPnP streaming
  - added a Function to create a m3u file for a client-applikation

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