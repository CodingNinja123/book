namespace Book_Pipelines.Chapter4.Adapter
{
    public sealed class Configuration
    {
        private static Configuration instance = null;
        private static readonly object lockObject = new object();

        private Configuration() { }

        private void LoadData()
        {
            this.TargetASystemUploadUrl = "http://file.storage.com/systemA/upload";
            this.TargetASystemSearchApiUrl = "http://systemA.com/api/search";
            this.TargetASystemStoreApiUrl = "http://systemA.com/api/store";
            this.TargetBSystemUploadUrl = "http://file.storage.com/systemB/upload";
            this.TargetBSystemApiUrl = "http://systemB.com/api";
            this.TargetCSystemApiUrl = "http://systemC.com/api";
            this.TargetCSystemProcessingApiUrl = "http://systemC.processing.com/api";
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

        public string TargetASystemUploadUrl
        {
            get; private set;
        }

        public String TargetASystemSearchApiUrl
        {
            get; private set;
        }
        public String TargetASystemStoreApiUrl
        {
            get; private set;
        }

        public string TargetBSystemApiUrl
        {
            get; private set;
        }
        public string TargetBSystemUploadUrl
        {
            get; private set;
        }

        public string TargetCSystemApiUrl
        {
            get; private set;
        }

        public string TargetCSystemProcessingApiUrl
        {
            get; private set;
        }
    }
}
