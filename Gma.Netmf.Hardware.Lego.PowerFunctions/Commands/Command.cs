// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Commands
{
    public abstract class Command
    {
        public abstract CommandType CommandType { get; }
    }
}