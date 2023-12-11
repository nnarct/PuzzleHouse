using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public static class FileHandler
{
    // Save a list of objects to a JSON file
    public static void SaveToJSON<T>(List<T> toSave, string filename)
    {
        // Convert the list of objects to a JSON string
        string content = JsonHelper.ToJson<T>(toSave.ToArray());

        // Write the JSON string to the file
        WriteFile(GetPath(filename), content);
    }

    // Save a single object to a JSON file
    public static void SaveToJSON<T>(T toSave, string filename)
    {
        // Convert the object to a JSON string
        string content = JsonUtility.ToJson(toSave);

        // Write the JSON string to the file
        WriteFile(GetPath(filename), content);
    }

    // Read a list of objects from a JSON file
    public static List<T> ReadListFromJSON<T>(string filename)
    {
        // Read the JSON content from the file
        string content = ReadFile(GetPath(filename));

        // Check if the content is empty or "{}"
        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            // Return an empty list
            return new List<T>();
        }
        
        // Convert the JSON content to a list of objects
        List<T> res = JsonHelper.FromJson<T>(content).ToList();

        // Return the list of objects
        return res;
    }

    // Read a single object from a JSON file
    public static T ReadFromJSON<T>(string filename)
    {
        // Read the JSON content from the file
        string content = ReadFile(GetPath(filename));

        // Check if the content is empty or "{}"
        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            // Return the default value for the object
            return default(T);
        }

        // Convert the JSON content to a single object
        T res = JsonUtility.FromJson<T>(content);

        // Return the single object
        return res;
    }

    // Get the full path for the file using the persistent data path
    private static string GetPath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }

    // Write content to a file at the specified path
    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    // Read content from a file at the specified path
    private static string ReadFile(string path)
    {
        // Check if the file exists
        if (File.Exists(path))
        {
            // Read the content from the file
            using (StreamReader reader = new StreamReader(path))
            {
                // Read the content from the file
                string content = reader.ReadToEnd();

                // Return the content
                return content;
            }
        }

        // Return an empty string
        return "";
    }
}

// Helper class for working with JSON serialization
public static class JsonHelper
{
    // Deserialize JSON array to an array of objects
    public static T[] FromJson<T>(string json)
    {
        // Create a wrapper to deserialize the JSON array
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);

        // Return the array of objects
        return wrapper.Players;
    }

    // Serialize an array of objects to JSON format
    public static string ToJson<T>(T[] array)
    {
        // Create a wrapper to serialize the array of objects
        Wrapper<T> wrapper = new Wrapper<T>();

        // Set the array of objects to the wrapper
        wrapper.Players = array;

        // Return the serialized JSON string
        return JsonUtility.ToJson(wrapper);
    }

    // Serialize an array of objects to JSON format with optional pretty printing
    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        // Create a wrapper to serialize the array of objects
        Wrapper<T> wrapper = new Wrapper<T>();

        // Set the array of objects to the wrapper
        wrapper.Players = array;
        
        // Return the serialized JSON string
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    // Wrapper class to assist in JSON serialization
    [Serializable]
    private class Wrapper<T>
    {
        // Array of objects to serialize
        public T[] Players;
    }
}

