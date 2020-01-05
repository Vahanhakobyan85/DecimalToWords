using System;
using System.Globalization;
using System.Text;

/// <summary>
/// A simple program which transform the Decimal or Numberic numbers to a sentences.
/// </summary>
public class Program
{
    static void Main(string[] args)
    {
        var stringInput = "0";
        var input = GetDecimalValue(stringInput);
        Console.WriteLine(PronounceDecimal(input));

        stringInput = "1";
        input = GetDecimalValue(stringInput);
        Console.WriteLine(PronounceDecimal(input));

        stringInput = "25,1";
        input = GetDecimalValue(stringInput);
        Console.WriteLine(PronounceDecimal(input));

        stringInput = "0,01";
        input = GetDecimalValue(stringInput);
        Console.WriteLine(PronounceDecimal(input));

        stringInput = "45 100";
        input = GetDecimalValue(stringInput);
        Console.WriteLine(PronounceDecimal(input));

        stringInput = "999 999 999,99";
        input = GetDecimalValue(stringInput);
        Console.WriteLine(PronounceDecimal(input));

        Console.ReadLine();
    }

    /// <summary>
    /// Converting string to decimal value.
    /// </summary>
    /// <param name="stringInput"></param>
    /// <returns></returns>
    public static decimal GetDecimalValue(string stringInput)
    {
        stringInput = stringInput.Replace(" ", string.Empty);
        return decimal.Parse(stringInput, new NumberFormatInfo { NumberDecimalSeparator = "," });// or here we can just replace the comma with dot
    }

    /// <summary>
    /// Pronouncing the decimal part.
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static StringBuilder PronounceDecimal(decimal num)
    {
        StringBuilder words = new StringBuilder();

        if (num > 1000000000)
        {
            Console.WriteLine("The number intended to be less than 1000000000.");
            // or throw some exception.
        }

        if (num < 0)
        {
            decimal absNum = Math.Abs(num);
            words.Append("minus " + PronounceDecimal(absNum));
        }

        int intPortion = (int)num;
        decimal fraction = (num - intPortion) * 100;

        if (fraction >= 100)
        {
            Console.WriteLine("Fraction cannot be more than 99 cents.");
            // or throw some exception.
        }

        int decPortion = (int)fraction;

        words = PronounceNumber(intPortion);
        words.Append((intPortion == 1) ? " dollar" : " dollars");
        if (decPortion > 0)
        {
            words.Append(" and ");
            words.Append(PronounceNumber(decPortion));
            words.Append((decPortion == 1) ? " cent" : " cents");
        }

        return words;
    }

    /// <summary>
    /// Pronouncing the numeric part.
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static StringBuilder PronounceNumber(int num)
    {
        if (num == 0)
        {
            return new StringBuilder("zero");
        }

        if (num < 0)
        {
            int absNum = Math.Abs(num);
            return new StringBuilder("minus " + PronounceNumber(absNum));
        }

        StringBuilder words = new StringBuilder("");


        if (num / 1000000000 > 0)
        {
            words.Append(PronounceNumber(num / 1000000000) + " billion ");
            num %= 1000000000;
        }


        if (num / 1000000 > 0)
        {
            words.Append(PronounceNumber(num / 1000000) + " million ");
            num %= 1000000;
        }

        if (num / 1000 > 0)
        {
            words.Append(PronounceNumber(num / 1000) + " thousand ");
            num %= 1000;
        }

        if (num / 100 > 0)
        {
            words.Append(PronounceNumber(num / 100) + " hundred ");
            num %= 100;
        }

        if (num > 0)
        {
            var units = GetUnitsMap();
            var tens = GetTensMap();
            
            if (num < 20)
            {
                words.Append(units[num]);
            }
            else
            {
                words.Append(tens[num / 10]);
                if (num % 10 > 0)
                {
                    words.Append("-" + units[num % 10]);
                }
            }
        }

        return words;
    }

    /// <summary>
    /// Getting the list of inits mapping.
    /// </summary>
    /// <returns></returns>
    public static string[] GetUnitsMap()
    {
        string[] units = null;
        if (units == null)
        {
            units = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        }

        return units;
    }

    /// <summary>
    /// Getting the list of tens mapping.
    /// </summary>
    /// <returns></returns>
    public static string[] GetTensMap()
    {
        string[] tens = null;
        if(tens == null)
        {
            tens = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sizty", "seventy", "eighty", "ninety" };
        }
        
        return tens;
    }
}

