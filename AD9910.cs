using BitField;
using Dln;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareLib
{
 
    namespace AD9910Regs
    {


        [BitFieldNumberOfBitsAttribute(32)]
        public struct ExampleBitField : IBitField
        {
            
            [BitFieldInfo(0, 1)]
            public bool Bit1 { get; set; }
            [BitFieldInfo(1, 1)]
            public byte Bit2 { get; set; }
            [BitFieldInfo(2, 2)]
            public byte TwoBits { get; set; }
            [BitFieldInfo(4, 4)]
            public byte FourBits { get; set; }

            [BitFieldInfo(8, 4)]
            public byte FourBits1 { get; set; }
            [BitFieldInfo(12, 4)]
            public byte FourBits2 { get; set; }

            [BitFieldInfo(16, 4)]
            public byte FourBits3 { get; set; }
            
        }


        //Register 0x00 
        [BitFieldNumberOfBitsAttribute(32)]
        public struct CFR1_ControlFunction : IBitField
        {

            [BitFieldInfo(0, 1)]
            public byte LSBfirst { get; set; }

            [BitFieldInfo(1, 1)]
            public byte SDIOInputOnly { get; set; }


            [BitFieldInfo(2, 1)]
            public byte Open { get; set; }


            [BitFieldInfo(3, 1)]
            public byte ExternalPowerDownControl { get; set; }


            [BitFieldInfo(4, 1)]
            public byte AuxDACPowerDown { get; set; }


            [BitFieldInfo(5, 1)]
            public byte REFCLKIinputPowerdown { get; set; }


            [BitFieldInfo(6, 1)]
            public byte DACPowerdown { get; set; }

            [BitFieldInfo(7, 1)]
            public byte DigitalPowerdown { get; set; }

            [BitFieldInfo(8, 1)]
            public byte SelectAutoOSK { get; set; }

            [BitFieldInfo(9, 1)]
            public byte OSKEnable { get; set; }

            [BitFieldInfo(10, 1)]
            public byte LoadARRIOUpdate { get; set; }

            [BitFieldInfo(11, 1)]
            public byte ClearPhaseAccumulator { get; set; }

            [BitFieldInfo(12, 1)]
            public byte ClearDigitalRampAccumulator { get; set; }

            [BitFieldInfo(13, 1)]
            public byte AutoclearPhaseAccumulator { get; set; }


            [BitFieldInfo(14, 1)]
            public byte AutoClearDigitalRampAccumulator { get; set; }

            [BitFieldInfo(15, 1)]
            public byte LoadLRRIOUpdate { get; set; }

            [BitFieldInfo(16, 1)]
            public byte SelectDDSSineOutput { get; set; }

            [BitFieldInfo(17, 4)]
            public byte InternalProfileControl { get; set; }

            [BitFieldInfo(21, 1)]
            public byte Open2 { get; set; }

            [BitFieldInfo(22, 1)]
            public byte InverseSincFilterEnable { get; set; }

            [BitFieldInfo(23, 1)]
            public byte ManualOSKExternalControl { get; set; }

            [BitFieldInfo(24, 5)]
            public byte Open3 { get; set; }

            [BitFieldInfo(29, 2)]
            public byte RAMPlaybackDestination { get; set; }

            [BitFieldInfo(31, 1)]
            public byte RAMEnable { get; set; }

        }
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        // Register 0x1 ///////////////////////////////////////
        [BitFieldNumberOfBitsAttribute(32)]
        public struct CFR2_ControlFunction : IBitField
        {
            [BitFieldInfo(0, 4)]
            public uint FMGain { get; set; }

            [BitFieldInfo(4, 1)]
            public uint ParalleldataPortEnable { get; set; }

            [BitFieldInfo(5, 1)]
            public uint SyncTimingValidationDisable { get; set; }

            [BitFieldInfo(6, 1)]
            public uint DataAssemblerHoldLastValue { get; set; }

            [BitFieldInfo(7, 1)]
            public uint MatchedLatencyEnable { get; set; }

            [BitFieldInfo(8, 1)]
            public uint Open1 { get; set; }

            [BitFieldInfo(9, 1)]
            public uint TxEnableInvert { get; set; }

            [BitFieldInfo(10, 1)]
            public uint PDCLKInvert { get; set; }

            [BitFieldInfo(11, 1)]
            public uint PDCLKEnable { get; set; }

            [BitFieldInfo(12, 2)]
            public uint Open2 { get; set; }

            [BitFieldInfo(14, 2)]
            public uint IOUpdatRateControl { get; set; }

            [BitFieldInfo(16, 1)]
            public uint ReadEffectiveFTW { get; set; }

            [BitFieldInfo(17, 1)]
            public uint DigitalRampNoDwellLow { get; set; }

            [BitFieldInfo(18, 1)]
            public uint DigitalRampNoDwellHigh { get; set; }

            [BitFieldInfo(19, 1)]
            public uint DigitalRampEnable { get; set; }

            [BitFieldInfo(20, 2)]
            public uint DigitalRampDestination { get; set; }

            [BitFieldInfo(22, 1)]
            public uint SYNC_CLKEnable { get; set; }

            [BitFieldInfo(23, 1)]
            public uint InternalIOUpdateActive { get; set; }

            [BitFieldInfo(24, 1)]
            public uint EnableAmplitudeScaleFromSingleToneProfiles { get; set; }

            [BitFieldInfo(25, 7)]
            public uint Open3 { get; set; }

        }

        // Register 0x3 ///////////////////////////////////////
        [BitFieldNumberOfBitsAttribute(32)]
        public struct CFR3_ControlFunction : IBitField
        {
            [BitFieldInfo(0, 1)]
            public uint Open { get; set; }

            [BitFieldInfo(1, 1)]
            public uint N { get; set; }

            [BitFieldInfo(8, 1)]
            public uint PllEnable { get; set; }

            [BitFieldInfo(9, 1)]
            public uint Open1 { get; set; }

            [BitFieldInfo(10, 1)]
            public uint PFDReset { get; set; }

            [BitFieldInfo(11, 3)]
            public uint Open2 { get; set; }

            [BitFieldInfo(14, 1)]
            public uint REFCLKInputDividerResetB { get; set; }

            [BitFieldInfo(15, 1)]
            public uint REFCLKInputDividerBypass { get; set; }

            [BitFieldInfo(16, 3)]
            public uint Open3 { get; set; }

            [BitFieldInfo(19, 3)]
            public uint LCP { get; set; }

            [BitFieldInfo(22, 2)]
            public uint Open4 { get; set; }

            [BitFieldInfo(24, 3)]
            public uint VCOSEL { get; set; }

            [BitFieldInfo(27, 1)]
            public uint Open5 { get; set; }

            [BitFieldInfo(28, 1)]
            public uint DRV0 { get; set; }

            [BitFieldInfo(30, 2)]
            public uint Open6 { get; set; }
        }


       //FTW—Frequency Tuning Word (0x07)
        [BitFieldNumberOfBitsAttribute(32)]
        public struct FTW_FrequencyTuningWord : IBitField
        {
             [BitFieldInfo(0, 32)]
            public uint FrequencyTuningWord { get; set; }
        }


        [BitFieldNumberOfBitsAttribute(32)]
        public struct DigitalRampStepSize : IBitField
        {


            [BitFieldInfo(0, 32)]
            public uint DigitalRampIncrementStepSize { get; set; }

            [BitFieldInfo(32, 64)]
            public uint DigitalRampDecrementStepSize { get; set; }

        }

        // Register 0xD ///////////////////////////////////////
        [BitFieldNumberOfBitsAttribute(32)]
        public struct DigitalRampRate : IBitField
        {
            [BitFieldInfo(0, 16)]
            public uint DigitalRampPositiveSlopeRate { get; set; }

            [BitFieldInfo(16, 32)]
            public uint DigitalRampNegativeSlopeRate { get; set; }
        }
         [BitFieldNumberOfBitsAttribute(32)]
        public struct DigitalRampLimit : IBitField
        {

            [BitFieldInfo(0, 32)]
            public uint DigitalRampLowerLimit { get; set; }

            [BitFieldInfo(32, 64)]
            public uint DigitalRampUpperLimit { get; set; }

        }


        [BitFieldNumberOfBitsAttribute(64)]
         public struct SingleToneProfile : IBitField
        {
            [BitFieldInfo(0, 32)]
            public uint FrequencyTuningWord { get; set; }

            [BitFieldInfo(32, 16)]
            public uint PhaseOffsetWord { get; set; }

            [BitFieldInfo(48, 14)]
            public uint AplitudeScale { get; set; }
        }
              
        public class AD9910 : DiolanSPIMaster
        {


            [Serializable]
            public class AD9910AllRegs
            {
                public CFR1_ControlFunction cfr1;
                public CFR2_ControlFunction cfr2;
                public CFR3_ControlFunction cfr3;
                public DigitalRampStepSize drss;
                public DigitalRampRate ddrr;
                public DigitalRampLimit ddl;
                public SingleToneProfile[] stp;
                public FTW_FrequencyTuningWord ftw;

                public AD9910AllRegs()
                {

                    stp = new SingleToneProfile[8];
                    cfr1 = new CFR1_ControlFunction();
                    cfr2 = new CFR2_ControlFunction();
                    cfr3 = new CFR3_ControlFunction();
                    drss = new DigitalRampStepSize();
                    ddrr = new DigitalRampRate();
                    ddl = new DigitalRampLimit();
                    ftw = new FTW_FrequencyTuningWord();
                }
            }
            AD9910AllRegs m_allRegs = new AD9910AllRegs();


            double Fsysclk = 1000; //MHZ
            uint[] m_regs = new uint[24];
            bool m_initialize = false;

            // The numbers here are diolan IO according to its datasheet 
            DiolanIO.IO[] DRCTL_IO_PIN = { DiolanIO.IO.D0, DiolanIO.IO.E6 };
            DiolanIO.IO[] DROVER_IO_PIN = { DiolanIO.IO.D1, DiolanIO.IO.E7 };
            //DiolanIO.IO[] DRHOLD_IO_PIN = { DiolanIO.IO.D1, DiolanIO.IO.E1 };
            DiolanIO.IO[] IOUPDATE_IO_PIN = { DiolanIO.IO.D3, DiolanIO.IO.F1 };
            DiolanIO.IO[] MASTER_RESET = { DiolanIO.IO.D2, DiolanIO.IO.F0 };

            DiolanIO.IO[] IO_RESET = { DiolanIO.IO.D4, DiolanIO.IO.F2 };

            //int [] EXT_POWER_DOWN = { 0, 0};
            DiolanIO.IO[] PROFILE_0 = { DiolanIO.IO.C3, DiolanIO.IO.C4 }; 
            
            public enum AD9910REGs
            {
                CFR1,
                CFR2,
                CFR3,
                AuxiliaryDACControlRegister,
                IOUpdateRateRegister,
                reserved1,
                reserved2,
                FTW_FrequencyTuningWordRegister,
                POW_PhaseOffsetWordRegister,
                ASF_AmplitudeScaleFactorRegister,
                MultichipSyncRegister,
                DigitalRampLimitRegister,  // 0xB  64 bit 
                DigitalRampStepSizeRegister,// 0xC  64 bit 
                DigitalRampRateRegister, // 0xD  32 bit 
                SingleToneProfile0, // 0xE  64 bit 
                SingleToneProfile1,
                SingleToneProfile2,
                SingleToneProfile3,
                SingleToneProfile4,
                SingleToneProfile5,
                SingleToneProfile6,
                SingleToneProfile7,
                ALL = 0xFFFF
            }


            enum REGsMask
            {
                CFR1 = 0x1,
                CFR2 = 0x2,
                CFR3 = 0x4,
                AuxiliaryDACControlRegister = 0x8,
                IOUpdateRateRegister = 0x10,
                reserved1 = 0x20,
                reserved2 = 0x40,
                FTW_FrequencyTuningWordRegister = 0x80,
                POW_PhaseOffsetWordRegister = 0x100,
                ASF_AmplitudeScaleFactorRegister = 0x200,
                MultichipSyncRegister = 0x400,
                DigitalRampLimitRegister = 0x800,
                DigitalRampStepSizeRegister = 0x1000,
                DigitalRampRateRegister = 0x2000,
                SingleToneProfile0 = 0x4000,
                SingleToneProfile1 = 0x8000,
                SingleToneProfile2 = 0x10000,
                SingleToneProfile3 = 0x20000,
                SingleToneProfile4 = 0x40000,
                SingleToneProfile5 = 0x80000,
                SingleToneProfile6 = 0x100000,
                SingleToneProfile7 = 0x200000,
                ALL = 0xFFFFFFF
            }

            public enum AD9910_PINs
            {
                DRCTL,
                DRHOLD,
                DROVER,
                IOUPDATE,
                MASTER_RESET,
                IO_RESET,
                //SPI_CS,
                PROFILE_0,
                //PROFILE_1,
                //PROFILE_2,
                LAST
            }

            bool m_externalClock1000Mega = false;

            DiolanIO[] m_dio = new DiolanIO[(int)AD9910_PINs.LAST];

            public int m_ioId = 0;
            SPI_CS m_cs = SPI_CS.SS0;
            public AD9910(Device device,
                          int ioId, SPI_CS cs, DiolanSPIMaster.SPI_CS_ENABLE reservedCS) : base(device, DiolanSPIMaster.SPI_MODE.SPI_MODE_3, cs, reservedCS)
            {
                m_ioId = ioId;
                m_cs = cs;

                m_dio[(int)AD9910_PINs.DRCTL] = new DiolanIO(device, DRCTL_IO_PIN[m_ioId]);
                //m_dio[(int)AD9910_PINs.DRHOLD] = new DiolanIO(device, DRHOLD_IO_PIN[m_ioId]);
                m_dio[(int)AD9910_PINs.DROVER] = new DiolanIO(device, DROVER_IO_PIN[m_ioId]);
                m_dio[(int)AD9910_PINs.IOUPDATE] = new DiolanIO(device, IOUPDATE_IO_PIN[m_ioId]);
                m_dio[(int)AD9910_PINs.MASTER_RESET] = new DiolanIO(device, MASTER_RESET[m_ioId]);
                m_dio[(int)AD9910_PINs.IO_RESET] = new DiolanIO(device, IO_RESET[m_ioId]);
                //m_dio[(int)AD9910_PINs.EXT_POWER_DOWN] = new DiolanIO(device, EXT_POWER_DOWN[m_ioId]);
                //m_dio[(int)AD9910_PINs.SPI_CS] = new DiolanIO(device, SPI_CS);

                m_dio[(int)AD9910_PINs.PROFILE_0] = new DiolanIO(device, PROFILE_0[m_ioId]);
                //m_dio[(int)AD9910_PINs.PROFILE_1] = new DiolanIO(device, PROFILE_1[m_ioId]);
                //m_dio[(int)AD9910_PINs.PROFILE_2] = new DiolanIO(device, PROFILE_2[m_ioId]);

                m_dio[(int)AD9910_PINs.DRCTL].Output();
                m_dio[(int)AD9910_PINs.IOUPDATE].Output();
                //m_dio[(int)AD9910_PINs.DRHOLD].Output();
                m_dio[(int)AD9910_PINs.MASTER_RESET].Output();
                m_dio[(int)AD9910_PINs.IO_RESET].Output();
                //m_dio[(int)AD9910_PINs.EXT_POWER_DOWN].Output();

                m_dio[(int)AD9910_PINs.PROFILE_0].Output();
                //m_dio[(int)AD9910_PINs.PROFILE_1].Output();
                //m_dio[(int)AD9910_PINs.PROFILE_2].Output();

                //m_dio[(int)AD9910_PINs.SPI_CS].Output();


                m_dio[(int)AD9910_PINs.DROVER].Input();
                Reset();
                PowerDown(false);
                SwitchTo3Wire();

                InitializeDefaultRegisters();


                uint data;
                ReadRegister(AD9910REGs.CFR1, out data);                
                if ((data & 0x2) != 2 || (data == 0xFFFFFFFF))
                {
                    if (data == 0xFFFFFFFF)
                    {
                        throw (new SystemException("AD9910 return 0xFFFFFFFF in read"));
                    }
                    throw (new SystemException("AD9910 shuold read back 2 for 3 wire mode"));
                }
              

            }
            private void PowerDown(bool down)
            {
                //m_dio[(int)AD9910_PINs.EXT_POWER_DOWN].Value = down;
            }
            private void Reset()
            {

                m_dio[(int)AD9910_PINs.IO_RESET].Value = false;
                Thread.Sleep(100);
                m_dio[(int)AD9910_PINs.MASTER_RESET].Value = true;
                Thread.Sleep(100);
                m_dio[(int)AD9910_PINs.MASTER_RESET].Value = false;
            }

            public AD9910AllRegs ReadBackAllRegisters(bool forceRead)
            {
                if (forceRead)
                    ReadBackAllRegisters(REGsMask.ALL);
                return null; 
            }
            void ReadBackAllRegisters(REGsMask regs)
            {
                uint data;
                uint data1;
                if ((regs & REGsMask.CFR1) == REGsMask.CFR1)
                {
                    ReadRegister(AD9910REGs.CFR1, out data);
                    m_allRegs.cfr1 = BF.CreateBitField<CFR1_ControlFunction>(data);

                }

                if ((regs & REGsMask.CFR2) == REGsMask.CFR2)
                {
                    ReadRegister(AD9910REGs.CFR2, out data);
                    m_allRegs.cfr2 = BF.CreateBitField<CFR2_ControlFunction>(data);
                     
                }

                if ((regs & REGsMask.CFR3) == REGsMask.CFR3)
                {
                    ReadRegister(AD9910REGs.CFR3, out data);
                    m_allRegs.cfr3 = BF.CreateBitField<CFR3_ControlFunction>(data);
                    
                }

                if ((regs & REGsMask.FTW_FrequencyTuningWordRegister) == REGsMask.FTW_FrequencyTuningWordRegister)
                {
                    ReadRegister(AD9910REGs.FTW_FrequencyTuningWordRegister, out data);
                    m_allRegs.ftw = BF.CreateBitField<FTW_FrequencyTuningWord>(data);

                }


                if ((regs & REGsMask.DigitalRampRateRegister) == REGsMask.DigitalRampRateRegister)
                {
                    ReadRegister(AD9910REGs.DigitalRampRateRegister, out data);
                    m_allRegs.ddrr = BF.CreateBitField<DigitalRampRate>(data);
                   
                }
                /*
                ulong udata;
                if ((regs & REGsMask.SingleToneProfile0) == REGsMask.SingleToneProfile0)
                {
                    
                    ReadRegister(AD9910REGs.SingleToneProfile0, out udata);
                    m_allRegs.stp[0] = BF.CreateBitField<SingleToneProfile>(udata);
                }
                if ((regs & REGsMask.SingleToneProfile1) == REGsMask.SingleToneProfile1)
                {

                    ReadRegister(AD9910REGs.SingleToneProfile1, out udata);
                    m_allRegs.stp[1] = BF.CreateBitField<SingleToneProfile>(udata);
                }
                if ((regs & REGsMask.SingleToneProfile2) == REGsMask.SingleToneProfile2)
                {

                    ReadRegister(AD9910REGs.SingleToneProfile2, out udata);
                    m_allRegs.stp[2] = BF.CreateBitField<SingleToneProfile>(udata);
                }

                if ((regs & REGsMask.SingleToneProfile3) == REGsMask.SingleToneProfile3)
                {

                    ReadRegister(AD9910REGs.SingleToneProfile3, out udata);
                    m_allRegs.stp[3] = BF.CreateBitField<SingleToneProfile>(udata);
                }

                if ((regs & REGsMask.SingleToneProfile4) == REGsMask.SingleToneProfile4)
                {

                    ReadRegister(AD9910REGs.SingleToneProfile4, out udata);
                    m_allRegs.stp[4] = BF.CreateBitField<SingleToneProfile>(udata);
                }

                if ((regs & REGsMask.SingleToneProfile5) == REGsMask.SingleToneProfile5)
                {

                    ReadRegister(AD9910REGs.SingleToneProfile4, out udata);
                    m_allRegs.stp[5] = BF.CreateBitField<SingleToneProfile>(udata);
                }

                if ((regs & REGsMask.SingleToneProfile6) == REGsMask.SingleToneProfile6)
                {

                    ReadRegister(AD9910REGs.SingleToneProfile6, out udata);
                    m_allRegs.stp[6] = BF.CreateBitField<SingleToneProfile>(udata);
                }

                if ((regs & REGsMask.SingleToneProfile7) == REGsMask.SingleToneProfile7)
                {

                    ReadRegister(AD9910REGs.SingleToneProfile7, out udata);
                    m_allRegs.stp[7] = BF.CreateBitField<SingleToneProfile>(udata);
                }
                */

            }
            void InitRegDumpProfile()
            {
                uint data;
                // to fill a real one
                m_regs[(byte)AD9910REGs.CFR1] = 2;
                WriteRegister(AD9910REGs.CFR1, m_regs[(byte)AD9910REGs.CFR1]);

                
                ReadRegister(AD9910REGs.CFR1, out data);



                m_regs[(byte)AD9910REGs.CFR2] = 0x400820;
                WriteRegister(AD9910REGs.CFR2, m_regs[(byte)AD9910REGs.CFR2]);


                ReadRegister(AD9910REGs.CFR2, out data);

                m_regs[(byte)AD9910REGs.CFR3] = 0x1F3FC000;//0x3D3F4128;
                if (m_externalClock1000Mega == true)
                {
                    m_regs[(byte)AD9910REGs.CFR3] = 0x3D3FC028;
                }
                WriteRegister(AD9910REGs.CFR3, m_regs[(byte)AD9910REGs.CFR3]);

                ReadRegister(AD9910REGs.CFR3, out data);

                m_regs[(byte)AD9910REGs.AuxiliaryDACControlRegister] = 0x7F7F;
                WriteRegister(AD9910REGs.AuxiliaryDACControlRegister, m_regs[(byte)AD9910REGs.AuxiliaryDACControlRegister]);



                ReadRegister(AD9910REGs.AuxiliaryDACControlRegister, out data);

                m_regs[(byte)AD9910REGs.IOUpdateRateRegister] = 0xFFFFFFFF;
                WriteRegister(AD9910REGs.IOUpdateRateRegister, m_regs[(byte)AD9910REGs.IOUpdateRateRegister]);

                ReadRegister(AD9910REGs.IOUpdateRateRegister, out data);

                /*
                m_regs[(byte)AD9910REGs.FTW_FrequencyTuningWordRegister] = 0;
                WriteRegister(AD9910REGs.FTW_FrequencyTuningWordRegister, m_regs[(byte)AD9910REGs.FTW_FrequencyTuningWordRegister]);


                m_regs[(byte)AD9910REGs.POW_PhaseOffsetWordRegister] = 0;
                WriteRegister(AD9910REGs.POW_PhaseOffsetWordRegister, m_regs[(byte)AD9910REGs.POW_PhaseOffsetWordRegister]);
                */


                /*                
                m_regs[(byte)AD9910REGs.ASF_AmplitudeScaleFactorRegister] = 0x0;
                m_regs[(byte)AD9910REGs.MultichipSyncRegister] = 0x0;
                m_regs[(byte)AD9910REGs.DigitalRampLimitRegister] = 0x0;
                m_regs[(byte)AD9910REGs.DigitalRampStepSizeRegister] = 0x0;
                */

                m_regs[(byte)AD9910REGs.DigitalRampRateRegister] = 0x0;
                WriteRegister(AD9910REGs.DigitalRampRateRegister, m_regs[(byte)AD9910REGs.DigitalRampRateRegister]);
                ReadRegister(AD9910REGs.DigitalRampRateRegister, out data);


                ulong data1;

                byte[] profile0 = { 0x08, 0xB5, 0x00, 0x00, 0x33, 0x33, 0x33, 0x33 }; // 200
                //byte[] profile0 = { 0x08, 0xB5, 0x00, 0x00, 0x28, 0xF5, 0xC2, 0x8F }; // 160
                WriteRegister(AD9910REGs.SingleToneProfile0, profile0);

                ReadRegister(AD9910REGs.SingleToneProfile0, out data1);

                byte[] profile1 = { 0x08, 0xB5, 0x00, 0x00, 0x2E, 0x14, 0x7A, 0xE1 };
                WriteRegister(AD9910REGs.SingleToneProfile1, profile1);

                byte[] profile2 = { 0x08, 0xB5, 0x00, 0x00, 0x29, 0x99, 0x99, 0x9A };
                WriteRegister(AD9910REGs.SingleToneProfile2, profile2);

                byte[] profile3 = { 0x08, 0xB5, 0x00, 0x00, 0x29, 0x99, 0x99, 0x9A };
                WriteRegister(AD9910REGs.SingleToneProfile3, profile3);

                byte[] profile4 = { 0x08, 0xB5, 0x00, 0x00, 0x29, 0x99, 0x99, 0x9A };
                WriteRegister(AD9910REGs.SingleToneProfile4, profile4);

                byte[] profile5 = { 0x08, 0xB5, 0x00, 0x00, 0x29, 0x99, 0x99, 0x9A };
                WriteRegister(AD9910REGs.SingleToneProfile5, profile5);

                byte[] profile6 = { 0x08, 0xB5, 0x00, 0x00, 0x29, 0x99, 0x99, 0x9A };
                WriteRegister(AD9910REGs.SingleToneProfile6, profile6);

                byte[] profile7 = { 0x08, 0xB5, 0x00, 0x00, 0x29, 0x99, 0x99, 0x9A };
                WriteRegister(AD9910REGs.SingleToneProfile7, profile7);

            }

            void InitRegDumpDRGContinues()
            {

                // to fill a real one
                m_regs[(byte)AD9910REGs.CFR1] = 2;
                WriteRegister(AD9910REGs.CFR1, m_regs[(byte)AD9910REGs.CFR1]);

                m_regs[(byte)AD9910REGs.CFR2] = 0x4E0820;
                WriteRegister(AD9910REGs.CFR2, m_regs[(byte)AD9910REGs.CFR2]);

                m_regs[(byte)AD9910REGs.CFR3] = 0x1D3F4128;
                if (m_externalClock1000Mega == true)
                {
                    m_regs[(byte)AD9910REGs.CFR3] = 0x3D3FC028;
                }
                WriteRegister(AD9910REGs.CFR3, m_regs[(byte)AD9910REGs.CFR3]);

                m_regs[(byte)AD9910REGs.AuxiliaryDACControlRegister] = 0x7F7F;
                WriteRegister(AD9910REGs.AuxiliaryDACControlRegister, m_regs[(byte)AD9910REGs.AuxiliaryDACControlRegister]);

                m_regs[(byte)AD9910REGs.IOUpdateRateRegister] = 0xFFFFFFFF;
                WriteRegister(AD9910REGs.IOUpdateRateRegister, m_regs[(byte)AD9910REGs.IOUpdateRateRegister]);


                byte[] DrglimitReg = { 0x9A, 0x99, 0x99, 0x19, 0x33, 0x33, 0x33, 0x33 };
                WriteRegister(AD9910REGs.DigitalRampLimitRegister, DrglimitReg);

                byte[] DrgStepSizeReg = { 0x37, 0x89, 0x41, 0x00, 0x37, 0x89, 0x41, 0x00 };
                WriteRegister(AD9910REGs.DigitalRampStepSizeRegister, DrgStepSizeReg);


                m_regs[(byte)AD9910REGs.DigitalRampRateRegister] = 0xFFDCFFDC;
                WriteRegister(AD9910REGs.DigitalRampRateRegister, m_regs[(byte)AD9910REGs.DigitalRampRateRegister]);

                
 


            }

            public void ChangeInternalProfile(byte profile)
            {
                if (profile < 1 || profile > 8)
                {
                    throw (new SystemException("profile range 1 - 8"));
                }
                uint data;
                ReadRegister(AD9910REGs.CFR1, out data);
                m_allRegs.cfr1 = BF.CreateBitField<CFR1_ControlFunction>(data);

                profile = (byte)(profile & 0xF);
                m_allRegs.cfr1.InternalProfileControl = profile;

                uint l = m_allRegs.cfr1.ToUInt32();          
                WriteRegister(AD9910REGs.CFR1, l);

            }

            /// <summary>
            /// The direction of the ramping function is controlled by the
            //  DRCTL pin. A Logic 0 on this pin causes the DRG to ramp
            //  with a negative slope, whereas a Logic 1 causes the DRG to
            //  ramp with a positive slope
            /// </summary>
            public void DRGRampDirection(bool PositiveSlope)
            {
                if (PositiveSlope == true)
                    m_dio[(byte)AD9910_PINs.DRCTL].Value = true;
                else
                    m_dio[(byte)AD9910_PINs.DRCTL].Value = false;

            }
            /*
            DRG Slope Control
            The core of the DRG is a 32-bit accumulator clocked by a
            programmable timer. The time base for the timer is the DDS
            clock, which operates at ¼ fSYSCLK. The timer establishes the
            interval between successive updates of the accumulator. The
            positive (+Δt) and negative (−Δt) slope step intervals are
            independently programmable as given by
            +t = 4P \ fsysclk
            -t = 4N \ fsysclk             
             */
            public void SetDRGNegativeSlopeRate(float time)
            {

                uint N = (uint)((time * Fsysclk) / 4);

                uint data;
                //ReadRegister(AD9910REGs.DigitalRampRateRegister, out data);
                //m_allRegs.ddrr = BF.CreateBitField<DigitalRampRate>(data);                                              
                m_allRegs.ddrr.DigitalRampNegativeSlopeRate = (uint)(N & 0xFFFF);
                uint l = m_allRegs.ddrr.ToUInt32();
                WriteRegister(AD9910REGs.DigitalRampRateRegister, l);

            }

            public void DumpDRG()
            {

                WriteRegister(AD9910REGs.CFR2, 0x4E0820);
                WriteRegister(AD9910REGs.CFR3, 0x1F3FC000);
                WriteRegister(AD9910REGs.AuxiliaryDACControlRegister, 0x7F7F);
                WriteRegister(AD9910REGs.IOUpdateRateRegister, 0xFFFFFFFF);
                
                //byte[] reserved1 = { 0x0, 0x0, 0xC1, 0x02, 0xD1, 0x24, 0x29, 0xD8 };
                //WriteRegister(AD9910REGs.DigitalRampLimitRegister, reserved1);

                //byte[] reserved2 = { 0x0, 0x0, 0xC1, 0x02, 0xD1, 0x24, 0x29, 0xD8 };
                //WriteRegister(AD9910REGs.DigitalRampLimitRegister, reserved2);


                byte[] DrglimitReg = { 0x22, 0xDE, 0x00, 0xD3, 0x20, 0x00, 0x00, 0x0 };
                WriteRegister(AD9910REGs.DigitalRampLimitRegister, DrglimitReg);

                byte[] DrgStepSizeReg = { 0x00, 0x41, 0x89, 0x37, 0x00, 0x06, 0x8D, 0xB9 };
                WriteRegister(AD9910REGs.DigitalRampStepSizeRegister, DrgStepSizeReg);

                m_regs[(byte)AD9910REGs.DigitalRampRateRegister] = 0x000200EE;
                WriteRegister(AD9910REGs.DigitalRampRateRegister, m_regs[(byte)AD9910REGs.DigitalRampRateRegister]);

                byte[] profile0 = { 0x08, 0xB5, 0x00, 0x00, 0x1B, 0x85, 0x1E, 0xB8 }; 
                WriteRegister(AD9910REGs.SingleToneProfile0, profile0);

            }

            public void SetDRGPositiveSlopeRate(float time)
            {

                uint P = (uint)((time * Fsysclk) / 4);

                //uint data;
                //ReadRegister(AD9910REGs.DigitalRampRateRegister, out data);
                //m_allRegs.ddrr = BF.CreateBitField<DigitalRampRate>(data);
                m_allRegs.ddrr.DigitalRampPositiveSlopeRate = (uint)(P & 0xFFFF);
                uint l = m_allRegs.ddrr.ToUInt32();

                WriteRegister(AD9910REGs.DigitalRampRateRegister, l);

            }

            /// <summary>
            /// The DRG also supports a hold feature controlled via the DRHOLD
            //  pin. When this pin is set to Logic 1, the DRG is stalled at its last
            //  state; otherwise, the DRG operates normally
            /// </summary>
            /// <param name="stall"></param>
            public void DRGStall(bool stall)
            {
                if (stall == true)
                    m_dio[(byte)AD9910_PINs.DRHOLD].Value = true;
                else
                    m_dio[(byte)AD9910_PINs.DRHOLD].Value = false;
            }
            public void EnableDigitalRamp(bool enable)
            {
                uint data;
                ReadRegister(AD9910REGs.CFR2, out data);
                m_allRegs.cfr2 = BF.CreateBitField<CFR2_ControlFunction>(data);
                m_allRegs.cfr2.DigitalRampEnable = (byte)(enable == true ? 1 : 0);
                uint l = m_allRegs.cfr2.ToUInt32();
             
                WriteRegister(AD9910REGs.CFR2 , l);

            }

            public enum DRG_RAM_DEST
            {

                Frequency  = 0,
                Phase  = 1,
                Amplitude = 2
            }

 

            public void SetDigitalRampDecrementStepSize(float val)
            {
                uint data;
                uint _FTW = getFTW(val);
                ReadRegister(AD9910REGs.DigitalRampStepSizeRegister, out data);
                m_allRegs.drss = BF.CreateBitField<DigitalRampStepSize>(data);
                m_allRegs.drss.DigitalRampDecrementStepSize = _FTW;
                ulong l = m_allRegs.drss.ToUInt64();
                WriteRegister(AD9910REGs.DigitalRampStepSizeRegister, l);

            }
            public void SetDigitalRampIncrementStepSize(float val)
            {
                uint data;
                uint _FTW = getFTW(val);
                ReadRegister(AD9910REGs.DigitalRampStepSizeRegister, out data);
                m_allRegs.drss = BF.CreateBitField<DigitalRampStepSize>(data);
                m_allRegs.drss.DigitalRampIncrementStepSize = _FTW;
                ulong l = m_allRegs.drss.ToUInt64();
                WriteRegister(AD9910REGs.DigitalRampStepSizeRegister, l);

            }
   
            public void SetDigitalRampDestination(DRG_RAM_DEST drgDest)
            {
                uint data;
                ReadRegister(AD9910REGs.CFR2, out data);
                m_allRegs.cfr2 = BF.CreateBitField<CFR2_ControlFunction>(data);
                m_allRegs.cfr2.DigitalRampDestination = (uint)drgDest;
                uint l = m_allRegs.cfr2.ToUInt32();
                WriteRegister(AD9910REGs.CFR2, l);

            }
            /// <summary>
            /// 63:32 Digital ramp decrement
            /// step size
            /// 32-bit digital ramp decrement step size value.
            /// 31:0 Digital ramp increment
            /// step size
            /// 32-bit digital ramp increment step size value
            /// </summary>
            public void SetDigitalRampStepSize()
            {
               
            }

            public void SetDigitalRampUpperLimit(float upperLimitFrequency)
            {
                uint data;
                uint _FTW = getFTW(upperLimitFrequency);
                //ReadRegister(AD9910REGs.DigitalRampLimitRegister, out data);
                //m_allRegs.ddl = BF.CreateBitField<DigitalRampLimit>(data);
                m_allRegs.ddl.DigitalRampUpperLimit = (uint)_FTW;
                ulong l = m_allRegs.ddl.ToUInt64();
                WriteRegister(AD9910REGs.DigitalRampLimitRegister, l);
                //ulong l1 = 0; ;
                //ReadRegister(AD9910REGs.DigitalRampLimitRegister, out l1);
                //if (l1 != l)
                {
                    //throw(new SystemException("Failed to write tDigitalRampUpperLimit"));
                }

            }

            public void SetDigitalRampLowerLimit(float val)
            {
                ulong data;
                uint _FTW = getFTW(val);
                ReadRegister(AD9910REGs.DigitalRampLimitRegister, out data);
                m_allRegs.ddl = BF.CreateBitField<DigitalRampLimit>(data);
                m_allRegs.ddl.DigitalRampLowerLimit = (uint)_FTW;
                ulong l = m_allRegs.ddl.ToUInt64();
                WriteRegister(AD9910REGs.DigitalRampLimitRegister, l);

            }

            void SwitchTo3Wire()
            {
                uint data = 0;
                m_allRegs.cfr1 = BF.CreateBitField<CFR1_ControlFunction>(data);

                m_allRegs.cfr1.SDIOInputOnly = 1;

                uint l = m_allRegs.cfr1.ToUInt32();
                WriteRegister(AD9910REGs.CFR1, l);
            }

            public void InitializeDefaultRegisters()
            {
                
                try
                {

                    InitRegDumpProfile();
                    //InitRegDumpDRGContinues();
                    m_initialize = true;
                }
                catch (Exception err)
                {
                    throw (new SystemException(err.Message));
                }
                return;
            }

            public void ReadRegister(AD9910REGs reg , out uint data)
            {
                // read bit 7 is 1  , msb first by default 
                m_spi.SS = (int)m_cs;

                byte[] dreg = { (byte)(0x80 | (byte)reg) };

                //m_dio[(int)AD9910_PINs.SPI_CS].Clear();

                m_spi.Write(dreg);
                    
                byte[] r = m_spi.Read(4);
                //m_dio[(int)AD9910_PINs.SPI_CS].Set();

                /// By default the chip is MSB first
                data = (uint)(r[0] << 24 | r[1] << 16 | r[2] << 8 | r[3]);
             
            }

            public void ReadRegister(AD9910REGs reg, out ulong data)
            {
                m_spi.SS = (int)m_cs;
                // read bit 7 is 1  , msb first by default 
                byte[] dreg = { (byte)(0x80 | (byte)reg) };
                m_spi.Write(dreg);
                byte[] r = m_spi.Read(8);

                /// By default the chip is MSB first
                uint data1 = (uint)(r[0] << 24 | r[1] << 16 | r[2] << 8 | r[3]);
                uint data2 = (uint)(r[4] << 24 | r[5] << 16 | r[6] << 8 | r[7]);

                data = data1 << 32 | data2;
            }

            public void ReadRegister(AD9910REGs reg, out byte [] data)
            {
                m_spi.SS = (int)m_cs;

                byte[] dreg = { (byte)(0x80 | (byte)reg) };
                m_spi.Write(dreg);
                data = new byte[8];
                data = m_spi.Read(data.Length);

            }

            /// <summary>
            /*
             Setting both no-dwell bits invokes a continuous ramping mode
            of operation; that is, the DRG output automatically oscillates
            between the two limits using the programmed slope parameters.
            Furthermore, the function of the DRCTL pin is slightly different.
            Instead of controlling the initiation of the ramp sequence, it
            only serves to change the direction of the ramp; that is, if the
            DRG output is in the midst of a positive slope and the DRCTL
            pin transitions from Logic 1 to Logic 0, then the DRG immediately
            switches to the negative slope parameters and resumes
            oscillation between the limits. Likewise, if the DRG output is in
            the midst of a negative slope and the DRCTL pin transitions from
            Logic 0 to Logic 1, the DRG immediately switchesto the positive
            slope parameters and resumes oscillation between the limits
             */
            /// </summary>

            public string SetContinuesDRGOperation(bool continues)
            {

                try
                {
                    uint data;
                    //ReadRegister(AD9910REGs.CFR2, out data);
                    //m_allRegs.cfr2 = BF.CreateBitField<CFR2_ControlFunction>(data);
                    
                    if (continues == true)
                    {
                        m_allRegs.cfr2.DigitalRampNoDwellHigh = 1;
                        m_allRegs.cfr2.DigitalRampNoDwellLow = 1;
                    }
                    else
                    {
                        m_allRegs.cfr2.DigitalRampNoDwellHigh = 0;
                        m_allRegs.cfr2.DigitalRampNoDwellLow = 0;
                    }
                    uint l = m_allRegs.cfr2.ToUInt32();
                    WriteRegister(AD9910REGs.CFR2, l);
                    return "ok";
                }
                catch (Exception err)
                {
                    return err.Message;
                }

            }

            /// <summary>
            /*
            if the digital ramp no-dwell high bit is set when the DRG reaches the
            upper limit, it automatically (and immediately) snaps to the lower
            limit (that is, it does not ramp back to the lower limit; it jumps to
            the lower limit)
             */
            /// </summary>
            public string DRGSnapsBackToLowerFromHigherLimit()
            {
                try
                {
                    uint data;
                    ReadRegister(AD9910REGs.CFR2, out data);
                    m_allRegs.cfr2 = BF.CreateBitField<CFR2_ControlFunction>(data);

                    m_allRegs.cfr2.DigitalRampNoDwellHigh = 1;
                    uint l = m_allRegs.cfr2.ToUInt32();
                    WriteRegister(AD9910REGs.CFR2, l);
                    return "ok";
                }
                catch (Exception err)
                {
                    return err.Message;
                }
            }

            /// <summary>
            /*
            Likewise, when the digital ramp no-dwell low bit is
            set, and the DRG reaches the lower limit, it automatically (and
            immediately) snaps to the upper limit.
            */
            /// </summary>
            public string DRGSnapsBackToHigherLimitFromLowLimit()
            {
                try
                {
                    uint data;
                    ReadRegister(AD9910REGs.CFR2, out data);
                    m_allRegs.cfr2 = BF.CreateBitField<CFR2_ControlFunction>(data);

                    m_allRegs.cfr2.DigitalRampNoDwellLow = 1;
                    uint l = m_allRegs.cfr2.ToUInt32();
                    WriteRegister(AD9910REGs.CFR2, l);
                    return "ok";
                }
                catch (Exception err)
                {
                    return err.Message;
                }
            }
                        
            public void WriteRegister(AD9910REGs reg, uint data)
            {

                m_spi.SS = (int)m_cs;


                // write bit 7 is 0 
                byte[] ddata = {
                                    (byte)(reg),
                                    (byte)((data >> 24) & 0xFF),
                                    (byte)((data >> 16) & 0xFF),
                                    (byte)((data >> 8) & 0xFF),
                                    (byte)(data  & 0xFF),
                                };

                m_spi.Write(ddata); // 5 bytes
                m_dio[(int)AD9910_PINs.IOUPDATE].Value = false;
                m_dio[(int)AD9910_PINs.IOUPDATE].Value = true;
                m_dio[(int)AD9910_PINs.IOUPDATE].Value = false;
            }

            public void WriteRegister(AD9910REGs reg, ulong data)
            {
                m_spi.SS = (int)m_cs;
                byte[] ddata = {
                                    (byte)(reg),
                                    (byte)((data >> 56) & 0xFF),
                                    (byte)((data >> 48) & 0xFF),
                                    (byte)((data >> 40) & 0xFF),
                                    (byte)((data >> 32) & 0xFF),
                                    (byte)((data >> 24) & 0xFF),
                                    (byte)((data >> 16) & 0xFF),
                                    (byte)((data >> 8) & 0xFF),
                                    (byte)(data  & 0xFF),
                                };

                m_spi.Write(ddata); // 5 bytes
                m_dio[(int)AD9910_PINs.IOUPDATE].Value = false;
                m_dio[(int)AD9910_PINs.IOUPDATE].Value = true;
                m_dio[(int)AD9910_PINs.IOUPDATE].Value = false;
            }

            void WriteRegister(AD9910REGs reg, byte [] data)
            {
                m_spi.SS = (int)m_cs;

                byte[] ddata = new byte[data.Length + 1];
                int j = 1;

              
                ddata[0] = (byte)reg;
                for (int i = 0; i < data.Length; i++)
                {
                    ddata[j++] = data[i];
                }
              
                m_spi.Write(ddata);

                m_dio[(int)AD9910_PINs.IOUPDATE].Value = false;
                m_dio[(int)AD9910_PINs.IOUPDATE].Value = true;
                m_dio[(int)AD9910_PINs.IOUPDATE].Value = false;
            }

            public void SetDac(byte val)
            {
                //double iOut = (86.4 / 10000) * ( 1 + )
            }
           
            public string SetProfileFrequency(int profileId, float frequency)
            {
                try
                {
                    uint _FTW = getFTW(frequency);
                    byte[] b = BitConverter.GetBytes(_FTW);
                    byte[] pdata = new byte[8];
                    ReadRegister((AD9910REGs)(AD9910REGs.SingleToneProfile0 + profileId), out pdata);
                    int j = 0;
                    for (int i = 4; i < 8; i++)
                    {
                        pdata[i] = b[j++];
                    }
                    WriteRegister((AD9910REGs)(AD9910REGs.SingleToneProfile0 + profileId), pdata);

                    return "ok";
                }
                catch (Exception err)
                {
                    return err.Message;
                }
            }

            public void SetProfile(int profileId, float frequency, ushort phaseOffset, float amplitudeSF)
            {
                uint _FTW = getFTW(frequency);
                m_allRegs.stp[profileId].AplitudeScale = 0x08C0;// amplitudeSF;
                m_allRegs.stp[profileId].FrequencyTuningWord = _FTW;
                m_allRegs.stp[profileId].PhaseOffsetWord = phaseOffset;
                ulong l = m_allRegs.stp[profileId].ToUInt64();
                WriteRegister((AD9910REGs)(AD9910REGs.SingleToneProfile0 + profileId), l);
            }

            public string SetProfilePhaseOffsetDegree(int profileId , ushort phaseOffsetDeg)
            {
                try
                {
                    ushort phaseOffsetWord = (ushort)(((float)((Math.Pow(2, 16) * phaseOffsetDeg) / 360)));
                    uint data;
                    ReadRegister((AD9910REGs)(AD9910REGs.SingleToneProfile0 + profileId), out data);
                    m_allRegs.stp[profileId] = BF.CreateBitField<SingleToneProfile>(data);

                    m_allRegs.stp[profileId].PhaseOffsetWord = phaseOffsetWord;
                    ulong l = m_allRegs.stp[profileId].ToUInt64();

                    byte[] b = BitConverter.GetBytes(l);
                    WriteRegister((AD9910REGs)(AD9910REGs.SingleToneProfile0 + profileId), b);
                    return "ok";
                }
                catch (Exception err)
                {
                    return err.Message;
                }
            }
            // this is a 64 bit register , we are writing all of it.

            public string SetProfileAmplitudeSF(int profileId, ushort amplitudeSF)
            {
                try
                {
                    ushort asf = (ushort)(amplitudeSF * Math.Pow(2, 14));
                    uint data;
                    ReadRegister((AD9910REGs)(AD9910REGs.SingleToneProfile0 + profileId), out data);
                    m_allRegs.stp[profileId] = BF.CreateBitField<SingleToneProfile>(data);

                    m_allRegs.stp[profileId].AplitudeScale = asf;
                    ulong l = m_allRegs.stp[profileId].ToUInt64();
                    byte[] b = BitConverter.GetBytes(l);
                    WriteRegister((AD9910REGs)(AD9910REGs.SingleToneProfile0 + profileId), b);
                    return "ok";
                }
                catch (Exception err)
                {
                    return err.Message;
                }
            }

            public string SetProfileAmplitudedB(int profileId, ushort amplitudeScale)
            {
                try
                {
                    ushort asf = (ushort)(20 * Math.Log(amplitudeScale * Math.Pow(2, 14)));
                    uint data;
                    ReadRegister((AD9910REGs)(AD9910REGs.SingleToneProfile0 + profileId), out data);
                    m_allRegs.stp[profileId] = BF.CreateBitField<SingleToneProfile>(data);

                    m_allRegs.stp[profileId].AplitudeScale = asf;
                    ulong l = m_allRegs.stp[profileId].ToUInt64();
                    byte[] b = BitConverter.GetBytes(l);
                    WriteRegister((AD9910REGs)(AD9910REGs.SingleToneProfile0 + profileId), b);

                    return "ok";
                }
                catch (Exception err)
                {
                    return err.Message;
                }
            }


            public void SetProfileMode(byte mode)
            {
                 
                if ((mode  & 1) == 0)
                    m_dio[(int)AD9910_PINs.PROFILE_0].Value = false;
                else
                    m_dio[(int)AD9910_PINs.PROFILE_0].Value = true;

                /*
                if (((mode >> 1) & 1) == 0)
                    m_dio[(int)AD9910_PINs.PROFILE_1].Value = false;
                else
                    m_dio[(int)AD9910_PINs.PROFILE_1].Value = true;

                if (((mode >> 2) & 1) == 0)
                    m_dio[(int)AD9910_PINs.PROFILE_2].Value = false;
                else
                    m_dio[(int)AD9910_PINs.PROFILE_2].Value = true;
                */
            }
             
            
            public void SetAuxDacPowerDown(bool PowerDown)
            {
                uint data;
                ReadRegister(AD9910REGs.CFR1, out data);
                m_allRegs.cfr1 = BF.CreateBitField<CFR1_ControlFunction>(data);

                if (PowerDown == true)
                    m_allRegs.cfr1.AuxDACPowerDown = 1;
                else
                    m_allRegs.cfr1.AuxDACPowerDown = 0;

                uint l = m_allRegs.cfr1.ToUInt32();
                WriteRegister(AD9910REGs.CFR1, l);
            }

            public void SetSystemClock(uint systemClock)
            {
                Fsysclk = systemClock;
            }

            public void SetEnablePowerDown(bool PowerDown)
            {
                uint data;
                ReadRegister(AD9910REGs.CFR1, out data);
                m_allRegs.cfr1 = BF.CreateBitField<CFR1_ControlFunction>(data);

                if (PowerDown == true)
                    m_allRegs.cfr1.ExternalPowerDownControl = 1;
                else
                    m_allRegs.cfr1.ExternalPowerDownControl = 0;

                uint l = m_allRegs.cfr1.ToUInt32();
                WriteRegister(AD9910REGs.CFR1, l);
            }


            public void SetDigitalDacPowerDown(bool PowerDown)
            {
                uint data;
                ReadRegister(AD9910REGs.CFR1, out data);
                m_allRegs.cfr1 = BF.CreateBitField<CFR1_ControlFunction>(data);

                if (PowerDown == true)
                    m_allRegs.cfr1.DACPowerdown = 1;
                else
                    m_allRegs.cfr1.DACPowerdown = 0;

                uint l = m_allRegs.cfr1.ToUInt32();
                WriteRegister(AD9910REGs.CFR1, l);
            }

            public void SetDigitalPowerdown(bool PowerDown)
            {
                uint data;
                ReadRegister(AD9910REGs.CFR1, out data);
                m_allRegs.cfr1 = BF.CreateBitField<CFR1_ControlFunction>(data);

                if (PowerDown == true)
                    m_allRegs.cfr1.DigitalPowerdown = 1;
                else
                    m_allRegs.cfr1.DigitalPowerdown = 0;

                uint l = m_allRegs.cfr1.ToUInt32();
                WriteRegister(AD9910REGs.CFR1, l);
            }

            uint getFTW(double Fout)
            {
                double x = Math.Pow(2, 32);
                double f = x * Fout / Fsysclk;
                double FTW = Math.Round(f);
                uint _FTW = (uint)FTW;
                return _FTW;
            }

            public string SetFrequency(double Fout)
            {
                try
                {
                    if (m_initialize == false)
                    {
                        return "DDS Not initialized";
                    }

                    uint _FTW = getFTW(Fout);

                    byte[] ftw = {
                                   (byte)(_FTW & 0xFF),
                                   (byte)((_FTW >> 8) & 0xFF),
                                   (byte)((_FTW >> 16) & 0xFF),
                                   (byte)((_FTW >> 24) & 0xFF),
                               };
                    WriteRegister(AD9910REGs.FTW_FrequencyTuningWordRegister, ftw);
                    return "ok";
                    
                }
                catch (Exception err)
                {
                    return err.Message;
                }
            }
        }
    }

}
