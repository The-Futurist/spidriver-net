namespace Radio.Nordic.NRF24L01P
{
    public class SETUP_RETR : REGISTER_SHORT
    {
        public SETUP_RETR()
        {
            Id = 0x04;
        }
        public byte ARD
        {
            get => (byte)((Register[0] & 0xF0) >> 4);
            set
            {
                Register[0] &= 0x0F;
                Register[0] |= (byte)((value & 0x0F) << 4);
            }
        }
        public byte ARC
        {
            get => (byte)(Register[0] & 0x0F);
            set
            {
                Register[0] &= 0xF0;
                Register[0] |= (byte)(value & 0x0F);
            }
        }
    }
}