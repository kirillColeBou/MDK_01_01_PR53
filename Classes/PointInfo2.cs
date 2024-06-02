using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace PermDynamics_Тепляков.Classes
{
    public class PointInfo2
    {
        public double value { get; set; }
        public Line line { get; set; }
        public PointInfo2(double value) => this.value = value;
    }
}
