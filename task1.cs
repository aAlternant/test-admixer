using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static (int, int) MinMeetingsToSingleColor(int[] population)
    {
        int red = population[0];
        int green = population[1];
        int blue = population[2];

        int total = red + green + blue;

        int minMeetings = int.MaxValue;
        int bestTargetColor = -1;

        if (red == total || green == total || blue == total)
        {
            return (-1, -1);
        }

        for (int target = 0; target < 3; target++)
        {
            // BFS
            var queue = new Queue<(int, int, int, int)>();
            var visited = new HashSet<(int, int, int)>();

            queue.Enqueue((red, green, blue, 0));
            visited.Add((red, green, blue));

            while (queue.Count > 0)
            {
                var (r, g, b, steps) = queue.Dequeue();

                // check targed color is reached
                if ((target == 0 && g == 0 && b == 0) ||
                    (target == 1 && r == 0 && b == 0) ||
                    (target == 2 && r == 0 && g == 0))
                {

                    if (steps < minMeetings)
                    {
                        minMeetings = steps;
                        bestTargetColor = target;
                    }
                    break;
                }

                // gen new states
                var newStates = new List<(int, int, int)>
                {
                    (r - 1, g - 1, b + 2),
                    (r - 1, g + 2, b - 1), 
                    (r + 2, g - 1, b - 1)  
                };

                foreach (var (nr, ng, nb) in newStates)
                {
                    if (nr >= 0 && ng >= 0 && nb >= 0 && !visited.Contains((nr, ng, nb)))
                    {

                        queue.Enqueue((nr, ng, nb, steps + 1));
                        visited.Add((nr, ng, nb));
                    }
                }
            }
        }

        return minMeetings == int.MaxValue ? (-1, -1) : (minMeetings, bestTargetColor);
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Random rand = new Random();

        int r = rand.Next(1, int.MaxValue);
        int g = rand.Next(1, int.MaxValue);
        int b = rand.Next(1, int.MaxValue);

        Console.WriteLine($"Кількість їжачків: Червоний - {r}, Зелений - {g}, Синій - {b}");

        int[] population = { r, g, b };
        var (result, color) = MinMeetingsToSingleColor(population);

        if (result == -1)
        {
            Console.WriteLine("Неможливо перефарбувати всiх їжачків в 1 колір :(.");
        }
        else
        {
            Console.WriteLine($"Мінімальна кількість зустрічей: {result}");
            Console.WriteLine($"Перефарбовані у: {(color == 0 ? "Червоний" : color == 1 ? "Зелений" : "Синій")}");
        }
    }
}
