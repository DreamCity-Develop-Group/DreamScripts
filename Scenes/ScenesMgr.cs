using System;
using System.Collections;
using Assets.Scripts.Framework;
using Assets.Scripts.Scenes.Msg;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes
{
    public class ScenesMgr : ManagerBase
    {
        public static ScenesMgr Instance = null;
        void Awake()
        {
            Instance = this;

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;

            Add(SceneEvent.MENU_PLAY_SCENE,this);
        }
        SceneMsg msg;
        public  AsyncOperation Async;
        private void Start()
        {
            msg = new SceneMsg();
        }

        protected internal override void Execute(int eventCode,  object message)
        {
            switch (eventCode)
            {
                case SceneEvent.MENU_PLAY_SCENE:
                    msg = message as SceneMsg;
                    LoadScene(msg);
                    break;
                default:
                    break;
            }
        }

        public static bool Loading;
        private Action _onSceneLoaded = null;

        private void  LoadScene(SceneMsg msg)
        {
            if (msg.SceneBuildIndex!=-1)
            {
                // SceneManager.LoadScene(msg.SceneBuildIndex);
                 SceneManager.LoadScene(msg.SceneBuildIndex);
               

            }
            if (msg.SceneName!=null)
            {
                // SceneManager.LoadScene(msg.SceneName);
                 SceneManager.LoadScene(msg.SceneName);
               
            }
            if (msg.OnSceneLoaded !=null)
            {
                _onSceneLoaded = msg.OnSceneLoaded;
            }
      
        }

        /// <summary>
        /// 当场景加载完成的时候调用
        /// </summary>
        private void SceneManager_sceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
        {
            if(_onSceneLoaded != null)
            {
                _onSceneLoaded();
                
                _onSceneLoaded = null;
            }
        }

    }
}
