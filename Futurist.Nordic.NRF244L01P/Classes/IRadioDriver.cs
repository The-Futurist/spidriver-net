// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

namespace Radio.Nordic.NRF24L01P
{
    public interface IRadioDriver
    {
        void Connect();
        void ReadRegister<T>(out T register) where T : struct, IREGISTER;
        void WriteRegister<T>(ref T register) where T : struct, IREGISTER;
        void SendCommand(byte Command);
        void SendCommand(byte Command, byte[] Buffer);
        void SendCommand(byte Command, Span<byte> Buffer);
        void Close();
        Pin CS { set; }
        Pin CE { set; }
    }
}