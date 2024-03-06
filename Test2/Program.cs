namespace Test2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] firstWords = { "cinema", "host", "aba", "train" };
            string[] secondWords = { "iceman", "shot", "bab", "rain" };

            Console.WriteLine(Anagrams(firstWords, secondWords));
        }

        static string Anagrams(string[] firstWords, string[] secondWords)
        {
            if (firstWords.Length != secondWords.Length) return null;

            char[] result = new char[firstWords.Length];

            for (int i = 0; i < firstWords.Length; i++)
            {
                char res = Anagram(firstWords[i].ToLower(), secondWords[i].ToLower()) ? '1' : '0';
                result[i] = res;
            }

            return new string(result);
        }

        static bool Anagram(string str1, string str2)
        {
            var sortedStr1 = String.Concat(str1.OrderBy(c => c));
            var sortedStr2 = String.Concat(str2.OrderBy(c => c));

            return sortedStr1 == sortedStr2;
        }
    }
}
