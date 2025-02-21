using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace SimpleSaveSystem
{
    public static class SaveSystem
    {
        private static Dictionary<string, SaveObject> nameToSaveObject = new Dictionary<string, SaveObject>();

        /// <summary>
        /// Adds a SaveObject to the SaveSystem.
        /// This is done automatically by the SaveObject itself.
        /// </summary>
        public static void AddSaveableObject(SaveObject saveObject)
        {
            if (saveObject.SaveFileName == null || saveObject.SaveFileName == "")
            {
                Debug.LogWarning("SaveObject has no name. Please give it a name before adding it to the SaveSystem.");
                return;
            }

            if (nameToSaveObject.ContainsKey(saveObject.SaveFileName))
            {
                return;
            }
            nameToSaveObject.Add(saveObject.SaveFileName, saveObject);
        }

        /// <summary>
        /// Returns the SaveObject with the given name.
        /// </summary>
        public static SaveObject GetSaveObject(string name)
        {
            if (!nameToSaveObject.ContainsKey(name))
            {
                Debug.LogWarning($"SaveableObject with name '{name}' does not exist.");
                return null;
            }
            return nameToSaveObject[name];
        }

        /// <summary>
        /// Saves the current information of a SaveObject locally.
        /// </summary>
        public static void SaveSaveObject(string name)
        {
            if (!nameToSaveObject.ContainsKey(name))
            {
                Debug.LogWarning($"SaveableObject with name '{name}' not recognized by SaveSystem. Check if the name was written right. Saving canceled.");
                return;
            }

            if (!nameToSaveObject[name].IsEveryDataNamed())
            {
                Debug.LogWarning($"SaveableObject with name '{name}' has missing Value Names. Saving canceled.");
                return;
            }

            SaveObjectData saveableData = nameToSaveObject[name].saveObjectData;


            string path = Application.persistentDataPath;
            string fileName = name + ".save";

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);

            formatter.Serialize(stream, saveableData);
            stream.Close();
        }

        /// <summary>
        /// Reads in the locally saved informaton into a SaveObject.
        /// </summary>
        public static void ReadSaveObject(string name)
        {
            if (!nameToSaveObject.ContainsKey(name))
            {
                Debug.LogWarning($"SaveableObject with name '{name}' not recognized by SaveSystem. Check if the name was written right.");
                return;
            }

            string path = Application.persistentDataPath;
            string fileName = name + ".save";

            if (!File.Exists(Path.Combine(path, fileName)))
            {
                SaveSaveObject(name);
                Debug.LogWarning($"Savefile '{name}' didn't exist and is now generated.");
                return;
            }

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Open);

            nameToSaveObject[name].saveObjectData = formatter.Deserialize(stream) as SaveObjectData;
            stream.Close();
        }
    }
}