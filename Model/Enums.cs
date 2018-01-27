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
        ///</summary>
        public enum EPGUpdate
        {
            ///<summary>
            /// Inaktiv
            ///</summary>
            inactive,
            ///<summary>
            /// Aktiv, läuft
            ///</summary>
            active,
            ///<summary>
            /// Wartet auf einen freien Tuner
            ///</summary>
            waiting
        }

        /// <summary>
        /// Stellt die DVBViewerCommands dar, welche mit X in der Kommandozeile gestartet werden.
        /// </summary>
        public enum DVBViewerXCommand
        {
            /// <summary>
            /// Pause (Toggle)
            /// </summary>
            Pause = 0,
            /// <summary>
            /// Holt den DVBViewer dauerhaft in den Vordergrund. (Toggle)
            /// </summary>
            OnTop = 1,
            /// <summary>
            /// Menuleiste ein- und ausblenden
            /// </summary>
            ShowMenu = 2,
            /// <summary>
            /// Statusbar ein- und ausblenden
            /// </summary>
            ShowStatusbar = 3,
            /// <summary>
            /// Toolbar ein- und ausblenden
            /// </summary>
            ShowToolbar = 4,
            /// <summary>
            /// Fullscreen (Toggle)
            /// </summary>
            Fullscreen = 5,
            /// <summary>
            /// DVBViewer beenden (mit vorheriger Bestätigung)
            /// </summary>
            Exit = 6,
            /// <summary>
            /// Senderlistenfenster (nicht im OSD)
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
            /// </summary>
            ChanSave = 10,
            /// <summary>
            /// Favoritenliste Pos. 1 
            /// </summary>
            Fav1 = 11,
            /// <summary>
            /// Favoritenliste Pos. 2 
            /// </summary>
            Fav2 = 12,
            /// <summary>
            /// Favoritenliste Pos. 3
            /// </summary>
            Fav3 = 13,
            /// <summary>
            /// Favoritenliste Pos. 4 
            /// </summary>
            Fav4 = 14,
            /// <summary>
            /// Favoritenliste Pos. 5 
            /// </summary>
            Fav5 = 15,
            /// <summary>
            /// Favoritenliste Pos. 6 
            /// </summary>
            Fav6 = 16,
            /// <summary>
            /// Favoritenliste Pos. 7 
            /// </summary>
            Fav7 = 17,
            /// <summary>
            /// Favoritenliste Pos. 8 
            /// </summary>
            Fav8 = 18,
            /// <summary>
            /// Favoritenliste Pos. 9 
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
            /// </summary>
            Aspect = 22,
            /// <summary>
            /// Einstellfenster für Bildlage und Größe
            /// </summary>
            Zoom = 23,
            /// <summary>
            /// Optionendialog
            /// </summary>
            Options = 24,
            /// <summary>
            /// Stummschaltung
            /// </summary>
            Mute = 25,
            /// <summary>
            /// Lauter
            /// </summary>
            VolUp = 26,
            /// <summary>
            /// Leiser
            /// </summary>
            VolDown = 27,
            /// <summary>
            /// Einstellfenster für Helligkeit, Kontrast, Farbe, Sättigung
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
            /// </summary>
            Desktop = 32,
            /// <summary>
            /// öffnet das Aufnahme-Programmierung Fenster (nicht OSD) 
            /// </summary>
            RecordSettings = 33,
            /// <summary>
            /// Aufnahme starten
            /// </summary>
            Record = 34,
            /// <summary>
            /// Teletextfenster öffnen (in separatem Windows-Fenster, nicht im OSD) 
            /// </summary>
            Teletext = 35,
            /// <summary>
            /// TV Programmführer öffnen (in separatem Windows-Fenster, nicht im OSD) 
            /// </summary>
            EPG = 37,
            /// <summary>
            /// Favoritenliste Pos. 0
            /// </summary>
            Fav0 = 38,
            /// <summary>
            /// TV-Kanalnummer 0 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// </summary>
            Channel0 = 40,
            /// <summary>
            /// TV-Kanalnummer 1 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// </summary>
            Channel1 = 41,
            /// <summary>
            /// TV-Kanalnummer 2 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// </summary>
            Channel2 = 42,
            /// <summary>
            /// TV-Kanalnummer 3 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// </summary>
            Channel3 = 43,
            /// <summary>
            /// TV-Kanalnummer 4 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// </summary>
            Channel4 = 44,
            /// <summary>
            /// TV-Kanalnummer 5 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// </summary>
            Channel5 = 45,
            /// <summary>
            /// TV-Kanalnummer 6 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// </summary>
            Channel6 = 46,
            /// <summary>
            /// TV-Kanalnummer 7 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// </summary>
            Channel7 = 47,
            /// <summary>
            /// TV-Kanalnummer 8 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// </summary>
            Channel8 = 48,
            /// <summary>
            /// TV-Kanalnummer 9 (drücken mehrerer Nummern kurz hintereinander für mehrstellige Kanalplätze)
            /// </summary>
            Channel9 = 49,
            /// <summary>
            /// startet Timeshiftaufnahme
            /// </summary>
            TimeShift = 50,
            /// <summary>
            /// Timeshift Fenster
            /// </summary>
            TimeShiftWindow = 51,
            /// <summary>
            /// pausiert die Timeshiftwiedergabe
            /// </summary>
            TimeshiftStop = 52,
            /// <summary>
            /// Wiedergabe neu aufbauen
            /// </summary>
            RebuildGraph = 53,
            /// <summary>
            /// aus/einblenden der Titelleiste (oberste Leiste mit dem minimieren Button)
            /// </summary>
            ShowTitlebar = 54,
            /// <summary>
            /// Helligkeit +
            /// </summary>
            BrightnessUp = 55,
            /// <summary>
            /// Helligkeit -
            /// </summary>
            BrightnessDown = 56,
            /// <summary>
            /// Sättigung +
            /// </summary>
            SaturationUp = 57,
            /// <summary>
            /// Sättigung -
            /// </summary>
            SaturationDown = 58,
            /// <summary>
            /// Kontrast +
            /// </summary>
            ContrastUp = 59,
            /// <summary>
            /// Kontrast -
            /// </summary>
            ContrastDown = 60,
            /// <summary>
            /// Farbton +
            /// </summary>
            HueUp = 61,
            /// <summary>
            /// Farbton -
            /// </summary>
            HueDown = 62,
            /// <summary>
            /// Letzter Kanal
            /// </summary>
            LastChannel = 63,
            /// <summary>
            /// Öffnet Fenster zum Bearbeiten der Playlist
            /// </summary>
            Playlist = 64,
            /// <summary>
            /// startet Wiedergabe der Playlist (immer vom Anfang, außer bei Zufallswiedergabe)
            /// </summary>
            PlaylistStart = 65,
            /// <summary>
            /// nächster Titel der Playlist 
            /// </summary>
            PlaylistNext = 66,
            /// <summary>
            /// vorheriger Titel der Playlist 
            /// </summary>
            PlaylistPrevious = 67,
            /// <summary>
            /// Playlist wird wiederholt (Endlosschleife)
            /// </summary>
            PlaylistLoop = 68,
            /// <summary>
            /// Playlistwiedergabe wird beendet
            /// </summary>
            PlaylistStop = 69,
            /// <summary>
            /// Playlist auf Zufallswiedergabe schalten
            /// </summary>
            PlaylistRandom = 70,
            /// <summary>
            /// Videofenster 2 anzeigen (blendet alle Leisten aus) (toggle)
            /// </summary>
            VideoWindow2 = 71,
            /// <summary>
            /// zwischen verschiedenen Audiokanälen umschalten
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
            /// </summary>
            OSD_Previous = 82,
            /// <summary>
            /// OSD Nächstes
            /// </summary>
            OSD_Next = 83,
            /// <summary>
            /// OSD Beenden
            /// </summary>
            OSD_Close = 84,
            /// <summary>
            /// passt DVBViewer Fenster an die TVBildgröße an 
            /// </summary>
            BestWidth = 89,
            /// <summary>
            /// Abspielen, Fortsetzen
            /// </summary>
            Play = 92,
            /// <summary>
            /// Datei öffnen, als weiteres Argument in "" Dateispeicherort inklusive Dateinamen angeben
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
            /// </summary>
            JumpMinus10 = 102,
            /// <summary>
            /// Sprung von +10 Sekunden (bei mehrmaliger Anwendung kurz hintereinander erhöht sich die Sprungzeit +10,+30,+60,+120,+300 -Zeit 
            /// kann eingestellt werden sh. tweaks.txt) 
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
            /// </summary>
            StretchHUp = 106,
            /// <summary>
            /// Bild in der Breite strecken - 
            /// </summary>
            StretchHDown = 107,
            /// <summary>
            /// Bild in der Höhe strecken - (ja, hier ist up -) 
            /// </summary>
            StretchVUp = 108,
            /// <summary>
            /// Bild in der Höhe strecken + (ja, hier ist down +) 
            /// </summary>
            StretchVDown = 109,
            /// <summary>
            /// Streckung zurücksetzten (auf Standard)
            /// </summary>
            StretchReset = 110,
            /// <summary>
            /// OSD Menü öffnen/schließen (toggle)
            /// </summary>
            OSD_Menu = 111,
            /// <summary>
            /// OSD Seite -, DVD Kapitel- / Video -1 Minute (bei Playlist vorheriger Titel) 
            /// </summary>
            Previous = 112,
            /// <summary>
            /// OSD Seite +, DVD Kapitel+ / Video +1 Minute (bei Playlist nächster Titel) 
            /// </summary>
            Next = 113,
            /// <summary>
            /// Stopp
            /// </summary>
            Stop = 114,
            /// <summary>
            /// Screenshot aufnehmen
            /// </summary>
            Screenshot = 115,
            /// <summary>
            /// öffnet das Equalizerfenster (nicht OSD) (toggle)
            /// </summary>
            Equalizer = 116,
            /// <summary>
            /// Senderlisteneditor (toggle)
            /// </summary>
            ChannelEdit = 117,
            /// <summary>
            /// Letzte Datei abspielen
            /// </summary>
            LastFile = 118,
            /// <summary>
            /// Fenster für Sendersuchlauf (kein Toggle)
            /// </summary>
            ChannelScan = 119,
            /// <summary>
            /// Normales Videofenster mit Leisten und ohne (toogle)
            /// </summary>
            VideoWindow1 = 130,
            /// <summary>
            /// Radiofenster (toggle)
            /// </summary>
            RadioWindow = 131,
            /// <summary>
            /// Schaltet zwischen Video A/B um (Optionendialog anschauen)
            /// </summary>
            VideoOutputAB = 132,
            /// <summary>
            /// Schaltet zwischen Audio A/B um (Optionendialog anschauen)
            /// </summary>
            AudioOutputAB = 133,
            /// <summary>
            ///  Zeigt die Eigenschaftsseite des DVBViewer Filter an
            /// </summary>
            DVBSourceProperties = 134,
            /// <summary>
            /// Beendet Videoausgabe 
            /// </summary>
            CloseGraph = 135,
            /// <summary>
            /// Zeigt das Videofenster an
            /// </summary>
            ShowVideowindow = 821,
            /// <summary>
            /// OSD LInks
            /// </summary>
            OSD_Left = 2000,
            /// <summary>
            /// OSD Uhr
            /// </summary>
            OSD_Clock = 2010,
            /// <summary>
            /// öffnet das Aufnahme- und Gerätestatistikfenster (nicht OSD)
            /// </summary>
            RecordedShowsandTimerstatistics = 2011,
            /// <summary>
            /// Timeshift Aufnahme beibehalten
            /// </summary>
            KeepTimeshiftFile = 2012,
            /// <summary>
            /// Zoom 60%
            /// </summary>
            Zoom60 = 2013,
            /// <summary>
            /// Deaktiviert den aktuell eingestellten Shader
            /// </summary>
            ShaderNone = 2014,
            /// <summary>
            /// Aktiviert den Shader der zuletzt eingestellt war
            /// </summary>
            ShaderLast = 2015,
            /// <summary>
            /// Aktiviert denHbbTV Browser sofern das Addon installiert ist 
            /// </summary>
            HbbTVBrowser = 2016,
            /// <summary>
            /// Videofenster an Videoauflösung anpassen
            /// </summary>
            SourceResolution = 2017,
            /// <summary>
            /// OSD Rechts
            /// </summary>
            OSD_Right = 2100,
            /// <summary>
            /// OSD MediaCenter Seite einblenden 
            /// </summary>
            OSD_ShowHTPC = 2110,
            /// <summary>
            /// Schaltet OSD Background aus (und nicht mehr ein, besser Befehl Togglebackground  nutzen) 
            /// </summary>
            OSD_BackgroundToggle = 8194,
            /// <summary>
            /// Liste der programmierten Aufnahmen (Timerfenster)
            /// </summary>
            OSD_ShowTimer = 8195,
            /// <summary>
            /// OSD Fenster der Aufnahmen einblenden
            /// </summary>
            OSD_ShowRecordings = 8196,
            /// <summary>
            /// Programmführer für die aktuelle Zeit (wie OSDgrün) 
            /// </summary>
            OSD_ShowNow = 8197,
            /// <summary>
            /// Programmführer für den aktuellen Sender 
            /// </summary>
            OSD_ShowEPG = 8198,
            /// <summary>
            /// TV Kanalliste 
            /// </summary>
            OSD_ShowChannels = 8199,
            /// <summary>
            /// TV Favoritenliste 
            /// </summary>
            OSD_ShowFavourites = 8200,
            /// <summary>
            /// OSD Timeline-Fenster 
            /// </summary>
            OSD_ShowTimeline = 8201,
            /// <summary>
            /// OSD Fenster der Bilder einblenden 
            /// </summary>
            OSD_ShowPicture = 8202,
            /// <summary>
            /// OSD Fenster der Musikdateien einblenden 
            /// </summary>
            OSD_ShowMusic = 8203,
            /// <summary>
            /// OSD Fenster der Videodateien einblenden 
            /// </summary>
            OSD_ShowVideo = 8204,
            /// <summary>
            /// OSD Fenster der Nachrichten einblenden (RSS Feeds) 
            /// </summary>
            OSD_ShowNews = 8205,
            /// <summary>
            /// OSD Fenster des Wetters einblenden 
            /// </summary>
            OSD_ShowWeather = 8206,
            /// <summary>
            /// MiniEPG Fenster einblenden 
            /// </summary>
            OSD_ShowMiniepg = 8207,
            /// <summary>
            /// OSD Fenster zum durchsuchen der Laufwerke 
            /// </summary>
            OSD_ShowComputer = 8210,
            /// <summary>
            /// Mosaikpreview an- und ausschalten
            /// </summary>
            ToggleMosaicpreview = 8211,
            /// <summary>
            /// OSD Fenster für gespeicherte Alarme (nicht Aufnahmen) einblenden 
            /// </summary>
            OSD_ShowAlarms = 8212,
            /// <summary>
            /// OSD Hilfefenster öffnen 
            /// </summary>
            Showhelp = 8213,
            /// <summary>
            /// Videofenster ausblenden
            /// </summary>
            HideVideowindow = 8214,
            /// <summary>
            /// DVD Menu
            /// </summary>
            DVDMenu = 8246,
            /// <summary>
            /// Untertitelfenster
            /// </summary>
            OSD_ShowSubtitlemenu = 8247,
            /// <summary>
            /// Audiospurfenster
            /// </summary>
            OSD_ShowAudiomenu = 8248,
            /// <summary>
            /// DVD abspielen
            /// </summary>
            PlayDVD = 8250,
            /// <summary>
            /// Portalauswahl für Portalsender (z.B. SKY)
            /// </summary>
            Portalselect = 8254,
            /// <summary>
            /// Senderbenutzung löschen
            /// </summary>
            ClearChannelusagecounter = 8255,
            /// <summary>
            /// Renderer stoppen
            /// </summary>
            StopRenderer = 8256,
            /// <summary>
            /// Audio CD abspielen
            /// </summary>
            PlayAudioCD = 8257,
            /// <summary>
            /// OSD Webcam Fenster
            /// </summary>
            OSD_CAMWindow = 8259,
            /// <summary>
            /// Aufnahmedatenbank aktualisieren
            /// </summary>
            RefreshRecDB = 8260,
            /// <summary>
            /// Aufnahmedatenbank bereinigen /aufräumen
            /// </summary>
            CleanupRecDB = 8261,
            /// <summary>
            /// Aufnahmedatenbank komprimieren 
            /// </summary>
            CompressRecDB = 8262,
            /// <summary>
            /// Aufnahmedatenbank auffrischen, reinigen und komprimieren 
            /// </summary>
            RefreshCleanupCompressRecDB = 8263,
            /// <summary>
            /// Blendet das detailierte EPG der aktuellen Sendung ein
            /// </summary>
            ShowCurrentInfo = 8264,
            /// <summary>
            /// Zeigt die OSD Radiokanalliste 
            /// </summary>
            ShowRadiolist = 8265,
            /// <summary>
            /// Schickt den PC in den StandBy, auf dem der Recording Service läuft mit dem der DVBViewer verbunden ist 
            /// </summary>
            ServiceStandby = 8272,
            /// <summary>
            /// Schaltet den PC aus, auf dem der Recording Service läuft mit dem der DVBViewer verbunden ist 
            /// </summary>
            ServiceShutdownPC = 8273,
            /// <summary>
            /// Schickt den PC in den Ruhezustand, auf dem der Recording Service läuft mit dem der DVBViewer verbunden ist 
            /// </summary>
            ServiceHibernate = 8274,
            /// <summary>
            /// Weckt den PC per WOL, auf dem der Recording Service installiert ist mit dem der DVBViewer verbunden ist 
            /// </summary>
            ServiceWakeonLAN = 8275,
            /// <summary>
            /// Ruft vom verbunden Recording Service die EPG Daten erneut ab 
            /// </summary>
            ServicegetEPG = 8276,
            /// <summary>
            /// Zeigt die SysInfo OSD-Seite an 
            /// </summary>
            OSD_ShowSysInfo = 8277,
            /// <summary>
            /// Ändert die Darstellung im OSD (Musik,Bilder,Video) zwischen kleine Icons, große Icons und Listenansicht durch. 
            /// </summary>
            OSDToggleView = 8278,
            /// <summary>
            /// Ändert die Sortierkriterien im OSD (Musik,Bilder,Video) zwischen Name, Datum, Größe usw. durch. 
            /// </summary>
            OSDToggleSort = 8279,
            /// <summary>
            /// Ändert die Sortierreihenfolge im OSD (Musik,Bilder,Video) A-Z, Z-A. 
            /// </summary>
            OSDToggleSortdirection = 8280,
            /// <summary>
            /// OSD Hintergrund ein/ausschalten
            /// </summary>
            Togglebackground = 12297,
            /// <summary>
            /// DVD auswerfen
            /// </summary>
            EjectCD = 12299,
            /// <summary>
            /// schneller Vorlauf 
            /// </summary>
            Forward = 12304,
            /// <summary>
            /// schneller Rücklauf 
            /// </summary>
            Rewind = 12305,
            /// <summary>
            /// Setzt ein Bookmark in der Videodatei/DVD 
            /// </summary>
            AddBookmark = 12306,
            /// <summary>
            /// PC in Ruhemodus versetzen 
            /// </summary>
            Hibernate = 12323,
            /// <summary>
            /// PC in Standbymodus versetzen 
            /// </summary>
            Standby = 12324,
            /// <summary>
            /// PC ausschalten
            /// </summary>
            PowerOff = 12325,
            /// <summary>
            /// PC ausschalten
            /// </summary>
            Slumbermode = 12325,
            /// <summary>
            /// DVBViewer wird ohne Nachfrage beendet 
            /// </summary>
            CloseDVBViewer = 12326,
            /// <summary>
            /// TV Karte abschalten
            /// </summary>
            ShutdownCard = 12327,
            /// <summary>
            /// Monitorsignal abschalten
            /// </summary>
            ShutdownMonitor = 12328,
            /// <summary>
            /// Startet den PC neu
            /// </summary>
            Reboot = 12329,
            /// <summary>
            /// Abspielgeschwindigkeit in Schritten erhöhen (bei DVD, bis 8x, Videounterstützung nicht bei allen Dateiformaten) 
            /// </summary>
            Speedup = 12382,
            /// <summary>
            /// Abspielgeschwindigkeit in Schritten verringern (bei DVD, bis 8x, Videounterstützung nicht bei allen Dateiformaten) 
            /// </summary>
            Speeddown = 12383,
            /// <summary>
            /// OSD Playlistfenster öffnen 
            /// </summary>
            ShowPlaylist = 12384,
            /// <summary>
            /// Minimiert den DVBViewer 
            /// </summary>
            WindowMinimize = 16382,
            /// <summary>
            /// Der Wiedergabegraph wird gestoppt 
            /// </summary>
            StopGraph = 16383,
            /// <summary>
            /// zeigt die DVBViewer-Version als OSD Einblendung an 
            /// </summary>
            ShowVersion = 16384,
            /// <summary>
            /// Audio deaktivieren 
            /// </summary>
            DisableAudio = 16385,
            /// <summary>
            /// Audio/Video deaktivieren 
            /// </summary>
            DisableAudioVideo = 16386,
            /// <summary>
            /// Video deaktivieren 
            /// </summary>
            DisableVideo = 16387,
            /// <summary>
            /// Audio/Video aktivieren 
            /// </summary>
            EnableAudioVideo = 16388,
            /// <summary>
            /// Zoom auf Normal setzen
            /// </summary>
            ZoomlevelStandard = 16389,
            /// <summary>
            /// Zoomlevel bzw. Zoompreset 0 (je eine Einstellung für 4:3 und 16:9) 
            /// </summary>
            Zoomlevel0 = 16390,
            /// <summary>
            /// Zoomlevel bzw. Zoompreset 1 (je eine Einstellung für 4:3 und 16:9) 
            /// </summary>
            Zoomlevel1 = 16391,
            /// <summary>
            /// Zoomlevel bzw. Zoompreset 2 (je eine Einstellung für 4:3 und 16:9)
            /// </summary>
            Zoomlevel2 = 16392,
            /// <summary>
            /// Zoomlevel bzw. Zoompreset 3 (je eine Einstellung für 4:3 und 16:9) 
            /// </summary>
            Zoomlevel3 = 16393,
            /// <summary>
            /// durch die ZoomPresets 0 - 3 springen (für 4:3 und 16:9 sind verschiedene Einstellungen möglich was insgesamt 8 Presets ergibt) 
            /// </summary>
            ZoomlevelToggle = 16394,
            /// <summary>
            /// ein/ausblenden des Bild in Bild Fensters 
            /// </summary>
            TogglePreview = 16395,
            /// <summary>
            /// setzt Helligkeit, Sättigung, Kontrast, Farbe auf die Standardeinstellungen 
            /// </summary>
            RestoreDefaultColors = 16396,
            /// <summary>
            /// hebt die Minimierung wieder auf (vorherige Fenstergröße wird wieder hergestellt) 
            /// </summary>
            WindowRestore = 16397,
            /// <summary>
            /// togglet das Statistik OSD vom Custom EVR und MadVR  
            /// </summary>
            ToggleRendererStats = 16398
        }
    }
}
