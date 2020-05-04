using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class InventariosServices
    {
        private InventariosBusiness BOInventarios;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Usuario
        /// </summary>
        public InventariosServices()
        {
            BOInventarios = new InventariosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "31010"; } }
        public string CodigoProgramaMC { get { return "31011"; } }
        public string CodigoProgramaComprobante { get { return "30810"; } }

        public List<ivcategoria> ListarCategoriasProductos(ivcategoria pIVCategoria, string pFiltro, Usuario pUsuario)
        {
            return BOInventarios.ListarCategoriasProductos(pIVCategoria, pFiltro, pUsuario);
        }

        public ParCueInventarios ConsultarParCueInventarios(Int64 pId, Usuario pUsuario)
        {
            return BOInventarios.ConsultarParCueInventarios(pId, pUsuario);
        }

        public ParCueInventarios CrearParCueInventarios(ParCueInventarios pConceptoCta, Usuario pusuario)
        {
            try
            {
                return BOInventarios.CrearParCueInventarios(pConceptoCta, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosServices", "CrearParCueInventarios", ex);
                return null;
            }
        }


        public ParCueInventarios ModificarParCueInventarios(ParCueInventarios pConceptoCta, Usuario pusuario)
        {
            try
            {
                return BOInventarios.ModificarParCueInventarios(pConceptoCta, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosServices", "ModificarParCueInventarios", ex);
                return null;
            }
        }


        public void EliminarParCueInventarios(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOInventarios.EliminarParCueInventarios(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosServices", "EliminarParCueInventarios", ex);
            }
        }

        public List<ivimpuesto> ListarImpuestos(ivimpuesto pIVProducto, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOInventarios.ListarImpuestos(pIVProducto, "", pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosServices", "ListarImpuestos", ex);
                return null;
            }
        }

        public ParCueIvimpuesto ModificarParCueIvimpuesto(ParCueIvimpuesto pParCueIvimpuesto, Usuario pUsuario)
        {
            try
            {
                return BOInventarios.ModificarParCueIvimpuesto(pParCueIvimpuesto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosServices", "ModificarParCueIvimpuesto", ex);
                return null;
            }
        }

        public List<ivmovimiento> ListarMovimiento(ivmovimiento pMovimiento, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOInventarios.ListarMovimiento(pMovimiento, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosServices", "ListarMovimiento", ex);
                return null;
            }
        }

        public List<ivmovimiento> ContabilizarOperacion(List<ivmovimiento> pLstCpnsulta, ref string pError, Usuario pusuario)
        {
            try
            {
                return BOInventarios.ContabilizarOperacion(pLstCpnsulta, ref pError, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosServices", "ContabilizarOperacion", ex);
                return null;
            }
        }

        public List<ivalmacen> ListarAlmacen(ivalmacen pIVAlmacen, Usuario pUsuario)
        {
            try
            {
                return BOInventarios.ListarAlmacen(pIVAlmacen, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosServices", "ListarAlmacen", ex);
                return null;
            }
        }

        public List<ivpersonas_autoret> ListarRetencion(string pPerDocumento, decimal pBase, Usuario pUsuario)
        {
            try
            {
                return BOInventarios.ListarRetencion(pPerDocumento, pBase, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosServices", "ListarRetencion", ex);
                return null;
            }
        }

        public List<ivconceptos> ListarConceptos(ivconceptos pConceptos, Usuario pUsuario)
        {
            try
            {
                List<ivconceptos> LstConceptos = new List<ivconceptos>();
                LstConceptos = BOInventarios.ListarConceptos(pConceptos, pUsuario);
                return LstConceptos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosServices", "ListarConceptos", ex);
                return null;
            }
        }

        public List<ivordenconcepto> ListarOrdenConceptos(Int64 pidOrden, double pBase, Usuario pUsuario)
        {
            try
            {
                List<ivordenconcepto> lstRetencion = new List<ivordenconcepto>();
                lstRetencion = BOInventarios.ListarOrdenConceptos(pidOrden, pBase, pUsuario);
                return lstRetencion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosServices", "ListarOrdenConceptos", ex);
                return null;
            }
        }

        public List<ivdatospago> ListarDatosPago(Int64 pCodProceso, Int64 pIdMovimiento, Usuario pUsuario)
        {
            return BOInventarios.ListarDatosPago(pCodProceso, pIdMovimiento, pUsuario);
        }




    }
}
