using System;
using System.Web.Http;
using Xpinn.Util;

namespace Xpinn.Synchronization
{
    public class GlobalApiController : ApiController
    {
        public Usuario Usuario { get; set; }

        public GlobalApiController()
        {
            Conexion conexion = new Conexion();
            Usuario = conexion.DeterminarUsuarioSinClave("COOTREGUA");

            if (Usuario == null) throw new NullReferenceException("No se pudo determinar el usuario default para ejecutar las consultas (GlobalApiController)");
        }
    }
}