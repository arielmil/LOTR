using System;
using System.Collections.Generic;

namespace LOTR {
    public class A_star {
        private bool debug;
        private List<Grid_node> Objectives { get; set; }

        private Grid_node Shire;
        public Grid_node fatherPath { get; private set; }

        private static Random randomizer = new Random();
        
        public A_star(MatrixSerializer matrixSerializer, bool debug = false) {
            this.debug = debug;
            
            Objectives = new List<Grid_node>();

            loadObjectives(matrixSerializer.exportObjectivesLocations());

            Shire = Objectives[0];
        }

        private void loadObjectives(List <(int, int)> objectivesLocations) {
            int objectiveX, objectiveY;
            
            foreach ((int, int) XY  in objectivesLocations) {
                objectiveX = XY.Item1;
                objectiveY = XY.Item2;

                if (debug) {
                    Console.WriteLine($"Objective {MatrixSerializer.map[objectiveX, objectiveY]}: X: {objectiveX}, Y: {objectiveY}");
                }
                
                Objectives.Add(new Grid_node(objectiveX, objectiveY));
            }
            
            if (debug) {
                Console.WriteLine();
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
            
            SortedSet<Grid_node> open = new SortedSet<Grid_node>(new NodeComparer(true));
            List<Grid_node> closed = new List<Grid_node>();
            
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
                
                currentNode.expand(destiny, Types.heuristicMethod.Manhattan, true);
                
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

        private class NodeComparer : IComparer<Grid_node> {
            private bool debug;

            public NodeComparer(bool debug = false) {
                this.debug = debug;
            }
            public int Compare(Grid_node node1, Grid_node node2) {
                if (node2 != null && node1 != null && node1.f < node2.f) {
                    return -1;
                }
                
                if (node2 != null && node1 != null && node1.f == node2.f) {
                    if (node1.h < node2.h) {
                        return -1;
                    }
                    
                    if (node1.h > node2.h) {
                        return 1;
                    }

                    if (node1.h == node2.h) {
                        if (!node1.Equals(node2)) {
                            return randomizer.Next(-1, 2);
                        }

                        return 0;
                    }
                    
                    return 0;
                }

                return 1;
            }
        }
    }
}
