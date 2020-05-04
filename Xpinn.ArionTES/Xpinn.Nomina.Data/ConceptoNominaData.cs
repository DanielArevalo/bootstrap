using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Nomina.Entities;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

namespace Xpinn.Nomina.Data
{
    public class ConceptoNominaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ConceptoNominaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public ConceptosNomina CrearConceptoNomina(ConceptosNomina pConceptosNomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pConceptosNomina.CONSECUTIVO;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pConceptosNomina.DESCRIPCION;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipoconcepto = cmdTransaccionFactory.CreateParameter();
                        ptipoconcepto.ParameterName = "p_tipoconcepto";
                        ptipoconcepto.Value = pConceptosNomina.TIPO_CONCEPTO;
                        ptipoconcepto.Direction = ParameterDirection.Input;
                        ptipoconcepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipoconcepto);

                        DbParameter pponderador = cmdTransaccionFactory.CreateParameter();
                        pponderador.ParameterName = "p_ponderador";
                        pponderador.Value = pConceptosNomina.PONDERADO;
                        pponderador.Direction = ParameterDirection.Input;
                        pponderador.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pponderador);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pConceptosNomina.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pclase = cmdTransaccionFactory.CreateParameter();
                        pclase.ParameterName = "p_clase";
                        pclase.Value = pConceptosNomina.CLASE;
                        pclase.Direction = ParameterDirection.Input;
                        pclase.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pclase);

                        DbParameter punidad_concepto = cmdTransaccionFactory.CreateParameter();
                        punidad_concepto.ParameterName = "p_unidad_concepto";
                        if (pConceptosNomina.UNIDAD_CONCEPTO == null)
                            punidad_concepto.Value = DBNull.Value;
                        else
                            punidad_concepto.Value = pConceptosNomina.UNIDAD_CONCEPTO;
                        punidad_concepto.Direction = ParameterDirection.Input;
                        punidad_concepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(punidad_concepto);

                        DbParameter pformula = cmdTransaccionFactory.CreateParameter();
                        pformula.ParameterName = "p_formula";
                        if (pConceptosNomina.FORMULA == null)
                            pformula.Value = DBNull.Value;
                        else
                            pformula.Value = pConceptosNomina.FORMULA;
                        pformula.Direction = ParameterDirection.Input;
                        pformula.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pformula);

                        DbParameter pprovisiona_extralegal = cmdTransaccionFactory.CreateParameter();
                        pprovisiona_extralegal.ParameterName = "P_PROVISIONA_EXTRALEGAL";
                        if (pConceptosNomina.provisiona_extralegal == null)
                            pprovisiona_extralegal.Value = 0;
                        else
                            pprovisiona_extralegal.Value = pConceptosNomina.provisiona_extralegal;
                        pprovisiona_extralegal.Direction = ParameterDirection.Input;
                        pprovisiona_extralegal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprovisiona_extralegal);

                        DbParameter pporcentajeprovisionextralegal = cmdTransaccionFactory.CreateParameter();
                        pporcentajeprovisionextralegal.ParameterName = "P_PORC_PROV_EXTRALEGAL";
                        if (pConceptosNomina.porcentajeprovisionextralegal == null)
                            pporcentajeprovisionextralegal.Value = 0;
                        else
                            pporcentajeprovisionextralegal.Value = pConceptosNomina.porcentajeprovisionextralegal;
                        pporcentajeprovisionextralegal.Direction = ParameterDirection.Input;
                        pporcentajeprovisionextralegal.DbType = DbType.Decimal;

                        cmdTransaccionFactory.Parameters.Add(pporcentajeprovisionextralegal);





                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CONCEP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pConceptosNomina.CONSECUTIVO = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return pConceptosNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoNominaData", "CrearConceptoNomina", ex);
                        return null;
                    }
                }
            }
        }
        public ConceptosNomina ConsultarCodigoMaximoConceptoNomina(Usuario pUsuario)
        {
            DbDataReader resultado;
            ConceptosNomina entidad = new ConceptosNomina();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT ESNULO(MAX(CONSECUTIVO ),0) as CONSECUTIVO FROM CONCEPTO_NOMINA ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.CONSECUTIVO = Convert.ToInt32(resultado["CONSECUTIVO"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoNominaData", "ConsultarCodigoMaximoConceptoNomina", ex);
                        return null;
                    }
                }
            }
        }
        public ConceptosNomina ModificarConceptoNomina(ConceptosNomina pConceptosNomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pConceptosNomina.CONSECUTIVO;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pConceptosNomina.DESCRIPCION;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipoconcepto = cmdTransaccionFactory.CreateParameter();
                        ptipoconcepto.ParameterName = "p_tipoconcepto";
                        ptipoconcepto.Value = pConceptosNomina.TIPO_CONCEPTO;
                        ptipoconcepto.Direction = ParameterDirection.Input;
                        ptipoconcepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipoconcepto);

                        DbParameter pponderador = cmdTransaccionFactory.CreateParameter();
                        pponderador.ParameterName = "p_ponderador";
                        pponderador.Value = pConceptosNomina.PONDERADO;
                        pponderador.Direction = ParameterDirection.Input;
                        pponderador.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pponderador);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pConceptosNomina.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pclase = cmdTransaccionFactory.CreateParameter();
                        pclase.ParameterName = "p_clase";
                        pclase.Value = pConceptosNomina.CLASE;
                        pclase.Direction = ParameterDirection.Input;
                        pclase.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pclase);

                        DbParameter punidad_concepto = cmdTransaccionFactory.CreateParameter();
                        punidad_concepto.ParameterName = "p_unidad_concepto";
                        if (pConceptosNomina.UNIDAD_CONCEPTO == null)
                            punidad_concepto.Value = DBNull.Value;
                        else
                            punidad_concepto.Value = pConceptosNomina.UNIDAD_CONCEPTO;
                        punidad_concepto.Direction = ParameterDirection.Input;
                        punidad_concepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(punidad_concepto);

                        DbParameter pformula = cmdTransaccionFactory.CreateParameter();
                        pformula.ParameterName = "p_formula";
                        if (pConceptosNomina.FORMULA == null)
                            pformula.Value = DBNull.Value;
                        else
                            pformula.Value = pConceptosNomina.FORMULA;
                        pformula.Direction = ParameterDirection.Input;
                        pformula.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pformula);


                        DbParameter pprovisiona_extralegal = cmdTransaccionFactory.CreateParameter();
                        pprovisiona_extralegal.ParameterName = "P_PROVISIONA_EXTRALEGAL";
                        if (pConceptosNomina.provisiona_extralegal == null)
                            pprovisiona_extralegal.Value = 0;
                        else
                            pprovisiona_extralegal.Value = pConceptosNomina.provisiona_extralegal;
                        pprovisiona_extralegal.Direction = ParameterDirection.Input;
                        pprovisiona_extralegal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprovisiona_extralegal);

                        DbParameter pporcentajeprovisionextralegal = cmdTransaccionFactory.CreateParameter();
                        pporcentajeprovisionextralegal.ParameterName = "P_PORC_PROV_EXTRALEGAL";
                        if (pConceptosNomina.porcentajeprovisionextralegal == null)
                            pporcentajeprovisionextralegal.Value = 0;
                        else
                            pporcentajeprovisionextralegal.Value = pConceptosNomina.porcentajeprovisionextralegal;
                        pporcentajeprovisionextralegal.Direction = ParameterDirection.Input;
                        pporcentajeprovisionextralegal.DbType = DbType.Decimal;

                        cmdTransaccionFactory.Parameters.Add(pporcentajeprovisionextralegal);






                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CONCEP_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pConceptosNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoNominaData", "ModificarConceptoNomina", ex);
                        return null;
                    }
                }
            }
        }
        public List<ConceptosNomina> ListarConceptosNomina(string Filtro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ConceptosNomina> lstConsulta = new List<ConceptosNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" select con.*, Tip.Descripcion as desc_tipoconcepto,
                                    CASE con.CLASE WHEN 1 THEN 'Prestacional' WHEN 2 THEN 'No Prestacional' WHEN 3 THEN 'Otros' END as desc_clase,
                                    CASE con.tipo WHEN 1 THEN 'Pago' WHEN 2 THEN 'Descuento' END as desc_tipo,
                                    CASE con.unidad_concepto WHEN 1 THEN 'Valor' WHEN 2 THEN 'Cantidad' END as desc_unidad_concepto
                                    from concepto_nomina con
                                    join tipoconcepto_nomina tip on Tip.Consecutivo = con.tipoconcepto " + Filtro + " order by con.CONSECUTIVO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ConceptosNomina entidad = new ConceptosNomina();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.CONSECUTIVO = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.DESCRIPCION = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.CLASEN = Convert.ToString(resultado["CLASE"]);
                            if (resultado["TIPOCONCEPTO"] != DBNull.Value) entidad.TIPO_CONCEPTON = Convert.ToString(resultado["TIPOCONCEPTO"]);
                            if (resultado["UNIDAD_CONCEPTO"] != DBNull.Value) entidad.UNIDAD_CONCEPTO = Convert.ToInt32(resultado["UNIDAD_CONCEPTO"]);
                            if (resultado["FORMULA"] != DBNull.Value) entidad.FORMULA = Convert.ToString(resultado["FORMULA"]);
                            if (resultado["PONDERADOR"] != DBNull.Value) entidad.PONDERADO = Convert.ToDecimal(resultado["PONDERADOR"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);
                            if (resultado["desc_clase"] != DBNull.Value) entidad.desc_clase = Convert.ToString(resultado["desc_clase"]);
                            if (resultado["desc_tipo"] != DBNull.Value) entidad.desc_tipo = Convert.ToString(resultado["desc_tipo"]);
                            if (resultado["desc_unidad_concepto"] != DBNull.Value) entidad.desc_unidad_concepto = Convert.ToString(resultado["desc_unidad_concepto"]);
                            if (resultado["desc_tipoconcepto"] != DBNull.Value) entidad.desc_tipoconcepto = Convert.ToString(resultado["desc_tipoconcepto"]);
                            if (resultado["PROVISIONA_EXTRALEGAL"] != DBNull.Value) entidad.provisiona_extralegal = Convert.ToInt32(resultado["PROVISIONA_EXTRALEGAL"]);
                            if (resultado["PORC_PROV_EXTRALEGAL"] != DBNull.Value) entidad.porcentajeprovisionextralegal = Convert.ToDecimal(resultado["PORC_PROV_EXTRALEGAL"]);

                            lstConsulta.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ConsultarMaxTiposDocumento", ex);
                        return null;
                    }
                }
            }
        }
        public ConceptosNomina EliminarConceptoNomina(ConceptosNomina pConceptoNomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        throw new NotImplementedException("Revisa el PL ya que apunta a una tabla que no es, adicional borrar un concepto nomina puede dañar algunos procesos como la liquidacion de nomina");

                        DbParameter PCONSECUTIVO = cmdTransaccionFactory.CreateParameter();
                        PCONSECUTIVO.ParameterName = "PCONSECUTIVO";
                        PCONSECUTIVO.Value = pConceptoNomina.CONSECUTIVO;
                        PCONSECUTIVO.Direction = ParameterDirection.Input;
                        PCONSECUTIVO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCONSECUTIVO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CONCEP_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pConceptoNomina.CONSECUTIVO = PCONSECUTIVO.Value != DBNull.Value ? Convert.ToInt64(PCONSECUTIVO.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return pConceptoNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoNominaData", "EliminarSeguridadSocial", ex);
                        return null;
                    }
                }
            }
        }
        public ConceptosNomina ConsultarConceptoNomina(Usuario pUsuario, string idObjeto)
        {
            DbDataReader resultado;
            ConceptosNomina entidad = new ConceptosNomina();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM CONCEPTO_NOMINA WHERE CONSECUTIVO = " + idObjeto;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.CONSECUTIVO = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.DESCRIPCION = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.CLASE = Convert.ToInt32(resultado["CLASE"]);
                            if (resultado["TIPOCONCEPTO"] != DBNull.Value) entidad.TIPO_CONCEPTO = Convert.ToInt32(resultado["TIPOCONCEPTO"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["UNIDAD_CONCEPTO"] != DBNull.Value) entidad.UNIDAD_CONCEPTO = Convert.ToInt32(resultado["UNIDAD_CONCEPTO"]);
                            if (resultado["FORMULA"] != DBNull.Value) entidad.FORMULA = Convert.ToString(resultado["FORMULA"]);
                            if (resultado["PONDERADOR"] != DBNull.Value) entidad.PONDERADO = Convert.ToDecimal(resultado["PONDERADOR"]);
                            if (resultado["PROVISIONA_EXTRALEGAL"] != DBNull.Value) entidad.provisiona_extralegal = Convert.ToInt32(resultado["PROVISIONA_EXTRALEGAL"]);
                            if (resultado["PORC_PROV_EXTRALEGAL"] != DBNull.Value) entidad.porcentajeprovisionextralegal = Convert.ToDecimal(resultado["PORC_PROV_EXTRALEGAL"]);


                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ConsultarMaxTiposDocumento", ex);
                        return null;
                    }
                }
            }
        }


        public List<ConceptosNomina> ConsultarConceptosNominaConfiltro(Usuario pUsuario, string Filtro)
        {
            DbDataReader resultado;
            List<ConceptosNomina> lstConsulta = new List<ConceptosNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM CONCEPTO_NOMINA WHERE 1=1 " + Filtro + " order by 1 asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ConceptosNomina entidad = new ConceptosNomina();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.CONSECUTIVO = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.DESCRIPCION = Convert.ToString(resultado["DESCRIPCION"]);

                            lstConsulta.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ConsultarConceptosNominaConfiltro", ex);
                        return null;
                    }
                }
            }
        }


    }
}
