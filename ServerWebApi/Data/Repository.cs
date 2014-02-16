using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ServerWebApi.Data
{
    public class Repository<T> where T : ICachedObj
    {
        CacheItemPolicy cp = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddYears(10) };
        MemoryCache mc = MemoryCache.Default;
        public void Add(T entity)
        {
            string key = entity.Id.ToString();
            if (mc.Contains(key))
            {
                mc.Remove(key);
            }
            mc.Add(entity.Id.ToString(), entity, cp);
        }
        public T Get(Guid Id)
        {
            return (T)mc.Get(Id.ToString());
        }

        public IEnumerable<T> GetAll()
        {
            var res = (from item in mc
                       where item.Value.GetType() == typeof(T)
                       select item.Value).ToArray<object>();
            List<T> list = new List<T>();
            foreach (var item in res)
            {
                list.Add((T)item);
            }

            return list;
        }
    }
}
