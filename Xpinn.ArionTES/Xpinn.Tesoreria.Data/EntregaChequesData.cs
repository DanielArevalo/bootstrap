using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla AreasCaj
    /// </summary>
    public class EntregaChequesData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla AreasCaj
        /// </summary>
        public EntregaChequesData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public EntregaCheques CrearEntregaCheque(EntregaCheques pEntregaCheque, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidentrega = cmdTransaccionFactory.CreateParameter();
                        pidentrega.ParameterName = "p_identrega";
                        pidentrega.Value = pEntregaCheque.identrega;
                        pidentrega.Direction = ParameterDirection.Output;
                        pidentrega.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidentrega);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pEntregaCheque.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pnum_comp = cmdTransaccionFactory.CreateParameter();
                        pnum_comp.ParameterName = "p_num_comp";
                        if (pEntregaCheque.num_comp == null)
                            pnum_comp.Value = DBNull.Value;
                        else
                            pnum_comp.Value = pEntregaCheque.num_comp;
                        pnum_comp.Direction = ParameterDirection.Input;
                        pnum_comp.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_comp);

                        DbParameter ptipo_comp = cmdTransaccionFactory.CreateParameter();
                        ptipo_comp.ParameterName = "p_tipo_comp";
                        if (pEntregaCheque.tipo_comp == null)
                            ptipo_comp.Value = DBNull.Value;
                        else
                            ptipo_comp.Value = pEntregaCheque.tipo_comp;
                        ptipo_comp.Direction = ParameterDirection.Input;
                        ptipo_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_comp);

                        DbParameter pidgiro = cmdTransaccionFactory.CreateParameter();
                        pidgiro.ParameterName = "p_idgiro";
                        if (pEntregaCheque.idgiro == null)
                            pidgiro.Value = DBNull.Value;
                        else
                            pidgiro.Value = pEntregaCheque.idgiro;
                        pidgiro.Direction = ParameterDirection.Input;
                        pidgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        DbParameter pnum_cheque = cmdTransaccionFactory.CreateParameter();
                        pnum_cheque.ParameterName = "p_num_cheque";
                        if (pEntregaCheque.num_cheque == null)
                            pnum_cheque.Value = DBNull.Value;
                        else
                            pnum_cheque.Value = pEntregaCheque.num_cheque;
                        pnum_cheque.Direction = ParameterDirection.Input;
                        pnum_cheque.DbType = DbType.String;
                        pnum_cheque.Size = 50;
                        cmdTransaccionFactory.Parameters.Add(pnum_cheque);

                        DbParameter pentidad = cmdTransaccionFactory.CreateParameter();
                        pentidad.ParameterName = "p_entidad";
                        pentidad.Value = pEntregaCheque.entidad;
                        pentidad.Direction = ParameterDirection.Input;
                        pentidad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pentidad);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pEntregaCheque.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pEntregaCheque.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pidautorizacion = cmdTransaccionFactory.CreateParameter();
                        pidautorizacion.ParameterName = "p_idautorizacion";
                        if (pEntregaCheque.idautorizacion == null)
                            pidautorizacion.Value = DBNull.Value;
                        else
                            pidautorizacion.Value = pEntregaCheque.idautorizacion;
                        pidautorizacion.Direction = ParameterDirection.Input;
                        pidautorizacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidautorizacion);

                        DbParameter pfecha_entrega = cmdTransaccionFactory.CreateParameter();
                        pfecha_entrega.ParameterName = "p_fecha_entrega";
                        if (pEntregaCheque.fecha_entrega == null)
                            pfecha_entrega.Value = DBNull.Value;
                        else
                            pfecha_entrega.Value = pEntregaCheque.fecha_entrega;
                        pfecha_entrega.Direction = ParameterDirection.Input;
                        pfecha_entrega.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_entrega);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = pEntregaCheque.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ENTREGACHE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntregaCheque;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EntregaChequeData", "CrearEntregaCheque", ex);
                        return null;
                    }
                }
            }
        }

        public List<EntregaCheques> ListarEntregaCheques(EntregaCheques pEntregaCheques, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EntregaCheques> lstEntregaCheques = new List<EntregaCheques>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM v_entrega_cheques " + ObtenerFiltro(pEntregaCheques) + " ORDER BY 1 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EntregaCheques entidad = new EntregaCheques();
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["IDGIRO"] != DBNull.Value) entidad.idgiro = Convert.ToInt64(resultado["IDGIRO"]);
                            if (resultado["N_DOCUMENTO"] != DBNull.Value) entidad.n_documento = Convert.ToString(resultado["N_DOCUMENTO"]);
                            if (resultado["ENTIDAD"] != DBNull.Value) entidad.entidad = Convert.ToInt32(resultado["ENTIDAD"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["NOMBREBANCO"]);
                            if (resultado["COD_BENEF"] != DBNull.Value) entidad.cod_benef = Convert.ToInt64(resultado["COD_BENEF"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToInt32(resultado["CONCEPTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO_CHEQUE"] != DBNull.Value) entidad.estado_cheque = Convert.ToInt32(resultado["ESTADO_CHEQUE"]);
                            lstEntregaCheques.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntregaCheques;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EntregaChequesData", "ListarEntregaCheques", ex);
                        return null;
                    }
                }
            }
        }

    }
}