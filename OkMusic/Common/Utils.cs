using System;
using System.IO;

namespace OkMusic.Common
{
    public class Utils
    {
    }

    public class StreamFileAbstraction : TagLib.File.IFileAbstraction
    {

        public string Name {get;set;}

        public Stream ReadStream {get;set;}

        public Stream WriteStream => throw new NotImplementedException();

        public void CloseStream(Stream stream)
        {
            ReadStream.Close();
        }
    }
}