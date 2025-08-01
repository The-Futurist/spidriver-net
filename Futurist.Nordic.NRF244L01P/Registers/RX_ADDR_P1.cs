﻿namespace Radio.Nordic.NRF24L01P
{
    public struct RX_ADDR_P1 : IRegister
    {

        private REGISTER_LONG bits;
        public byte REGID => 0x0B;
        public ulong VALUE { get => bits.BYTES; set => bits.BYTES = value; }
        public ulong ADDRESS
        {
            get => bits.BYTES; set => bits.BYTES = value;
        }

        public int LENGTH => 5;
    }
}