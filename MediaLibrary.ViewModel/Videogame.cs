using System;

namespace MediaLibrary.ViewModel
{
    class Videogame : MultimediaItem
    {
        public string SoftwareHouse { get; private set; }
        public bool HasMultiplayer { get; private set; }

        /// <summary>
        /// Videogame constructor..
        /// </summary>
        public Videogame(string[] data)
            : base(data) // ..will let base MultimediaItem constructor to initialize Title, Year and IsLent...
        {
            // ..and then will initialize its own fields: SoftwareHouse and HasMultiplayer
            SoftwareHouse = data[2];
            HasMultiplayer = Convert.ToBoolean(data[3]);
        }

        /// <summary>
        /// Using Polymorphism, this method is called when the base class
        /// cannot find the property the user is asking for
        /// among the default ones (Title, Year, IsLent)
        /// so we're looking into Videogame-specific properties
        /// </summary>
        protected override string GetMediaSpecificProperty(string propertyName)
        {
            switch (propertyName)
            {
                case "s": case "softwarehouse": return SoftwareHouse;
                case "h": case "hasmultiplayer": return HasMultiplayer.ToString();

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
                return FormatStrings(false, SoftwareHouse, HasMultiplayer ? "    YES" : "    NO");
            }
        }

        #endregion Aestethic features
    }
}
