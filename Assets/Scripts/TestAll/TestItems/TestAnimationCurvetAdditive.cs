using UnityEngine;

namespace fsp.testall.TestItems
{
    public class TestAnimationCurvetAdditive : TestItemBase
    {
        public float additiveValue = 100;
        public AnimationCurve sourceAC = new AnimationCurve();
        public AnimationCurve targetAC = new AnimationCurve();
        public override void TestFunc0()
        {
            if (!TestBool0) return;
            TestBool0 = false;
            targetAC = sourceAC.AddtiveAnimationCurve(additiveValue);
        }
    }
}