namespace Radio.Nordic.NRF24L01P
{
    public static class LogicalExtensions
    {
        public static byte OR (this byte LEFT, byte RIGHT)
        {  
            return (byte)(LEFT | RIGHT); 
        }

        public static byte AND(this byte LEFT, byte RIGHT)
        {
            return (byte)(LEFT & RIGHT);
        }

        public static byte XOR(this byte LEFT, byte RIGHT)
        {
            return (byte)(LEFT ^ RIGHT);
        }

        public static byte NOT(this byte LEFT)
        {
            return (byte)(~LEFT);
        }
    }

    public struct BYTE
    {
        private byte value;

        public BYTE(byte value)
        {
            this.value = value;
        }

        public static BYTE operator &(BYTE a, BYTE b)
        {
            return new BYTE((byte)(a.value & b.value));
        }

        public static BYTE operator |(BYTE a, BYTE b)
        {
            return new BYTE((byte)(a.value | b.value));
        }

        public static BYTE operator ^(BYTE a, BYTE b)
        {
            return new BYTE((byte)(a.value ^ b.value));
        }

    }
}