using UnityEngine;

namespace fsp
{
    public static partial class UnityApiExtend
    {
        public static Rect ChangeValue(this Rect curRect, float newRectX, float newRectY)
        {
            Rect newRect = curRect;
            newRect.x = newRectX;
            newRect.y = newRectY;
            return newRect;
        }
    }
}