using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace UNIVERSKY
{
    public partial class mainForm : Form
    {
        const int portNo = 8888;
        private TcpClient _client;
        byte[] data;
        Queue<float> Roll_List = new Queue<float>(360);
        Queue<float> Pitch_List = new Queue<float>(360);
        Queue<float> Yaw_List = new Queue<float>(360);

        public void Socket_SendMessage(string message)//发送数据
        {
            try
            {
                NetworkStream ns = this._client.GetStream();
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);//将要发送的数据转化为字节流，然后发送数据
                ns.Write(data, 0, data.Length);
                ns.Flush();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        private void mainPage_Wave()
        {
            int i = 0;
            foreach (var item in Roll_List)
            {
                RollWave.Series[0].Points.AddXY(i, item);

                i++;
            }
            i = 0;
            foreach (var item in Pitch_List)
            {

                PitchWave.Series[0].Points.AddXY(i, item);

                i++;
            }
            i = 0;
            foreach (var item in Yaw_List)
            {
                YawWave.Series[0].Points.AddXY(i, item);
                i++;
            }
            if (Roll_List.Count > 200)
            {
                Roll_List.Dequeue();
                Pitch_List.Dequeue();
                Yaw_List.Dequeue();
            }
        }
        private void Timer_Form_Tick(object sender, EventArgs e)
        {
            Label_RollData.Text = SkyLink.Roll.ToString("#0.0");
            Label_PitchData.Text = SkyLink.Pitch.ToString("#0.0");
            Label_YawData.Text = SkyLink.Yaw.ToString("#0.0");

            Roll_List.Enqueue(SkyLink.Roll);//入列
            Pitch_List.Enqueue(SkyLink.Pitch);//入列
            Yaw_List.Enqueue(SkyLink.Yaw);//入列

            RollWave.Series[0].Points.Clear();
            PitchWave.Series[0].Points.Clear();
            YawWave.Series[0].Points.Clear();
            mainPage_Wave();

            Thr_Color.Width = (int)(SkyLink.Rc_Thr / 1000 * 101);
            Rol_Color.Width = (int)(SkyLink.Rc_Roll / 1000 * 101);
            Pit_Color.Width = (int)(SkyLink.Rc_Pitch / 1000 * 101);
            Yaw_Color.Width = (int)(SkyLink.Rc_Yaw / 1000 * 101);
            Aux1_Color.Width = (int)(SkyLink.Rc_Aux1 / 1000 * 101);
            Aux2_Color.Width = (int)(SkyLink.Rc_Aux2 / 1000 * 101);
            Aux3_Color.Width = (int)(SkyLink.Rc_Aux3 / 1000 * 101);
            Aux4_Color.Width = (int)(SkyLink.Rc_Aux4 / 1000 * 101);
            Aux5_Color.Width = (int)(SkyLink.Rc_Aux5 / 1000 * 101);
            Aux6_Color.Width = (int)(SkyLink.Rc_Aux6 / 1000 * 101);

            if (SkyLink.Rc_Thr == 0) SkyLink.Rc_Thr = -1000;
            Rc_Thr_Label.Text = (SkyLink.Rc_Thr + 1000).ToString();
            if (SkyLink.Rc_Roll == 0) SkyLink.Rc_Roll = -1000;
            Rc_Roll_Label.Text = (SkyLink.Rc_Roll + 1000).ToString();
            if (SkyLink.Rc_Pitch == 0) SkyLink.Rc_Pitch = -1000;
            Rc_Pitch_Label.Text = (SkyLink.Rc_Pitch + 1000).ToString();
            if (SkyLink.Rc_Yaw == 0) SkyLink.Rc_Yaw = -1000;
            Rc_Yaw_Label.Text = (SkyLink.Rc_Yaw + 1000).ToString();
            if (SkyLink.Rc_Aux1 == 0) SkyLink.Rc_Aux1 = -1000;
            Rc_Aux1_Label.Text = (SkyLink.Rc_Aux1 + 1000).ToString();
            if (SkyLink.Rc_Aux2 == 0) SkyLink.Rc_Aux2 = -1000;
            Rc_Aux2_Label.Text = (SkyLink.Rc_Aux2 + 1000).ToString();
            if (SkyLink.Rc_Aux3 == 0) SkyLink.Rc_Aux3 = -1000;
            Rc_Aux3_Label.Text = (SkyLink.Rc_Aux3 + 1000).ToString();
            if (SkyLink.Rc_Aux4 == 0) SkyLink.Rc_Aux4 = -1000;
            Rc_Aux4_Label.Text = (SkyLink.Rc_Aux4 + 1000).ToString();
            if (SkyLink.Rc_Aux5 == 0) SkyLink.Rc_Aux5 = -1000;
            Rc_Aux5_Label.Text = (SkyLink.Rc_Aux5 + 1000).ToString();
            if (SkyLink.Rc_Aux6 == 0) SkyLink.Rc_Aux6 = -1000;
            Rc_Aux6_Label.Text = (SkyLink.Rc_Aux6 + 1000).ToString();

            Accel_X_Label.Text = SkyLink.Accel_X.ToString();
            Accel_Y_Label.Text = SkyLink.Accel_Y.ToString();
            Accel_Z_Label.Text = SkyLink.Accel_Z.ToString();
            Gyro_X_Label.Text = SkyLink.Gyro_X.ToString();
            Gyro_Y_Label.Text = SkyLink.Gyro_Y.ToString();
            Gyro_Z_Label.Text = SkyLink.Gyro_Z.ToString();
            Mag_X_Label.Text = SkyLink.Mag_X.ToString();
            Mag_Y_Label.Text = SkyLink.Mag_Y.ToString();
            Mag_Z_Label.Text = SkyLink.Mag_Z.ToString();

            Baro_Height_Label.Text = SkyLink.Baro_Hight.ToString();
            Laser_Height_Label.Text = SkyLink.Ultrasonic_Hight.ToString();

            Socket_SendMessage(SkyLink.Roll.ToString() + "," + SkyLink.Pitch.ToString() + "," + SkyLink.Yaw.ToString());//发送数据
        }
    }
}
