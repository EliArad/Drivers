using HardwareLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareApi   
{
    public class PCA9505_06
    {
        DiolanI2CController m_i2c;
        byte m_slaveAddress;

        class IOPORTS
        {
            public byte[] port;
            public IOPORTS()
            {
                port = new byte[5];
            }
        };

        public enum POLARITY
        {
            NO,
            INVERT
        }

        public enum BIT
        {
            CLEAR,
            SET
        }

        public enum DIRECTION
        {
            OUTPUT = 0,
            INPUT = 0
        };

        const int PCA9505_AI_ON = 0x80;
        const int PCA9505_AI_OFF = 0x00;
        // Addressing
        const int PCA9505_BASE_ADDRESS = 0x20;
        const int PCA9505_A0 = 0x01;
        const int PCA9505_A1 = 0x02;
        const int PCA9505_A2 = 0x04;

        const int PCA9505_INPUTP_BASE = 0x00;
        const int PCA9505_INPUTP0 = 0x00;
        const int PCA9505_INPUTP1 = 0x01;
        const int PCA9505_INPUTP2 = 0x02;
        const int PCA9505_INPUTP3 = 0x03;
        const int PCA9505_INPUTP4 = 0x04;

        const int PCA9505_OUTPUT_BASE = 0x08;
        const int PCA9505_OUTPUTP0 = 0x08;
        const int PCA9505_OUTPUTP1 = 0x09;
        const int PCA9505_OUTPUTP2 = 0x0A;
        const int PCA9505_OUTPUTP3 = 0x0B;
        const int PCA9505_OUTPUTP4 = 0x0C;

        const int PCA9505_POLARITY_BASE = 0x10;
        const int PCA9505_POLARITY0 = 0x10;
        const int PCA9505_POLARITY1 = 0x11;
        const int PCA9505_POLARITY2 = 0x12;
        const int PCA9505_POLARITY3 = 0x13;
        const int PCA9505_POLARITY4 = 0x14;

        const int PCA9505_IO_CONFUGURATION_BASE = 0x18;
        const int PCA9505_IO_CONFUGURATION_C0 = 0x18;
        const int PCA9505_IO_CONFUGURATION_C1 = 0x19;
        const int PCA9505_IO_CONFUGURATION_C2 = 0x1A;
        const int PCA9505_IO_CONFUGURATION_C3 = 0x1B;
        const int PCA9505_IO_CONFUGURATION_C4 = 0x1C;

        const int PCA9505_MASK_INTERRUPT_BASE = 0x20;
        const int PCA9505_MASK_INTERRUPT0 = 0x20;
        const int PCA9505_MASK_INTERRUPT1 = 0x21;
        const int PCA9505_MASK_INTERRUPT2 = 0x22;
        const int PCA9505_MASK_INTERRUPT3 = 0x23;
        const int PCA9505_MASK_INTERRUPT4 = 0x24;

        public PCA9505_06 (DiolanI2CController i2c, byte SlaveAddress)
        {
            m_i2c = i2c;
            m_slaveAddress = SlaveAddress;
        }

        public PCA9505_06()
        {

        }

        byte bitRead(byte value, byte bit)
        {
            return (byte)((value >> bit) & 0x01);
        }
        void bitSet(ref byte value, byte bit)
        {
             value |= (byte)(1L << bit);
        }
        void bitClear(ref byte value, byte bit) 
        {
            value &= (byte)(~(1L << bit));
        }
        
        void bitWrite(ref byte value, byte bit, BIT b) 
        {
            if (b == BIT.SET)
                bitSet(ref value, bit);
            else
                bitClear(ref value, bit);
        }

        void bitWrite(ref byte value, byte bit, byte b)
        {
            if (b == 1)
                bitSet(ref value, bit);
            else
                bitClear(ref value, bit);
        }

        byte getPort(byte pinNumber) 
        {
            return (byte)(pinNumber / 8);
        }

        byte getBit(byte pin)        
        { 
	        return (byte)(pin % 8);
        }

        byte retrievePortData(byte reg, byte port) 
        {
            try
            {
                if (port > 5)
                {
                    throw (new SystemException("Invalid port number for PCA9505"));
                }
                byte data = (byte)((reg + port) | PCA9505_AI_OFF);

                m_i2c.Write(m_slaveAddress, data);
                data = m_i2c.Read(m_slaveAddress);
                return data;
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }
         

        public virtual DIRECTION GetPinDirection(byte pin) 
        {
            byte res = (byte)(retrievePortData(PCA9505_IO_CONFUGURATION_BASE, getPort(pin)) & (1 << getBit(pin)));
            DIRECTION dir = (DIRECTION)(res);
	        return  dir;
        }       

        public virtual void SetAllDirectionOutput()
        {
            for (byte i = 0; i < 5; i++)
            {
                sendPortData(PCA9505_IO_CONFUGURATION_BASE, i, 0);
            }
        }
        public virtual void SetAllPortsHigh()
        {
            for (byte i = 0; i < 5; i++)
            {
                sendPortData(PCA9505_OUTPUT_BASE, i, 0xFF);
            }
        }

        public virtual void ClearAllPorts()
        {
            for (byte i = 0; i < 5; i++)
            {
                sendPortData(PCA9505_OUTPUT_BASE, i, 0);
            }
        }

        // Set the direction of the PIN  from 0 to 39.
        public virtual void SetPinDirection(byte pin, DIRECTION dir) 
        {
            try
            {
                byte _dir = (byte)dir;
                byte value = retrievePortData(PCA9505_IO_CONFUGURATION_BASE, getPort(pin));
                bitWrite(ref value, getBit(pin), dir == DIRECTION.INPUT ? BIT.SET : BIT.CLEAR);
                sendPortData(PCA9505_IO_CONFUGURATION_BASE, getPort(pin), value);
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }

        }

        public virtual void SetPin(byte pin) 
        {
            lock (this)
            {
                try
                {
                    byte portValue = retrievePortData(PCA9505_OUTPUT_BASE, getPort(pin));
                    bitWrite(ref portValue, getBit(pin), BIT.SET);
                    sendPortData(PCA9505_OUTPUT_BASE, getPort(pin), portValue);
                }
                catch (Exception err)
                {
                    throw (new SystemException(err.Message));
                }
            }
        }

        public virtual void SetPin(byte port, byte pin)
        {
            lock (this)
            {
                try
                {
                    pin = (byte)(8 * port + pin);
                    byte portValue = retrievePortData(PCA9505_OUTPUT_BASE, getPort(pin));
                    bitWrite(ref portValue, getBit(pin), BIT.SET);
                    sendPortData(PCA9505_OUTPUT_BASE, getPort(pin), portValue);
                }
                catch (Exception err)
                {
                    throw (new SystemException(err.Message));
                }
            }
        }

        public virtual void ClearPin(byte port, byte pin)
        {
            lock (this)
            {
                try
                {
                    pin = (byte)(8 * port + pin);
                    byte portValue = retrievePortData(PCA9505_OUTPUT_BASE, getPort(pin));
                    bitClear(ref portValue, getBit(pin));
                    sendPortData(PCA9505_OUTPUT_BASE, getPort(pin), portValue);
                }
                catch (Exception err)
                {
                    throw (new SystemException(err.Message));
                }
            }
        }

        public virtual void ClearPin(byte pin)
        {
            lock (this)
            {
                try
                {
                    byte portValue = retrievePortData(PCA9505_OUTPUT_BASE, getPort(pin));
                    bitClear(ref portValue, getBit(pin));
                    sendPortData(PCA9505_OUTPUT_BASE, getPort(pin), portValue);
                }
                catch (Exception err)
                {
                    throw (new SystemException(err.Message));
                }
            }
        }

        public virtual byte[] ReadAllOutputPortsValues()
        {
            try
            {
                byte[] gpios = new byte[40];

                byte[] data = new byte[5];
                for (byte i = 0; i < 5; i++)
                {
                    data[i] = (byte)(retrievePortData(PCA9505_INPUTP_BASE, i));
                }
                int n = 0;
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        gpios[n++] = (byte)(data[i] >> j & 01);
                    }
                return gpios;
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

        public virtual BIT GetPin(byte pin) 
        {
            try
            {
                byte value = retrievePortData(PCA9505_INPUTP_BASE, getPort(pin));
                byte b = (byte)(value & (1 << getBit(pin)));
                return b > 0 ? BIT.SET : BIT.CLEAR;
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

        public virtual byte GetPortData(byte port)
        {
            try
            {
                if (port > 5)
                {
                    throw (new SystemException("Port is out of range"));
                }

                byte value = retrievePortData(PCA9505_INPUTP_BASE, port);
                return value;
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }
        public void ReadDeviceId(out uint deviceId)
        {

            byte[] data = { 0, 0, 0 };
            byte[] addr = { 0xF8, 0x21 };
            m_i2c.Write(m_slaveAddress, addr);
            m_i2c.Read(data);
            deviceId = (uint)(data[2] << 16 | data[1] << 8 | data[0]);
                
        }

        public virtual byte[] GetPortsData()
        {
            try
            {
                byte[] value = new byte[5];
                for (byte i = 0; i < 5; i++)
                {
                    value[i] = retrievePortData(PCA9505_INPUTP_BASE, i);
                }
                return value;
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

        void sendPortData(byte reg, byte port, byte data) 
        {
            try
            {
                byte[] dataToWrite = { (byte)((reg + port) | PCA9505_AI_OFF), data };
                m_i2c.Write(m_slaveAddress, dataToWrite);
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

        public virtual void setPinPolarity(byte pin, POLARITY polarity) 
        {
            try
            {
                byte set = retrievePortData(PCA9505_POLARITY_BASE, getPort(pin));
                bitWrite(ref set, getBit(pin), (byte)(polarity));
                sendPortData(PCA9505_POLARITY_BASE, getPort(pin), set);
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

        public virtual byte getPinPolarity(byte pin)
        {
            try
            {
                return (byte)((retrievePortData(PCA9505_POLARITY_BASE, getPort(pin)) >> getBit(pin)) & 0x01);
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

        public virtual void setPortPolarity(byte port, byte toggle)
        {
            try
            {
                sendPortData(PCA9505_POLARITY_BASE, port, toggle);
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

        public virtual byte getPortPolarity(byte port)
        {
            try
            {
                return retrievePortData(PCA9505_POLARITY_BASE, port);
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

         
    }
}
