using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace LOTR {
    public class MatrixSerializer {
        private static String terrainOptions = ".#RVMP";
        public static char[,] map;

        private SortedSet<char> objectives;
        
        public MatrixSerializer(bool debug = false) {
            char tyle;
            
            string pathI = Path.GetFullPath(Path.Combine(".", "..", "..", "..", "..", "mapa4.txt"));
            
            string[] lines = File.ReadAllLines(pathI);
            
            objectives = new SortedSet<char>(new ObjectiveComparer(debug));
            
            int lineCount = lines.Length;
            int rowCount = lines[0].Length;
            Console.WriteLine($"rowCount: {rowCount}, lineCount: {lineCount}");
            map = new char[rowCount, lineCount];

            int i, j = 0;
            for (i = 0; i < lineCount - 1; i++) {
                for (j = 0; j < rowCount; j++) {
                    try {
                        tyle = lines[j][i];

                    }
                    catch(Exception er) {
                        throw (new Exception($"Error. Matrix out of bounds at line {i} row {j}."));
                    }

                    if (!terrainOptions.Contains(tyle)) {
                        Types.charToObjectiveLocation.Add(tyle, (j, i));
                        Types.tileTypeToInt.Add(tyle, 1);
                        objectives.Add(tyle);
                    }
                    
                    map[j, i] = tyle;
                }
            }
            
            
            Grid_node.XMax = j;
            Grid_node.YMax = i;
        }

        public List<(int, int)>  exportObjectivesLocations() {
            List<(int, int)> objectiveLocations = new List<(int, int)>();
            foreach (char objective in objectives) {
                objectiveLocations.Add(Types.charToObjectiveLocation[objective]);
            }

            return objectiveLocations;
        }
        
        private class ObjectiveComparer : IComparer<char> {
            private bool debug;

            public ObjectiveComparer(bool debug) {
                this.debug = debug;
            }
            public int Compare(char objective1, char objective2) {
                bool objective1IsNumber = Char.IsNumber(objective1);
                bool objective2IsNumber = Char.IsNumber(objective2);

                int val1, val2;
            
                if (objective1IsNumber) {
                    if (objective2IsNumber) {
                        if (debug) {
                            Console.WriteLine($"objective1: {objective1}, objective2: {objective2}");
                        }
                        return objective1.CompareTo(objective2);
                    }
                
                    return -1;
                }
            
                if (objective2IsNumber) {
                    return 1;
                }
            
                return objective1.CompareTo(objective2);
            }
        }
    }
}