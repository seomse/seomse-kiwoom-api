using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Model.Def
{
    class Separator
    {
        public static char FIELD { get { return ','; } }
        public static char DATA { get { return '|'; } }
        public static string[] ParseField(string message)
        {
            return message.Split(FIELD);
        }

        public static string[] ParseData(string message)
        {
            return message.Split(DATA);
        }

    }
}
