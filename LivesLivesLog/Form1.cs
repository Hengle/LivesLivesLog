using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LivesLivesLog
{
    public partial class Form1 : Form
    {
        public string mLogSaveFilePath = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            mLogSaveFilePath = "./" + System.DateTime.Now.Year + System.DateTime.Now.Month + System.DateTime.Now.Day + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + ".txt";

            this.FormClosed += delegate (object s, FormClosedEventArgs fe) 
            {
                NetWorkServer.GetSingleton().DisConnect();
            };
        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string tmpContent = NetWorkServer.GetSingleton().Dequeue();


            if (string.IsNullOrEmpty(tmpContent) == false)
            {
                using (StreamWriter sw = new StreamWriter(mLogSaveFilePath, true))
                {
                    sw.WriteLine(tmpContent);
                    sw.Flush();
                    sw.Close();
                }



                string[] tmpContentArray = tmpContent.Split(new string[] { "\n"}, StringSplitOptions.RemoveEmptyEntries);
                string tmpLogType = "Log";
                if (tmpContentArray.Length>=1)
                {
                    tmpLogType = tmpContentArray[0];
                }

                for (int i = 1; i < tmpContentArray.Length; i++)
                {
                    ListViewItem tmpListViewItem = new ListViewItem();
                    tmpListViewItem.SubItems[0].Text = tmpLogType;
                    tmpListViewItem.SubItems.Add(tmpContentArray[i]);

                    this.listView1.Items.Add(tmpListViewItem);
                    tmpListViewItem.EnsureVisible();
                    tmpLogType = string.Empty;
                }

                //加一个空行
                ListViewItem tmpListViewItemEmpty = new ListViewItem();
                this.listView1.Items.Add(tmpListViewItemEmpty);
                tmpListViewItemEmpty.EnsureVisible();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tmpIp = this.textBox1.Text;
            NetWorkServer.GetSingleton().Init(tmpIp.Trim());
        }
    }
}
