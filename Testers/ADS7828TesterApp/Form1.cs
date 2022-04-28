using ADS7828Api;
using BaseApi;
using LTC2637Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADS7828TesterApp
{
    public partial class Form1 : Form
    {
        LTC2637[] m_ltc2637 = { null, null };
        ADS7828[] m_ads7828 = new ADS7828[4];
        I2CBase m_i2c;
        bool m_connect = false;
        Thread m_thread;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            cbDac1.DataSource = Enum.GetNames(typeof(LTC2637.DAC));
            cbDac2.DataSource = Enum.GetNames(typeof(LTC2637.DAC));


        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
            if (m_connect == false)
            {
                // need the slave address
                m_i2c = new I2CMCP2221();
                if (m_i2c.Init(out string outMessage) == false)
                {
                    MessageBox.Show(outMessage);
                    return;
                }
                m_ads7828[0] = new ADS7828(m_i2c, 0x4A);
                m_ads7828[1] = new ADS7828(m_i2c, 0x4B);
                m_ads7828[2] = new ADS7828(m_i2c, 0x48);
                m_ads7828[3] = new ADS7828(m_i2c, 0x49);

                m_ads7828[0].ConfigurePowerDown(ADS7828.PowerDownSelection.InternalReferenceOFFAndADConverterOn);
                m_ads7828[1].ConfigurePowerDown(ADS7828.PowerDownSelection.InternalReferenceOFFAndADConverterOn);
                m_ads7828[2].ConfigurePowerDown(ADS7828.PowerDownSelection.InternalReferenceOFFAndADConverterOn);
                m_ads7828[3].ConfigurePowerDown(ADS7828.PowerDownSelection.InternalReferenceOFFAndADConverterOn);

                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                groupBox5.Enabled = true;


                m_connect = true;
                m_thread = new Thread(ReadData);
                m_thread.Start();
            }
        }
        void ReadData()
        {
            TextBox[] txtAdc0 = { txtAdc0Ch0, txtAdc0Ch1, txtAdc0Ch2, txtAdc0Ch3, txtAdc0Ch4, txtAdc0Ch5, txtAdc0Ch6, txtAdc0Ch7 };
            TextBox[] txtAdc1 = { txtAdc1Ch0, txtAdc1Ch1, txtAdc1Ch2};
            TextBox[] txtAdc2 = { txtAdc2Ch0, txtAdc2Ch1 , txtAdc2Ch3, txtAdc2Ch4 , txtAdc2Ch5 , txtAdc2Ch6 , txtAdc2Ch7 , txtAdc2Ch8 };
            TextBox[] txtAdc3 = { txtAdc3Ch0, txtAdc3Ch1, txtAdc3Ch2, txtAdc3Ch3, txtAdc3Ch4, txtAdc3Ch5, txtAdc3Ch6, txtAdc3Ch7 };

            while (m_connect)
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        m_ads7828[0].SelectSingleEnddedChannel((ADS7828.CHANNEL)i);
                        float data = m_ads7828[0].ReadChannel();
                        txtAdc0[i].Text = data.ToString("0.0000");
                    }
                }

                if (checkBox2.Checked)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        m_ads7828[1].SelectSingleEnddedChannel((ADS7828.CHANNEL)i);
                        float data = m_ads7828[1].ReadChannel();
                        txtAdc1[i].Text = data.ToString("0.0000");
                    }
                }

                if (checkBox3.Checked)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        m_ads7828[2].SelectSingleEnddedChannel((ADS7828.CHANNEL)i);
                        //float data = m_ads7828[2].ReadChannel();
                        ushort data = m_ads7828[2].ReadChannel16();
                        txtAdc2[i].Text = data.ToString("X");
                    }
                }

                if (checkBox4.Checked)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        m_ads7828[3].SelectSingleEnddedChannel((ADS7828.CHANNEL)i);
                        float data = m_ads7828[3].ReadChannel();
                        txtAdc3[i].Text = data.ToString("0.0000");
                    }
                }

                Thread.Sleep(100);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_connect = false;
            Thread.Sleep(100);

            if (m_thread != null)
                m_thread.Join();

        }
        bool[] m_connectLtc = { false, false };
        private void button3_Click(object sender, EventArgs e)
        {
            if (m_connectLtc[1] == false)
            {
                m_ltc2637[1] = new LTC2637(m_i2c, 0x24);
                m_connectLtc[1] = true;


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
