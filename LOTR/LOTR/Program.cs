using System;

namespace LOTR {
    class Program {
        static void Main(string[] args) {
            A_star aStar = new A_star(new MatrixSerializer());
            Grid_node test = aStar.FromSourceToDestiny(new Grid_node(15, 57), new Grid_node(15, 68));
            Console.WriteLine("voltei");
            //aStar.ToMountDoomAndBack();
        }
    }
}