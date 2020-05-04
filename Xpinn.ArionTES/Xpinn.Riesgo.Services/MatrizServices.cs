using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Services
{
    public class MatrizServices : GlobalService
    {
        private MatrizBusiness BOMatriz;

        /// <summary>
        /// Constructor para el acceso a la capa Data
        /// </summary>
        public MatrizServices()
        {
            BOMatriz = new MatrizBusiness();
        }

        public string CodigoProgramaI { get { return "270401"; } }
        public string CodigoProgramaC { get { return "270402"; } }
        public string CodigoProgramaG { get { return "270403"; } }
        public string CodigoProgramaM { get { return "270404"; } }
        public string CodigoProgramaCR { get { return "270405"; } }
        public string CodigoProgramaMR { get { return "270407"; } }
        public string CodigoProgramaPMR { get { return "270309"; } }
        public string CodigoProgramaMC { get { return "270310"; } }
        public string CodigoProgramaFC { get { return "270311"; } }

        /// <summary>
        /// Servicio para crear registro de la matriz de riesgo inherente
        /// </summary>
        /// <param name="lstMatriz">Listado de registros que conforman la matriz</param>
        /// <param name="vUsuario"></param>
        /// <returns>Variable de confirmación</returns>
        public bool CrearMatriz(List<Matriz> lstMatriz, Usuario vUsuario)
        {
            try
            {
                BOMatriz.CrearMatrizRInherente(lstMatriz, vUsuario);
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "CrearMatriz", ex);
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
                BOMatriz.MatrizParametroMonitoreo(lstMatriz, vUsuario);
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "MatrizParametroMonitoreo", ex);
                return false;
            }
        }

        /// <summary>
        /// Eliminar un registro de la matriz
        /// </summary>
        /// <param name="cod_matriz">Código del registro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public bool EliminarMatrizRInherente(Int64 cod_matriz, Usuario vUsuario)
        {
            try
            {
                BOMatriz.EliminarMatrizRInherente(cod_matriz, vUsuario);
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "EliminarMatrizRInherente", ex);
                return false;
            }
        }

        /// <summary>
        /// Servicio para crear registro de la matriz de riesgo residual
        /// </summary>
        /// <param name="lstMatriz">Listado de registros que conforman la matriz</param>
        /// <param name="vUsuario"></param>
        /// <returns>Variable de confirmación</returns>
        public bool CrearMatrizRResidual(List<Matriz> lstMatriz, Usuario vUsuario)
        {
            try
            {
                BOMatriz.CrearMatrizRResidual(lstMatriz, vUsuario);
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "CrearMatrizRResidual", ex);
                return false;
            }
        }

        /// <summary>
        /// Servicio para consultar el valor de la calificación segun el nivel de impacto y probabilidad
        /// </summary>
        /// <param name="cod_probabilidad">Nivel de probabilidad</param>
        /// /// <param name="cod_impacto">Nivel de impacto</param>
        /// <param name="vUsuario"></param>
        /// <returns>Código calificación</returns>
        public int ConsultarCalificacion(Int64 cod_probabilidad, Int64 cod_impacto, Usuario vUsuario)
        {
            try
            {
                return BOMatriz.ConsultarCalificacion(cod_probabilidad, cod_impacto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ConsultarCalificacion", ex);
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
                return BOMatriz.ConsultarCalificacionControl(cod_clase, cod_forma, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ConsultarCalificacionControl", ex);
                return 0;
            }
        }

        /// <summary>
        //Servicio para consultar nivel de impacto
        /// </summary>
        /// <param name="cod_impacto">Código del nivel consultado</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con descripción y código</returns>
        public Matriz ConsultarImpacto(Int64 cod_impacto, Usuario vUsuario)
        {
            try
            {
                return BOMatriz.ConsultarImpacto(cod_impacto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ConsultarImpacto", ex);
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
                return BOMatriz.ListarImpacto(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ListarImpacto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para consultar nivel de probabilidad
        /// </summary>
        /// <param name="cod_probabilidad">Código del nivel de probabilidad</param>
        /// <param name="vUsuario"></param>
        /// <returns>Objeto con descripción y código</returns>
        public Matriz ConsultarProbabilidad(Int64 cod_probabilidad, Usuario vUsuario)
        {
            try
            {
                return BOMatriz.ConsultarProbabilidad(cod_probabilidad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ConsultarProbabilidad", ex);
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
                return BOMatriz.ListarProbabilidad(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ListarProbabilidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar factores de riesgo según el sistema de riesgo
        /// </summary>
        /// <param name="cod_riesgo">Código del sistema de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns>Lista de factores</returns>
        public List<Matriz> ListarFactor(Int64 cod_riesgo, Usuario vUsuario)
        {
            try
            {
                return BOMatriz.ListarFactor(cod_riesgo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ListarFactor", ex);
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
                return BOMatriz.ListarMatriz(cod_riesgo, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ListarMatriz", ex);
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
                return BOMatriz.ListarPromedioFactor(cod_riesgo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ListarPromedioFactor", ex);
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
                return BOMatriz.ListarMatrizRResidual(cod_riesgo, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ListarMatrizRResidual", ex);
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
                return BOMatriz.ListarMatrizMonitoreo(cod_riesgo, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ListarMatrizMonitoreo", ex);
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
                return BOMatriz.ListarMatrizGlobal(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ListarMatrizGlobal", ex);
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
                return BOMatriz.ConsultarEvaluacionRiesgo(cod_factor, cod_riesgo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ConsultarEvaluacionRiesgo", ex);
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
        public List<Matriz> listarEvaluacionRiesgoXcausa(Int64 cod_factor,Int64 cod_riesgo, Usuario vUsuario)
        {
            try
            {
                List<Matriz> lsRiesgoXcausa = new List<Matriz>();
                lsRiesgoXcausa = BOMatriz.ListarRiesgoXcausa(cod_factor, cod_riesgo, vUsuario);
                return lsRiesgoXcausa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ConsultarEvaluacionRiesgo", ex);
                return null;
            }
        }
        public List<Matriz> ListarMatrizCalor(Usuario vUsuario)
        {
            try
            {
                return BOMatriz.ListarMatrizCalor(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ListarMatrizCalor", ex);
                return null;
            }
        }
        public Matriz ModificarMatrizCalor(Matriz mCalor, Usuario usuario)
        {
            try
            {
                return BOMatriz.ModificarMatrizCalor(mCalor, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ModificarRangoPerfil", ex);
                return null;
            }
        }
        public List<Identificacion> ListarPonderadoFactores(Int32 pFactor, Usuario vUsuario)
        {
            try
            {
                return BOMatriz.ListarPonderadoFactores(pFactor, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ListarPonderadoFactores", ex);
                return null;
            }
        }
        public List<Matriz> ListarRangosMatrizRiesgo(Usuario vUsuario)
        {
            try
            {
                return BOMatriz.ListarRangosMatrizRiesgo(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ListarRangosMatrizRiesgo", ex);
                return null;
            }
        }
        public Matriz ModificarRangoMatrizRiesgo(Matriz mRango, Usuario usuario)
        {
            try
            {
                return BOMatriz.ModificarRangoMatrizRiesgo(mRango, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ModificarRangoMatrizRiesgo", ex);
                return null;
            }
        }
        public Int32 calcularValoracionRango(Int32 val, Usuario vUsuario)
        {
            try
            {
                return BOMatriz.calcularValoracionRango(val, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MatrizServices", "ListarRangosMatrizRiesgo", ex);
                return 0;
            }
        }

        public Matriz[] ColoresRInherente = new Matriz[5]
        {
            new Matriz { valor_rinherente = 0, descripcion = "", nivel = "White" },
            new Matriz { valor_rinherente = 1, descripcion = "Bajo", nivel = "Gold" },
            new Matriz { valor_rinherente = 2, descripcion = "Moderado", nivel = "Orange" },
            new Matriz { valor_rinherente = 3, descripcion = "Alto", nivel = "Tomato" },
            new Matriz { valor_rinherente = 4, descripcion = "Extremo", nivel = "Red" },
        };

        public Matriz[] ColoresVControl = new Matriz[6]
        {
            new Matriz { valor_control = 0, descripcion = "", nivel = "White" },
            new Matriz { valor_control = 1, descripcion = "Eficiente", nivel = "YellowGreen" },
            new Matriz { valor_control = 2, descripcion = "Alto", nivel = "Gold" },
            new Matriz { valor_control = 3, descripcion = "Medio", nivel = "Orange" },
            new Matriz { valor_control = 4, descripcion = "Bajo", nivel = "Tomato" },
            new Matriz { valor_control = 5, descripcion = "Ineficaz", nivel = "Red" },
        };

        public Matriz[] ColoresRResidual = new Matriz[5]
        {
            new Matriz { valor_rresidual = 0, descripcion = "", nivel = "White" },
            new Matriz { valor_rresidual = 1, descripcion = "Bajo", nivel = "Gold" },
            new Matriz { valor_rresidual = 2, descripcion = "Moderado", nivel = "Orange" },
            new Matriz { valor_rresidual = 3, descripcion = "Alto", nivel = "Tomato" },
            new Matriz { valor_rresidual = 4, descripcion = "Extremo", nivel = "Red" },                               
        };

        public Matriz[] ColoresAlerta = new Matriz[4]
        {
            new Matriz { valor_rresidual = 0, descripcion = "", nivel = "White" },
            new Matriz { valor_rresidual = 1, descripcion = "Pasiva", nivel = "Gold"  },
            new Matriz { valor_rresidual = 2, descripcion = "Pendiente", nivel = "YellowGreen" },
            new Matriz { valor_rresidual = 3, descripcion = "Activa", nivel = "Tomato" }
        };

        public FormaControl ModificarFormaControlPuntaje(FormaControl pItem, Usuario pUsuario)
        {
            return BOMatriz.ModificarFormaControlPuntaje(pItem, pUsuario);
        }

        public List<Xpinn.Riesgo.Entities.FormaControl> ListaFormaControl()
        {
            List<Xpinn.Riesgo.Entities.FormaControl> lstTipos = new List<Xpinn.Riesgo.Entities.FormaControl>();

            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 1, cod_atributo = 1, atributo = "Grado de Automatización", cod_opcion = 1, opcion = "Manual", valor = 0 });
            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 2, cod_atributo = 1, atributo = "Grado de Automatización", cod_opcion = 2, opcion = "Automático", valor = 0 });
            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 3, cod_atributo = 1, atributo = "Grado de Automatización", cod_opcion = 3, opcion = "Semiautomático", valor = 0 });

            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 4, cod_atributo = 2, atributo = "Ejecución", cod_opcion = 1, opcion = "Casi Siempre", valor = 0 });
            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 5, cod_atributo = 2, atributo = "Ejecución", cod_opcion = 2, opcion = "Eventualmente", valor = 0 });
            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 6, cod_atributo = 2, atributo = "Ejecución", cod_opcion = 3, opcion = "Siempre", valor = 0 });

            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 7, cod_atributo = 3, atributo = "Documentación", cod_opcion = 1, opcion = "Documentado y Divulgado", valor = 0 });
            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 8, cod_atributo = 3, atributo = "Documentación", cod_opcion = 2, opcion = "Documentado", valor = 0 });
            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 9, cod_atributo = 3, atributo = "Documentación", cod_opcion = 3, opcion = "Parcialmente Documentado", valor = 0 });
            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 10, cod_atributo = 3, atributo = "Documentación", cod_opcion = 4, opcion = "No Documentado", valor = 0 });

            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 11, cod_atributo = 4, atributo = "Complejidad", cod_opcion = 1, opcion = "Muy Complejo", valor = 0 });
            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 12, cod_atributo = 4, atributo = "Complejidad", cod_opcion = 2, opcion = "Complejo", valor = 0 });
            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 13, cod_atributo = 4, atributo = "Complejidad", cod_opcion = 3, opcion = "Simple", valor = 0 });

            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 14, cod_atributo = 5, atributo = "Histórico de Fallas (último año)", cod_opcion = 1, opcion = "Si", valor = 0 });
            lstTipos.Add(new Xpinn.Riesgo.Entities.FormaControl { cod_formacontrol = 15, cod_atributo = 5, atributo = "Histórico de Fallas (último año)", cod_opcion = 2, opcion = "No", valor = 0 });
            return lstTipos;
        }

        public int ConsultarPuntajeFormaControl(FormaControl pItem, Usuario pUsuario)
        {
            return BOMatriz.ConsultarPuntajeFormaControl(pItem, pUsuario);
        }

        public string ConsultarValoracionFormaControl(int pPuntaje, ref int pValor, Usuario pUsuario)
        {
            return BOMatriz.ConsultarValoracionFormaControl(pPuntaje, ref pValor, pUsuario);
        }

        public int ConsultarRangoMatrizRiesgo(Int64 pPuntaje, Usuario pUsuario)
        {
            return BOMatriz.ConsultarRangoMatrizRiesgo(pPuntaje, pUsuario);
        }

        public Int64 PuntajeMaximoMatrizRiesgo(Usuario pUsuario)
        {
            return BOMatriz.PuntajeMaximoMatrizRiesgo(pUsuario);
        }


    }
}
