// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Commands;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Communication;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Control;
using NUnit.Framework;

#endregion

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Tests.Communication
{
    [TestFixture]
    public class MessageTests
    {
        private const string MessageExtendedModeTestCatses = "Communication\\MessageTestCases.json";

        private static IEnumerable<TestCaseData> GetTestCasesFromFile()
        {
            using (var file = File.OpenRead(MessageExtendedModeTestCatses))
            {
                var jsonSerializer = CreateJsonSerializer();
                var testCases = ((IEnumerable<MessageTestCase>) jsonSerializer.ReadObject(file)).ToArray();
                return testCases.Select(
                    tc => new TestCaseData(
                        tc.Command,
                        tc.Channel,
                        tc.Toggle)
                        .Returns(tc.Result));
            }
        }


        private static DataContractJsonSerializer CreateJsonSerializer()
        {
            var jsonSerializer = new DataContractJsonSerializer(
                typeof (IEnumerable<MessageTestCase>),
                new[]
                {
                    typeof (ExtendedCmd),
                    typeof (ComboDirectCmd),
                    typeof (ComboPwmCmd),
                    typeof (SingleOutputCmd)
                });
            return jsonSerializer;
        }

        private IEnumerable<MessageTestCase> GetTestCases()
        {
            foreach (Toggle toggle in Enum.GetValues(typeof (Toggle)))
                foreach (Channel channel in Enum.GetValues(typeof (Channel)))
                    foreach (var command in CreateCommands())
                    {
                        var message = MessageFactory.GetMessage(command, channel, toggle);
                        yield return new MessageTestCase
                        {
                            Command = command,
                            Channel = channel,
                            Toggle = toggle,
                            Result = message.ToString()
                        };
                    }
        }

        private IEnumerable<ICommand> CreateCommands()
        {
            return
                CreateComboDirectCommands()
                    .Concat(CreateComboPwCommands())
                    .Concat(CreateExtendedCommands())
                    .Concat(CreateSingleOutputCommands());
        }

        private IEnumerable<ICommand> CreateComboDirectCommands()
        {
            foreach (DirectState blueState in Enum.GetValues(typeof (DirectState)))
                foreach (DirectState redState in Enum.GetValues(typeof (DirectState)))
                    yield return new ComboDirectCmd(blueState, redState);
        }

        private IEnumerable<ICommand> CreateComboPwCommands()
        {
            foreach (PwmSpeed redSpeed in Enum.GetValues(typeof (PwmSpeed)))
                foreach (PwmSpeed blueSpeed in Enum.GetValues(typeof (PwmSpeed)))
                    yield return new ComboPwmCmd(redSpeed, blueSpeed);
        }

        private IEnumerable<ICommand> CreateExtendedCommands()
        {
            foreach (ExtFunction function in Enum.GetValues(typeof (ExtFunction)))
                yield return CommandFactory.Create(function);
        }

        private IEnumerable<ICommand> CreateSingleOutputCommands()
        {
            foreach (Output output in Enum.GetValues(typeof (Output)))
            {
                foreach (PwmSpeed speed in Enum.GetValues(typeof (PwmSpeed)))
                    yield return new SingleOutputCmd(output, speed);

                foreach (IncDec incDec in Enum.GetValues(typeof (IncDec)))
                    yield return new SingleOutputCmd(output, incDec);
            }
        }

        //[Test]
        public void GenerateTestCases()
        {
            using (var file = File.Create(MessageExtendedModeTestCatses))
            {
                var testCases = GetTestCases();
                var jsonSerializer = CreateJsonSerializer();
                jsonSerializer.WriteObject(file, testCases);
            }
        }

        [Test, TestCaseSource("GetTestCasesFromFile")]
        public string RunTestCases(ICommand command, Channel channel, Toggle toggle)
        {
            var message = MessageFactory.GetMessage(command, channel, toggle);
            return message.ToString();
        }
    }
}