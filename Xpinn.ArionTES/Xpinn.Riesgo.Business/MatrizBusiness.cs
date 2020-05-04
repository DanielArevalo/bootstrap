using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;
using System.Transactions;


namespace Xpinn.Riesgo.Business
{
    public class MatrizBusiness : GlobalBusiness
    {
        private MatrizData DAMatriz;

        /// <summary>
        /// Constructor para el acceso a la capa Data
        /// </summary>
        public MatrizBusiness()
        {
            DAMatriz = new MatrizData();
        }

        /// <summary>
        /// Crear registro de la matriz de riesgo inherente
        /// </summary>
        /// <param name="lstMatriz">Listado de registros que conforman la matriz</param>
        /// <param name="vUsuario"></param>
        /// <returns>Variable de confirmación</returns>
        public bool CrearMatrizRInherente(List<Matriz> lstMatriz, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (Matriz pMatriz in lstMatriz)
                    {
                        if (pMatriz.cod_matriz == 0)
                            DAMatriz.CrearMatrizRInherente(pMatriz, vUsuario);
                        else
                            DAMatriz.ModificarMatrizRInherente(pMatriz, vUsuario);
                    }
                    ts.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "CrearMatriz", ex);
                return false;
            }
        }

        /// <summary>
        /// Eliminar un registro de la matriz de riesgo inherente
        /// </summary>
        /// <param name="cod_matriz">Código del registro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public bool EliminarMatrizRInherente(Int64 cod_matriz, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAMatriz.EliminarMatrizRInherente(cod_matriz, vUsuario);

                    ts.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "EliminarMatrizRInherente", ex);
                return false;
            }
        }

        /// <summary>
        /// Crear registro de la matriz de riesgo residual
        /// </summary>
        /// <param name="lstMatriz">Listado de registros que conforman la matriz</param>
        /// <param name="vUsuario"></param>
        /// <returns>Variable de confirmación</returns>
        public bool CrearMatrizRResidual(List<Matriz> lstMatriz, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (Matriz pMatriz in lstMatriz)
                    {
                        if (pMatriz.cod_matriz == 0)
                            DAMatriz.CrearMatrizRResidual(pMatriz, vUsuario);
                        else
                            DAMatriz.ModificarMatrizRResidual(pMatriz, vUsuario);
                    }
                    ts.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "CrearMatrizRResidual", ex);
                return false;
            }
        }

        /// <summary>
        /// Agregar parametro de monitoreo en cada registro de la matriz de riesgo residual
        /// </summary>
        /// <param name="lstMatriz"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public bool MatrizParametroMonitoreo(List<Matriz> lstMatriz, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (Matriz pMatriz in lstMatriz)
                    {
                        DAMatriz.MatrizParametroMonitoreo(pMatriz, vUsuario);
                    }
                    ts.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "MatrizParametroMonitoreo", ex);
                return false;
            }
        }

        /// <summary>
        /// Consultar el valor de la calificación para riesgo inherente
        /// </summary>
        /// <param name="cod_probabilidad">Nivel de probabilidad</param>
        /// /// <param name="cod_impacto">Nivel de impacto</param>
        /// <param name="vUsuario"></param>
        /// <returns>Código calificación</returns>
        public int ConsultarCalificacion(Int64 cod_probabilidad, Int64 cod_impacto, Usuario vUsuario)
        {
            try
            {
                return DAMatriz.ConsultarCalificacion(cod_probabilidad, cod_impacto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ConsultarCalificacion", ex);
                return 0;
            }
        }

        /// <summary>
        /// Consultar el valor de calificación para control
        /// </summary>
        /// <param name="cod_clase">Clase de control</param>
        /// <param name="cod_forma">Forma de control</param>
        /// <param name="vUsuario"></param>
        /// <returns>Código calificación</returns>
        public int ConsultarCalificacionControl(Int64 cod_clase, Int64 cod_forma, Usuario vUsuario)
        {
            try
            {
                return DAMatriz.ConsultarCalificacionControl(cod_clase, cod_forma, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ConsultarCalificacionControl", ex);
                return 0;
            }
        }

        /// <summary>
        /// Consultar nivel de impacto
        /// </summary>
        /// <param name="cod_impacto">Código del nivel consultado</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con descripción y código</returns>
        public Matriz ConsultarImpacto(Int64 cod_impacto, Usuario vUsuario)
        {
            try
            {
                return DAMatriz.ConsultarImpacto(cod_impacto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ConsultarImpacto", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar niveles de impacto
        /// </summary>
        /// <param name="vUsuario"></param>
        /// <returns>Listado con los niveles de impacto</returns>
        public List<Matriz> ListarImpacto(Usuario vUsuario)
        {
            try
            {
                return DAMatriz.ListarImpacto(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ListarImpacto", ex);
                return null;
            }
        }

        /// <summary>
        /// Consultar nivel de probabilidad
        /// </summary>
        /// <param name="cod_probabilidad">Código del nivel de probabilidad</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con descripción y código</returns>
        public Matriz ConsultarProbabilidad(Int64 cod_probabilidad, Usuario vUsuario)
        {
            try
            {
                return DAMatriz.ConsultarProbabilidad(cod_probabilidad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ConsultarProbabilidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar niveles de probabilidad
        /// </summary>
        /// <param name="vUsuario"></param>
        /// <returns>Listado con niveles de probabilidad</returns>
        public List<Matriz> ListarProbabilidad(Usuario vUsuario)
        {
            try
            {
                return DAMatriz.ListarProbabilidad(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ListarProbabilidad", ex);
                return null;
            }
        }

        public List<Matriz> ListarFactor(Int64 cod_riesgo, Usuario vUsuario)
        {
            try
            {
                List<Matriz> lstFactores = new List<Matriz>();
                List<Identificacion> lstIdentificacion = new List<Identificacion>();
                Identificacion pFactor = new Identificacion();
                IdentificacionData DAIdentificacion = new IdentificacionData();

                pFactor.cod_riesgo = cod_riesgo;
                lstIdentificacion = DAIdentificacion.ListarFactoresRiesgo(pFactor, "", vUsuario);

                lstFactores = (from item in lstIdentificacion
                               select new Matriz
                               {
                                   cod_factor = item.cod_factor,
                                   desc_factor = item.descripcion
                               }).ToList();
                return lstFactores;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ListarFactor", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar matriz según el sistema de riesgo
        /// </summary>
        /// /// <param name="cod_riesgo">Código del sistema de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns>Listado con valores de la matriz</returns>
        public List<Matriz> ListarMatriz(Int64 cod_riesgo, string filtro, Usuario vUsuario)
        {
            try
            {
                return DAMatriz.ListarMatriz(cod_riesgo, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ListarMatriz", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar factores con promedio de calificación
        /// </summary>
        /// <param name="cod_riesgo">Sistema de riesgo correspondiente</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Matriz> ListarPromedioFactor(Int64 cod_riesgo, Usuario vUsuario)
        {
            try
            {
                return DAMatriz.ListarPromedioFactor(cod_riesgo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ListarPromedioFactor", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar matriz de riesgo residual según el sistema de riesgo
        /// </summary>
        /// /// <param name="cod_riesgo">Código del sistema de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns>Listado con valores de la matriz</returns>
        public List<Matriz> ListarMatrizRResidual(Int64 cod_riesgo, string filtro, Usuario vUsuario)
        {
            try
            {
                return DAMatriz.ListarMatrizRResidual(cod_riesgo, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ListarMatrizRResidual", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar valores para generación de matriz de monitoreo
        /// </summary>
        /// <param name="cod_riesgo"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Matriz> ListarMatrizMonitoreo(Int64 cod_riesgo, string filtro, Usuario vUsuario)
        {
            try
            {
                return DAMatriz.ListarMatrizMonitoreo(cod_riesgo, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ListarMatrizMonitoreo", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar los sistemas de riesgo con los valores promediados
        /// </summary>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Matriz> ListarMatrizGlobal(Usuario vUsuario)
        {
            try
            {
                return DAMatriz.ListarMatrizGlobal(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ListarMatrizGlobal", ex);
                return null;
            }
        }

        /// <summary>
        /// Consultar valores para reporte de evaluación de riesgo
        /// </summary>
        /// <param name="cod_factor">Código del factor de riesgo</param>
        /// <param name="cod_riesgo">Código del sistema de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Matriz ConsultarEvaluacionRiesgo(Int64 cod_factor, Int64 cod_riesgo, Usuario vUsuario)
        {
            try
            {
                return DAMatriz.ConsultarEvaluacionRiesgo(cod_factor, cod_riesgo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ConsultarEvaluacionRiesgo", ex);
                return null;
            }
        }
        /// <returns></returns>
        public List<Matriz> ListarRiesgoXcausa(Int64 cod_factor, Int64 cod_riesgo, Usuario vUsuario)
        {
            try
            {
                List<Matriz> lsRiesgoXcausa = new List<Matriz>();
                lsRiesgoXcausa = DAMatriz.ListarRiesgoXcausa(cod_factor, cod_riesgo, vUsuario);
                return lsRiesgoXcausa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ListarCausas", ex);
                return null;
            }
        }
        public Matriz ModificarMatrizCalor(Matriz mCalor, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    mCalor = DAMatriz.ModificarMatrizCalor(mCalor, usuario);
                    ts.Complete();
                }

                return mCalor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ModificarRangoPerfil", ex);
                return null;
            }
        }
        public List<Matriz> ListarMatrizCalor(Usuario usuario)
        {
            try
            {
                return DAMatriz.ListarMatrizCalor(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ListarMatrizCalor", ex);
                return null;
            }
        }
        public List<Identificacion> ListarPonderadoFactores(Int32 pFactor, Usuario usuario)
        {
            try
            {
                return DAMatriz.ListarPonderadoFactores(pFactor, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ListarPonderadoFactores", ex);
                return null;
            }
        }
        public List<Matriz> ListarRangosMatrizRiesgo(Usuario usuario)
        {
            try
            {
                return DAMatriz.ListarRangosMatrizRiesgo(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ListarRangosMatrizRiesgo", ex);
                return null;
            }
        }
        public Matriz ModificarRangoMatrizRiesgo(Matriz mRango, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    mRango = DAMatriz.ModificarRangoMatrizRiesgo(mRango, usuario);
                    ts.Complete();
                }

                return mRango;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ModificarRangoMatrizRiesgo", ex);
                return null;
            }
        }

        public Int32 calcularValoracionRango(Int32 val, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    val = DAMatriz.calcularValoracionRango(val, usuario);
                }

                return val;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ModificarRangoMatrizRiesgo", ex);
                return 0;
            }
        }

        public FormaControl ModificarFormaControlPuntaje(FormaControl pItem, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pItem = DAMatriz.ModificarFormaControlPuntaje(pItem, pUsuario);
                    ts.Complete();
                }

                return pItem;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizBusiness", "ModificarRangoMatrizRiesgo", ex);
                return null;
            }
        }

        public int ConsultarPuntajeFormaControl(FormaControl pItem, Usuario pUsuario)
        {
            return DAMatriz.ConsultarPuntajeFormaControl(pItem, pUsuario);
        }

        public string ConsultarValoracionFormaControl(int pPuntaje, ref int pValor, Usuario pUsuario)
        {
            return DAMatriz.ConsultarValoracionFormaControl(pPuntaje, ref pValor, pUsuario);
        }

        public int ConsultarRangoMatrizRiesgo(Int64 pPuntaje, Usuario pUsuario)
        {
            return DAMatriz.ConsultarRangoMatrizRiesgo(pPuntaje, pUsuario);
        }

        public Int64 PuntajeMaximoMatrizRiesgo(Usuario pUsuario)
        {
            return DAMatriz.PuntajeMaximoMatrizRiesgo(pUsuario);
        }



    }
}
