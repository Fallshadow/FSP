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
        protected ObjectStylingStrategyBase(ObjectStylingStrategyInfo info)
        {
            
        }
        
        public abstract void Init();
    }
}