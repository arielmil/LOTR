using System.IO;
using System.Linq;

namespace LOTR {
    public class MatrixSerializer {
        public static char[,] map;
        
        public static void fileToMap() {
            int i, j;
            string pathI = Path.GetFullPath(Path.Combine(".", "..", "..", "..", "..", "mapa4.txt"));
            string pathO =  Path.GetFullPath(Path.Combine(".", "..", "..", "..", "..", "mapaOut.txt"));
            
            string[] lines = System.IO.File.ReadAllLines(pathI);
            
            int lineCount = lines.Length;
            int rowCount = lines[0].Length;

            map = new char[lineCount, rowCount];

            for (i = 0; i < lineCount; i++) {
                for (j = 0; j < rowCount; i++) {
                    map[i, j] = lines[i][j];
                }
            }
            
            File.WriteAllLinesAsync(pathO, lines);
        }
    }
}