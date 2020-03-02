using System;

namespace MediaLibrary.ViewModel
{
    class Book : MultimediaItem
    {
        public string Author { get; private set; }
        public int Pages { get; private set; }

        /// <summary>
        /// Book constructor..
        /// </summary>
        public Book(string[] data)
            : base(data) // ..will let base MultimediaItem constructor to initialize Title, Year and IsLent...
        {
            // ..and then will initialize its own fields: Author and Pages
            Author = data[2];
            Pages = Convert.ToInt32(data[3]);
        }

        /// <summary>
        /// Using Polymorphism, this method is called when the base class
        /// cannot find the property the user is asking for
        /// among the default ones (Title, Year, IsLent)
        /// so we're looking into Book-specific properties
        /// </summary>
        protected override string GetMediaSpecificProperty(string propertyName)
        {
            switch (propertyName)
            {
                case "a": case "author": return Author;
                case "p": case "pages": return Pages.ToString();

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
                return FormatStrings(false, Author, Pages.ToString());
            }
        }

        #endregion Aestethic features
    }
}
