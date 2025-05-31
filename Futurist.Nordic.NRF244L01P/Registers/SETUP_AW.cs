namespace Radio.Nordic.NRF24L01P
{
    public struct SETUP_AW : IRegister
    {
        private REGISTER bits;
        public byte REGID => 0x03;
        public ulong VALUE { get => bits; set => bits = (REGISTER)value; }
        public byte AW
        {
            get => (byte)(VALUE & 0x03);
            set
            {
                VALUE &= 0xFC;
                VALUE |= (byte)(value & 0x03);
            }
        }

        public int LENGTH => 1;
    }
}