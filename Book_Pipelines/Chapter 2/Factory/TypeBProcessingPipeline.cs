using System.Text.Json;

namespace Book_Pipelines.Chapter_2.Factory
{
    public class TypeBProcessingPipeline : AbstractPipeline
    {
        private EventTypeBData data = null;
        private string targetSystemUploadUrl = "http://file.storage.test/systemB/upload";
        private string targetSystemApiUrl = "http://systemB.test/api";

        protected override object Preprocess(BasicEvent basicEvent)
        {
            this.data = JsonSerializer.Deserialize<EventTypeBData>(basicEvent.Data) ?? new EventTypeBData();
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
