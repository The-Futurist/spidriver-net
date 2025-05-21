namespace Radio.Nordic.NRF24L01P
{
    //public abstract class REGISTER
    //{

    //    public byte[] Register { get => register; protected set => register = value; }
    //    public byte Id { get => id; protected set => id = value; }
    //    public int Length { get => length; protected set => length = value; }
    //}

    public interface IREGISTER
    {
        byte Id { get; }
        int Length { get; }
    }
}