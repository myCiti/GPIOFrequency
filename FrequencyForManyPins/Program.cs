using System;
using System.Device;
using System.Device.Gpio;
using System.Diagnostics;
using System.Globalization;

namespace FrequencyForManyPins
{
    class Program
    {
        static void Main(string[] args)
        {
            var iMapping = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            var oMapping = new int[] { 17, 18, 22, 23, 24, 25, 26, 27,  };

            int inputPinCount = iMapping.Length;
            int outputPinCount = oMapping.Length;

            var gpio = new GpioController();

            // define pin mode
            for (int i = 0; i < inputPinCount; ++i) gpio.OpenPin(iMapping[i], PinMode.Input);
            for (int i = 0; i < outputPinCount; ++i) gpio.OpenPin(oMapping[i], PinMode.Output);

            // warm up
            for (int i = 0; i < inputPinCount; ++i)  gpio.Read(iMapping[i]);

            var stopWatch = Stopwatch.StartNew();
            int cycleCounter = 0;

            // Loop for 10 seconds
            while(stopWatch.Elapsed.TotalSeconds < 10)
            {
                for (int i = 0; i < inputPinCount; ++i)  gpio.Read(iMapping[i]);
                for (int i = 0; i < outputPinCount; ++i) gpio.Write(oMapping[i], PinValue.High);
                for (int i = 0; i < outputPinCount; ++i) gpio.Write(oMapping[i], PinValue.Low);
                ++cycleCounter;
            }
            Console.WriteLine("Read/Write frequency for " + inputPinCount + " inputs and "
                               + outputPinCount + " outputs : "
                               + (cycleCounter / stopWatch.Elapsed.TotalSeconds).ToString("N", CultureInfo.InvariantCulture)
                               + "Hz"); ;
            Console.WriteLine("Running for : " + stopWatch.Elapsed.TotalSeconds + " seconds.");
        }
    }
}
