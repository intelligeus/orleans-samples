namespace OrleansStreaming;

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
    
    Task UnsubscribeFromStream();

    Task<int> GetNumberConsumed();
}