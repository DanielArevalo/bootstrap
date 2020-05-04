using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para PolizasSeguros
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PolizasSegurosService
    {
        private Xpinn.FabricaCreditos.Business.PolizasSegurosBusiness BOPolizasSeguros;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "100103"; } }
        public string CodigoPrograma1 { get { return "100147"; } }
        /// <summary>
        /// Constructor del servicio para PolizasSeguros
        /// </summary>
        public PolizasSegurosService()
        {
            BOPolizasSeguros = new PolizasSegurosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        public int numero_radicacion;
        public int CodigoPoliza;

        /// <summary>
        /// Obtiene la lista de PolizasSeguros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PolizasSeguros obtenidos</returns>
        public List<PolizasSeguros> ListarPolizasSeguros(PolizasSeguros pPolizasSeguros, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOPolizasSeguros.ListarPolizasSeguros(pPolizasSeguros, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "ListarPolizasSeguros", ex);
                return null;
            }
        }
        public List<PolizasSeguros> ListarPolizassinSeguros(PolizasSeguros pPolizasSeguros, Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.ListarPolizassinSeguros(pPolizasSeguros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "ListarPolizasSeguros", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de PolizasSeguros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PolizasSeguros obtenidos</returns>
        public PolizasSeguros ConsultarPolizasSegurosValidacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.ConsultarPolizasSegurosValidacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "ConsultarPolizasSegurosValidacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de PolizasSeguros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PolizasSeguros obtenidos</returns>
        public List<PolizasSegurosVida> ListarPolizasSegurosVida(PolizasSegurosVida pPolizasSegurosvida, Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.ListarPolizasSegurosVida(pPolizasSegurosvida, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "ListarPolizasSegurosVida", ex);
                return null;
            }
        }
        /// <summary>
        /// Modifica una PolizaSeguros
        /// </summary>
        /// <param name="pEntity">Entidad PolizaSeguros</param>
        /// <returns>Entidad modificada</returns>
        public PolizasSeguros ModificarPolizasSeguros(PolizasSeguros pPolizasSeguros, Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.ModificarPolizasSeguros(pPolizasSeguros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "ModificarPolizasSeguros", ex);
                return null;
            }

        }


        /// <summary>
        /// Modifica una PolizaSegurosVida
        /// </summary>
        /// <param name="pEntity">Entidad PolizaSeguros</param>
        /// <returns>Entidad modificada</returns>
        public PolizasSegurosVida ModificarPolizasSegurosVida(PolizasSegurosVida pPolizasSegurosVida, Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.ModificarPolizasSegurosVida(pPolizasSegurosVida, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosVidaService", "ModificarPolizasSegurosVida", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina un PolizasSeguros
        /// /// </summary>
        /// <param name="pId">identificador del PolizasSeguros</param>
        public void EliminarPolizasSeguros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOPolizasSeguros.EliminarPolizasSeguros(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "EliminarPolizasSeguros", ex);
            }
        }
        
        
        /// <summary>
        /// Obtiene un PolizasSeguros
        /// </summary>
        /// <param name="pId">identificador de PolizasSeguros</param>
        /// <returns>PolizasSeguros consultada</returns>
        public PolizasSeguros ConsultarPolizasSeguros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.ConsultarPolizasSeguros(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "ConsultarPolizasSeguros", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un dato de la tabla general para edad
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Paraemtro consultada</returns>
        public PolizasSeguros ConsultarParametroEdad(Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.ConsultarParametroEdad(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "ConsultarParametroEdad", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un dato de la tabla general para edad de los beneficiarios
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Paraemtro consultada</returns>
        public PolizasSeguros ConsultarParametroEdadBeneficiarios(Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.ConsultarParametroEdadBeneficiarios(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "ConsultarParametroEdadBeneficiarios", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un PolizasSeguros
        /// </summary>
        /// <param name="pId">identificador de PolizasSeguros</param>
        /// <returns>PolizasSeguros consultada</returns>
        public PolizasSegurosVida ConsultarPolizasSegurosVida(Int64 pId, String tipo, Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.ConsultarPolizasSegurosVida(pId,tipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "ConsultarPolizasSegurosVida", ex);
                return null;
            }
        }
        /// <summary>
        /// ObtieneDatso del desembolso
        /// </summary>
        /// <param name="pId">identificador del desembolso</param>
        /// <returns>Credito consultada</returns>
        public PolizasSeguros ConsultarCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.ConsultarCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "ConsultarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// ObtieneDatso del desembolso
        /// </summary>
        /// <param name="pId">identificador del desembolso</param>
        /// <returns>Credito consultada</returns>
        public List<PolizasSeguros> FiltrarCredito(PolizasSeguros pEntidad, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOPolizasSeguros.FiltrarCredito(pEntidad, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "FiltrarCredito", ex);
                return null;
            }
        }


        /// <summary>
        /// Crea una PolizaSeguros
        /// </summary>
        /// <param name="pEntity">Entidad oficina</param>
        /// <returns>Entidad creada</returns>
        public PolizasSeguros CrearPolizasSeguros(PolizasSeguros pPolizaSeguros, Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.CrearPolizasSeguros(pPolizaSeguros, pUsuario);
            }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("PolizasSegurosService", "CrearPolizasSeguros", ex);
                    return null;
                }
        }

        public PolizasSegurosVida CrearPolizasSegurosVida(PolizasSegurosVida pPolizaSegurosVida, Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.CrearPolizasSegurosVida(pPolizaSegurosVida, pUsuario);
            }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("PolizasSegurosService", "CrearPolizasSegurosVida", ex);
                    return null;
                }
        }


        /// <summary>
        /// Obtiene la lista de ParentescoBeneficiarios dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ParentescoBeneficiarios obtenidos</returns>
        public List<ParentescoPolizas> ListarParentesco(ParentescoPolizas pBeneficiarios, Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.ListarParentesco(pBeneficiarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("parentescoBenefService", "ListarParentesco", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de ParentescoBeneficiarios dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ParentescoBeneficiarios obtenidos</returns>
        public List<ParentescoPolizas> ListarParentescofamiliares(ParentescoPolizas pBeneficiarios, Usuario pUsuario)
        {
            try
            {
                return BOPolizasSeguros.ListarParentescofamiliares(pBeneficiarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("parentescoBenefService", "ListarParentesco", ex);
                return null;
            }
        }



        public int CalcularMeses(DateTime fechaComienzo, DateTime fechaFin)
        {
             try
            {
                return BOPolizasSeguros.CalcularMeses(fechaComienzo, fechaFin);
            }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("PolizasSegurosService", "CalcularMesesPolizasSeguros", ex);
                    return 0;
                }
        
        }



        public Int64 CalcularEdad2(DateTime fechaNacimiento)
        {
            try
            {
                return BOPolizasSeguros.CalcularEdad2(fechaNacimiento);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosService", "CalcularEdadPolizasSeguros", ex);
                return 0;
            }

        }
        
        
    }
 }