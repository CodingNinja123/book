namespace Book_Pipelines.Chapter3.Prototype
{
    public sealed class Configuration
    {
        private static Configuration instance = null;
        private static readonly object lockObject = new object();

        private Configuration() { }

        private void LoadData()
        {
            this.TargetASystemUploadUrl = "http://file.storage.test/systemA/upload";
            this.TargetASystemApiUrl = "http://systemA.test/api";
            this.TargetBSystemUploadUrl = "http://file.storage.test/systemB/upload";
            this.TargetBSystemApiUrl = "http://systemB.test/api";
            this.TargetCSystemApiUrl = "http://systemC.test/api";
            this.TargetCSystemProcessingApiUrl = "http://systemC.processing.test/api";
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
        public String TargetASystemApiUrl
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
