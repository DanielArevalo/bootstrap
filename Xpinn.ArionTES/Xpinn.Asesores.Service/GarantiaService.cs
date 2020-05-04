using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using System.Web;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.Asesores.Services
{
    public class GarantiaService
    {
        private GarantiasBusiness BOGarantias;
        private ExcepcionBusiness BOExcepcion;

        public GarantiaService(){
            BOGarantias = new GarantiasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110118"; } }
        public string CodigoPrograma2 { get { return "100144"; } }


        public List<Garantia> ListarCodeudores(Int64 pNumeroRadicacion,Usuario pUsuario)
        {
            try{
                return BOGarantias.ListarGarantia(pNumeroRadicacion, pUsuario);
            }
            catch (Exception ex){
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }
        public List<Garantia> ListarSinGarantias(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.ListarSinGarantias(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para obtener lista de Garantias Maestras a partir de unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Garantias obtenidos CRUD</returns>
        public List<Garantia> ListarFullGarantias(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.ListarFullGarantias(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiaService", "ListarFullGarantias", ex);
                return null;
            }
        }

        public string ConsultarCliente(string nradicacion, Usuario _usuario)
        {
            try
            {
                return BOGarantias.ConsultarCliente(nradicacion, _usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiaService", "ConsultarCliente", ex);
                return null;
            }
        }

        public List<Garantia> Listaractivos(string cod_persona, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.Listaractivos(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiaService", "ListarFullGarantias", ex);
                return null;
            }
        }


        public ActivoFijo CrearActivoFijoPersonal(ActivoFijo vActivoFijo, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.CrearActivoFijoPersonal(vActivoFijo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiaService", "CrearActivoFijoPersonal", ex);
                return null;
            }
        }

        public ActivoFijo ConsultarActivoFijoPersonal(long idActivoFijo, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.ConsultarActivoFijoPersonal(idActivoFijo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiaService", "ConsultarActivoFijoPersonal", ex);
                return null;
            }
        }

        public bool EliminarActivoFijo(Int64 pId, Int64 pNum_Radicacion, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.EliminarActivoFijo(pId, pNum_Radicacion, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiaService", "EliminarActivoFijo", ex);
                return false;
            }
        }

        public void EliminarGarantia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOGarantias.EliminarGarantia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiaService", "EliminarGarantia", ex);
            }
        }



        /// <summary>
        /// Crea una Garantia
        /// </summary>
        /// <param name="pEntity">Entidad Garantia</param>
        /// <returns>Entidad creada</returns>
        public Garantia CrearGarantia(Garantia pGarantia, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.CrearGarantia(pGarantia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiaService", "CrearGarantia", ex);
                return null;
            }
        }


        /// <summary>
        /// Modificar una Garantia
        /// </summary>
        /// <param name="pEntity">Entidad Garantia</param>
        /// <returns>Entidad creada</returns>
        public Garantia ModificarGarantia(Int16 origen,Garantia pGarantia, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.ModificarGarantia(origen,pGarantia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiaService", "ModificarGarantia", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene una Garantia
        /// </summary>
        /// <param name="pId">identificador de la Garantia</param>
        /// <returns>Garantia consultada</returns>
        public Garantia ConsultarGarantia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.ConsultarGarantia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiaService", "ConsultarGarantia", ex);
                return null;
            }
        }

        public Garantia ConsultarCreditoCliente(Int64 pnum, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.ConsultarCreditoCliente(pnum, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiaService", "ConsultarCreditoCliente", ex);
                return null;
            }
        }

        public void ModificarActivoFijoPersonal(ActivoFijo activoFijo, Usuario usuario)
        {
            try
            {
                BOGarantias.ModificarActivoFijoPersonal(activoFijo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiaService", "ModificarActivoFijoPersonal", ex);
            }
        }
    }
}
