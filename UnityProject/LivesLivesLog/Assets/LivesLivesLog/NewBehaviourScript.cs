using UnityEngine;
using System.Collections;

namespace LivesLives
{
    public class NewBehaviourScript : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            LivesLivesLog.GetSingleton().Init();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnGUI()
        {
            if (GUILayout.Button("PrintLog"))
            {
                Debug.Log("LivesLivesLog Test LivesLivesLog Test LivesLivesLog Test LivesLivesLog Test");
            }
        }
    }
}


