namespace Radio.Nordic.NRF24L01P
{
    public class SETUP_AW : REGISTER_SHORT
    {
        public SETUP_AW()
        {
            Id = 0x03;
        }
        public byte AW
        {
            get => (byte)(Register[0] & 0x03);
            set
            {
                Register[0] &= 0xFC;
                Register[0] |= (byte)(value & 0x03);
            }
        }
    }
}