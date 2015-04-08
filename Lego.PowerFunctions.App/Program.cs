using System;
using System.Threading;
using Gma.Netmf.Hardware.Lego.PowerFunctions.WebApi;

namespace Lego.PowerFunctions.App
{
    public class Program
    {
        public static void Main()
        {
            Server.Run();
           
            //using (var transmitter = new Transmitter())
            //{
            //    //Rover on another channel
            //    var receiverRover = new Receiver(transmitter, Channel.Ch2);
            //    var drive = new Motor(receiverRover.BlueConnector);
            //    var steeringWheel = new Servo(receiverRover.RedConnector);

            //    //Now Control
            //    while (true)
            //    {
            //        drive.SetSpeed(100);
            //        steeringWheel.SetAngle180(45);

            //        Thread.Sleep(1000);

            //        steeringWheel.Center();
            //    }
            //}

        }

    }
}
