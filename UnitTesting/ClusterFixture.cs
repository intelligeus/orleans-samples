using Orleans.TestingHost;
using Xunit;

namespace UnitTesting;

[CollectionDefinition(Name)]
public class ClusterCollection : ICollectionFixture<ClusterFixture>
{
    public const string Name = "ClusterCollection";
}

public class ClusterFixture : IDisposable
{
    public TestCluster Cluster { get;}

    public ClusterFixture()
    {
        var builder = new TestClusterBuilder();
        Cluster = builder.Build();
        Cluster.Deploy();
    }
    
    public void Dispose()
    {
        Cluster.StopAllSilos();
    }
}