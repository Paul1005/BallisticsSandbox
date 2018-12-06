using System;
using BallisticsSandbox;
using BallisticsSandbox.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class TestCaculations
    {
        [TestMethod]
        public void TestInputs()
        {
            // Arrange
            Calculator calculator = new Calculator();
            MainWindow mainWindow = new MainWindow
            {
                velocity = 864,
                weight = 4,
                diameter = 5.56,
                gravity = 9.8,
                airDensity = 1.225,
                dragCoefficient = 0.25,
                angle = 45
            };

            // Act
            mainWindow.Test_Calculate_Click();
            Output output = mainWindow.output;

            // Assert
            Assert.AreEqual(calculator.CalculateArea(5.56), output.area);
            Assert.AreEqual(calculator.CalculateKineticEnergy(4, 864), output.maxKineticEnergy);
            Assert.AreEqual(calculator.CalculatePenetration(output.maxKineticEnergy, output.area), output.maxPenetration);
            Assert.AreEqual(calculator.CalculateMomentum(4, 864), output.maxMomentum);
            Assert.AreEqual(calculator.CalculateTerminalVelocity(4, 9.8, 0.25, 1.225, output.area), output.terminalVelocity);
            Assert.AreEqual(calculator.CalculateRange(864, 9.8, 45, output.terminalVelocity), output.range);
            Assert.AreEqual(calculator.CalculateTimeInFlight(864, 45, 9.8, output.terminalVelocity), output.flightTime);
        }
    }
}
