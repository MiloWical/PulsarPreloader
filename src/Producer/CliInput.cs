namespace Producer;

public class CliInput
{
  public string? Topic { get; set; }
  public string? Message { get; set; }

  public override string ToString()
  {
    return $"Topic: {Topic}\n\nMessage:\n--------\n{Message}";
  }
}