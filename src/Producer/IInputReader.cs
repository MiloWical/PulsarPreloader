namespace Producer;

public interface IInputReader<TInput>
{
  public string ProcessInput(TInput input);    
}