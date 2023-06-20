using System.Text.Json;

namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class FileUploadClient : IFileUploadClient
    {
        private string baseUrl = string.Empty;
        private readonly ITokenFactory tokenFactory;

        public FileUploadClient(string baseUrl, ITokenFactory tokenFactory)
        {
            this.baseUrl = baseUrl;
            this.tokenFactory = tokenFactory;
        }

        public int ExecuteRequest(UploadFileInfo data)
        {
            var token = tokenFactory.GetToken(SystemType.SystemUpload);

            string jsonString = JsonSerializer.Serialize(data);
            Console.WriteLine($"FILE_UPLOAD_CLIENT: Uploading file to {this.baseUrl}. File: {jsonString}");
            return 0;
        }
    }


    public class FileAUploadClient: FileUploadClient, IFileAUploadClient
    {
        public FileAUploadClient(string baseUrl, ITokenFactory tokenFactory): base(baseUrl, tokenFactory)
        {

        }
    }

    public class FileBUploadClient : FileUploadClient, IFileBUploadClient
    {
        public FileBUploadClient(string baseUrl, ITokenFactory tokenFactory) : base(baseUrl, tokenFactory)
        {

        }
    }
}
