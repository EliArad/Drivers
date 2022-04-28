 
using System;
//STEP 1: 
//   Add the DLL as a reference to your project through "Project" -> "Add Reference" menu item within Visual Studio
using MCP2221;     //<---- Include this namespace

namespace MCP2221DLL_M_CSExampleCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //STEP 2:
            //	Make an instance of the MCP2221.MchpUsbI2c class. If using custom VID/PID, use VID and PID as arguments to the constructor.
            MCP2221.MchpUsbI2c UsbI2c = new MchpUsbI2c();

            //STEP 3:
			//	Navigate the DLL classes to find your desired function. Other examples are shown below.
			bool isConnected = UsbI2c.Settings.GetConnectionStatus();

			//Print the result to the console window
            if (isConnected == true)
            {
                Console.WriteLine("The device is connected.\n");

                //
                //  Ex. Check for the total number of devices connected. Select first one.
                //
                // Get total number of devices plugged into PC
                int devCount = UsbI2c.Management.GetDevCount();
                Console.WriteLine("There are " + devCount.ToString() + " MCP2221 devices plugged into the PC.\n");
                UsbI2c.Management.SelectDev(0);

                //
                //  Ex. Get USB descriptor string
                //

                string usbDescriptor = UsbI2c.Settings.GetUsbStringDescriptor();
                Console.WriteLine("The USB descriptor string is: " + usbDescriptor + "\n");

                //
                //  Get the current (SRAM) setting of the clock pin divider value
                //
                int rslt = UsbI2c.Settings.GetClockPinDividerValue(DllConstants.CURRENT_SETTINGS_ONLY);
                if (rslt > 0)	
                {
                    Console.WriteLine("The current value of clock pin divider is: " + (1 << rslt).ToString() + "\n");
                }
                else
                {
                    Console.WriteLine("Encountered error " + rslt.ToString() + " when getting clock pin divider value.");
                }
                
            }
            else
            {
                Console.WriteLine("The device is NOT connected.\n");
            }	
        }
    }
}
