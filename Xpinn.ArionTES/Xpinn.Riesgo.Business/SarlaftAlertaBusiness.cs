using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Linq;
using Xpinn.Util;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;
using Xpinn.Interfaces.Entities;
using System.Web.Script.Serialization;

namespace Xpinn.Riesgo.Business
{

    public class SarlaftAlertaBusiness : GlobalBusiness
    {

        private SarlaftAlertaData DASarlaftAlerta;

        public SarlaftAlertaBusiness()
        {
            DASarlaftAlerta = new SarlaftAlertaData();
        }

        public SarlaftAlerta CrearSarlaftAlerta(SarlaftAlerta pSarlaftAlerta, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSarlaftAlerta = DASarlaftAlerta.CrearSarlaftAlerta(pSarlaftAlerta, pusuario);

                    ts.Complete();

                }

                return pSarlaftAlerta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaBusiness", "CrearSarlaftAlerta", ex);
                return null;
            }
        }


        public SarlaftAlerta ModificarSarlaftAlerta(SarlaftAlerta pSarlaftAlerta, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSarlaftAlerta = DASarlaftAlerta.ModificarSarlaftAlerta(pSarlaftAlerta, pusuario);

                    ts.Complete();

                }

                return pSarlaftAlerta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaBusiness", "ModificarSarlaftAlerta", ex);
                return null;
            }
        }


        public void EliminarSarlaftAlerta(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DASarlaftAlerta.EliminarSarlaftAlerta(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaBusiness", "EliminarSarlaftAlerta", ex);
            }
        }


        public SarlaftAlerta ConsultarSarlaftAlerta(Int64 pId, Usuario pusuario)
        {
            try
            {
                SarlaftAlerta SarlaftAlerta = new SarlaftAlerta();
                SarlaftAlerta = DASarlaftAlerta.ConsultarSarlaftAlerta(pId, pusuario);
                return SarlaftAlerta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaBusiness", "ConsultarSarlaftAlerta", ex);
                return null;
            }
        }


        public List<SarlaftAlerta> ListarSarlaftAlerta(SarlaftAlerta pSarlaftAlerta, DateTime? pFecIni, DateTime? pFecFin, int pOrden, Usuario pusuario)
        {
            try
            {
                return DASarlaftAlerta.ListarSarlaftAlerta(pSarlaftAlerta, pFecIni, pFecFin, pOrden, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaBusiness", "ListarSarlaftAlerta", ex);
                return null;
            }
        }

        public Dictionary<int, string>  _alertas = new Dictionary<int, string>
        {
            { 0, "" },
            { 1, "Mantener vigilancia en movimientos y confirmar realización de consultas regulares a la persona"},
            { 2, "Persona es Asociado Especial" },
        };

        /// <summary>
        /// Creación de registro de las consultas a listas restrictivas por persona
        /// </summary>
        /// <param name="lstOFAC">Listado de registros consulta OFAC</param>
        /// <param name="lstONUInd">Listado de registros consulta ONU Individual</param>
        /// <param name="lstONUEnt">Listado de registros consulta ONU Entity</param>
        /// <param name="cod_persona">Código de la persona</param>
        /// <param name="pusuario"></param>
        public void CrearRegistroConsultaLista(List<TradeUSSearchInd> lstOFAC, List<Individual> lstONUInd, List<Entity> lstONUEnt, Int64 cod_persona, bool nuevo, bool reg, bool pro, bool con, bool ru, bool poli, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Consulta pRegLista;

                        if (lstOFAC != null)
                        {
                            foreach (TradeUSSearchInd pRegistro in lstOFAC)
                            {
                                JavaScriptSerializer ser = new JavaScriptSerializer();
                                pRegLista = new Consulta();
                                pRegLista.cod_persona = cod_persona;
                                pRegLista.tipo_consulta = "1";
                                pRegLista.contenido = ser.Serialize(pRegistro);
                                pRegLista.coincidencia = Convert.ToInt32(pRegistro.coincidencia);
                                pRegLista.fecha_consulta = DateTime.Now;

                                DASarlaftAlerta.CrearRegistroConsultaLista(pRegLista, pusuario);
                            }
                        }

                        if (lstONUInd != null)
                        {
                            foreach (Individual pRegistro in lstONUInd)
                            {
                                JavaScriptSerializer ser = new JavaScriptSerializer();
                                pRegLista = new Consulta();
                                pRegLista.cod_persona = cod_persona;
                                pRegLista.tipo_consulta = "2";
                                pRegLista.contenido = ser.Serialize(pRegistro);
                                pRegLista.coincidencia = Convert.ToInt32(pRegistro.coincidencia);
                                pRegLista.fecha_consulta = DateTime.Now;

                                DASarlaftAlerta.CrearRegistroConsultaLista(pRegLista, pusuario);
                            }
                        }

                        if (lstONUEnt != null)
                        {
                            foreach (Entity pRegistro in lstONUEnt)
                            {
                                JavaScriptSerializer ser = new JavaScriptSerializer();
                                pRegLista = new Consulta();
                                pRegLista.cod_persona = cod_persona;
                                pRegLista.tipo_consulta = "2";
                                pRegLista.contenido = ser.Serialize(pRegistro);
                                pRegLista.coincidencia = Convert.ToInt32(pRegistro.coincidencia);
                                pRegLista.fecha_consulta = DateTime.Now;

                                DASarlaftAlerta.CrearRegistroConsultaLista(pRegLista, pusuario);
                            }
                        }

                        //Crea el registro con coincidencia 0 en caso de que no se hallan registrado en los ciclos anteriores
                        if (lstONUInd != null && lstONUEnt != null)
                        {
                            if (lstONUInd.Count == 0 && lstONUEnt.Count == 0)
                            {
                                JavaScriptSerializer ser = new JavaScriptSerializer();
                                pRegLista = new Consulta();
                                pRegLista.cod_persona = cod_persona;
                                pRegLista.tipo_consulta = "2";
                                pRegLista.contenido = "Consulta sin resultados";
                                pRegLista.coincidencia = Convert.ToInt32(0);
                                pRegLista.fecha_consulta = DateTime.Now;

                                DASarlaftAlerta.CrearRegistroConsultaLista(pRegLista, pusuario);
                            }
                        }

                        /******************OTROS TIPOS DE CONSULTAS EN LISTAS******************************/
                        //REGISTRADURIA
                        //if (Convert.ToString(reg) != null)
                        //{
                        //    JavaScriptSerializer ser = new JavaScriptSerializer();
                        //    pRegLista = new Consulta();
                        //    pRegLista.cod_persona = cod_persona;
                        //    pRegLista.tipo_consulta = "3";
                        //    if (lstOFAC != null)
                        //        if (lstOFAC.Count > 0)
                        //            pRegLista.contenido = "consulta masiva";
                        //    pRegLista.coincidencia = reg ? 1 : 0;
                        //    pRegLista.fecha_consulta = DateTime.Now;

                        //    DASarlaftAlerta.CrearRegistroConsultaLista(pRegLista, pusuario);
                        //}
                        ////PROCURADURIA
                        //if (Convert.ToString(pro) != null)
                        //{
                        //    JavaScriptSerializer ser = new JavaScriptSerializer();
                        //    pRegLista = new Consulta();
                        //    pRegLista.cod_persona = cod_persona;
                        //    pRegLista.tipo_consulta = "4";
                        //    if (lstOFAC != null)
                        //        if (lstOFAC.Count > 0)
                        //            pRegLista.contenido = "consulta masiva";
                        //    pRegLista.coincidencia = pro ? 1 : 0;
                        //    pRegLista.fecha_consulta = DateTime.Now;

                        //    DASarlaftAlerta.CrearRegistroConsultaLista(pRegLista, pusuario);
                        //}
                        ////CONTRALORIA
                        //if (Convert.ToString(con) != null)
                        //{
                        //    JavaScriptSerializer ser = new JavaScriptSerializer();
                        //    pRegLista = new Consulta();
                        //    pRegLista.cod_persona = cod_persona;
                        //    pRegLista.tipo_consulta = "5";
                        //    if (lstOFAC != null)
                        //        if (lstOFAC.Count > 0)
                        //            pRegLista.contenido = "consulta masiva";
                        //    pRegLista.coincidencia = con ? 1 : 0;
                        //    pRegLista.fecha_consulta = DateTime.Now;

                        //    DASarlaftAlerta.CrearRegistroConsultaLista(pRegLista, pusuario);
                        //}
                        ////RUES  
                        //if (Convert.ToString(ru) != null)
                        //{
                        //    JavaScriptSerializer ser = new JavaScriptSerializer();
                        //    pRegLista = new Consulta();
                        //    pRegLista.cod_persona = cod_persona;
                        //    pRegLista.tipo_consulta = "6";
                        //    if (lstOFAC != null)
                        //        if (lstOFAC.Count > 0)
                        //            pRegLista.contenido = "consulta masiva";
                        //    pRegLista.coincidencia = ru ? 1 : 0;
                        //    pRegLista.fecha_consulta = DateTime.Now;

                        //    DASarlaftAlerta.CrearRegistroConsultaLista(pRegLista, pusuario);
                        //}
                        ////POLICIA
                        //if (Convert.ToString(poli) != null)
                        //{
                        //    JavaScriptSerializer ser = new JavaScriptSerializer();
                        //    pRegLista = new Consulta();
                        //    pRegLista.cod_persona = cod_persona;
                        //    pRegLista.tipo_consulta = "7";
                        //    if (lstOFAC != null)
                        //        if (lstOFAC.Count > 0)
                        //            pRegLista.contenido = "consulta masiva";
                        //    pRegLista.coincidencia = poli ? 1 : 0;
                        //    pRegLista.fecha_consulta = DateTime.Now;

                        //    DASarlaftAlerta.CrearRegistroConsultaLista(pRegLista, pusuario);
                        //}
                                        
                    //Verificar las coincidencias encontradas para que en caso de ser un nuevo asociado, su estado sea pendiente
                    int coincidencia_ofac = 0;
                    int coincidencia_ONUInd = 0;
                    int coincidencia_ONUEnt = 0;

                    coincidencia_ofac = lstOFAC.Where(x => x.coincidencia).Count();
                    coincidencia_ONUInd = lstONUInd.Where(x => x.coincidencia).Count();
                    coincidencia_ONUEnt = lstONUEnt.Where(x => x.coincidencia).Count();

                    if ((coincidencia_ofac > 0 || coincidencia_ONUEnt > 0 || coincidencia_ONUInd > 0) && nuevo)
                    {
                        DASarlaftAlerta.ModificarEstadoPersona(cod_persona, pusuario);
                    }

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaBusiness", "CrearRegistroConsultaLista", ex);
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
                return DASarlaftAlerta.ConsultarReportePersona(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaBusiness", "ConsultarReportePersona", ex);
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
                return DASarlaftAlerta.ListarPersonasConsultadas(filtro, pUltimo, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaBusiness", "ListarPersonasConsultadas", ex);
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
                DASarlaftAlerta.ModificarEstadoPersona(cod_persona, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaBusiness", "ModificarEstadoPersona", ex);
            }
        }

        public List<SarlaftAlerta> ListarPersonasParaConsultar(string filtro, Usuario pusuario)
        {
            try
            {
                return DASarlaftAlerta.ListarPersonasParaConsultar(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SarlaftAlertaBusiness", "ListarPersonasParaConsultar", ex);
                return null;
            }
        }


    }
}

