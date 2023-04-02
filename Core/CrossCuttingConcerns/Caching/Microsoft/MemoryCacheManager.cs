using Core.Utilities.IOC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memoryCache;
        private List<string> _keys;

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
            _keys= new List<string>();
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key,value,TimeSpan.FromMinutes(duration)); 
            _keys.Add(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
            _keys.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = _keys.Where(d => regex.IsMatch(d.ToString())).Select(d => d).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
                _keys.Remove(key);
            }
        }
    }
}