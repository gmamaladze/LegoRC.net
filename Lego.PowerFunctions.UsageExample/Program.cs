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
            using (var transmitter = new Transmitter())
            {

                //Firts vehiclle
                var receiverTrain = new Receiver(transmitter, Channel.Ch1);
                var motor = new Motor(receiverTrain.RedConnector);
                var light = new Led(receiverTrain.RedConnector);

                //Rover on another channel
                var receiverRover = new Receiver(transmitter, Channel.Ch2);
                var drive = new Motor(receiverRover.BlueConnector);
                var steeringWheel = new Servo(receiverRover.RedConnector);

                //Now Control
                while (true)
                {
                    motor.IncSpeed();
                    light.TurnOn();

                    drive.SetSpeed(100);
                    steeringWheel.SetAngle180(45);

                    Thread.Sleep(10000);

                    steeringWheel.Center();
                    motor.Brake();

                    light.TurnOff();
                }
            }
        }
    }
}