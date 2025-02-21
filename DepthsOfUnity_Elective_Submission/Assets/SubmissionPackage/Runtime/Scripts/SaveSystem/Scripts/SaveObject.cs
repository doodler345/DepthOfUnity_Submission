using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace SimpleSaveSystem
{
    [System.Serializable]
    public class GenericObjectData
    {
        public enum ValueTypes
        {
            Bool,
            Int,
            Float,
            String
        }

        public string ValueName => valueName;
        [SerializeField] private string valueName;

        public ValueTypes ValueType => valueType;
        [SerializeField] private ValueTypes valueType;

        // Single value types
        [ShowIf("valueType", ValueTypes.Bool)]
        [Label("Value")]
        [AllowNesting]
        [SerializeField] private bool boolValue;
        [ShowIf("valueType", ValueTypes.Int)]
        [Label("Value")]
        [AllowNesting]
        [SerializeField] private int intValue;
        [ShowIf("valueType", ValueTypes.Float)]
        [Label("Value")]
        [AllowNesting]
        [SerializeField] private float floatValue;
        [ShowIf("valueType", ValueTypes.String)]
        [Label("Value")]
        [AllowNesting] [TextArea()]
        [SerializeField] private string stringValue;

        [ShowIf("valueType", ValueTypes.Bool)]
        [Label("Default Value")]
        [AllowNesting]
        [SerializeField] private bool boolValueDefault;
        [ShowIf("valueType", ValueTypes.Int)]
        [Label("Default Value")]
        [AllowNesting]
        [SerializeField] private int intValueDefault;
        [ShowIf("valueType", ValueTypes.Float)]
        [Label("Default Value")]
        [AllowNesting]
        [SerializeField] private float floatValueDefault;
        [ShowIf("valueType", ValueTypes.String)]
        [Label("Default Value")]
        [AllowNesting] [TextArea()]
        [SerializeField] private string stringValueDefault;

        /// <summary>
        /// Gets/Sets the Value depending on the set valueType. Make sure to use the correct type.
        /// </summary>
        public object Value
        {
            get
            {
                switch (valueType)
                {
                    case ValueTypes.Bool: return boolValue;
                    case ValueTypes.Int: return intValue;
                    case ValueTypes.Float: return floatValue;
                    case ValueTypes.String: return stringValue;
                    default: return null;
                }
            }
            set
            {
                switch (valueType)
                {
                    case ValueTypes.Bool: boolValue = (bool)value; break;
                    case ValueTypes.Int: intValue = (int)value; break;
                    case ValueTypes.Float: floatValue = (float)value; break;
                    case ValueTypes.String: stringValue = (string)value; break;
                }
            }
        }

        /// <summary>
        /// Resets the Value to the set default value.
        /// </summary>
        public void ResetValue()
        {
            switch (valueType)
            {
                case ValueTypes.Bool: boolValue = boolValueDefault; break;
                case ValueTypes.Int: intValue = intValueDefault; break;
                case ValueTypes.Float: floatValue = floatValueDefault; break;
                case ValueTypes.String: stringValue = stringValueDefault; break;
                default: return;
            }
        }
    }

    [System.Serializable]
    public  class GenericObjectDataList<T>
    {
        public string ValueName => valueName;
        [SerializeField] private string valueName;
        [SerializeField] private List<T> values;

        /// <summary>
        /// Gets/Sets the Value depending on the chosen list type. Make sure to use the correct list type.
        /// </summary>
        public object Value
        {
            get
            {
                return values;
            }
            set
            {
                values = (List<T>)value;
            }
        }

        /// <summary>
        /// Clears the List.
        /// </summary>
        public void ResetValue()
        {
            values.Clear();
        }
    }

    [System.Serializable]
    public class SaveObjectData
    {
        public GenericObjectData[] singleDatas;
        public GenericObjectDataList<bool>[] boolLists;
        public GenericObjectDataList<int>[] intLists;
        public GenericObjectDataList<float>[] floatLists;
        public GenericObjectDataList<string>[] stringLists;
    }

    public class SaveObject : MonoBehaviour
    {
        public Action saved;
        public Action reset;

        public string SaveFileName => saveFileName;
        [SerializeField] private string saveFileName;

        public SaveObjectData saveObjectData;

        private void Start()
        {
            SaveSystem.AddSaveableObject(this);
            Read();
        }

        /// <summary>
        /// Returns the current SaveObject value (Value is not garuanteed to be saved locally!).
        /// </summary>
        public object GetValue(string valueName)
        {
            object value = null;

            for (int i = 0; i < saveObjectData.singleDatas.Length; i++)
            {
                if (saveObjectData.singleDatas[i].ValueName == valueName)
                {
                    return saveObjectData.singleDatas[i].Value;
                }
            }

            value = GetValueFromGenericObjectDataListArray<bool>(saveObjectData.boolLists, valueName);
            if (value != null) return value;

            value = GetValueFromGenericObjectDataListArray<int>(saveObjectData.intLists, valueName);
            if (value != null) return value;

            value = GetValueFromGenericObjectDataListArray<float>(saveObjectData.floatLists, valueName);
            if (value != null) return value;

            value = GetValueFromGenericObjectDataListArray<string>(saveObjectData.stringLists, valueName);
            if (value != null) return value;

            Debug.LogWarning($"GenericObjectData with value name '{valueName}' does not exist.");
            return null;
        }

        private object GetValueFromGenericObjectDataListArray<T>(object[] objectDataListsArray, string valueName)
        {
            GenericObjectDataList<T>[] objectDataLists = (GenericObjectDataList<T>[])objectDataListsArray;
            for (int i = 0; i < objectDataLists.Length; i++)
            {
                if (objectDataLists[i].ValueName == valueName)
                {
                    return objectDataLists[i].Value;
                }
            }
            return null;
        }

        /// <summary>
        /// Sets a value for the given valueName. Value can be a single value or a list.
        /// Value can optionally directly be saved after setting it.
        /// If not saved directly after setting, it is recommended to call Save() afterwards.
        /// </summary>
        public void SetValue(string valueName, object value, bool saveAfterComplete = true)
        {
            for (int i = 0; i < saveObjectData.singleDatas.Length; i++)
            {
                if (saveObjectData.singleDatas[i].ValueName == valueName)
                {
                    saveObjectData.singleDatas[i].Value = value;
                    if (saveAfterComplete) Save();
                    return;
                }
            }
            for (int i = 0; i < saveObjectData.boolLists.Length; i++)
            {
                if (saveObjectData.boolLists[i].ValueName == valueName)
                {
                    saveObjectData.boolLists[i].Value = value;
                    if (saveAfterComplete) Save();
                    return;
                }
            }
            for (int i = 0; i < saveObjectData.intLists.Length; i++)
            {
                if (saveObjectData.intLists[i].ValueName == valueName)
                {
                    saveObjectData.intLists[i].Value = value;
                    if (saveAfterComplete) Save();
                    return;
                }
            }
            for (int i = 0; i < saveObjectData.floatLists.Length; i++)
            {
                if (saveObjectData.floatLists[i].ValueName == valueName)
                {
                    saveObjectData.floatLists[i].Value = value;
                    if (saveAfterComplete) Save();
                    return;
                }
            }
            for (int i = 0; i < saveObjectData.stringLists.Length; i++)
            {
                if (saveObjectData.stringLists[i].ValueName == valueName)
                {
                    saveObjectData.stringLists[i].Value = value;
                    if (saveAfterComplete) Save();
                    return;
                }
            }

            Debug.LogWarning($"GenericObjectData with value name '{valueName}' does not exist.");
        }

        /// <summary>
        /// Reads the locally saved information into the SaveObject.
        /// </summary>
        public void Read()
        {
    #if UNITY_EDITOR
            SaveSystem.AddSaveableObject(this);
    #endif
            SaveSystem.ReadSaveObject(saveFileName);
        }

        /// <summary>
        /// Saves the current information locally.
        /// </summary>
        public void Save()
        {
            SaveSystem.SaveSaveObject(saveFileName);
            saved?.Invoke();
        }

        /// <summary>
        /// Resets all values of the SaveObject to their default value.
        /// </summary>
        public void ResetAllValues()
        {
            for (int i = 0; i < saveObjectData.singleDatas.Length; i++)
            {
                saveObjectData.singleDatas[i].ResetValue();
            }
            ResetCompleteGenericObjectDataListArray<bool>(saveObjectData.boolLists);
            ResetCompleteGenericObjectDataListArray<int>(saveObjectData.intLists);
            ResetCompleteGenericObjectDataListArray<float>(saveObjectData.floatLists);
            ResetCompleteGenericObjectDataListArray<string>(saveObjectData.stringLists);

            reset?.Invoke();

            Save();
        }

        private void ResetCompleteGenericObjectDataListArray<T>(object[] objectDataListsArray)
        {
            GenericObjectDataList<T>[] objectDataLists = (GenericObjectDataList<T>[])objectDataListsArray;
            for (int i = 0; i < objectDataLists.Length; i++)
            {
                objectDataLists[i].ResetValue();
            }
        }

        /// <summary>
        /// Checks if every value in the SaveObject is named.
        /// </summary>
        public bool IsEveryDataNamed()
        {
            for (int i = 0; i < saveObjectData.singleDatas.Length; i++)
            {
                if (saveObjectData.singleDatas[i].ValueName == "")
                {
                    return false;
                }
            }

            if (!IsEveryDataNamedInGenericObjectDataListArray<bool>(saveObjectData.boolLists)) return false;
            if (!IsEveryDataNamedInGenericObjectDataListArray<int>(saveObjectData.intLists)) return false;
            if (!IsEveryDataNamedInGenericObjectDataListArray<float>(saveObjectData.floatLists)) return false;
            if (!IsEveryDataNamedInGenericObjectDataListArray<string>(saveObjectData.stringLists)) return false;

            return true;
        }

        private bool IsEveryDataNamedInGenericObjectDataListArray<T>(object[] dataListArrays)
        {
            for (int i = 0; i < dataListArrays.Length; i++)
            {
                GenericObjectDataList<T> genericObjectDataList = (GenericObjectDataList<T>)dataListArrays[i];

                if (genericObjectDataList.ValueName == "")
                {
                    return false;
                }
            }
            return true;
        }

    #if UNITY_EDITOR
        [SimpleSaveSystem.Button("Read")]
        private void ReadByButton()
        {
            Read();
        }

        [SimpleSaveSystem.Button("Save")]
        private void SaveByButton()
        {
            SaveSystem.AddSaveableObject(this);
            Save();
        }

        [SimpleSaveSystem.Button("Reset All")]
        private void ResetAllByButton()
        {
            SaveSystem.AddSaveableObject(this);
            ResetAllValues();
        }
    #endif
    }
}
