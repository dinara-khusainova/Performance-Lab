using System;
using System.Text;

class TraversingCircularArray
{
    static void Main()
    {
        string[] input = Console.ReadLine().Split();
        if (input.Length != 2 || !int.TryParse(input[0], out int n) || !int.TryParse(input[1], out int m) || n <= 0 || m <= 0)
        {
            Console.WriteLine("Error. Inputed numbers must be greater than 0.");
            return;
        }
        int[] CircleArray = new int[n];
        int[] IntervalArray; 
        int EndElement=0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < n; i++)
        {
            CircleArray[i] = i+1;
        }
        int o = 0;
        while (EndElement != CircleArray[0]) {
            IntervalArray = new int[m];
            for (int w = 0; w < m; w++)
            {
                if (o == n) {o = 0;}
                IntervalArray[w] = CircleArray[o];
                o++;
            }
            o--;
            EndElement = IntervalArray[m - 1];
            sb.Append(IntervalArray[0]);
        }
        Console.WriteLine(sb);
    }
}