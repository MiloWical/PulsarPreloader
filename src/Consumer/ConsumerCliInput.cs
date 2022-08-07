namespace Consumer;

public class ConsumerCliInput
{
  public string? Host { get; set; }
  public string? Topic { get; set; }
  public string? Subscription { get; set; }

  public int MessageCount { get; set; } = -1;

  public override string ToString()
  {
    return $"Host: {Host}\nTopic: {Topic}\nSubscription: {Subscription}";
  }
}