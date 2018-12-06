using System;
using BallisticsSandbox;
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
            MainWindow mainWindow = new MainWindow();
            double velocity = 864;
            double weight = 4;
            double diameter = 5.56;
            double airDensity = 1.225;
            double dragCoefficient = 0.25;
            double area = (diameter / 2) * Math.PI;

            // Act
            mainWindow.CalculateKineticEnergy(weight, velocity);
            mainWindow.CalculateMomentum(weight, velocity);
            mainWindow.CalculatePenetration(mainWindow.kineticEnergy, diameter);

            // Assert
            Assert.AreEqual(0.5 * weight * Math.Pow(velocity, 2), mainWindow.kineticEnergy);
            Assert.AreEqual(weight * velocity, mainWindow.momentum);
            Assert.AreEqual(mainWindow.kineticEnergy / diameter, mainWindow.penetration);
            Assert.AreEqual((airDensity * dragCoefficient * area / 2) * Math.Pow(velocity, 2), mainWindow.drag);
        }

        [TestMethod]
        public void TestOutputs()
        {
            // Arrange
            double kineticEnergy = 1;
            double momentum = 2;
            double penetration = 3;
            double drag = 4;

            // Act
            Output output = new Output(kineticEnergy, momentum, penetration, drag);

            // Assert
            Assert.AreEqual("1", output.kineticEnergy);
            Assert.AreEqual("2", output.momentum);
            Assert.AreEqual("3", output.penetration);
            Assert.AreEqual("4", output.drag);
        }
    }
}
