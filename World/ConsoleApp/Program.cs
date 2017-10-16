using System;
using System.Text;
using System.Threading;
using Assets.Utils;
using Assets.World;
using World.Map;

namespace TestApp
{
    class Program
    {
        private static WorldModel _world;
        static void Main(string[] args)
        {
            _world = new WorldModel();
            _world.InitMap(10, 10);
            Visualize();
            var obj = _world.AddMovingObject(new Point(0, 0));
            obj.MoveToPoint(new Point(8, 8));
            while (true)
            {
                Visualize();
                _world.Update();
                Thread.Sleep(100);
            }
        }

        private static void Visualize()
        {
            Console.Clear();
            for (int i = 0; i < _world.WorldMap.Rows; i++)
            {
                var count = 0;
                var sb = new StringBuilder();
                for (int j = 0; j < _world.WorldMap.Cols; j++)
                {
                    sb.Append(VisualizeCell(_world.WorldMap.GetCell(j, i)));
                }
                Console.WriteLine(sb);
            }
        }

        private static string VisualizeCell(CellModel cell)
        {
            var str = string.Empty;
            str = "_";
            foreach (var obj in _world.Objects)
            {
                if (_world.IsObjectInCell(cell, obj))
                {
                    str = "0";
                    break;
                }
            }
            return str;
        }
    }
}
