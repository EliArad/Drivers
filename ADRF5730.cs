using Dln;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareLib
{
    public class ADRF5730
    {
        // Parallel mode - PS pin must be 0

        DiolanIO[] m_Dout = new DiolanIO[6];
        DiolanIO m_LE;

        public ADRF5730(Device device)
        {
            m_LE = new DiolanIO(device, 8);
            m_Dout[0] = new DiolanIO(device, 0);
            m_Dout[1] = new DiolanIO(device, 1);
            m_Dout[2] = new DiolanIO(device, 11);
            m_Dout[3] = new DiolanIO(device, 12);
            m_Dout[4] = new DiolanIO(device, 13);

            for (int i = 0; i < m_Dout.Length; i++)
            {
                m_Dout[i].Output();
                m_Dout[i].Enable(true);
            }
            
            m_LE.Output();
            m_LE.Enable(true);

            SetAtten(0);

        }
        
        // The atten value are in step of 0.5  maximum of 1 << 6 / 2 = 32dB
        public void SetAtten(float attenValue)
        {
            try
            {
                m_LE.Value = false;
                attenValue = attenValue * 2;
                int _attenValue = (int)(Math.Round(attenValue)) & 0x3F;

                // D0 is LSB
                for (int i = 0; i < 6; i++)
                {
                    byte b = (byte)(_attenValue & 0x1);
                    m_Dout[i].SetValue(b);
                    _attenValue = _attenValue >> 1;
                }

                m_LE.Value = true; // it is nano sec
                m_LE.Value = false;
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }        
    }
}
