using System.IO;

namespace LOTR {
    public class MatrixSerializer {
        public static char[,] map;
        
        public static void fileToMap() {
            //j = 0 por que compilador estava reclamando na linha 28
            int i, j = 0;
            string pathI = Path.GetFullPath(Path.Combine(".", "..", "..", "..", "..", "mapa4.txt"));
            
            string[] lines = File.ReadAllLines(pathI);
            
            int lineCount = lines.Length;
            int rowCount = lines[0].Length;

            map = new char[lineCount, rowCount];

            for (i = 0; i < lineCount; i++) {
                for (j = 0; j < rowCount; j++) {
                    map[i, j] = lines[i][j];
                }
            }

            Grid_node.XMax = j;
            Grid_node.YMax = i;
        }
    }
}