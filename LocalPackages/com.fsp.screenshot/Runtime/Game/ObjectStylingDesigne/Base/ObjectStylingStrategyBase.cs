using System;
using System.Collections.Generic;

namespace fsp.modelshot.Game.ObjectStylingDesigne
{
    // 物体创造的策略基类，属于整体大策略，里面会有自己的物体摆放细节
    // 简单来说做这样几件事：
    // 创建策略信息
    // 供给外部获取策略信息
    // 创建骨架并在骨架上安放自己的物体！随后撒手不管
    // 创建骨架并在骨架上安放自己的物体！记录这次的骨架引用
    // 销毁某次骨架
    // 销毁策略信息
    public abstract class ObjectStylingStrategyBase
    {
        public List<String> ObjectNames = new List<string>();
        public List<String> SubStrategyNames = new List<string>();
        public ObjectStylingStrategyInfo curInfo = null;

        private ObjectStylingStrategySkeleton osSkeleton = new ObjectStylingStrategySkeleton();
        
        protected ObjectStylingStrategyBase(ObjectStylingStrategyInfo info)
        {
            curInfo = info;
            Init();
        }

        public virtual void Init()
        {
            osSkeleton.Init(curInfo.MaxLayer);
        }

        public virtual void Release()
        {
            osSkeleton.Release();
        }

        // 使用哪种物件骨骼信息
        public abstract void ApplySubStrategy(int subStategyIndex);
    }
}