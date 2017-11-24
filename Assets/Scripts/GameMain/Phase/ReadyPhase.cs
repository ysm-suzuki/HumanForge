using System.Collections.Generic;

namespace GameMain
{
    public class ReadyPhase : Phase
    {
        public static string Tag = "ReadyPhase";

        private Dictionary<string, bool> _initializationFlags = new Dictionary<string, bool>();
        private const string SQLite = "SQLite";


        override public void Initialize()
        {
            _initializationFlags[SQLite] = false;

            GameMain.SQLite.Instance.Initialize(()=> { _initializationFlags[SQLite] = true; });
        }

        override public void Tick(float delta)
        {
            if (AllInitialized())
                End();
        }

        override protected void End()
        {
            UnityEngine.Debug.Log("Maser data Version : " + VersionData.loader.Get());

            base.End(MainPhase.Tag);
        }


        private bool AllInitialized()
        {
            foreach (var pair in _initializationFlags)
                if (!pair.Value)
                    return false;

            return true;
        }
    }
}

