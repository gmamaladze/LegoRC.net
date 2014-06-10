// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using Gma.Netmf.Hardware.Lego.PowerFunctions.Control;

#endregion

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Commands
{
    public class CommandFactory
    {
        public static Command Create(ExtFunction extFunction)
        {
            return new ExtendedCmd(extFunction);
        }

        public static Command Create(DirectState blueState, DirectState redState)
        {
            return new ComboDirectCmd(blueState, redState);
        }

        public static Command Create(PwmSpeed redSpeed, PwmSpeed blueSpeed)
        {
            return new ComboPwmCmd(redSpeed, blueSpeed);
        }

        public static Command Create(Output output, IncDec incDec)
        {
            return new SingleOutputCmd(output, incDec);
        }

        public static Command Create(Output output, PwmSpeed speed)
        {
            return new SingleOutputCmd(output, speed);
        }
    }
}