// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using Gma.Netmf.Hardware.Lego.PowerFunctions.Control;

#endregion

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Commands
{
    public class ExtendedCmd : Command
    {
        private readonly ExtFunction m_ExtFunction;

        public ExtendedCmd(ExtFunction extFunction)
        {
            m_ExtFunction = extFunction;
        }

        public override CommandType CommandType
        {
            get { return CommandType.Extended; }
        }

        public ExtFunction ExtFunction
        {
            get { return m_ExtFunction; }
        }
    }
}