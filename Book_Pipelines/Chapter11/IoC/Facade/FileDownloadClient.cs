namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public class FileDownloadClient : IFileDownloadClient
    {
        private readonly ITokenFactory tokenFactory;

        public FileDownloadClient(ITokenFactory tokenFactory)
        {
            this.tokenFactory = tokenFactory;
        }
        public byte[] ExecuteRequest(string fileUrl)
        {
            var token = tokenFactory.GetToken(SystemType.SystemDownload);

            Console.WriteLine($"DOWNLOAD_CLIENT: Downloading file from  {fileUrl}.");
            return new byte[0];
        }
    }
}
