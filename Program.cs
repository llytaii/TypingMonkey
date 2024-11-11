int delayNextLetter = 200;

if (args.Any() && int.TryParse(args[0], out int val))
    delayNextLetter = val;

foreach (char c in GenerateNextLetter())
{
    Console.Write(c);
    await Task.Delay(delayNextLetter);
}

// Generator
IEnumerable<char> GenerateNextLetter()
{
    string alphabet = "abcdefghijklmnopqrstuvwxyz";
    Random random = new Random();

    while (true)
        yield return alphabet[random.Next(alphabet.Length)];
}
