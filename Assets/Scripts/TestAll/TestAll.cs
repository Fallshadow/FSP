using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fsp.testall
{
    public class TestAll : MonoBehaviour
    {
        public List<TestItemBase> TestItems = new List<TestItemBase>();
        private void Start()
        {
            TestItems.Clear();
            Array testTypes = Enum.GetValues(typeof(TestItemType));
            foreach (var item in testTypes)
            {
                TestItems.Add(TestItemFactory.GetTestItem<TestItemBase>((TestItemType) item));
            }
        }

        void Update()
        {
            foreach (var item in TestItems)
            {
                item.TestFunc0();
                item.TestFunc1();
                item.TestFunc2();
                item.TestFunc3();
            }
        }
    }
}