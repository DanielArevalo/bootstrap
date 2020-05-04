using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class AsaUdModificacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public AsaUdModificacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

    }
}