namespace UnitTesting;

public interface IStatefulActivationGrain : IGrainWithIntegerKey
{
    Task<int> GetActivationValue();

    Task<int> GetStateValue();
}

public sealed class StatefulActivationGrain : Grain<StatefulActivationGrainState>, IStatefulActivationGrain
{
    private int _activationValue;

    public Task<int> GetActivationValue() => Task.FromResult(_activationValue);

    public Task<int> GetStateValue() => Task.FromResult(State.Value);

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _activationValue = this.State.Value;
        return Task.CompletedTask;
    }
}

public sealed class StatefulActivationGrainState
{
    public int Value { get; set; } = 123;
}

public class HelloTestKitGrain: Grain, IHelloComputer
{
    
    public int ActivateCount { get; set; }

    public int DeactivateCount { get; set; }

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        ActivateCount++;
        return base.OnActivateAsync(cancellationToken);
    }

    public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
    {
        DeactivateCount++;
        return base.OnDeactivateAsync(reason, cancellationToken);
    }

    public Task<string> HelloComputer(string name)=> Task.FromResult($"Affirmative {name}, I read you.");
}