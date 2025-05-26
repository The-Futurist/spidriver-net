using System.Reflection.Metadata.Ecma335;

namespace Radio.Nordic.NRF24L01P
{
    public unsafe struct REGISTER_SHORT : IREGISTER
    {
        public byte Register;
        public byte id;
        public const int length = 1;

        public REGISTER_SHORT()
        {
        }
        public bool BIT0
        {
            get => (Register & BIT.ZERO) > 0; set => Register = (byte)((Register & ~BIT.ZERO) | (value ? BIT.ZERO : 0x00));
        }
        public bool BIT1
        {
            get => (Register & BIT.ONE) > 0; set => Register = (byte)((Register & ~BIT.ONE) | (value ? BIT.ONE : 0x00));
        }
        public bool BIT2
        {
            get => (Register & BIT.TWO) > 0; set => Register = (byte)((Register & ~BIT.TWO) | (value ? BIT.TWO : 0x00));
        }
        public bool BIT3
        {
            get => (Register & BIT.THREE) > 0; set => Register = (byte)((Register & ~BIT.THREE) | (value ? BIT.THREE : 0x00));
        }
        public bool BIT4
        {
            get => (Register & BIT.FOUR) > 0; set => Register = (byte)((Register & ~BIT.FOUR) | (value ? BIT.FOUR : 0x00));
        }
        public bool BIT5
        {
            get => (Register & BIT.FIVE) > 0; set => Register = (byte)((Register & ~BIT.FIVE) | (value ? BIT.FIVE : 0x00));
        }
        public bool BIT6
        {
            get => (Register & BIT.SIX) > 0; set => Register = (byte)((Register & ~BIT.SIX) | (value ? BIT.SIX : 0x00));
        }
        public bool BIT7
        {
            get => (Register & BIT.SEVEN) > 0; set => Register = (byte)((Register & ~BIT.SEVEN) | (value ? BIT.SEVEN : 0x00));
        }

        public ref byte BYTE
        {
            get => ref Register;
        }

        public byte Id
        {
            get { return id; } 
            set { id = value; }
        }

        public int Length => length;
    }
}