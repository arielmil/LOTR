using System;

namespace LOTR {
    class Program {
        static void Main(string[] args) {
            A_star aStar = new A_star(new MatrixSerializer(), true);
            Grid_node test = aStar.FromSourceToDestiny(new Grid_node(25, 21), new Grid_node(36, 21));
            
            //aStar.ToMountDoomAndBack();
        }
    }
}