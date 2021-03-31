using System;
using System.Collections.Generic;

namespace SYFProtocol
{
    public class CRC16Verify
    {
        public static void Test( )
        {
            var data = new byte[] { 0x01, 0x03, 0x00, 0x04, 0x00, 0x02 };
            data = new byte[] { 0x01, 0x03, 0x04, 0x06, 0x51, 0x3F, 0x9E };

            //85  CA
            var xor = CRCCalc(data);
            //3B  32
            Console.WriteLine(BitConverter.ToString(xor));
            Console.ReadLine();
        }

        /// <summary>
        /// CRC校验
        /// </summary>
        /// <param name="data">校验数据</param>
        /// <returns>高低8位</returns>
        public static string CRCCalc(string data)
        {
            string[] datas = data.Split(' ');
            List<byte> bytedata = new List<byte>();

            foreach (string str in datas)
            {
                bytedata.Add(byte.Parse(str, System.Globalization.NumberStyles.AllowHexSpecifier));
            }
            byte[] crcbuf = bytedata.ToArray();
            //计算并填写CRC校验码
            int crc = 0xffff;
            int len = crcbuf.Length;
            for (int n = 0; n < len; n++)
            {
                byte i;
                crc = crc ^ crcbuf[n];
                for (i = 0; i < 8; i++)
                {
                    int TT;
                    TT = crc & 1;
                    crc = crc >> 1;
                    crc = crc & 0x7fff;
                    if (TT == 1)
                    {
                        crc = crc ^ 0xa001;
                    }
                    crc = crc & 0xffff;
                }

            }
            string[] redata = new string[2];
            redata[1] = Convert.ToString((byte)((crc >> 8) & 0xff), 16);
            redata[0] = Convert.ToString((byte)((crc & 0xff)), 16);
            //return FormatHEX(redata[0]) + " " + FormatHEX(redata[1]);
            return "";
        }

        /// <summary>
        /// CRC校验
        /// </summary>
        /// <param name="data">校验数据</param>
        /// <returns></returns>
        public static byte[] CRCCalc(byte[] data)
        {
            byte[] result = new byte[2];
            //计算并填写CRC校验码
            int crc = 0xffff;
            int len = data.Length;
            for (int n = 0; n < len; n++)
            {
                byte i;
                crc = crc ^ data[n];
                for (i = 0; i < 8; i++)
                {
                    int TT;
                    TT = crc & 1;
                    crc = crc >> 1;
                    crc = crc & 0x7fff;
                    if (TT == 1)
                    {
                        crc = crc ^ 0xa001;
                    }
                    crc = crc & 0xffff;
                }

            }
            result[0] = (byte)((crc & 0xff));
            result[1] = (byte)((crc >> 8) & 0xff);

            return result;
        }
    }
}
