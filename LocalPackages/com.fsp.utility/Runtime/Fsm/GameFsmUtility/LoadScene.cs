using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fsp.utility
{
    public abstract class LoadScene : State<GameController>
    {
        private const string EMPTY_SCENE_NAME = "Empty";
        public string scene_Name = "Empty";
        private IEnumerator loading;

        public MonoBehaviour MonoObj => monoObj;
        private MonoBehaviour monoObj; 
        
        public LoadScene(MonoBehaviour monoP)
        {
            monoObj = monoP;
        }
        
        /// <summary>
        /// 开始加载场景，子类重写这个方法来进行一些独特的数据更新和加载界面
        /// </summary>
        protected abstract void onEnter();

        /// <summary>
        /// 场景关闭，子类重写这个方法来进行一些独特的数据更新和加载界面
        /// </summary>
        protected abstract void onExit();

        /// <summary>
        /// 加载场景中, 真正的场景加载会在这个消息之后
        /// </summary>
        protected abstract void onLoading();

        /// <summary>
        /// 场景加载完
        /// </summary>
        protected abstract void onLoadingEnd();

        /// <summary>
        /// 加载的场景id
        /// </summary>
        /// <returns></returns>
        protected abstract int levelId();

        public override void Enter()
        {
            ui.UiManager.instance.DestroyAllUi();//一定放在onenter前面
            onEnter();
            
            if (loading != null)
            {
                MonoObj.StopCoroutine(loading);
            }

            loading = _loading();
            MonoObj.StartCoroutine(loading);
        }

        public override void Exit()
        {
            onExit();
            if (loading != null)
            {
                MonoObj.StopCoroutine(loading);
            }

            loading = _loading();
            MonoObj.StartCoroutine(loading);
        }

        private IEnumerator _loading()
        {
            SceneManager.LoadScene(EMPTY_SCENE_NAME);
            yield return null;

            Utility.UnLoadAllUnusedAssets();
            yield return null;

            Utility.GcCollect();
            yield return null;

            onLoading();

            SceneManager.LoadSceneAsync(scene_Name);
            yield return null;

            onLoadingEnd();
        }
    }

}