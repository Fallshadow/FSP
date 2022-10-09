using UnityEngine;

namespace fsp
{
    // 这边是对unity的方法扩展
    public static partial class UnityApiExtend
    {
        public static void SetActive(this Component comp, bool value)
        {
            if (comp == null)
            {
                return;
            }

            if (comp.gameObject.activeSelf != value)
            {
                comp.gameObject.SetActive(value);
            }
        }
    }
}