using fsp.ObjectStylingDesigne;
using fsp.ui.utility;
using UnityEngine;

namespace fsp.modelshot.ui
{
    public class ModelViewerStringPathUiItem : UiItem<ObjectStringPath>
    {
        public Color selectColor;
        public Color unselectColor;
        
        public override void UpdateItem(int index, ObjectStringPath data)
        {
            Index = index;
            Data = data;
            GetText(0).text = data.FilterName;
            gameObject.name = GetText(0).text;
        }
        
        public void ShowApply(int index)
        {
            if (index == Index)
            {
                GetImage(0).color = selectColor;
            }
            else
            {
                GetImage(0).color = unselectColor;
            }
        }
    }
}