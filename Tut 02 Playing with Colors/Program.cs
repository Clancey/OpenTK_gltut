using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenTKSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if Fragment
            new FragPosition().Run(60);
#else
            new VertexColors().Run(60);
#endif
        }
    }
}
