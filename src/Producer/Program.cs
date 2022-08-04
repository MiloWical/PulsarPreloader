using Producer;

CliInput input = new CliInput
{
  Topic = args[0]
};

if(args.Length == 1)
  input.Message = new PipeReader().ProcessInput(Console.In.ReadToEnd());

else 
  input.Message = new ArgsReader().ProcessInput(args);

Console.WriteLine(input);