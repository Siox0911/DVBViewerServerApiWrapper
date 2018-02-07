using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVBViewerServerApiWrapper.Model
{
    ///<summary>
    /// Statische Klasse mit allen Enums
    ///</summary>
    public static class Enums
    {
        ///<summary>
        /// Zeigt an welcher Status das EPGUpdate hat
        /// Shows which status the EPGUpdate has
        ///</summary>
        public enum EPGUpdate
        {
            ///<summary>
            /// Inaktiv
            /// Inactiv
            ///</summary>
            inactive,
            ///<summary>
            /// Aktiv, läuft
            /// Activ, runs
            ///</summary>
            active,
            ///<summary>
            /// Wartet auf einen freien Tuner
            /// Wait for a free tuner
            ///</summary>
            waiting
        }

        /// <summary>
        /// Stellt die DVBViewerCommands dar, welche mit X in der Kommandozeile gestartet werden.
        /// Represents the DVBViewerCommands, which are started with X in the command line.
        /// </summary>
        public enum DVBViewerXCommand
        {
            /// <summary>
            /// Pause (Toggle)
            /// </summary>
            Pause = 0,
            /// <summary>
            /// Holt den DVBViewer dauerhaft in den Vordergrund. (Toggle)
            /// Command to present the window on topmost
            /// </summary>
            OnTop = 1,
            /// <summary>
            /// Menuleiste ein- und ausblenden
            /// Show and hide menu bar
            /// </summary>
            ShowMenu = 2,
            /// <summary>
            /// Statusbar ein- und ausblenden
            /// Show and hide status bar
            /// </summary>
            ShowStatusbar = 3,
            /// <summary>
            /// Toolbar ein- und ausblenden
            /// show and hide tool bar
            /// </summary>
            ShowToolbar = 4,
            /// <summary>
            /// Fullscreen (Toggle)
            /// </summary>
            Fullscreen = 5,
            /// <summary>
            /// DVBViewer beenden (mit vorheriger Bestätigung)
            /// Closing DVBViewer with confirmation
            /// </summary>
            Exit = 6,
            /// <summary>
            /// Senderlistenfenster (nicht im OSD)
            /// Channelwindow (no OSD)
            /// </summary>
            Channellist = 7,
            /// <summary>
            /// Programm - 
            /// </summary>
            ChanMinus = 8,
            /// <summary>
            /// Programm +
            /// </summary>
            ChanPlus = 9,
            /// <summary>
            /// öffnet den Senderlisteneditor 
            /// Channeleditor
            /// </summary>
            ChanSave = 10,
            /// <summary>
            /// Favoritenliste Pos. 1 
            /// Favorites list pos. 1
            /// </summary>
            Fav1 = 11,
            /// <summary>
            /// Favoritenliste Pos. 2
            /// Favorites list pos. 2
            /// </summary>
            Fav2 = 12,
            /// <summary>
            /// Favoritenliste Pos. 3
            /// Favorites list pos. 3
            /// </summary>
            Fav3 = 13,
            /// <summary>
            /// Favoritenliste Pos. 4
            /// Favorites list pos. 4
            /// </summary>
            Fav4 = 14,
            /// <summary>
            /// Favoritenliste Pos. 5
            /// Favorites list pos. 5
            /// </summary>
            Fav5 = 15,
            /// <summary>
            /// Favoritenliste Pos. 6
            /// Favorites list pos. 6
            /// </summary>
            Fav6 = 16,
            /// <summary>
            /// Favoritenliste Pos. 7
            /// Favorites list pos. 7
            /// </summary>
            Fav7 = 17,
            /// <summary>
            /// Favoritenliste Pos. 8
            /// Favorites list pos. 8
            /// </summary>
            Fav8 = 18,
            /// <summary>
            /// Favoritenliste Pos. 9
            /// Favorites list pos. 9
            /// </summary>
            Fav9 = 19,
            /// <summary>
            /// Favoriten + 
            /// </summary>
            FavPlus = 20,
            /// <summary>
            /// Favoriten -
            /// </summary>
            FavMinus = 21,
            /// <summary>
            /// Aspektumumschaltung (Aspect Ratio) (Toggle)
            /// toggles aspect ratio
            /// </summary>
            Aspect = 22,
            /// <summary>
            /// Einstellfenster für Bildlage und Größe
            /// Adjustment window for image position and size
            /// </summary>
            Zoom = 23,
            /// <summary>
            /// Optionendialog
            /// Options dialog
            /// </summary>
            Options = 24,
            /// <summary>
            /// Stummschaltung
            /// Mute
            /// </summary>
            Mute = 25,
            /// <summary>
            /// Lauter
            /// volume up
            /// </summary>
            VolUp = 26,
            /// <summary>
            /// Leiser
            /// volume down
            /// </summary>
            VolDown = 27,
            /// <summary>
            /// Einstellfenster für Helligkeit, Kontrast, Farbe, Sättigung
            /// Adjustment window for brightness, contrast, color, saturation
            /// </summary>
            Display = 28,
            /// <summary>
            /// Zoom 50%
            /// </summary>
            Zoom50 = 29,
            /// <summary>
            /// Zoom 75%
            /// </summary>
            Zoom75 = 30,
            /// <summary>
            /// Zoom 100%
            /// </summary>
            Zoom100 = 31,
            /// <summary>
            /// aktiviert den Desktop-Mode
            /// activates the desktop mode
            /// </summary>
            Desktop = 32,
            /// <summary>
            /// öffnet das Aufnahme-Programmierung Fenster (nicht OSD)
            /// opens the recording programming window (not OSD)
            /// </summary>
            RecordSettings = 33,
            /// <summary>
            /// Aufnahme starten
            /// start recording
            /// </summary>
            Record = 34,
            /// <summary>
            /// Teletextfenster öffnen (in separatem Windows-Fenster, nicht im OSD)
            /// Open Teletext window (in separate Windows window, not in the OSD)
            /// </summary>
            Teletext = 35,
            /// <summary>
            /// TV Programmführer öffnen (in separatem Windows-Fenster, nicht im OSD)
            /// Open TV program guide (in separate Windows window, not in OSD)
            /// </summary>
            EPG = 37,
            /// <summary>
            /// Favoritenliste Pos. 0
            /// Favorites list Pos. 0
            /// </summary>
            Fav0 = 38,
            /// <summary>
            /// TV-Kanalnummer 0 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// TV channel number 0 (press several numbers in quick succession for multi-digit channel positions)
            /// </summary>
            Channel0 = 40,
            /// <summary>
            /// TV-Kanalnummer 1 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// TV channel number 1 (pressing several numbers in quick succession for multi-digit channel positions)
            /// </summary>
            Channel1 = 41,
            /// <summary>
            /// TV-Kanalnummer 2 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// TV channel number 2 (pressing several numbers in quick succession for multi-digit channel positions)
            /// </summary>
            Channel2 = 42,
            /// <summary>
            /// TV-Kanalnummer 3 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// TV channel number 3 (pressing several numbers in quick succession for multi-digit channel positions)
            /// </summary>
            Channel3 = 43,
            /// <summary>
            /// TV-Kanalnummer 4 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// TV channel number 4 (pressing several numbers in quick succession for multi-digit channel positions)
            /// </summary>
            Channel4 = 44,
            /// <summary>
            /// TV-Kanalnummer 5 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// TV channel number 5 (pressing several numbers in quick succession for multi-digit channel positions)
            /// </summary>
            Channel5 = 45,
            /// <summary>
            /// TV-Kanalnummer 6 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// TV channel number 6 (pressing several numbers in quick succession for multi-digit channel positions)
            /// </summary>
            Channel6 = 46,
            /// <summary>
            /// TV-Kanalnummer 7 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// TV channel number 7 (pressing several numbers in quick succession for multi-digit channel positions)
            /// </summary>
            Channel7 = 47,
            /// <summary>
            /// TV-Kanalnummer 8 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// TV channel number 8 (pressing several numbers in quick succession for multi-digit channel positions)
            /// </summary>
            Channel8 = 48,
            /// <summary>
            /// TV-Kanalnummer 9 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// TV channel number 9 (pressing several numbers in quick succession for multi-digit channel positions)
            /// </summary>
            Channel9 = 49,
            /// <summary>
            /// startet Timeshiftaufnahme
            /// starts timeshift recording
            /// </summary>
            TimeShift = 50,
            /// <summary>
            /// Timeshift Fenster
            /// timeshift window
            /// </summary>
            TimeShiftWindow = 51,
            /// <summary>
            /// pausiert die Timeshiftwiedergabe
            /// pauses the timeshift playback
            /// </summary>
            TimeshiftStop = 52,
            /// <summary>
            /// Wiedergabe neu aufbauen
            /// Rebuild the replay
            /// </summary>
            RebuildGraph = 53,
            /// <summary>
            /// aus/einblenden der Titelleiste (oberste Leiste mit dem minimieren Button)
            /// hide / show the title bar (top bar with the minimize button)
            /// </summary>
            ShowTitlebar = 54,
            /// <summary>
            /// Helligkeit +
            /// Brightness +
            /// </summary>
            BrightnessUp = 55,
            /// <summary>
            /// Helligkeit -
            /// Brightness -
            /// </summary>
            BrightnessDown = 56,
            /// <summary>
            /// Sättigung +
            /// Saturation +
            /// </summary>
            SaturationUp = 57,
            /// <summary>
            /// Sättigung -
            /// Saturation -
            /// </summary>
            SaturationDown = 58,
            /// <summary>
            /// Kontrast +
            /// Contrast +
            /// </summary>
            ContrastUp = 59,
            /// <summary>
            /// Kontrast -
            /// Contrast -
            /// </summary>
            ContrastDown = 60,
            /// <summary>
            /// Farbton +
            /// Hue +
            /// </summary>
            HueUp = 61,
            /// <summary>
            /// Farbton -
            /// Hue -
            /// </summary>
            HueDown = 62,
            /// <summary>
            /// Letzter Kanal
            /// last channel
            /// </summary>
            LastChannel = 63,
            /// <summary>
            /// Öffnet Fenster zum Bearbeiten der Playlist
            /// Opens window for editing the playlist
            /// </summary>
            Playlist = 64,
            /// <summary>
            /// startet Wiedergabe der Playlist (immer vom Anfang, außer bei Zufallswiedergabe)
            /// starts playing the playlist (always from the beginning, except when playing randomly)
            /// </summary>
            PlaylistStart = 65,
            /// <summary>
            /// nächster Titel der Playlist
            /// next title of the playlist
            /// </summary>
            PlaylistNext = 66,
            /// <summary>
            /// vorheriger Titel der Playlist 
            /// previous track of the playlist
            /// </summary>
            PlaylistPrevious = 67,
            /// <summary>
            /// Playlist wird wiederholt (Endlosschleife)
            /// Playlist is repeated (endless loop)
            /// </summary>
            PlaylistLoop = 68,
            /// <summary>
            /// Playlistwiedergabe wird beendet
            /// Playlist play will stop
            /// </summary>
            PlaylistStop = 69,
            /// <summary>
            /// Playlist auf Zufallswiedergabe schalten
            /// Playlist switch to random play
            /// </summary>
            PlaylistRandom = 70,
            /// <summary>
            /// Videofenster 2 anzeigen (blendet alle Leisten aus) (toggle)
            /// Show video window 2 (hides all bars) (toggle)
            /// </summary>
            VideoWindow2 = 71,
            /// <summary>
            /// zwischen verschiedenen Audiokanälen umschalten
            /// switch between different audio channels
            /// </summary>
            AudioChannel = 72,
            /// <summary>
            /// OSD OK
            /// </summary>
            OSD_OK = 73,
            /// <summary>
            /// Red Button
            /// </summary>
            OSD_Red = 74,
            /// <summary>
            /// Green Button
            /// </summary>
            OSD_Green = 75,
            /// <summary>
            /// Yellow Button
            /// </summary>
            OSD_Yellow = 76,
            /// <summary>
            /// Blue Button
            /// </summary>
            OSD_Blue = 77,
            /// <summary>
            /// OSD Up
            /// </summary>
            OSD_Up = 78,
            /// <summary>
            /// OSD Down
            /// </summary>
            OSD_Down = 79,
            /// <summary>
            /// OSD First
            /// </summary>
            OSD_First = 80,
            /// <summary>
            /// OSD Last
            /// </summary>
            OSD_Last = 81,
            /// <summary>
            /// OSD Vorheriges
            /// OSD Previous
            /// </summary>
            OSD_Previous = 82,
            /// <summary>
            /// OSD Nächstes
            /// OSD Next
            /// </summary>
            OSD_Next = 83,
            /// <summary>
            /// OSD Beenden
            /// quit OSD
            /// </summary>
            OSD_Close = 84,
            /// <summary>
            /// passt DVBViewer Fenster an die TVBildgröße an
            /// adjusts DVBViewer window to the TV picture size
            /// </summary>
            BestWidth = 89,
            /// <summary>
            /// Abspielen, Fortsetzen
            /// Play, resume
            /// </summary>
            Play = 92,
            /// <summary>
            /// Datei öffnen, als weiteres Argument in "" Dateispeicherort inklusive Dateinamen angeben
            /// Open file, specify as additional argument in "" File location including file name
            /// </summary>
            OpenFile = 94,
            /// <summary>
            /// Stereo Toggle
            /// </summary>
            StereoLeftRight = 95,
            /// <summary>
            /// OSD Teletext
            /// </summary>
            OSD_Teletext = 101,
            /// <summary>
            /// Sprung von -10 Sekunden (bei mehrmaliger Anwendung kurz hintereinander erhöht sich die Sprungzeit -10,-30,-60,-120,-300 -Zeit kann 
            /// eingestellt werden sh. tweaks.txt)
            /// Jump of -10 seconds (with repeated use in quick succession the jump time increases -10, -30, -60, -120, -300 -time can be set sh. Tweaks.txt)
            /// </summary>
            JumpMinus10 = 102,
            /// <summary>
            /// Sprung von +10 Sekunden (bei mehrmaliger Anwendung kurz hintereinander erhöht sich die Sprungzeit +10,+30,+60,+120,+300 -Zeit 
            /// kann eingestellt werden sh. tweaks.txt)
            /// Jump of +10 seconds (with repeated use in quick succession the jump time increases + 10, + 30, + 60, + 120, + 300 -time can be adjusted see tweaks.txt)
            /// </summary>
            JumpPlus10 = 103,
            /// <summary>
            /// Zoom +
            /// </summary>
            ZoomUp = 104,
            /// <summary>
            /// Zoom -
            /// </summary>
            ZoomDown = 105,
            /// <summary>
            /// Bild in der Breite strecken +
            /// Stretch the image in width +
            /// </summary>
            StretchHUp = 106,
            /// <summary>
            /// Bild in der Breite strecken -
            /// Stretch the image in width -
            /// </summary>
            StretchHDown = 107,
            /// <summary>
            /// Bild in der Höhe strecken - (ja, hier ist up -)
            /// Stretch picture in height - (yes, here's up -)
            /// </summary>
            StretchVUp = 108,
            /// <summary>
            /// Bild in der Höhe strecken + (ja, hier ist down +) 
            /// Stretch picture in height + (yes, here is down +)
            /// </summary>
            StretchVDown = 109,
            /// <summary>
            /// Streckung zurücksetzten (auf Standard)
            /// Reset stretch (to default)
            /// </summary>
            StretchReset = 110,
            /// <summary>
            /// OSD Menü öffnen/schließen (toggle)
            /// Open / close OSD menu (toggle)
            /// </summary>
            OSD_Menu = 111,
            /// <summary>
            /// OSD Seite -, DVD Kapitel- / Video -1 Minute (bei Playlist vorheriger Titel)
            /// OSD page -, DVD chapter- / video -1 minute (at playlist previous title)
            /// </summary>
            Previous = 112,
            /// <summary>
            /// OSD Seite +, DVD Kapitel+ / Video +1 Minute (bei Playlist nächster Titel)
            /// OSD page +, DVD chapter + / video +1 minute (at playlist next title)
            /// </summary>
            Next = 113,
            /// <summary>
            /// Stopp
            /// </summary>
            Stop = 114,
            /// <summary>
            /// Screenshot aufnehmen
            /// Take screenshot
            /// </summary>
            Screenshot = 115,
            /// <summary>
            /// öffnet das Equalizerfenster (nicht OSD) (toggle)
            /// opens the equalizer window (not OSD) (toggle)
            /// </summary>
            Equalizer = 116,
            /// <summary>
            /// Senderlisteneditor (toggle)
            /// Sender list editor (toggle)
            /// </summary>
            ChannelEdit = 117,
            /// <summary>
            /// Letzte Datei abspielen
            /// Play last file
            /// </summary>
            LastFile = 118,
            /// <summary>
            /// Fenster für Sendersuchlauf (kein Toggle)
            /// Window for station search (no toggle)
            /// </summary>
            ChannelScan = 119,
            /// <summary>
            /// Normales Videofenster mit Leisten und ohne (toogle)
            /// Normal video window with bars and without (toggle)
            /// </summary>
            VideoWindow1 = 130,
            /// <summary>
            /// Radiofenster (toggle)
            /// Radio window (toggle)
            /// </summary>
            RadioWindow = 131,
            /// <summary>
            /// Schaltet zwischen Video A/B um (Optionendialog anschauen)
            /// Toggles between Video A / B (see Options dialog)
            /// </summary>
            VideoOutputAB = 132,
            /// <summary>
            /// Schaltet zwischen Audio A/B um (Optionendialog anschauen)
            /// Switches between Audio A / B (see option dialog)
            /// </summary>
            AudioOutputAB = 133,
            /// <summary>
            ///  Zeigt die Eigenschaftsseite des DVBViewer Filter an
            ///  Displays the property page of the DVBViewer Filter
            /// </summary>
            DVBSourceProperties = 134,
            /// <summary>
            /// Beendet Videoausgabe
            /// Ends video output
            /// </summary>
            CloseGraph = 135,
            /// <summary>
            /// Zeigt das Videofenster an
            /// Displays the video window
            /// </summary>
            ShowVideowindow = 821,
            /// <summary>
            /// OSD Links
            /// OSD Left
            /// </summary>
            OSD_Left = 2000,
            /// <summary>
            /// OSD Uhr
            /// OSD Clock
            /// </summary>
            OSD_Clock = 2010,
            /// <summary>
            /// öffnet das Aufnahme- und Gerätestatistikfenster (nicht OSD)
            /// opens the recording and device statistics window (not OSD)
            /// </summary>
            RecordedShowsandTimerstatistics = 2011,
            /// <summary>
            /// Timeshift Aufnahme beibehalten
            /// Timeshift recording preserve
            /// </summary>
            KeepTimeshiftFile = 2012,
            /// <summary>
            /// Zoom 60%
            /// </summary>
            Zoom60 = 2013,
            /// <summary>
            /// Deaktiviert den aktuell eingestellten Shader
            /// Disables the currently set shader
            /// </summary>
            ShaderNone = 2014,
            /// <summary>
            /// Aktiviert den Shader der zuletzt eingestellt war
            /// Enables the shader that was last set
            /// </summary>
            ShaderLast = 2015,
            /// <summary>
            /// Aktiviert denHbbTV Browser sofern das Addon installiert ist
            /// Activates the HbbTV browser if the addon is installed
            /// </summary>
            HbbTVBrowser = 2016,
            /// <summary>
            /// Videofenster an Videoauflösung anpassen
            /// Adjust video window to video resolution
            /// </summary>
            SourceResolution = 2017,
            /// <summary>
            /// OSD Rechts
            /// OSD Right
            /// </summary>
            OSD_Right = 2100,
            /// <summary>
            /// OSD MediaCenter Seite einblenden
            /// Show OSD MediaCenter page
            /// </summary>
            OSD_ShowHTPC = 2110,
            /// <summary>
            /// Schaltet OSD Background aus (und nicht mehr ein, besser Befehl Togglebackground  nutzen)
            /// Turn off OSD Background (and not more, better use Togglebackground command)
            /// </summary>
            OSD_BackgroundToggle = 8194,
            /// <summary>
            /// Liste der programmierten Aufnahmen (Timerfenster)
            /// List of programmed recordings (timer window)
            /// </summary>
            OSD_ShowTimer = 8195,
            /// <summary>
            /// OSD Fenster der Aufnahmen einblenden
            /// Show OSD window of recordings
            /// </summary>
            OSD_ShowRecordings = 8196,
            /// <summary>
            /// Programmführer für die aktuelle Zeit (wie OSDgrün)
            /// Program guide for the current time (like OSDgreen)
            /// </summary>
            OSD_ShowNow = 8197,
            /// <summary>
            /// Programmführer für den aktuellen Sender
            /// Program guide for the current station
            /// </summary>
            OSD_ShowEPG = 8198,
            /// <summary>
            /// TV Kanalliste
            /// TV channel list
            /// </summary>
            OSD_ShowChannels = 8199,
            /// <summary>
            /// TV Favoritenliste
            /// TV favorites list
            /// </summary>
            OSD_ShowFavourites = 8200,
            /// <summary>
            /// OSD Timeline-Fenster
            /// OSD timeline window
            /// </summary>
            OSD_ShowTimeline = 8201,
            /// <summary>
            /// OSD Fenster der Bilder einblenden
            /// Show OSD window of pictures
            /// </summary>
            OSD_ShowPicture = 8202,
            /// <summary>
            /// OSD Fenster der Musikdateien einblenden
            /// Show OSD window of music files
            /// </summary>
            OSD_ShowMusic = 8203,
            /// <summary>
            /// OSD Fenster der Videodateien einblenden
            /// Show OSD window of video files
            /// </summary>
            OSD_ShowVideo = 8204,
            /// <summary>
            /// OSD Fenster der Nachrichten einblenden (RSS Feeds)
            /// Show OSD Message Window (RSS Feeds)
            /// </summary>
            OSD_ShowNews = 8205,
            /// <summary>
            /// OSD Fenster des Wetters einblenden
            /// Show OSD windows of the weather
            /// </summary>
            OSD_ShowWeather = 8206,
            /// <summary>
            /// MiniEPG Fenster einblenden
            /// Show MiniEPG window
            /// </summary>
            OSD_ShowMiniepg = 8207,
            /// <summary>
            /// OSD Fenster zum durchsuchen der Laufwerke
            /// OSD window to browse the drives
            /// </summary>
            OSD_ShowComputer = 8210,
            /// <summary>
            /// Mosaikpreview an- und ausschalten
            /// Turn Mosaic Preview on and off
            /// </summary>
            ToggleMosaicpreview = 8211,
            /// <summary>
            /// OSD Fenster für gespeicherte Alarme (nicht Aufnahmen) einblenden
            /// Show OSD saved alarms window (not recordings)
            /// </summary>
            OSD_ShowAlarms = 8212,
            /// <summary>
            /// OSD Hilfefenster öffnen
            /// Open OSD help window
            /// </summary>
            Showhelp = 8213,
            /// <summary>
            /// Videofenster ausblenden
            /// Hide video window
            /// </summary>
            HideVideowindow = 8214,
            /// <summary>
            /// DVD Menu
            /// </summary>
            DVDMenu = 8246,
            /// <summary>
            /// Untertitelfenster
            /// Subtitle Window
            /// </summary>
            OSD_ShowSubtitlemenu = 8247,
            /// <summary>
            /// Audiospurfenster
            /// Audio track window
            /// </summary>
            OSD_ShowAudiomenu = 8248,
            /// <summary>
            /// DVD abspielen
            /// Play DVD
            /// </summary>
            PlayDVD = 8250,
            /// <summary>
            /// Portalauswahl für Portalsender (z.B. SKY)
            /// Portal selection for portal sender (e.g., SKY)
            /// </summary>
            Portalselect = 8254,
            /// <summary>
            /// Senderbenutzung löschen
            /// Delete station usage
            /// </summary>
            ClearChannelusagecounter = 8255,
            /// <summary>
            /// Renderer stoppen
            /// Stop renderer
            /// </summary>
            StopRenderer = 8256,
            /// <summary>
            /// Audio CD abspielen
            /// Play Audio CD
            /// </summary>
            PlayAudioCD = 8257,
            /// <summary>
            /// OSD Webcam Fenster
            /// OSD webcam window
            /// </summary>
            OSD_CAMWindow = 8259,
            /// <summary>
            /// Aufnahmedatenbank aktualisieren
            /// Update recording database
            /// </summary>
            RefreshRecDB = 8260,
            /// <summary>
            /// Aufnahmedatenbank bereinigen /aufräumen
            /// clean up the recording database
            /// </summary>
            CleanupRecDB = 8261,
            /// <summary>
            /// Aufnahmedatenbank komprimieren
            /// Compress the recording database
            /// </summary>
            CompressRecDB = 8262,
            /// <summary>
            /// Aufnahmedatenbank auffrischen, reinigen und komprimieren
            /// Refresh, cleanse and compress the recording database
            /// </summary>
            RefreshCleanupCompressRecDB = 8263,
            /// <summary>
            /// Blendet das detailierte EPG der aktuellen Sendung ein
            /// Shows the detailed EPG of the current program
            /// </summary>
            ShowCurrentInfo = 8264,
            /// <summary>
            /// Zeigt die OSD Radiokanalliste 
            /// Shows the OSD radio channel list
            /// </summary>
            ShowRadiolist = 8265,
            /// <summary>
            /// Schickt den PC in den StandBy, auf dem der Recording Service läuft mit dem der DVBViewer verbunden ist
            /// Send the PC to StandBy, where the recording service is running, to which the DVBViewer is connected
            /// </summary>
            ServiceStandby = 8272,
            /// <summary>
            /// Schaltet den PC aus, auf dem der Recording Service läuft mit dem der DVBViewer verbunden ist
            /// Turns off the PC running the recording service the DVBViewer is connected to
            /// </summary>
            ServiceShutdownPC = 8273,
            /// <summary>
            /// Schickt den PC in den Ruhezustand, auf dem der Recording Service läuft mit dem der DVBViewer verbunden ist 
            /// Sends the PC into hibernation, where the recording service is running, to which the DVBViewer is connected
            /// </summary>
            ServiceHibernate = 8274,
            /// <summary>
            /// Weckt den PC per WOL, auf dem der Recording Service installiert ist mit dem der DVBViewer verbunden ist
            /// Wakes up the PC via WOL on which the recording service is installed with which the DVBViewer is connected
            /// </summary>
            ServiceWakeonLAN = 8275,
            /// <summary>
            /// Ruft vom verbunden Recording Service die EPG Daten erneut ab
            /// Recalls the EPG data from the connected Recording Service
            /// </summary>
            ServicegetEPG = 8276,
            /// <summary>
            /// Zeigt die SysInfo OSD-Seite an 
            /// Displays the SysInfo OSD page
            /// </summary>
            OSD_ShowSysInfo = 8277,
            /// <summary>
            /// Ändert die Darstellung im OSD (Musik,Bilder,Video) zwischen kleine Icons, große Icons und Listenansicht durch.
            /// Changes the appearance in the OSD (music, pictures, video) between small icons, large icons and list view.
            /// </summary>
            OSDToggleView = 8278,
            /// <summary>
            /// Ändert die Sortierkriterien im OSD (Musik,Bilder,Video) zwischen Name, Datum, Größe usw. durch.
            /// Changes the sorting criteria in the OSD (music, images, video) between name, date, size, etc.
            /// </summary>
            OSDToggleSort = 8279,
            /// <summary>
            /// Ändert die Sortierreihenfolge im OSD (Musik,Bilder,Video) A-Z, Z-A.
            /// Changes the sort order in the OSD (Music, Pictures, Video) A-Z, Z-A.
            /// </summary>
            OSDToggleSortdirection = 8280,
            /// <summary>
            /// OSD Hintergrund ein/ausschalten
            /// Switch OSD background on / off
            /// </summary>
            Togglebackground = 12297,
            /// <summary>
            /// DVD auswerfen
            /// Eject the DVD
            /// </summary>
            EjectCD = 12299,
            /// <summary>
            /// schneller Vorlauf 
            /// fast forward
            /// </summary>
            Forward = 12304,
            /// <summary>
            /// schneller Rücklauf 
            /// fast return
            /// </summary>
            Rewind = 12305,
            /// <summary>
            /// Setzt ein Bookmark in der Videodatei/DVD 
            /// Sets a bookmark in the video file / DVD
            /// </summary>
            AddBookmark = 12306,
            /// <summary>
            /// PC in Ruhemodus versetzen 
            /// Hibernating the PC
            /// </summary>
            Hibernate = 12323,
            /// <summary>
            /// PC in Standbymodus versetzen
            /// Put the PC in standby mode
            /// </summary>
            Standby = 12324,
            /// <summary>
            /// PC ausschalten
            /// turn off the computer
            /// </summary>
            PowerOff = 12325,
            /// <summary>
            /// PC ausschalten
            /// turn off the computer
            /// </summary>
            Slumbermode = 12325,
            /// <summary>
            /// DVBViewer wird ohne Nachfrage beendet 
            /// DVBViewer will quit without asking
            /// </summary>
            CloseDVBViewer = 12326,
            /// <summary>
            /// TV Karte abschalten
            /// Switch off the TV card
            /// </summary>
            ShutdownCard = 12327,
            /// <summary>
            /// Monitorsignal abschalten
            /// Switch off the monitor signal
            /// </summary>
            ShutdownMonitor = 12328,
            /// <summary>
            /// Startet den PC neu
            /// Restart the PC
            /// </summary>
            Reboot = 12329,
            /// <summary>
            /// Abspielgeschwindigkeit in Schritten erhöhen (bei DVD, bis 8x, Videounterstützung nicht bei allen Dateiformaten) 
            /// Increase playback speed in steps (for DVD, up to 8x, video support not for all file formats)
            /// </summary>
            Speedup = 12382,
            /// <summary>
            /// Abspielgeschwindigkeit in Schritten verringern (bei DVD, bis 8x, Videounterstützung nicht bei allen Dateiformaten)
            /// Reduce playback speed in steps (for DVD, up to 8x, video support not for all file formats)
            /// </summary>
            Speeddown = 12383,
            /// <summary>
            /// OSD Playlistfenster öffnen 
            /// Open OSD playlist window
            /// </summary>
            ShowPlaylist = 12384,
            /// <summary>
            /// Minimiert den DVBViewer 
            /// Minimizes the DVBViewer
            /// </summary>
            WindowMinimize = 16382,
            /// <summary>
            /// Der Wiedergabegraph wird gestoppt 
            /// The playback graph is stopped
            /// </summary>
            StopGraph = 16383,
            /// <summary>
            /// zeigt die DVBViewer-Version als OSD Einblendung an 
            /// displays the DVBViewer version as OSD overlay
            /// </summary>
            ShowVersion = 16384,
            /// <summary>
            /// Audio deaktivieren 
            /// Disable audio
            /// </summary>
            DisableAudio = 16385,
            /// <summary>
            /// Audio/Video deaktivieren 
            /// Disable audio / video
            /// </summary>
            DisableAudioVideo = 16386,
            /// <summary>
            /// Video deaktivieren 
            /// Disable video
            /// </summary>
            DisableVideo = 16387,
            /// <summary>
            /// Audio/Video aktivieren 
            /// Enable audio / video
            /// </summary>
            EnableAudioVideo = 16388,
            /// <summary>
            /// Zoom auf Normal setzen
            /// Set zoom to normal
            /// </summary>
            ZoomlevelStandard = 16389,
            /// <summary>
            /// Zoomlevel bzw. Zoompreset 0 (je eine Einstellung für 4:3 und 16:9)
            /// Zoomlevel or Zoompreset 0 (one setting each for 4: 3 and 16: 9)
            /// </summary>
            Zoomlevel0 = 16390,
            /// <summary>
            /// Zoomlevel bzw. Zoompreset 1 (je eine Einstellung für 4:3 und 16:9) 
            /// Zoomlevel or Zoompreset 1 (one setting each for 4: 3 and 16: 9)
            /// </summary>
            Zoomlevel1 = 16391,
            /// <summary>
            /// Zoomlevel bzw. Zoompreset 2 (je eine Einstellung für 4:3 und 16:9)
            /// Zoomlevel or Zoompreset 2 (one setting each for 4: 3 and 16: 9)
            /// </summary>
            Zoomlevel2 = 16392,
            /// <summary>
            /// Zoomlevel bzw. Zoompreset 3 (je eine Einstellung für 4:3 und 16:9) 
            /// Zoomlevel or Zoompreset 3 (one setting each for 4: 3 and 16: 9)
            /// </summary>
            Zoomlevel3 = 16393,
            /// <summary>
            /// durch die ZoomPresets 0 - 3 springen (für 4:3 und 16:9 sind verschiedene Einstellungen möglich was insgesamt 8 Presets ergibt) 
            /// toggle through the zoom presets 0 - 3 (for 4: 3 and 16: 9 different settings are possible giving a total of 8 presets)
            /// </summary>
            ZoomlevelToggle = 16394,
            /// <summary>
            /// ein/ausblenden des Bild in Bild Fensters 
            /// show / hide the picture in picture window
            /// </summary>
            TogglePreview = 16395,
            /// <summary>
            /// setzt Helligkeit, Sättigung, Kontrast, Farbe auf die Standardeinstellungen 
            /// sets brightness, saturation, contrast, color to the default settings
            /// </summary>
            RestoreDefaultColors = 16396,
            /// <summary>
            /// hebt die Minimierung wieder auf (vorherige Fenstergröße wird wieder hergestellt) 
            /// removes the minimization again (previous window size is restored)
            /// </summary>
            WindowRestore = 16397,
            /// <summary>
            /// togglet das Statistik OSD vom Custom EVR und MadVR
            /// toggles the statistics OSD from Custom EVR and MadVR
            /// </summary>
            ToggleRendererStats = 16398
        }
    }
}
