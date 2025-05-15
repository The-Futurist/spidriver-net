namespace Radio.Nordic.NRF24L01P
{
    public class OBSERVE_TX : REGISTER_SHORT
    {
        public OBSERVE_TX()
        {
            Id = 0x08;
        }
        public byte PLOS_CNT => (byte)((Register[0] & 0xF0) >> 4);
        public byte ARC_CNT => (byte)(Register[0] & 0x0F);
    }
}