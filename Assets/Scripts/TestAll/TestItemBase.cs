namespace fsp.testall
{
    public class TestItemBase
    {
        public TestItemType TestItemType;
        
        public bool TestBool0 = false;
        public bool TestBool1 = false;
        public bool TestBool2 = false;
        public bool TestBool3 = false;

        public virtual void TestFunc0() { if (!TestBool0) return; }
        public virtual void TestFunc1() { if (!TestBool1) return; }
        public virtual void TestFunc2() { if (!TestBool2) return; }
        public virtual void TestFunc3() { if (!TestBool3) return; }
    }
}