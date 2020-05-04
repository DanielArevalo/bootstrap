using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para FabricaCreditos
    /// </summary>
    public class DatosSolicitudBusiness : GlobalData
    {
        private DatosSolicitudData DASolicitud;

        /// <summary>
        /// Constructor del objeto de negocio para Solicitud
        /// </summary>
        public DatosSolicitudBusiness()
        {
            DASolicitud = new DatosSolicitudData();
        }

        /// <summary>
        /// Crea un Solicitud
        /// </summary>
        /// <param name="pEntity">Entidad Solicitud</param>
        /// <returns>Entidad creada</returns>
        public DatosSolicitud CrearSolicitud(DatosSolicitud pSolicitud, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSolicitud = DASolicitud.CrearSolicitud(pSolicitud, pUsuario);

                    ts.Complete();
                }

                return pSolicitud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudBusiness", "CrearSolicitud", ex);
                return null;
            }
        }





        public DatosSolicitud ModificarSolicitudes(DatosSolicitud pSolicitud, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSolicitud = DASolicitud.ModificarSolicitudes(pSolicitud, pUsuario);

                    ts.Complete();
                }

                return pSolicitud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudBusiness", "ModificarSolicitudes", ex);
                return null;
            }
        }

        public DatosSolicitud ModificarSolicitudRotativo(DatosSolicitud pSolicitud, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSolicitud = DASolicitud.ModificarSolicitudRotativo(pSolicitud, pUsuario);

                    ts.Complete();
                }

                return pSolicitud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudBusiness", "ModificarSolicitud", ex);
                return null;
            }
        }
        /// <summary>
        /// Crea un Solicitud
        /// </summary>
        /// <param name="pEntity">Entidad Solicitud</param>
        /// <returns>Entidad creada</returns>
        public DatosSolicitud CrearRadicadoRotativo(DatosSolicitud pSolicitud, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSolicitud = DASolicitud.CrearRadicadoRotativo(pSolicitud, pUsuario);

                    ts.Complete();
                }

                return pSolicitud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudBusiness", "CrearRadicadoRotativo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Persona1 cedula
        /// </summary>
        /// <param name="pId">Identificador de Persona1</param>
        /// <returns>Entidad Persona1</returns>
        public DatosSolicitud ListarDatosSolicitud(DatosSolicitud pDatosSolicitud, Usuario pUsuario)
        {
            try
            {
                return DASolicitud.ListarDatosSolicitud(pDatosSolicitud, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudBusiness", "ListarDatosSolicitud", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un Persona1 cedula
        /// </summary>
        /// <param name="pId">Identificador de Persona1</param>
        /// <returns>Entidad Persona1</returns>
        public List<DatosSolicitud> ListarLineasCredito(Usuario pUsuario)
        {
            try
            {
                return DASolicitud.ListarLineasCredito(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudBusiness", "ListarLineasCredito", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para obtener Persona1 por parametros
        /// </summary>
        /// <param name="pId">identificador de Persona1</param>
        /// <returns>Entidad Persona1</returns>
        public DatosSolicitud ListarSolicitudCrtlTiempos(DatosSolicitud pDatosSolicitud, Usuario pUsuario)
        {
            try
            {
                return DASolicitud.ListarSolicitudCrtlTiempos(pDatosSolicitud, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersona1", ex);
                return null;
            }
        }


        public DatosSolicitud ValidarSolicitud(DatosSolicitud pSolicitud, Usuario pUsuario, ref string sError)
        {
            try
            {
                return DASolicitud.ValidarSolicitud(pSolicitud, pUsuario, ref sError);
            }
            catch
            {
                return pSolicitud;
            }
        }

        public DatosSolicitud ValidarSolicitudRotativo(DatosSolicitud pSolicitud, Usuario pUsuario, ref string sError)
        {
            try
            {
                return DASolicitud.ValidarSolicitudRotativo(pSolicitud, pUsuario, ref sError);
            }
            catch
            {
                return pSolicitud;
            }
        }

        public DatosSolicitud ConsultarCliente(String pId, Usuario pUsuario)
        {
            try
            {
                DatosSolicitud Aporte = new DatosSolicitud();

                Aporte = DASolicitud.ConsultarCliente(pId, pUsuario);

                return Aporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DASolicitudBusiness", "ConsultarCliente", ex);
                return null;
            }
        }

        public DatosSolicitud ConsultarParametroEdadMinima(Usuario pUsuario)
        {
            try
            {
                DatosSolicitud Aporte = new DatosSolicitud();

                Aporte = DASolicitud.ConsultarParametroEdadMinima(pUsuario);

                return Aporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DASolicitudBusiness", "ConsultarParametroEdadMinima", ex);
                return null;
            }
        }


        public DatosSolicitud ConsultarParametroEdadMaxima(Usuario pUsuario)
        {
            try
            {
                DatosSolicitud Aporte = new DatosSolicitud();

                Aporte = DASolicitud.ConsultarParametroEdadMaxima(pUsuario);

                return Aporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DASolicitudBusiness", "ConsultarParametroEdadMaxima", ex);
                return null;
            }
        }

        public DatosSolicitud ValidarCliente(DatosSolicitud pSolicitud, Usuario pUsuario, ref string sError)
        {
            try
            {
                return DASolicitud.ValidarCliente(pSolicitud, pUsuario, ref sError);
            }
            catch
            {
                return pSolicitud;
            }
        }


        public DatosSolicitud ConsultarSolicitudCreditos(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DASolicitud.ConsultarSolicitudCreditos(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DASolicitudBusiness", "ConsultarSolicitudCreditos", ex);
                return null;
            }
        }

        public int ConsultarCreditosActivosXLinea(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DASolicitud.ConsultarCreditosActivosXLinea(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudBusiness", "ConsultarCreditosActivosXLinea", ex);
                return 0;
            }
        }

        public int? ConsultarCreditosPermitidosXLinea(string cod_linea, Usuario vUsuario)
        {
            try
            {
                return DASolicitud.ConsultarCreditosPermitidosXLinea(cod_linea, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudBusiness", "ConsultarCreditosPermitidosXLinea", ex);
                return null;
            }
        }

        public string ConsultaValorGarantia(string num_radica, Usuario pUsuario)
        {
            try
            {
                return DASolicitud.ConsultaValorGarantia(num_radica, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "ConsultaValorGarantia", ex);
                return null;
            }
        }
    }
}
