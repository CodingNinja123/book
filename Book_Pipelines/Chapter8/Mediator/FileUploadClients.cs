using System.Text.Json;

namespace Book_Pipelines.Chapter8.Mediator
{
    public class FileUploadClient : ICommunicationClient<UploadFileInfo, int>
    {
        private string baseUrl = string.Empty;

        public FileUploadClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public int ExecuteRequest(UploadFileInfo data)
        {
            var token = TokenFactory.GetToken(SystemType.SystemUpload);

            string jsonString = JsonSerializer.Serialize(data);
            Console.WriteLine($"FILE_UPLOAD_CLIENT: Uploading file to {this.baseUrl}. File: {jsonString}");
            return 0;
        }
    }
}
