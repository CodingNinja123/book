using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Book_Pipelines.Chapter_2.AbstractFactoryNM
{
    public class TypeAPipeline : UploadFilePipeleine
    {
        private BaseUploadEvent data = null;
        private string targetSystemUploadUrl = "http://file.storage.test/systemA/upload";
        private string targetSystemApiUrl = "http://systemA.test/api";

        protected override object Preprocess(BasicEvent basicEvent)
        {
            this.data = basicEvent as BaseUploadEvent;
            Notify(basicEvent, "Preprocessing event");
            Notify(basicEvent, $"Downloading file {this.data.FileName} from {this.data.FileUrl}");
            return this.data;
        }

        protected override void ProcessEvent(BasicEvent basicEvent, object preprocessingResult)
        {
            Notify(basicEvent, "Processing event");
            Notify(basicEvent, $"Transfering file {this.data.FileName} downloaded from {this.data.FileUrl} to {this.targetSystemUploadUrl}");
        }

        protected override object Search(BasicEvent basicEvent)
        {
            Notify(basicEvent, "Searching event in the target system");
            Notify(basicEvent, $"Calling {this.targetSystemApiUrl} to search for {this.data.FileName}");
            return null;
        }

        protected override void Store(BasicEvent basicEvent, object existingObject)
        {
            Notify(basicEvent, "Storing event in the target system");
            Notify(basicEvent, $"Calling {this.targetSystemApiUrl} api to save the data about transfered file");
        }
    }
}
