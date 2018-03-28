using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FuzzyCore.Permissions;
using System.IO;

namespace FuzzyTest.Permissions
{
    [TestClass]
    public class Permission_Test
    {
        IpPermission IP_Perm = new IpPermission();
        MacPermission MAC_Perm = new MacPermission();

        [TestMethod]
        public void Test_IP_Perm()
        {
            IP_Perm.FilePath = "C:\\Users\\muham\\Documents\\Visual Studio 2015\\Projects\\TT\\TT\\bin\\Debug\\Permissions\\Ip.json";
            Assert.IsTrue(IP_Perm.FileControl());
            IP_Perm.TargetIP = "127.0.0.1";
            Assert.IsTrue(IP_Perm.PermissionControl());
            string mContent = File.ReadAllText(IP_Perm.FilePath);
            Assert.AreEqual(IP_Perm.FileContent.Length,mContent.Length);
        }

        [TestMethod]
        public void Test_MAC_Perm()
        {
            MAC_Perm.FilePath = "C:\\Users\\muham\\Documents\\Visual Studio 2015\\Projects\\TT\\TT\\bin\\Debug\\Permissions\\Mac.json";
            Assert.IsTrue(MAC_Perm.FileControl());
            MAC_Perm.MacObject = new MacPermission.PermissionMac() { MacAddress = "nope" };
            Assert.IsFalse(MAC_Perm.PermissionControl());
            string mContent = File.ReadAllText(MAC_Perm.FilePath);
            Assert.AreEqual(MAC_Perm.FileContent.Length, mContent.Length);
        }

    }
}
