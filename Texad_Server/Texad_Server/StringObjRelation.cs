using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texad_Server
{
    public class StringObjRelation
    {
        public string objString;
        public object strObject;

        public StringObjRelation(string s, object o)
        {
            objString = s;
            strObject = o;
        }

        public object objFromStr(string s)
        {
            if (s.Equals(objString))
                return strObject;
            else
                return null;
        }

        public string strFromObject(object o)
        {
            if (o.Equals(strObject))
                return objString;
            else
                return null;
        }
    }
}
