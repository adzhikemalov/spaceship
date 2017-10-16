using System;

namespace Assets.Utils
{
    public delegate void EmptyHandler();
    public delegate void ParameterHandler<T>(T v);
    public delegate void Parameter2Handler<T1, T2>(T1 v1, T2 v2);
    public delegate void Parameter3Handler<T1, T2, T3>(T1 v1, T2 v2, T3 v3);

    class Utils
    {
    }

	[Serializable]
	public struct Point
    {
        public static Point Empty = new Point(float.NaN, float.NaN);
		public float x, y;

        public Point(float px, float py)
        {
            x = px;
            y = py;
        }

        public float Magnitude
        {
            get {return (float)Math.Sqrt(x*x + y*y); }
        }

        public bool isEmpty
        {
            get { return (float.IsNaN(x) || float.IsNaN(y)); } 
        }

        public float Distance(Point point)
        {
            if (!isEmpty && !point.isEmpty)
                return (float) Math.Sqrt(Math.Pow(point.x - x, 2) + Math.Pow(point.y - y, 2));
            return 0;
        }

        public Point Normalize(float length)
        {
            return new Point(x / Magnitude * length, y / Magnitude * length);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.x - p2.x, p1.y-p2.y);
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.x + p2.x, p1.y+p2.y);
        }
        
        public static Point operator *(Point p1, float value)
        {
            return new Point(p1.x*value, p1.y*value);
        }

        public static bool operator ==(Point p1, Point p2)
        {
            if (p1.x == p2.x && p1.y == p2.y) return true;
            return false;
        }

        public static bool operator !=(Point p1, Point p2)
        {
            if (p1.x != p2.x || p1.y != p2.y) return true;
            return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Point p = (Point)obj;
            return (x == p.x) && (y == p.y);
        }

        public override string ToString()
        {
            return "{x:"+x+"; y:"+y+"}";
        }

        public override int GetHashCode() 
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
         
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                return hash;
            }
        }
    }
}
