// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Net;
using Microsoft.Extensions.FileProviders;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;

namespace Evorine.FileSystem.S3FileProvider
{
    public class S3FileInfo : IFileInfo
    {
        private readonly IAmazonS3 amazonS3;
        private readonly string bucketName;
        private readonly string key;

        private GetObjectResponse fileObject;
        private bool? exists;

        public S3FileInfo(IAmazonS3 amazonS3, string bucketName, string key)
        {
            this.amazonS3 = amazonS3;
            this.bucketName = bucketName;
            this.key = key;
        }

        private GetObjectResponse getfileObject()
        {
            if (fileObject == null)
            {
                fileObject = amazonS3.GetObjectAsync(bucketName, key).Result;
            }
            return fileObject;
        }


        public string MD5 => getfileObject().Metadata["Content-MD5"];

        public bool Exists
        {
            get
            {
                if (!exists.HasValue)
                {
                    try
                    {
                        getfileObject();
                        exists = true;
                    }
                    catch (AmazonS3Exception e)
                    {
                        if (e.StatusCode == HttpStatusCode.NotFound) exists = false;
                        throw;
                    }
                }
                return exists.Value;
            }
        }



        public long Length => getfileObject().ContentLength;


        /// <summary>
        /// A http url to the file, including the file name.
        /// </summary>
        public string PhysicalPath => null;

        public string Name => Path.GetFileName(getfileObject().Key.TrimEnd('/'));

        public DateTimeOffset LastModified => getfileObject().LastModified;

        public bool IsDirectory => getfileObject().Key.EndsWith("/");

        public Stream CreateReadStream()
        {
            return AmazonS3Util.MakeStreamSeekable(getfileObject().ResponseStream);
        }
    }
}
