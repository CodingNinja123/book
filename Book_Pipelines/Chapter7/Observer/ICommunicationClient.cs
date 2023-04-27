namespace Book_Pipelines.Chapter7.Observer
{
    public interface ICommunicationClient<TRequest, TResponse>
    {
        TResponse ExecuteRequest(TRequest request);
    }
}
