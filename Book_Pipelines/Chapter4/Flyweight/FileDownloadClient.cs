namespace Book_Pipelines.Chapter4.Flyweight
{
    public class FileDownloadClient : ICommunicationClient<string, byte[]>
    {
        public async Task<byte[]> ExecuteRequest(string fileUrl)
        {
            var token = TokenFactory.GetToken(SystemType.SystemDownload);
            Console.WriteLine($"Token received {token.TokenValue}");

            Console.WriteLine($"Downloading file from  {fileUrl}.");
            return await Task.FromResult(new byte[0]);
        }
    }
}
