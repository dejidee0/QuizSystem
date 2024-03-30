using System;
public static class ExamCodeGenerator
{
    private const string AllowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private static readonly Random Random = new Random();

    public static string GenerateExamCode(int length)
    {
        char[] chars = new char[length];
        for (int i = 0; i < length; i++)
        {
            chars[i] = AllowedChars[Random.Next(0, AllowedChars.Length)];
        }
        return new string(chars);
    }
}
