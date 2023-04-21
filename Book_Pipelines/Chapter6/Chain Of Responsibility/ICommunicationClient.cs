namespace Book_Pipelines.Chapter6.ChainOfResponsibility
{
    public interface ICommunicationClient<TRequest, TResponse>
    {
        TResponse ExecuteRequest(TRequest request);
    }
}
