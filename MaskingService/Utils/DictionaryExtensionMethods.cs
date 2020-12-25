using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MaskingService.Utils
{
    public static class DictionaryExtensionMethods
    {
        public static TValue SetKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key) where TValue : new()
        {
            TValue val;

            if (!dic.TryGetValue(key, out val))
            {
                val = new TValue();
                dic.Add(key, val);
            }

            return val;
        }
    }
}
