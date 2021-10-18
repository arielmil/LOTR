using System.Collections.Generic;

namespace LOTR {
    public static class Grid_node_network {
        private static List<Grid_node> existingNodes = new List<Grid_node>();

        public static bool Exist(int X, int Y) {
            return existingNodes.Exists(grid_node => (grid_node.X == X && grid_node.Y == Y));
        }
        
        //Fazer entrar sempre sorted para facilitar a busca para exists
        public static void Add(Grid_node gridNode) {
            existingNodes.Add(gridNode);
        }

        public static Grid_node Get(int X, int Y) {
            return existingNodes.Find(grid_node => (grid_node.X == X && grid_node.Y == Y));
        }
        
    }
}