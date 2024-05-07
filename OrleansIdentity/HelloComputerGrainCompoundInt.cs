namespace OrleansIdentity;

public class HelloComputerGrainCompoundInt : Grain, IHelloComputerCompoundInt
{

    public Task<string> HelloComputer(string name) => Task.FromResult($"Affirmative {name}, I read you. VIA Grain ID {this.GetGrainId()}");
    
}