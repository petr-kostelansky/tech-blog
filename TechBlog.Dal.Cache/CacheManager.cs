using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Caching;

namespace TechBlog.Dal.Cache
{
    public class CacheManager : ICacheManager
    {
        private object lockObj = new Object();
        private bool disposed = false;
        private MemoryCache cache;
        private IDictionary<string, HashSet<string>> tagCollection;

        public IDictionary<string, HashSet<string>> TagCollection
        {
            get
            {
                return tagCollection;
            }
        }

        public CacheManager()
        {
            Init();
        }

        public CacheManager(string name, NameValueCollection config)
        {
            Init(name, config);
        }

        private void Init(string name = "CacheManager", NameValueCollection config = null)
        {
            cache = new MemoryCache(name, config);
            tagCollection = new Dictionary<string, HashSet<string>>();
        }

        public string GetTagName<TEntityEnum>(TEntityEnum cacheEntity, int entityId)
        {
            if (cacheEntity == null)
                throw new ArgumentNullException(nameof(cacheEntity));

            return $"{cacheEntity.ToString()}_{entityId}";
        }

        public T GetValue<T>(string key)
        {
            bool cached;
            return GetValue<T>(key, out cached);
        }

        public T GetValue<T>(string key, out bool cached)
        {
            cached = false;
            CacheItemWrapper cacheItemWrapper = cache.Get(key) as CacheItemWrapper;
            T outputObject = default(T);

            if (cacheItemWrapper != null)
            {
                cached = true;

                if (cacheItemWrapper.Value == null)
                {
                    return outputObject;
                }
            }
            else
            {
                return outputObject;
            }

            outputObject = (T)cacheItemWrapper.Value;

            return outputObject;
        }

        public CacheItemWrapper GetValue(string key)
        {
            return cache.Get(key) as CacheItemWrapper;
        }

        public void SetValue(string key, object obj)
        {
            SetValue<DefaultEntities>(key, obj, 1, null, Enumerable.Empty<int>());
        }

        public void SetValue(string key, object obj, double duration)
        {
            SetValue<DefaultEntities>(key, obj, duration, null, Enumerable.Empty<int>());
        }

        public void SetValue<TEntityEnum>(string key, object obj, double duration, TEntityEnum? cacheEntity, IEnumerable<int> entityIds)
            where TEntityEnum : struct
        {
            CacheItemWrapper cacheItemWrapper = new CacheItemWrapper
            {
                Value = obj,
                Key = key,
                Tags = entityIds.Select(id => GetTagName(cacheEntity, id))
            };

            lock (lockObj)
            {
                Add(key, cacheItemWrapper, duration);
                AddTags(key, cacheEntity, entityIds);
            }
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public void RemoveByTags<TEntityEnum>(TEntityEnum cacheEntity, int entityId)
        {
            var tag = GetTagName(cacheEntity, entityId);

            lock (lockObj)
            {
                if (tagCollection.ContainsKey(tag))
                {
                    var relatedKeys = tagCollection[tag].ToList();

                    foreach (var relatedKey in relatedKeys)
                    {
                        Remove(relatedKey);
                    }

                    tagCollection.Remove(tag);
                }
            }
        }

        public void Clear()
        {
            lock (lockObj)
            {
                cache.Dispose();
                Init();
            }
        }

        private void Add(string key, CacheItemWrapper value, double duration)
        {
            var option = new CacheItemPolicy();
            option.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(duration);
            option.Priority = CacheItemPriority.Default;
            option.RemovedCallback = new CacheEntryRemovedCallback(CacheItemRemovedEvent);

            cache.Set(key, value, option);
        }

        private void AddTags<TEntityEnum>(string key, TEntityEnum cacheEntity, IEnumerable<int> entityIds)
        {
            if (cacheEntity == null || !entityIds.Any())
                return;

            string newTag = null;
            foreach (var entityId in entityIds)
            {
                newTag = GetTagName(cacheEntity, entityId);

                if (!tagCollection.ContainsKey(newTag))
                {
                    var relatedKeys = new HashSet<string>();
                    relatedKeys.Add(key);

                    tagCollection.Add(new KeyValuePair<string, HashSet<string>>(newTag, relatedKeys));
                }
                else
                {
                    var relatedKeys = tagCollection[newTag];
                    relatedKeys.Add(key);
                }
            }
        }

        private void RemoveTagItems(CacheItemWrapper itemWrapper)
        {
            if (itemWrapper == null)
                return;

            foreach (var tag in itemWrapper.Tags)
            {
                RemoveTagItem(tag, itemWrapper.Key);
            }
        }

        private void RemoveTagItem(string tag, string key)
        {
            if (tagCollection.TryGetValue(tag, out HashSet<string> relatedKeys))
            {
                relatedKeys.Remove(key);
            }
        }

        private void CacheItemRemovedEvent(CacheEntryRemovedArguments args)
        {
            if (args.RemovedReason == CacheEntryRemovedReason.Expired
                || args.RemovedReason == CacheEntryRemovedReason.Removed
                || args.RemovedReason == CacheEntryRemovedReason.Evicted)
            {
                var itemWrapper = args.CacheItem?.Value as CacheItemWrapper;

                lock (lockObj)
                {
                    RemoveTagItems(itemWrapper);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free managed objects.
                cache.Dispose();
            }

            disposed = true;
        }
    }

    enum DefaultEntities
    {

    }
}
