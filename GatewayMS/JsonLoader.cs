// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;
using System.IO;
using Newtonsoft.Json;

namespace GatewayMS
{
    /// <summary>
    /// This class contains the implementation of the json loader for deserializing json objects
    /// </summary>
    public class JsonLoader
    {
        /// <summary>
        /// This method deserializes the provided json file into the specified generic object type
        /// </summary>
        /// <param name="filePath">The file path</param>
        /// <typeparam name="T">The generic object type</typeparam>
        /// <returns>The generic object filled with the data extracted from the json file</returns>
        public static T LoadFromFile<T>(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }

        /// <summary>
        /// This method deserializes the provided json object into the specified generic object type
        /// </summary>
        /// <param name="jsonObject">The json object to be deserialized</param>
        /// <typeparam name="T">The generic object type</typeparam>
        /// <returns>The generic object filled with the data extracted from the json object</returns>
        public static T Deserialize<T>(object jsonObject)
        {
            return JsonConvert.DeserializeObject<T>(Convert.ToString(jsonObject));
        }
    }
}