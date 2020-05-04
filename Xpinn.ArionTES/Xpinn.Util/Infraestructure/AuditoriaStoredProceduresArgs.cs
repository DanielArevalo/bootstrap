using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.Util
{
    public class AuditoriaStoredProceduresArgs : EventArgs
    {
        public AuditoriaStoredProcedures AuditoriaEntity { get; set; }

        public AuditoriaStoredProceduresArgs(AuditoriaStoredProcedures auditoria)
        {
            AuditoriaEntity = auditoria;
        }
    }
}
