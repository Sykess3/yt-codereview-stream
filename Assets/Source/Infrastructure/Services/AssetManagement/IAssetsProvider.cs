using UnityEngine;

namespace Source.Infrastructure.Services.AssetManagement
{
    public interface IAssetsProvider
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        T Instantiate<T>(string path, Vector3 at) where T : Object;
        T Instantiate<T>(string path) where T : Object;
        T Load<T>(string path) where T : Object;
        GameObject Instantiate(string path, Transform parent);
    }
}