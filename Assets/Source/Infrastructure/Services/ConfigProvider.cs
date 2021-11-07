using System;
using System.Collections.Generic;
using System.Linq;
using Source.Configs;
using UnityEngine;

namespace Source.Infrastructure.Services
{
    public class ConfigProvider : IConfigProvider
    {
                private static readonly ConfigLoader _configLoader = new ConfigLoader();
        public TType Get<TType, TIdentifier>(TIdentifier identifier, string path) where TType : class, IConfigWithIdentifier<TIdentifier>
        {
            TType config = MultipleConfig<TType, TIdentifier>.Get(identifier);
            if (config != null)
                return config;

            MultipleConfig<TType, TIdentifier>.Load(path);
            return MultipleConfig<TType, TIdentifier>.Get(identifier);
        }

        public TType Get<TType>(string path) where TType : class
        {
            TType config = SingleConfig<TType>.Get();
            if (config != null)
                return config;
            
            SingleConfig<TType>.Load(path);
            return SingleConfig<TType>.Get();
        }

        private static class MultipleConfig<TType, TIdentifier> where TType : class, IConfigWithIdentifier<TIdentifier>
        {
            private static Dictionary<TIdentifier, TType> _configsMap = new Dictionary<TIdentifier, TType>();
            public static TType Get(TIdentifier identifier) =>
                _configsMap.TryGetValue(identifier, out var config)
                    ? config
                    : null;

            public static void Load(string path)
            {
                _configsMap = _configLoader.LoadAll<TType>(path)
                    .ToDictionary(x => x.Identifier, y => y);
            }
        }

        private static class SingleConfig<TType> where TType : class
        {
            private static TType _config;

            public static TType Get() => _config;

            public static void Load(string path) => _config = _configLoader.Load<TType>(path);
        }
    }

    class ConfigLoader
    {
        public TType Load<TType>(string path) where TType : class
        {
            var config = Resources.Load<ScriptableObject>(path);
            return config as TType;
        }

        public TType[] LoadAll<TType>(string path) where TType : class
        {
            var configs = Resources.LoadAll<ScriptableObject>(path);
            return configs.Cast<TType>().ToArray();
        }
    }
}