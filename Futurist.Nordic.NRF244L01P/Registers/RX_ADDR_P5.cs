﻿namespace Radio.Nordic.NRF24L01P
{
    public class RX_ADDR_P5 : REGISTER_SHORT
    {

        public RX_ADDR_P5()
        {
            Id = 0x0F;
        }
        public byte ADDR
        {
            get => Register[0]; set => Register[0] = value;
        }
    }
}