namespace Radio.Nordic.NRF24L01P
{
    public struct REGISTER_LONG
    {
        private ulong databits;
        public ulong BYTES
        {
            get => databits;
            set => databits = value;
        }
    }
}