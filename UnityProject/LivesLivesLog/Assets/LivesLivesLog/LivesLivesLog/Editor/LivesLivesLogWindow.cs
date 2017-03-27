using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System;
using System.Diagnostics;

namespace LivesLives
{
    public class LivesLivesLogWindow : Editor
    {

        [MenuItem("LivesLives/LivesLivesLog")]
        static void ShowLivesLivesLogWindow()
        {
            Process.Start(Application.dataPath + "/LivesLivesLog/LivesLivesLog/Editor/LivesLivesLog.exe");
        }
    }
}


