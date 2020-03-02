using System;

namespace MediaLibrary.ViewModel
{
    class Album : MultimediaItem
    {
        public string Artist { get; private set; }
        public string Duration { get; private set; }

        /// <summary>
        /// Album constructor..
        /// </summary>
        public Album(string[] data)
            : base(data) // ..will let base MultimediaItem constructor to initialize Title, Year and IsLent...
        {
            // ..and then will initialize its own fields: Artist and Duration
            Artist = data[2];
            Duration = data[3];
        }

        /// <summary>
        /// Using Polymorphism, this method is called when the base class
        /// cannot find the property the user is asking for
        /// among the default ones (Title, Year, IsLent)
        /// so we're looking into Album-specific properties
        /// </summary>
        protected override string GetMediaSpecificProperty(string propertyName)
        {
            switch (propertyName)
            {
                case "a": case "artist": return Artist;
                case "d": case "duration": return Duration;

                default: return null;
            }
        }

        #region Aestethic features

        /// <summary>
        /// Aestethic only, do not waste time here
        /// </summary>
        protected override string MediaSpecificInformation
        {
            get
            {
                return FormatStrings(false, Artist, Duration);
            }
        }

        #endregion Aestethic features
    }
}
