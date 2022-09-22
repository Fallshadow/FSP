using System.Collections.Generic;
using UnityEngine;

namespace fsp
{
    public static partial class UnityApiExtend
    {
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static void AdjustToConformCountNewT<T>(this List<T> list, int fullCount) where T : new()
        {
            if (list.Count == fullCount) return;

            while (list.Count < fullCount)
            {
                list.Add(new T());
            }

            while (list.Count > fullCount)
            {
                list.RemoveAt(list.Count - 1);
            }
        }

        public static void AdjustToConformCountDefaultT<T>(this List<T> list, int fullCount)
        {
            if (list.Count == fullCount) return;

            while (list.Count < fullCount)
            {
                list.Add(default);
            }

            while (list.Count > fullCount)
            {
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}