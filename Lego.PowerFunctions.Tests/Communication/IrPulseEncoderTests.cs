// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Communication;
using NUnit.Framework;

#endregion

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Tests.Communication
{
    [TestFixture]
    public class IrPulseEncoderTests
    {
        private const string PulseEncoderTestCases = "Communication\\PulseEncoderTestCases.txt";

        public static IEnumerable<TestCaseData> GetTestCasesFromFile()
        {
            using (var testData = File.OpenText(PulseEncoderTestCases))
            {
                while (!testData.EndOfStream)
                {
                    var line = testData.ReadLine();
                    if (line == null) yield break;
                    var parts = line.Split(';');
                    int message;
                    bool isOk = int.TryParse(parts[0].Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture,
                        out message);
                    if (!isOk)
                        throw new InvalidOperationException(
                            string.Format("Invalid test case data. {0} is not a hexadecimal number.", parts[0]));
                    var testCaseData = new TestCaseData((ushort) message).Returns(parts[2]);
                    yield return testCaseData;
                }
            }
        }

        //[Test]
        public void GenerateTestCases()
        {
            var random = new Random();
            using (var testData = File.CreateText(PulseEncoderTestCases))
            {
                for (int i = 0; i < 100; i++)
                {
                    var message = (ushort) random.Next(0xFFFF);
                    var messageString = MessageToString(message);
                    var pulse = IrPulseEncoder.Encode(message);
                    var pulseString = GetPulseString(pulse);
                    var line = string.Format("0x{0:X4};{1};{2}", message, messageString, pulseString);
                    testData.WriteLine(line);
                }
            }
        }

        private string GetPulseString(IEnumerable<ushort> data)
        {
            var result = new StringBuilder();
            foreach (var word in data)
            {
                for (int bitNr = 0; bitNr < 16; bitNr++)
                {
                    bool bit = ((word << bitNr) & 0x8000) != 0;
                    var ch = bit ? '▄' : '█';
                    result.Append(ch);
                }
            }
            return result.ToString();
        }

        private string MessageToString(ushort message)
        {
            var result = new StringBuilder();
            result.Append('|');
            for (int nibbleNr = 0; nibbleNr < 4; nibbleNr++)
            {
                for (int bitNr = 0; bitNr < 4; bitNr++)
                {
                    bool bit = ((message << (nibbleNr*4 + bitNr)) & 0x8000) != 0;
                    char ch = bit ? '1' : '0';
                    result.Append(ch);
                }
                result.Append('|');
            }
            return result.ToString();
        }

        [Test, TestCaseSource("GetTestCasesFromFile")]
        public string RunTestCases(ushort message)
        {
            ushort[] data = IrPulseEncoder.Encode(message);
            return GetPulseString(data);
        }
    }
}