using UnityEngine;

namespace Source.PropertyAttributes
{
    public class InterfaceTooltipAttribute : PropertyAttribute
    {
        public string Name { get; }

        public InterfaceTooltipAttribute(string name) => Name = name;
    }
}