namespace Book_Pipelines.Chapter4.Adapter
{
    public interface ICommunicationClient<TRequest, TResponse>
    {
        Task<TResponse> ExecuteRequest(TRequest request);
    }
}
