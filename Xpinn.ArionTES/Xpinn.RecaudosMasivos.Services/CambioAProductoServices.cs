using System;
using System.Collections.Generic;
using System.Data;using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;
using Xpinn.FabricaCreditos.Entities;


namespace Xpinn.Tesoreria.Services
{
    public class CambioAProductoServices : GlobalData
    {

        private CambioAProductoBusiness BOCambio;
        private ExcepcionBusiness BOExcepcion;


        public string CodigoPrograma { get { return "180105"; } }

        /// <summary>
        /// Constructor del servicio para Acceso
        /// </summary>
        public CambioAProductoServices()
        {
            BOCambio = new CambioAProductoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public CambioAProducto ModificarProducto(CambioAProducto pEntidad, String tabla, Usuario vUsuario)
        {
            try
            {
                return BOCambio.ModificarProducto(pEntidad, tabla, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioAProductoServices", "ModificarProducto", ex);
                return null;
            }
        }


        public List<PersonaEmpresaRecaudo> ListarPersonaEmpresaRecaudo(PersonaEmpresaRecaudo pPersonaEmpresaRecaudo, Usuario vUsuario)
        {
            try
            {
                return BOCambio.ListarPersonaEmpresaRecaudo(pPersonaEmpresaRecaudo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioAProductoServices", "ListarPersonaEmpresaRecaudo", ex);
                return null;
            }
        }

        public List<CambioAProducto> ListarCreditoEmpresa_Recaudo(Int64 pNumRadicacion, Usuario vUsuario)
        {
            try
            {
                return BOCambio.ListarCreditoEmpresa_Recaudo(pNumRadicacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioAProductoServices", "ListarCreditoEmpresa_Recaudo", ex);
                return null;
            }
        }


        public CambioAProducto ConsultarFormaDePagoProducto(String pId, String tabla, Usuario vUsuario)
        {
            try
            {
                return BOCambio.ConsultarFormaDePagoProducto(pId, tabla, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioAProductoServices", "ConsultarFormaDePagoProducto", ex);
                return null;
            }
        }

    }
}


