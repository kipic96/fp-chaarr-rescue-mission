using ChaarrRescueMission.Properties;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;


namespace ChaarrRescueMission.Output
{
    class FileManager
    {
        /// <summary>
        /// Calls Open File Dialog, user selects path to save file 
        /// and file is saved.
        /// </summary>
        /// <param name="json"></param>
        public static void SaveToFile(string json)
        {
            var dlg = new SaveFileDialog
            {
                FileName = Resources.CaptionFileJsonDefault,
                DefaultExt = Resources.CaptionFileJsonDot,
                Filter = Resources.CaptionFileJsonFilter
            };

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
