using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crm
{
    public class gamekedo
    {

        private static readonly Dictionary<string, int> PointTable = new Dictionary<string, int>

        {
            { "1to12",3},
            { "2to12",3},
            { "3to12",3},
            { "1to2",3},
            { "2to2",3},
            { "3to2",3},
            { "even",2},
            { "odd",2},


        };

        void Eval(List<Dictionary<string, int>> bet, int winNum)
        {
            //{ { "1to12",5},{ "6,7,8",5},{ "even",5},{ "red",5} }

            //return points;
        }
    }


}