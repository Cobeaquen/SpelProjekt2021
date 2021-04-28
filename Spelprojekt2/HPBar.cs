using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelprojekt2
{
    public class HPBar
    {
        private float value;
        public HPBar()
        {
            value = 1f;
        }

        public void SetValue(float value)
        {
            this.value = value;
        }
    }
}
