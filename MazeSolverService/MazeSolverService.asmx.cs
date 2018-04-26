using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MazeSolverService
{
    /// <summary>
    /// Summary description for MazeSolverService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MazeSolverService : System.Web.Services.WebService
    {
        [WebMethod]
        public string MazeSolver(string maze)
        {
            //string to contain returned solution
            var solution = String.Empty;

            //convert input to 2D array
            var mazearr = maze.ToCharArrayArray();

            //Find the start and end of the maze
            var start = mazearr.Find('A');
            var end = mazearr.Find('B');

            //Initialize array indicating distance from start
            var distancearr = new int[mazearr.Length][];

            for (int i = 0; i < mazearr.Length; i++)
            {
                distancearr[i] = new int[mazearr[i].Length];

                for (int j = 0; j < mazearr[i].Length; j++)
                    distancearr[i][j] = int.MaxValue;
            }

            distancearr[start.Item1][start.Item2] = 0;

            var depth = 0;

            int r, c;
            var solved = false;

            do
            {
                //loop through the maze looking for current depth
                for (r = 0; r < distancearr.Length; r++)
                {
                    for (c = 0; c < distancearr[r].Length; c++)
                    {
                        if (distancearr[r][c] == depth)
                        {
                            //Check the coordinate in each direction
                            solved = CheckCoordinate(mazearr, distancearr, r - 1, c, depth + 1);
                            if (!solved) solved = CheckCoordinate(mazearr, distancearr, r, c + 1, depth + 1);
                            if (!solved) solved = CheckCoordinate(mazearr, distancearr, r + 1, c, depth + 1);
                            if (!solved) solved = CheckCoordinate(mazearr, distancearr, r, c - 1, depth + 1);
                        }
                    }
                }

                //Increase depth for the next loop
                depth++;
            }
            while (!solved);

            r = end.Item1;
            c = end.Item2;

            //Work backwards from the end to mark the path
            do
            {

                if (mazearr[r][c] == '.')
                    mazearr[r][c] = '@';

                //Find the coordinate from the current position closest to the start
                if (r - 1 >= 0 && distancearr[r - 1][c] == distancearr[r][c] - 1) r--;
                else if (c + 1 < distancearr[r].Length && distancearr[r][c + 1] == distancearr[r][c] - 1) c++;
                else if (r + 1 < distancearr.Length && distancearr[r + 1][c] == distancearr[r][c] - 1) r++;
                else if (c - 1 >= 0 && distancearr[r][c - 1] == distancearr[r][c] - 1) c--;
            }
            while (distancearr[r][c] != 0);

            //Create output
            solution = "Solved in " + depth + " steps" + Environment.NewLine;

            for (r = 0; r < mazearr.Length; r++)
            {
                for (c = 0; c < mazearr[r].Length; c++)
                {
                    solution += mazearr[r][c];
                }

                if (r != mazearr.Length - 1)
                    solution += Environment.NewLine;
            }

            return solution;
        }

        private bool CheckCoordinate(char[][] maze, int[][] distance, int row, int col, int depth)
        {
            //If coordinates are out of bounds
            if (row < 0 || row >= maze.Length || col < 0 || col >= maze[row].Length)
                return false;

            //If we already reached this coordinate at an earlier depth
            if (distance[row][col] <= depth)
                return false;

            //If the coordinate is not an open path
            if (maze[row][col] != '.' && maze[row][col] != 'B')
                return false;

            //Mark the coordinate at the current depth
            distance[row][col] = depth;

            //If we found the exit, the maze is solved
            return maze[row][col] == 'B';
        }
    }

    public static class Extensions
    {
        public static char[][] ToCharArrayArray(this string s)
        {
            s = s.Replace(Environment.NewLine, "\n");
            var rows = s.Split('\n');

            for (int i = 0; i < rows.Length; i++)
                rows[i] = rows[i].Replace("\n", String.Empty);

            var arr = new char[rows.Length][];

            var r = 0;

            foreach (var row in rows)
            {
                arr[r] = row.ToCharArray();
                r++;
            }

            return arr;
        }

        public static Tuple<int, int> Find(this char[][] arr, char s)
        {
            for (int r = 0; r < arr.Length; r++)
                for (int c = 0; c < arr[r].Length; c++)
                    if (arr[r][c] == s)
                        return new Tuple<int, int>(r, c);

            return null;
        }
    }
}
