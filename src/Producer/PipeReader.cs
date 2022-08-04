using System.Text;

namespace Producer;

public class PipeReader : IInputReader<string>
{
  // public CliInput ProcessInput(string input)
  // {
  //   var cliInput = new CliInput();
  //   var payload = new StringBuilder();

  //   var i = 0;

  //   while(i < input.Length && char.IsWhiteSpace(input[i]))
  //   {
  //     i++;
  //   }
    
  //   while(i < input.Length && !char.IsWhiteSpace(input[i]))
  //   {
  //     payload.Append(input[i]);
  //     i++;
  //   }

  //   cliInput.Topic = payload.ToString();
  //   payload.Clear();

  //   while(i < input.Length && char.IsWhiteSpace(input[i]))
  //   {
  //     i++;
  //   }

  //   while(i < input.Length)
  //   {
  //     payload.Append(input[i]);
  //     i++;
  //   }

  //   cliInput.Message = payload.ToString();

  //   return cliInput;
  // }

  public string ProcessInput(string input)
  {
    var payload = new StringBuilder();

    var i = 0;

    while(i < input.Length && char.IsWhiteSpace(input[i]))
    {
      i++;
    }

    while(i < input.Length)
    {
      payload.Append(input[i]);
      i++;
    }

    return payload.ToString();
  }
}