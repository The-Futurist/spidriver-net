namespace Radio.Nordic
{
    public class STATUS : REGISTER_SHORT
    {
        public STATUS()
        {
            Id = 7;
        }
        public bool RX_DR
        {
            get
            {
                return (Register[0] & 0x40) != 0;
            }
        }
        public bool TX_DS
        {
            get
            {
                return (Register[0] & 0x20) != 0;
            }
        }
        public bool MAX_RT
        {
            get
            {
                return (Register[0] & 0x10) != 0;
            }
        }
        public byte RX_P_NO
        {
            get
            {
                return (byte)((Register[0] & 0x0E) >> 1);
            }
        }
    }
}