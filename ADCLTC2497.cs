using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareLib
{
    public class ADCLTC2497
    {

        DiolanI2CController m_i2c;
        byte m_slaveAddress;
        public enum CHIP_SLAVE_ADDRESS
        {
            LOW,
            HIGH,
            FLOAT
        };
         
        struct ChipSlaveAddressTable
        {
             public CHIP_SLAVE_ADDRESS CA2;
             public CHIP_SLAVE_ADDRESS CA1;
             public CHIP_SLAVE_ADDRESS CA0;
             public byte value;
        };
        // diffrential mux                         //SGL, ODD, A2, A1 , A0
        Dictionary<Tuple<CHANNEL, CHANNEL>, Tuple<byte, byte, byte, byte, byte>> m_diffMuxPlusMinus = new Dictionary<Tuple<CHANNEL, CHANNEL>, Tuple<byte, byte, byte, byte, byte>>();

        // diffrential mux                         //SGL, ODD, A2, A1 , A0
        Dictionary<Tuple<CHANNEL, CHANNEL>, Tuple<byte, byte, byte, byte, byte>> m_diffMuxMinusPlus = new Dictionary<Tuple<CHANNEL, CHANNEL>, Tuple<byte, byte, byte, byte, byte>>();

        //sibgle endded mux SGL, ODD, A2, A1 , A0
        Dictionary<CHANNEL, Tuple<byte, byte, byte, byte, byte>> m_seMux = new Dictionary<CHANNEL, Tuple<byte, byte, byte, byte, byte>>();

        Dictionary<Tuple<CHIP_SLAVE_ADDRESS, CHIP_SLAVE_ADDRESS, CHIP_SLAVE_ADDRESS>, byte> m_chipSlaveAddress = new Dictionary<Tuple<CHIP_SLAVE_ADDRESS, CHIP_SLAVE_ADDRESS, CHIP_SLAVE_ADDRESS>, byte>();

        void InitSlaveAddressTable()
        {
            m_chipSlaveAddress.Add(new Tuple<CHIP_SLAVE_ADDRESS, CHIP_SLAVE_ADDRESS, CHIP_SLAVE_ADDRESS>(CHIP_SLAVE_ADDRESS.LOW, CHIP_SLAVE_ADDRESS.LOW, CHIP_SLAVE_ADDRESS.LOW), Convert.ToByte("0010110", 2));
            // we can have here more , see data sheet.
           
        }

        void InitMuxTable()
        {

            m_diffMuxPlusMinus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL0, CHANNEL.CHANNEL1), new Tuple<byte, byte, byte, byte, byte>(0, 0, 0, 0, 0));
            m_diffMuxPlusMinus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL2, CHANNEL.CHANNEL3), new Tuple<byte, byte, byte, byte, byte>(0, 0, 0, 0, 1));
            m_diffMuxPlusMinus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL4, CHANNEL.CHANNEL5), new Tuple<byte, byte, byte, byte, byte>(0, 0, 0, 1, 0));
            m_diffMuxPlusMinus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL6, CHANNEL.CHANNEL7), new Tuple<byte, byte, byte, byte, byte>(0, 0, 0, 1, 1));
            m_diffMuxPlusMinus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL8, CHANNEL.CHANNEL9), new Tuple<byte, byte, byte, byte, byte>(0, 0, 1, 0, 0));
            m_diffMuxPlusMinus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL10, CHANNEL.CHANNEL11), new Tuple<byte, byte, byte, byte, byte>(0, 0, 1, 0, 1));
            m_diffMuxPlusMinus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL12, CHANNEL.CHANNEL13), new Tuple<byte, byte, byte, byte, byte>(0, 0, 1, 1, 0));
            m_diffMuxPlusMinus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL14, CHANNEL.CHANNEL15), new Tuple<byte, byte, byte, byte, byte>(0, 0, 1, 1, 1));


            m_diffMuxMinusPlus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL0, CHANNEL.CHANNEL1), new Tuple<byte, byte, byte, byte, byte>(0, 1, 0, 0, 0));
            m_diffMuxMinusPlus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL2, CHANNEL.CHANNEL3), new Tuple<byte, byte, byte, byte, byte>(0, 1, 0, 0, 1));
            m_diffMuxMinusPlus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL4, CHANNEL.CHANNEL5), new Tuple<byte, byte, byte, byte, byte>(0, 1, 0, 1, 0));
            m_diffMuxMinusPlus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL6, CHANNEL.CHANNEL7), new Tuple<byte, byte, byte, byte, byte>(0, 1, 0, 1, 1));
            m_diffMuxMinusPlus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL8, CHANNEL.CHANNEL9), new Tuple<byte, byte, byte, byte, byte>(0, 1, 1, 0, 0));
            m_diffMuxMinusPlus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL10, CHANNEL.CHANNEL11), new Tuple<byte, byte, byte, byte, byte>(0, 1, 1, 0, 1));
            m_diffMuxMinusPlus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL12, CHANNEL.CHANNEL13), new Tuple<byte, byte, byte, byte, byte>(0, 1, 1, 1, 0));
            m_diffMuxMinusPlus.Add(new Tuple<CHANNEL, CHANNEL>(CHANNEL.CHANNEL14, CHANNEL.CHANNEL15), new Tuple<byte, byte, byte, byte, byte>(0, 1, 1, 1, 1));


            m_seMux.Add(CHANNEL.CHANNEL0, new Tuple<byte, byte, byte, byte, byte>(1, 0, 0,0, 0));
            m_seMux.Add(CHANNEL.CHANNEL2, new Tuple<byte, byte, byte, byte, byte>(1, 0, 0, 0, 1));
            m_seMux.Add(CHANNEL.CHANNEL4, new Tuple<byte, byte, byte, byte, byte>(1, 0, 0, 1, 0));
            m_seMux.Add(CHANNEL.CHANNEL6, new Tuple<byte, byte, byte, byte, byte>(1, 0, 0, 1, 1));
            m_seMux.Add(CHANNEL.CHANNEL8, new Tuple<byte, byte, byte, byte, byte>(1, 0, 1, 0, 0));
            m_seMux.Add(CHANNEL.CHANNEL10, new Tuple<byte, byte, byte, byte, byte>(1, 0, 1, 0, 1));
            m_seMux.Add(CHANNEL.CHANNEL12, new Tuple<byte, byte, byte, byte, byte>(1, 0, 1, 1, 0));
            m_seMux.Add(CHANNEL.CHANNEL14, new Tuple<byte, byte, byte, byte, byte>(1, 0, 1, 1, 1));


            m_seMux.Add(CHANNEL.CHANNEL1, new Tuple<byte, byte, byte, byte, byte>(1, 1, 0, 0, 0));
            m_seMux.Add(CHANNEL.CHANNEL3, new Tuple<byte, byte, byte, byte, byte>(1, 1, 0, 0, 1));
            m_seMux.Add(CHANNEL.CHANNEL5, new Tuple<byte, byte, byte, byte, byte>(1, 1, 0, 1, 0));
            m_seMux.Add(CHANNEL.CHANNEL7, new Tuple<byte, byte, byte, byte, byte>(1, 1, 0, 1, 1));
            m_seMux.Add(CHANNEL.CHANNEL9, new Tuple<byte, byte, byte, byte, byte>(1, 1, 1, 0, 0));
            m_seMux.Add(CHANNEL.CHANNEL11, new Tuple<byte, byte, byte, byte, byte>(1, 1, 1, 0, 1));
            m_seMux.Add(CHANNEL.CHANNEL13, new Tuple<byte, byte, byte, byte, byte>(1, 1, 1, 1, 0));
            m_seMux.Add(CHANNEL.CHANNEL15, new Tuple<byte, byte, byte, byte, byte>(1, 0, 1, 1, 1));

        }
    

        public ADCLTC2497(CHIP_SLAVE_ADDRESS CA2,
                  CHIP_SLAVE_ADDRESS CA1,
                  CHIP_SLAVE_ADDRESS CA0,
                  DiolanI2CController i2c)
        {


            InitMuxTable();
            InitSlaveAddressTable();

            Tuple<CHIP_SLAVE_ADDRESS, CHIP_SLAVE_ADDRESS, CHIP_SLAVE_ADDRESS> t = new Tuple<CHIP_SLAVE_ADDRESS, CHIP_SLAVE_ADDRESS, CHIP_SLAVE_ADDRESS>(CA2, CA1, CA0);
            m_slaveAddress = m_chipSlaveAddress[t];

            m_i2c = i2c;
        }

        public enum CHANNEL
        {
            CHANNEL0,
            CHANNEL1,
            CHANNEL2,
            CHANNEL3,
            CHANNEL4,
            CHANNEL5,
            CHANNEL6,
            CHANNEL7,
            CHANNEL8,
            CHANNEL9,
            CHANNEL10,
            CHANNEL11,
            CHANNEL12,
            CHANNEL13,
            CHANNEL14,
            CHANNEL15,
        }

        public void SelectDiffrentialChannelPlusMinus(CHANNEL ChannelInPlus , CHANNEL ChannelInMinus , bool Enable)
        {
            byte Prehumble = 0x80;
            byte SG = 0;
            byte ODD_SIGN = 0;
            byte A2, A1, A0;
            byte dataToWrite = 0;
            byte enable = (byte)(Enable == true ? 0x20 : 0);

            Tuple <CHANNEL, CHANNEL> t = new Tuple<CHANNEL,CHANNEL>(ChannelInPlus , ChannelInMinus);

            SG = m_diffMuxPlusMinus[t].Item1;
            ODD_SIGN = m_diffMuxPlusMinus[t].Item2;
            A2 = m_diffMuxPlusMinus[t].Item3;
            A1 = m_diffMuxPlusMinus[t].Item4;
            A0 = m_diffMuxPlusMinus[t].Item5;

            dataToWrite = (byte)(Prehumble | enable |  SG << 4 | ODD_SIGN << 3 | A2<< 2 | A1 << 1 | A0);

            //see table here:  http://www.analog.com/media/en/technical-documentation/data-sheets/2497fb.pdf

            m_i2c.Write(m_slaveAddress, dataToWrite);
        }

        public void SelectDiffrentialChannelMinusPlus(CHANNEL ChannelInMinus , CHANNEL ChannelInPlus, bool Enable)
        {
            byte Prehumble = 0x80;
            byte SG = 0;
            byte ODD_SIGN = 0;
            byte A2, A1, A0;
            byte dataToWrite = 0;
            byte enable = (byte)(Enable == true ? 0x20 : 0);

            Tuple <CHANNEL, CHANNEL> t = new Tuple<CHANNEL,CHANNEL>(ChannelInPlus , ChannelInMinus);

            SG = m_diffMuxMinusPlus[t].Item1;
            ODD_SIGN = m_diffMuxPlusMinus[t].Item2;
            A2 = m_diffMuxPlusMinus[t].Item3;
            A1 = m_diffMuxPlusMinus[t].Item4;
            A0 = m_diffMuxPlusMinus[t].Item5;

            dataToWrite = (byte)(Prehumble | enable |  SG << 4 | ODD_SIGN << 3 | A2<< 2 | A1 << 1 | A0);

            //see table here:  http://www.analog.com/media/en/technical-documentation/data-sheets/2497fb.pdf

            m_i2c.Write(m_slaveAddress, dataToWrite);
        }

        void SelectSingleEnddedChannel(CHANNEL channel, bool Enable)
        {
            byte Prehumble = 0x80;
            byte SG = 0;
            byte ODD_SIGN = 0;
            byte A2, A1, A0;
            byte dataToWrite = 0;
            byte enable = (byte)(Enable == true ? 0x20 : 0);


            SG = m_seMux[channel].Item1;
            ODD_SIGN = m_seMux[channel].Item2;
            A2 = m_seMux[channel].Item3;
            A1 = m_seMux[channel].Item4;
            A0 = m_seMux[channel].Item5;

            dataToWrite = (byte)(Prehumble | enable | SG << 4 | ODD_SIGN << 3 | A2 << 2 | A1 << 1 | A0);

            //see table here:  http://www.analog.com/media/en/technical-documentation/data-sheets/2497fb.pdf

            m_i2c.Write(m_slaveAddress, dataToWrite);
        }

        public short ReadChannel()
        {
            ushort binaryTwoComplement = Read16Bit();
            binaryTwoComplement = 0x852A;
            // If a positive value, return it
            if ((binaryTwoComplement & 0x8000) == 0)
            {
                return (short)binaryTwoComplement;
            }

            // Otherwise perform the 2's complement math on the value
            return (short)((short)(~(binaryTwoComplement - 0x01)) * -1);
        }

        ushort Read16Bit()
        {
            byte[] data = Read24Bit();
            int r = (data[2] << 16) | (data[1]  << 8) | data[0];
            r = (r >> 6) & 0xFFFF;
            return (ushort)r;
        }

        byte [] Read24Bit()
        {
            byte [] data = {0,0,0};
            m_i2c.Read(data , 0, 3);
            return data;
        }

        public virtual void Connect(DiolanI2CController i2c)
        {
            m_i2c = i2c;
        }
       
    }
}
