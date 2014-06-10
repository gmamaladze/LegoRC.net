using System;
using Gma.Netmf.Hardware.Lego.PowerFunctions.Control;
using NUnit.Framework;

namespace Gma.Netmf.Hardware.Lego.PowerFunctions.Tests
{
    [TestFixture]
    public class PwmSpeedExtensionsTest
    {
        [TestCase(0, PwmSpeed.BreakThenFloat)]
        [TestCase(100, PwmSpeed.ForwardStep7)]
        [TestCase(-100, PwmSpeed.BackwardStep7)]
        [TestCase(101, PwmSpeed.Float, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(-101, PwmSpeed.Float, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(30, PwmSpeed.ForwardStep2)]
        [TestCase(15, PwmSpeed.ForwardStep1)]
        public void TestPercentToSpeedConvertion(int percent, PwmSpeed expected)
        {
            PwmSpeed actual = percent.FromPercent();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(90, PwmSpeed.BreakThenFloat)]
        [TestCase(0, PwmSpeed.BackwardStep7)]
        [TestCase(-1, PwmSpeed.Float, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(181, PwmSpeed.Float, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(180, PwmSpeed.ForwardStep7)]
        [TestCase(45, PwmSpeed.BackwardStep3)]
        [TestCase(135, PwmSpeed.ForwardStep3)]
        public void TestAngleToPosition(int angle, PwmSpeed expected)
        {
            PwmSpeed actual = angle.FromAngle();
            Assert.AreEqual(expected, actual);
        }

    }
}
