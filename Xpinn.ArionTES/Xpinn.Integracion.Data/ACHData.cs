using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;

using Xpinn.Integracion.Entities;

namespace Xpinn.Integracion.Data
{
    public class ACHData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ACHData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public ACHPayment CreateModifyPaymentACH(ACHPayment pPaymentACH, ref string pError, OperacionCRUD pOperacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_payment = cmdTransaccionFactory.CreateParameter();
                        pid_payment.ParameterName = "p_id_payment";
                        pid_payment.Value = pPaymentACH.ID;
                        pid_payment.Direction = pOperacion == OperacionCRUD.Crear ? ParameterDirection.Output : ParameterDirection.Input;
                        pid_payment.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_payment);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pPaymentACH.Cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pPaymentACH.Cod_ope == 0)
                            pcod_ope.Value = DBNull.Value;
                        else
                            pcod_ope.Value = pPaymentACH.Cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter ptypepayment = cmdTransaccionFactory.CreateParameter();
                        ptypepayment.ParameterName = "p_typepayment";
                        ptypepayment.Value = (int)pPaymentACH.Type;
                        ptypepayment.Direction = ParameterDirection.Input;
                        ptypepayment.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptypepayment);

                        DbParameter pamount = cmdTransaccionFactory.CreateParameter();
                        pamount.ParameterName = "p_amount";
                        pamount.Value = pPaymentACH.Amount;
                        pamount.Direction = ParameterDirection.Input;
                        pamount.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pamount);

                        DbParameter piva_amount = cmdTransaccionFactory.CreateParameter();
                        piva_amount.ParameterName = "p_iva_amount";
                        piva_amount.Value = pPaymentACH.VATAmount;
                        piva_amount.Direction = ParameterDirection.Input;
                        piva_amount.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(piva_amount);

                        DbParameter ppaymentdescription = cmdTransaccionFactory.CreateParameter();
                        ppaymentdescription.ParameterName = "p_paymentdescription";
                        if (string.IsNullOrEmpty(pPaymentACH.PaymentDescription))
                            ppaymentdescription.Value = DBNull.Value;
                        else
                            ppaymentdescription.Value = pPaymentACH.PaymentDescription;
                        ppaymentdescription.Direction = ParameterDirection.Input;
                        ppaymentdescription.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ppaymentdescription);

                        DbParameter preferencenumber1 = cmdTransaccionFactory.CreateParameter();
                        preferencenumber1.ParameterName = "p_referencenumber1";
                        if (string.IsNullOrEmpty(pPaymentACH.ReferenceNumber1))
                            preferencenumber1.Value = DBNull.Value;
                        else
                            preferencenumber1.Value = pPaymentACH.ReferenceNumber1;
                        preferencenumber1.Direction = ParameterDirection.Input;
                        preferencenumber1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preferencenumber1);

                        DbParameter preferencenumber2 = cmdTransaccionFactory.CreateParameter();
                        preferencenumber2.ParameterName = "p_referencenumber2";
                        if (string.IsNullOrEmpty(pPaymentACH.ReferenceNumber2))
                            preferencenumber2.Value = DBNull.Value;
                        else
                            preferencenumber2.Value = pPaymentACH.ReferenceNumber2;
                        preferencenumber2.Direction = ParameterDirection.Input;
                        preferencenumber2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preferencenumber2);

                        DbParameter preferencenumber3 = cmdTransaccionFactory.CreateParameter();
                        preferencenumber3.ParameterName = "p_referencenumber3";
                        if (string.IsNullOrEmpty(pPaymentACH.ReferenceNumber3))
                            preferencenumber3.Value = DBNull.Value;
                        else
                            preferencenumber3.Value = pPaymentACH.ReferenceNumber3;
                        preferencenumber3.Direction = ParameterDirection.Input;
                        preferencenumber3.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preferencenumber3);

                        DbParameter pservicecode = cmdTransaccionFactory.CreateParameter();
                        pservicecode.ParameterName = "p_servicecode";
                        if (string.IsNullOrEmpty(pPaymentACH.ServiceCode))
                            pservicecode.Value = DBNull.Value;
                        else
                            pservicecode.Value = pPaymentACH.ServiceCode;
                        pservicecode.Direction = ParameterDirection.Input;
                        pservicecode.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pservicecode);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (string.IsNullOrEmpty(pPaymentACH.Email))
                            pemail.Value = DBNull.Value;
                        else
                            pemail.Value = pPaymentACH.Email;
                        pemail.Direction = ParameterDirection.Input;
                        pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pstate = cmdTransaccionFactory.CreateParameter();
                        pstate.ParameterName = "p_state";
                        pstate.Value = (int)pPaymentACH.Status;
                        pstate.Direction = ParameterDirection.Input;
                        pstate.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pstate);

                        DbParameter pidentifier = cmdTransaccionFactory.CreateParameter();
                        pidentifier.ParameterName = "p_identifier";
                        if (string.IsNullOrEmpty(pPaymentACH.Identifier))
                            pidentifier.Value = DBNull.Value;
                        else
                            pidentifier.Value = pPaymentACH.Identifier;
                        pidentifier.Direction = ParameterDirection.Input;
                        pidentifier.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentifier);

                        DbParameter pbankcode = cmdTransaccionFactory.CreateParameter();
                        pbankcode.ParameterName = "p_bankcode";
                        if (string.IsNullOrEmpty(pPaymentACH.BankCode))
                            pbankcode.Value = DBNull.Value;
                        else
                            pbankcode.Value = pPaymentACH.BankCode;
                        pbankcode.Direction = ParameterDirection.Input;
                        pbankcode.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pbankcode);

                        DbParameter pbankname = cmdTransaccionFactory.CreateParameter();
                        pbankname.ParameterName = "p_bankname";
                        if (string.IsNullOrEmpty(pPaymentACH.BankName))
                            pbankname.Value = DBNull.Value;
                        else
                            pbankname.Value = pPaymentACH.BankName;
                        pbankname.Direction = ParameterDirection.Input;
                        pbankname.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pbankname);

                        DbParameter ptype_product = cmdTransaccionFactory.CreateParameter();
                        ptype_product.ParameterName = "p_type_product";
                        ptype_product.Value = pPaymentACH.TypeProduct;
                        ptype_product.Direction = ParameterDirection.Input;
                        ptype_product.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptype_product);

                        DbParameter pnumber_product = cmdTransaccionFactory.CreateParameter();
                        pnumber_product.ParameterName = "p_number_product";
                        if (string.IsNullOrEmpty(pPaymentACH.NumberProduct))
                            pnumber_product.Value = DBNull.Value;
                        else
                            pnumber_product.Value = pPaymentACH.NumberProduct;
                        pnumber_product.Direction = ParameterDirection.Input;
                        pnumber_product.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumber_product);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        pcodusuario.Value = vUsuario.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        DbParameter pmensaje = cmdTransaccionFactory.CreateParameter();
                        pmensaje.ParameterName = "p_mensaje";
                        pmensaje.Value = DBNull.Value;
                        pmensaje.Direction = ParameterDirection.Output;
                        pmensaje.DbType = DbType.String;
                        pmensaje.Size = 2000;
                        cmdTransaccionFactory.Parameters.Add(pmensaje);

                        DbParameter p_TrazabilityCode = cmdTransaccionFactory.CreateParameter();
                        p_TrazabilityCode.ParameterName = "p_TrazabilityCode";
                        p_TrazabilityCode.Value = pPaymentACH.TrazabilityCode;
                        p_TrazabilityCode.Direction = ParameterDirection.Output;
                        p_TrazabilityCode.DbType = DbType.String;
                        p_TrazabilityCode.Size = 2000;
                        cmdTransaccionFactory.Parameters.Add(p_TrazabilityCode);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pOperacion == OperacionCRUD.Crear ? "USP_XPINN_TES_PAYMENTACH_CREAR" : "USP_XPINN_TES_PAYMENTACH_APPLY";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pOperacion == OperacionCRUD.Crear)
                        {
                            if (pid_payment.Value == DBNull.Value)
                            {
                                pError = "SE GENERÓ UN ERROR AL CREAR LA TRANSACCIÓN";
                            }
                            else // CAPA BUSINNES SE CONTROLA SI GENERO BIEN EL CODIGO
                                pPaymentACH.ID = Convert.ToInt64(pid_payment.Value);
                        }
                        if (pmensaje.Value != DBNull.Value)
                            pError = Convert.ToString(pmensaje.Value);

                        return pPaymentACH;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }

        public ACHPayment ConsultPaymentACH(string pFiltro, ref string pError, Usuario vUsuario)
        {
            DbDataReader resultado;
            ACHPayment entidad = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PAYMENT_ACH " + pFiltro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            entidad = new ACHPayment();
                            if (resultado.Read())
                            {
                                if (resultado["ID_PAYMENT"] != DBNull.Value) entidad.ID = Convert.ToInt64(resultado["ID_PAYMENT"]);
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.Cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["COD_OPE"] != DBNull.Value) entidad.Cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                                if (resultado["TYPEPAYMENT"] != DBNull.Value) entidad.Type = resultado["TYPEPAYMENT"].ToString().ToEnum<PaymentTypeEnum>();
                                if (resultado["AMOUNT"] != DBNull.Value) entidad.Amount = Convert.ToDecimal(resultado["AMOUNT"]);
                                if (resultado["IVA_AMOUNT"] != DBNull.Value) entidad.VATAmount = Convert.ToDecimal(resultado["IVA_AMOUNT"]);
                                if (resultado["PAYMENTDESCRIPTION"] != DBNull.Value) entidad.PaymentDescription = Convert.ToString(resultado["PAYMENTDESCRIPTION"]);
                                if (resultado["REFERENCENUMBER1"] != DBNull.Value) entidad.ReferenceNumber1 = Convert.ToString(resultado["REFERENCENUMBER1"]);
                                if (resultado["REFERENCENUMBER2"] != DBNull.Value) entidad.ReferenceNumber2 = Convert.ToString(resultado["REFERENCENUMBER2"]);
                                if (resultado["REFERENCENUMBER3"] != DBNull.Value) entidad.ReferenceNumber3 = Convert.ToString(resultado["REFERENCENUMBER3"]);
                                if (resultado["SERVICECODE"] != DBNull.Value) entidad.ServiceCode = Convert.ToString(resultado["SERVICECODE"]);
                                if (resultado["EMAIL"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["EMAIL"]);
                                if (resultado["STATE"] != DBNull.Value) entidad.Status = resultado["STATE"].ToString().ToEnum<PaymentStatusEnum>();
                                if (resultado["FECHA_CREACION"] != DBNull.Value) entidad.Fecha_Creacion = Convert.ToDateTime(resultado["FECHA_CREACION"]);
                                if (resultado["IDENTIFIER"] != DBNull.Value) entidad.Identifier = Convert.ToString(resultado["IDENTIFIER"]);
                                if (resultado["BANKCODE"] != DBNull.Value) entidad.BankCode = Convert.ToString(resultado["BANKCODE"]);
                                if (resultado["BANKNAME"] != DBNull.Value) entidad.BankName = Convert.ToString(resultado["BANKNAME"]);
                                if (resultado["TYPE_PRODUCT"] != DBNull.Value) entidad.TypeProduct = Convert.ToInt32(resultado["TYPE_PRODUCT"]);
                                if (resultado["NUMBER_PRODUCT"] != DBNull.Value) entidad.NumberProduct = Convert.ToString(resultado["NUMBER_PRODUCT"]);
                                if (resultado["TrazabilityCode"] != DBNull.Value) entidad.TrazabilityCode = Convert.ToString(resultado["TrazabilityCode"]);

                            }
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }

        public List<ACHPayment> ListPaymentACH(string pFiltro, ref string pError, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ACHPayment> lstPayments = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PAYMENT_ACH " + pFiltro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            lstPayments = new List<ACHPayment>();
                            ACHPayment entidad;
                            while (resultado.Read())
                            {
                                entidad = new ACHPayment();
                                if (resultado["ID_PAYMENT"] != DBNull.Value) entidad.ID = Convert.ToInt64(resultado["ID_PAYMENT"]);
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.Cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["COD_OPE"] != DBNull.Value) entidad.Cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                                if (resultado["TYPEPAYMENT"] != DBNull.Value) entidad.Type = resultado["TYPEPAYMENT"].ToString().ToEnum<PaymentTypeEnum>();
                                if (resultado["AMOUNT"] != DBNull.Value) entidad.Amount = Convert.ToDecimal(resultado["AMOUNT"]);
                                if (resultado["IVA_AMOUNT"] != DBNull.Value) entidad.VATAmount = Convert.ToDecimal(resultado["IVA_AMOUNT"]);
                                if (resultado["PAYMENTDESCRIPTION"] != DBNull.Value) entidad.PaymentDescription = Convert.ToString(resultado["PAYMENTDESCRIPTION"]);
                                if (resultado["REFERENCENUMBER1"] != DBNull.Value) entidad.ReferenceNumber1 = Convert.ToString(resultado["REFERENCENUMBER1"]);
                                if (resultado["REFERENCENUMBER2"] != DBNull.Value) entidad.ReferenceNumber2 = Convert.ToString(resultado["REFERENCENUMBER2"]);
                                if (resultado["REFERENCENUMBER3"] != DBNull.Value) entidad.ReferenceNumber3 = Convert.ToString(resultado["REFERENCENUMBER3"]);
                                if (resultado["SERVICECODE"] != DBNull.Value) entidad.ServiceCode = Convert.ToString(resultado["SERVICECODE"]);
                                if (resultado["EMAIL"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["EMAIL"]);
                                if (resultado["STATE"] != DBNull.Value) entidad.Status = resultado["STATE"].ToString().ToEnum<PaymentStatusEnum>();
                                if (resultado["FECHA_CREACION"] != DBNull.Value) entidad.Fecha_Creacion = Convert.ToDateTime(resultado["FECHA_CREACION"]);
                                if (resultado["IDENTIFIER"] != DBNull.Value) entidad.Identifier = Convert.ToString(resultado["IDENTIFIER"]);
                                if (resultado["BANKCODE"] != DBNull.Value) entidad.BankCode = Convert.ToString(resultado["BANKCODE"]);
                                if (resultado["BANKNAME"] != DBNull.Value) entidad.BankName = Convert.ToString(resultado["BANKNAME"]);
                                if (resultado["TYPE_PRODUCT"] != DBNull.Value) entidad.TypeProduct = Convert.ToInt32(resultado["TYPE_PRODUCT"]);
                                if (resultado["NUMBER_PRODUCT"] != DBNull.Value) entidad.NumberProduct = Convert.ToString(resultado["NUMBER_PRODUCT"]);
                                if (resultado["TrazabilityCode"] != DBNull.Value) entidad.TrazabilityCode = Convert.ToString(resultado["TrazabilityCode"]);

                                lstPayments.Add(entidad);
                            }
                        }
                        return lstPayments;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }
    }
}
