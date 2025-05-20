namespace Radio.Nordic.NRF24L01P
{
    public class Address
    {
        private readonly ulong address;
        private readonly byte[] bytes;
        private readonly string hexString;

        public Address(ulong Address) 
        { 
            if (address > 0xFF_FF_FF_FF_FF)
                throw new ArgumentException("The value must <= 0xFF_FF_FF_FF_FF.", nameof(Address));

            address = Address;

            byte[] addr = BitConverter.GetBytes(address);
            bytes = new byte[5];
            Array.Copy(addr, bytes, 5);
            Array.Reverse(bytes);

            hexString = address.ToString("X10");
            hexString = $"{hexString[..2]}-{hexString[2..4]}-{hexString[4..6]}-{hexString[6..8]}-{hexString[8..10]}";
        }

        public byte[] Bytes => bytes;

        public override string ToString()
        {
            return hexString;
        }
    }
}