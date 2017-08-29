using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class TexadItemAttribute
    {
        public string name;
        public string currentValue;
        public string maxValue;

        public TexadItemAttribute(string n, string cv = "none", string mv = "none")
        {
            name = n;
            currentValue = cv;
            maxValue = mv;
        }
    }

    public class QuantityAttribute : TexadItemAttribute
    {
        public QuantityAttribute(string cv, string n = "Quantity") :base(n, cv)
        {

        }
    }

}
