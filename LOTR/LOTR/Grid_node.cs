using System;
using System.Collections.Generic;
using static LOTR.Types;

namespace LOTR {
    public class Grid_node {
        public char tileType { get; set; }
        
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

        public Grid_node(int X, int Y) {
            tileType = MatrixSerializer.map[X, Y];
            Neighbors = new List<Grid_node>();
            
            g = 0;
            f = 0;
            h = 0;
            
            this.X = X;
            this.Y = Y;
        }
        
        public int getTypeCost() {
            return tileTypeToInt[tileType];
        }

        public void expand(Grid_node endNode, heuristicMethod method) {
            if (X > 0 && X < XMax || Y > 0 && Y < YMax) {
                if (X > 0 && X < XMax) {
                    setOneNeighbor(X + 1, Y, endNode, method);
                    setOneNeighbor(X - 1, Y, endNode, method);
                }
                if (Y > 0 && Y < YMax) {
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
                    
                neighbor = new Grid_node(neighborX, neighborY);
                neighbor.g = g + neighbor.getTypeCost();
                neighbor.SetEstimateHValue(destiny, method);
                neighbor.f = neighbor.g + neighbor.h;
                neighbor.parent = this;
                    
                Grid_node_network.Add(neighbor);
            }
                
            Neighbors.Add(neighbor);
        }

        public void updateGandFvalues() {
            g = parent.g + getTypeCost();
            f = g + h;
        }

        public void SetEstimateHValue(Grid_node destiny, heuristicMethod method) {
            switch (method) {
                case(heuristicMethod.Manhattan):
                    h = ManhattanHeuristic(destiny);
                    break;
                
                case(heuristicMethod.Euclidian):
                    h = EuclidianHeuristic(destiny);
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

        public void retracePath() {
            int i = 0;
            
            Console.WriteLine($"Step {0}: X: {X}, Y: {Y}, type: {tileType.ToString()}");
            
            Grid_node Localparent = this.parent;
            while (Localparent.parent != null) {
                Console.WriteLine($"Step {i + 1}: X: {Localparent.X}, Y: {Localparent.Y}, type: {Localparent.tileType.ToString()}");
                i++;

                Localparent = Localparent.parent;
            }
            
            Console.WriteLine($"Step {i}: X: {Localparent.X}, Y: {Localparent.Y}, type: {Localparent.tileType.ToString()}");
        }
        
        public override bool Equals(Object O) {
            Grid_node gridNode = (Grid_node) O;
            return gridNode != null && (gridNode.X == X && gridNode.Y == Y);
        }
    }
}