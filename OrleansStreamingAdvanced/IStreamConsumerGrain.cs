namespace OrleansStreamingAdvanced
{
    public static class StreamConst
    {
        public const string ProviderName = "StreamExample";
        public const string BatchingNameSpace = "batching";
        public const string NonBatchingNameSpace = "nonbatching";
    }


    [GenerateSerializer]
    public class ConsumptionReport
    {
        [Id(0)]
        public int Consumed { get; set; }

        [Id(1)]
        public int MaxBatchSize { get; set; }
    }

    public interface IStreamConsumerGrain : IGrainWithGuidKey
    {
        Task<ConsumptionReport> GetConsumptionReport();
    
        Task SubscribeToStream(Guid streamId, string streamNamespace, string providerToUse);

        Task UnsubscribeFromStream();

        Task<int> GetNumberConsumed();
    }
}