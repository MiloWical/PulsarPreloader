using System.Text;
using DotPulsar;
using DotPulsar.Extensions;

namespace Producer;

public class MessagingClient
{
  public static async Task SendMessageAsync(ProducerCliInput input)
  {
    var client = PulsarClient.Builder()
      .ServiceUrl(new Uri($"pulsar://{input.Host}"))
      .Build();

    var producer = client.NewProducer()
                     .Topic(input.Topic!)
                     .Create();

    await producer.Send(Encoding.UTF8.GetBytes(input.Message!));                  
  }   
}
