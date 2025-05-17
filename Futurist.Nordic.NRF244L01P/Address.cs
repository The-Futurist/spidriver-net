using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futurist.Nordic.NRF244L01P
{
    public class Address
    {
        private ulong address;

        public Address(ulong Address) 
        { 
            address = Address;
        }

        public byte[] Bytes
        {
            get 
            {
                byte[] addr = BitConverter.GetBytes(address);
                byte[] buffer = new byte[5];
                Array.Copy(addr, buffer, 5);
                Array.Reverse(buffer);
                return buffer;
            }
        }

        public override string ToString()
        {
            string hexString = address.ToString("X10");
            return ($"{hexString[..2]}-{hexString[2..4]}-{hexString[4..6]}-{hexString[6..8]}-{hexString[8..10]}");
        }
    }
}