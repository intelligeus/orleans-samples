using System.Reflection;
using Orleans.Concurrency;
using Orleans.Serialization.Invocation;

namespace ReentrantGrains;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class InterleaveAttribute : Attribute { }

// Specify the may-interleave predicate.
[MayInterleave(nameof(MayInterleave))]
public class MayInterleaveGrain : Grain, IMayInterleaveGrain
{
    public static bool MayInterleave(IInvokable req)
    {
        Console.WriteLine($"May Interleave called with result {req.GetArgumentCount() == 1 && (string)req.GetArgument(0) == "interleave"}");
        // Return true if the call can be interleaved
        return req.GetArgumentCount() == 1
               && (string)req.GetArgument(0) == "interleave";
    }
    

    public async Task DoWork(string arg)
    {
        
        await Task.Delay(TimeSpan.FromSeconds(5)); 
    }
}