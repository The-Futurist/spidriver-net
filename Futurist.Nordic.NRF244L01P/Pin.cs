namespace Radio.Nordic
{
    // On the NRF devices, the pins are assigned to the following functions using standard numbering for IDC
    //
    // 1 => GND
    // 2 => VCC
    // 3 => CE
    // 4 => CSN
    // 5 => SCK
    // 6 => MOSI
    // 7 => MISO
    // 8 => IRQ
    //
    // The IDC Pin 1 is always the pin that has a square solder pad.
    // Sometimes, when viewed with Pin 1 at the top-right, the pins either come up towards 
    // you or are on the underside of the board pointing away from you - beware.
    //
    public enum Pin
    {
        Low,
        High
    }
}