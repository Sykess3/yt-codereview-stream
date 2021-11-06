using UnityEngine;

namespace Source.Infrastructure.Services.AssetManagement
{
    public interface IAssetsProvider
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        T Instantiate<T>(string path) where T : Object;
    }
}