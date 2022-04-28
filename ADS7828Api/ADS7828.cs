﻿using BaseApi;
using BitField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ADS7828Api
{
    public class ADS7828
    {


        [BitFieldNumberOfBitsAttribute(8)]
        public struct CommandRegister : IBitField
        {
            
           
            [BitFieldInfo(7, 1)]
            public byte SD { get; set; }


            [BitFieldInfo(6, 1)]
            public byte C2{ get; set; }

            [BitFieldInfo(5, 1)]
            public byte C1 { get; set; }                        

            [BitFieldInfo(4, 1)]
            public byte C0 { get; set; }            

            [BitFieldInfo(2, 2)]
            public byte PD { get; set; }            

            [BitFieldInfo(1, 1)]
            public byte unused2 { get; set; }

            [BitFieldInfo(0, 1)]
            public bool unused1 { get; set; }
        }

        public enum CHANNEL
        {
            CH0, CH1, CH2, CH3, CH4, CH5, CH6, CH7
        };
         
        CommandRegister m_cmdReg = new CommandRegister();

        public enum PowerDownSelection
        {
            PowerDownBetweenA_DConverterConversions = 0,
            InternalReferenceOFFAndADConverterOn= 1,
            InternalReferenceONAndADConverterOFF = 2,
            InternalReferenceONSndADConverterON = 3
        };

        BaseApi.I2CBase m_i2c;
        
          
        byte m_slaveAddress;
        public ADS7828( I2CBase i2c, byte slaveAddress)
        {
            m_i2c = i2c;
            m_slaveAddress = slaveAddress;
            m_slaveAddress = (byte)(m_slaveAddress << 1);
            SetVREF(2.5f);
        }

        public void ConfigurePowerDown(PowerDownSelection pd)
        {
            m_cmdReg.PD = (byte)pd;
        }
          
        public void SelectDiffrentialChannel(CHANNEL chPlus , CHANNEL chMinus)
        {
            m_cmdReg.SD = 0;

            if (chPlus == CHANNEL.CH0 && chMinus == CHANNEL.CH1)
            {
                m_cmdReg.C0 = 0;
                m_cmdReg.C1 = 0;
                m_cmdReg.C2 = 0;
                return;
            }
            if (chPlus == CHANNEL.CH2 && chMinus == CHANNEL.CH3)
            {
                m_cmdReg.C0 = 1;
                m_cmdReg.C1 = 0;
                m_cmdReg.C2 = 0;
                return;
            }
            
            if (chPlus == CHANNEL.CH4 && chMinus == CHANNEL.CH5)
            {
                m_cmdReg.C0 = 0;
                m_cmdReg.C1 = 1;
                m_cmdReg.C2 = 0;
                return;
            }

            // Complete here
            //http://www.ti.com/lit/ds/symlink/ads7828.pdf

            m_i2c.Write(m_slaveAddress, m_cmdReg.ToUInt8());

        }

        public void SelectSingleEnddedChannel(CHANNEL ch)
        {
            m_cmdReg.SD = 1;

            if (ch == CHANNEL.CH0)
            {
                m_cmdReg.C0 = 0;
                m_cmdReg.C1 = 0;
                m_cmdReg.C2 = 0;
            } else 
            if (ch == CHANNEL.CH1)
            {
                m_cmdReg.C0 = 0;
                m_cmdReg.C1 = 0;
                m_cmdReg.C2 = 1;
            }
            else
            if (ch == CHANNEL.CH2)
            {
                m_cmdReg.C0 = 1;
                m_cmdReg.C1 = 0;
                m_cmdReg.C2 = 0;
            }
            else
            if (ch == CHANNEL.CH3)
            {
                m_cmdReg.C0 = 1;
                m_cmdReg.C1 = 0;
                m_cmdReg.C2 = 1;
            }
            else
            if (ch == CHANNEL.CH4)
            {
                m_cmdReg.C0 = 0;
                m_cmdReg.C1 = 1;
                m_cmdReg.C2 = 0;
            }
            else
            if (ch == CHANNEL.CH5)
            {
                m_cmdReg.C0 = 0;
                m_cmdReg.C1 = 1;
                m_cmdReg.C2 = 1;
            }
            else
            if (ch == CHANNEL.CH6)
            {
                m_cmdReg.C0 = 1;
                m_cmdReg.C1 = 1;
                m_cmdReg.C2 = 0;
            }
            else
            if (ch == CHANNEL.CH7)
            {
                m_cmdReg.C0 = 1;
                m_cmdReg.C1 = 1;
                m_cmdReg.C2 = 1;

            }
            byte d = m_cmdReg.ToUInt8();
            m_i2c.Write(m_slaveAddress,d);
            Thread.Sleep(10);
        }
        float LSB_Volatage;
        public void SetVREF(float VREF)
        {
            LSB_Volatage = VREF / 4096;
        }

        public float ReadChannel()
        {
            byte[] data = { 0, 0 };
            m_i2c.Read(m_slaveAddress, data);
            byte channelAddress = (byte)((data[0] & 0xF0) >> 4);
            byte low = (byte)((data[0] & 0xF));
            ushort channelData = (ushort)((data[1] & 0x0FF)  << 4| low);
            return channelData * LSB_Volatage;
        }

        public ushort ReadChannel16()
        {
            byte[] data = { 0, 0 };
            m_i2c.Read(m_slaveAddress, data);
            ushort channelData = (ushort)((data[0] << 8) | data[1]);
            channelData = (ushort)((channelData & 0xFFFC) >> 2);
            return channelData;// * LSB_Volatage;
        }
    }
}
