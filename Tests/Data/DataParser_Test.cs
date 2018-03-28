using Microsoft.VisualStudio.TestTools.UnitTesting;
using FuzzyCore.Data;
using System.Net.Sockets;

namespace FuzzyTest.Data
{
    [TestClass]
    public class DataParser_Test
    {
        [TestMethod]
        public void DataTransfer_Test()
        {
            FuzzyCore.Initialize.InitType m_initializeType = new FuzzyCore.Initialize.InitType();
            m_initializeType.ServerProp = new FuzzyCore.Initialize.Props();
            m_initializeType.ServerProp.TYPE = FuzzyCore.Initialize.Props.ServerType.DATATRANFSER;
            string Data = "{\"CommandType\" : \"get_folder_list\"}";
            var CurrentSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            DataParser parser = new DataParser(Data, CurrentSocket, m_initializeType);
            parser.Parse();

            Assert.AreEqual(true, FuzzyCore.Commands.GetFolderList.Test_StackBoolean);
        }
        [TestMethod]
        public void Remoting_Test()
        {
            FuzzyCore.Initialize.InitType m_initializeType = new FuzzyCore.Initialize.InitType();
            m_initializeType.ServerProp = new FuzzyCore.Initialize.Props();
            m_initializeType.ServerProp.TYPE = FuzzyCore.Initialize.Props.ServerType.REMOTING;
            string Data = "{\"CommandType\" : \"print_message\",\"Text\" : \"Hello World\"}";
            var CurrentSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            DataParser parser = new DataParser(Data, CurrentSocket, m_initializeType);
            parser.Parse();

            Assert.AreEqual(true, FuzzyCore.Commands.PrintMessage.Test_StackBoolean);
        }
    }
}
