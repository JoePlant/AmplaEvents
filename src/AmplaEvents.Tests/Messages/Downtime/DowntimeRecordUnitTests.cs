using System;
using System.Diagnostics;
using AmplaEvents.RecordChanged.Downtime;
using NUnit.Framework;

namespace AmplaEvents.Messages.Downtime
{
    [TestFixture]
    public class DowntimeRecordUnitTests : TestFixture
    {
        private int recordId = 100;
        private const string location = "Enterprise.Site.Area.Downtime";
        private const string module = "Downtime";

        protected override void OnSetUp()
        {
            recordId++;
            base.OnSetUp();
        }

        [Test]
        public void ModulePropertySet()
        {
            DowntimeRecord record = new DowntimeRecord(location, recordId);
            Assert.That(record.Location, Is.EqualTo(location));
            Assert.That(record.Module, Is.EqualTo(module));
            Assert.That(record.RecordId, Is.EqualTo(recordId));
        }
    }
}