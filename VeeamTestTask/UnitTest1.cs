using System.Diagnostics;
using VeeamTestConsoleApp;

namespace VeeamTestTask
{
    public class Tests
    {

        private FindAndKillProcess FindAndKillProcess { get; set; } = null!;

        [SetUp]
        public void Setup()
        {
            FindAndKillProcess = new FindAndKillProcess();
        }

        [Test]
        public void Test1()
        {
            // Assign
            string name = "notepad";
            Process.Start(name); //start process to validate test
            Thread.Sleep(5000);
            string lifetime = "0";
            //var frequency = 5;
            List<int> expected = new List<int>() { 0, 0 };
            List<int> actual = FindAndKillProcess.DoFindAndKillProcess(name, lifetime);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));

        }
    }
}