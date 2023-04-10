using System.Text.Json;

namespace Book_Pipelines.Chapter4.Adapter
{
    public class FileUploadClient : ICommunicationClient<UploadFileInfo, int>
    {
        private string baseUrl = string.Empty;

        public FileUploadClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<int> ExecuteRequest(UploadFileInfo data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            Console.WriteLine($"Uploading file to {this.baseUrl}. File: {jsonString}");
            return await Task.FromResult(0);
        }
    }
}
