using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_DBHExtraction
{
    class Point
    {
        private Double x;
        private Double y;
        private Double z;
        private Double comp;
       
        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public double Z
        {
            get
            {
                return z;
            }

            set
            {
                z = value;
            }
        }

        public double Comp
        {
            get
            {
                return comp;
            }

            set
            {
                comp = value;
            }
        }
       
        public Point(Double X, Double Y, Double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.comp = 0;          
        }
    }
}
