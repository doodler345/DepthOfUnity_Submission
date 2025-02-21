using System;
using UnityEngine;

namespace SimpleSaveSystem
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string Label { get; private set; }

        public ButtonAttribute()
        {
            Label = string.Empty;
        }
        public ButtonAttribute(string label)
        {
            Label = label;
        }
    }
}
