using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;


namespace ChaarrRescueMission.Output
{
    class FileManager
    {
        public static void SaveToFile(string json)
        {
            var dlg = new SaveFileDialog();
            dlg.FileName = "jsonOutput.json";
            dlg.DefaultExt = ".";
            dlg.Filter = "JSON File (.json)|*.json";

            bool? result = dlg.ShowDialog();

            if (result.HasValue && result.Value)
            {
                using (StreamWriter file = File.CreateText(dlg.FileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, json);
                }
            }
        }
    }
}
