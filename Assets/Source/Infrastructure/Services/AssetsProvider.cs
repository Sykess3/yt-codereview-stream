using Source.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace Source.Infrastructure.Services
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

        public T Instantiate<T>(string path) where T : Object
        {
            var gameObject = Resources.Load<T>(path);
            return Object.Instantiate(gameObject);
        }
    }
}