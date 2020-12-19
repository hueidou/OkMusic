using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OkMusic.Domain;
using OkMusic.Models;

namespace OkMusic.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class MusicFileRepository
    {
        private readonly ILogger _logger;
        private readonly Amazon.S3.IAmazonS3 _s3;
        private readonly string bucketName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="s3"></param>
        public MusicFileRepository(ILogger<MusicFileRepository> logger,
            IConfiguration configuration,
            Amazon.S3.IAmazonS3 s3)
        {
            _logger = logger;
            _s3 = s3;

            bucketName = configuration["S3:BucketName"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        public async Task Add(string fileName, Stream data, string contentType)
        {
            var putObjectRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = fileName,
                ContentType = contentType,
                InputStream = data
            };
            await _s3.PutObjectAsync(putObjectRequest);
        }
    }
}