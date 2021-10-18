using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace LOTR {
    public class MatrixSerializer {
        private static String terrainOptions = ".#RVMP";
        public static char[,] map;

        private SortedSet<char> objectives;
        
        public MatrixSerializer() {
            char tyle;
            
            string pathI = Path.GetFullPath(Path.Combine(".", "..", "..", "..", "..", "mapa4.txt"));
            
            string[] lines = File.ReadAllLines(pathI);
            
            objectives = new SortedSet<char>(new ObjectiveComparer());
            
            int lineCount = lines.Length;
            int rowCount = lines[0].Length;

            map = new char[lineCount, rowCount];

            int i, j = 0;
            for (i = 0; i < lineCount; i++) {
                for (j = 0; j < rowCount; j++) {
                    tyle = lines[i][j];

                    if (!terrainOptions.Contains(tyle)) {
                        Types.charToObjectiveLocation.Add(tyle, (i, j));
                        objectives.Add(tyle);
                    }
                    
                    map[i, j] = tyle;
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
            public int Compare(char objective1, char objective2) {
                bool objective1IsNumber = Char.IsNumber(objective1);
                bool objective2IsNumber = Char.IsNumber(objective2);

                int val1, val2;
            
                if (objective1IsNumber) {
                    if (objective2IsNumber) {
                        System.Console.WriteLine($"objective1: {objective1}, objective2: {objective2}");
                        return objective1.CompareTo(objective2);
                    }
                
                    return 1;
                }
            
                if (objective2IsNumber) {
                    return 1;
                }
            
                return objective1.CompareTo(objective2);
            }
        }
    }
}