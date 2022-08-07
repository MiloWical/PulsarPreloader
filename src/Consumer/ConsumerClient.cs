using System.Buffers;
using System.Text;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;

namespace Consumer;

public class ConsumerClient
{
  public async static Task ConsumeMessagesAsync(ConsumerCliInput cliInput)
  {
    var client = PulsarClient.Builder()
      .ServiceUrl(new Uri($"pulsar://{cliInput.Host}"))
      .Build();

    var consumer = client.NewConsumer()
      .Topic(cliInput.Topic!)
      .SubscriptionName(cliInput.Subscription!)
      .InitialPosition(SubscriptionInitialPosition.Earliest)
      .Create();
    
    if(cliInput.MessageCount >= 0)
      await FiniteReadAsync(consumer, cliInput.MessageCount);
    else
      await InfiniteReadAsync(consumer);
    
  }

  private static async Task FiniteReadAsync(IConsumer<ReadOnlySequence<byte>> consumer, int messageCount)
  {
    var messages = consumer.Messages().GetAsyncEnumerator();

    if(null == messages.Current)
      await messages.MoveNextAsync();

    for(var i = 0; i < messageCount; i++)
    {
      Console.Write($"[{i+1}] ");
      await ProcessMessageAsync(consumer, messages.Current!);
    }
  }

  private static async Task InfiniteReadAsync(IConsumer<ReadOnlySequence<byte>> consumer)
  {
    await foreach (var message in consumer.Messages())
    {
      await ProcessMessageAsync(consumer, message);
    }
  }

  private static async Task ProcessMessageAsync(IConsumer<ReadOnlySequence<byte>> consumer, IMessage<ReadOnlySequence<byte>> message)
  {
    Console.WriteLine("Received Message: " + Encoding.UTF8.GetString(message.Data.ToArray()));
    await consumer.Acknowledge(message);
  }
}
