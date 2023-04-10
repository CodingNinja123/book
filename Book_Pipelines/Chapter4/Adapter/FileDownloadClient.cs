namespace Book_Pipelines.Chapter4.Adapter
{
    public class FileDownloadClient : ICommunicationClient<string, byte[]>
    {
        public async Task<byte[]> ExecuteRequest(string fileUrl)
        {
            Console.WriteLine($"Downloading file from  {fileUrl}.");
            return await Task.FromResult(new byte[0]);
        }
    }
}
