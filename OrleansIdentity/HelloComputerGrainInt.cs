namespace OrleansIdentity;

public class HelloComputerGrainInt : Grain, IHelloComputerInt
{

    public Task<string> HelloComputer(string name) => Task.FromResult($"Affirmative {name}, I read you. VIA Grain ID {this.GetGrainId()}");
    
}