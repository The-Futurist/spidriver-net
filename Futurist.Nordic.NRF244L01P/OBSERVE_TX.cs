namespace Radio.Nordic
{
    public class OBSERVE_TX : REGISTER_SHORT
    {
        public OBSERVE_TX()
        {
            Id = 9;
        }
        public byte PLOS_CNT
        {
            get
            {
                return (byte)((Register[0] & 0xF0) >> 4);
            }
        }
        public byte ARC_CNT
        {
            get
            {
                return (byte)(Register[0] & 0x0F);
            }
        }
    }
}