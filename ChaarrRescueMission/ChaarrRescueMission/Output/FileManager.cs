using ChaarrRescueMission.Model.Entity.Cargos;
using ChaarrRescueMission.Properties;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
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
                FileName = Resources.CaptionFileLogDefault,
                DefaultExt = Resources.CaptionFileLogDot,
                Filter = Resources.CaptionFileLogFilter,
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
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

        public static void ToFile(string pathFile, Cargo cargo)
        {
            var command = cargo.Command == null ? $"Command = {cargo.Command}" : string.Empty;
            var parameter = cargo.Parameter == null ? $"Parameter = {cargo.Parameter}" : string.Empty;
            var value = cargo.Value == null ? $"Value = {cargo.Value}" : string.Empty;
            File.AppendAllText(pathFile, command + parameter + value);
        }
    }
}
