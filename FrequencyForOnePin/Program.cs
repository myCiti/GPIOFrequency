using System.Device;
using System;
using System.Device.Gpio;
using System.Diagnostics;

namespace FrequencyForOnePin
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputPinNum = 6;
            var gpio = new GpioController();
            gpio.OpenPin(inputPinNum, PinMode.Input);

            gpio.Read(inputPinNum); // Warmup

            var stopWatch = Stopwatch.StartNew();
            var cycleCounter = 0;

            while (stopWatch.Elapsed.TotalSeconds < 5)
            {
                gpio.Read(inputPinNum);
                ++cycleCounter;
            }

            Console.WriteLine("Read Frequency for input pin #" + inputPinNum.ToString() +" : "
                  + cycleCounter / stopWatch.Elapsed.TotalSeconds + "Hz");
        }
    }
}
