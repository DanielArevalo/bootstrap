using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.Util
{
    public class GlobalPackage
    {
        public void MensajeConsola(string mensaje)
        {
            Console.WriteLine("====================== Mensaje Expinn ====================================");
            Console.WriteLine(mensaje);
            Console.WriteLine("==========================================================================");
        }

        public void Mensaje_Error(string mensaje)
        {
            throw new InvalidOperationException(mensaje);
        }
    }
}
