﻿using System;
using System.Collections.Generic;

namespace Utility
{
    public static class ListUtility
    {
        public static bool TryGet<T>(this List<T> list, Predicate<T> predicate, out T result)
        {
            foreach (T element in list)
            {
                if (predicate.Invoke(element))
                {
                    result = element;
                    return true;
                }
            }

            result = default;
            return false;
        }
        
        public static void Shuffle<T>(this IList<T> ts) 
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) {
                var r = UnityEngine.Random.Range(i, count);
                (ts[i], ts[r]) = (ts[r], ts[i]);
            }
        }
    }
}