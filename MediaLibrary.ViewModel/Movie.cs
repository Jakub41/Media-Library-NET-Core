using System;

namespace MediaLibrary.ViewModel
{
    class Movie : MultimediaItem
    {
        public string Director { get; private set; }
        public string Production { get; private set; }

        /// <summary>
        /// Movie constructor..
        /// </summary>
        public Movie(string[] data)
            : base(data) // ..will let base MultimediaItem constructor to initialize Title, Year and IsLent...
        {
            // ..and then will initialize its own fields: Director and Production
            Director = data[2];
            Production = data[3];
        }

        /// <summary>
        /// Using Polymorphism, this method is called when the base class
        /// cannot find the property the user is asking for
        /// among the default ones (Title, Year, IsLent)
        /// so we're looking into Movie-specific properties
        /// </summary>
        protected override string GetMediaSpecificProperty(string propertyName)
        {
            switch (propertyName)
            {
                case "d": case "director": return Director;
                case "p": case "production": return Production;

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
                return FormatStrings(false, Director, Production);
            }
        }

        #endregion Aestethic features
    }
}
