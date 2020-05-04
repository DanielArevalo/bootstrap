using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Comun;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Data
{
    public class HistoricoSegmentacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public HistoricoSegmentacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public void CierreSegmentacion(DateTime fechaCierre, string estado, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "PFECHA";
                        PFECHA.Value = fechaCierre;
                        PFECHA.Direction = ParameterDirection.Input;

                        DbParameter PUSUARIO = cmdTransaccionFactory.CreateParameter();
                        PUSUARIO.ParameterName = "PUSUARIO";
                        PUSUARIO.Value = usuario.codusuario;
                        PUSUARIO.Direction = ParameterDirection.Input;

                        DbParameter PESTADO = cmdTransaccionFactory.CreateParameter();
                        PESTADO.ParameterName = "PESTADO";
                        PESTADO.Value = estado;
                        PESTADO.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(PFECHA);
                        cmdTransaccionFactory.Parameters.Add(PUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(PESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_CIERRESEGM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "CierreSegmentacion", ex);
                    }
                }
            }
        }

        public List<HistoricoSegmentacion> ListarHistoricosSegmentacion(string filtro, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<HistoricoSegmentacion> lista = new List<HistoricoSegmentacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT hist.consecutivo, hist.fechacierre, hist.codigopersona, per.identificacion, per.primer_nombre || ' ' || per.primer_Apellido as Nombre, ofi.cod_oficina, 
                                              ofi.nombre as nombre_oficina, segAct.codsegmento as codigoSegmentoActual, segAct.Nombre as SegmentoActual, segAnt.codsegmento as codigoSegmentoAnterior, segAnt.Nombre as SegmentoAnterior,
                                              sa.nombre As segmentoaso, sp.nombre As segmentopro, sc.nombre As segmentocan, sj.nombre As segmentojur, hist.calificacion,
                                              case NVL(hper.estado, NVL(hist.tipocliente, per.estado)) when 'A' then 'Asociado' when 'R' then 'Retirado' else 'Tercero' end as estado, 
                                              sat.nombre As segmentoaso_anterior, spt.nombre As segmentopro_anterior, sct.nombre As segmentocan_anterior, sj.nombre As segmentojur_anterior, Ant.calificacion as anterior
                                        FROM HISTORICO_SEGMENTACION hist
                                        JOIN persona per on per.cod_persona = hist.codigopersona
                                        LEFT JOIN historico_persona hper on hper.fecha_historico = hist.fechacierre And hper.cod_persona = hist.codigopersona
                                        LEFT JOIN oficina ofi on ofi.cod_oficina = per.cod_oficina
                                        LEFT JOIN segmentos segAct on segAct.codsegmento = hist.segmentoActual 
                                        LEFT JOIN segmentos segAnt on segAnt.codsegmento = hist.segmentoAnterior
                                        LEFT JOIN segmentos sa on sa.codsegmento = hist.segmentoaso
                                        LEFT JOIN segmentos sp on sp.codsegmento = hist.segmentopro
                                        LEFT JOIN segmentos sc on sc.codsegmento = hist.segmentocan
                                        LEFT JOIN segmentos sj on sj.codsegmento = hist.segmentojur    
                                        LEFT JOIN historico_segmentacion ant on Ant.Codigopersona = hist.Codigopersona And Ant.fechacierre < hist.fechacierre
                                        LEFT JOIN segmentos sat on sat.codsegmento = ant.segmentoaso
                                        LEFT JOIN segmentos spt on spt.codsegmento = ant.segmentopro
                                        LEFT JOIN segmentos sct on sct.codsegmento = ant.segmentocan
                                        LEFT JOIN segmentos sjt on sjt.codsegmento = ant.segmentojur
                                        " + filtro;
                        sql += (filtro.ToUpper().Contains("WHERE") ? " AND " : " WHERE ") + " (Ant.consecutivo Is Null OR Ant.consecutivo In (SELECT Max(P.Consecutivo) FROM HISTORICO_SEGMENTACION p WHERE P.codigopersona = hist.codigopersona And P.Fechacierre < hist.fechacierre)) ";
                        sql += " ORDER BY hist.fechacierre, per.identificacion ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            HistoricoSegmentacion entidad = new HistoricoSegmentacion();

                            if (resultado["consecutivo"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["consecutivo"]);
                            if (resultado["fechacierre"] != DBNull.Value) entidad.fechacierre = Convert.ToDateTime(resultado["fechacierre"]);
                            if (resultado["codigopersona"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["codigopersona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion_persona = Convert.ToString(resultado["identificacion"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre_persona = Convert.ToString(resultado["Nombre"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["nombre_oficina"] != DBNull.Value) entidad.nombre_oficina = Convert.ToString(resultado["nombre_oficina"]);
                            if (resultado["codigoSegmentoActual"] != DBNull.Value) entidad.segmentoactual = Convert.ToInt32(resultado["codigoSegmentoActual"]);
                            if (resultado["SegmentoActual"] != DBNull.Value) entidad.nombre_segmentoactual = Convert.ToString(resultado["SegmentoActual"]);
                            if (resultado["codigoSegmentoAnterior"] != DBNull.Value) entidad.segmentoanterior = Convert.ToInt32(resultado["codigoSegmentoAnterior"]);
                            if (resultado["SegmentoAnterior"] != DBNull.Value) entidad.nombre_segmentoanterior = Convert.ToString(resultado["SegmentoAnterior"]);
                            if (resultado["Segmentoaso"] != DBNull.Value) entidad.segmentoaso = Convert.ToString(resultado["Segmentoaso"]);
                            if (resultado["Segmentopro"] != DBNull.Value) entidad.segmentopro = Convert.ToString(resultado["Segmentopro"]);
                            if (resultado["Segmentocan"] != DBNull.Value) entidad.segmentocan = Convert.ToString(resultado["Segmentocan"]);
                            if (resultado["Segmentojur"] != DBNull.Value) entidad.segmentojur = Convert.ToString(resultado["Segmentojur"]);
                            if (resultado["calificacion"] != DBNull.Value)
                            { 
                                switch (Convert.ToInt32(resultado["calificacion"]))
                                {
                                    case 1: entidad.calificacion = "BAJO"; break;
                                    case 2: entidad.calificacion = "MODERADO"; break;
                                    case 3: entidad.calificacion = "ALTO"; break;
                                    case 4: entidad.calificacion = "EXTREMO"; break;
                                }
                            }
                            if (resultado["estado"] != DBNull.Value) entidad.tipo_rol= Convert.ToString(resultado["estado"]);
                            if (resultado["Segmentoaso_anterior"] != DBNull.Value) entidad.segmentoaso_anterior = Convert.ToString(resultado["Segmentoaso_anterior"]);
                            if (resultado["Segmentopro_anterior"] != DBNull.Value) entidad.segmentopro_anterior = Convert.ToString(resultado["Segmentopro_anterior"]);
                            if (resultado["Segmentocan_anterior"] != DBNull.Value) entidad.segmentocan_anterior = Convert.ToString(resultado["Segmentocan_anterior"]);
                            if (resultado["Segmentojur_anterior"] != DBNull.Value) entidad.segmentojur_anterior = Convert.ToString(resultado["Segmentojur_anterior"]);
                            if (resultado["anterior"] != DBNull.Value)
                            {
                                switch (Convert.ToInt32(resultado["anterior"]))
                                {
                                    case 1: entidad.calificacion_anterior = "BAJO"; break;
                                    case 2: entidad.calificacion_anterior = "MODERADO"; break;
                                    case 3: entidad.calificacion_anterior = "ALTO"; break;
                                    case 4: entidad.calificacion_anterior = "EXTREMO"; break;
                                }
                            }

                            lista.Add(entidad);
                        }

                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "ListarHistoricosSegmentacion", ex);
                        return null;
                    }
                }
            }
        }

        public Segmentos CrearSegmento(Segmentos vSegment, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodsegmento = cmdTransaccionFactory.CreateParameter();
                        pcodsegmento.ParameterName = "p_codsegmento";
                        pcodsegmento.Value = vSegment.codsegmento;
                        pcodsegmento.Direction = ParameterDirection.Output;
                        pcodsegmento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodsegmento);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = vSegment.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipoVariable = cmdTransaccionFactory.CreateParameter();
                        ptipoVariable.ParameterName = "p_tipo_variable";
                        ptipoVariable.Value = vSegment.tipo_variable;
                        ptipoVariable.Direction = ParameterDirection.Input;
                        ptipoVariable.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipoVariable);

                        DbParameter pcalSeg = cmdTransaccionFactory.CreateParameter();
                        pcalSeg.ParameterName = "p_calificacion_segmento";
                        pcalSeg.Value = vSegment.calificacion_segmento;
                        pcalSeg.Direction = ParameterDirection.Input;
                        pcalSeg.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcalSeg);

                        DbParameter pValAu = cmdTransaccionFactory.CreateParameter();
                        pValAu.ParameterName = "p_valida_almenosuno";
                        pValAu.Value = (vSegment.valida_alguno == true ? 1 : 0);
                        pValAu.Direction = ParameterDirection.Input;
                        pValAu.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(pValAu);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SEGMEN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        vSegment.codsegmento = Convert.ToInt32(pcodsegmento.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return vSegment;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "CrearSegmento", ex);
                        return null;
                    }
                }
            }
        }

        public Segmento_Detalles CrearDetalleSegmento(Segmento_Detalles pSeg, Usuario usuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcondiciontran = cmdTransaccionFactory.CreateParameter();
                        pidcondiciontran.ParameterName = "p_idcondiciontran";
                        pidcondiciontran.Value = pSeg.idcondiciontran;
                        if (opcion == 1)
                            pidcondiciontran.Direction = ParameterDirection.Output;
                        else
                            pidcondiciontran.Direction = ParameterDirection.Input;
                        pidcondiciontran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcondiciontran);

                        DbParameter pcodsegmento = cmdTransaccionFactory.CreateParameter();
                        pcodsegmento.ParameterName = "p_codsegmento";
                        pcodsegmento.Value = pSeg.codsegmento;
                        pcodsegmento.Direction = ParameterDirection.Input;
                        pcodsegmento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodsegmento);

                        DbParameter pvariable = cmdTransaccionFactory.CreateParameter();
                        pvariable.ParameterName = "p_variable";
                        pvariable.Value = pSeg.variable;
                        pvariable.Direction = ParameterDirection.Input;
                        pvariable.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvariable);

                        DbParameter poperador = cmdTransaccionFactory.CreateParameter();
                        poperador.ParameterName = "p_operador";
                        poperador.Value = pSeg.operador;
                        poperador.Direction = ParameterDirection.Input;
                        poperador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(poperador);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pSeg.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter p_valor_segundo = cmdTransaccionFactory.CreateParameter();
                        p_valor_segundo.ParameterName = "p_valor_segundo";
                        if (!string.IsNullOrWhiteSpace(pSeg.segundo_valor))
                        {
                            p_valor_segundo.Value = pSeg.segundo_valor;
                        }
                        else
                        {
                            p_valor_segundo.Value = DBNull.Value;
                        }

                        p_valor_segundo.Direction = ParameterDirection.Input;
                        p_valor_segundo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_valor_segundo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1) //CREAR
                            cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_DETSEGM_CRE";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_DETSEGM_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (opcion == 1)
                            pSeg.idcondiciontran = Convert.ToInt32(pidcondiciontran.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pSeg;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "CrearDetalleSegmento", ex);
                        return null;
                    }
                }
            }
        }

        public List<Segmento_Detalles> ListarDetalleSegmentos(int consecutivo, Usuario usuario)
        {
            DbDataReader resultado;
            List<Segmento_Detalles> listaDetalles = new List<Segmento_Detalles>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM SEGMENTO_DETALLES where CODSEGMENTO = " + consecutivo.ToString() + " ORDER BY IDCONDICIONTRAN ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Segmento_Detalles entidad = new Segmento_Detalles();
                            if (resultado["IDCONDICIONTRAN"] != DBNull.Value) entidad.idcondiciontran = Convert.ToInt32(resultado["IDCONDICIONTRAN"]);
                            if (resultado["CODSEGMENTO"] != DBNull.Value) entidad.codsegmento = Convert.ToInt32(resultado["CODSEGMENTO"]);
                            if (resultado["VARIABLE"] != DBNull.Value) entidad.variable = Convert.ToString(resultado["VARIABLE"]);
                            if (resultado["OPERADOR"] != DBNull.Value) entidad.operador = Convert.ToString(resultado["OPERADOR"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            if (resultado["SEGUNDOVALOR"] != DBNull.Value) entidad.segundo_valor = Convert.ToString(resultado["SEGUNDOVALOR"]);

                            listaDetalles.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listaDetalles;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "ListarDetalleSegmentos", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarSegmentoDetalle(int conseID, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcondiciontran = cmdTransaccionFactory.CreateParameter();
                        pidcondiciontran.ParameterName = "p_idcondiciontran";
                        pidcondiciontran.Value = conseID;
                        pidcondiciontran.Direction = ParameterDirection.Input;
                        pidcondiciontran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcondiciontran);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_DETSEGME_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "EliminarSegmentoDetalle", ex);
                    }
                }
            }
        }
        public tipoVariable ModificarVariable(tipoVariable vVariable, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodsegmento = cmdTransaccionFactory.CreateParameter();
                        pcodsegmento.ParameterName = "P_COD_TIPO_VARIABLE";
                        pcodsegmento.Value = vVariable.cod_variable;
                        pcodsegmento.Direction = ParameterDirection.Input;
                        pcodsegmento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodsegmento);

                        DbParameter p_riesgo_bajo = cmdTransaccionFactory.CreateParameter();
                        p_riesgo_bajo.ParameterName = "P_RIESGO_BAJO";
                        p_riesgo_bajo.Value = vVariable.riesgo_bajo;
                        p_riesgo_bajo.Direction = ParameterDirection.Input;
                        p_riesgo_bajo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_riesgo_bajo);

                        DbParameter p_riesgo_moderado = cmdTransaccionFactory.CreateParameter();
                        p_riesgo_moderado.ParameterName = "P_RIESGO_MODERADO";
                        p_riesgo_moderado.Value = vVariable.riesgo_moderado;
                        p_riesgo_moderado.Direction = ParameterDirection.Input;
                        p_riesgo_moderado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_riesgo_moderado);

                        DbParameter p_riesgo_alto = cmdTransaccionFactory.CreateParameter();
                        p_riesgo_alto.ParameterName = "P_RIESGO_ALTO";
                        p_riesgo_alto.Value = vVariable.riesgo_alto;
                        p_riesgo_alto.Direction = ParameterDirection.Input;
                        p_riesgo_alto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_riesgo_alto);

                        DbParameter p_riesgo_extremo = cmdTransaccionFactory.CreateParameter();
                        p_riesgo_extremo.ParameterName = "P_RIESGO_EXTREMO";
                        p_riesgo_extremo.Value = vVariable.riesgo_alto;
                        p_riesgo_extremo.Direction = ParameterDirection.Input;
                        p_riesgo_extremo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_riesgo_extremo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_FACTOR_SEGMENTO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return vVariable;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "ModificarVariable", ex);
                        return null;
                    }
                }
            }
        }

        public Segmentos ModificarSegmento(Segmentos vSegment, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodsegmento = cmdTransaccionFactory.CreateParameter();
                        pcodsegmento.ParameterName = "p_codsegmento";
                        pcodsegmento.Value = vSegment.codsegmento;
                        pcodsegmento.Direction = ParameterDirection.Input;
                        pcodsegmento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodsegmento);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = vSegment.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipoVariable = cmdTransaccionFactory.CreateParameter();
                        ptipoVariable.ParameterName = "p_tipo_variable";
                        ptipoVariable.Value = vSegment.tipo_variable;
                        ptipoVariable.Direction = ParameterDirection.Input;
                        ptipoVariable.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipoVariable);

                        DbParameter pcalSeg = cmdTransaccionFactory.CreateParameter();
                        pcalSeg.ParameterName = "p_calificacion_segmento";
                        pcalSeg.Value = vSegment.calificacion_segmento;
                        pcalSeg.Direction = ParameterDirection.Input;
                        pcalSeg.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcalSeg);

                        DbParameter pValAu = cmdTransaccionFactory.CreateParameter();
                        pValAu.ParameterName = "p_valida_almenosuno";
                        pValAu.Value = vSegment.valida_alguno ? 1 : 0;
                        pValAu.Direction = ParameterDirection.Input;
                        pValAu.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pValAu);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SEGMEN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return vSegment;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "ModificarSegmento", ex);
                        return null;
                    }
                }
            }
        }

        public Segmentos ConsultarSegmento(Segmentos vDatos, Usuario usuario)
        {
            DbDataReader resultado;
            Segmentos entidad = new Segmentos();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from SEGMENTOS " + ObtenerFiltro(vDatos);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODSEGMENTO"] != DBNull.Value) entidad.codsegmento = Convert.ToInt32(resultado["CODSEGMENTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_VARIABLE"] != DBNull.Value) entidad.tipo_variable = Convert.ToString(resultado["TIPO_VARIABLE"]);
                            if (resultado["CALIFICACION_SEGMENTO"] != DBNull.Value) entidad.calificacion_segmento = Convert.ToInt32(resultado["CALIFICACION_SEGMENTO"]);
                            if (resultado["VALIDA_ALMENOSUNO"] != DBNull.Value) entidad.valida_alguno = Convert.ToBoolean(resultado["VALIDA_ALMENOSUNO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "ConsultarSegmento", ex);
                        return null;
                    }
                }
            }
        }
        
        public List<tipoVariable> ListarTiposVariable(tipoVariable variables, Usuario usuario)
        {
            DbDataReader resultado;
            List<tipoVariable> listaVariables = new List<tipoVariable>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM GR_PARAM_TIPO_VARIABLE ORDER BY COD_TIPO_VARIABLE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            tipoVariable entidad = new tipoVariable();
                            if (resultado["COD_TIPO_VARIABLE"] != DBNull.Value) entidad.cod_variable = Convert.ToInt32(resultado["COD_TIPO_VARIABLE"]);
                            if (resultado["VARIABLE_RIESGO"] != DBNull.Value) entidad.nombre_variable = Convert.ToString(resultado["VARIABLE_RIESGO"]);
                            if (resultado["RIESGO_BAJO"] != DBNull.Value) entidad.riesgo_bajo = Convert.ToInt32(resultado["RIESGO_BAJO"]);
                            if (resultado["RIESGO_MODERADO"] != DBNull.Value) entidad.riesgo_moderado = Convert.ToInt32(resultado["RIESGO_MODERADO"]);
                            if (resultado["RIESGO_ALTO"] != DBNull.Value) entidad.riesgo_alto = Convert.ToInt32(resultado["RIESGO_ALTO"]);
                            if (resultado["RIESGO_EXTREMO"] != DBNull.Value) entidad.riesgo_extremo = Convert.ToInt32(resultado["RIESGO_EXTREMO"]);
                            listaVariables.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaVariables;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "ListarSegmentos", ex);
                        return null;
                    }
                }
            }
        }
        public List<Segmentos> ListarSegmentos(Segmentos segmentos, Usuario usuario)
        {
            DbDataReader resultado;
            List<Segmentos> listaSegmentos = new List<Segmentos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM SEGMENTOS " + ObtenerFiltro(segmentos) + " ORDER BY TIPO_VARIABLE, CODSEGMENTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Segmentos entidad = new Segmentos();
                            if (resultado["CODSEGMENTO"] != DBNull.Value) entidad.codsegmento = Convert.ToInt32(resultado["CODSEGMENTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_VARIABLE"] != DBNull.Value) entidad.tipo_variable = Convert.ToString(resultado["TIPO_VARIABLE"]);
                            if (resultado["CALIFICACION_SEGMENTO"] != DBNull.Value) entidad.calificacion_segmento = Convert.ToInt32(resultado["CALIFICACION_SEGMENTO"]);
                            switch (Convert.ToInt32(entidad.tipo_variable))
                            {
                                case 1: entidad.factor_riesgo = "ASOCIADO"; break;
                                case 2: entidad.factor_riesgo = "PRODUCTOS"; break;
                                case 3: entidad.factor_riesgo = "CANALES DE DISTRIBUCION"; break;
                                case 4: entidad.factor_riesgo = "JURISDICCION"; break;
                            }
                            listaSegmentos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaSegmentos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "ListarSegmentos", ex);
                        return null;
                    }
                }
            }
        }
        public List<listaMultiple> ListarActividadesMultiple(Usuario usuario)
        {
            DbDataReader resultado;
            List<listaMultiple> listaActividades = new List<listaMultiple>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COD_ACTIVIDADLABORAL, DESCRIPCION FROM ACTIVIDADLABORAL ORDER BY COD_ACTIVIDADLABORAL ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            listaMultiple entidad = new listaMultiple();
                            if (resultado["COD_ACTIVIDADLABORAL"] != DBNull.Value) entidad.cod_act = Convert.ToInt32(resultado["COD_ACTIVIDADLABORAL"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nombre_act = Convert.ToString(resultado["DESCRIPCION"]);
                            listaActividades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaActividades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "ListarActividadesMultiple", ex);
                        return null;
                    }
                }
            }
        }
        
        public void EliminarSegmentos(int consecutivo, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodsegmento = cmdTransaccionFactory.CreateParameter();
                        pcodsegmento.ParameterName = "p_codsegmento";
                        pcodsegmento.Value = consecutivo;
                        pcodsegmento.Direction = ParameterDirection.Input;
                        pcodsegmento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodsegmento);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SEGMEN_ELIMI";

                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "EliminarSegmentos", ex);
                    }
                }
            }
        }

        public void GuardarAnalisisOficialDeHistoricoSegmentacion(HistoricoSegmentacion historico, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pConsecutivoHistorico = cmdTransaccionFactory.CreateParameter();
                        pConsecutivoHistorico.ParameterName = "pConsecutivoHistorico";
                        pConsecutivoHistorico.Value = historico.consecutivo;
                        pConsecutivoHistorico.Direction = ParameterDirection.Input;

                        DbParameter pCodigoUsuarioAnalisis = cmdTransaccionFactory.CreateParameter();
                        pCodigoUsuarioAnalisis.ParameterName = "pCodigoUsuarioAnalisis";
                        pCodigoUsuarioAnalisis.Value = usuario.codusuario;
                        pCodigoUsuarioAnalisis.Direction = ParameterDirection.Input;

                        DbParameter pAnalisisHecho = cmdTransaccionFactory.CreateParameter();
                        pAnalisisHecho.ParameterName = "pAnalisisHecho";
                        pAnalisisHecho.Value = historico.analisisoficialcumplimiento;
                        pAnalisisHecho.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pConsecutivoHistorico);
                        cmdTransaccionFactory.Parameters.Add(pCodigoUsuarioAnalisis);
                        cmdTransaccionFactory.Parameters.Add(pAnalisisHecho);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_HISTSEGMOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "GuardarAnalisisOficialDeHistoricoSegmentacion", ex);
                    }
                }
            }
        }

        public HistoricoSegmentacion ConsultarHistoricoSegmentoAnterior(string consecutivo, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            HistoricoSegmentacion entidad = new HistoricoSegmentacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select Ant.*, Ant.Calificacion as Anterior
                                        From HISTORICO_SEGMENTACION hist
                                        INNER JOIN HISTORICO_SEGMENTACION ant on hist.Codigopersona = Ant.Codigopersona
                                                                                        and Ant.Consecutivo in (select * from (select P.Consecutivo from HISTORICO_SEGMENTACION p where P.Codigopersona = hist.Codigopersona and P.Fechacierre < hist.Fechacierre order by 1 desc) where Rownum = 1)
                                        where hist.consecutivo = " + consecutivo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["FECHACIERRE"] != DBNull.Value) entidad.fechacierre = Convert.ToDateTime(resultado["FECHACIERRE"]);
                            if (resultado["USUARIOCIERRE"] != DBNull.Value) entidad.usuariocierre = Convert.ToInt32(resultado["USUARIOCIERRE"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.segmentoactual = Convert.ToInt32(resultado["CALIFICACION"]);
                            if (resultado["SEGMENTOANTERIOR"] != DBNull.Value) entidad.segmentoanterior = Convert.ToInt32(resultado["SEGMENTOANTERIOR"]);
                            if (resultado["ANALISISOFICIALCUMPLIMIENTO"] != DBNull.Value) entidad.analisisoficialcumplimiento = Convert.ToString(resultado["ANALISISOFICIALCUMPLIMIENTO"]);
                            if (resultado["FECHAANALISIS"] != DBNull.Value) entidad.fechaanalisis = Convert.ToDateTime(resultado["FECHAANALISIS"]);
                            if (resultado["USUARIOANALISIS"] != DBNull.Value) entidad.usuarioanalisis = Convert.ToInt32(resultado["USUARIOANALISIS"]);
                            if (resultado["PUNTAJECALIFICACION"] != DBNull.Value) entidad.puntajecalificacion = Convert.ToInt64(resultado["PUNTAJECALIFICACION"]);
                            if (resultado["ENDEUDAMIENTO"] != DBNull.Value) entidad.endeudamiento = Convert.ToInt64(resultado["ENDEUDAMIENTO"]);
                            if (resultado["INGRESOSMENSUALES"] != DBNull.Value) entidad.ingresosmensuales = Convert.ToInt64(resultado["INGRESOSMENSUALES"]);
                            if (resultado["EDAD"] != DBNull.Value) entidad.edad = Convert.ToInt64(resultado["EDAD"]);
                            if (resultado["PERSONASACARGO"] != DBNull.Value) entidad.personasacargo = Convert.ToInt64(resultado["PERSONASACARGO"]);
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value) entidad.tipovivienda = Convert.ToInt64(resultado["TIPOVIVIENDA"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt64(resultado["ESTRATO"]);
                            if (resultado["NIVELACADEMICO"] != DBNull.Value) entidad.nivelacademico = Convert.ToInt64(resultado["NIVELACADEMICO"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToInt64(resultado["SEXO"]);
                            if (resultado["MONTODELCREDITOSMLV"] != DBNull.Value) entidad.montodelcreditosmlv = Convert.ToInt64(resultado["MONTODELCREDITOSMLV"]);
                            if (resultado["CIUDADRESIDENCIA"] != DBNull.Value) entidad.ciudadresidencia = Convert.ToInt64(resultado["CIUDADRESIDENCIA"]);
                            if (resultado["ACTIVIDADECONOMICA"] != DBNull.Value) entidad.actividadeconomica = Convert.ToInt64(resultado["ACTIVIDADECONOMICA"]);
                            if (resultado["SALDOPROMEDIOAHORROS"] != DBNull.Value) entidad.saldopromedioahorros = Convert.ToInt64(resultado["SALDOPROMEDIOAHORROS"]);
                            if (resultado["SALDOPROMEDIOAPORTES"] != DBNull.Value) entidad.saldopromedioaportes = Convert.ToInt64(resultado["SALDOPROMEDIOAPORTES"]);
                            if (resultado["SALDOCREDITOSACTIVOS"] != DBNull.Value) entidad.saldocreditosactivos = Convert.ToInt64(resultado["SALDOCREDITOSACTIVOS"]);
                            if (resultado["NUMEROCREDITOSACTIVOS"] != DBNull.Value) entidad.numerocreditosactivos = Convert.ToInt64(resultado["NUMEROCREDITOSACTIVOS"]);
                            if (resultado["OPERACIONESPRODUCTOSALMES"] != DBNull.Value) entidad.operacionesproductosalmes = Convert.ToInt64(resultado["OPERACIONESPRODUCTOSALMES"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "ConsultarHistoricoSegmentoAnterior", ex);
                        return null;
                    }

                }
            }
        }

        public HistoricoSegmentacion ConsultarHistoricoSegmentoActual(string consecutivo, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            HistoricoSegmentacion entidad = new HistoricoSegmentacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select hist.*, usu.nombre as NombreAnalisis, Ant.Calificacion as Anterior, Ant.segmentoaso As segmentoaso_anterior, Ant.segmentopro As Segmentopro_anterior, Ant.segmentocan Segmentocan_anterior, Ant.segmentojur As Segmentojur_anterior
                                        From HISTORICO_SEGMENTACION hist
                                        left join usuarios usu on usu.codusuario = hist.UsuarioAnalisis
                                        LEFT JOIN HISTORICO_SEGMENTACION ant on hist.Codigopersona = Ant.Codigopersona
                                                and Ant.Consecutivo in (select * from (select P.Consecutivo from HISTORICO_SEGMENTACION p where P.Codigopersona = hist.Codigopersona and P.Fechacierre < hist.Fechacierre order by 1 desc) where Rownum = 1)
                                        where hist.consecutivo = " + consecutivo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["FECHACIERRE"] != DBNull.Value) entidad.fechacierre = Convert.ToDateTime(resultado["FECHACIERRE"]);
                            if (resultado["USUARIOCIERRE"] != DBNull.Value) entidad.usuariocierre = Convert.ToInt32(resultado["USUARIOCIERRE"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.segmentoactual = Convert.ToInt32(resultado["CALIFICACION"]);
                            if (resultado["ANTERIOR"] != DBNull.Value) entidad.segmentoanterior = Convert.ToInt32(resultado["ANTERIOR"]);
                            if (resultado["ANALISISOFICIALCUMPLIMIENTO"] != DBNull.Value) entidad.analisisoficialcumplimiento = Convert.ToString(resultado["ANALISISOFICIALCUMPLIMIENTO"]);
                            if (resultado["FECHAANALISIS"] != DBNull.Value) entidad.fechaanalisis = Convert.ToDateTime(resultado["FECHAANALISIS"]);
                            if (resultado["USUARIOANALISIS"] != DBNull.Value) entidad.usuarioanalisis = Convert.ToInt32(resultado["USUARIOANALISIS"]);
                            if (resultado["PUNTAJECALIFICACION"] != DBNull.Value) entidad.puntajecalificacion = Convert.ToInt64(resultado["PUNTAJECALIFICACION"]);
                            if (resultado["ENDEUDAMIENTO"] != DBNull.Value) entidad.endeudamiento = Convert.ToInt64(resultado["ENDEUDAMIENTO"]);
                            if (resultado["INGRESOSMENSUALES"] != DBNull.Value) entidad.ingresosmensuales = Convert.ToInt64(resultado["INGRESOSMENSUALES"]);
                            if (resultado["EDAD"] != DBNull.Value) entidad.edad = Convert.ToInt64(resultado["EDAD"]);
                            if (resultado["PERSONASACARGO"] != DBNull.Value) entidad.personasacargo = Convert.ToInt64(resultado["PERSONASACARGO"]);
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value) entidad.tipovivienda = Convert.ToInt64(resultado["TIPOVIVIENDA"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt64(resultado["ESTRATO"]);
                            if (resultado["NIVELACADEMICO"] != DBNull.Value) entidad.nivelacademico = Convert.ToInt64(resultado["NIVELACADEMICO"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToInt64(resultado["SEXO"]);
                            if (resultado["MONTODELCREDITOSMLV"] != DBNull.Value) entidad.montodelcreditosmlv = Convert.ToInt64(resultado["MONTODELCREDITOSMLV"]);
                            if (resultado["CIUDADRESIDENCIA"] != DBNull.Value) entidad.ciudadresidencia = Convert.ToInt64(resultado["CIUDADRESIDENCIA"]);
                            if (resultado["ACTIVIDADECONOMICA"] != DBNull.Value) entidad.actividadeconomica = Convert.ToInt64(resultado["ACTIVIDADECONOMICA"]);
                            if (resultado["SALDOPROMEDIOAHORROS"] != DBNull.Value) entidad.saldopromedioahorros = Convert.ToInt64(resultado["SALDOPROMEDIOAHORROS"]);
                            if (resultado["SALDOPROMEDIOAPORTES"] != DBNull.Value) entidad.saldopromedioaportes = Convert.ToInt64(resultado["SALDOPROMEDIOAPORTES"]);
                            if (resultado["SALDOCREDITOSACTIVOS"] != DBNull.Value) entidad.saldocreditosactivos = Convert.ToInt64(resultado["SALDOCREDITOSACTIVOS"]);
                            if (resultado["NUMEROCREDITOSACTIVOS"] != DBNull.Value) entidad.numerocreditosactivos = Convert.ToInt64(resultado["NUMEROCREDITOSACTIVOS"]);
                            if (resultado["OPERACIONESPRODUCTOSALMES"] != DBNull.Value) entidad.operacionesproductosalmes = Convert.ToInt64(resultado["OPERACIONESPRODUCTOSALMES"]);
                            if (resultado["NOMBREANALISIS"] != DBNull.Value) entidad.nombre_usuario_analisis = Convert.ToString(resultado["NOMBREANALISIS"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "ConsultarHistoricoSegmentoActual", ex);
                        return null;
                    }

                }
            }
        }

        public List<HistoricoSegmentacion> ListarFechaCierreYaHechas(string pEstado = "D", Usuario usuario = null)
        {
            DbDataReader resultado = default(DbDataReader);
            List<HistoricoSegmentacion> lista = new List<HistoricoSegmentacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT DISTINCT(FECHACIERRE) FROM HISTORICO_SEGMENTACION ";
                        if (pEstado == "D")
                            sql += " WHERE FECHACIERRE IN (SELECT fecha FROM cierea WHERE tipo = 'X' AND estado = '"+ pEstado + "' ) ";
                        sql += " ORDER BY 1 DESC";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            HistoricoSegmentacion entidad = new HistoricoSegmentacion();

                            if (resultado["FECHACIERRE"] != DBNull.Value) entidad.fechacierre = Convert.ToDateTime(resultado["FECHACIERRE"]);

                            lista.Add(entidad);
                        }

                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistoricoSegmentacionData", "ListarFechaCierreYaHechas", ex);
                        return null;
                    }

                }
            }
        }

        public Xpinn.Comun.Entities.Cierea FechaUltimoCierre(Xpinn.Comun.Entities.Cierea pCierea, String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from cierea " + ObtenerFiltro(pCierea) + " Order by fecha desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            Xpinn.Comun.Entities.Cierea entidad = new Xpinn.Comun.Entities.Cierea();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["CAMPO1"] != DBNull.Value) entidad.campo1 = Convert.ToString(resultado["CAMPO1"]);
                            if (resultado["CAMPO2"] != DBNull.Value) entidad.campo2 = Convert.ToString(resultado["CAMPO2"]);
                            if (resultado["FECREA"] != DBNull.Value) entidad.fecrea = Convert.ToDateTime(resultado["FECREA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);

                            dbConnectionFactory.CerrarConexion(connection);

                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "FechaUltimoCierre", ex);
                        return null;
                    }
                }
            }
        }


        public void PeriodicidadCierre(ref int dias_cierre, ref int tipo_calendario, Usuario pUsuario)
        {
            dias_cierre = 30;
            tipo_calendario = 1;
            int periodicidad = 0;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string valor = "";
                        string sql = "Select valor From general Where codigo = 1910 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToString(resultado["VALOR"].ToString().Trim());
                        }
                        try
                        {
                            periodicidad = Convert.ToInt16(valor);
                        }
                        catch
                        {
                            return;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "PeriodicidadCierre", ex);
                        return;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select numero_dias, tipo_calendario From periodicidad Where cod_periodicidad = " + periodicidad;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) dias_cierre = Convert.ToInt16(resultado["NUMERO_DIAS"].ToString());
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) tipo_calendario = Convert.ToInt16(resultado["TIPO_CALENDARIO"].ToString());
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "PeriodicidadCierre", ex);
                        return;
                    }
                }
            }
        }


    }
}
