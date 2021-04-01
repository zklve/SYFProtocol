using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SYFProtocol
{
    class Call
    {
        /// <summary>
        ///     Simple Modbus TCP master read inputs example.
        /// </summary>
        public static void ModbusTcpMasterReadInputs()
        {
            using (TcpClient client = new TcpClient("127.0.0.1", 502))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                // read five input values
                ushort startAddress = 0;
                ushort numInputs = 2;
                var inputs = master.ReadHoldingRegisters(1, startAddress, numInputs);

                byte[] data = new byte[4];
                for (int i = 0; i < inputs.Length; i++)
                {
                    byte[] byt = BitConverter.GetBytes(inputs[i]);
                    Array.Copy(byt, 0, data, i * numInputs, byt.Length);

                }

                var res = GetFloatFromBigEndian(data);
                Console.WriteLine(String.Format("{0,18}", res));
                //float value = BitConverter.ToSingle(data.Reverse().ToArray(), 0);
                //Console.WriteLine(string.Format("{0:#.00000}", value));
            }
        }

        static float GetFloatFromBigEndian(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes); // We have to reverse
            return BitConverter.ToSingle(bytes, 0);
        }
    }
}
