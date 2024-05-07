namespace OrleansIdentity;

public class HelloComputerGrain : Grain, IHelloComputer
{

    public Task<string> HelloComputer(string name) => Task.FromResult($"Affirmative {name}, I read you. VIA Grain ID {this.GetGrainId()}");
    
}