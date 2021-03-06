﻿# Changelog

v0.5.0.0
  - changed: Changed from .Net Framework to .Net Standard 2.0.3 to support more targets like UWP, .NetCore and Desktop
  - requirement: .Net Framework 4.6.1 if needed or one of the next targets
  - requirement: .Net Core 2.0 if needed
  - requirement: Mono 5.4 if needed
  - requirement: Xamarin.iOS 10.14 if needed
  - requirement: Xamarin.Android 8.0 if needed
  - requirement: Universal Windows-Plattform 10.0.16299 if needed
  - requirement: Unity 2018.1 if needed
  - changed: Changed the encryption of the server password from `RNGCryptoServiceProvider` to 
  `AesCryptoServiceProvider`. Please never save the `Key` (public key) and the `IV` (secure key) 
on the same place! The public key can be published, but the secure key is TOP SECRET! 
  - removed: Removed the AssemblyInfo.cs, it was replaced by the version control in the .csproj file
  - update: Updated the secureString section in readme.md to use this library.

v0.4.2.1
  - add: `ChannelList`, add function `GetChannelListAsync(string partOfChannelName, bool exact)`
  to searching for a Channel.
  - add: `ChannelItem`, add property `ChannelLogoURL` to get the url of a logo for a channel directly.
  - fix: `ChannelItem`, reworked internal function `GetM3uPrefString` to get the real value of the
  current EPG title. This works only on cable, sattelite. Webstreams are currently not supported.
  - fix: `ChannelItem`, reworked public function `GetChannelLogo` to get the current channel logo.
  - some little improvements...

v0.4.2.0
  - changed: `ChannelItem`, the type of `ChannelID` was changed from long to string: The default 
  `ChannelID` in the DMS is **IDOfTheChannel|ChannelName**.
  - changed: `ChannelList`, parameters channelID from long to string.
  - renamed: `ChannelList`, function `GetChannelListByChannelIDAsync(long channelID)` to 
  `GetChannelListAsync(string channelID)`.
  - renamed: `ChannelList`, function `GetChannelListByEpgChannelIDAsync(long epgChannelID)` 
  to `GetChannelListAsync(long epgChannelID)`.
  - added: `ChannelList`, added function `GetChannelListAsync(ChannelItem channelItem)` to get a 
  `ChannelList` with one channel from the DMS. The `ChannelItem` in this case must be filled only
  with a `ChannelID`.
  - fix: `TimerItem`, the type of the property `Enabled` was changed from bool to int.
  - fix: `TimerItem`, the type of the property `Channel` was changed from string to `ChannelItem`.
  - add: `TimerItem`, add property Type for the type of the timer.
  - fix: `TimerList`, correct parameters to deserialize the XML-Data.
  - removed: `RecordingItem`, the property EventID was removed. This is maybe current temporary, 
  so I can check the references.
  - fix: `EpgItem`, the XML-Element name "Description" was changed to the lower case name "description".

v0.4.1.0
  - added EPG Clear to `EPGList`
  - added classes `TimerList`, `TimerItem` and `TimerOptions` to represent the timers of the DMS
  - changed: class `EpgList` - the `epgChannelID` has been replaced by `ChannelItem`
  - renamed function `GetChannelListAsync(long channelID)` to 
    `GetChannelListByChannelIDAsync(long channelID)`
  - renamed function `GetChannelListAsync(string epgChannelID)` to
    `GetChannelListByEpgChannelIDAsync(long epgChannelID)`

v0.4.0.1
  - added Image to VideoFileItem
  - added MediaFileList and MediaFileItem to parse mediafiles.html for VideoFiles
    - used only to get the mediafile thumb in the VideoFileItem

v0.4.0.0
  - changed Datatype of EpgChannelID from string to long
  - renamed ID to ChannelID in class ChannelSubItem
  - added classes EpgList, EpgItem for EPG support
  - added Enum EpgSearchOptions for EPG Search
  - added class Lists to Helper, for removing double codeinstructions
  - rewritten base features of each List to get the data from the server
  - added CreateM3UFile to ChannelItem, to get a UPnPStream for playback that channel on a 
  player of your choice
  - added GetEpgListNow to ChannelItem to get the current EPGList entry
  - added GetEpgList to ChannelItem to get the complete EPGList from this channel

v0.3.6.1
  - added Channeltuner for details of a channel
  - renamed ID in the class Channel to ChannelID
  - renamed EpgID to EpgChannelID
  - reworked Enum ChannelProperties
    - renamed Reserved to NoAutoUpdateOrIgnorePtsJumps
      - The transmitter data is protected during an AutoUpdate. TS channels ignore PTS jumps.
    - renamed Bandstacking to BandstackingOrPcrRemoving
      - Bandstacking, internally polarisation is always set to H. For TS transmitters, the PCR is removed.
  - ChannelList
    - added some functions to filter the channellist

v0.3.6.0
 - added ChannelList
   - added ChannelTopGroup, this is a list of groups in the ChannelList which includes top-groups 
   of the channel-list
   - added ChannelGroup, this is a list of sub groups in the top-groups 
   - added ChannelItem, this is a list of channels in a sub group
   - added ChannelSubItem, this is a list of subchannel in a channel.
 - added DVBViewerClients can play a channel or his subchannels directly
 - added Server-Tasks to the recordings and videos directly
   - In objects RecordingList and VideoList Update-, Recreate- and Delete-Databasetasks from 
   server can now be started

v0.3.5.3
 - replaced WebRequest trough HttpClient for easier async handling
 - renamed DVBViewerServerApi.GetDataAsync to DVBViewerServerApi.GetApiDataAsync

v0.3.5.2
 - fixed bug on editing series of a recording and series is null or becomes null
 - fixed bug on editing channel of a recording and channel is null or becomes null
 - fixed bug if RecordingSeries.Name is null on GetChannels
 - fixed bug if RecordingChannel.Name is null on GetSeries
 - fixed bug if an item.value is Null on send the POST data for an update
 
v0.3.5.1
 - added TrustedDevice Property. 
   - The DMS has an option for trusted devices, with this username and password are not required.
 - added BypassLocalhost Property
   - If the DMS and an APP are running on the same machine, a bypass can be set that points 
   directly to the media file. This ensures that the wrapper, in Play, does not create playlists 
   as m3u for the UPnP streams. It will directly return the media file.

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

v0.0.1.0
- initial Version
- base functionality
  - Serverstatus
  - Recordings
  - Clients
