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
        
        public static int maxX { get; set; }
        public static int maxY { get; set; }
        
        public Grid_node parent { get; set; }

        public List<Grid_node> Neighbors { get; set; }

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
        
        public int getTypeCost() {
            return tileTypeToInt[tileType];
        }
        
        
        //Refatorar, quebrar em metodos distintos para reutilizar os códigos que estão reescritos e são quase iguais.
        public static void expand(Grid_node gridNode) {
            int neighborX, neighborY;
            char neighborTileType;

            Grid_node neighbor;
            
            int i;
            if (gridNode.X > 0 && gridNode.X < maxX) {
                for (i = 0; i < 2; i++) {
                    neighborY = gridNode.Y;
                    neighborX = gridNode.X + i + 1;

                    neighborTileType = MatrixSerializer.map[neighborX, neighborY];

                    if (Grid_node_network.Exist(neighborX, neighborY)) {
                        neighbor = Grid_node_network.Get(neighborX, neighborY);
                        gridNode.Neighbors.Add(neighbor);
                    }

                    else {
                        neighbor = new Grid_node(neighborTileType, neighborX, neighborY);
                        Grid_node_network.Add(neighbor);
                        gridNode.Neighbors.Add(neighbor);
                    }

                    neighbor.gscore = gridNode.gscore + neighbor.getTypeCost();
                    neighbor.hscore = gridNode.hscore - neighbor.getTypeCost();
                    neighbor.fscore = neighbor.gscore + neighbor.hscore;
                    
                    neighborX = gridNode.X - (i + 1);
                    
                    neighborTileType = MatrixSerializer.map[neighborX, neighborY];
                    
                    if (Grid_node_network.Exist(neighborX, neighborY)) {
                        gridNode.Neighbors.Add(Grid_node_network.Get(neighborX, neighborY));
                    }

                    else {
                        neighbor = new Grid_node(neighborTileType, neighborX, neighborY);
                        
                        Grid_node_network.Add(neighbor);
                        gridNode.Neighbors.Add(neighbor);
                    }
                    
                    neighbor.gscore = gridNode.gscore + neighbor.getTypeCost();
                    neighbor.hscore = gridNode.hscore - neighbor.getTypeCost();
                    neighbor.fscore = neighbor.gscore + neighbor.hscore;
                }
            }
            
            else {
                if (gridNode.X == 0) {
                    neighborY = gridNode.Y;
                    neighborX = gridNode.X + 1;
                }

                else {
                    neighborY = gridNode.Y;
                    neighborX = gridNode.X - 1;
                }
                
                neighborTileType = MatrixSerializer.map[neighborX, neighborY];
                    
                if (Grid_node_network.Exist(neighborX, neighborY)) {
                    neighbor = Grid_node_network.Get(neighborX, neighborY);
                    gridNode.Neighbors.Add(neighbor);
                }

                else {
                    neighbor = new Grid_node(neighborTileType, neighborX, neighborY);
                    Grid_node_network.Add(neighbor);
                    gridNode.Neighbors.Add(neighbor);
                }
                
                neighbor.gscore = gridNode.gscore + neighbor.getTypeCost();
                neighbor.hscore = gridNode.hscore - neighbor.getTypeCost();
                neighbor.fscore = neighbor.gscore + neighbor.hscore;
                
            }

            if (gridNode.Y > 0 && gridNode.Y < maxY) {
                for (i = 0; i < 4; i++) {
                    neighborY = gridNode.Y + i + 1;
                    neighborX = gridNode.X;
                    
                    neighborTileType = MatrixSerializer.map[neighborX, neighborY];
                    
                    if (Grid_node_network.Exist(neighborX, neighborY)) {
                        neighbor = Grid_node_network.Get(neighborX, neighborY);
                        gridNode.Neighbors.Add(neighbor);
                    }

                    else {
                        neighbor = new Grid_node(neighborTileType, neighborX, neighborY);
                        Grid_node_network.Add(neighbor);
                        gridNode.Neighbors.Add(neighbor);
                    }
                    
                    neighbor.gscore = gridNode.gscore + neighbor.getTypeCost();
                    neighbor.hscore = gridNode.hscore - neighbor.getTypeCost();
                    neighbor.fscore = neighbor.gscore + neighbor.hscore;
                    
                    neighborY = gridNode.Y - (i + 1);
                    
                    neighborTileType = MatrixSerializer.map[neighborX, neighborY];
                    
                    if (Grid_node_network.Exist(neighborX, neighborY)) {
                        neighbor = Grid_node_network.Get(neighborX, neighborY);
                        gridNode.Neighbors.Add(neighbor);
                    }

                    else {
                        neighbor = new Grid_node(neighborTileType, neighborX, neighborY);
                        
                        Grid_node_network.Add(neighbor);
                        gridNode.Neighbors.Add(neighbor);
                    }
                    
                    neighbor.gscore = gridNode.gscore + neighbor.getTypeCost();
                    neighbor.hscore = gridNode.hscore - neighbor.getTypeCost();
                    neighbor.fscore = neighbor.gscore + neighbor.hscore;
                }
            }

            else {
                if (gridNode.Y == 0) {
                    neighborX = gridNode.X;
                    neighborY = gridNode.Y + 1;
                }

                else {
                    neighborX = gridNode.X;
                    neighborY = gridNode.Y - 1;
                }
                
                neighborTileType = MatrixSerializer.map[neighborX, neighborY];
                    
                if (Grid_node_network.Exist(neighborX, neighborY)) {
                    neighbor = Grid_node_network.Get(neighborX, neighborY);
                    gridNode.Neighbors.Add(neighbor);
                }

                else {
                    neighbor = new Grid_node(neighborTileType, neighborX, neighborY);
                    Grid_node_network.Add(neighbor);
                    gridNode.Neighbors.Add(neighbor);
                }
                
                neighbor.gscore = gridNode.gscore + neighbor.getTypeCost();
                neighbor.hscore = gridNode.hscore - neighbor.getTypeCost();
                neighbor.fscore = neighbor.gscore + neighbor.hscore;
            }
        }
        
        public bool Equals(Grid_node gridNode) {
            return (gridNode.X == X && gridNode.Y == Y);
        }
    }
}