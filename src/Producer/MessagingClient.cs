using System.Text;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;

namespace Producer;

public class MessagingClient
{
  static IPulsarClient client = PulsarClient.Builder().Build();
  public static async Task SendMessageAsync(CliInput input)
  {
    var producer = client.NewProducer()
                     .Topic(input.Topic!)
                     .Create();

    await producer.Send(Encoding.UTF8.GetBytes(input.Message!));                  
  }   
}
