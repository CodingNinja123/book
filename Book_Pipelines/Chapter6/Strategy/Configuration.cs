namespace Book_Pipelines.Chapter6.Strategy
{
    public sealed class Configuration
    {
        private static Configuration instance = null;
        private static readonly object lockObject = new object();

        private Configuration() { }

        private void LoadData()
        {
            this.ASystemUploadUrl = "http://file.storage.test/systemA/upload";
            this.ASystemSearchApi = "http://systemA.test/api/search";
            this.ASystemStoreApi = "http://systemA.test/api/store";
            this.BSystemUploadUrl = "http://file.storage.test/systemB/upload";
            this.BSystemApi = "http://systemB.test/api";
            this.CSystemApi = "http://systemC.processing.test/api";
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
