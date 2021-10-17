using System;
using System.Collections.Generic;
using static LOTR.Types;

namespace LOTR {
    public class Grid_node {
        public char tileType { get; set; }

        public bool visited { get; set; }

        //Distance from node to begin
        public int gscore { get; set; }
        
        //Distance from node to end
        public int hscore { get; set; }
        
        //Sum of g + h
        public int fscore { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        private List<Grid_node> Neighbors;

        public Grid_node(char type, int X, int Y) {
            tileType = type;
            Neighbors = new List<Grid_node>();


            visited = false;

            gscore = 0;
            fscore = 0;
            
            this.X = X;
            this.Y = Y;
        }

        public void setVisited() {
            visited = !visited;
        }

        public void addNeighbor(Grid_node neighbor) {
            Neighbors.Add(neighbor);
        }
        
        public int getCost() {
            return types[tileType];
        }

        public bool Equals(Grid_node gridNode) {
            return (gridNode.X == X && gridNode.Y == Y);
        }
    }
}