using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Comm
{
    public class MyCache
    {
        private static Dictionary<string, object> _KeyValuePairs;
        static MyCache()
        {
            _KeyValuePairs = new Dictionary<string, object>();
        }

        public static object GetCache(string key)
        {
            if (_KeyValuePairs.ContainsKey(key))
                return _KeyValuePairs[key];
            return null;
        }
        public static void SetCache(string key, object oValue)
        {
            _KeyValuePairs[key] = oValue;
        }
    }
}
