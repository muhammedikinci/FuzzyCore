using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace FuzzyTest.Data
{
    [TestClass]
    public class DataSerializer_Test
    {
        [TestMethod]
        public void Serializer_Test_FullStream()
        {
            FuzzyCore.Data.DataSerializer Serializer = new FuzzyCore.Data.DataSerializer();
            string serializeExport = Serializer.Serialize("{\"CommandType\" : \"open_program\"}");
            Assert.AreEqual(serializeExport, "{\"CommandType\" : \"open_program\"}");
        }
        [TestMethod]
        public void Serilizer_Test_ParsedStream()
        {
            FuzzyCore.Data.DataSerializer Serializer = new FuzzyCore.Data.DataSerializer();
            string serializeExport = Serializer.Serialize("{\"CommandType\" : \"open");
            string serializeExport2 = "";
            if (serializeExport == "WAIT_NEXT_DATA")
            {
                serializeExport2 = Serializer.Serialize("_program\"}");
            }
            Assert.AreEqual(serializeExport2, "{\"CommandType\" : \"open_program\"}");
        }
    }
}
