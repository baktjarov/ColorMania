using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace Helpers
{
    public class SaveHelper
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Project/Open Save Folder")]
#endif
        public static void OpenSaveFolder()
        {
#if UNITY_EDITOR
            try
            {
                string path = Path.Combine(Application.persistentDataPath, "Saves");

#if UNITY_EDITOR_WIN            
                System.Diagnostics.Process.Start(path);
#else
                System.Diagnostics.Process.Start("nautilus", path);
#endif
            }
            catch (Exception ex)
            {
                Debug.LogError("Error opening save folder: " + ex.Message);
                Debug.LogError(ex.StackTrace);
            }
#endif
        }

        public static void SaveToJson<T>(T objectToSave, string folder, string fileName)
        {
            try
            {
                string folderPath = Path.Combine(Application.persistentDataPath, folder);
                Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, fileName + ".json");
                string json = JsonConvert.SerializeObject(objectToSave);
                File.WriteAllText(filePath, json);

                Debug.Log("Saving: " + json);
            }
            catch (Exception e)
            {
                Debug.LogError("Error saving to file: " + e.Message);
                Debug.LogError(e.StackTrace);
            }
        }

        public static T GetStoredDataClass<T>(string folder, string fileName)
        {
            string path = Path.Combine(GetFullFolderName(folder), fileName + ".json");

            T result = default(T);

            if (File.Exists(path))
            {
                try
                {
                    string json = File.ReadAllText(path);
                    result = JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error loading from file: " + e.Message);
                    Debug.LogError(e.StackTrace);
                }
            }

            return result;
        }

        private static string GetFullFolderName(string folder)
        {
            return Path.Combine(Application.persistentDataPath, folder);
        }
    }
}
