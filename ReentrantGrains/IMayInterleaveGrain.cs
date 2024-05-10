namespace ReentrantGrains;

public interface IMayInterleaveGrain : IGrainWithStringKey
{
    Task DoWork(string arg);
    
    
}