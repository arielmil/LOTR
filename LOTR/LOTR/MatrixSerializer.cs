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

            map = new char[rowCount, lineCount];

            int line, row = 0;
            for (line = 0; line < lineCount - 1; line++) {
                for (row = 0; row < rowCount; row++) {
                    try {
                        tyle = lines[line][row];

                    }
                    
                    catch {
                        throw (new Exception($"Error. Matrix out of bounds at [row {row} line {line}]."));
                    }

                    if (!terrainOptions.Contains(tyle)) {
                        Types.charToObjectiveLocation.Add(tyle, (row, line));
                        Types.tileTypeToInt.Add(tyle, 1);
                        objectives.Add(tyle);
                    }
                    
                    try {
                        map[row, line] = tyle;
                    }
                    
                    catch {
                        throw (new Exception($"Error. Matrix out of bounds at [row {row} line {line}]."));
                    }
                }
            }
            
            
            Grid_node.XMax = row;
            Grid_node.YMax = line;
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