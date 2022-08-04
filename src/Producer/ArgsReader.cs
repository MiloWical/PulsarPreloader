using System.Text;

namespace Producer;
public class ArgsReader : IInputReader<string[]>
{
  // public CliInput ProcessInput(string[] input)
  // {
  //   var payload = new StringBuilder();

  //   payload.AppendJoin(' ', input[1..]);

  //   return new CliInput {
  //     Topic = input[0],
  //     Message = payload.ToString()
  //   };
  // }

  public string ProcessInput(string[] input)
  {
    var payload = new StringBuilder();

    payload.AppendJoin(' ', input[1..]);

    return payload.ToString();
  }
}