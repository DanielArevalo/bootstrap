using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla MATRIZ_RIESGO
    /// </summary>
    public class EstadosFinancierosNIIFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        
        public EstadosFinancierosNIIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }            
        public EstadosFinancierosNIIF CrearConceptosNIF(EstadosFinancierosNIIF pEstadosFinancierosNIF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_concepto = cmdTransaccionFactory.CreateParameter();
                        p_cod_concepto.ParameterName = "P_CODIGO";
                        p_cod_concepto.Value = pEstadosFinancierosNIF.cod_concepto;
                        p_cod_concepto.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(p_cod_concepto);

                        DbParameter p_des_concepto = cmdTransaccionFactory.CreateParameter();
                        p_des_concepto.ParameterName = "P_DESCRIPCION";
                        p_des_concepto.Value = pEstadosFinancierosNIF.descripcion_concepto;
                        p_des_concepto.Direction = ParameterDirection.Input;                      
                        cmdTransaccionFactory.Parameters.Add(p_des_concepto);

                        DbParameter P_COD_TIPO_ESTADO_FINAN = cmdTransaccionFactory.CreateParameter();
                        P_COD_TIPO_ESTADO_FINAN.ParameterName = "P_COD_TIPO_ESTADO_FINAN";
                        P_COD_TIPO_ESTADO_FINAN.Value = pEstadosFinancierosNIF.cod_estado_financiero;
                        P_COD_TIPO_ESTADO_FINAN.Direction = ParameterDirection.Input;                       
                        cmdTransaccionFactory.Parameters.Add(P_COD_TIPO_ESTADO_FINAN);



                        DbParameter P_CORRIENTE = cmdTransaccionFactory.CreateParameter();
                        P_CORRIENTE.ParameterName = "P_CORRIENTE";
                        P_CORRIENTE.Value = pEstadosFinancierosNIF.corriente;
                        P_CORRIENTE.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_CORRIENTE);

                        DbParameter P_NOCORRIENTE = cmdTransaccionFactory.CreateParameter();
                        P_NOCORRIENTE.ParameterName = "P_NOCORRIENTE";
                        P_NOCORRIENTE.Value = pEstadosFinancierosNIF.nocorriente;
                        P_NOCORRIENTE.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_NOCORRIENTE);


                        DbParameter P_DEPENDE_DE = cmdTransaccionFactory.CreateParameter();
                        P_DEPENDE_DE.ParameterName = "P_DEPENDE_DE";
                        P_DEPENDE_DE.Value = pEstadosFinancierosNIF.depende_de;
                        P_DEPENDE_DE.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_DEPENDE_DE);

                        DbParameter P_TITULO = cmdTransaccionFactory.CreateParameter();
                        P_TITULO.ParameterName = "P_TITULO";
                        P_TITULO.Value = pEstadosFinancierosNIF.titulo;
                        P_TITULO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TITULO);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_CON_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEstadosFinancierosNIF.cod_concepto = Convert.ToInt32(p_cod_concepto.Value);

                        return pEstadosFinancierosNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "CrearConceptosNIF", ex);
                        return null;
                    }
                }
            }
        }
        public EstadosFinancierosNIIF CrearCuentasConceptosNIF(EstadosFinancierosNIIF pEstadosFinancierosNIF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_CODIGO = cmdTransaccionFactory.CreateParameter();
                        P_CODIGO.ParameterName = "P_CODIGO";
                        P_CODIGO.Value = pEstadosFinancierosNIF.codigo;
                        P_CODIGO.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(P_CODIGO);

                        DbParameter P_COD_CONCEPTO_NIIF = cmdTransaccionFactory.CreateParameter();
                        P_COD_CONCEPTO_NIIF.ParameterName = "P_COD_CONCEPTO_NIIF";
                        P_COD_CONCEPTO_NIIF.Value = pEstadosFinancierosNIF.cod_concepto;
                        P_COD_CONCEPTO_NIIF.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_CONCEPTO_NIIF);


                        DbParameter P_CUENTA = cmdTransaccionFactory.CreateParameter();
                        P_CUENTA.ParameterName = "P_CUENTA";
                        P_CUENTA.Value = pEstadosFinancierosNIF.cod_cuenta_niif;
                        P_CUENTA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_CUENTA);

                     

                        DbParameter P_CORRIENTE = cmdTransaccionFactory.CreateParameter();
                        P_CORRIENTE.ParameterName = "P_CORRIENTE";
                        P_CORRIENTE.Value = pEstadosFinancierosNIF.corriente;
                        P_CORRIENTE.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_CORRIENTE);

                        DbParameter P_NOCORRIENTE = cmdTransaccionFactory.CreateParameter();
                        P_NOCORRIENTE.ParameterName = "P_NOCORRIENTE";
                        P_NOCORRIENTE.Value = pEstadosFinancierosNIF.nocorriente;
                        P_NOCORRIENTE.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_NOCORRIENTE);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_CUENTAS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEstadosFinancierosNIF.codigo = Convert.ToInt32(P_CODIGO.Value);

                        return pEstadosFinancierosNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "CrearConceptosNIF", ex);
                        return null;
                    }
                }
            }
        }
        public EstadosFinancierosNIIF ModificarEstructuraDetalle(EstadosFinancierosNIIF pEstadosFinancierosNIF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_CODIGO = cmdTransaccionFactory.CreateParameter();
                        P_CODIGO.ParameterName = "P_CODIGO";
                        P_CODIGO.Value = pEstadosFinancierosNIF.codigo;
                        P_CODIGO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_CODIGO);

                        DbParameter P_COD_CONCEPTO_NIIF = cmdTransaccionFactory.CreateParameter();
                        P_COD_CONCEPTO_NIIF.ParameterName = "P_COD_CONCEPTO_NIIF";
                        P_COD_CONCEPTO_NIIF.Value = pEstadosFinancierosNIF.cod_concepto;
                        P_COD_CONCEPTO_NIIF.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_CONCEPTO_NIIF);



                        DbParameter P_CUENTA = cmdTransaccionFactory.CreateParameter();
                        P_CUENTA.ParameterName = "P_CUENTA";
                        if (pEstadosFinancierosNIF.cod_cuenta_niif == null)
                            P_CUENTA.Value = DBNull.Value;
                        else
                            P_CUENTA.Value = pEstadosFinancierosNIF.cod_cuenta_niif;
                        P_CUENTA.Direction = ParameterDirection.Input;
                        
                        cmdTransaccionFactory.Parameters.Add(P_CUENTA);





                        DbParameter P_CORRIENTE = cmdTransaccionFactory.CreateParameter();
                        P_CORRIENTE.ParameterName = "P_CORRIENTE";
                        P_CORRIENTE.Value = pEstadosFinancierosNIF.corriente;
                        P_CORRIENTE.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_CORRIENTE);

                        DbParameter P_NOCORRIENTE = cmdTransaccionFactory.CreateParameter();
                        P_NOCORRIENTE.ParameterName = "P_NOCORRIENTE";
                        P_NOCORRIENTE.Value = pEstadosFinancierosNIF.nocorriente;
                        P_NOCORRIENTE.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_NOCORRIENTE);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_CUENTAS_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        
                        return pEstadosFinancierosNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "ModificarEstructuraDetalle", ex);
                        return null;
                    }
                }
            }
        }
        public EstadosFinancierosNIIF ModificarConceptosNIF(EstadosFinancierosNIIF pEstadosFinancierosNIF, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                     

                        DbParameter p_cod_concepto = cmdTransaccionFactory.CreateParameter();
                        p_cod_concepto.ParameterName = "P_CODIGO";
                        p_cod_concepto.Value = pEstadosFinancierosNIF.cod_concepto;
                        p_cod_concepto.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_cod_concepto);

                        DbParameter p_des_concepto = cmdTransaccionFactory.CreateParameter();
                        p_des_concepto.ParameterName = "P_DESCRIPCION";
                        p_des_concepto.Value = pEstadosFinancierosNIF.descripcion_concepto;
                        p_des_concepto.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_des_concepto);

                        DbParameter P_COD_TIPO_ESTADO_FINAN = cmdTransaccionFactory.CreateParameter();
                        P_COD_TIPO_ESTADO_FINAN.ParameterName = "P_COD_TIPO_ESTADO_FINAN";
                        P_COD_TIPO_ESTADO_FINAN.Value = pEstadosFinancierosNIF.cod_estado_financiero;
                        P_COD_TIPO_ESTADO_FINAN.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_TIPO_ESTADO_FINAN);


                        DbParameter P_DEPENDE_DE = cmdTransaccionFactory.CreateParameter();
                        P_DEPENDE_DE.ParameterName = "P_DEPENDE_DE";
                        P_DEPENDE_DE.Value = pEstadosFinancierosNIF.depende_de;
                        P_DEPENDE_DE.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_DEPENDE_DE);


                        DbParameter P_TITULO = cmdTransaccionFactory.CreateParameter();
                        P_TITULO.ParameterName = "P_TITULO";
                        P_TITULO.Value = pEstadosFinancierosNIF.titulo;
                        P_TITULO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TITULO);





                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_CON_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();

                     
                        return pEstadosFinancierosNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "ModificarConceptosNIF", ex);
                        return null;
                    }
                }
            }
        }
        public void EliminarConceptosNIF(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        EstadosFinancierosNIIF pEstadosFinancierosNIF = new EstadosFinancierosNIIF();
                        pEstadosFinancierosNIF = ConsultarConceptosNIF(pId, vUsuario);

                        DbParameter pidmatriz = cmdTransaccionFactory.CreateParameter();
                        pidmatriz.ParameterName = "p_cod_concepto";
                        pidmatriz.Value = pEstadosFinancierosNIF.cod_concepto;
                        pidmatriz.Direction = ParameterDirection.Input;
                        pidmatriz.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidmatriz);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_MATRIZRIES_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "EliminarConceptosNIF", ex);
                    }
                }
            }
        }
        public void EliminarCuentasConceptosNIF(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        EstadosFinancierosNIIF pEstadosFinancierosNIF = new EstadosFinancierosNIIF();

                        DbParameter p_codigo = cmdTransaccionFactory.CreateParameter();
                        p_codigo.ParameterName = "p_codigo";
                        p_codigo.Value = pId;
                        p_codigo.Direction = ParameterDirection.Input;
                    
                        cmdTransaccionFactory.Parameters.Add(p_codigo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_CUENTAS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "EliminarCuentasConceptosNIF", ex);
                    }
                }
            }
        }


        public EstadosFinancierosNIIF ConsultarConceptosNIF(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            EstadosFinancierosNIIF entidad = new EstadosFinancierosNIIF();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CONCEPTOS_NIIF WHERE codigo = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["codigo"] != DBNull.Value) entidad.cod_concepto = Convert.ToInt32(resultado["codigo"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion_concepto = Convert.ToString(resultado["descripcion"]);

                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToInt32(resultado["DEPENDE_DE"]);
                            if (resultado["SOLOTITULO"] != DBNull.Value) entidad.titulo = Convert.ToInt32(resultado["SOLOTITULO"]);

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
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "ConsultarConceptosNIF", ex);
                        return null;
                    }
                }
            }
        }     
        public List<EstadosFinancierosNIIF>  ConsultarCuentasNIIF(Int32 nivel, Usuario vUsuario)
        {
            DbDataReader resultado;
          
            List<EstadosFinancierosNIIF> lstConsulta = new List<EstadosFinancierosNIIF>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PLAN_CUENTAS_NIIF where 1=1 and nivel= " + nivel + " order by 1 asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {

                            EstadosFinancierosNIIF entidad = new EstadosFinancierosNIIF();
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.desc_cuenta_niif = Convert.ToString(resultado["NOMBRE"]);

                            lstConsulta.Add(entidad);

                        }
                       
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta; 
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "ConsultarCuentasNIIF", ex);
                        return null;
                    }
                }
            }
        }
        public List<EstadosFinancierosNIIF> ConsultarDependeDe(Int32 tipo, Usuario vUsuario)
        {
            DbDataReader resultado;

            List<EstadosFinancierosNIIF> lstConsulta = new List<EstadosFinancierosNIIF>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       
                        string sql = @"SELECT depende_de as ListaId FROM CONCEPTOS_NIIF where 1=1 and COD_TIPO_ESTADO_FINAN= " + tipo + " order by 1 asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {

                            EstadosFinancierosNIIF entidad = new EstadosFinancierosNIIF();
                            if (resultado["ListaId"] != DBNull.Value) entidad.depende_de = Convert.ToInt32(resultado["ListaId"]);
                           
                            lstConsulta.Add(entidad);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "ConsultarDependeDe", ex);
                        return null;
                    }
                }
            }
        }


        public List<EstadosFinancierosNIIF> ConsultarCuentasLocalNIIF(Int32 nivel,Usuario vUsuario)
        {
            DbDataReader resultado;

            List<EstadosFinancierosNIIF> lstConsulta = new List<EstadosFinancierosNIIF>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PLAN_CUENTAS   where 1=1 and  nivel= " + nivel + " order by 1 asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {

                            EstadosFinancierosNIIF entidad = new EstadosFinancierosNIIF();
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.desc_cuenta_niif = Convert.ToString(resultado["NOMBRE"]);

                            lstConsulta.Add(entidad);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "ConsultarCuentasNIIF", ex);
                        return null;
                    }
                }
            }
        }
        public List<EstadosFinancierosNIIF> ListarCuentasNIIF(Int64 pcodigo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EstadosFinancierosNIIF> lstEstadosFinancierosNIIF = new List<EstadosFinancierosNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // string sql = @"SELECT A.CODIGO, B.COD_CUENTA_NIIF,B.NOMBRE,(Case A.CORRIENTE When 1 Then 1 When 0 Then 0 When null Then 0 Else 0 End) As CORRIENTE,(Case A.NOCORRIENTE  When 1 Then 1 When 0 Then 0 When null Then 0 Else 0 End) As NOCORRIENTE FROM PLAN_CUENTAS_NIIF B LEFT JOIN CUENTAS_CONCEPTOS_NIIF  A ON A.COD_CUENTA_NIIF=B.COD_CUENTA_NIIF ORDER BY B.COD_CUENTA_NIIF ASC ";

                        string sql = @"SELECT A.CODIGO,A.COD_CUENTA_NIIF,B.NOMBRE,(Case A.CORRIENTE When 1 Then 1 When 0 Then 0 When null Then 0 Else 0 End) As CORRIENTE,(Case A.NOCORRIENTE  When 1 Then 1 When 0 Then 0 When null Then 0 Else 0 End) As NOCORRIENTE FROM CUENTAS_CONCEPTOS_NIIF  A inner JOIN PLAN_CUENTAS_NIIF B ON A.COD_CUENTA_NIIF = B.COD_CUENTA_NIIF  WHERE A.COD_CONCEPTO_NIIF = " + pcodigo + "  union all  SELECT 0 as CODIGO, B.COD_CUENTA_NIIF,B.NOMBRE,0 As CORRIENTE, 0 As NOCORRIENTE FROM PLAN_CUENTAS_NIIF B  where B.COD_CUENTA_NIIF not in(select B.COD_CUENTA_NIIF FROM PLAN_CUENTAS_NIIF B inner JOIN CUENTAS_CONCEPTOS_NIIF A ON A.COD_CUENTA_NIIF = B.COD_CUENTA_NIIF where A.COD_CONCEPTO_NIIF =  " + pcodigo + ") order by 2 asc ";                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EstadosFinancierosNIIF entidad = new EstadosFinancierosNIIF();
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.desc_cuenta_niif = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CORRIENTE"] != DBNull.Value) entidad.corriente = Convert.ToInt32(resultado["CORRIENTE"]);                         
                            if (resultado["NOCORRIENTE"] != DBNull.Value) entidad.nocorriente = Convert.ToInt32(resultado["NOCORRIENTE"]);
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt32(resultado["CODIGO"]);
                          
                            lstEstadosFinancierosNIIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstadosFinancierosNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "ListarCuentasNIIF", ex);
                        return null;
                    }
                }
            }
        }

        public List<EstadosFinancierosNIIF> ListarCuentasLocalNIIF(Int64 pcodigo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EstadosFinancierosNIIF> lstEstadosFinancierosNIIF = new List<EstadosFinancierosNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // string sql = @"SELECT A.CODIGO, B.COD_CUENTA_NIIF,B.NOMBRE,(Case A.CORRIENTE When 1 Then 1 When 0 Then 0 When null Then 0 Else 0 End) As CORRIENTE,(Case A.NOCORRIENTE  When 1 Then 1 When 0 Then 0 When null Then 0 Else 0 End) As NOCORRIENTE FROM PLAN_CUENTAS_NIIF B LEFT JOIN CUENTAS_CONCEPTOS_NIIF  A ON A.COD_CUENTA_NIIF=B.COD_CUENTA_NIIF ORDER BY B.COD_CUENTA_NIIF ASC ";

                        string sql = @"SELECT A.CODIGO,A.COD_CUENTA_NIIF,B.NOMBRE,(Case A.CORRIENTE When 1 Then 1 When 0 Then 0 When null Then 0 Else 0 End) As CORRIENTE,(Case A.NOCORRIENTE  When 1 Then 1 When 0 Then 0 When null Then 0 Else 0 End) As NOCORRIENTE FROM CUENTAS_CONCEPTOS_NIIF  A inner JOIN PLAN_CUENTAS B ON A.COD_CUENTA_NIIF = B.COD_CUENTA  WHERE A.COD_CONCEPTO_NIIF = " + pcodigo + "  union all  SELECT 0 as CODIGO, B.COD_CUENTA as COD_CUENTA_NIIF,B.NOMBRE,0 As CORRIENTE, 0 As NOCORRIENTE FROM PLAN_CUENTAS B  where B.COD_CUENTA not in(select B.COD_CUENTA FROM PLAN_CUENTAS B inner JOIN CUENTAS_CONCEPTOS_NIIF A ON A.COD_CUENTA_NIIF = B.COD_CUENTA where A.COD_CONCEPTO_NIIF =  " + pcodigo + ") order by 2 asc "; connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EstadosFinancierosNIIF entidad = new EstadosFinancierosNIIF();
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.desc_cuenta_niif = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CORRIENTE"] != DBNull.Value) entidad.corriente = Convert.ToInt32(resultado["CORRIENTE"]);
                            if (resultado["NOCORRIENTE"] != DBNull.Value) entidad.nocorriente = Convert.ToInt32(resultado["NOCORRIENTE"]);
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt32(resultado["CODIGO"]);

                            lstEstadosFinancierosNIIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstadosFinancierosNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "ListarCuentasNIIF", ex);
                        return null;
                    }
                }
            }
        }


        public List<EstadosFinancierosNIIF> ListarConceptosNIF(Int64 pestadofinanciero, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EstadosFinancierosNIIF> lstEstadosFinancierosNIIF = new List<EstadosFinancierosNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CONCEPTOS_NIIF sc LEFT JOIN tipo_est_finan_niif c ON sc.COD_TIPO_ESTADO_FINAN = c.codigo
                                        WHERE SC.COD_TIPO_ESTADO_FINAN= " + pestadofinanciero + "  ORDER BY SC.CODIGO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EstadosFinancierosNIIF entidad = new EstadosFinancierosNIIF();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.cod_concepto = Convert.ToInt32(resultado["CODIGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion_concepto = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToInt32(resultado["DEPENDE_DE"]);
                            if (resultado["SOLOTITULO"] != DBNull.Value) entidad.titulo = Convert.ToInt32(resultado["SOLOTITULO"]);


                            lstEstadosFinancierosNIIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstadosFinancierosNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "ListarConceptosNIF", ex);
                        return null;
                    }
                }
            }
        }
        public List<EstadosFinancierosNIIF> ListarTipoEstadoFinancieroNIIF(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EstadosFinancierosNIIF> lstEstadosFinancierosNIIF = new List<EstadosFinancierosNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT *    FROM TIPO_EST_FINAN_NIIF  " + filtro + "  ORDER BY 1 asc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EstadosFinancierosNIIF entidad = new EstadosFinancierosNIIF();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt32(resultado["CODIGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstEstadosFinancierosNIIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstadosFinancierosNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosNIFData", "ListarTipoEstadoFinancieroNIIF", ex);
                        return null;
                    }
                }
            }
        }


    }
}