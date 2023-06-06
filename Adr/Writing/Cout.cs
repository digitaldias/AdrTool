﻿using System.Text.RegularExpressions;
using Spectre.Console;

namespace Adr;

public static partial class Cout
{
    public static void Hr()
        => AnsiConsole.Write(new Rule());
    public static void Cls()
        => AnsiConsole.Clear();

    public static void Title(string titleText)
        => AnsiConsole.Write(
            new FigletText(titleText)
                .LeftJustified()
                .Color(Color.RoyalBlue1));

    public static void Failed(string message)
    {
        Write(Situation.Error, message);
    }

    private static List<string> ExtractPlaceholders(string messageTemplate)
    {
        var placeHolderMatches = Regex.Matches(messageTemplate, @"\{(\w+)\}");
        return placeHolderMatches.Cast<Match>().Select(m => m.Groups[1].Value).ToList();
    }

    private static void Write(Situation color, string message)
        => AnsiConsole.MarkupLine(Wrap(message, color));

    private static string Wrap(string text, Situation color)
        => $"[{SituationColors[color]}]{text}[/]";


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

    private static Dictionary<Situation, string> SituationColors = new()
    {
        { Situation.Information, "white" },
        { Situation.Success, "green" },
        { Situation.Warning, "yellow" },
        { Situation.Error, "red" },
        { Situation.NumericValue, "orange3" },
        { Situation.Choice, "green" },
        { Situation.InputLabel, "royalblue1" }
    };
}