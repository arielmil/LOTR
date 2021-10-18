using System;
using System.Collections.Generic;

namespace LOTR {
    public static class Types {

        public enum heuristicMethod {
            Manhattan,
            Euclidian
        }
        
        public static Dictionary<char, int> tileTypeToInt = new Dictionary<char, int>() {{'.', 1}, {'R', 5},
            {'F', 10}, {'M', 50}, {'P', Int32.MaxValue}, {'#', Int32.MaxValue}};
        
    }
}