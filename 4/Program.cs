using System.Text.RegularExpressions;

var sum = 0;
var line = "";
var pattern = @"Game (\d+): (.*)";

using var streamReader = new StreamReader("input.txt");

while ((line = streamReader.ReadLine()) != null)
{
    var cubesMap = new Dictionary<string, int>
    {
        ["red"] = 0, ["green"] = 0, ["blue"] = 0
    };

    var match = Regex.Match(line, pattern);
    var gameNumber = int.Parse(match.Groups[1].Value);
    var grabs = match.Groups[2].Value.Split(";");

    foreach (var grab in grabs)
    {
        var parts = grab.Split(',').Select(grab => grab.Trim());
        var parsedParts = parts
            .Select(part => part.Split(' '))
            .Where(group => group.Length == 2)
            .Select(group => (int.Parse(group[0]), group[1]));

        foreach (var (count, color) in parsedParts)
            if (cubesMap.TryGetValue(color, out var minCount) && count > minCount)
                cubesMap[color] = count;
    }

    sum += cubesMap.Values.Aggregate((cur, next) => cur * next);
}

Console.WriteLine(sum);