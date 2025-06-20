﻿using Radio.Nordic.NRF24L01P;
using static Radio.Nordic.NRF24L01P.Literals;
using static Radio.Nordic.NRF24L01P.Pipe;

namespace UnitTests
{
    [TestClass]
    public sealed class RegisterAccessTests
    {
        public static string PORT = String.Empty; // NRF24L01P.TryGetNrfComPort();

        static RegisterAccessTests()
        {
            //if (NRF24L01P.TryGetNrfComPort(out PORT) == false)
            //{
            //    throw new InvalidOperationException("No atatched device was detectec.");
            //}

            //iodriver = DriverFactory.CreateFT232H("D3","D4"); //DriverFactory.CreateSPIDriver(PORT, Output.A);

        }
        public NRF24L01P CreateDevice()
        {
            var settings = new FT232HSettings() { CSNPin = "D3", CENPin = "D4",  IRQPin = "D5", ClockSpeed = 10_000_000 };
            return NRF24L01P.Create(settings);
        }
        [TestMethod]
        [DoNotParallelize]
        public void ConnectTest()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
        }
        [TestMethod]
        [DoNotParallelize]
        public void CONFIG()
        {
            using NRF24L01P nrf = CreateDevice();

            nrf.Connect();

            nrf.ReadRegister<CONFIG>(out var restore);
            nrf.ReadRegister<CONFIG>(out var changed);

            changed.MASK_RX_DR = !restore.MASK_RX_DR;
            changed.MASK_TX_DS = !restore.MASK_TX_DS;
            changed.MASK_MAX_RT = !restore.MASK_MAX_RT;
            changed.EN_CRC = !restore.EN_CRC;
            changed.CRCO = !restore.CRCO;
            changed.PWR_UP = !restore.PWR_UP;
            changed.PRIM_RX = !restore.PRIM_RX;

            nrf.WriteRegister(ref changed);

            nrf.ReadRegister<CONFIG>(out changed);

            Assert.AreNotEqual(changed.MASK_RX_DR, restore.MASK_RX_DR);
            Assert.AreNotEqual(changed.MASK_TX_DS, restore.MASK_TX_DS);
            Assert.AreNotEqual(changed.MASK_MAX_RT, restore.MASK_MAX_RT);
            Assert.AreNotEqual(changed.EN_CRC, restore.EN_CRC);
            Assert.AreNotEqual(changed.CRCO, restore.CRCO);
            Assert.AreNotEqual(changed.PWR_UP, restore.PWR_UP);
            Assert.AreNotEqual(changed.PRIM_RX, restore.PRIM_RX);

            nrf.WriteRegister(ref restore);

            nrf.ReadRegister<CONFIG>(out changed);

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
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<EN_AA>(out var restore);
            nrf.ReadRegister<EN_AA>(out var changed);
            changed[Pipe_0] = !restore[Pipe_0];
            changed[Pipe_1] = !restore[Pipe_1];
            changed[Pipe_2] = !restore[Pipe_2];
            changed[Pipe_3] = !restore[Pipe_3];
            changed[Pipe_4] = !restore[Pipe_4];
            changed[Pipe_5] = !restore[Pipe_5];
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<EN_AA>(out changed);
            Assert.AreNotEqual(changed[Pipe_0], restore[Pipe_0]);
            Assert.AreNotEqual(changed[Pipe_1], restore[Pipe_1]);
            Assert.AreNotEqual(changed[Pipe_2], restore[Pipe_2]);
            Assert.AreNotEqual(changed[Pipe_3], restore[Pipe_3]);
            Assert.AreNotEqual(changed[Pipe_4], restore[Pipe_4]);
            Assert.AreNotEqual(changed[Pipe_5], restore[Pipe_5]);
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<EN_AA>(out changed);
            Assert.AreEqual(changed[Pipe_0], restore[Pipe_0]);
            Assert.AreEqual(changed[Pipe_1], restore[Pipe_1]);
            Assert.AreEqual(changed[Pipe_2], restore[Pipe_2]);
            Assert.AreEqual(changed[Pipe_3], restore[Pipe_3]);
            Assert.AreEqual(changed[Pipe_4], restore[Pipe_4]);
            Assert.AreEqual(changed[Pipe_5], restore[Pipe_5]);
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_ADDR_P0()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RX_ADDR_P0>(out var restore);
            nrf.ReadRegister<RX_ADDR_P0>(out var changed);
            changed.ADDRESS = restore.ADDRESS + 1;
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RX_ADDR_P0>(out changed);
            Assert.AreNotEqual(changed.ADDRESS,(restore.ADDRESS)); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RX_ADDR_P0>(out changed);
            Assert.AreEqual(changed.ADDRESS,(restore.ADDRESS));  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_ADDR_P1()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RX_ADDR_P1>(out var restore);
            nrf.ReadRegister<RX_ADDR_P1>(out var changed);
            changed.ADDRESS = restore.ADDRESS + 1;
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RX_ADDR_P1>(out changed);
            Assert.AreNotEqual(changed.ADDRESS,(restore.ADDRESS)); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RX_ADDR_P1>(out changed);
            Assert.AreEqual(changed.ADDRESS,(restore.ADDRESS));  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_ADDR_P2()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RX_ADDR_P2>(out var restore);
            nrf.ReadRegister<RX_ADDR_P2>(out var changed);
            changed.VALUE = (byte)(restore.VALUE + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RX_ADDR_P2>(out changed);
            Assert.IsFalse(changed.VALUE == restore.VALUE); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RX_ADDR_P2>(out changed);
            Assert.IsTrue(changed.VALUE == restore.VALUE);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_ADDR_P3()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RX_ADDR_P3>(out var restore);
            nrf.ReadRegister<RX_ADDR_P3>(out var changed);
            changed.VALUE = (byte)(restore.VALUE + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RX_ADDR_P3>(out changed);
            Assert.IsFalse(changed.VALUE == restore.VALUE); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RX_ADDR_P3>(out changed);
            Assert.IsTrue(changed.VALUE == restore.VALUE);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_ADDR_P4()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RX_ADDR_P4>(out var restore);
            nrf.ReadRegister<RX_ADDR_P4>(out var changed);
            changed.VALUE = (byte)(restore.VALUE + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RX_ADDR_P4>(out changed);
            Assert.IsFalse(changed.VALUE == restore.VALUE); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RX_ADDR_P4>(out changed);
            Assert.IsTrue(changed.VALUE == restore.VALUE);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_ADDR_P5()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RX_ADDR_P5>(out var restore);
            nrf.ReadRegister<RX_ADDR_P5>(out var changed);
            changed.VALUE = (byte)(restore.VALUE + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RX_ADDR_P5>(out changed);
            Assert.IsFalse(changed.VALUE == restore.VALUE); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RX_ADDR_P5>(out changed);
            Assert.IsTrue(changed.VALUE == restore.VALUE);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void TX_ADDR()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<TX_ADDR>(out var restore);
            nrf.ReadRegister<TX_ADDR>(out var changed);
            changed.ADDRESS = restore.ADDRESS + 1;
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<TX_ADDR>(out changed);
            Assert.AreNotEqual(changed.ADDRESS,(restore.ADDRESS)); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<TX_ADDR>(out changed);
            Assert.AreEqual(changed.ADDRESS, (restore.ADDRESS));  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_PW_P0()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RX_PW_P0>(out var restore);
            nrf.ReadRegister<RX_PW_P0>(out var changed);
            changed.RX_PW = (byte)(restore.RX_PW + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RX_PW_P0>(out changed);
            Assert.IsFalse(changed.RX_PW == restore.RX_PW); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RX_PW_P0>(out changed);
            Assert.IsTrue(changed.RX_PW == restore.RX_PW);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_PW_P1()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RX_PW_P1>(out var restore);
            nrf.ReadRegister<RX_PW_P1>(out var changed);
            changed.RX_PW = (byte)(restore.RX_PW + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RX_PW_P1>(out changed);
            Assert.IsFalse(changed.RX_PW == restore.RX_PW); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RX_PW_P1>(out changed);
            Assert.IsTrue(changed.RX_PW == restore.RX_PW);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_PW_P2()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RX_PW_P2>(out var restore);
            nrf.ReadRegister<RX_PW_P2>(out var changed);
            changed.RX_PW = (byte)(restore.RX_PW + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RX_PW_P2>(out changed);
            Assert.IsFalse(changed.RX_PW == restore.RX_PW); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RX_PW_P2>(out changed);
            Assert.IsTrue(changed.RX_PW == restore.RX_PW);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_PW_P3()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RX_PW_P3>(out var restore);
            nrf.ReadRegister<RX_PW_P3>(out var changed);
            changed.RX_PW = (byte)(restore.RX_PW + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RX_PW_P3>(out changed);
            Assert.IsFalse(changed.RX_PW == restore.RX_PW); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RX_PW_P3>(out changed);
            Assert.IsTrue(changed.RX_PW == restore.RX_PW);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_PW_P4()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RX_PW_P4>(out var restore);
            nrf.ReadRegister<RX_PW_P4>(out var changed);
            changed.RX_PW = (byte)(restore.RX_PW + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RX_PW_P4>(out changed);
            Assert.IsFalse(changed.RX_PW == restore.RX_PW); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RX_PW_P4>(out changed);
            Assert.IsTrue(changed.RX_PW == restore.RX_PW);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RX_PW_P5()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RX_PW_P5>(out var restore);
            nrf.ReadRegister<RX_PW_P5>(out var changed);
            changed.RX_PW = (byte)(restore.RX_PW + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RX_PW_P5>(out changed);
            Assert.IsFalse(changed.RX_PW == restore.RX_PW); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RX_PW_P5>(out changed);
            Assert.IsTrue(changed.RX_PW == restore.RX_PW);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void DYNPD()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<DYNPD>(out var restore);
            nrf.ReadRegister<DYNPD>(out var changed);
            changed[Pipe_0] = !restore[Pipe_0];
            changed[Pipe_1] = !restore[Pipe_1];
            changed[Pipe_2] = !restore[Pipe_2];
            changed[Pipe_3] = !restore[Pipe_3];
            changed[Pipe_4] = !restore[Pipe_4];
            changed[Pipe_5] = !restore[Pipe_5];
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<DYNPD>(out changed);
            Assert.AreNotEqual(changed[Pipe_0], restore[Pipe_0]);
            Assert.AreNotEqual(changed[Pipe_1], restore[Pipe_1]);
            Assert.AreNotEqual(changed[Pipe_2], restore[Pipe_2]);
            Assert.AreNotEqual(changed[Pipe_3], restore[Pipe_3]);
            Assert.AreNotEqual(changed[Pipe_4], restore[Pipe_4]);
            Assert.AreNotEqual(changed[Pipe_5], restore[Pipe_5]);
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<DYNPD>(out changed);
            Assert.AreEqual(changed[Pipe_0], restore[Pipe_0]);
            Assert.AreEqual(changed[Pipe_1], restore[Pipe_1]);
            Assert.AreEqual(changed[Pipe_2], restore[Pipe_2]);
            Assert.AreEqual(changed[Pipe_3], restore[Pipe_3]);
            Assert.AreEqual(changed[Pipe_4], restore[Pipe_4]);
            Assert.AreEqual(changed[Pipe_5], restore[Pipe_5]);
        }
        [TestMethod]
        [DoNotParallelize]
        public void SETUP_AW()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<SETUP_AW>(out var restore);
            nrf.ReadRegister<SETUP_AW>(out var changed);
            changed.AW = (byte)(restore.AW + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<SETUP_AW>(out changed);
            Assert.IsFalse(changed.AW == restore.AW); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<SETUP_AW>(out changed);
            Assert.IsTrue(changed.AW == restore.AW);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void SETUP_RETR()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<SETUP_RETR>(out var restore);
            nrf.ReadRegister<SETUP_RETR>(out var changed);
            changed.ARC = (byte)(restore.ARC + 1);
            changed.ARD = (byte)(restore.ARD + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<SETUP_RETR>(out changed);
            Assert.AreNotEqual(changed.ARC, restore.ARC);
            Assert.AreNotEqual(changed.ARD, restore.ARD);
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<SETUP_RETR>(out changed);
            Assert.AreEqual(changed.ARC, restore.ARC);
            Assert.AreEqual(changed.ARD, restore.ARD);
        }
        [TestMethod]
        [DoNotParallelize]
        public void RF_CH()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RF_CH>(out var restore);
            nrf.ReadRegister<RF_CH>(out var changed);
            changed.CH = (byte)(restore.CH + 1);
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RF_CH>(out changed);
            Assert.IsFalse(changed.CH == restore.CH); // must not be equal
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RF_CH>(out changed);
            Assert.IsTrue(changed.CH == restore.CH);  // must be equal
        }
        [TestMethod]
        [DoNotParallelize]
        public void RF_SETUP()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<RF_SETUP>(out var restore);
            nrf.ReadRegister<RF_SETUP>(out var changed);
            changed.CONT_WAVE = !restore.CONT_WAVE;
            changed.RF_DR_LOW = !restore.RF_DR_LOW;
            changed.PLL_LOCK = !restore.PLL_LOCK;
            changed.RF_DR_HIGH = !restore.RF_DR_HIGH;
            changed.RF_PWR = NBYTE(restore.RF_PWR);

            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<RF_SETUP>(out changed);
            Assert.AreNotEqual(changed.CONT_WAVE, restore.CONT_WAVE);
            Assert.AreNotEqual(changed.RF_DR_LOW, restore.RF_DR_LOW);
            Assert.AreNotEqual(changed.PLL_LOCK, restore.PLL_LOCK);
            Assert.AreNotEqual(changed.RF_DR_HIGH, restore.RF_DR_HIGH);
            Assert.AreNotEqual(changed.RF_PWR, restore.RF_PWR);
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<RF_SETUP>(out changed);
            Assert.AreEqual(changed.CONT_WAVE, restore.CONT_WAVE);
            Assert.AreEqual(changed.RF_DR_LOW, restore.RF_DR_LOW);
            Assert.AreEqual(changed.PLL_LOCK, restore.PLL_LOCK);
            Assert.AreEqual(changed.RF_DR_HIGH, restore.RF_DR_HIGH);
            Assert.AreEqual(changed.RF_PWR, restore.RF_PWR);

        }
        [TestMethod]
        [DoNotParallelize]
        public void FEATURE()
        {
            using NRF24L01P nrf = CreateDevice();
            nrf.Connect();
            nrf.ReadRegister<FEATURE>(out var restore);
            nrf.ReadRegister<FEATURE>(out var changed);
            changed.EN_DPL = !restore.EN_DPL;
            changed.EN_ACK_PAY = !restore.EN_ACK_PAY;
            changed.EN_DYN_ACK = !restore.EN_DYN_ACK;
            nrf.WriteRegister(ref changed);
            nrf.ReadRegister<FEATURE>(out changed);
            Assert.AreNotEqual(changed.EN_DPL, restore.EN_DPL);
            Assert.AreNotEqual(changed.EN_ACK_PAY, restore.EN_ACK_PAY);
            Assert.AreNotEqual(changed.EN_DYN_ACK, restore.EN_DYN_ACK);
            nrf.WriteRegister(ref restore);
            nrf.ReadRegister<FEATURE>(out changed);
            Assert.AreEqual(changed.EN_DPL, restore.EN_DPL);
            Assert.AreEqual(changed.EN_ACK_PAY, restore.EN_ACK_PAY);
            Assert.AreEqual(changed.EN_DYN_ACK, restore.EN_DYN_ACK);
        }
    }

}                                                                      