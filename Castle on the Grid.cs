using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'minimumMoves' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. STRING_ARRAY grid
     *  2. INTEGER startX
     *  3. INTEGER startY
     *  4. INTEGER goalX
     *  5. INTEGER goalY
     */

    public static int minimumMoves(List<string> grid, int startX, int startY, int goalX, int goalY)
    {
        int n = grid.Count;
        if (startX == goalX && startY == goalY) return 0;
        
        int[,] dist = new int[n, n];
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                dist[i, j] = -1; // -1 means unvisited
            }
        }
        
        Queue<(int, int)> queue = new Queue<(int, int)>();
        queue.Enqueue((startX, startY));
        dist[startX, startY] = 0;
        
        int[] dx = {-1, 1, 0, 0}; // Up, Down, Left, Right
        int[] dy = {0, 0, -1, 1};
        
        while (queue.Count > 0) {
            var (x, y) = queue.Dequeue();
            
            // Try all four directions
            for (int dir = 0; dir < 4; dir++) {
                int newX = x + dx[dir];
                int newY = y + dy[dir];
                
                // Keep moving in the same direction until we hit obstacle or boundary
                while (newX >= 0 && newX < n && newY >= 0 && newY < n && grid[newX][newY] != 'X') {
                    if (dist[newX, newY] == -1) {
                        dist[newX, newY] = dist[x, y] + 1;
                        
                        if (newX == goalX && newY == goalY) {
                            return dist[newX, newY];
                        }
                        
                        queue.Enqueue((newX, newY));
                    }
                    newX += dx[dir];
                    newY += dy[dir];
                }
            }
        }
        
        return -1; // No path found
    }


    }

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<string> grid = new List<string>();

        for (int i = 0; i < n; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int startX = Convert.ToInt32(firstMultipleInput[0]);

        int startY = Convert.ToInt32(firstMultipleInput[1]);

        int goalX = Convert.ToInt32(firstMultipleInput[2]);

        int goalY = Convert.ToInt32(firstMultipleInput[3]);

        int result = Result.minimumMoves(grid, startX, startY, goalX, goalY);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
