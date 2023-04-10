namespace Book_Pipelines.Chapter5.Facade
{
    public class FileDownloadClient : ICommunicationClient<string, byte[]>
    {
        public byte[] ExecuteRequest(string fileUrl)
        {
            var token = TokenFactory.GetToken(SystemType.SystemDownload);

            Console.WriteLine($"DOWNLOAD_CLIENT: Downloading file from  {fileUrl}.");
            return new byte[0];
        }
    }
}
