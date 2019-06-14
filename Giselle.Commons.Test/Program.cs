using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giselle.Commons;

namespace Giselle.Commons.Test
{
    public class Program
    {
        public static event EventHandler<EventArgs> Test;

        public static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Listen("TEST" + i);
            }

            Test?.Invoke(null, new EventArgs());
        }

        public static void Listen(string name)
        {
            void wrapper(object sender, EventArgs e)
            {
                Test -= wrapper;
                Console.WriteLine(name);
            };

            Test += wrapper;
        }

    }

}
