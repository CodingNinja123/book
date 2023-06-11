namespace Book_Pipelines.Chapter11.IoC.Facade
{
    public interface ICommunicationClient<TRequest, TResponse>
    {
        TResponse ExecuteRequest(TRequest request);
    }
}
