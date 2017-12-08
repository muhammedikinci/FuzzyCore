using FuzzyCore.Data;
using FuzzyCore.Server;
using System;
using System.IO;

namespace FuzzyCore.Commands
{
    public class GetFile
    {
        ConsoleMessage Message = new ConsoleMessage();
        private String FilePath;
        private String FileName;
        private JsonCommand mCommand;
        public GetFile(Data.JsonCommand Command)
        {
            FilePath = Command.FilePath;
            FileName = Command.Text;
            this.mCommand = Command;
        }
        bool FileControl()
        {
            FileInfo mfileInfo = new FileInfo(FilePath);
            return mfileInfo.Exists;
        }
        public byte[] GetFileBytes()
        {
            if (FileControl())
            {
                byte[] file = File.ReadAllBytes(FilePath + "/" + FileName);
                return file;
            }
            return new byte[0];
        }
        public string GetFileText()
        {
            if (FileControl())
            {
                return File.ReadAllText(FilePath + "/" + FileName);
            }
            return "";
        }
    }
}
