using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.DataSeeding
{
    public static class DataReader
    {
        #region METHODS

        public static IEnumerable<T> Read<T>() => Read<T>(typeof(T).Name);
        public static IEnumerable<T> Read<T>(string filename)
        {
            string jsonFile = $"..\\WatchIt.Database\\WatchIt.Database.DataSeeding\\Data\\{filename}.json";
            string dataString = File.ReadAllText(jsonFile);
            IEnumerable<T> data = JsonConvert.DeserializeObject<IEnumerable<T>>(dataString);
            return data;
        }

        #endregion
    }
}
