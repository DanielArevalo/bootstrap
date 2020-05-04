using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MovGralCreditoService
    {
        private MovGralCreditoBusiness busMovGralCredito;
        private ProductoBusiness busProducto;
        private ExcepcionBusiness excepBusinnes;

        public MovGralCreditoService()
        {
            busMovGralCredito = new MovGralCreditoBusiness();
            busProducto = new ProductoBusiness();
            excepBusinnes = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110103"; } }
        public string CodigoProgramaAvances { get { return "100512"; } }

        public List<Persona> ListarMovGral(Persona pEntityPersona, Usuario pUsuario)
        {
            return busMovGralCredito.ListarMovGralCredito(pEntityPersona, pUsuario);
        }

        public List<Producto> ListarProductos(Persona pEntityPersona, Usuario pUsuario)
        {
            return busProducto.ListarProductos(new Producto() { Persona = pEntityPersona }, pUsuario);
        }

        public List<Producto> ListarProductosMovGral(Persona pEntityPersona, Usuario pUsuario)
        {
            return busProducto.ListarProductosMovGral(new Producto() { Persona = pEntityPersona }, pUsuario);
        }



    }
}
