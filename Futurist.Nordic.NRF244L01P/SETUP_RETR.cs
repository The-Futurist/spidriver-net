namespace Radio.Nordic
{
    public class SETUP_RETR : REGISTER_SHORT
    {
        public SETUP_RETR()
        {
            Id = 4;
        }
        public byte ARD
        {
            get
            {
                return (byte)((Register[0] & 0xF0) >> 4);
            }
            set
            {
                Register[0] &= 0x0F;
                Register[0] |= (byte)((value & 0x0F) << 4);
            }
        }
        public byte ARC
        {
            get
            {
                return (byte)(Register[0] & 0x0F);
            }
            set
            {
                Register[0] &= 0xF0;
                Register[0] |= (byte)(value & 0x0F);
            }
        }
    }
}