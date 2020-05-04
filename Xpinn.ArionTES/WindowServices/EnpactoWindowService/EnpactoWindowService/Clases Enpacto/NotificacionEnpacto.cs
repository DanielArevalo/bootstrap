using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnpactoWindowService.Clases_Enpacto
{
    public class NotificacionEnpacto
    {
        public List<trans> trans { get; set; }
    }

    public class NotificacionCoopcentral
    {
        public List<tran> trans { get; set; }

        public class tran : transCoopcentral { }

    }

}
