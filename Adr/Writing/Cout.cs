using System.Text.RegularExpressions;
using Spectre.Console;

namespace Adr;

public static partial class Cout
{
    private static readonly Dictionary<Situation, string> SituationColors = new()
    {
        { Situation.Information, "white" },
        { Situation.Success, "green" },
        { Situation.Warning, "yellow" },
        { Situation.Error, "red" },
        { Situation.NumericValue, "orange3" },
        { Situation.Choice, "green" },
        { Situation.InputLabel, "royalblue1" }
    };

    public enum Situation
    {
        Information,
        Success,
        Warning,
        Error,
        NumericValue,
        Choice,
        InputLabel
    }

    public static void Hr(string title = "")
    {
        if (string.IsNullOrEmpty(title))
        {
            AnsiConsole.Write(new Rule());
            return;
        }

        AnsiConsole.Write(new Rule(title).LeftJustified());
    }

    public static void Cls()
        => AnsiConsole.Clear();

    public static void Title(string titleText)
        => AnsiConsole.Write(
            new FigletText(titleText)
            .LeftJustified()
            .Color(Color.RoyalBlue1));

    public static void Failed(string message)
        => Write(Situation.Error, message);

    public static string Wrap(string text, Situation color)
        => $"[{SituationColors[color]}]{text}[/]";

    public static string Bold(string text)
        => $"[bold]{text}[/]";

    private static void Write(Situation color, string message)
        => AnsiConsole.MarkupLine(Wrap(message, color));

    private static List<string> ExtractPlaceholders(string messageTemplate)
    {
        var placeHolderMatches = BracketedParameterPattern().Matches(messageTemplate);

        return placeHolderMatches.Cast<Match>().Select(m => m.Groups[1].Value).ToList();
    }

    [GeneratedRegex("\\{(\\w+)\\}")]
    private static partial Regex BracketedParameterPattern();
}
