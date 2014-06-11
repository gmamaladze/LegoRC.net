// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System.Threading;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Actuators;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Communication;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Control;

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.UsageExample
{
    public class Program
    {
        public static void Main()
        {
            var transmitter = new Transmitter();
            var receiver = new Receiver(transmitter, Channel.Ch1);
            var motor = new Motor(receiver.RedConnector);

            motor.SetSpeed(100);
            Thread.Sleep(1000);
            motor.Brake();
        }
    }
}