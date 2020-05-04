using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TipoListaRecaudoS
    /// </summary>
    public class CambioAProductoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoListaRecaudoS
        /// </summary>
        public CambioAProductoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public CambioAProducto ModificarProducto(CambioAProducto pCambio,String tabla, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnum_devolucion = cmdTransaccionFactory.CreateParameter();
                        pnum_devolucion.ParameterName = "p_numproducto";
                        pnum_devolucion.Value = pCambio.numero_producto;
                        pnum_devolucion.Direction = ParameterDirection.Input;
                        pnum_devolucion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_devolucion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_forma_pago";
                        pcod_persona.Value = pCambio.val_forma_pago;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_cod_empresa";
                        if (pCambio.cod_empresa != 0) pidentificacion.Value = pCambio.cod_empresa; else pidentificacion.Value = DBNull.Value;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pvalor_cuota = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota.ParameterName = "p_cuota";
                        pvalor_cuota.Value = pCambio.valor_cuota.HasValue ? (object)pCambio.valor_cuota : DBNull.Value;
                        pvalor_cuota.Direction = ParameterDirection.Input;
                        pvalor_cuota.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota);

                        DbParameter ptabla = cmdTransaccionFactory.CreateParameter();
                        ptabla.ParameterName = "p_tabla";
                        ptabla.Value = tabla;
                        ptabla.Direction = ParameterDirection.Input;
                        ptabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_CAMBIOPRODUCTOS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pCambio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioAProductoData", "ModificarProducto", ex);
                        return null;
                    }
                }
            }
        }


        public List<PersonaEmpresaRecaudo> ListarPersonaEmpresaRecaudo(PersonaEmpresaRecaudo pPersonaEmpresaRecaudo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PersonaEmpresaRecaudo> lstPersonaEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT P.* ,E.Nom_Empresa FROM Persona_Empresa_Recaudo P inner join Empresa_Recaudo E "
                                     + "ON P.Cod_Empresa = E.Cod_Empresa where P.Cod_Persona = (Select X.Cod_Persona from Persona X where X.identificacion = '"+pPersonaEmpresaRecaudo.identificacion+"') ORDER BY p.IDEMPRESARECAUDO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PersonaEmpresaRecaudo entidad = new PersonaEmpresaRecaudo();
                            //if (resultado["IDEMPRESARECAUDO"] != DBNull.Value) entidad.idempresarecaudo = Convert.ToInt64(resultado["IDEMPRESARECAUDO"]);
                            entidad.idempresarecaudo = 0;
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            entidad.porcentaje = 0;
                            lstPersonaEmpresaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersonaEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioAProductoData", "ListarPersonaEmpresaRecaudo", ex);
                        return null;
                    }
                }
            }
        }


        public List<CambioAProducto> ListarCreditoEmpresa_Recaudo(Int64 pNumRadicacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CambioAProducto> lstEmpresa = new List<CambioAProducto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Distinct P.cod_empresa, E.Nom_Empresa, c.porcentaje, c.idcrerecaudo "
                                        + "From Persona_Empresa_Recaudo P "
                                        + "Left Join Credito_Empresa_Recaudo C On P.Cod_Empresa = C.Cod_Empresa And C.Numero_Radicacion = " + pNumRadicacion.ToString()
                                        + "Left Join Empresa_Recaudo E On P.Cod_Empresa = E.Cod_Empresa "
                                        + "Where P.Cod_Persona = (SELECT x.cod_deudor FROM credito X WHERE X.Numero_Radicacion = " + pNumRadicacion.ToString() + " ) " +
                                     @"Union 
                                       Select Distinct c.cod_empresa, E.Nom_Empresa, c.porcentaje, c.idcrerecaudo "
                                        + "From Credito_Empresa_Recaudo C "
                                        + "Left Join Empresa_Recaudo E On c.Cod_Empresa = E.Cod_Empresa "
                                        + "Where C.Numero_Radicacion = " + pNumRadicacion.ToString()
                                        + "And c.cod_empresa Not In (Select p.cod_empresa From persona_empresa_recaudo p Where P.Cod_Persona = (SELECT x.cod_deudor FROM credito X WHERE X.Numero_Radicacion = " + pNumRadicacion.ToString() + " )) ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CambioAProducto entidad = new CambioAProducto();
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]);
                            if (resultado["IDCRERECAUDO"] != DBNull.Value) entidad.idempresarecaudo = Convert.ToInt64(resultado["IDCRERECAUDO"]);
                            lstEmpresa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("consultasdatacreditoData", "ListarCreditoEmpresa_Recaudo", ex);
                        return null;
                    }
                }
            }
        }


        public CambioAProducto ConsultarFormaDePagoProducto(String pId, String tabla, Usuario vUsuario)
        {
            DbDataReader resultado;
            CambioAProducto entidad = new CambioAProducto();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Empty;

                        switch (tabla)
                        {
                            case "APORTE":
                                sql = "SELECT FORMA_PAGO, COD_EMPRESA FROM APORTE WHERE NUMERO_APORTE = " + pId.ToString();
                                break;
                            case "CREDITO":
                                sql = "SELECT CASE FORMA_PAGO WHEN 'C' THEN 1 WHEN 'N' THEN 2 WHEN '1' THEN 1 WHEN '2' THEN 2 END AS FORMA_PAGO FROM CREDITO WHERE NUMERO_RADICACION = " + pId.ToString();
                                break;
                            case "AFILIACION":
                                sql = "SELECT FORMA_PAGO, COD_EMPRESA FROM PERSONA_AFILIACION WHERE IDAFILIACION = " + pId.ToString();
                                break;
                            case "SERVICIOS":
                                sql = "SELECT FORMA_PAGO, COD_EMPRESA FROM SERVICIOS WHERE NUMERO_SERVICIO = " + pId.ToString();
                                break;
                            case "AHORRO_PROGRAMADO":
                                sql = "SELECT FORMA_PAGO, COD_EMPRESA FROM AHORRO_PROGRAMADO WHERE NUMERO_PROGRAMADO = " + pId.ToString();
                                break;
                            case "AHORRO_VISTA":
                                sql = "SELECT COD_FORMA_PAGO AS FORMA_PAGO, COD_EMPRESA FROM AHORRO_VISTA WHERE NUMERO_CUENTA = " + pId.ToString();
                                break;
                        }                            

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.val_forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if( tabla != "CREDITO" ) // Date cuenta que la consulta de credito no le estoy pidiendo que me traiga el cod_empresa
                            {
                                if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioAProductoData", "ConsultarFormaDePagoProducto", ex);
                        return null;
                    }
                }
            }
        }


    }
}