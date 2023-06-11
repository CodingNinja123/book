namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public sealed class Configuration
    {
        private static Configuration instance = null;
        private static readonly object lockObject = new object();

        private Configuration() { }

        private void LoadData()
        {
            this.ASystemUploadUrl = "http://file.storage.com/systemA/upload";
            this.ASystemSearchApi = "http://systemA.com/api/search";
            this.ASystemStoreApi = "http://systemA.com/api/store";
            this.BSystemUploadUrl = "http://file.storage.com/systemB/upload";
            this.BSystemApi = "http://systemB.com/api";
            this.CSystemApi = "http://systemC.processing.com/api";
            this.DashboardLoggingUrl = "http://logging.url";
        }

        public static Configuration Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new Configuration();
                        instance.LoadData();
                    }
                    return instance;
                }
            }
        }

        public string DashboardLoggingUrl 
        { 
            get; private set; 
        }


        public string ASystemUploadUrl
        {
            get; private set;
        }

        public String ASystemSearchApi
        {
            get; private set;
        }
        public String ASystemStoreApi
        {
            get; private set;
        }

        public string BSystemApi
        {
            get; private set;
        }
        public string BSystemUploadUrl
        {
            get; private set;
        }
        public string CSystemApi
        {
            get; private set;
        }
    }
}
