namespace Book_Pipelines.Chapter4.Flyweight
{
    public abstract class AbstractPipeline
    {
        public abstract void Process(IBasicEvent basicEvent);
        protected virtual void Notify(IIoTEventData badicEvent, string message)
        {
            Console.WriteLine($"Processing pipeline: {message}: {badicEvent.Id}");
        }
    }
}
