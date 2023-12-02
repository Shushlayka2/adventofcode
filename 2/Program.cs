var sum = 0;
var line = "";

PrepareData();
using var streamReader = new StreamReader("input.txt");

while ((line = streamReader.ReadLine()) != null)
{
    var (firstNum, lastNum) = (-1, -1);

    for (int i = 0; i < line.Length; i++)
    {
        if (WordsMap.TryGetValue(line[i], out var words))
        {
            foreach (var word in words)
            {
                word.Apply(line[i]);
                
                if (word.IsFound())
                {
                    lastNum = word.NumRepresentation;
                }

                if (firstNum == -1)
                {
                    firstNum = lastNum;
                }
            }
        }
    }

    sum += firstNum * 10 + lastNum;
    Reset();
}

Console.WriteLine(sum);

public class Word
{
    public string Value { get; }

    public int NumRepresentation { get; }

    public int Pointer { get; private set; } = 0;

    public Word(string value, int numRepresentation)
    {
        Value = value;
        NumRepresentation = numRepresentation;
    }

    public void Apply(char letter)
    {
        if (Pointer == Value.Length)
            Reset();
    
        Pointer = Value[Pointer] == letter ? Pointer + 1
            : Value[0] == letter ? 1 : 0;
    }

    public bool IsFound()
    {
        return Pointer == Value.Length;
    }

    public void Reset()
    {
        Pointer = 0;
    }
} 

public partial class Program
{
    public static Word[] Words { get; } =
    { 
        new Word("0", 0), new Word("1", 1), new Word("2", 2), new Word("3", 3), new Word("4", 4),
        new Word("5", 5), new Word("6", 6), new Word("7", 7), new Word("8", 8), new Word("9", 9),
        new Word("zero", 0), new Word("one", 1), new Word("two", 2), new Word("three", 3), new Word("four", 4),
        new Word("five", 5), new Word("six", 6), new Word("seven", 7), new Word("eight", 8), new Word("nine", 9)
    };

    public static Dictionary<char, HashSet<Word>> WordsMap { get; } = new();

    public static void PrepareData()
    {
        foreach (var word in Words)
        {
            foreach (var letter in word.Value)
            {
                if (!WordsMap.TryGetValue(letter, out var words))
                {
                    WordsMap.Add(letter, words = new HashSet<Word>());
                }
                
                words.Add(word);
            }
        }
    }

    public static void Reset()
    {
        foreach (var words in WordsMap.Values)
        {
            foreach (var word in words)
            {
                word.Reset();
            }
        }
    }
}