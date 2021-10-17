using System.Collections.Generic;
using System.Numerics;

namespace LOTR {
    public class A_star {
        private List<Grid_node> Open;
        private List<Grid_node> Closed;
        private List<Grid_node> Objectives;

        private Grid_node Shire;
        private Grid_node MountainOfDoom;
        
        public A_star(Vector2 ShireLocation, Vector2 MountainOfDoomLocation) {
            Open = new List<Grid_node>();
            Closed = new List<Grid_node>();
            Objectives = new List<Grid_node>();

            int ShireX = (int) ShireLocation.X;
            int ShireY = (int) ShireLocation.Y;
            
            int MountainOfDoomX = (int) MountainOfDoom.X;
            int MountainOfDoomY = (int) MountainOfDoom.Y;

            Shire = new Grid_node('.', ShireX, ShireY);
            MountainOfDoom = new Grid_node('.', MountainOfDoomX, MountainOfDoomY);
        }
        
        //Main Loop from shire to mountDoom and back
        public void ToMountDoomAndBack() {
            foreach (Grid_node Objective in Objectives) {
                
            }
        }

        //A* algorithim implementation
        public void FromSourceToDestiny(Grid_node source, Grid_node destiny) {
            
        }

    }
}
