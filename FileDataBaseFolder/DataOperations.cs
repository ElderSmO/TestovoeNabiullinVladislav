using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestovoeNabiullinVladislav.FileDataBaseFolder;

namespace TestovoeNabiullinVladislav
{
    /// <summary>
    /// Операции по сохранению информации
    /// </summary>
    public static class DataOperations
    {
        static string BaseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BD.json");
        public static FileDataBase ReadData()
        {
            try
            {
                string readJson = File.ReadAllText(BaseDirectory);
                if (readJson == "")
                {
                    return null;
                }
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
                FileDataBase clientsTableFile;
                clientsTableFile = JsonConvert.DeserializeObject<FileDataBase>(readJson, settings);
                return clientsTableFile;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Ошибка чтения" + ex);
                return null;
            }
            
        }
        public static void WriteData(FileDataBase clientsTableFile) 
        {
            try
            {
                if (clientsTableFile != null)
                {
                    var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };
                    JsonSerializer.Create(settings);
                    string json = JsonConvert.SerializeObject(clientsTableFile, settings);
                    File.WriteAllText(BaseDirectory, json);
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("ошибка сохранения " + ex);

            }
        }

    }
}
