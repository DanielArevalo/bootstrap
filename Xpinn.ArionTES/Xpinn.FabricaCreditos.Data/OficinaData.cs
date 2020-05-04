using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class OficinaData : GlobalData 
    {  
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public OficinaData()
        {
           dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Lineas de credito
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Lineas de credito obtenidos</returns>
        public Int32 UsuarioPuedeConsultarCreditosOficinas(int cod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Int32 resul = new Int32();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "Select count(*) as NUMERO From usuario_atribuciones Where codusuario = " + cod + " And tipoatribucion = 1";

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();
                    if (resultado.Read())
                    {
                        try { resul = Convert.ToInt32(resultado["NUMERO"]); }
                        catch { return 0; }
                    }
                    dbConnectionFactory.CerrarConexion(connection);
                    return resul;

                }
            }            
        }

        /// <summary>
        /// Obtiene la lista de Lineas de credito
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Lineas de credito obtenidos</returns>
        public Int32 UsuarioPuedecambiartasas(int cod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Int32 resul = new Int32();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "Select count(*) as NUMERO From usuario_atribuciones Where codusuario = " + cod + " And tipoatribucion = 2";

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();
                    if (resultado.Read())
                    {
                        try
                        {
                            resul = Convert.ToInt32(resultado["NUMERO"]);
                        }
                        catch { return 0; }                       
                    }
                    dbConnectionFactory.CerrarConexion(connection);
                    return resul;

                }
            }
        }

        /// <summary>
        /// Listado de oficinas
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<Oficina> ListarOficinas(Oficina pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Oficina> lstOficinas = new List<Oficina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_oficina as codigo, nombre from oficina " + ObtenerFiltro(pEntidad);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Oficina entidad = new Oficina();
                            //Asociar todos los valores a la entidad
                            if (resultado["codigo"] != DBNull.Value) entidad.Codigo = Convert.ToInt32(resultado["codigo"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["nombre"]);
                            lstOficinas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstOficinas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "ListarOficinas", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene la Oficina del usuario de credito
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Oficina obtenidos</returns>
        
        public List<Oficina> ListarOficinasAsesores(Oficina entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Oficina> lstOficinas = new List<Oficina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select a.cod_oficina as codigo, a.nombre, b.iusuario  from oficina a inner join asejecutivos b on  a.cod_oficina=b.ioficina";
                        if (pUsuario.codusuario.ToString().Trim().Length >= 0)
                        {
                            sql = sql + " where iusuario  =  " + pUsuario.codusuario.ToString();
                        }
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Oficina();
                            //Asociar todos los valores a la entidad
                            if (resultado["codigo"] != DBNull.Value) entidad.Codigo = Convert.ToInt32(resultado["codigo"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["iusuario"] != DBNull.Value) entidad.iusuario = Convert.ToInt32(resultado["iusuario"]);
                            lstOficinas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstOficinas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "ListarOficinasAsesores", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene la Oficina del usuario
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Oficina obtenidos</returns>
        
        public List<Oficina> ListarOficinasUsuarios(Oficina entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Oficina> lstOficinas = new List<Oficina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select a.cod_oficina as codigo, a.nombre,b.codusuario from oficina a inner join usuarios b on  a.cod_oficina = b.cod_oficina";
                        if (pUsuario.codusuario.ToString().Trim().Length >= 0)
                        {
                            sql = sql + " where codusuario  =  " + pUsuario.codusuario.ToString();
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Oficina();
                            //Asociar todos los valores a la entidad
                            if (resultado["codigo"] != DBNull.Value) entidad.Codigo = Convert.ToInt32(resultado["codigo"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["codusuario"] != DBNull.Value) entidad.iusuario = Convert.ToInt32(resultado["codusuario"]);
                            lstOficinas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstOficinas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "ListarOficinasUsuarios", ex);
                        return null;
                    }
                }
            }
        }


        public void ValidarComisionAporte(string pCod_Linea, ref bool comision, ref bool aporte, ref bool seguro, Usuario pUsuario)
        {
            seguro = false;
            DbDataReader resultadoComi = default(DbDataReader);
            DbDataReader resultadoApor = default(DbDataReader);
            DbDataReader resultadoSegu = default(DbDataReader);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql =  "Select * From DESCUENTOSLINEA Where cod_atr In (AtrComision()) And cod_linea_credito = '"+pCod_Linea.ToString()+"'";
                        string sql1 = "Select * From DESCUENTOSLINEA Where cod_atr In (AtrAporte())  And cod_linea_credito = '" + pCod_Linea.ToString() + "'";
                        string sql2 = "Select * From DESCUENTOSLINEA Where cod_atr In (AtrSeguro())  And cod_linea_credito = '" + pCod_Linea.ToString() + "'";
                       
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultadoComi = cmdTransaccionFactory.ExecuteReader();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql1;
                        resultadoApor = cmdTransaccionFactory.ExecuteReader();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql2;
                        resultadoSegu = cmdTransaccionFactory.ExecuteReader();

                        while (resultadoComi.Read())
                        {
                            comision = true;
                        }
                        while (resultadoApor.Read())
                        {
                            aporte = true;
                        }

                        while (resultadoSegu.Read())
                        {
                            seguro = true;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OficinaData", "ValidarComisionAporte", ex);
                    }
                }
            }
        }


    }
}