namespace Radio.Nordic.NRF24L01P
{
    public static class Literals
    {
        public static byte BYTE(int arg)
        {
            return (byte)(arg & 0xff);
        }

        public static byte NBYTE(int arg)
        {
            return (byte)(~arg & 0xff);
        }

        public static byte BIT(byte p)
        {
            return (byte)(1 << p);
        }

    }
}