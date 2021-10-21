using System;
using System.Collections.Generic;

namespace LOTR {
    public class NodeComparer : IComparer<Grid_node> {
        private static Random randomizer;
        
        private bool debug;

        public NodeComparer(Random randomizer, bool debug = false) {
            this.debug = debug;
            NodeComparer.randomizer = randomizer;
        }
        public int Compare(Grid_node node1, Grid_node node2) {
            if (node2 != null && node1 != null && node1.f < node2.f) {
                return -1;
            }
                
            if (node2 != null && node1 != null && node1.f == node2.f) {
                if (node1.h < node2.h) {
                    return -1;
                }
                    
                if (node1.h > node2.h) {
                    return 1;
                }

                if (node1.h == node2.h) {
                    if (!node1.Equals(node2)) {
                        return randomizer.Next(-1, 2);
                    }

                    return 0;
                }
                    
                return 0;
            }

            return 1;
        }
    }
}