using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

//  ID3tags - This class is used to store ID3 v1 Tags 
//      from any Mp3 file still supporting v1. The tags
//      are referenced through a public dictionary and looked
//      up by name.
//
//  Authored by: Nicholas B. Strydom
//  Date revised: 04/08/2017 2:14 pm CST

namespace ID3tags
{
    class TagReader
    {
        // Dictionary declaration, used to store all tag data
        // and easily referenced with our "String" key.
        public Dictionary<String, String> tagData = null;

        public TagReader(String songFileLocation)
        {
            // Initialize our dictionary
            this.tagData = new Dictionary<String, String>();

            // Pass file location and store Mp3 ID3 v1 tags
            this.storeTags(songFileLocation);
        }

        /// <summary>
        /// Stores ID3v1 tags in tagData Dictionary object
        /// </summary>
        /// <param name="songFileLocation"></param>
        private void storeTags(String songFileLocation)
        {
            // Declare our Filestream and Binary reader objects
            // so we can read bytes from file at the proper location.
            FileStream file = new FileStream(songFileLocation, FileMode.Open, FileAccess.Read);
            BinaryReader binary = new BinaryReader(file);
            String rawData = String.Empty;

            try
            {
                // Set the binary reader to start reading from 
                // 128 bytes from the EOF
                binary.BaseStream.Position = file.Length - 128;

                // Read bytes from file and convert them to string
                rawData = Encoding.UTF8.GetString(binary.ReadBytes((int)file.Length));

                // Make sure properly read all 128 bytes
                if (rawData.Length >= 128)
                {
                    // Store the tags with proper keys by each tags length
                    // as referenced here https://en.wikipedia.org/wiki/ID3
                    this.tagData.Add("header", rawData.Substring(0, 3));
                    this.tagData.Add("title", rawData.Substring(3, 30));
                    this.tagData.Add("artist", rawData.Substring(33, 30));
                    this.tagData.Add("album", rawData.Substring(63, 30));
                    this.tagData.Add("year", rawData.Substring(93, 4));
                    this.tagData.Add("comment", rawData.Substring(97, 30));
                    this.tagData.Add("zero-byte", rawData.Substring(125, 1));
                    this.tagData.Add("track", rawData.Substring(126, 1));
                    this.tagData.Add("genre", rawData.Substring(127, 1));
                }

                else
                {
                    // Post error if unable to read from file.
                    Console.WriteLine("Error while reading ID3v1 Tag.");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            finally
            {
                // If all went well close objects
                if (binary != null)
                {
                    binary.Close();
                }
                if (file != null)
                {
                    file.Close();
                    file.Dispose();
                }
            }
        }

        public String getHeader()
        {
            return tagData["header"];
        }

        public String getTitle()
        {
            return tagData["title"];
        }

        public String getArtist()
        {
            return tagData["artist"];
        }

        public String getAlbum()
        {
            return tagData["album"];
        }

        public String getYear()
        {
            return tagData["year"];
        }

        public String getComment()
        {
            return tagData["comment"];
        }

        public String getTrack()
        {
            return tagData["track"];
        }

        public String getGenre()
        {
            return tagData["genre"];
        }

    }
}
