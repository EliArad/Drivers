using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareLib
{

    namespace AD7291Regs
    {
        [global::System.AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
        sealed class BitfieldLengthAttribute : Attribute
        {
            uint length;

            public BitfieldLengthAttribute(uint length)
            {
                this.length = length;
            }

            public uint Length { get { return length; } }
        }

        static class PrimitiveConversion
        {
            public static long ToLong<T>(T t) where T : struct
            {
                long r = 0;
                int offset = 0;

                // For every field suitably attributed with a BitfieldLength
                foreach (System.Reflection.FieldInfo f in t.GetType().GetFields())
                {
                    object[] attrs = f.GetCustomAttributes(typeof(BitfieldLengthAttribute), false);
                    if (attrs.Length == 1)
                    {
                        uint fieldLength = ((BitfieldLengthAttribute)attrs[0]).Length;

                        // Calculate a bitmask of the desired length
                        long mask = 0;
                        for (int i = 0; i < fieldLength; i++)
                            mask |= 1 << i;

                        r |= ((UInt32)f.GetValue(t) & mask) << offset;

                        offset += (int)fieldLength;
                    }
                }

                return r;
            }
        }


        //Register COMMAND REGISTER
        struct COMMAND_REGISTER
        {
            [BitfieldLength(1)]
            public uint AutocycleMode;

            [BitfieldLength(1)]
            public uint RESET;

            [BitfieldLength(1)]
            public uint CleaAlert;
           

            [BitfieldLength(1)]
            public uint PolarityOfALERTPin;

            [BitfieldLength(1)]
            public uint EXT_REF;

            [BitfieldLength(1)]
            public uint NoiseDelayedBitTrialAndSampling;

            [BitfieldLength(1)]
            public uint DontCare;

            [BitfieldLength(1)]
            public uint TSense;

            [BitfieldLength(1)]
            public uint MuxCH0;

            [BitfieldLength(1)]
            public uint MuxCH1;

            [BitfieldLength(1)]
            public uint MuxCH2;

            [BitfieldLength(1)]
            public uint MuxCH3;

            [BitfieldLength(1)]
            public uint MuxCH4;

            [BitfieldLength(1)]
            public uint MuxCH5;

            [BitfieldLength(1)]
            public uint MuxCH6;

            [BitfieldLength(1)]
            public uint MuxCH7;
        }

        public class AD7291
        {
            bool m_tempEnable = true;

            float VREF = 2.5f;

            ushort [] m_voltageConData = new ushort[16];
            float[] m_temperatureConData = new float[16];
            
            /*
            public enum CHANNEL
            {
                CHANNEL0 = 0x80,
                CHANNEL1 = 0x40,
                CHANNEL2 = 0x20,
                CHANNEL3 = 0x10,
                CHANNEL4 = 0x8,
                CHANNEL5 = 0x4,
                CHANNEL6  = 0x2,
                CHANNEL7 = 0x1      
            }
            */
            public enum CHANNEL
            {
                CHANNEL0 = 0,
                CHANNEL1 = 1,
                CHANNEL2 = 2,
                CHANNEL3 = 3,
                CHANNEL4 = 4,
                CHANNEL5 = 5,
                CHANNEL6 = 6,
                CHANNEL7 = 7
            }

            const byte AD7291_REG_COMMAND = 0x00;
            const byte AD7291_REG_VOLTAGE = 0x01;
            const byte AD7291_REG_T_SENSE = 0x02;
            const byte AD7291_REG_T_AVERAGE = 0x03;
            const byte AD7291_REG_LIMIT_BASE = 0x4;
            const byte AD7291_REG_CH0_DATA_HIGH = 0x04;
            const byte AD7291_REG_CH0_DATA_LOW = 0x05;
            const byte AD7291_REG_CH0_HYST = 0x06;
            const byte AD7291_REG_CH1_DATA_HIGH = 0x07;
            const byte AD7291_REG_CH1_DATA_LOW = 0x08;
            const byte AD7291_REG_CH1_HYST = 0x09;
            const byte AD7291_REG_CH2_DATA_HIGH = 0x0A;
            const int AD7291_REG_CH2_DATA_LOW = 0x0B;
            const byte AD7291_REG_CH2_HYST = 0x0C;
            const byte AD7291_REG_CH3_DATA_HIGH = 0x0D;
            const byte AD7291_REG_CH3_DATA_LOW = 0x0E;
            const byte AD7291_REG_CH3_HYST = 0x0F;
            const byte AD7291_REG_CH4_DATA_HIGH = 0x10;
            const byte AD7291_REG_CH4_DATA_LOW = 0x11;
            const byte AD7291_REG_CH4_HYST = 0x12;
            const byte AD7291_REG_CH5_DATA_HIGH = 0x13;
            const byte AD7291_REG_CH5_DATA_LOW = 0x14;
            const byte AD7291_REG_CH5_HYST = 0x15;
            const byte AD7291_REG_CH6_DATA_HIGH = 0x16;
            const byte AD7291_REG_CH6_DATA_LOW = 0x17;
            const byte AD7291_REG_CH6_HYST = 0x18;
            const byte AD7291_REG_CH7_DATA_HIGH = 0x19;
            const byte AD7291_REG_CH7_DATA_LOW = 0x1A;
            const byte AD7291_REG_CH7_HYST = 0x2B;
            const byte AD7291_REG_T_SENSE_HIGH = 0x1C;
            const byte AD7291_REG_T_SENSE_LOW = 0x1D;
            const byte AD7291_REG_T_SENSE_HYST = 0x1E;
            const byte AD7291_REG_VOLTAGE_ALERT_STATUS = 0x1F;
            const byte AD7291_REG_T_ALERT_STATUS = 0x20;

            /* AD7291_REG_COMMAND Definition */
            const byte AD7291_COMMAND_TSENSE = (1 << 7);
            const byte AD7291_COMMAND_DELAY = (1 << 5);
            const byte AD7291_COMMAND_EXT_REF = (1 << 4);
            const byte AD7291_COMMAND_ALERT_POL_LOW = (1 << 3);
            const byte AD7291_COMMAND_ALERT_POL_HIGH = (0 << 3);
            const byte AD7291_COMMAND_CLR_ALERT = (1 << 2);
            const byte AD7291_COMMAND_RESET = (1 << 1);
            const byte AD7291_COMMAND_AUTOCYCLE = (1 << 0);

            DiolanI2CController m_i2c;
            byte m_slaveAddress;
            double LSB_Volatage;


            COMMAND_REGISTER m_cmdReg = new COMMAND_REGISTER();

            public AD7291(byte slaveAddress, DiolanI2CController i2c)
            {
                m_slaveAddress = slaveAddress;
                m_i2c = i2c;
                SetVREF(VREF);


                /* It is recommended to enable AD7291_COMMAND_DELAY feature for
                   normal operation. */
                m_cmdReg.AutocycleMode = 1;
                m_cmdReg.EXT_REF = 0;
                m_cmdReg.NoiseDelayedBitTrialAndSampling = 1;
                m_cmdReg.TSense = 1;

                ushort data = (ushort)(PrimitiveConversion.ToLong(m_cmdReg));
                SetCommandRegister(data); 

                Console.WriteLine("Command resgister {0}", data);
            }

            public void SetVREF(double VREF)
            {
                LSB_Volatage = (double)(VREF / 4096.0);
            }
            
            ushort GetCommandRegister()
            {
                try
                {
                    byte[] receiveBuffer = new byte[2];
                    m_i2c.Write(m_slaveAddress, AD7291_REG_COMMAND);
                    receiveBuffer[0] = m_i2c.Read(m_slaveAddress);
                    receiveBuffer[1] = m_i2c.Read(m_slaveAddress);

                    ushort registerValue = (ushort)((receiveBuffer[0] << 8) + receiveBuffer[1]);
                    return registerValue;
                }
                catch (Exception err)
                {
                    throw (new SystemException(err.Message));
                }
            }
            
            public void SetMux(CHANNEL channel)
            {

                byte val = 1;
                switch (channel)
                {
                    case CHANNEL.CHANNEL0:
                        m_cmdReg.MuxCH7 = val;
                        break;
                    case CHANNEL.CHANNEL1:
                        m_cmdReg.MuxCH6 = val;
                        break;
                    case CHANNEL.CHANNEL2:
                        m_cmdReg.MuxCH5 = val;
                        break;
                    case CHANNEL.CHANNEL3:
                        m_cmdReg.MuxCH4 = val;
                        break;
                    case CHANNEL.CHANNEL4:
                        m_cmdReg.MuxCH3 = val;
                        break;
                    case CHANNEL.CHANNEL5:
                        m_cmdReg.MuxCH2 = val;
                        break;
                    case CHANNEL.CHANNEL6:
                        m_cmdReg.MuxCH1 = val;
                        break;
                    case CHANNEL.CHANNEL7:
                        m_cmdReg.MuxCH0 = val;
                    break;
                }
                ushort data = (ushort)(PrimitiveConversion.ToLong(m_cmdReg));
                SetCommandRegister(data);
            }

            public void ClearMux(CHANNEL channel)
            {

                byte val = 0;
                switch (channel)
                {
                    case CHANNEL.CHANNEL0:
                        m_cmdReg.MuxCH7 = val;
                    break;
                    case CHANNEL.CHANNEL1:
                        m_cmdReg.MuxCH6 = val;
                        break;
                    case CHANNEL.CHANNEL2:
                        m_cmdReg.MuxCH5 = val;
                    break;
                    case CHANNEL.CHANNEL3:
                       m_cmdReg.MuxCH4 = val;
                    break;
                    case CHANNEL.CHANNEL4:
                        m_cmdReg.MuxCH3 = val;
                    break;
                    case CHANNEL.CHANNEL5:
                        m_cmdReg.MuxCH2 = val;
                    break;
                    case CHANNEL.CHANNEL6:
                        m_cmdReg.MuxCH1 = val;
                    break;
                    case CHANNEL.CHANNEL7:
                        m_cmdReg.MuxCH0 = val;
                    break;
                }
                ushort data = (ushort)(PrimitiveConversion.ToLong(m_cmdReg));
                SetCommandRegister(data);

            }

            void SetCommandRegister(ushort registerValue)
            {

                //while (true)
                {
                    try
                    {
                        byte[] sendData = { 0, 0, 0 };

                        sendData[0] = AD7291_REG_COMMAND;
                        sendData[1] = (byte)(((registerValue & 0xFF00) >> 8));
                        sendData[2] = (byte)(((registerValue & 0x00FF)));
                        m_i2c.Write(m_slaveAddress, sendData);
                    }
                    catch (Exception err)
                    {
                        Thread.Sleep(10);
                    }
                }
            }

            /* Writes data into a register. */
            void SetRegisterValue(byte registerAddress,
                                  ushort registerValue)
            {


                byte[] sendData = { 0, 0, 0 };

                sendData[0] = registerAddress;
                sendData[1] = (byte)(((registerValue & 0xFF00) >> 8));
                sendData[2] = (byte)(((registerValue & 0x00FF)));
                m_i2c.Write(m_slaveAddress, sendData);
            }

            void GetRegisterValue(byte registerAddress,
                                  ushort [] registerValue,
                                  int count)
            {
                 
                byte[] receiveBuffer = new byte[2];
                m_i2c.Write(m_slaveAddress, registerAddress);
                int j = 0;
                for (int i = 0; i < count * 2; i++)
                {
                    receiveBuffer[0] = m_i2c.Read(m_slaveAddress);
                    receiveBuffer[1] = m_i2c.Read(m_slaveAddress);
                    registerValue[j++] = (ushort)((receiveBuffer[0] << 8) + receiveBuffer[1]);
                }                
            }
                     
            void GetRegisterValue(byte registerAddress,
                                  out ushort registerValue)
            {

                byte[] receiveBuffer = new byte[2];
                m_i2c.Write(m_slaveAddress, registerAddress);
                m_i2c.Read(m_slaveAddress, receiveBuffer);
                registerValue = (ushort)((receiveBuffer[0] << 8) + receiveBuffer[1]);
            } 


            public string GetTemperatureConversionResults(out double data, bool Average)
            {
                
                lock (this)
                {
                    data = 0;
                    try
                    {
                    
                        ushort[] temperatureData = new ushort[16];
                        byte channel = 0;
                        byte channelAddress = 0;
                        int count = 0;
                        ushort commandValue = 0;
                        byte mask = 0x80;

                        commandValue = GetCommandRegister();
                        // get the mask channels values
                        byte ChannelMux = (byte)((commandValue & 0xFF00) >> 8);
                        if (ChannelMux == 0)
                        {
                            return "No Channel active selected in adc mux"; // no channels are active
                        }

                        // Loop to count how many channels are active in the command register 
                        while (mask != 0)
                        {
                            if ((ChannelMux & mask) == mask)
                            {
                                count++;
                            }
                            mask = (byte)(mask >> 1);
                        }

                        ushort tsenseReg = 0;
                        if (Average == false)
                        {
                            GetRegisterValue(AD7291_REG_T_SENSE,
                                             temperatureData,
                                             count);

                            channelAddress = (byte)((temperatureData[channel] & 0xF000) >> 12);
                            tsenseReg = (ushort)((temperatureData[channel] & 0x0FFF));

                            if (channelAddress != 0x8)
                            {
                                return "failed";
                            }
                        }
                        else
                        {
                            GetRegisterValue(AD7291_REG_T_AVERAGE,
                                             temperatureData,
                                             count);

                            channelAddress = (byte)((temperatureData[channel] & 0xF000) >> 12);
                            tsenseReg = (ushort)((temperatureData[channel] & 0x0FFF));

                            if (channelAddress != 0x9)
                            {
                                return "failed";
                            }
                        }



                        if ((tsenseReg & 0x800) == 0x800)
                        {
                            /* Negative Temperature = (4096 - ADC Code) / 4 */
                            tsenseReg = (ushort)(4096 - tsenseReg);
                            data = tsenseReg;
                            data = data / 4;
                            data = data * (-1);
                        }
                        else
                        {
                            /* Positive Temperature = ADC Code / 4 */
                            data = tsenseReg;
                            data = data / 4;
                        }

                        return "ok";
                    }
                    catch (Exception err)
                    {
                        return err.Message;
                    }

                }
            }

            public string LimitChannelVoltage(CHANNEL channel , ushort lowLimit, ushort highLimit)
            {
                try
                {
                    highLimit &= 0x0FFF; // must containe zero at D12 - D15
                    lowLimit &= 0x0FFF; // must containe zero at D12 - D15
                    SetRegisterValue((byte)(AD7291_REG_LIMIT_BASE + (byte)channel), highLimit);
                    SetRegisterValue((byte)(AD7291_REG_LIMIT_BASE + (byte)channel + 1), highLimit);
                    return "ok";
                }
                catch (Exception err)
                {
                    return err.Message;
                }
            }

            int maxVal = 0;
            int minVal = 100000;
            public string GetVoltageConversionResults(CHANNEL channel, out double data)
            {
                lock (this)
                {
                    try
                    {
                        data = 0;
                        string res;
                        byte count = 0;
                        if ((res = GetVoltageConversionResults(out count, m_voltageConData)) != "ok")
                            return res;


                        if (channel == CHANNEL.CHANNEL0)
                        {
                            if (m_voltageConData[(byte)channel] > maxVal)
                                maxVal = m_voltageConData[(byte)channel];

                            if (m_voltageConData[(byte)channel] < minVal)
                                minVal = m_voltageConData[(byte)channel];

                            Console.WriteLine("{0} ,{1} , {2}" , m_voltageConData[(byte)channel] , maxVal, minVal);
                        }
                        data = m_voltageConData[(byte)channel]  * LSB_Volatage;
                        return "ok";
                    }
                    catch (Exception err)
                    {
                        throw (new SystemException(err.Message));
                    }
                }
            }

            string GetVoltageConversionResults(out byte count, ushort [] data)
            {
                try
                {
                    ushort commandValue = 0;
                    byte mask = 0x80;
                    ushort[] voltageData = new ushort[16];
                    byte channel = 0;
                    byte channelAddress = 0;
                    count = 0;
                    commandValue = GetCommandRegister();
                    // get the mask channels values
                    byte ChannelMux = (byte)((commandValue & 0xFF00) >> 8);
                    if (ChannelMux == 0)
                    {
                        return "No Channel active selected in adc mux"; // no channels are active
                    }
 
                    // Loop to count how many channels are active in the command register 
                    while (mask != 0)
                    {
                        if ((ChannelMux & mask) == mask)
                        {
                            count++;
                        }
                        mask = (byte)(mask >> 1);
                    }

                    GetRegisterValue(AD7291_REG_VOLTAGE,
                                     voltageData,
                                     count);

                    for (channel = 0; channel < count; channel++)
                    {
                        channelAddress = (byte)((voltageData[channel] & 0xF000) >> 12);
                        data[channelAddress] = (ushort)((voltageData[channel] & 0x0FFF));                     
                    }

                    return "ok";
                }
                catch (Exception err)
                {
                    count = 0;
                    return err.Message;
                }
            } 
        }
    }
}
