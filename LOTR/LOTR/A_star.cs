using System;
using System.Collections.Generic;
using System.Numerics;

namespace LOTR {
    public class A_star {
        private List<Grid_node> Objectives { get; set; }

        private Grid_node Shire;
        public Grid_node fatherPath { get; private set; }
        
        public A_star(MatrixSerializer matrixSerializer) {
            Objectives = new List<Grid_node>();

            loadObjectives(matrixSerializer.exportObjectivesLocations());

            Shire = Objectives[0];
        }

        private void loadObjectives(List <(int, int)> objectivesLocations) {
            int objectiveX, objectiveY;
            
            foreach ((int, int) XY  in objectivesLocations) {
                objectiveX = XY.Item1;
                objectiveY = XY.Item2;
                Console.WriteLine($"Objective {MatrixSerializer.map[objectiveX, objectiveY]}: X: {objectiveX + 1}, Y: {objectiveY + 1}");
                Objectives.Add(new Grid_node(MatrixSerializer.map[objectiveX, objectiveY], objectiveX, objectiveY));
            }
        }
        
        //Main Loop from shire to mountDoom and back
        public void ToMountDoomAndBack() {
            Grid_node source = Shire;
            
            int i;
            for (i = 0; i < Objectives.Count - 1; i++) {
                source = FromSourceToDestiny(source, Objectives[i + 1]);
            }

            fatherPath = source;
        }

        //A* algorithm implementation
        public Grid_node FromSourceToDestiny(Grid_node source, Grid_node destiny) {
            int i;

            float best;
            
            SortedSet<Grid_node> open = new SortedSet<Grid_node>();
            SortedSet<Grid_node> closed = new SortedSet<Grid_node>();
            
            Grid_node currentNode = source;
            Grid_node neighbor;
            
            open.Add(currentNode);
            
            while (open.Count > 0) {
                currentNode = open.Min;
                
                if (currentNode.Equals(destiny)) {
                    return currentNode;
                }
                
                open.Remove(currentNode);
                closed.Add(currentNode);
                
                currentNode.expand(destiny, Types.heuristicMethod.Manhattan);
                
                best = float.MaxValue;
                
                for (i = 0; i < currentNode.Neighbors.Count; i++) {
                    neighbor = currentNode.Neighbors[i];

                    if (neighbor.Equals(destiny)) {
                        return neighbor;
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

            //No path found
            return null;
        }

    }
}
