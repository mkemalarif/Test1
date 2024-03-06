namespace Test1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Masukkan karakter: ");
            string input = Console.ReadLine();

            if (input.Length % 2 == 0)
            {
                string bagianPertama = input.Substring(0, input.Length / 2);
                string bagianKedua = input.Substring(input.Length / 2);

                char[] array1 = bagianPertama.ToCharArray();
                Array.Reverse(array1);

                char[] array2 = bagianKedua.ToCharArray();
                Array.Reverse(array2);

                string result = new string(array2) + new string(array2);

                Console.WriteLine("Output: " + result);
            }
            else
            {
                Console.WriteLine("Jumlah karakter harus genap.");
            }
        }
    }
}
