using System;
using System.Device;
using System.Device.Gpio;
using System.Diagnostics;

namespace FrequencyForManyPins
{
    class Program
    {
        static void Main(string[] args)
        {
            var gpio = new GpioController();

            var iMapping = new int[] { 3, 5, 7, 11, 13, 15, 19, 21, 23, 29, 31, 33 };
            var oMapping = new int[] { 35, 37, 12, 16, 18, 22, 24, 26 };

            int inputPinCount = iMapping.Length;
            int outputPinCount = oMapping.Length;

            // define pin mode
            for (int i = 0; i < inputPinCount; ++i) gpio.OpenPin(iMapping[i], PinMode.Input);
            for (int i = 0; i < outputPinCount; ++i) gpio.OpenPin(oMapping[i], PinMode.Output);

            // warm up
            for (int i = 0; i < inputPinCount; ++i)  gpio.Read(iMapping[i]);

            StopWatch stopWatch = StopWatch.StartNew();
            int cycleCounter = 0;

            // Loop for 10 seconds
            while(stopWatch.Elapsed.TotalSeconds < 10)
            {
                for (int i = 0; i < inputPinCount; ++i)  gpio.Read(iMapping[i]);
                for (int i = 0; i < outputPinCount; ++i) gpio.Write(oMapping[i], PinValue.High);
                for (int i = 0; i < outputPinCount; ++i) gpio.Write(oMapping[i], PinValue.Low);
                ++cycleCounter;
            }
            Console.WriteLine($"Read/Write frequency for {inputPinCount} input pins an ");
            Console.WriteLine($"{outputPinCount} output pins : ");
            Console.WriteLine($"{ cycleCounter / stopWatch.Elapsed.TotalSeconds } Hz");
        }
    }
}
