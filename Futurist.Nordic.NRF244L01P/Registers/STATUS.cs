namespace Radio.Nordic.NRF24L01P
{
    //public class STATUS : REGISTER_SHORT
    //{
    //    public STATUS()
    //    {
    //        Id = 0x07;
    //    }
    //    public bool RX_DR
    //    {
    //        get => BIT6;
    //        set => BIT6 = value;
    //    }
    //    public bool TX_DS
    //    {
    //        get => BIT5;
    //        set => BIT5 = value;
    //    }
    //    public bool MAX_RT
    //    {
    //        get => BIT4;
    //        set => BIT4 = value;
    //    }
    //    public byte RX_P_NO => (byte)((Register[0] & 0x0E) >> 1);
    //    public bool TX_FULL => BIT0;
    //}

    public struct STATUS : IRegister
    {
        private REGISTER bits;
        public byte REGID => 0x07;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        #region BIT Fields
        public bool RX_DR
        {
            get => bits.BIT6;
            set => bits.BIT6 = value;
        }
        public bool TX_DS
        {
            get => bits.BIT5;
            set => bits.BIT5 = value;
        }
        public bool MAX_RT
        {
            get => bits.BIT4;
            set => bits.BIT4 = value;
        }
        public byte RX_P_NO => (byte)((VALUE & 0x0E) >> 1);
        public bool TX_FULL => bits.BIT0;

        public int LENGTH => 1;
        #endregion
    }

}