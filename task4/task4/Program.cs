using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Используйте в виде аргумента файл input_file.txt");
            return;
        }

        string inputFile = args[0];

        try
        {
            int[] nums = File.ReadAllLines(inputFile).Select(int.Parse).ToArray();

            int moves = MinMovesToEqualize(nums);

            Console.WriteLine(moves);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static int MinMovesToEqualize(int[] nums)
    {
        if (nums.Length == 0)
        {
            throw new ArgumentException("Массив чисел не может быть пустым.");
        }

        int moves = 0;
        int target = nums[nums.Length / 2]; // Медиана массива как целевое значение

        foreach (int num in nums)
        {
            moves += Math.Abs(num - target);
        }

        return moves;
    }
}
