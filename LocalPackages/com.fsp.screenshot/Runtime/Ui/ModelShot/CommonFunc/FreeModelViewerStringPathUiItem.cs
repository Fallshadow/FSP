using System;
using fsp.ObjectStylingDesigne;
using UnityEngine.UI;

namespace fsp.modelshot.ui
{
    public class FreeModelViewerStringPathUiItem : ModelViewerStringPathUiItem
    {
        public Toggle appearToggle;
        public Action<ObjectStringPath, int> SelectCurIndex;
        public Action<ObjectStringPath> DeleteCurIndex;
        public Action<ObjectStringPath> AppearCurIndex;
        public Action<ObjectStringPath> DisAppearCurIndex;
        
        public override void UpdateItem(int index, ObjectStringPath data)
        {
            Index = index;
            Data = data;
            GetText(0).text = data.FilterName;
            gameObject.name = GetText(0).text;
            GetButton(0).onClick.RemoveAllListeners();
            GetButton(1).onClick.RemoveAllListeners();
            appearToggle.onValueChanged.RemoveAllListeners();
            GetButton(0).onClick.AddListener(() =>
            {
                SelectCurIndex?.Invoke(Data, Index);
            });
            GetButton(1).onClick.AddListener(() => { DeleteCurIndex?.Invoke(Data); });
            appearToggle.onValueChanged.AddListener((bool isAppear) => {
                if (isAppear) AppearCurIndex?.Invoke(Data);
                else DisAppearCurIndex?.Invoke(Data);
            });
        }
    }
}