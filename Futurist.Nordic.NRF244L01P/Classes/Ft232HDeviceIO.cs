// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

namespace Radio.Nordic.NRF24L01P
{
    public class Ft232HDeviceIO : INRF24L01IO
    {
        public Pin CS { set => throw new NotImplementedException(); }
        public Pin CE { set => throw new NotImplementedException(); }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void ReadRegister<T>(out T register) where T : struct, IREGISTER
        {
            throw new NotImplementedException();
        }

        public void SendCommand(byte Command)
        {
            throw new NotImplementedException();
        }

        public void SendCommand(byte Command, byte[] Buffer)
        {
            throw new NotImplementedException();
        }

        public void WriteRegister<T>(ref T register) where T : struct, IREGISTER
        {
            throw new NotImplementedException();
        }
    }
}