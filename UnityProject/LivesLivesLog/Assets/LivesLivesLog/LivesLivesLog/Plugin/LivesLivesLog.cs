using UnityEngine;
using System.Collections;
using System.IO;

namespace LivesLives
{
    public class LivesLivesLog : MonoBehaviour
    {
        private static LivesLivesLog mInstance = null;

        public static LivesLivesLog GetSingleton()
        {
            if (mInstance == null)
            {
                GameObject tmpGo = new GameObject("LivesLivesLog");
                GameObject.DontDestroyOnLoad(tmpGo);

                mInstance = tmpGo.AddComponent<LivesLivesLog>();
            }
            return mInstance;
        }

        public void Init()
        {
            if (File.Exists(Application.persistentDataPath + "/LivesLivesLog.txt"))
            {
                Application.logMessageReceived += LogCallback;
                using (StreamReader sr = new StreamReader(Application.persistentDataPath + "/LivesLivesLog.txt"))
                {
                    string tmpIp = sr.ReadLine();
                    tmpIp = tmpIp.Trim();
                    NetWork.GetSingleton().Connect(tmpIp, 12312);
                }

            }
            else
            {
                Debug.LogError(Application.persistentDataPath + "/LivesLivesLog.txt Not Exist");
            }
        }

        private static void LogCallback(string condition, string stackTrace, LogType type)
        {
            NetWork.GetSingleton().Send(type + System.Environment.NewLine + condition + System.Environment.NewLine + stackTrace);
        }

        void OnDestroy()
        {
            NetWork.GetSingleton().DisConnect();
        }
    }
}


