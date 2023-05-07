namespace Book_Pipelines.Chapter7.Visitor
{
    public interface ICommunicationClient<TRequest, TResponse>
    {
        TResponse ExecuteRequest(TRequest request);
    }
}
