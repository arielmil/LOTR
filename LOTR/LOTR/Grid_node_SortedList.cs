using System;
using System.Collections.Generic;

namespace LOTR {
    public class Grid_node_SortedList {
        private static Random randomizer;
        private static NodeComparer Comparer;
        
        private List<Grid_node> Grid_NodeList;
        
        public int Count => Grid_NodeList.Count;

        public Grid_node_SortedList() {
            Grid_NodeList = new List<Grid_node>();
            
            if (randomizer == null) {
                randomizer = new Random();
            }

            if (Comparer == null) {
                Comparer = new NodeComparer(randomizer);
            }
        }
        
        public void Add(Grid_node gridNode) {
            int lowerBoundIndex = 0;
            int higherBoundIndex = Count;
            int mid;

            int comparerReturnValue;

            if (Contains(gridNode)) {
                return;
            }
            
            while (lowerBoundIndex < higherBoundIndex) {
                mid = (lowerBoundIndex + higherBoundIndex) / 2;
                comparerReturnValue = Comparer.Compare(Grid_NodeList[mid], gridNode);
                
                if (comparerReturnValue == -1) {
                    lowerBoundIndex = mid + 1;
                }

                else {
                    higherBoundIndex = mid;
                }
            }
            
            Grid_NodeList.Insert(lowerBoundIndex, gridNode);
        }

        public bool Contains(Grid_node gridNode) {
            int index = Grid_NodeList.BinarySearch(gridNode, Comparer);
            return index >= 0;
        }

        public Grid_node PopMin() {
            if (Grid_NodeList.Count == 0) {
                throw (new Exception("Error: List is empty, so no item is min item."));
            }
            
            Grid_node returnValue = Grid_NodeList[0];
            Grid_NodeList.Remove(returnValue);

            return returnValue;
        }
    }
}