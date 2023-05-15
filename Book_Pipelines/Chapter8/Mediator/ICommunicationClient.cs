namespace Book_Pipelines.Chapter8.Mediator
{
    public interface ICommunicationClient<TRequest, TResponse>
    {
        TResponse ExecuteRequest(TRequest request);
    }
}
