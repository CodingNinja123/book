namespace Book_Pipelines.Chapter4.Flyweight
{
    public interface ICommunicationClient<TRequest, TResponse>
    {
        Task<TResponse> ExecuteRequest(TRequest request);
    }
}
