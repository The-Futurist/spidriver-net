namespace Radio.Nordic.NRF24L01P
{
    //public abstract class REGISTER
    //{
    //    private byte[] register;
    //    private byte id;
    //    private int length;

    //    public byte[] Register { get => register; protected set => register = value; }
    //    public byte Id { get => id; protected set => id = value; }
    //    public int Length { get => length; protected set => length = value; }
    //}

    public interface IREGISTER
    {
        public byte REGID { get; }
        public ulong VALUE { get; set; }
        public byte READ { get => (byte)(0x00 | REGID); }
        public byte WRITE { get => (byte)(0x02 | REGID); }
        public int LENGTH { get; }
    }

    public struct REGISTER
    {
        private byte databits;
        public REGISTER(byte databits)
        {
            this.databits = databits;
        }

        public bool BIT0
        {
            get => (databits & 0x01) != 0; // Check if bit 0 is set
            set => databits = (byte)((databits & ~0x01) | (value ? 0x01 : 0x00));
        }
        public bool BIT1
        {
            get => (databits & 0x02) != 0; // Check if bit 1 is set
            set => databits = (byte)((databits & ~0x02) | (value ? 0x02 : 0x00));
        }
        public bool BIT2
        {
            get => (databits & 0x04) != 0; // Check if bit 2 is set
            set => databits = (byte)((databits & ~0x04) | (value ? 0x04 : 0x00));
        }
        public bool BIT3
        {
            get => (databits & 0x08) != 0; // Check if bit 3 is set
            set => databits = (byte)((databits & ~0x08) | (value ? 0x08 : 0x00));
        }
        public bool BIT4
        {
            get => (databits & 0x10) != 0; // Check if bit 4 is set
            set => databits = (byte)((databits & ~0x10) | (value ? 0x10 : 0x00));
        }
        public bool BIT5
        {
            get => (databits & 0x20) != 0; // Check if bit 5 is set
            set => databits = (byte)((databits & ~0x20) | (value ? 0x20 : 0x00));
        }
        public bool BIT6
        {
            get => (databits & 0x40) != 0; // Check if bit 6 is set
            set => databits = (byte)((databits & ~0x40) | (value ? 0x40 : 0x00));
        }
        public bool BIT7
        {
            get => (databits & 0x80) != 0; // Check if bit 7 is set
            set => databits = (byte)((databits & ~0x80) | (value ? 0x80 : 0x00));
        }

        public byte BYTE
        {
            get => databits;
            set => databits = value;
        }

        public static implicit operator byte(REGISTER ms)
        {
            return ms.databits;
        }

        // Explicit conversion from byte to MyStruct
        public static explicit operator REGISTER(byte b)
        {
            return new REGISTER(b);
        }
    }

}