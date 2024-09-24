﻿using System.Diagnostics;
using System.Text.Json;

namespace WatchIt.Database.Model.Seeding;

public class DataReader
{
    #region METHODS

    public static IEnumerable<T> Read<T>() => Read<T>(typeof(T).Name);
    public static IEnumerable<T> Read<T>(string filename)
    {
        string jsonFile = $@"{AppContext.BaseDirectory}/WatchIt.Database/WatchIt.Database.Model/WatchIt.Database.Model.Seeding/Data/{filename}.json";
        Console.WriteLine(string.Join(';', Directory.GetFiles($@"{AppContext.BaseDirectory}")));
        Debug.WriteLine(string.Join(';', Directory.GetFiles($@"{AppContext.BaseDirectory}")));
        string dataString = File.ReadAllText(jsonFile);
        IEnumerable<T>? data = JsonSerializer.Deserialize<IEnumerable<T>>(dataString);
        if (data is null)
        {
            throw new JsonException();
        }
        return data;
    }

    #endregion
}
