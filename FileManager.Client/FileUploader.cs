namespace FileManager.Client
{
    using Minio.DataModel.Args;
    using Minio;

    public sealed class FileUploader:IFileUploader
    {
        private readonly IMinioClient _minioClient;

        public FileUploader(string endpoint, string accessKey, string secretKey)
        {
            _minioClient = new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .WithSSL(true)
                .Build();
        }

        public async Task UploadFileAsync(string bucketName, string objectName, Stream fileStream, long fileSize, string contentType)
        {
            bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (!found)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
            }

            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileSize)
                .WithContentType(contentType));
        }
    }
}
