namespace DVBViewerServerApiWrapper.Helper
{
    /// <summary>
    /// Sammelsurium der einfachsten UriParameter, welche von der API benötigt werden.
    /// Bundle of the simplest UriParameters, needed by the API.
    /// </summary>
    public static class SUriParams
    {
        /// <summary>
        /// favonly = 1
        /// </summary>
        public static readonly UriParameter FavOnly1 = new UriParameter("favonly", "1");

        /// <summary>
        /// hls = 0
        /// </summary>
        public static readonly UriParameter HLS0 = new UriParameter("hls", "0");

        /// <summary>
        /// hls = 1
        /// </summary>
        public static readonly UriParameter HLS1 = new UriParameter("hls", "1");

        /// <summary>
        /// images = 1
        /// </summary>
        public static readonly UriParameter Images1 = new UriParameter("images", "1");

        /// <summary>
        /// nodesc = 1
        /// </summary>
        public static readonly UriParameter NoDesc1 = new UriParameter("nodesc", "1");

        /// <summary>
        /// nofilename = 1
        /// </summary>
        public static readonly UriParameter NoFileName1 = new UriParameter("nofilename", "1");

        /// <summary>
        /// photo = 1
        /// </summary>
        public static readonly UriParameter Photo1 = new UriParameter("photo", "1");

        /// <summary>
        /// radio = 1
        /// </summary>
        public static readonly UriParameter Radio1 = new UriParameter("radio", "1");

        /// <summary>
        /// rec = 1
        /// </summary>
        public static readonly UriParameter Rec1 = new UriParameter("rec", "1");

        /// <summary>
        /// rpreset = 1
        /// </summary>
        public static readonly UriParameter RPreset1 = new UriParameter("rpreset", "1");

        /// <summary>
        /// tv = 1
        /// </summary>
        public static readonly UriParameter TV1 = new UriParameter("tv", "1");

        /// <summary>
        /// tvpreset = 1
        /// </summary>
        public static readonly UriParameter TvPreset1 = new UriParameter("tvpreset", "1");

        /// <summary>
        /// utf8 = 1
        /// </summary>
        public static readonly UriParameter Utf81 = new UriParameter("utf8", "1");

        /// <summary>
        /// video = 1
        /// </summary>
        public static readonly UriParameter Video1 = new UriParameter("video", "1");
        /// <summary>
        /// audio = 0
        /// </summary>
        public static readonly UriParameter Audio0 = new UriParameter("audio", "0");

        /// <summary>
        /// audio = 1
        /// </summary>
        public static readonly UriParameter Audio1 = new UriParameter("audio", "1");

        /// <summary>
        /// eventid = 0
        /// </summary>
        public static readonly UriParameter EventID0 = new UriParameter("eventid", "0");

        /// <summary>
        /// eventid = 1
        /// </summary>
        public static readonly UriParameter EventID1 = new UriParameter("eventid", "1");
        /// <summary>
        /// fav = 1
        /// </summary>
        public static readonly UriParameter Fav1 = new UriParameter("fav", "1");

        /// <summary>
        /// fav = 0
        /// </summary>
        public static readonly UriParameter Fav0 = new UriParameter("fav", "0");

        /// <summary>
        /// favonly = 0
        /// </summary>
        public static readonly UriParameter FavOnly0 = new UriParameter("favonly", "0");

        /// <summary>
        /// images = 0
        /// </summary>
        public static readonly UriParameter Images0 = new UriParameter("images", "0");

        /// <summary>
        /// nodesc = 0
        /// </summary>
        public static readonly UriParameter NoDesc0 = new UriParameter("nodesc", "0");

        /// <summary>
        /// nofilename = 0
        /// </summary>
        public static readonly UriParameter NoFileName0 = new UriParameter("nofilename", "0");

        /// <summary>
        /// photo = 0
        /// </summary>
        public static readonly UriParameter Photo0 = new UriParameter("photo", "0");

        /// <summary>
        /// radio = 0
        /// </summary>
        public static readonly UriParameter Radio0 = new UriParameter("radio", "0");

        /// <summary>
        /// rec = 0
        /// </summary>
        public static readonly UriParameter Rec0 = new UriParameter("rec", "0");

        /// <summary>
        /// rpreset = 0
        /// </summary>
        public static readonly UriParameter RPreset0 = new UriParameter("rpreset", "0");

        /// <summary>
        /// tv = 0
        /// </summary>
        public static readonly UriParameter TV0 = new UriParameter("tv", "0");

        /// <summary>
        /// tvpreset = 0
        /// </summary>
        public static readonly UriParameter TvPreset0 = new UriParameter("tvpreset", "0");

        /// <summary>
        /// utf8 = 0
        /// </summary>
        public static readonly UriParameter Utf80 = new UriParameter("utf8", "0");

        /// <summary>
        /// video = 0
        /// </summary>
        public static readonly UriParameter Video0 = new UriParameter("video", "0");

        /// <summary>
        /// Gibt einen Uriparameter mit einer SQL Abfrage zurück. Returns a UriParameter with a SQL query.
        /// </summary>
        /// <param name="query">Die SQL Abfrage. The SQL Query</param>
        /// <returns></returns>
        public static UriParameter Query(string query)
        {
            return new UriParameter("query", query);
        }
    }
}