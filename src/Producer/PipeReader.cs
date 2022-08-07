using System.Text;

namespace Producer;

public class PipeReader : IInputReader<string>
{
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