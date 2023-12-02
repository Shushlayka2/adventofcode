using var streamReader = new StreamReader("input.txt");
var line = "";
var sum = 0;

while ((line = streamReader.ReadLine()) != null)
{
    sum += GetCalibrationValue(line);
}

Console.WriteLine(sum);

public partial class Program
{

    private static int GetCalibrationValue(string inputLine)
    {
        var firstNum = inputLine.FirstOrDefault(ch => char.IsDigit(ch)) - '0';
        var lastNum = inputLine.LastOrDefault(ch => char.IsDigit(ch)) - '0';

        return firstNum * 10 + lastNum;
    }
}
