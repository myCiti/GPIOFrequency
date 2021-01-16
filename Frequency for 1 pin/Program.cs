using System.Device;
using System;
using System.Device.Gpio;
using System.Diagnostics;

namespace Frequency_for_1_pin
{
    class Program
    {
        static void Main(string[] args)
        {
            var gpio = new GpioController();
            gpio.OpenPin(5, PinMode.Input);

            gpio.Read(5); // Warmup

            var stopWatch = Stopwatch.StartNew();
            var cycleCounter = 0;
            while (stopWatch.Elapsed.TotalSeconds < 5000)
            {
                gpio.Read(5);
                ++cycleCounter;
            }

            Console.WriteLine("Read Frequency: " + cycleCounter / stopWatch.Elapsed.TotalSeconds + "Hz");
        }
    }
}
