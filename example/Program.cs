using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.common;
using jmessage.util;
using jmessage.user;
using jmessage;

namespace example
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable ht = new Hashtable();
            ht.Add("1", "1111");
            ht.Add("2", "2222");
            ht.Add("3", "3333");
            ht.Add("4", "4444");
            List<string> add = new List<string> { "jmessage123" };
            ht.Add("5", add);
        }
        }
}
