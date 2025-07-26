namespace FileManager.Client
{
    public interface IFileUploader
    {
        Task UploadFileAsync(string bucketName, string objectName, Stream fileStream, long fileSize, string contentType);
    }
}
