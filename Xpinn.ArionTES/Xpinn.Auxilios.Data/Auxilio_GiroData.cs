using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Entities;

namespace Xpinn.Auxilios.Data
{
   public class Auxilio_GiroData:GlobalData
    {
       
        protected ConnectionDataBase dbConnectionFactory;

        public Auxilio_GiroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Auxilios_Giros CrearAuxilio_giro(Auxilios_Giros pAuxGiro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidgiro = cmdTransaccionFactory.CreateParameter();
                        pidgiro.ParameterName = "p_idgiro";
                        pidgiro.Value = pAuxGiro.idgiro;
                        pidgiro.Direction = ParameterDirection.Output;
                        pidgiro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pAuxGiro.numero_auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pAuxGiro.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pAuxGiro.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pAuxGiro.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pAuxGiro.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pAuxGiro.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pAuxGiro.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pAuxGiro.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pAuxGiro.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pAuxGiro.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pAuxGiro.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_GIROS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAuxGiro.idgiro = Convert.ToInt64(pidgiro.Value);
                        return pAuxGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Auxilio_GiroData", "CrearAuxilio_giro", ex);
                        return null;
                    }
                }
            }
        }

        public List<Auxilios_Giros> ListarAuxilio_giro(Auxilios_Giros pAuxilio_giro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Auxilios_Giros> lstGiros = new List<Auxilios_Giros>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT A.*,CASE A.TIPO WHEN 0 THEN 'Asociado' WHEN 1 THEN 'Tercero' END AS NOM_TIPO FROM AUXILIOS_GIRO A" + ObtenerFiltro(pAuxilio_giro, "A.") + " ORDER BY IDGIRO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Auxilios_Giros entidad = new Auxilios_Giros();
                            if (resultado["IDGIRO"] != DBNull.Value) entidad.idgiro = Convert.ToInt64(resultado["IDGIRO"]);
                            if (resultado["NUMERO_AUXILIO"] != DBNull.Value) entidad.numero_auxilio = Convert.ToInt32(resultado["NUMERO_AUXILIO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOM_TIPO"] != DBNull.Value) entidad.nom_tipo = Convert.ToString(resultado["NOM_TIPO"]);
                            lstGiros.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGiros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Auxilio_giroData", "ListarAuxilio_giro", ex);
                        return null;
                    }
                }
            }
        }


    }
}
