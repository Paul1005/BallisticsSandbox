using System;
using System.IO;
using BallisticsSandbox;
using BallisticsSandbox.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace TestStackWhiteTests
{
    [TestClass]
    public class UITests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Calculator calculator = new Calculator();
            var applicationPath = "BallisticsSandbox.exe";
            Application application = Application.Launch(applicationPath);
            Window ballisticsSandbox = application.GetWindow("BallisticsSandbox", InitializeOption.NoCache);

            TextBox velocity = ballisticsSandbox.Get<TextBox>("Velocity");
            velocity.Text = "833";

            TextBox weight = ballisticsSandbox.Get<TextBox>("Weight");
            weight.Text = "10";

            TextBox diameter = ballisticsSandbox.Get<TextBox>("Diameter");
            diameter.Text = "7.62";

            TextBox gravity = ballisticsSandbox.Get<TextBox>("Gravity");
            gravity.Text = "9.8";

            TextBox airDensity = ballisticsSandbox.Get<TextBox>("AirDensity");
            airDensity.Text = "1";

            TextBox dragCoefficient = ballisticsSandbox.Get<TextBox>("DragCoefficient");
            dragCoefficient.Text = "0.3";

            TextBox angle = ballisticsSandbox.Get<TextBox>("Angle");
            angle.Text = "25";

            Button calculate = ballisticsSandbox.Get<Button>("Calculate");
            calculate.Click();

            TextBox terminalVelocity = ballisticsSandbox.Get<TextBox>("TerminalVelocity");
            Assert.AreEqual(calculator.CalculateTerminalVelocity(10, 9.8, 0.3, 1, calculator.CalculateArea(7.62)).ToString(), terminalVelocity.Text);

            TextBox range = ballisticsSandbox.Get<TextBox>("Range");
            Assert.AreEqual(calculator.CalculateRange(833, 9.8, 25 * (Math.PI / 180), Double.Parse(terminalVelocity.Text)).ToString(), range.Text);

            TextBox flightTime = ballisticsSandbox.Get<TextBox>("FlightTime");
            Assert.AreEqual(calculator.CalculateTimeInFlight(833, 25 * (Math.PI / 180), 9.8, Double.Parse(terminalVelocity.Text)).ToString(), flightTime.Text);  

            TextBox recoil = ballisticsSandbox.Get<TextBox>("Recoil");
            Assert.AreEqual(calculator.CalculateMomentum(10, 833).ToString(), recoil.Text); 

            Button back = ballisticsSandbox.Get<Button>("Back");
            back.Click();

            velocity = ballisticsSandbox.Get<TextBox>("Velocity");
            weight = ballisticsSandbox.Get<TextBox>("Weight");
            diameter = ballisticsSandbox.Get<TextBox>("Diameter");
            gravity = ballisticsSandbox.Get<TextBox>("Gravity");
            airDensity = ballisticsSandbox.Get<TextBox>("AirDensity");
            dragCoefficient = ballisticsSandbox.Get<TextBox>("DragCoefficient");
            angle = ballisticsSandbox.Get<TextBox>("Angle");

            Assert.AreEqual("833", velocity.Text);
            Assert.AreEqual("10", weight.Text);
            Assert.AreEqual("7.62", diameter.Text);
            Assert.AreEqual("9.8", gravity.Text);
            Assert.AreEqual("1", airDensity.Text);
            Assert.AreEqual("0.3", dragCoefficient.Text);
            Assert.AreEqual("25", angle.Text);
        }
    }
}
