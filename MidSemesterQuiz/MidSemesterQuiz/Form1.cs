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
using System.IO.Ports;

namespace NewReplicant
{
    public partial class Form1 : Form
    {
        private SerialPort serialport = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);//NEW一@個OSerialPort並LA連s接Ó£g埠Xe，A鮑j率v，A同P位i檢E查d，A資Me料A位i元¡M，A停X¡Ó止i位i元¡M
        char[] inbyte = new char[200];//建O立Ds一@個O陣X
        string[] sentc = { "A1B", "A0B", "C" };//要n傳C送Xe的o值E
        int flag = 0;
        int i = 0;
        int time_test = 0;
        int num_value = 0;
        int[] chart_valueY = new int[65];
        UInt32 num_time = 0;

        public Form1()
        {
            InitializeComponent();
            serialport.ReadBufferSize = 1000;
            serialport.Open();
            timer1.Enabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chart1.ChartAreas["ChartArea1"].AxisX.Maximum = 65;
            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 5;
            for (i = 0; i < 65; i++)
                chart_valueY[i] = 0;
        }

        void serialport_datareceived(object sender, SerialDataReceivedEventArgs e)
        {

            if (serialport.BytesToRead >= 5)
            {
                flag = serialport.BytesToRead;
                serialport.Read(inbyte, 0, serialport.BytesToRead);
                serialport.DiscardInBuffer();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            serialport.Write(sentc[0]);
            serialport.DiscardOutBuffer();
            pictureBox1.Image = NewReplicant.Resource1.red;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialport.Write(sentc[1]);
            serialport.DiscardOutBuffer();
            pictureBox1.Image = NewReplicant.Resource1.green;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*label1.Text = time_test.ToString();
            time_test++; */

            serialport.DataReceived += new SerialDataReceivedEventHandler(serialport_datareceived);
            /* one word */
            /* if (flag >= 1)
             {
                 /*label1.Text = Convert.ToString(inbyte[0]);
                 label3.Text = label3.Text + inbyte[0];
                 if ((inbyte[0] >= 0x30) && (inbyte[0] <= 0x39))
                     label2.Text = Convert.ToString(inbyte[0]);

                 if (inbyte[0] == 0x43)
                     label3.Text=""; */

            /*if (inbyte[0] == 'N')
                label1.Text = "Turn ON";
            else if (inbyte[0] == 'F')
                label2.Text = "Turn OFF";

            flag = 0; 
        } */
            /* if (textBox1.TextLength >= 1)
                 {
                     char[] cv0 = textBox1.Text.ToCharArray();
                     label3.Text = cv0[0].ToString();
                     textBox1.Clear();
                 }
             /* more word */
            if (flag >= 1)
            {
                label1.Text = "";
                string news0 = new String(inbyte);
                label1.Text = news0;

                //label3.Text = label3.Text + news0;

                if (((inbyte[0] == 'A') || (inbyte[0] == 'C')) && (inbyte[4] == 'B'))
                {
                    num_value = 0;
                    for (i = 1; i < 4; i++)
                    {
                        if ((inbyte[i] >= 0x30) && (inbyte[i] <= 0x39))
                            num_value = num_value * 10 + (inbyte[i] - 0x30);
                        else
                            break;
                        //label3.Text = i.ToString();
                    }
                    if (i == 4)
                    {
                        if(inbyte[0] == 'A')
                        {
                            label2.Text = num_value.ToString();
                            if (num_time > 65)
                            {
                                chart1.ChartAreas["ChartArea1"].AxisX.Maximum = num_time;
                                chart1.ChartAreas["ChartArea1"].AxisX.Minimum = num_time - 65;
                            }

                            chart1.Series["Series1"].Points.Add(num_value);
                            num_time++;
                        }
                        else if(inbyte[0] == 'C')
                        {
                            label2.Text = num_value.ToString();
                            if (num_time > 65)
                            {
                                chart1.ChartAreas["ChartArea1"].AxisX.Maximum = num_time;
                                chart1.ChartAreas["ChartArea1"].AxisX.Minimum = num_time - 65;
                            }

                            chart1.Series["Series2"].Points.Add(num_value);
                        }
                    }
                    else
                        label2.Text = "Error";

                    //label2.Text = "";
                    //label2.Text = news0;
                }
                //num_time++;

                if (inbyte[0] == 0x43)
                    label3.Text = "";

                for (i = 0; i < 200; i++)
                {
                    inbyte[i] = Convert.ToChar(0x00);
                }
                flag = 0;
            }
            if (textBox1.TextLength >= 1)
            {
                char[] cv0 = textBox1.Text.ToCharArray();
                label3.Text = (5 * (Convert.ToSByte(cv0[0]) - 0x30) + 3).ToString();
                textBox1.Clear();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /*char cin[];
            /*serialport.Write(textBox1.Text);
            serialport.DiscardOutBuffer(); */
            /*cin = textBox1.Text;*/

        }

        private void button3_Click(object sender, EventArgs e)
        {
            serialport.Write("1");
            serialport.DiscardOutBuffer();
        }
    }
}
