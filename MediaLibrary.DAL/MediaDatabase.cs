using MediaLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MediaLibrary.DAL
{
    public class MediaDatabase
    {
        /// <summary>
        /// The Array where all our multimedia items are stored
        /// </summary>
        private MultimediaItem[] _mediaDatabase;
        string []mediaarray;
       // private MultimediaItem _multimediaitem;

        /// <summary>
        /// This is the constructor, called when somebody writes "new MediaDatabase(path)"
        /// </summary>
        public MultimediaItem[] GetMediaDatabase()
        {
            string databasePath = "Media_Library_Database.csv";
            // Read file contents (as an entire string)
            _ = LoadDatabaseFromDisk(databasePath);
            int i = 0;

            using (StreamReader sr = new StreamReader(databasePath))
            {
                string currentLine;
                // Create empty array
                var lines = File.ReadLines(databasePath);
                _mediaDatabase = new MultimediaItem[lines.Count()];
                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = sr.ReadLine()) != null)
                {

                    // Search, case insensitive, if the currentLine contains the searched keyword
                    if (currentLine.IndexOf(";", StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        // Console.WriteLine(currentLine);


                        // Create each record, one per line that exist on the file
                        //for (int i = 0; i < lines.Count(); i++) 
                        //mediaarray.Append(MultimediaItem.ParseMultimediaItem(currentLine).Title);
                        //mediaarray.Append(MultimediaItem.ParseMultimediaItem(currentLine).Year.ToString());
                        //mediaarray.Append(MultimediaItem.ParseMultimediaItem(currentLine).IsLent.ToString());
                        //mediaarray.Append(MultimediaItem.ParseMultimediaItem(currentLine).DetailedInformation);
                        ////  _mediaDatabase.Append(MultimediaItem.ParseMultimediaItem();
                        ////  i++;
                        //_mediaDatabase.Append(mediaarray);
                        _mediaDatabase[i] = MultimediaItem.ParseMultimediaItem(currentLine);
                        _mediaDatabase.Append(_mediaDatabase[i]);
                        i++;

                    }
                }
            }


            // Split the string it into single lines
            //string[] records = fileContent.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            //// Create empty array
            //_mediaDatabase = new MultimediaItem[records.Length];

            //// Create each record, one per line that exist on the file
            //for (int i = 0; i < records.Length; i++)
            //    _mediaDatabase[i] = MultimediaItem.ParseMultimediaItem(records[i]);

            return _mediaDatabase;
        }

        public IEnumerable<MultimediaItem> GetElements(string type, string sortingProperty, MultimediaItem[] _mediaDatabase)
        {
            // Converts letter pressed by the user into the name of the class: "a" => "Album"
            type = type.ToLower();
            switch (type)
            {
                case "a": case "album": type = "Album"; break;
                case "m": case "movie": type = "Movie"; break;
                case "b": case "book": type = "Book"; break;
                case "v": case "videogame": type = "Videogame"; break;

                default: return null;
            }


            var result = from media in _mediaDatabase                   // Look at the elements in our _mediaDatabase array...
                         where media.GetType().Name == type             // ...take only the ones whose type is the same we want (IE: "a" => "Album" => take only Album instances)...
                         orderby media.GetProperty(sortingProperty)     // ...and sort them by the property the user has specified
                         select media;

            return result;
        }


        private string LoadDatabaseFromDisk(string databasePath)
        {
            // Utility method provided in System.IO namespace: it does all the work of opening a file stream, reading it and closing it.. in a single line
            return File.ReadAllText(databasePath);
        }
    }
}
