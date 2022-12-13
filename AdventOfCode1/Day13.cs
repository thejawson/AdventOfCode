using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode;

internal class Day13 : IDay
{
    private IEnumerable<string[]> Data = Input.Day13.Split("\r\n\r\n")
                .Select(m => m.Split("\r\n")).ToList();

    public string Puzzle1()
    {
        var totalOrder = 0;
        int count = 1;
        foreach (var signals in Data)
        {
            if (IsArrayInOrder(signals) ?? true)
                totalOrder += count;
            count++;
        }
        return totalOrder.ToString();
    }

    public string Puzzle2()
    {
        var data = Input.Day13.Split("\r\n").Where(m => !string.IsNullOrEmpty(m)).ToList();
        data.Add("[[2]]");
        data.Add("[[6]]");
        data.Sort((n, m) => { return IsArrayInOrderSorter(new[] { n, m }); });
        return ((data.IndexOf("[[2]]")+1) * (data.IndexOf("[[6]]")+1)).ToString();
    }

    private int IsArrayInOrderSorter(string[] signals)
    {
        return IsArrayInOrder(signals) switch
        {
            true => -1,
            false => 1,
            _ => signals[0].Length - signals[1].Length,
        };
    }


    private bool? IsArrayInOrder(string[] signals)
    {
        var signal1 = ParseSignal (signals[0]).ToArray();
        var signal2 = ParseSignal(signals[1]).ToArray();

        for (int i = 0; i < signal1.Length; i++)
        {
            if (signal1[i] == "")
                return true;
            if (signal2.Length <= i || signal2[i] == "")
                return false;
            if (signal1[i][0] =='[' || signal2[i][0] =='[')
            {
                signal1[i] = signal1[i][0] == '[' ? signal1[i]: $"[{signal1[i]}]";
                signal2[i] = signal2[i][0] == '[' ? signal2[i]: $"[{signal2[i]}]"; ;                

                var isInOrder = IsArrayInOrder(new[] { signal1[i], signal2[i] });
                if (isInOrder.HasValue)
                    return isInOrder;
            }
            else
            {
                if (int.TryParse(signal1[i], out var s1) && int.TryParse(signal2[i], out var s2))
                {
                    if (s1 < s2)
                        return true;

                    if (s1 > s2)
                        return false;
                }
            }
        }
        if (signal1.Length < signal2.Length)
            return true;
        return null;
    }

    private IEnumerable<string> ParseSignal(string parseString)
    {
        int nextSplit = 0;
        int bracketCount = 0;
        parseString = parseString.Substring(1, parseString.Length - 2);
        for (int i = 0; i < parseString.Length; i++)
        {
            switch (parseString[i])
            {
                case ',':
                    if (bracketCount == 0)
                        if(nextSplit == i)
                            nextSplit++;
                        else
                        {
                            yield return parseString.Substring(nextSplit, i - nextSplit);
                            nextSplit = i + 1;
                        }
                    break;
                case '[':
                    bracketCount++;
                    break;

                case ']':
                    bracketCount--;
                    if (bracketCount == 0)
                    {
                        yield return parseString.Substring(nextSplit, i - nextSplit+1);
                        nextSplit = i + 1;
                    }
                    break;
            }
        }

        if(nextSplit< parseString.Length )
            yield return parseString.Substring(nextSplit, parseString.Length - nextSplit);
    }
}