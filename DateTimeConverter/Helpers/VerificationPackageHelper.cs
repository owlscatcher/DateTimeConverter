using System;

namespace DateTimeConverter.Helpers
{
    public static class VerificationPackageHelper
    {
        /// <summary>
        /// Checks if the package CRC16 control sum is valid
        /// </summary>
        /// <param name="message">8 byte package</param>
        /// <returns>Compare result</returns>
        public static bool IsVerified(byte[] message)
        {
            var receivedCrc = new byte[2];
            receivedCrc[0] = message[6];
            receivedCrc[1] = message[7];

            var calculatedCrc = GetCrc16(message);
            var diff = BitConverter.ToInt16(receivedCrc) - BitConverter.ToInt16(calculatedCrc);

            return diff == 0;
        }

        // TODO: Refactor this
        public static byte[] GetCrc16(byte[] message)
        {
            var crc = CalculateCrc16(message);
            return crc;
        }

        /// <summary>
        /// Added to the message CRC16 control sum
        /// </summary>
        /// <param name="message">8 byte package</param>
        /// <returns>8 byte array with CRC16 control sum</returns>
        public static byte[] GetVerifiedPackage(byte[] message)
        {
            var crc = CalculateCrc16(message);
            var vMessage = message;

            vMessage[6] = crc[0];
            vMessage[7] = crc[1];

            return vMessage;
        }

        /// <summary>
        /// Calculates the CRC16 value of a 8 byte message
        /// </summary>
        /// <param name="message">8 byte package</param>
        /// <returns>Two byte array</returns>
        private static byte[] CalculateCrc16(byte[] message)
        {
            var CRC = new byte[2];

            ushort CRCFull = 0xFFFF;
            char CRCLSB;
            for (int i = 0; i < (message.Length) - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);
                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);
                    if (CRCLSB == 1)
                    {
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                    }
                }
            }
            CRC[1] = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = (byte)(CRCFull & 0xFF);

            return CRC;
        }
    }
}
