using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class AnalisisCreditoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public AnalisisCreditoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Analisis_Credito CrearAnalisis_Credito(Analisis_Credito pAnalisis_Credito, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidanalisis = cmdTransaccionFactory.CreateParameter();
                        pidanalisis.ParameterName = "p_idanalisis";
                        pidanalisis.Value = pAnalisis_Credito.idanalisis;
                        pidanalisis.Direction = ParameterDirection.Output;
                        pidanalisis.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidanalisis);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pAnalisis_Credito.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcapacidad_pago = cmdTransaccionFactory.CreateParameter();
                        pcapacidad_pago.ParameterName = "p_capacidad_pago";
                        if (pAnalisis_Credito.capacidad_pago == null)
                            pcapacidad_pago.Value = DBNull.Value;
                        else
                            pcapacidad_pago.Value = pAnalisis_Credito.capacidad_pago;
                        pcapacidad_pago.Direction = ParameterDirection.Input;
                        pcapacidad_pago.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcapacidad_pago);

                        DbParameter pgarantias_ofrecidas = cmdTransaccionFactory.CreateParameter();
                        pgarantias_ofrecidas.ParameterName = "p_garantias_ofrecidas";
                        if (pAnalisis_Credito.garantias_ofrecidas == null)
                            pgarantias_ofrecidas.Value = DBNull.Value;
                        else
                            pgarantias_ofrecidas.Value = pAnalisis_Credito.garantias_ofrecidas;
                        pgarantias_ofrecidas.Direction = ParameterDirection.Input;
                        pgarantias_ofrecidas.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pgarantias_ofrecidas);

                        DbParameter pdocumentos_provistos = cmdTransaccionFactory.CreateParameter();
                        pdocumentos_provistos.ParameterName = "p_documentos_provistos";
                        if (pAnalisis_Credito.documentos_provistos == null)
                            pdocumentos_provistos.Value = DBNull.Value;
                        else
                            pdocumentos_provistos.Value = pAnalisis_Credito.documentos_provistos;
                        pdocumentos_provistos.Direction = ParameterDirection.Input;
                        pdocumentos_provistos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdocumentos_provistos);

                        DbParameter panalisis_docs = cmdTransaccionFactory.CreateParameter();
                        panalisis_docs.ParameterName = "p_analisis_docs";
                        if (pAnalisis_Credito.analisis_docs == null)
                            panalisis_docs.Value = DBNull.Value;
                        else
                            panalisis_docs.Value = pAnalisis_Credito.analisis_docs;
                        panalisis_docs.Direction = ParameterDirection.Input;
                        panalisis_docs.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(panalisis_docs);

                        DbParameter panalisis_doc_cod1 = cmdTransaccionFactory.CreateParameter();
                        panalisis_doc_cod1.ParameterName = "p_analisis_doc_cod1";
                        if (pAnalisis_Credito.analisis_doc_cod1 == null)
                            panalisis_doc_cod1.Value = DBNull.Value;
                        else
                            panalisis_doc_cod1.Value = pAnalisis_Credito.analisis_doc_cod1;
                        panalisis_doc_cod1.Direction = ParameterDirection.Input;
                        panalisis_doc_cod1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(panalisis_doc_cod1);

                        DbParameter panalisis_doc_cod2 = cmdTransaccionFactory.CreateParameter();
                        panalisis_doc_cod2.ParameterName = "p_analisis_doc_cod2";
                        if (pAnalisis_Credito.analisis_doc_cod2 == null)
                            panalisis_doc_cod2.Value = DBNull.Value;
                        else
                            panalisis_doc_cod2.Value = pAnalisis_Credito.analisis_doc_cod2;
                        panalisis_doc_cod2.Direction = ParameterDirection.Input;
                        panalisis_doc_cod2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(panalisis_doc_cod2);

                        DbParameter panalisis_doc_cod3 = cmdTransaccionFactory.CreateParameter();
                        panalisis_doc_cod3.ParameterName = "p_analisis_doc_cod3";
                        if (pAnalisis_Credito.analisis_doc_cod3 == null)
                            panalisis_doc_cod3.Value = DBNull.Value;
                        else
                            panalisis_doc_cod3.Value = pAnalisis_Credito.analisis_doc_cod3;
                        panalisis_doc_cod3.Direction = ParameterDirection.Input;
                        panalisis_doc_cod3.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(panalisis_doc_cod3);

                        DbParameter pviabilidad = cmdTransaccionFactory.CreateParameter();
                        pviabilidad.ParameterName = "p_viabilidad";
                        pviabilidad.Value = pAnalisis_Credito.viabilidad;
                        pviabilidad.Direction = ParameterDirection.Input;
                        pviabilidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pviabilidad);

                        DbParameter pjustificacion = cmdTransaccionFactory.CreateParameter();
                        pjustificacion.ParameterName = "p_justificacion";
                        if (pAnalisis_Credito.justificacion == null)
                            pjustificacion.Value = DBNull.Value;
                        else
                            pjustificacion.Value = pAnalisis_Credito.justificacion;
                        pjustificacion.Direction = ParameterDirection.Input;
                        pjustificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pjustificacion);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        if (pAnalisis_Credito.cod_usuario == null)
                            pcod_usuario.Value = DBNull.Value;
                        else
                            pcod_usuario.Value = pAnalisis_Credito.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pfecha_analisis = cmdTransaccionFactory.CreateParameter();
                        pfecha_analisis.ParameterName = "p_fecha_analisis";
                        pfecha_analisis.Value = pAnalisis_Credito.fecha_analisis;
                        pfecha_analisis.Direction = ParameterDirection.Input;
                        pfecha_analisis.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_analisis);


                        DbParameter pReqAfiancol = cmdTransaccionFactory.CreateParameter();
                        pReqAfiancol.ParameterName = "pReqAfiancol";
                        pReqAfiancol.Value = pAnalisis_Credito.ReqAfiancol;
                        pReqAfiancol.Direction = ParameterDirection.Input;
                        pReqAfiancol.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pReqAfiancol);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ANALISISCRE_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAnalisis_Credito.idanalisis = Convert.ToInt32(pidanalisis.Value);

                        return pAnalisis_Credito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Analisis_CreditoData", "CrearAnalisis_Credito", ex);
                        return null;
                    }
                }
            }
        }


        public Analisis_Credito ModificarAnalisis_Credito(Analisis_Credito pAnalisis_Credito, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidanalisis = cmdTransaccionFactory.CreateParameter();
                        pidanalisis.ParameterName = "p_idanalisis";
                        pidanalisis.Value = pAnalisis_Credito.idanalisis;
                        pidanalisis.Direction = ParameterDirection.Input;
                        pidanalisis.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidanalisis);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pAnalisis_Credito.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcapacidad_pago = cmdTransaccionFactory.CreateParameter();
                        pcapacidad_pago.ParameterName = "p_capacidad_pago";
                        if (pAnalisis_Credito.capacidad_pago == null)
                            pcapacidad_pago.Value = DBNull.Value;
                        else
                            pcapacidad_pago.Value = pAnalisis_Credito.capacidad_pago;
                        pcapacidad_pago.Direction = ParameterDirection.Input;
                        pcapacidad_pago.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcapacidad_pago);

                        DbParameter pgarantias_ofrecidas = cmdTransaccionFactory.CreateParameter();
                        pgarantias_ofrecidas.ParameterName = "p_garantias_ofrecidas";
                        if (pAnalisis_Credito.garantias_ofrecidas == null)
                            pgarantias_ofrecidas.Value = DBNull.Value;
                        else
                            pgarantias_ofrecidas.Value = pAnalisis_Credito.garantias_ofrecidas;
                        pgarantias_ofrecidas.Direction = ParameterDirection.Input;
                        pgarantias_ofrecidas.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pgarantias_ofrecidas);

                        DbParameter pdocumentos_provistos = cmdTransaccionFactory.CreateParameter();
                        pdocumentos_provistos.ParameterName = "p_documentos_provistos";
                        if (pAnalisis_Credito.documentos_provistos == null)
                            pdocumentos_provistos.Value = DBNull.Value;
                        else
                            pdocumentos_provistos.Value = pAnalisis_Credito.documentos_provistos;
                        pdocumentos_provistos.Direction = ParameterDirection.Input;
                        pdocumentos_provistos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdocumentos_provistos);

                        DbParameter panalisis_docs = cmdTransaccionFactory.CreateParameter();
                        panalisis_docs.ParameterName = "p_analisis_docs";
                        if (pAnalisis_Credito.analisis_docs == null)
                            panalisis_docs.Value = DBNull.Value;
                        else
                            panalisis_docs.Value = pAnalisis_Credito.analisis_docs;
                        panalisis_docs.Direction = ParameterDirection.Input;
                        panalisis_docs.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(panalisis_docs);

                        DbParameter panalisis_doc_cod1 = cmdTransaccionFactory.CreateParameter();
                        panalisis_doc_cod1.ParameterName = "p_analisis_doc_cod1";
                        if (pAnalisis_Credito.analisis_doc_cod1 == null)
                            panalisis_doc_cod1.Value = DBNull.Value;
                        else
                            panalisis_doc_cod1.Value = pAnalisis_Credito.analisis_doc_cod1;
                        panalisis_doc_cod1.Direction = ParameterDirection.Input;
                        panalisis_doc_cod1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(panalisis_doc_cod1);

                        DbParameter panalisis_doc_cod2 = cmdTransaccionFactory.CreateParameter();
                        panalisis_doc_cod2.ParameterName = "p_analisis_doc_cod2";
                        if (pAnalisis_Credito.analisis_doc_cod2 == null)
                            panalisis_doc_cod2.Value = DBNull.Value;
                        else
                            panalisis_doc_cod2.Value = pAnalisis_Credito.analisis_doc_cod2;
                        panalisis_doc_cod2.Direction = ParameterDirection.Input;
                        panalisis_doc_cod2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(panalisis_doc_cod2);

                        DbParameter panalisis_doc_cod3 = cmdTransaccionFactory.CreateParameter();
                        panalisis_doc_cod3.ParameterName = "p_analisis_doc_cod3";
                        if (pAnalisis_Credito.analisis_doc_cod3 == null)
                            panalisis_doc_cod3.Value = DBNull.Value;
                        else
                            panalisis_doc_cod3.Value = pAnalisis_Credito.analisis_doc_cod3;
                        panalisis_doc_cod3.Direction = ParameterDirection.Input;
                        panalisis_doc_cod3.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(panalisis_doc_cod3);

                        DbParameter pviabilidad = cmdTransaccionFactory.CreateParameter();
                        pviabilidad.ParameterName = "p_viabilidad";
                        pviabilidad.Value = pAnalisis_Credito.viabilidad;
                        pviabilidad.Direction = ParameterDirection.Input;
                        pviabilidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pviabilidad);

                        DbParameter pjustificacion = cmdTransaccionFactory.CreateParameter();
                        pjustificacion.ParameterName = "p_justificacion";
                        if (pAnalisis_Credito.justificacion == null)
                            pjustificacion.Value = DBNull.Value;
                        else
                            pjustificacion.Value = pAnalisis_Credito.justificacion;
                        pjustificacion.Direction = ParameterDirection.Input;
                        pjustificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pjustificacion);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        if (pAnalisis_Credito.cod_usuario == null)
                            pcod_usuario.Value = DBNull.Value;
                        else
                            pcod_usuario.Value = pAnalisis_Credito.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pfecha_analisis = cmdTransaccionFactory.CreateParameter();
                        pfecha_analisis.ParameterName = "p_fecha_analisis";
                        pfecha_analisis.Value = pAnalisis_Credito.fecha_analisis;
                        pfecha_analisis.Direction = ParameterDirection.Input;
                        pfecha_analisis.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_analisis);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ANALISISCRE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAnalisis_Credito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Analisis_CreditoData", "ModificarAnalisis_Credito", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarAnalisis_Credito(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Analisis_Credito pAnalisis_Credito = new Analisis_Credito();
                        pAnalisis_Credito = ConsultarAnalisis_Credito(pId, vUsuario);

                        DbParameter pidanalisis = cmdTransaccionFactory.CreateParameter();
                        pidanalisis.ParameterName = "p_idanalisis";
                        pidanalisis.Value = pAnalisis_Credito.idanalisis;
                        pidanalisis.Direction = ParameterDirection.Input;
                        pidanalisis.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidanalisis);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ANALISISCRE_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Analisis_CreditoData", "EliminarAnalisis_Credito", ex);
                    }
                }
            }
        }


        public Analisis_Credito ConsultarAnalisis_Credito(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Analisis_Credito entidad = new Analisis_Credito();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Analisis_Credito WHERE IDANALISIS = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDANALISIS"] != DBNull.Value) entidad.idanalisis = Convert.ToInt32(resultado["IDANALISIS"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["CAPACIDAD_PAGO"] != DBNull.Value) entidad.capacidad_pago = Convert.ToDecimal(resultado["CAPACIDAD_PAGO"]);
                            if (resultado["GARANTIAS_OFRECIDAS"] != DBNull.Value) entidad.garantias_ofrecidas = Convert.ToString(resultado["GARANTIAS_OFRECIDAS"]);
                            if (resultado["DOCUMENTOS_PROVISTOS"] != DBNull.Value) entidad.documentos_provistos = Convert.ToString(resultado["DOCUMENTOS_PROVISTOS"]);
                            if (resultado["ANALISIS_DOCS"] != DBNull.Value) entidad.analisis_docs = Convert.ToString(resultado["ANALISIS_DOCS"]);
                            if (resultado["ANALISIS_DOC_COD1"] != DBNull.Value) entidad.analisis_doc_cod1 = Convert.ToString(resultado["ANALISIS_DOC_COD1"]);
                            if (resultado["ANALISIS_DOC_COD2"] != DBNull.Value) entidad.analisis_doc_cod2 = Convert.ToString(resultado["ANALISIS_DOC_COD2"]);
                            if (resultado["ANALISIS_DOC_COD3"] != DBNull.Value) entidad.analisis_doc_cod3 = Convert.ToString(resultado["ANALISIS_DOC_COD3"]);
                            if (resultado["VIABILIDAD"] != DBNull.Value) entidad.viabilidad = Convert.ToString(resultado["VIABILIDAD"]);
                            if (resultado["JUSTIFICACION"] != DBNull.Value) entidad.justificacion = Convert.ToString(resultado["JUSTIFICACION"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["FECHA_ANALISIS"] != DBNull.Value) entidad.fecha_analisis = Convert.ToDateTime(resultado["FECHA_ANALISIS"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Analisis_CreditoData", "ConsultarAnalisis_Credito", ex);
                        return null;
                    }
                }
            }
        }


        public Analisis_Credito ListarAnalisis_Credito(Analisis_Credito pAnalisis_Credito, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Analisis_Credito> lstAnalisis_Credito = new List<Analisis_Credito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Analisis_Credito " + ObtenerFiltro(pAnalisis_Credito) + " ORDER BY IDANALISIS ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        Analisis_Credito entidad = new Analisis_Credito();

                        while (resultado.Read())
                        {
                            if (resultado["IDANALISIS"] != DBNull.Value) entidad.idanalisis = Convert.ToInt32(resultado["IDANALISIS"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["CAPACIDAD_PAGO"] != DBNull.Value) entidad.capacidad_pago = Convert.ToDecimal(resultado["CAPACIDAD_PAGO"]);
                            if (resultado["GARANTIAS_OFRECIDAS"] != DBNull.Value) entidad.garantias_ofrecidas = Convert.ToString(resultado["GARANTIAS_OFRECIDAS"]);
                            if (resultado["DOCUMENTOS_PROVISTOS"] != DBNull.Value) entidad.documentos_provistos = Convert.ToString(resultado["DOCUMENTOS_PROVISTOS"]);
                            if (resultado["ANALISIS_DOCS"] != DBNull.Value) entidad.analisis_docs = Convert.ToString(resultado["ANALISIS_DOCS"]);
                            if (resultado["ANALISIS_DOC_COD1"] != DBNull.Value) entidad.analisis_doc_cod1 = Convert.ToString(resultado["ANALISIS_DOC_COD1"]);
                            if (resultado["ANALISIS_DOC_COD2"] != DBNull.Value) entidad.analisis_doc_cod2 = Convert.ToString(resultado["ANALISIS_DOC_COD2"]);
                            if (resultado["ANALISIS_DOC_COD3"] != DBNull.Value) entidad.analisis_doc_cod3 = Convert.ToString(resultado["ANALISIS_DOC_COD3"]);
                            if (resultado["VIABILIDAD"] != DBNull.Value) entidad.viabilidad = Convert.ToString(resultado["VIABILIDAD"]);
                            if (resultado["JUSTIFICACION"] != DBNull.Value) entidad.justificacion = Convert.ToString(resultado["JUSTIFICACION"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["FECHA_ANALISIS"] != DBNull.Value) entidad.fecha_analisis = Convert.ToDateTime(resultado["FECHA_ANALISIS"]);
                            lstAnalisis_Credito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Analisis_CreditoData", "ListarAnalisis_Credito", ex);
                        return null;
                    }
                }
            }
        }

        public bool CrearAnalisis_Info(AnalisisInfo analisisInfo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pNumero_radicacion.ParameterName = "pNumero_radicacion";
                        pNumero_radicacion.Value = analisisInfo.NumeroRadicacion;
                        pNumero_radicacion.Direction = ParameterDirection.Input;
                        pNumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pNumero_radicacion);

                        DbParameter PIngresos_Deu = cmdTransaccionFactory.CreateParameter();
                        PIngresos_Deu.ParameterName = "PIngresos_Deu";
                        PIngresos_Deu.Value = analisisInfo.Ingresos;
                        PIngresos_Deu.Direction = ParameterDirection.Input;
                        PIngresos_Deu.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PIngresos_Deu);

                        DbParameter POtros_Ingresos_Deu = cmdTransaccionFactory.CreateParameter();
                        POtros_Ingresos_Deu.ParameterName = "POtros_Ingresos_Deu";
                        POtros_Ingresos_Deu.Value = analisisInfo.OtrosIngresos;
                        POtros_Ingresos_Deu.Direction = ParameterDirection.Input;
                        POtros_Ingresos_Deu.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(POtros_Ingresos_Deu);

                        DbParameter PArrendamientos_Deu = cmdTransaccionFactory.CreateParameter();
                        PArrendamientos_Deu.ParameterName = "PArrendamientos_Deu";
                        PArrendamientos_Deu.Value = analisisInfo.Arrendamientos;
                        PArrendamientos_Deu.Direction = ParameterDirection.Input;
                        PArrendamientos_Deu.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PArrendamientos_Deu);

                        DbParameter PHonorarios_Deu = cmdTransaccionFactory.CreateParameter();
                        PHonorarios_Deu.ParameterName = "PHonorarios_Deu";
                        PHonorarios_Deu.Value = analisisInfo.Honorarios;
                        PHonorarios_Deu.Direction = ParameterDirection.Input;
                        PHonorarios_Deu.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PHonorarios_Deu);

                        DbParameter PCobro_Juridico_Deu = cmdTransaccionFactory.CreateParameter();
                        PCobro_Juridico_Deu.ParameterName = "PCobro_Juridico_Deu";
                        PCobro_Juridico_Deu.Value = analisisInfo.CobroJuridico;
                        PCobro_Juridico_Deu.Direction = ParameterDirection.Input;
                        PCobro_Juridico_Deu.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCobro_Juridico_Deu);

                        DbParameter PCIfin_Principal_deu = cmdTransaccionFactory.CreateParameter();
                        PCIfin_Principal_deu.ParameterName = "PCIfin_Principal_deu";
                        PCIfin_Principal_deu.Value = analisisInfo.CIfinPrincipal;
                        PCIfin_Principal_deu.Direction = ParameterDirection.Input;
                        PCIfin_Principal_deu.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCIfin_Principal_deu);

                        DbParameter PCifin_Codeudor_deu = cmdTransaccionFactory.CreateParameter();
                        PCifin_Codeudor_deu.ParameterName = "PCifin_Codeudor_deu";
                        PCifin_Codeudor_deu.Value = analisisInfo.CifinCodor;
                        PCifin_Codeudor_deu.Direction = ParameterDirection.Input;
                        PCifin_Codeudor_deu.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCifin_Codeudor_deu);

                        DbParameter PGastos_falimiares_deu = cmdTransaccionFactory.CreateParameter();
                        PGastos_falimiares_deu.ParameterName = "PGastos_falimiares_deu";
                        PGastos_falimiares_deu.Value = analisisInfo.Gastosfalimiares;
                        PGastos_falimiares_deu.Direction = ParameterDirection.Input;
                        PGastos_falimiares_deu.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PGastos_falimiares_deu);

                        DbParameter POtros_Descuentos_Deu = cmdTransaccionFactory.CreateParameter();
                        POtros_Descuentos_Deu.ParameterName = "POtros_Descuentos_Deu";
                        POtros_Descuentos_Deu.Value = analisisInfo.OtrosDescuentos;
                        POtros_Descuentos_Deu.Direction = ParameterDirection.Input;
                        POtros_Descuentos_Deu.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(POtros_Descuentos_Deu);

                        DbParameter PDeudas_Tercero_Deu = cmdTransaccionFactory.CreateParameter();
                        PDeudas_Tercero_Deu.ParameterName = "PDeudas_Tercero_Deu";
                        PDeudas_Tercero_Deu.Value = analisisInfo.DasTercero;
                        PDeudas_Tercero_Deu.Direction = ParameterDirection.Input;
                        PDeudas_Tercero_Deu.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PDeudas_Tercero_Deu);

                        DbParameter PAportes = cmdTransaccionFactory.CreateParameter();
                        PAportes.ParameterName = "PAportes";
                        PAportes.Value = analisisInfo.Aportes;
                        PAportes.Direction = ParameterDirection.Input;
                        PAportes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PAportes);

                        DbParameter pCreditosVigentes = cmdTransaccionFactory.CreateParameter();
                        pCreditosVigentes.ParameterName = "pCreditosVigentes";
                        pCreditosVigentes.Value = analisisInfo.CreditosVigentes;
                        pCreditosVigentes.Direction = ParameterDirection.Input;
                        pCreditosVigentes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pCreditosVigentes);

                        DbParameter PServiciosVigentes = cmdTransaccionFactory.CreateParameter();
                        PServiciosVigentes.ParameterName = "PServiciosVigentes";
                        PServiciosVigentes.Value = analisisInfo.Servicios;
                        PServiciosVigentes.Direction = ParameterDirection.Input;
                        PServiciosVigentes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PServiciosVigentes);

                        DbParameter pProteccionSalarial = cmdTransaccionFactory.CreateParameter();
                        pProteccionSalarial.ParameterName = "pProteccionSalarial";
                        pProteccionSalarial.Value = analisisInfo.ProteccionSalarial;
                        pProteccionSalarial.Direction = ParameterDirection.Input;
                        pProteccionSalarial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pProteccionSalarial);

                        DbParameter PDatoPersona = cmdTransaccionFactory.CreateParameter();
                        PDatoPersona.ParameterName = "PDatoPersona";
                        PDatoPersona.Value = analisisInfo.DatoPersona;
                        PDatoPersona.Direction = ParameterDirection.Input;
                        PDatoPersona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PDatoPersona);

                        DbParameter pCalif_CRiesgo = cmdTransaccionFactory.CreateParameter();
                        pCalif_CRiesgo.ParameterName = "pCalif_CRiesgo";
                        if (analisisInfo.calif_criesgo != null)
                            pCalif_CRiesgo.Value = analisisInfo.calif_criesgo;
                        else
                            pCalif_CRiesgo.Value = DBNull.Value;
                        pCalif_CRiesgo.Direction = ParameterDirection.Input;
                        pCalif_CRiesgo.DbType = DbType.AnsiStringFixedLength;
                        pCalif_CRiesgo.Size = 2;
                        cmdTransaccionFactory.Parameters.Add(pCalif_CRiesgo);

                        DbParameter pFecha_Consulta = cmdTransaccionFactory.CreateParameter();
                        pFecha_Consulta.ParameterName = "pFecha_Consulta";
                        if (analisisInfo.calif_criesgo != null)
                            pFecha_Consulta.Value = analisisInfo.fecha_consulta;
                        else
                            pFecha_Consulta.Value = DBNull.Value;
                        pFecha_Consulta.Direction = ParameterDirection.Input;
                        pFecha_Consulta.DbType = DbType.DateTime    ;
                        cmdTransaccionFactory.Parameters.Add(pFecha_Consulta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Usp_Xpinn_Analisis_Cred_Info";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Analisis_CreditoData", "CrearAnalisis_Info", ex);
                        return false;
                    }
                }
            }
        }

        public AnalisisInfo ConsultarAnalisis_Info(long numeroRadicacion, int tipoPersona, Usuario vUsuario)
        {
            DbDataReader resultado;
            AnalisisInfo entidad = new AnalisisInfo();
            string sql = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        switch (tipoPersona)
                        {
                            case 0:
                                sql =
                                    @"Select numero_radicacion NumeroRadicacion, INGRESOS_DEU ingresos, OTROS_INGRESOS_DEU Otros_Ingresos,ARRENDAMIENTOS_DEU Arrendamientos, 
                                    HONORARIOS_DEU HOnorarios, COBRO_JURIDICO_DEU CobroJuridico,CIFIN_PRINCIPAL_DEU CifinPrincipal, CIFIN_CODEUDOR_DEU CifinCodeudores, GASTOS_FALIMIARES_DEU GastosFamiliares,APORTES_DEU Aportes,CREDITOS_VIGEN_DEU Creditos,DEC_SERVICIOS_DEU Servicios,PROTECCION_SALARIO_DEU  ProteccionSalario, 
                                    OTROS_DESCUENTOS_DEU OtrosDescuentos, DEUDAS_TERCERO_DEU DeudasTercero, CALIF_CRIESGO_DEU As CALIF_CRIESGO, FECHA_CONSULTA_DEU as FECHA_CONSULTA  
                                    from ANALISIS_CRE_INFO where Numero_radicacion = " + numeroRadicacion;
                                break;
                            case 1:
                                sql =
                                    @"Select numero_radicacion NumeroRadicacion, INGRESOS_Cod1 ingresos, OTROS_INGRESOS_Cod1 Otros_Ingresos,ARRENDAMIENTOS_Cod1 Arrendamientos, 
                                    HONORARIOS_Cod1 HOnorarios, COBRO_JURIDICO_Cod1 CobroJuridico,CIFIN_PRINCIPAL_Cod1 CifinPrincipal, CIFIN_CODEUDOR_Cod1 CifinCodeudores, GASTOS_FALIMIARES_Cod1 GastosFamiliares,APORTES_COD1 Aportes,CREDITOS_VIGEN_COD1 Creditos,DEC_SERVICIOS_COD1 Servicios,PROTECCION_SALARIO_COD1  ProteccionSalario, 
                                    OTROS_DESCUENTOS_Cod1 OtrosDescuentos, DEUDAS_TERCERO_Cod1 DeudasTercero, CALIF_CRIESGO_COD1 As CALIF_CRIESGO, FECHA_CONSULTA_COD1 as FECHA_CONSULTA  
                                    from ANALISIS_CRE_INFO where Numero_radicacion =" + numeroRadicacion;
                                break;
                            case 2:
                                sql =
                                    @"Select numero_radicacion NumeroRadicacion , INGRESOS_Cod2 ingresos, OTROS_INGRESOS_Cod2 Otros_Ingresos,ARRENDAMIENTOS_Cod2 Arrendamientos, 
                                    HONORARIOS_Cod2 HOnorarios, COBRO_JURIDICO_Cod2 CobroJuridico,CIFIN_PRINCIPAL_Cod2 CifinPrincipal, CIFIN_CODEUDOR_Cod2 CifinCodeudores, GASTOS_FALIMIARES_Cod2 GastosFamiliares,APORTES_COD2 Aportes,CREDITOS_VIGEN_COD2 Creditos, DEC_SERVICIOS_COD2 Servicios,PROTECCION_SALARIO_COD2  ProteccionSalario, 
                                    OTROS_DESCUENTOS_Cod2 OtrosDescuentos, DEUDAS_TERCERO_Cod2 DeudasTercero, CALIF_CRIESGO_COD2 As CALIF_CRIESGO, FECHA_CONSULTA_COD2 as FECHA_CONSULTA    
                                    from ANALISIS_CRE_INFO where Numero_radicacion =" + numeroRadicacion;
                                break;
                            case 3:
                                sql =
                                    @"Select numero_radicacion NumeroRadicacion, INGRESOS_Cod3 ingresos, OTROS_INGRESOS_Cod3 Otros_Ingresos,ARRENDAMIENTOS_Cod3 Arrendamientos, 
                                    HONORARIOS_Cod3 HOnorarios, COBRO_JURIDICO_Cod3 CobroJuridico,CIFIN_PRINCIPAL_Cod3 CifinPrincipal, CIFIN_CODEUDOR_Cod3 CifinCodeudores, GASTOS_FALIMIARES_Cod3 GastosFamiliares,APORTES_COD3 Aportes,CREDITOS_VIGEN_COD3 Creditos,DEC_SERVICIOS_COD3 Servicios,PROTECCION_SALARIO_COD3  ProteccionSalario, 
                                    OTROS_DESCUENTOS_Cod3 OtrosDescuentos, DEUDAS_TERCERO_Cod3 DeudasTercero, CALIF_CRIESGO_COD3 As CALIF_CRIESGO, FECHA_CONSULTA_COD3 as FECHA_CONSULTA    
                                    from ANALISIS_CRE_INFO where Numero_radicacion =" + numeroRadicacion;
                                break;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NumeroRadicacion"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["NumeroRadicacion"]);
                            if (resultado["ingresos"] != DBNull.Value) entidad.Ingresos = Convert.ToDecimal(resultado["ingresos"]);
                            if (resultado["Otros_Ingresos"] != DBNull.Value) entidad.OtrosIngresos = Convert.ToDecimal(resultado["Otros_Ingresos"]);
                            if (resultado["HOnorarios"] != DBNull.Value) entidad.Honorarios = Convert.ToDecimal(resultado["HOnorarios"]);
                            if (resultado["Arrendamientos"] != DBNull.Value) entidad.Arrendamientos= Convert.ToDecimal(resultado["Arrendamientos"]);
                            if (resultado["CobroJuridico"] != DBNull.Value) entidad.CobroJuridico = Convert.ToInt32(resultado["CobroJuridico"]);
                            if (resultado["CifinPrincipal"] != DBNull.Value) entidad.CIfinPrincipal = Convert.ToInt32(resultado["CifinPrincipal"]);
                            if (resultado["CifinCodeudores"] != DBNull.Value) entidad.CifinCodor = Convert.ToInt32(resultado["CifinCodeudores"]);
                            if (resultado["GastosFamiliares"] != DBNull.Value) entidad.Gastosfalimiares = Convert.ToDecimal(resultado["GastosFamiliares"]);
                            if (resultado["OtrosDescuentos"] != DBNull.Value) entidad.OtrosDescuentos = Convert.ToDecimal(resultado["OtrosDescuentos"]);
                            if (resultado["DeudasTercero"] != DBNull.Value) entidad.DasTercero = Convert.ToDecimal(resultado["DeudasTercero"]);
                            if (resultado["Creditos"] != DBNull.Value) entidad.CreditosVigentes = Convert.ToInt32(resultado["Creditos"]);
                            if (resultado["Aportes"] != DBNull.Value) entidad.Aportes = Convert.ToInt32(resultado["Aportes"]);
                            if (resultado["Servicios"] != DBNull.Value) entidad.Servicios = Convert.ToInt32(resultado["Servicios"]);
                            if (resultado["ProteccionSalario"] != DBNull.Value) entidad.ProteccionSalarial = Convert.ToInt32(resultado["ProteccionSalario"]);
                            if (resultado["calif_criesgo"] != DBNull.Value) entidad.calif_criesgo = Convert.ToString(resultado["calif_criesgo"]);
                            if (resultado["fecha_consulta"] != DBNull.Value) entidad.fecha_consulta = Convert.ToDateTime(resultado["fecha_consulta"]);
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Analisis_CreditoData", "ConsultarAnalisis_Credito", ex);
                        return null;
                    }
                }
            }
        }
    }
}