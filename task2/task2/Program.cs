using System;
class PointsCircle
{
    static void Main(string[] args)
    {
        string[] coordsCenterString;
        string[] CoordsPoints;
        float[] CoordsCenter = null;
        float Radius = 0.0f;
        using (StreamReader sr1 = new StreamReader(args[0]))
        {
            coordsCenterString = sr1.ReadLine()?.Split(' ');
            if (coordsCenterString != null && coordsCenterString.Length == 2)
            {
                float x = float.Parse(coordsCenterString[0]);
                float y = float.Parse(coordsCenterString[1]);
                CoordsCenter = [x, y];
            }
            string RadiusString = sr1.ReadLine();
            if (!string.IsNullOrEmpty(RadiusString) && float.TryParse(RadiusString, out Radius))
            {
                Radius = float.Parse(RadiusString);
            }
        }
        using (StreamReader sr2 = new StreamReader(args[1]))
        {
            while (!sr2.EndOfStream)
            {
                CoordsPoints = sr2.ReadLine().Split(' ');
                if (CoordsPoints.Length == 2)
                {
                    int x0 = int.Parse(CoordsPoints[0]);
                    int y0 = int.Parse(CoordsPoints[1]);
                    float distanceSquared =(CoordsCenter[0] - x0) * (CoordsCenter[0] - x0) + (CoordsCenter[1] - y0) * (CoordsCenter[1] - y0);
                    if (distanceSquared < Radius * Radius)
                    {
                        // Точка находится внутри окружности:
                        Console.WriteLine(1);
                    }
                    else if (Math.Abs(distanceSquared - Radius * Radius) < 1e-10)
                    {
                        // Точка находится на окружности: 
                        Console.WriteLine(0);
                    }
                    else
                    {
                        // Точка находится снаружи окружности:
                        Console.WriteLine(2);
                    }

                }
            }
        }
    }
}