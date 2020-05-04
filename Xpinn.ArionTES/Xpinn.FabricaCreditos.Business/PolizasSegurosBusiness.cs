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
    /// Objeto de negocio para PolizasSeguros
    /// </summary>
    /// 
    public class PolizasSegurosBusiness : GlobalData
    {

        private PolizasSegurosData  DAPolizasSeguros;

        /// <summary>
        /// Constructor del objeto de negocio para PolizasSeguros
        /// </summary>
        public PolizasSegurosBusiness()
        {
            DAPolizasSeguros = new PolizasSegurosData();
        }

        /// <summary>
        /// Obtiene la lista de PolizasSeguros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PolizasSeguros obtenidos</returns>
        public List<PolizasSeguros> ListarPolizasSeguros(PolizasSeguros pPolizasSeguros, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAPolizasSeguros.ListarPolizasSeguros(pPolizasSeguros, pUsuario, filtro); 
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "ListarPolizasSeguros", ex);
                return null;
            }
        }
        public List<PolizasSeguros> ListarPolizassinSeguros(PolizasSeguros pPolizasSeguros, Usuario pUsuario)
        {
            try
            {
                return DAPolizasSeguros.ListarPolizassinSeguros(pPolizasSeguros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "ListarPolizasSeguros", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de PolizasSeguros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PolizasSeguros obtenidos</returns>
        public PolizasSeguros ConsultarPolizasSegurosValidacion(Int64 pId,  Usuario pUsuario)
        {
            try
            {
                return DAPolizasSeguros.ConsultarPolizasSegurosValidacion(pId, pUsuario); 
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "ConsultarPolizasSegurosValidacion", ex);
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
                return DAPolizasSeguros.ListarPolizasSegurosvida(pPolizasSegurosvida, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "ListarPolizasSegurosVida", ex);
                return null;
            }
        }
        /// <summary>
        /// Modifica una PolizaSeguros
        /// /// </summary>
        /// <param name="pEntity">Entidad PolizaSeguros</param>
        /// <returns>Entidad modificada</returns>
        public PolizasSeguros ModificarPolizasSeguros(PolizasSeguros pPolizasSeguros, Usuario pUsuario)
        {
            try
            {
               // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                    pPolizasSeguros = DAPolizasSeguros.ModificarPolizasSeguros(pPolizasSeguros, pUsuario);

                   // ts.Complete();
               // }

                return pPolizasSeguros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "ModificarPolizasSeguros", ex);
                return null;
            }

        }

        /// <summary>
        /// Modifica una PolizaSegurosVida
        /// /// </summary>
        /// <param name="pEntity">Entidad PolizaSeguros</param>
        /// <returns>Entidad modificada</returns>
        public PolizasSegurosVida ModificarPolizasSegurosVida(PolizasSegurosVida pPolizasSegurosVida, Usuario pUsuario)
        {
            try
            {
                // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                pPolizasSegurosVida = DAPolizasSeguros.ModificarPolizasSegurosVida(pPolizasSegurosVida, pUsuario);

                // ts.Complete();
                // }

                return pPolizasSegurosVida;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosVidaBusiness", "ModificarPolizasSegurosVida", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina una PolizasSeguros
        /// </summary>
        /// <param name="pId">identificador de  PolizasSeguros</param>
        public void EliminarPolizasSeguros(Int64 pId, Usuario pUsuario)
        {
            try
            {
              // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
               // {

                    DAPolizasSeguros.EliminarPolizasSeguros(pId, pUsuario);

                   // ts.Complete();
                }
          //  }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "EliminarPolizasSeguros", ex);
            }
        }

        /// <summary>
        /// Obtiene una PolizasSeguros
        /// </summary>
        /// <param name="pId">identificador del PolizasSeguros</param>
        /// <returns>PolizasSeguros consultada</returns>
        public PolizasSeguros ConsultarPolizasSeguros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAPolizasSeguros.ConsultarPolizasSeguros(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "ConsultarPolizasSeguros", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una PolizasSeguros
        /// </summary>
        /// <param name="pId">identificador del PolizasSeguros</param>
        /// <returns>PolizasSeguros consultada</returns>
        public PolizasSegurosVida ConsultarPolizasSegurosVida(Int64 pId, String tipo, Usuario pUsuario)
        {
            try
            {
                return DAPolizasSeguros.ConsultarPolizasSegurosVida(pId,tipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "ConsultarPolizasSegurosVida", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene datos desembolso
        /// </summary>
        /// <param name="pId">identificador del DatosDesembolso</param>
        /// <returns>DatosDesembolso consultada</returns>
        public PolizasSeguros ConsultarCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAPolizasSeguros.ConsultarCredito(pId, pUsuario);
            }
            /* catch (ExceptionBusiness ex)
            {
              throw new ExceptionBusiness(ex1.Message,ex1);
                   
             }
              */

            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "ConsultarCredito", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene datos General
        /// </summary>
        /// <param name="pId">identificador del dato de la tabla general</param>
        /// <returns>Parametro consultada</returns>
        public PolizasSeguros ConsultarParametroEdadBeneficiarios(Usuario pUsuario)
        {
            try
            {
                return DAPolizasSeguros.ConsultarParametroEdadBeneficiarios(pUsuario);
            }
            /* catch (ExceptionBusiness ex)
            {
              throw new ExceptionBusiness(ex1.Message,ex1);
                   
             }
              */

            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "ConsultarParametroEdadBeneficiarios", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene datos General
        /// </summary>
        /// <param name="pId">identificador del dato de la tabla general</param>
        /// <returns>Parametro consultada</returns>
        public PolizasSeguros ConsultarParametroEdad(Usuario pUsuario)
        {
            try
            {
                return DAPolizasSeguros.ConsultarParametroEdad(pUsuario);
            }
            /* catch (ExceptionBusiness ex)
            {
              throw new ExceptionBusiness(ex1.Message,ex1);
                   
             }
              */

            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "ConsultarParametroEdad", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene datos desembolso
        /// </summary>
        /// <param name="pId">identificador del DatosDesembolso</param>
        /// <returns>DatosDesembolso consultada</returns>
        public List<PolizasSeguros> FiltrarCredito(PolizasSeguros pEntidad, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAPolizasSeguros.FiltrarCredito(pEntidad, pUsuario, filtro);
            }
            /* catch (ExceptionBusiness ex)
            {
              throw new ExceptionBusiness(ex1.Message,ex1);
                   
             }
              */

            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "FiltrarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea una PolizasSeguros
        /// </summary>
        /// <param name="pEntity">Entidad PolizasSeguros</param>
        /// <returns>Entidad creada</returns>
        public PolizasSeguros CrearPolizasSeguros(PolizasSeguros pPolizasSeguros, Usuario pUsuario)
        {
            try
            {
              //  using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                    pPolizasSeguros = DAPolizasSeguros.InsertarPolizasSeguros(pPolizasSeguros, pUsuario);

                  //  ts.Complete();
                //}

                return pPolizasSeguros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "CrearPolizasSeguros", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea una PolizasSegurosVida
        /// </summary>
        /// <param name="pEntity">Entidad PolizasSegurosVidad</param>
        /// <returns>Entidad creada</returns>
        public PolizasSegurosVida CrearPolizasSegurosVida(PolizasSegurosVida pPolizasSegurosVida, Usuario pUsuario)
        {
            try
            {
                //  using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                pPolizasSegurosVida = DAPolizasSeguros.InsertarPolizasSegurosVida(pPolizasSegurosVida, pUsuario);

                //  ts.Complete();
                //}

                return pPolizasSegurosVida;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PolizasSegurosBusiness", "CrearPolizasSeguros", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de PlanesSegurosAmparos dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanesSegurosAmparos obtenidos</returns>
        public List<ParentescoPolizas> ListarParentesco(ParentescoPolizas pParentesco, Usuario pUsuario)
        {
            try
            {
                return DAPolizasSeguros.ListarParentesco(pParentesco, pUsuario);
                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParentescoBusiness", "ListarParentesco", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de PlanesSegurosAmparos dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanesSegurosAmparos obtenidos</returns>
        public List<ParentescoPolizas> ListarParentescofamiliares(ParentescoPolizas pParentesco, Usuario pUsuario)
        {
            try
            {
                return DAPolizasSeguros.ListarParentescofamiliares(pParentesco, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParentescoBusiness", "ListarParentesco", ex);
                return null;
            }
        }


        public int CalcularMeses(DateTime fechaComienzo, DateTime fechaFin)
        {
            fechaComienzo = fechaComienzo.Date;
            int diaComienzo = fechaComienzo.Day;
            fechaFin = fechaFin.Date;
            int count = 0;
            while (fechaComienzo < fechaFin)
            {
                int añoComienzo = fechaComienzo.Year;
                int mesComienzo = fechaComienzo.Month;
                mesComienzo = mesComienzo + 1;
                if (mesComienzo > 12)
                {
                    mesComienzo = 1;
                    añoComienzo = añoComienzo + 1;
                }
                if (mesComienzo == 2 & diaComienzo >= 28)
                    fechaComienzo = new DateTime(añoComienzo, mesComienzo, 28, 0, 0, 0);
                else
                    if ((mesComienzo == 4 | mesComienzo == 6 | mesComienzo == 9 | mesComienzo == 11) & diaComienzo >= 30)
                        fechaComienzo = new DateTime(añoComienzo, mesComienzo, 30, 0, 0, 0);
                    else
                        fechaComienzo = new DateTime(añoComienzo, mesComienzo, diaComienzo, 0, 0, 0);
                count++;
            }
            return count;
        }

        public int CalcularEdad(DateTime fechanacimiento, DateTime fechaactual)
        {
            int edad = fechaactual.Year- fechanacimiento.Year;
            if (fechaactual.Month < fechanacimiento.Month || (fechaactual.Month == fechanacimiento.Month && fechaactual.Day < fechanacimiento.Day))
                edad--;
            return edad;
        }       
      
 
        public Int64 CalcularEdad2(DateTime fechaNacimiento) 

        {
            
             DateTime fechaActual = DateTime.Now; 
             Int64 edad2= fechaActual.Year - fechaNacimiento.Year;
             Int64 edad = Convert.ToInt64(edad2.ToString());
            // Calcular si no ha cumplido aun 
             if ( new DateTime(fechaActual.Year, fechaNacimiento.Month, fechaNacimiento.Day) > fechaActual ) 
             { 
                 edad--; 
             }    

            return edad; 
        } 


    }

    

}
