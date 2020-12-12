namespace OkMusic.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMusicRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetMusic(string fileName);
    }
}