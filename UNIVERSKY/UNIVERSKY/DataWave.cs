using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNIVERSKY
{
    public partial class mainForm : Form
    {
        // 20条线的数据
        bool[] SeriesEnabled = new bool[20];
        int X_Data_Length = 99;//X轴长度
        int Y_Data_Length = 1000;//Y轴长度
        Queue<float> Series0_List  = new Queue<float>();
        Queue<float> Series1_List  = new Queue<float>();
        Queue<float> Series2_List  = new Queue<float>();
        Queue<float> Series3_List  = new Queue<float>();
        Queue<float> Series4_List  = new Queue<float>();
        Queue<float> Series5_List  = new Queue<float>();
        Queue<float> Series6_List  = new Queue<float>();
        Queue<float> Series7_List  = new Queue<float>();
        Queue<float> Series8_List  = new Queue<float>();
        Queue<float> Series9_List  = new Queue<float>();
        Queue<float> Series10_List = new Queue<float>();
        Queue<float> Series11_List = new Queue<float>();
        Queue<float> Series12_List = new Queue<float>();
        Queue<float> Series13_List = new Queue<float>();
        Queue<float> Series14_List = new Queue<float>();
        Queue<float> Series15_List = new Queue<float>();
        Queue<float> Series16_List = new Queue<float>();
        Queue<float> Series17_List = new Queue<float>();
        Queue<float> Series18_List = new Queue<float>();
        Queue<float> Series19_List = new Queue<float>();
        bool SensorWaveEnabled = false;
        private void StartWave_Click(object sender, EventArgs e)
        {
            SensorWaveEnabled = !SensorWaveEnabled;
            if (SensorWaveEnabled)
            {
                StartWave.Text = "停止显示";
                Timer_Wave.Interval = 1;
                Timer_Wave.Start();
            }
            else
            {
                StartWave.Text = "开始显示";
                Timer_Wave.Stop();
            }
        }
        private void SensorsWave_Init()
        {
            for (int i = 0; i < X_Data_Length; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    SensorChart.Series[j].Points.Add(j);
                }
            }
        }
        private void Timer_Wave_Tick(object sender, EventArgs e)
        {
            Series0_List.Enqueue(SkyLink.Accel_X);//入列
            Series1_List.Enqueue(SkyLink.Accel_Y);//入列
            Series2_List.Enqueue(SkyLink.Accel_Z);//入列
            Series3_List.Enqueue(SkyLink.Gyro_X);//入列
            Series4_List.Enqueue(SkyLink.Gyro_Y);//入列
            Series5_List.Enqueue(SkyLink.Gyro_Z);//入列
            Series6_List.Enqueue(SkyLink.Mag_X);//入列
            Series7_List.Enqueue(SkyLink.Mag_Y);//入列
            Series8_List.Enqueue(SkyLink.Mag_Z);//入列
            Series9_List.Enqueue(SkyLink.Roll);//入列
            Series10_List.Enqueue(SkyLink.Pitch);//入列
            Series11_List.Enqueue(SkyLink.Yaw);//入列
            Series12_List.Enqueue(SkyLink.ALT_USE);//入列
            Series13_List.Enqueue(SkyLink.Baro_Hight);//入列
            Series14_List.Enqueue(SkyLink.Ultrasonic_Hight);//入列
            Series15_List.Enqueue(SkyLink.Laser_Height);//入列
            Series16_List.Enqueue(0);//入列
            Series17_List.Enqueue(0);//入列
            Series18_List.Enqueue(0);//入列
            Series19_List.Enqueue(0);//入列
            if (Series0_List.Count > X_Data_Length)
            {
                Series0_List.Dequeue();
                Series1_List.Dequeue();
                Series2_List.Dequeue();
                Series3_List.Dequeue();
                Series4_List.Dequeue();
                Series5_List.Dequeue();
                Series6_List.Dequeue();
                Series7_List.Dequeue();
                Series8_List.Dequeue();
                Series9_List.Dequeue();
                Series10_List.Dequeue();
                Series11_List.Dequeue();
                Series12_List.Dequeue();
                Series13_List.Dequeue();
                Series14_List.Dequeue();
                Series15_List.Dequeue();
                Series16_List.Dequeue();
                Series17_List.Dequeue();
                Series18_List.Dequeue();
                Series19_List.Dequeue();
            }
            for (int i = 0; i < 20; i++)
            {
                SensorChart.Series[i].Points.Clear(); //清屏
            }
            int List_Cnt = 0;
            if (Series_Check0.Checked)
            {
                foreach (var item in Series0_List)
                {
                    SensorChart.Series[0].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check1.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series1_List)
                {
                    SensorChart.Series[1].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check2.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series2_List)
                {
                    SensorChart.Series[2].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check3.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series3_List)
                {
                    SensorChart.Series[3].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check4.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series4_List)
                {
                    SensorChart.Series[4].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check5.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series5_List)
                {
                    SensorChart.Series[5].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check6.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series6_List)
                {
                    SensorChart.Series[6].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check7.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series7_List)
                {
                    SensorChart.Series[7].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check8.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series8_List)
                {
                    SensorChart.Series[8].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check9.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series9_List)
                {
                    SensorChart.Series[9].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check10.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series10_List)
                {
                    SensorChart.Series[10].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check11.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series11_List)
                {
                    SensorChart.Series[11].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check12.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series12_List)
                {
                    SensorChart.Series[12].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check13.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series13_List)
                {
                    SensorChart.Series[13].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check14.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series14_List)
                {
                    SensorChart.Series[14].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check15.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series15_List)
                {
                    SensorChart.Series[15].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check16.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series16_List)
                {
                    SensorChart.Series[16].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check17.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series17_List)
                {
                    SensorChart.Series[17].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check18.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series18_List)
                {
                    SensorChart.Series[18].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
            if (Series_Check19.Checked)
            {
                List_Cnt = 0;
                foreach (var item in Series19_List)
                {
                    SensorChart.Series[19].Points.AddXY(List_Cnt, item);
                    List_Cnt++;
                }
            }
        }
        private void Series_Check0_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[0].Enabled = Series_Check0.Checked;
        }

        private void Series_Check1_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[1].Enabled = Series_Check1.Checked;
        }

        private void Series_Check2_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[2].Enabled = Series_Check2.Checked;
        }

        private void Series_Check3_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[3].Enabled = Series_Check3.Checked;
        }

        private void Series_Check4_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[4].Enabled = Series_Check4.Checked;
        }

        private void Series_Check5_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[5].Enabled = Series_Check5.Checked;
        }

        private void Series_Check6_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[6].Enabled = Series_Check6.Checked;
        }

        private void Series_Check7_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[7].Enabled = Series_Check7.Checked;
        }

        private void Series_Check8_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[8].Enabled = Series_Check8.Checked;
        }

        private void Series_Check9_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[9].Enabled = Series_Check9.Checked;
        }

        private void Series_Check10_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[10].Enabled = Series_Check10.Checked;
        }

        private void Series_Check11_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[11].Enabled = Series_Check11.Checked;
        }

        private void Series_Check12_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[12].Enabled = Series_Check12.Checked;
        }

        private void Series_Check13_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[13].Enabled = Series_Check13.Checked;
        }

        private void Series_Check14_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[14].Enabled = Series_Check14.Checked;
        }

        private void Series_Check15_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[15].Enabled = Series_Check15.Checked;
        }

        private void Series_Check16_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[16].Enabled = Series_Check16.Checked;
        }

        private void Series_Check17_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[17].Enabled = Series_Check17.Checked;
        }

        private void Series_Check18_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[18].Enabled = Series_Check18.Checked;
        }

        private void Series_Check19_CheckedChanged(object sender, EventArgs e)
        {
            SensorChart.Series[19].Enabled = Series_Check19.Checked;
        }
    }

}
