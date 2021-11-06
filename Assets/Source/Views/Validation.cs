using UnityEngine;

namespace Source.Views
{
    public static class Validation
    {
        public static void GameObjectImplements<TInterfaceType>(ref GameObject gameObject)
        {
            if (gameObject != null && gameObject.GetComponent<TInterfaceType>() == null)
            {
                gameObject = null;
                Debug.LogError($"Object must contain component that implements {typeof(TInterfaceType).FullName}");
            }
        }
    }
}