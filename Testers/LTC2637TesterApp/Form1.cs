using BaseApi;
using LTC2637Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTC2637TesterApp
{
    public partial class Form1 : Form
    {
        LTC2637 [] m_ltc2637 = { null, null };
        public Form1()
        {
            InitializeComponent();
            cbDac1.DataSource = Enum.GetNames(typeof(LTC2637.DAC));
            cbDac2.DataSource = Enum.GetNames(typeof(LTC2637.DAC));

            m_i2c = new I2CMCP2221();
            if (m_i2c.Init(out string outMessage) == false)
            {
                MessageBox.Show(outMessage);
                return;
            }
        }

        LTC2637.DAC GetDacFromCombo(ComboBox cmb)
        {
            switch (cmb.SelectedIndex)
            {
                case 0:
                    return LTC2637.DAC.DAC_A;
                case 1:
                    return LTC2637.DAC.DAC_B;
                case 2:
                    return LTC2637.DAC.DAC_C;
                case 3:
                    return LTC2637.DAC.DAC_D;
                case 4:
                    return LTC2637.DAC.DAC_E;
                case 5:
                    return LTC2637.DAC.DAC_F;
                case 6:
                    return LTC2637.DAC.DAC_G;
                case 7:
                    return LTC2637.DAC.DAC_H;
                default:
                    return LTC2637.DAC.All_DACS;

            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (m_ltc2637[0] == null)
                return;
            try
            { 
                int value = Convert.ToInt32(tbMSData1.Text, 16);
                LTC2637.DAC dac;
                LTC2637.Command command;

                dac = dac = GetDacFromCombo(cbDac1);
                command = LTC2637.Command.WriteToAndUpdatePowerUpDACRegisterN;
                m_ltc2637[0].Write(command, dac, (ushort)value);
            }   
            catch (Exception err)
            {
                MessageBox.Show("MS/LS Data should be integer value");
            }
            
        }

        I2CBase m_i2c;
        bool [] m_connect = { false, false };
        private void button1_Click(object sender, EventArgs e)
        {
            if (m_connect[0] == false)
            {               
                m_ltc2637[0] = new LTC2637(m_i2c, 0x20); 
                m_connect[0] = true;

                LTC2637.DAC dac;
                LTC2637.Command command;

                dac = LTC2637.DAC.All_DACS;
                command = LTC2637.Command.SelectExternalReference;
                m_ltc2637[0].Write(command, dac);

            }
            else
            {
                LTC2637.DAC dac;
                LTC2637.Command command;

                dac = LTC2637.DAC.All_DACS;
                command = LTC2637.Command.SelectExternalReference;
                m_ltc2637[0].Write(command, dac);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (m_connect[1] == false)
            {
                m_ltc2637[1] = new LTC2637(m_i2c, 0x24);
                m_connect[1] = true;
                 

                LTC2637.DAC dac;  
                LTC2637.Command command;

                dac = LTC2637.DAC.All_DACS;
                command = LTC2637.Command.SelectExternalReference;
                m_ltc2637[1].Write(command, dac);

            }
            else
            {
                LTC2637.DAC dac;
                LTC2637.Command command;

                dac = LTC2637.DAC.All_DACS;
                command = LTC2637.Command.SelectExternalReference;
                m_ltc2637[1].Write(command, dac);


            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            if (m_ltc2637[1] == null)
                return;
            int value = Convert.ToInt32(txtValue2.Text, 16);
            try
            {
                LTC2637.DAC dac;
                LTC2637.Command command;

                dac = GetDacFromCombo(cbDac2);
                command = LTC2637.Command.WriteToAndUpdatePowerUpDACRegisterN;
                m_ltc2637[1].Write(command, dac, (ushort)value);
            }
            catch (Exception err)
            { 
                MessageBox.Show("MS/LS Data should be integer value");
            }
        }
    }
}
