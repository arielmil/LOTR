using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace LOTR {
    public class A_star {
        private static List<Grid_node> Objectives;

        private Grid_node Shire;
        private Grid_node MountainOfDoom;
        
        public A_star(List <Vector2> objectivesLocations) {
            Objectives = new List<Grid_node>();

            loadObjectives(objectivesLocations);

            Shire = Objectives[0];
            MountainOfDoom = Objectives[Objectives.Count - 1];
        }

        public static void loadObjectives(List <Vector2> objectivesLocations) {
            int objectiveX, objectiveY;
            Grid_node objectiveNode;
            
            foreach (Vector2 objectiveLocation in objectivesLocations) {
                objectiveX = (int) objectiveLocation.X;
                objectiveY = (int) objectiveLocation.Y;

                Objectives.Add(new Grid_node(MatrixSerializer.map[objectiveX, objectiveY], objectiveX, objectiveY));
            }
        }
        
        //Main Loop from shire to mountDoom and back
        public void ToMountDoomAndBack() {
            //Falta fazer uma estrutura para armazenar o path todo
            int i;

            for (i = 0; i < Objectives.Count - 1; i++) {
                FromSourceToDestiny(Objectives[i], Objectives[i + 1]);
            }
        }

        //A* algorithm implementation
        public void FromSourceToDestiny(Grid_node source, Grid_node destiny) {
            int i;

            float best;
            
            SortedSet<Grid_node> open = new SortedSet<Grid_node>();
            SortedSet<Grid_node> closed = new SortedSet<Grid_node>();
            
            Grid_node currentNode = source;
            Grid_node neighbor;
            
            open.Add(currentNode);
            
            while (!currentNode.Equals(destiny)) {
                currentNode = open.Min;
                
                if (currentNode.Equals(destiny)) {
                    return;
                }
                
                open.Remove(currentNode);
                closed.Add(currentNode);
                
                currentNode.expand(destiny, Types.heuristicMethod.Manhattan);
                
                best = float.MaxValue;
                
                for (i = 0; i < currentNode.Neighbors.Count; i++) {
                    neighbor = currentNode.Neighbors[i];

                    if (neighbor.Equals(destiny)) {
                        return;
                    }

                    if (neighbor.tileType == '#' || neighbor.tileType == 'P' || closed.Contains(neighbor)) {
                        continue;
                    }
                    
                    if (neighbor.f < best) {
                        best = neighbor.f;
                        neighbor.parent = currentNode;
                        neighbor.updateGandFvalues();
                    }

                    if (!open.Contains(neighbor)) {
                        open.Add(neighbor);
                    }
                    
                }
                
            }
        }

    }
}
