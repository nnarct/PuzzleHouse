using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public static class FileHandler
{

    public static void SaveToJSON<T>(List<T> toSave, string filename)
    {
        Debug.Log(_getPath(filename));
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
        _writeFile(_getPath(filename), content);
    }

    public static void SaveToJSON<T>(T toSave, string filename)
    {
        string content = JsonUtility.ToJson(toSave);
        _writeFile(_getPath(filename), content);
    }

    public static List<T> ReadListFromJSON<T>(string filename)
    {
        string content = _readFile(_getPath(filename));

        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return new List<T>();
        }

        List<T> res = JsonHelper.FromJson<T>(content).ToList();

        return res;

    }

    public static T ReadFromJSON<T>(string filename)
    {
        string content = _readFile(_getPath(filename));

        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return default(T);
        }

        T res = JsonUtility.FromJson<T>(content);

        return res;

    }

    private static string _getPath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }

    private static void _writeFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    private static string _readFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Players;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Players = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Players = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Players;
    }
}

