using System.Linq; //a set of technologies based on the integration of query capabilities directly into the C# language

public static bool IsPalindrome(string str)  
{
    return str.SequenceEqual(str.Reverse());
}
