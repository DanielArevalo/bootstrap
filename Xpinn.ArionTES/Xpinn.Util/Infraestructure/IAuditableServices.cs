using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.Util
{
    public interface IAuditableServices
    {
        AuditoriaStoredProcedures CrearAuditoriaStoredProcedures(AuditoriaStoredProcedures entity, Usuario usuario);
    }
}
