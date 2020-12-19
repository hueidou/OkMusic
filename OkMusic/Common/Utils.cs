using System;
using System.IO;

namespace OkMusic.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class StreamFileAbstraction : TagLib.File.IFileAbstraction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Name {get;set;}

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Stream ReadStream {get;set;}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Stream WriteStream => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void CloseStream(Stream stream)
        {
            ReadStream.Close();
        }
    }
}