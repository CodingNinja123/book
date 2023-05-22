namespace Book_Pipelines.Chapter8.Command
{
    public interface ICommunicationClient<TRequest, TResponse>
    {
        TResponse ExecuteRequest(TRequest request);
    }
}
