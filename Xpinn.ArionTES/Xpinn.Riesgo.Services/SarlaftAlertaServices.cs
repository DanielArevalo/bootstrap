using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Business;
using Xpinn.Interfaces.Entities;

namespace Xpinn.Riesgo.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SarlaftAlertaServices
    {

        private SarlaftAlertaBusiness BOSarlaftAlerta;
        private ExcepcionBusiness BOExcepcion;

        public SarlaftAlertaServices()
        {
            BOSarlaftAlerta = new SarlaftAlertaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "270103"; } }

        public SarlaftAlerta CrearSarlaftAlerta(SarlaftAlerta pSarlaftAlerta, Usuario pusuario)
        {
            try
            {
                pSarlaftAlerta = BOSarlaftAlerta.CrearSarlaftAlerta(pSarlaftAlerta, pusuario);
                return pSarlaftAlerta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaService", "CrearSarlaftAlerta", ex);
                return null;
            }
        }


        public SarlaftAlerta ModificarSarlaftAlerta(SarlaftAlerta pSarlaftAlerta, Usuario pusuario)
        {
            try
            {
                pSarlaftAlerta = BOSarlaftAlerta.ModificarSarlaftAlerta(pSarlaftAlerta, pusuario);
                return pSarlaftAlerta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaService", "ModificarSarlaftAlerta", ex);
                return null;
            }
        }


        public void EliminarSarlaftAlerta(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOSarlaftAlerta.EliminarSarlaftAlerta(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaService", "EliminarSarlaftAlerta", ex);
            }
        }


        public SarlaftAlerta ConsultarSarlaftAlerta(Int64 pId, Usuario pusuario)
        {
            try
            {
                SarlaftAlerta SarlaftAlerta = new SarlaftAlerta();
                SarlaftAlerta = BOSarlaftAlerta.ConsultarSarlaftAlerta(pId, pusuario);
                return SarlaftAlerta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaService", "ConsultarSarlaftAlerta", ex);
                return null;
            }
        }


        public List<SarlaftAlerta> ListarSarlaftAlerta(SarlaftAlerta pSarlaftAlerta, DateTime? pFecIni, DateTime? pFecFin, int pOrden, Usuario pusuario)
        {
            try
            {
                return BOSarlaftAlerta.ListarSarlaftAlerta(pSarlaftAlerta, pFecIni, pFecFin, pOrden, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaService", "ListarSarlaftAlerta", ex);
                return null;
            }
        }

        /// <summary>
        /// Creación de registro de las consultas a listas restrictivas por persona
        /// </summary>
        /// <param name="lstOFAC">Listado de registros consulta OFAC</param>
        /// <param name="lstONUInd">Listado de registros consulta ONU Individual</param>
        /// <param name="lstONUEnt">Listado de registros consulta ONU Entity</param>
        /// <param name="cod_persona">Código de la persona</param>
        /// <param name="pusuario"></param>
        public void CrearRegistroConsultaLista(List<TradeUSSearchInd> lstOFAC, List<Individual> lstONUInd, List<Entity> lstONUEnt, Int64 cod_persona, bool nuevo, bool reg, bool pro, bool con, bool ru, bool poli,  Usuario pusuario)
        {
            try
            {
                BOSarlaftAlerta.CrearRegistroConsultaLista(lstOFAC, lstONUInd, lstONUEnt, cod_persona, nuevo, reg, pro, con, ru, poli, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaService", "CrearRegistroConsultaLista", ex);
            }
        }

        /// <summary>
        /// Consultar si la persona se encuentra reportada
        /// </summary>
        /// <param name="pId">Código de la persona</param>
        /// <param name="pusuario"></param>
        /// <returns>Valor booleano</returns>
        public bool ConsultarReportePersona(Int64 pId, Usuario pusuario)
        {
            try
            {
                return BOSarlaftAlerta.ConsultarReportePersona(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaService", "ConsultarReportePersona", ex);
                return false;
            }
        }

        /// <summary>
        /// Listar personas que ya fueron consultadas en listas restrictivas
        /// </summary>
        /// <param name="filtro">Filtro para listado</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<SarlaftAlerta> ListarPersonasConsultadas(string filtro, bool pUltimo, Usuario pusuario)
        {
            try
            {
                return BOSarlaftAlerta.ListarPersonasConsultadas(filtro, pUltimo, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaService", "ListarPersonasConsultadas", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica el estado de la persona si se encuentra reportada en listas restrictivas
        /// </summary>
        /// <param name="cod_persona">Código del asociado</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public void ModificarEstadoPersona(Int64 cod_persona, Usuario pusuario)
        {
            try
            {
                BOSarlaftAlerta.ModificarEstadoPersona(cod_persona, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaService", "ModificarEstadoPersona", ex);
            }
        }

        public List<SarlaftAlerta> ListarPersonasParaConsultar(string filtro, Usuario pusuario)
        {
            try
            {
                return BOSarlaftAlerta.ListarPersonasParaConsultar(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaService", "ListarPersonasParaConsultar", ex);
                return null;
            }
        }



    }
}