using AmplaEvents.RecordChanged;
using AmplaEvents.RecordChanged.Production;
using NUnit.Framework;

namespace AmplaEvents.Messages.Production
{
    [TestFixture]
    public class ProductionRecordUnitTests : TestFixture
    {
        private int recordId = 100;
        private const string location = "Enterprise.Site.Area.Production";
        private const string module = "Production";

        protected override void OnSetUp()
        {
            recordId++;
            base.OnSetUp();
        }

        [Test]
        public void ModulePropertySet()
        {
            IModuleRecord moduleRecord = new ProductionRecord(location,  recordId);
            Assert.That(moduleRecord, Is.Not.Null);
            Assert.That(moduleRecord.Location, Is.EqualTo(location));
            Assert.That(moduleRecord.Module, Is.EqualTo(module));
            Assert.That(moduleRecord.RecordId, Is.EqualTo(recordId));
        }
    }
}
