using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace UNIVERSKY
{
    public partial class mainForm : Form
    {
        public SerialPort ComDevice = new SerialPort();
        private void SerialInit()
        {
            CbbComList.Items.AddRange(SerialPort.GetPortNames());
            if (CbbComList.Items.Count > 0)
            {
                CbbComList.SelectedIndex = 0;
            }
            cbbBaudRate.SelectedIndex = 5;
            ComDevice.Parity = (Parity)0;
            ComDevice.DataBits = 8;
            ComDevice.StopBits = (StopBits)1;
            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);//绑定事件
            //btnOpen_Click_Event();
        }
        private void CbbComList_MouseDown(object sender, MouseEventArgs e)
        {
            CbbComList.Items.Clear();
            CbbComList.Items.AddRange(SerialPort.GetPortNames());
            if (CbbComList.Items.Count > 0)
            {
                CbbComList.SelectedIndex = 0;
            }
        }
        // 打开串口

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            btnOpen_Click_Event();
        }
        private void btnOpen_Click_Event()
        {
            if (CbbComList.Items.Count <= 0)
            {
                MessageBox.Show("没有发现串口,请检查线路！");
                return;
            }
            if (ComDevice.IsOpen == false)
            {
                ComDevice.PortName = CbbComList.SelectedItem.ToString();
                ComDevice.BaudRate = Convert.ToInt32(cbbBaudRate.SelectedItem.ToString());
                try
                {
                    //this._client = new TcpClient();
                    //this._client.Connect("127.0.0.1", portNo);
                    //data = new byte[this._client.ReceiveBufferSize];
                    Timer_Form.Interval = 50;
                    Timer_Form.Start();

                    ComDevice.Open();
                    BtnOpen.Text = "断开设备";
                    btnOpen_Img.BackgroundImage = Properties.Resources.green;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                try
                {
                    //this._client.Close();
                    if (!comPortUsing) ComDevice.Close();
                    BtnOpen.Text = "连接设备";
                    btnOpen_Img.BackgroundImage = Properties.Resources.Gray;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            CbbComList.Enabled = !ComDevice.IsOpen;
            cbbBaudRate.Enabled = !ComDevice.IsOpen;
        }
        // 发送数据
        public bool SendData(byte[] data)
        {
            if (ComDevice.IsOpen)
            {
                try
                {
                    ComDevice.Write(data, 0, data.Length);//发送数据
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("串口未打开", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        // 接收数据
        bool comPortUsing = false; //串口关闭使能状态  正在接收数据时不可关闭串口
        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            comPortUsing = true;
            byte[] ReDatas = new byte[ComDevice.BytesToRead];
            ComDevice.Read(ReDatas, 0, ReDatas.Length);//读取数据
            for (int i = 0; i < ReDatas.Length; i++)
            {
                SkyLink.SkyLink_Prepare(ReDatas[i]);
            }
            comPortUsing = false;
        }
    }
}
