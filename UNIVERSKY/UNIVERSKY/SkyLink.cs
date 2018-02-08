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

namespace UNIVERSKY
{
    public class SkyLink
    {
        //协议解析变量
        public static float Roll=0.0f, Pitch=0.0f, Yaw=0.0f;//角度
        public static float Rc_Thr =0,Rc_Roll=0,Rc_Pitch=0,Rc_Yaw=0;//遥控四通道
        public static float Rc_Aux1 = 0, Rc_Aux2 = 0, Rc_Aux3 = 0, Rc_Aux4 = 0, Rc_Aux5 = 0, Rc_Aux6 = 0;//遥控四通道
        public static Int16 Accel_X = 0, Accel_Y = 0, Accel_Z = 0;//三轴加速度
        public static Int16 Gyro_X = 0, Gyro_Y = 0, Gyro_Z = 0;//三轴陀螺仪
        public static Int16 Mag_X = 0, Mag_Y = 0, Mag_Z = 0;//三轴磁力计
        public static float Baro_Hight = 0.0f;//气压计高度
        public static float Laser_Height = 0.0f;//激光高度
        public static float Ultrasonic_Hight = 0.0f;//超声波高度
        public static Int32 ALT_USE=0;//(高度cm)
        public static byte FLY_MODEL = 0;// (飞行模式）
        public static byte ARMED = 0;//  0加锁 1解锁

        static byte[] RxBuffer = new byte[50];
        static byte _data_len = 0, _data_cnt = 0;
        static byte state = 0;
        public static void SkyLink_Prepare(byte SigChar)
        {
            if (state == 0 && SigChar == 0xAA)
            {
                state = 1;
                RxBuffer[0] = SigChar;
            }
            else if (state == 1 && SigChar == 0xAA)
            {
                state = 2;
                RxBuffer[1] = SigChar;
            }
            else if (state == 2 && SigChar < 0XF1)
            {
                state = 3;
                RxBuffer[2] = SigChar;
            }
            else if (state == 3 && SigChar < 50)
            {
                state = 4;
                RxBuffer[3] = SigChar;
                _data_len = SigChar;
                _data_cnt = 0;
            }
            else if (state == 4 && _data_len > 0)
            {
                _data_len--;
                RxBuffer[4 + _data_cnt++] = SigChar;
                if (_data_len == 0)
                    state = 5;
            }
            else if (state == 5)
            {
                state = 0;
                RxBuffer[4 + _data_cnt] = SigChar;
                SkyLink_Data_Analysis(RxBuffer, _data_cnt + 5);
            }
            else
                state = 0;
        }
        public static void SkyLink_Data_Analysis(byte[] data_buf, int num)
        {
            byte sum = 0;
            for (byte i = 0; i < (num - 1); i++)
                sum += data_buf[i];
            if (!(sum == data_buf[num - 1])) return;        //判断sum
            if (!(data_buf[0] == 0xAA && data_buf[1] == 0xAA)) return;      //判断帧头
            if (data_buf[2] == 0X01) //飞机姿态等基本信息
            {
                SkyLink_Status_Rec(data_buf);
            }
            if (data_buf[2] == 0X02) //飞机传感器数据
            {
                SkyLink_Sensor_Rec(data_buf);
            }
            if (data_buf[2] == 0X03) //飞机传感器数据
            {
                SkyLink_Rcdata_Rec(data_buf);
            }
            if (data_buf[2] == 0X07) //飞机传感器2数据
            {
                SkyLink_Sensor2_Rec(data_buf);
            }
        }
        //飞机姿态等基本信息
        private static void SkyLink_Status_Rec(byte[] data_buf)
        {
            byte[] Data = new byte[2];
            Data[0] = data_buf[5]; Data[1] = data_buf[4];
            Roll = System.BitConverter.ToInt16(Data, 0) / 100.0f;
            Data[0] = data_buf[7]; Data[1] = data_buf[6];
            Pitch = System.BitConverter.ToInt16(Data, 0) / 100.0f;
            Data[0] = data_buf[9]; Data[1] = data_buf[8];
            Yaw = System.BitConverter.ToInt16(Data, 0) / 100.0f;
            ALT_USE = System.BitConverter.ToInt32(data_buf,9);
            FLY_MODEL = data_buf[14];
            ARMED = data_buf[15];
        }
        //飞机传感器数据
        private static void SkyLink_Sensor_Rec(byte[] data_buf)
        {
            byte[] Data = new byte[2];
            Data[0] = data_buf[5]; Data[1] = data_buf[4];
            Accel_X = System.BitConverter.ToInt16(Data, 0);
            Data[0] = data_buf[7]; Data[1] = data_buf[6];
            Accel_Y = System.BitConverter.ToInt16(Data, 0);
            Data[0] = data_buf[9]; Data[1] = data_buf[8];
            Accel_Z = System.BitConverter.ToInt16(Data, 0);
            Data[0] = data_buf[11]; Data[1] = data_buf[10];
            Gyro_X = System.BitConverter.ToInt16(Data, 0);
            Data[0] = data_buf[13]; Data[1] = data_buf[12];
            Gyro_Y = System.BitConverter.ToInt16(Data, 0);
            Data[0] = data_buf[15]; Data[1] = data_buf[14];
            Gyro_Z = System.BitConverter.ToInt16(Data, 0);
            Data[0] = data_buf[17]; Data[1] = data_buf[16];
            Mag_X = System.BitConverter.ToInt16(Data, 0);
            Data[0] = data_buf[19]; Data[1] = data_buf[18];
            Mag_Y = System.BitConverter.ToInt16(Data, 0);
            Data[0] = data_buf[21]; Data[1] = data_buf[20];
            Mag_Z = System.BitConverter.ToInt16(Data, 0);
        }
        //飞机传感器2数据
        private static void SkyLink_Sensor2_Rec(byte[] data_buf)
        {
            byte[] Data = new byte[4];
            Data[0] = data_buf[7]; Data[1] = data_buf[6];
            Data[2] = data_buf[5]; Data[3] = data_buf[4];
            Baro_Hight = System.BitConverter.ToInt32(Data, 0)/ 100.0f;
            Data[0] = data_buf[11]; Data[1] = data_buf[10];
            Data[2] = data_buf[9]; Data[3] = data_buf[8];
            Ultrasonic_Hight = System.BitConverter.ToInt32(Data, 0) / 100.0f;
        }
        //飞机收到的控制数据
        private static void SkyLink_Rcdata_Rec(byte[] data_buf)
        {
            byte[] Data = new byte[2];
            Data[0] = data_buf[5]; Data[1] = data_buf[4];
            Rc_Thr = System.BitConverter.ToUInt16(Data, 0)/1.0f;
            Data[0] = data_buf[7]; Data[1] = data_buf[6];
            Rc_Yaw = System.BitConverter.ToUInt16(Data, 0) / 1.0f;
            Data[0] = data_buf[9]; Data[1] = data_buf[8];
            Rc_Roll = System.BitConverter.ToUInt16(Data, 0) / 1.0f;
            Data[0] = data_buf[11]; Data[1] = data_buf[10];
            Rc_Pitch = System.BitConverter.ToUInt16(Data, 0) / 1.0f;
            Data[0] = data_buf[13]; Data[1] = data_buf[12];
            Rc_Aux1 = System.BitConverter.ToUInt16(Data, 0) / 1.0f;
            Data[0] = data_buf[15]; Data[1] = data_buf[14];
            Rc_Aux2 = System.BitConverter.ToUInt16(Data, 0) / 1.0f;
            Data[0] = data_buf[17]; Data[1] = data_buf[16];
            Rc_Aux3 = System.BitConverter.ToUInt16(Data, 0) / 1.0f;
            Data[0] = data_buf[19]; Data[1] = data_buf[18];
            Rc_Aux4 = System.BitConverter.ToUInt16(Data, 0) / 1.0f;
            Data[0] = data_buf[21]; Data[1] = data_buf[20];
            Rc_Aux5 = System.BitConverter.ToUInt16(Data, 0) / 1.0f;
            Data[0] = data_buf[23]; Data[1] = data_buf[22];
            Rc_Aux6 = System.BitConverter.ToUInt16(Data, 0) / 1.0f;
            Rc_Thr = Rc_Thr - 1000;
            if (Rc_Thr < 0) Rc_Thr = 0;
            Rc_Yaw = Rc_Yaw - 1000;
            if (Rc_Yaw < 0) Rc_Yaw = 0;
            Rc_Roll = Rc_Roll - 1000;
            if (Rc_Roll < 0) Rc_Roll = 0;
            Rc_Pitch = Rc_Pitch - 1000;
            if (Rc_Pitch < 0) Rc_Pitch = 0;
            Rc_Aux1 = Rc_Aux1 - 1000;
            if (Rc_Aux1 < 0) Rc_Aux1 = 0;
            Rc_Aux2 = Rc_Aux2 - 1000;
            if (Rc_Aux2 < 0) Rc_Aux2 = 0;
            Rc_Aux3 = Rc_Aux3 - 1000;
            if (Rc_Aux3 < 0) Rc_Aux3 = 0;
            Rc_Aux4 = Rc_Aux4 - 1000;
            if (Rc_Aux4 < 0) Rc_Aux4 = 0;
            Rc_Aux5 = Rc_Aux5 - 1000;
            if (Rc_Aux5 < 0) Rc_Aux5 = 0;
            Rc_Aux6 = Rc_Aux6 - 1000;
            if (Rc_Aux6 < 0) Rc_Aux6 = 0;
        }

    }
}
