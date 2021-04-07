using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Models
{
    class Point
    {
        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public int X { get; set; }
        public int Y { get; set; }

        public double g { get; set; } = 0;

        private double _h;

        public Point Parent { get; set; }

        public double f
        {
            get => _h + g;
        }

        public Point()
        {
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void CalculateHeuristic(Point finish)
        {
            var a = Math.Abs(finish.X - X);
            var b = Math.Abs(finish.Y - Y);
            _h = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        }
        
    }
}
