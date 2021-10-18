using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace LOTR {
    public class A_star {
        private List<Grid_node> Objectives;

        private Grid_node Shire;
        private Grid_node MountainOfDoom;
        
        public A_star(Vector2 ShireLocation, Vector2 MountainOfDoomLocation) {
            Objectives = new List<Grid_node>();

            int ShireX = (int) ShireLocation.X;
            int ShireY = (int) ShireLocation.Y;
            
            int MountainOfDoomX = (int) MountainOfDoom.X;
            int MountainOfDoomY = (int) MountainOfDoom.Y;

            Shire = new Grid_node('.', ShireX, ShireY);
            MountainOfDoom = new Grid_node('.', MountainOfDoomX, MountainOfDoomY);
        }
        
        //Main Loop from shire to mountDoom and back
        public void ToMountDoomAndBack() {
            foreach (Grid_node Objective in Objectives) {
                
            }
        }

        //A* algorithim implementation
        public void FromSourceToDestiny(Grid_node source, Grid_node destiny) {
            List<Grid_node> open = new List<Grid_node>();
            List<Grid_node> closed = new List<Grid_node>();
            
            Grid_node currentNode = source;
            open.Add(currentNode);
            
            while (currentNode.Equals(destiny)) {
                currentNode = open[0];

                if (currentNode.Equals(destiny)) {
                    return;
                }
                
                open.Remove(currentNode);
                closed.Add(currentNode);

                //Transformar em um loop de repetição aonde a váriavel neighbor pode ser alterada
                foreach (Grid_node neighbor in currentNode.Neighbors) {
                    if (neighbor.tileType == '#' || neighbor.tileType == 'P' || closed.Contains(neighbor)) {
                        continue;
                    }

                    Grid_node.expand(neighbor);
                }
            }
        }

    }
}
