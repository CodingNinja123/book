namespace Book_Pipelines.Chapter6.Strategy
{
    public interface ICommunicationClient<TRequest, TResponse>
    {
        TResponse ExecuteRequest(TRequest request);
    }
}
