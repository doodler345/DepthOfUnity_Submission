using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace SimpleSaveSystem
{
    [InitializeOnLoad]
    class SaveReaderOnSelect
    {
        static GameObject currentGameObject = null;

        static SaveReaderOnSelect()
        {
            EditorApplication.update += Update;
        }

        static void Update()
        {
            GameObject obj = Selection.activeGameObject;

            if (obj == null || currentGameObject == obj)
            {
                return;
            }
            currentGameObject = obj;

            SaveObject[] saveObjects = obj.GetComponents<SaveObject>();
            if (saveObjects != null)
            {
                for (int i = 0; i < saveObjects.Length; i++)
                {
                    saveObjects[i].Read();
                }
            }
        }
    }
}

#endif
