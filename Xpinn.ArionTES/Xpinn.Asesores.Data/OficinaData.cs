using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class OficinaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public OficinaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Oficina> ListarOficina(Oficina pEntOficina, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Oficina> lstEntOficina = new List<Oficina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT * FROM OFICINA " + ObtenerFiltro(pEntOficina);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Oficina entityOficina = new Oficina();

                            if (reader["COD_OFICINA"] != DBNull.Value) entityOficina.IdOficina = Convert.ToInt64(reader["COD_OFICINA"].ToString());
                            if (reader["NOMBRE"] != DBNull.Value) entityOficina.NombreOficina = reader["NOMBRE"].ToString();

                            lstEntOficina.Add(entityOficina);
                        }
                        return lstEntOficina;
                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("DataOficina", "ListarOficina", ex);
                        return null;
                    }
                }
            }
        }

        public List<Oficina> ListarDireccionesOficinas(Oficina pEntOficina, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Oficina> lstEntOficina = new List<Oficina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT O.*,C.NOMCIUDAD  FROM OFICINA O left join CIUDADES C on C.CODCIUDAD = O.COD_CIUDAD " + ObtenerFiltro(pEntOficina,"O.");
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Oficina entidad = new Oficina();

                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.IdOficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.NombreOficina = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_CREACION"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["FECHA_CREACION"]).ToString("dd/MM/yyyy"); else entidad.fecha_creacion = "";
                            if (resultado["COD_CIUDAD"] != DBNull.Value) entidad.idCiudad = Convert.ToInt64(resultado["COD_CIUDAD"]); 
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["DIRECCION"]); else entidad.Direccion = "";
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["TELEFONO"]);else entidad.Telefono = "";
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["NOMCIUDAD"]); else entidad.nomciudad = "";
                            lstEntOficina.Add(entidad);
                        }
                        return lstEntOficina;
                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("DataOficina", "ListarOficina", ex);
                        return null;
                    }
                }
            }
        }


        public EntEmpresa ConsultarEmpresa(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            EntEmpresa entidad = new EntEmpresa();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT e.*, c.NOMCIUDAD FROM Empresa e left join ciudades c on e.CIUDAD = c.CODCIUDAD " + pFiltro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SIGLA"] != DBNull.Value) entidad.sigla = Convert.ToString(resultado["SIGLA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt32(resultado["CIUDAD"]);
                            if (resultado["E_MAIL"] != DBNull.Value) entidad.e_mail = Convert.ToString(resultado["E_MAIL"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nom_Ciudad = Convert.ToString(resultado["NOMCIUDAD"]); 
                            if (resultado["tipo_empresa"] != DBNull.Value) entidad.tipo_de_empresa = Convert.ToInt32(resultado["tipo_empresa"]); 
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaData", "ConsultarEmpresa", ex);
                        return null;
                    }
                }
            }
        }


    }//end class
}//end namespace
