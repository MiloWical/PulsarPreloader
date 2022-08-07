namespace Producer;

public class ProducerCliInput
{
  public string? Host { get; set; }
  public string? Topic { get; set; }
  public string? Message { get; set; }

  public override string ToString()
  {
    return $"Host: {Host}\nTopic: {Topic}\n\nMessage:\n--------\n{Message}";
  }
}