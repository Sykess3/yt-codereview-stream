using UnityEngine;

namespace Source
{
    public static class Constants
    {
        public static void Init()
        {
            ClickableMask = 1 << LayerMask.NameToLayer("Clickable");
        }
        
        public static LayerMask ClickableMask { get; private set; }
    }
}