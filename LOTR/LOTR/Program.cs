using System;

namespace LOTR {
    class Program {
        static void Main(string[] args) {
            A_star aStar = new A_star(new MatrixSerializer(), true);
            Grid_node test = aStar.FromSourceToDestiny(new Grid_node(52, 15), new Grid_node(68, 15));
            MatrixSerializer.printTracedPathOnMap(test);
            
            //aStar.ToMountDoomAndBack();
        }
    }
}