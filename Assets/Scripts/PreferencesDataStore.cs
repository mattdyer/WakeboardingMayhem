using Google.Apis.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Google.Apis.Util.Store
{
    
    public class PreferencesDataStore : IDataStore
    {
        private static readonly Task CompletedTask = Task.FromResult(0);

        
        public PreferencesDataStore()
        {
            Debug.Log("datastore initialized");
        }

        
        public Task StoreAsync<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key MUST have a value");
            }

            Debug.Log("store: " + key);

            var serialized = NewtonsoftJsonSerializer.Instance.Serialize(value);
            
            PlayerPrefs.SetString(key,serialized);

            return CompletedTask;
        }

        
        public Task DeleteAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key MUST have a value");
            }

            PlayerPrefs.DeleteKey(key);

            return CompletedTask;
        }

        
        public Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key MUST have a value");
            }

            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            
            var obj = PlayerPrefs.GetString(key);

            tcs.SetResult(NewtonsoftJsonSerializer.Instance.Deserialize<T>(obj));
            
            return tcs.Task;
        }

        public Task ClearAsync()
        {
            

            return CompletedTask;
        }

    }
}