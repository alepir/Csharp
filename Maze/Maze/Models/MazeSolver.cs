using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maze.Models
{
    class MazeSolver
    {
        private MazeStates[,] maze;

        private int _rows;
        private int _cols;

        public MazeSolver()
        {
            
        }

        public void SetMazeSize(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            maze = new MazeStates[_rows, _cols];
        }

        private bool Exist(MazeStates findValue)
        {
            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _cols; j++)
                {
                    if (GetItem(i, j) == findValue)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public Point GetFirstItemByType(MazeStates findValue)
        {
            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _cols; j++)
                {
                    if (GetItem(i, j) == findValue)
                    {
                        return new Point(j,i);
                    }
                }
            }

            return null;
        } 


        public bool ExistState(MazeStates findValue)
        {
            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _cols; j++)
                {
                    if (GetItem(i, j) == findValue)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void FindAndReplace(MazeStates findValue, MazeStates replaceValue)
        {
            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _cols; j++)
                {
                    if (GetItem(i, j) == findValue)
                    {
                        SetItem(i,j,replaceValue);
                    }
                }
            }
        }

        public void SetItem(int row, int col, MazeStates value)
        {
            if (value == MazeStates.Finish || value == MazeStates.Start)
            {
                FindAndReplace(value, MazeStates.Empty);
            }

            maze[row, col] = value;
        }

        public MazeStates GetItem(int row, int col)
        {
            return maze[row, col];
        }

        public void SolveMaze()
        {
            var start = GetFirstItemByType(MazeStates.Start);
            var finish = GetFirstItemByType(MazeStates.Finish);
            FindAndReplace(MazeStates.Path, MazeStates.Empty);


            if (start is null || finish is null)
            {
                MessageBox.Show("Cannot solve maze without specified start and finish. Use Right click on mouse.");
                return;
            }

            AStar(start,finish);
        }

        
        public void AStar(Point start, Point finish)
        {

            var open = new List<Point>();
            var closed = new List<Point>();
            var sqrt2 = Math.Sqrt(2);

            start.CalculateHeuristic(finish);
            open.Add(start);

            var successorsPoints = new List<Point>
            {
                new (-1,-1),
                new (0,-1),
                new (1,-1),
                new (-1,0),
                new (1,0),
                new (-1,1),
                new (0,1),
                new (1,1)
            };


            while (open.Count > 0)
            {
                var q = open.OrderBy(point => point.f).FirstOrDefault();


                foreach (var succPoint in successorsPoints)
                {
                    var successor = new Point(q.X + succPoint.X, q.Y + succPoint.Y);
                    
                    //Out of bound
                    if(successor.X < 0 || successor.Y < 0 || successor.X >= _cols || successor.Y >= _rows) { continue; }

                    if (GetItem(successor.Y, successor.X) == MazeStates.Wall)
                    {
                        continue;
                    }

                    if (succPoint.X == 0 || succPoint.Y == 0)
                    {
                        
                        successor.g = q.g + 1;
                    }
                    else
                    {
                        if (GetItem(successor.Y, q.X) == MazeStates.Wall &&
                            GetItem(q.Y, successor.X) == MazeStates.Wall)
                        {
                            continue;
                        }
                        successor.g = q.g + sqrt2;
                    }

                    successor.CalculateHeuristic(finish);
                    successor.Parent = q;

                    if (successor.Equals(finish))
                    {
                        while (successor.Parent != start)
                        {
                            successor = successor.Parent;
                            SetItem(successor.Y,successor.X,MazeStates.Path);
                        }

                        return;
                    }
                    
                    var successorInOpen = open.FirstOrDefault(point => point.X == successor.X && point.Y == successor.Y);
                    
                    if (successorInOpen?.f > successor.f)
                    {
                        open.Remove(successorInOpen);
                    }

                    var successorInClose = closed.FirstOrDefault(point => point.X == successor.X && point.Y == successor.Y);
                    if (successorInClose?.f < successor.f)
                    {
                        continue;
                    }

                    open.Add(successor);
                } //end of foreach

                closed.Add(q);
                open.Remove(q);

            } // end while
            
        }


    }
}
