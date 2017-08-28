using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class TexadBehavior
    {
        public TexadActor owner;

        public TexadBehavior(TexadActor owner)
        {
            this.owner = owner;
        }
    }
}
