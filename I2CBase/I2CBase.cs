using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApi
{
    public abstract class I2CBase
    {

        public abstract bool Init(out string outMessage);
        public abstract void Write(byte[] data);
        public abstract void Write(byte slaveAddress, byte[] data);
        public abstract void Write(byte data);
        public abstract void Write(byte slaveAddress, byte data);
        public abstract void Write(int memoryAddressLength, int memoryAddress, byte[] data);
        public abstract void Write(int memoryAddressLength, int memoryAddress, byte[] data, int dataSize);
        public abstract void Write(int memoryAddressLength, int memoryAddress, byte[] data, int dataOffset, int dataSize);
        public abstract byte Read();
        public abstract byte Read(byte slaveAddress);
        public abstract void Read(byte[] data);
        public abstract void Read(byte slaveAddress, byte[] data);
        public abstract void Read(byte[] data, int index, int size);
        public abstract void Read(byte slaveAddress, byte[] data, int index, int size);
        public abstract void Close();

        protected uint m_speed = 100000;
        public void SetSpeed400()
        {
            m_speed = 400000;
        }

        public void SetSpeed100()
        {
            m_speed = 100000;
        }

    }
}
