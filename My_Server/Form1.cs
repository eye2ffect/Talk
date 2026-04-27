using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace My_Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        StreamReader streamReader1;
        StreamWriter streamWriter1;

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(connect);
            thread1.IsBackground = true;
            thread1.Start();
        }

        private void connect() 
        {
            TcpListener tcpListener1 = new TcpListener(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
            tcpListener1.Start();
            writeRichTextbox("서버 준비...클라이언트 기다리는 중...");

            TcpClient tcpClient1 = tcpListener1.AcceptTcpClient();
            writeRichTextbox("클라이언트 연결됨...");

            streamReader1 = new StreamReader(tcpClient1.GetStream()); 
            streamWriter1 = new StreamWriter(tcpClient1.GetStream()); 
            streamWriter1.AutoFlush = true;

            while (tcpClient1.Connected)  
            {
                string receiveData1 = streamReader1.ReadLine();
                writeRichTextbox(receiveData1);                  
            }
        }

        private void writeRichTextbox(string str) 
        {
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText(str + "\r\n"); });
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.ScrollToCaret(); }); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sendData1 = textBox3.Text; 
            streamWriter1.WriteLine(sendData1);  
        }
    }
}
