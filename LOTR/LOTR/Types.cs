using System;
using System.Collections.Generic;

namespace LOTR {
    public class Types {
        
        public static Dictionary<char, int> types = new Dictionary<char, int>() {{'.', 1}, {'R', 5},
            {'F', 10}, {'M', 50}, {'P', Int32.MaxValue}, {'#', Int32.MaxValue}};
        
    }
}