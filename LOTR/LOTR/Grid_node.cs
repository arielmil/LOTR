using System;
using System.Collections.Generic;
using static LOTR.Types;

namespace LOTR {
    public class Grid_node {
        public char tileType { get; set; }

        public bool visited { get; set; }

        //Distance from node to begin
        public float g { get; set; }
        
        //Distance from node to end (may be estimated by an heuristic function)
        public float h { get; set; }
        
        //Sum of g + h
        public float f { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        
        public static int XMax { get; set; }
        public static int YMax { get; set; }
        
        public Grid_node parent { get; set; }

        public List<Grid_node> Neighbors { get; private set; }

        public Grid_node(char type, int X, int Y) {
            tileType = type;
            Neighbors = new List<Grid_node>();


            visited = false;

            g = 0;
            f = 0;
            h = 0;
            
            this.X = X;
            this.Y = Y;
        }

        //Ver se é necessário
        public void setVisited() {
            visited = !visited;
        }
        
        public int getTypeCost() {
            return tileTypeToInt[tileType];
        }

        public void expand(Grid_node endNode, heuristicMethod method) {
            if (X > XMax && X < 0 || Y > YMax && Y < 0) {
                if (X > XMax && X < 0) {
                    setOneNeighbor(X + 1, Y, endNode, method);
                    setOneNeighbor(X - 1, Y, endNode, method);
                }
                if (Y > YMax && Y < 0) {
                    setOneNeighbor(X, Y + 1, endNode, method);
                    setOneNeighbor(X, Y - 1, endNode, method);
                }
            }

            else if (X == 0 || Y == 0) {
                if (X == 0) {
                    setOneNeighbor(1, Y, endNode, method);
                }

                if (Y == 0) {
                    setOneNeighbor(X, 1, endNode, method);
                }
            }

            else {
                if (X == XMax) {
                    setOneNeighbor(XMax - 1, Y, endNode, method);
                }

                if (Y == YMax) {
                    setOneNeighbor(X, Y - 1, endNode, method);
                }
            }
        }

        public void setOneNeighbor(int neighborX, int neighborY, Grid_node destiny, heuristicMethod method) {
            Grid_node neighbor = Grid_node_network.Get(neighborX, neighborY);
            if (neighbor == null) {
                    
                neighbor = new Grid_node(MatrixSerializer.map[neighborX, neighborY], neighborX, neighborY);
                neighbor.g = g + neighbor.getTypeCost();
                neighbor.SetFestimateHValue(destiny, method);
                neighbor.f = g + h;
                neighbor.parent = this;
                    
                Grid_node_network.Add(neighbor);
            }
                
            Neighbors.Add(neighbor);
        }

        public void updateGandFvalues() {
            g = parent.g + getTypeCost();
            f = g + h;
        }

        public void SetFestimateHValue(Grid_node destiny, heuristicMethod method) {
            switch (method) {
                case(heuristicMethod.Manhattan):
                    f = ManhattanHeuristic(destiny);
                    break;
                
                case(heuristicMethod.Euclidian):
                    f = EuclidianHeuristic(destiny);
                    break;
            }
        }
        
        private float ManhattanHeuristic(Grid_node destiny) {
            return Math.Abs(destiny.X - X) + Math.Abs(destiny.Y - Y);
        }

        private float EuclidianHeuristic(Grid_node destiny) {
            float triangleBase = destiny.X - X;
            float triangleHeight = destiny.Y - Y;

            return (float)Math.Sqrt(Math.Pow(triangleBase, 2) + Math.Pow(triangleHeight, 2));
        }

        public bool Equals(Grid_node gridNode) {
            return (gridNode.X == X && gridNode.Y == Y);
        }
    }
}