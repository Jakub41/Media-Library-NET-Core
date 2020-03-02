using System;

namespace MediaLibrary.ViewModel
{
    public abstract class MultimediaItem
    {
        public string Title { get; private set; }
        public int Year { get; private set; }
        public bool IsLent { get; private set; }

        /// <summary>
        /// Base constructor: it initializes common item properties (Title, Year, IsLent)
        /// based on the data provided
        /// </summary>
        protected MultimediaItem(string[] data)
        {
            Title = data[1];
            Year = Convert.ToInt32(data[4]);
            IsLent = Convert.ToBoolean(data[5].EndsWith("e"));
        }

        /// <summary>
        /// This static method creates a proper instance (Album, Movie, Book or Videogame)
        /// based on the first letter of the provided data.
        /// IE: "A;Evil Empire;RATM;46' 37";1999;FALSE" will create a new Album
        /// </summary>
        public static MultimediaItem ParseMultimediaItem(string record)
        {
            string[] data = record.Split(';');
            switch (data[0])
            {
                case "A": return new Album(data);
                case "M": return new Movie(data);
                case "B": return new Book(data);
                case "V": return new Videogame(data);

                default: return null;
            }
        }

        /// <summary>
        /// Get the value of a property (Title, Year, Pages)
        /// based on the user input
        /// </summary>
        public string GetProperty(string propertyName)
        {
            // Try looking in base properties...
            propertyName = propertyName.ToLower();
            switch (propertyName)
            {
                case "t": case "title": return Title;
                case "y": case "year": return Year.ToString();
                case "i": case "islent": return IsLent.ToString();

                // ..if none of them match, check in our specific implementation 
                // (IE: Book will check for p -> Pages or a -> Author
                default: return GetMediaSpecificProperty(propertyName);
            }
        }

        /// <summary>
        /// This uses Polymorphism to ask to our specific instance (IE: Book)
        /// to return a the value of specific property
        /// the user is wanting to sort records by
        /// 
        /// (same thing that happened with special actions in Conan the CSharpian)
        /// </summary>
        protected abstract string GetMediaSpecificProperty(string propertyName);

        #region Aestethic features

        /// <summary>
        /// Aestethic only, do not waste time here
        /// </summary>
        public static string GetHeaders(string mediaType)
        {
            mediaType = mediaType.ToLower();
            string specificHeaders;
            switch (mediaType)
            {
                case "a": case "album": specificHeaders = FormatStrings(true, "Artist", "Duration"); break;
                case "m": case "movie": specificHeaders = FormatStrings(true, "Director", "Production"); break;
                case "b": case "book": specificHeaders = FormatStrings(true, "Author", "Pages"); break;
                case "v": case "videogame": specificHeaders = FormatStrings(true, "SoftwareHouse", "HasMultiplayer"); break;

                default: return null;
            }

            return FormatStrings(true, "Is Lent", "Title", "Year") + "|" + specificHeaders;
        }

        /// <summary>
        /// Aestethic only, do not waste time here
        /// </summary>
        public string DetailedInformation
        { 
            get
            {
                string info = FormatStrings(false, IsLent ? "  LENT" : "Available", Title, Year.ToString());
                info += "|" + MediaSpecificInformation;
                return info;
            }
        }

        /// <summary>
        /// Aestethic only, do not waste time here
        /// </summary>
        protected static string FormatStrings(bool showKeyHint, params string[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
                inputs[i] = FormatString(showKeyHint, inputs[i]);

            return string.Join("|", inputs);
        }

        /// <summary>
        /// Aestethic only, do not waste time here
        /// </summary>
        protected static string FormatString(bool showKeyHint, string input)
        {
            if (showKeyHint)
                input = $"[{input[0].ToString().ToUpper()}]{input.Substring(1, input.Length - 1)}";

            return $" {input}".PadRight(21);
        }

        /// <summary>
        /// Aestethic only, do not waste time here
        /// </summary>
        protected abstract string MediaSpecificInformation { get; }

        #endregion Aestethic features
    }
}
