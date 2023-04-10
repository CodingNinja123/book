namespace Book_Pipelines.Chapter4.Composite
{
    public interface ICommunicationClient<TRequest, TResponse>
    {
        Task<TResponse> ExecuteRequest(TRequest request);
    }
}
