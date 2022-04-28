using MCP2221;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApi
{
    public class I2CMCP2221 : I2CBase
    {
        MchpUsbI2c UsbI2c = new MchpUsbI2c();
        byte m_slaveAddress;
       
        public I2CMCP2221(byte sla)
        {
            m_slaveAddress = sla;            
        }

        public I2CMCP2221()
        {        
            
        }

        public override bool Init(out string outMessage)
        {

            try
            {
                bool isConnected = UsbI2c.Settings.GetConnectionStatus();

                //Print the result to the console window
                if (isConnected == false)
                {
                    outMessage = "No I2C Device found";
                    return false;
                }

                // Get total number of devices plugged into PC
                int devCount = UsbI2c.Management.GetDevCount();
                UsbI2c.Management.SelectDev(0);

                string usbDescriptor = UsbI2c.Settings.GetUsbStringDescriptor();
                outMessage = "The USB descriptor string is: " + usbDescriptor + "\n";
                return true;
            }
            catch (Exception err)
            {
                outMessage = err.Message;
                return false;
            }
        }

        public override void Close()
        {
            UsbI2c.Dispose();
        }

        public override byte Read()
        {
            throw new NotImplementedException();
        }

        public override byte Read(byte slaveAddress)
        {
            throw new NotImplementedException();
        }

        public override void Read(byte[] data)
        {
            lock (this)
            {
                UsbI2c.Functions.ReadI2cData(m_slaveAddress, data, (uint)data.Length, m_speed);
            }
        }

        public override void Read(byte slaveAddress, byte[] data)
        {
            lock (this)
            {
                UsbI2c.Functions.ReadI2cData(slaveAddress, data, (uint)data.Length, m_speed);
            }
        }

        public override void Read(byte[] data, int index, int size)
        {
            throw new NotImplementedException();
        }

        public override void Read(byte slaveAddress, byte[] data, int index, int size)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] data)
        {
            lock (this)
            {
                UsbI2c.Functions.WriteI2cData(m_slaveAddress, data, (uint)data.Length, m_speed);
            }
        }

        public override void Write(byte slaveAddress, byte[] data)
        {
            lock (this)
            {
                UsbI2c.Functions.WriteI2cData(slaveAddress, data, (uint)data.Length, m_speed);
            }
        }

        public override void Write(byte data)
        {
            lock (this)
            {
                byte[] d = { data };
                UsbI2c.Functions.WriteI2cData(m_slaveAddress, d, 1, m_speed);
            }
        }

        public override void Write(byte slaveAddress, byte data)
        {
            lock (this)
            {
                byte[] d = { data };
                UsbI2c.Functions.WriteI2cData(slaveAddress, d, 1, m_speed);
            }
        }

        public override void Write(int memoryAddressLength, int memoryAddress, byte[] data)
        {
            throw new NotImplementedException();
        }

        public override void Write(int memoryAddressLength, int memoryAddress, byte[] data, int dataSize)
        {
            throw new NotImplementedException();
        }

        public override void Write(int memoryAddressLength, int memoryAddress,
                                   byte[] data, 
                                   int dataOffset, int dataSize)
        {
            throw new NotImplementedException();
        }
    }
}
