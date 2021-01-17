using System.Device;
using System;
using System.Device.Gpio;
using System.Diagnostics;
using System.Globalization;

namespace FrequencyForOnePin
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputPinNum = 10;
            var gpio = new GpioController();
            gpio.OpenPin(inputPinNum, PinMode.Input);

            gpio.Read(inputPinNum); // Warmup

            var stopWatch = Stopwatch.StartNew();
            var cycleCounter = 0;

            while (stopWatch.Elapsed.TotalSeconds < 10)
            {
                gpio.Read(inputPinNum);
                ++cycleCounter;
            }

            Console.WriteLine("Read Frequency for input pin #" + inputPinNum.ToString() +" : "
                  + (cycleCounter / stopWatch.Elapsed.TotalSeconds).ToString("N", CultureInfo.InvariantCulture)
                  + " Hz");
            Console.WriteLine("Runnning for : " + stopWatch.Elapsed.TotalSeconds + " seconds");
        }
    }
}
