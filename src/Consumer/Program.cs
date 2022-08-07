using Consumer;

var cliInput = new ConsumerCliInput 
{
  Host = args[0],
  Topic = args[1],
  Subscription = args[2],
};

if(args.Length == 4 && int.TryParse(args[3], out var messageCount))
  cliInput.MessageCount = messageCount;

await ConsumerClient.ConsumeMessagesAsync(cliInput);