using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.IO;


namespace LivesLives
{
    public class NetWork
    {
        private static NetWork mInstance = null;

        private Socket mSocketSend = null;

        private Queue<string> mQueueSend = new Queue<string>();

        public static NetWork GetSingleton()
        {
            if (mInstance == null)
            {
                mInstance = new NetWork();
            }
            return mInstance;
        }

        public void Connect(string varIp, int varPort)
        {
            if (mSocketSend == null)
            {
                Loom.RunAsync(() =>
                {
                    mSocketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    mSocketSend.Connect(varIp, varPort);
                    while (true)
                    {
                        if (mQueueSend.Count > 0)
                        {
                            string tmpContent = mQueueSend.Dequeue();
                            byte[] tmpData = System.Text.Encoding.Default.GetBytes(tmpContent);
                            mSocketSend.Send(tmpData);
                        }
                        Thread.Sleep(1);
                    }
                });
            }
        }

        public void Send(string varContent)
        {
            mQueueSend.Enqueue(varContent);
        }

        public void DisConnect()
        {
            if (mSocketSend != null)
            {
                mSocketSend.Close();
            }
        }
    }
}


