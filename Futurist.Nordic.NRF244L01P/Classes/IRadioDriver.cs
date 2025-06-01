// SEE: https://cdn.sparkfun.com/assets/3/d/8/5/1/nRF24L01P_Product_Specification_1_0.pdf

namespace Radio.Nordic.NRF24L01P
{
    public delegate void RefAction<T>(ref T R) where T : IRegister;

    public interface IRadioDriver
    {
        void Connect();
        void ReadRegister<T>(out T register) where T : struct, IRegister;
        void WriteRegister<T>(ref T register) where T : struct, IRegister;
        void EditRegister<T>(RefAction<T> Editor) where T : struct, IRegister;
        void SendCommand(byte Command);
        void SendCommand(byte Command, byte[] Buffer);
        void SendCommand(byte Command, Span<byte> Buffer);
        void Close();
        Pin CS { set; }
        Pin CE { set; }
    }
}