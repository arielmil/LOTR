using System;
using System.Collections.Generic;

namespace LOTR {
    public static class Types {

        public enum neighborConstrains {
            XEqual0,
            XGreaterThen0,
            XEqualMax,
            YEqual0,
            YGreaterThen0,
            YEqualMax
        }
        
        public static Dictionary<char, int> types = new Dictionary<char, int>() {{'.', 1}, {'R', 5},
            {'F', 10}, {'M', 50}, {'P', Int32.MaxValue}, {'#', Int32.MaxValue}};
        
    }
}