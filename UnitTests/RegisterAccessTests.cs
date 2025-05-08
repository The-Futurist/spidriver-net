using Radio.Nordic;
using static Radio.Nordic.Literals;

namespace UnitTests
{
    [TestClass]
    public sealed class RegisterAccessTests
    {
        [TestMethod]
        [DoNotParallelize]
        public void ConnectTest()
        {
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
        }

        [TestMethod]
        [DoNotParallelize]
        public void CONFIG()
        {
            CONFIG restore, changed;

            using NRF24L01P nrf = new NRF24L01P("COM7");

            nrf.ConnectUSB();

            restore = nrf.ReadRegister<CONFIG>();
            changed = nrf.ReadRegister<CONFIG>();

            changed.MASK_RX_DR = !restore.MASK_RX_DR;
            changed.MASK_TX_DS = !restore.MASK_TX_DS;
            changed.MASK_MAX_RT = !restore.MASK_MAX_RT;
            changed.EN_CRC = !restore.EN_CRC;
            changed.CRCO = !restore.CRCO;
            changed.PWR_UP = !restore.PWR_UP;
            changed.PRIM_RX = !restore.PRIM_RX;

            nrf.WriteRegister(changed);

            changed = nrf.ReadRegister<CONFIG>();

            Assert.AreNotEqual(changed.MASK_RX_DR, restore.MASK_RX_DR);
            Assert.AreNotEqual(changed.MASK_TX_DS, restore.MASK_TX_DS);
            Assert.AreNotEqual(changed.MASK_MAX_RT, restore.MASK_MAX_RT);
            Assert.AreNotEqual(changed.EN_CRC, restore.EN_CRC);
            Assert.AreNotEqual(changed.CRCO, restore.CRCO);
            Assert.AreNotEqual(changed.PWR_UP, restore.PWR_UP);
            Assert.AreNotEqual(changed.PRIM_RX, restore.PRIM_RX);

            nrf.WriteRegister(restore);

            changed = nrf.ReadRegister<CONFIG>();

            Assert.AreEqual(changed.MASK_RX_DR, restore.MASK_RX_DR);
            Assert.AreEqual(changed.MASK_TX_DS, restore.MASK_TX_DS);
            Assert.AreEqual(changed.MASK_MAX_RT, restore.MASK_MAX_RT);
            Assert.AreEqual(changed.EN_CRC, restore.EN_CRC);
            Assert.AreEqual(changed.CRCO, restore.CRCO);
            Assert.AreEqual(changed.PWR_UP, restore.PWR_UP);
            Assert.AreEqual(changed.PRIM_RX, restore.PRIM_RX);
        }
        [TestMethod]
        [DoNotParallelize]
        public void EN_AA()
        {
            EN_AA restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<EN_AA>();
            changed = nrf.ReadRegister<EN_AA>();
            changed.ENAA_P0 = !restore.ENAA_P0;
            changed.ENAA_P1 = !restore.ENAA_P1;
            changed.ENAA_P2 = !restore.ENAA_P2;
            changed.ENAA_P3 = !restore.ENAA_P3;
            changed.ENAA_P4 = !restore.ENAA_P4;
            changed.ENAA_P5 = !restore.ENAA_P5;
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<EN_AA>();
            Assert.AreNotEqual(changed.ENAA_P0, restore.ENAA_P0);
            Assert.AreNotEqual(changed.ENAA_P1, restore.ENAA_P1);
            Assert.AreNotEqual(changed.ENAA_P2, restore.ENAA_P2);
            Assert.AreNotEqual(changed.ENAA_P3, restore.ENAA_P3);
            Assert.AreNotEqual(changed.ENAA_P4, restore.ENAA_P4);
            Assert.AreNotEqual(changed.ENAA_P5, restore.ENAA_P5);
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<EN_AA>();
            Assert.AreEqual(changed.ENAA_P0, restore.ENAA_P0);
            Assert.AreEqual(changed.ENAA_P1, restore.ENAA_P1);
            Assert.AreEqual(changed.ENAA_P2, restore.ENAA_P2);
            Assert.AreEqual(changed.ENAA_P3, restore.ENAA_P3);
            Assert.AreEqual(changed.ENAA_P4, restore.ENAA_P4);
            Assert.AreEqual(changed.ENAA_P5, restore.ENAA_P5);
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_ADDR_P0()
        {
            RX_ADDR_P0 restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RX_ADDR_P0>();
            changed = nrf.ReadRegister<RX_ADDR_P0>();
            changed.ADDR[0] = (byte)(restore.ADDR[0] + 1);
            changed.ADDR[1] = (byte)(restore.ADDR[1] + 1);
            changed.ADDR[2] = (byte)(restore.ADDR[2] + 1);
            changed.ADDR[3] = (byte)(restore.ADDR[3] + 1);
            changed.ADDR[4] = (byte)(restore.ADDR[4] + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RX_ADDR_P0>();
            Assert.IsFalse(changed.ADDR.SequenceEqual(restore.ADDR)); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RX_ADDR_P0>();
            Assert.IsTrue(changed.ADDR.SequenceEqual(restore.ADDR));  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_ADDR_P1()
        {
            RX_ADDR_P1 restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RX_ADDR_P1>();
            changed = nrf.ReadRegister<RX_ADDR_P1>();
            changed.ADDR[0] = (byte)(restore.ADDR[0] + 1);
            changed.ADDR[1] = (byte)(restore.ADDR[1] + 1);
            changed.ADDR[2] = (byte)(restore.ADDR[2] + 1);
            changed.ADDR[3] = (byte)(restore.ADDR[3] + 1);
            changed.ADDR[4] = (byte)(restore.ADDR[4] + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RX_ADDR_P1>();
            Assert.IsFalse(changed.ADDR.SequenceEqual(restore.ADDR)); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RX_ADDR_P1>();
            Assert.IsTrue(changed.ADDR.SequenceEqual(restore.ADDR));  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_ADDR_P2()
        {
            RX_ADDR_P2 restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RX_ADDR_P2>();
            changed = nrf.ReadRegister<RX_ADDR_P2>();
            changed.ADDR = (byte)(restore.ADDR + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RX_ADDR_P2>();
            Assert.IsFalse(changed.ADDR == restore.ADDR); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RX_ADDR_P2>();
            Assert.IsTrue(changed.ADDR == restore.ADDR);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_ADDR_P3()
        {
            RX_ADDR_P3 restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RX_ADDR_P3>();
            changed = nrf.ReadRegister<RX_ADDR_P3>();
            changed.ADDR = (byte)(restore.ADDR + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RX_ADDR_P3>();
            Assert.IsFalse(changed.ADDR == restore.ADDR); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RX_ADDR_P3>();
            Assert.IsTrue(changed.ADDR == restore.ADDR);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_ADDR_P4()
        {
            RX_ADDR_P4 restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RX_ADDR_P4>();
            changed = nrf.ReadRegister<RX_ADDR_P4>();
            changed.ADDR = (byte)(restore.ADDR + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RX_ADDR_P4>();
            Assert.IsFalse(changed.ADDR == restore.ADDR); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RX_ADDR_P4>();
            Assert.IsTrue(changed.ADDR == restore.ADDR);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_ADDR_P5()
        {
            RX_ADDR_P5 restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RX_ADDR_P5>();
            changed = nrf.ReadRegister<RX_ADDR_P5>();
            changed.ADDR = (byte)(restore.ADDR + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RX_ADDR_P5>();
            Assert.IsFalse(changed.ADDR == restore.ADDR); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RX_ADDR_P5>();
            Assert.IsTrue(changed.ADDR == restore.ADDR);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void TX_ADDR()
        {
            TX_ADDR restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<TX_ADDR>();
            changed = nrf.ReadRegister<TX_ADDR>();
            changed.ADDR[0] = (byte)(restore.ADDR[0] + 1);
            changed.ADDR[1] = (byte)(restore.ADDR[1] + 1);
            changed.ADDR[2] = (byte)(restore.ADDR[2] + 1);
            changed.ADDR[3] = (byte)(restore.ADDR[3] + 1);
            changed.ADDR[4] = (byte)(restore.ADDR[4] + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<TX_ADDR>();
            Assert.IsFalse(changed.ADDR.SequenceEqual(restore.ADDR)); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<TX_ADDR>();
            Assert.IsTrue(changed.ADDR.SequenceEqual(restore.ADDR));  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_PW_P0()
        {
            RX_PW_P0 restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RX_PW_P0>();
            changed = nrf.ReadRegister<RX_PW_P0>();
            changed.RX_PW = (byte)(restore.RX_PW + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RX_PW_P0>();
            Assert.IsFalse(changed.RX_PW == restore.RX_PW); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RX_PW_P0>();
            Assert.IsTrue(changed.RX_PW == restore.RX_PW);  // must be equal
        }

        [TestMethod]
        [DoNotParallelize]
        public void RX_PW_P1()
        {
            RX_PW_P1 restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RX_PW_P1>();
            changed = nrf.ReadRegister<RX_PW_P1>();
            changed.RX_PW = (byte)(restore.RX_PW + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RX_PW_P1>();
            Assert.IsFalse(changed.RX_PW == restore.RX_PW); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RX_PW_P1>();
            Assert.IsTrue(changed.RX_PW == restore.RX_PW);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_PW_P2()
        {
            RX_PW_P2 restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RX_PW_P2>();
            changed = nrf.ReadRegister<RX_PW_P2>();
            changed.RX_PW = (byte)(restore.RX_PW + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RX_PW_P2>();
            Assert.IsFalse(changed.RX_PW == restore.RX_PW); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RX_PW_P2>();
            Assert.IsTrue(changed.RX_PW == restore.RX_PW);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_PW_P3()
        {
            RX_PW_P3 restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RX_PW_P3>();
            changed = nrf.ReadRegister<RX_PW_P3>();
            changed.RX_PW = (byte)(restore.RX_PW + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RX_PW_P3>();
            Assert.IsFalse(changed.RX_PW == restore.RX_PW); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RX_PW_P3>();
            Assert.IsTrue(changed.RX_PW == restore.RX_PW);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_PW_P4()
        {
            RX_PW_P4 restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RX_PW_P4>();
            changed = nrf.ReadRegister<RX_PW_P4>();
            changed.RX_PW = (byte)(restore.RX_PW + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RX_PW_P4>();
            Assert.IsFalse(changed.RX_PW == restore.RX_PW); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RX_PW_P4>();
            Assert.IsTrue(changed.RX_PW == restore.RX_PW);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_PW_P5()
        {
            RX_PW_P5 restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RX_PW_P5>();
            changed = nrf.ReadRegister<RX_PW_P5>();
            changed.RX_PW = (byte)(restore.RX_PW + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RX_PW_P5>();
            Assert.IsFalse(changed.RX_PW == restore.RX_PW); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RX_PW_P5>();
            Assert.IsTrue(changed.RX_PW == restore.RX_PW);  // must be equal
        }

        [TestMethod]
        [DoNotParallelize]
        public void SETUP_AW()
        {
            SETUP_AW restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<SETUP_AW>();
            changed = nrf.ReadRegister<SETUP_AW>();
            changed.AW = (byte)(restore.AW + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<SETUP_AW>();
            Assert.IsFalse(changed.AW == restore.AW); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<SETUP_AW>();
            Assert.IsTrue(changed.AW == restore.AW);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void SETUP_RETR()
        {
            SETUP_RETR restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<SETUP_RETR>();
            changed = nrf.ReadRegister<SETUP_RETR>();
            changed.ARC = (byte)(restore.ARC + 1);
            changed.ARD = (byte)(restore.ARD + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<SETUP_RETR>();
            Assert.AreNotEqual(changed.ARC, restore.ARC);
            Assert.AreNotEqual(changed.ARD, restore.ARD);
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<SETUP_RETR>();
            Assert.AreEqual(changed.ARC, restore.ARC);
            Assert.AreEqual(changed.ARD, restore.ARD);
        }
        [TestMethod]
        [DoNotParallelize]
        public void RF_CH()
        {
            RF_CH restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RF_CH>();
            changed = nrf.ReadRegister<RF_CH>();
            changed.CH = (byte)(restore.CH + 1);
            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RF_CH>();
            Assert.IsFalse(changed.CH == restore.CH); // must not be equal
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RF_CH>();
            Assert.IsTrue(changed.CH == restore.CH);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RF_SETUP()
        {
            RF_SETUP restore, changed;
            using NRF24L01P nrf = new NRF24L01P("COM7");
            nrf.ConnectUSB();
            restore = nrf.ReadRegister<RF_SETUP>();
            changed = nrf.ReadRegister<RF_SETUP>();
            changed.CONT_WAVE = !restore.CONT_WAVE;
            changed.RF_DR_LOW = !restore.RF_DR_LOW;
            changed.PLL_LOCK = !restore.PLL_LOCK;
            changed.RF_DR_HIGH = !restore.RF_DR_HIGH;
            changed.RF_PWR = NBYTE(restore.RF_PWR);

            nrf.WriteRegister(changed);
            changed = nrf.ReadRegister<RF_SETUP>();
            Assert.AreNotEqual(changed.CONT_WAVE, restore.CONT_WAVE);
            Assert.AreNotEqual(changed.RF_DR_LOW, restore.RF_DR_LOW);
            Assert.AreNotEqual(changed.PLL_LOCK, restore.PLL_LOCK);
            Assert.AreNotEqual(changed.RF_DR_HIGH, restore.RF_DR_HIGH);
            Assert.AreNotEqual(changed.RF_PWR, restore.RF_PWR);
            nrf.WriteRegister(restore);
            changed = nrf.ReadRegister<RF_SETUP>();
            Assert.AreEqual(changed.CONT_WAVE, restore.CONT_WAVE);
            Assert.AreEqual(changed.RF_DR_LOW, restore.RF_DR_LOW);
            Assert.AreEqual(changed.PLL_LOCK, restore.PLL_LOCK);
            Assert.AreEqual(changed.RF_DR_HIGH, restore.RF_DR_HIGH);
            Assert.AreEqual(changed.RF_PWR, restore.RF_PWR);

        }
    }

}                                                                      