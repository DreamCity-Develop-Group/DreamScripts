using System;

namespace Assets.Scripts.Scenes.Msg
{
    public class SceneMsg
    {
        public string SceneName;

        public int SceneBuildIndex;

        public Action OnSceneLoaded;

        public SceneMsg()
        {
            this.SceneBuildIndex = -1;
            this.SceneName = null;
            this.OnSceneLoaded = null;
        }

        public SceneMsg(string name, Action loaded)
        {
            this.SceneBuildIndex = -1;
            this.SceneName = name;
            this.OnSceneLoaded = loaded;
        }

        public SceneMsg(int index, Action loaded)
        {
            this.SceneBuildIndex = index;
            this.SceneName = null;
            this.OnSceneLoaded = loaded;
        }

    }
}
