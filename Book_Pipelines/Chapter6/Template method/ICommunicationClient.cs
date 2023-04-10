namespace Book_Pipelines.Chapter5.TemplateMethod
{
    public interface ICommunicationClient<TRequest, TResponse>
    {
        TResponse ExecuteRequest(TRequest request);
    }
}
