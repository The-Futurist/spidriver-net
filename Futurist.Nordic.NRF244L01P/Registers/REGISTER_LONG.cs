using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace Radio.Nordic.NRF24L01P
{
    public struct REGISTER_LONG : IREGISTER
    {
        public FIVEBYTE Register;
        public byte id;
        public const int length = 5;

        public REGISTER_LONG()
        {
        }

        public byte Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Length => length;

        public byte[] BYTES
        {
            get
            {
                return MemoryMarshal.CreateSpan(ref Register[0], 5).ToArray();
            }
            set
            {
                int X = 0;

                foreach (byte b in value)
                {
                    Register[X++] = b;
                }
            }

        }

    }

    [System.Runtime.CompilerServices.InlineArray(5)]
    public struct FIVEBYTE
    {
        public byte BYTE;
    }
}