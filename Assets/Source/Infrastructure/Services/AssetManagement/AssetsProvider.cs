using UnityEngine;

namespace Source.Infrastructure.Services.AssetManagement
{
    public class AssetsProvider : IAssetsProvider
    {
        public GameObject Instantiate(string path)
        {
            var gameObject = Resources.Load<GameObject>(path);
            return Object.Instantiate(gameObject);
        }
        public GameObject Instantiate(string path, Vector3 at)
        {
            var gameObject = Resources.Load<GameObject>(path);
            return Object.Instantiate(gameObject, at, Quaternion.identity);
        }

        public T Instantiate<T>(string path, Vector3 at) where T : Object
        {
            var gameObject = Resources.Load<T>(path);
            return Object.Instantiate(gameObject, at, Quaternion.identity);
        }

        public T Instantiate<T>(string path) where T : Object
        {
            var gameObject = Resources.Load<T>(path);
            return Object.Instantiate(gameObject);
        }

        public GameObject Instantiate(string path, Transform parent) 
        {
            var gameObject = Resources.Load<GameObject>(path);
            return Object.Instantiate(gameObject, parent);
        }

        public T Load<T>(string path) where T : Object => 
            Resources.Load<T>(path);
    }
}