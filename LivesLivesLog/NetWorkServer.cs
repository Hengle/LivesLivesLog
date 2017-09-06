using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LivesLivesLog
{
    class NetWorkServer
    {
        private static NetWorkServer mInstance = null;
        private Socket mSocketAccept = null;
        private Socket mSocketReceive = null;
        private Queue<string> mQueueReceived = new Queue<string>();

        private Thread mThreadReceive = null;

        public static NetWorkServer GetSingleton()
        {
            if (mInstance == null)
            {
                mInstance = new NetWorkServer();
            }
            return mInstance;
        }

        public void Init(string varIp)
        {
            mThreadReceive = new Thread(new ThreadStart(() =>
            {
                Receive(varIp);
            }));
            mThreadReceive.Start();
        }

        private void Receive(string varIp)
        {
            if (mSocketAccept == null)
            {
                mSocketAccept = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint tmpIpEndPoint = new IPEndPoint(IPAddress.Parse(varIp), 12312);
                mSocketAccept.Bind(tmpIpEndPoint);
                mSocketAccept.Listen(20);

                if (mSocketReceive == null)
                {
                    mSocketReceive = mSocketAccept.Accept();
                }

                while (mSocketReceive.Connected)
                {
                    byte[] tmpByteArray = new byte[4096];
                    int tmpReceived = mSocketReceive.Receive(tmpByteArray);
                    if(tmpReceived>0)
                    {
                        byte[] tmpRealData = new byte[tmpReceived];
                        for (int i = 0; i < tmpReceived; i++)
                        {
                            tmpRealData[i] = tmpByteArray[i];
                        }

                        string tmpContent = System.Text.Encoding.Default.GetString(tmpRealData);
                        lock(this)
                        {
                            mQueueReceived.Enqueue(tmpContent);
                        }
                    }
                    else
                    {
                        mSocketReceive = mSocketAccept.Accept();
                    }
                }
            }
        }

        public string Dequeue()
        {
            lock(this)
            {
                if(mQueueReceived.Count>0)
                {
                    return mQueueReceived.Dequeue();
                }
                return null;
            }
        }

        public void DisConnect()
        {
            if (mSocketAccept != null)
            {
                mSocketAccept.Close();
            }

            if (mSocketReceive != null)
            {
                mSocketReceive.Close();
            }

            if (mThreadReceive != null)
            {
                mThreadReceive.Abort();
            }
        }
    }
}
