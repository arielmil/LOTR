using System;
using System.Diagnostics;
using System.Timers;

namespace LOTR {
    class Program {
        private static Stopwatch t = new Stopwatch();
        static void Main(string[] args) {
            A_star aStar = new A_star(new MatrixSerializer());
            
            //t.Start();
            //Grid_node test = aStar.FromSourceToDestiny(new Grid_node(52, 15), new Grid_node(160, 39));
            //t.Stop();
            
            //Console.WriteLine($"Elapsed time: {(t.ElapsedMilliseconds / 1000.0)}s");
            //MatrixSerializer.printTracedPathOnMap(test);
            
            aStar.ToMountDoomAndBack();
        }
    }
}