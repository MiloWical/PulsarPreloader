using Producer;

ProducerCliInput input = new ProducerCliInput
{
  Host = args[0],
  Topic = args[1]
};

if(args.Length == 2)
  input.Message = new PipeReader().ProcessInput(Console.In.ReadToEnd());

else 
  input.Message = new ArgsReader().ProcessInput(args);

await MessagingClient.SendMessageAsync(input);