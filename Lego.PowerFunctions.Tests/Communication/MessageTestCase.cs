using System.Runtime.Serialization;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Commands;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Communication;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Control;

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Tests.Communication
{
    [DataContract]
    internal class MessageTestCase
    {
        [DataMember]
        public ICommand Command { get; set; }

        [DataMember]
        public Channel Channel { get; set; }

        [DataMember]
        public Toggle Toggle { get; set; }

        [DataMember]
        public string Result { get; set; }
    }
}