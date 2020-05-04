using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
using Xpinn.FabricaCreditos.Data;
using System.Web;


namespace Xpinn.Cartera.Business
{
    public class ReestructuracionBusiness : GlobalData
    {

        private ReestructuracionData DAReestructuracion;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public ReestructuracionBusiness()
        {
            DAReestructuracion = new ReestructuracionData();
        }

        /// <summary>
        /// Método para listar los créditos a refinanciar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pUsuario, String filtro)
        {
            try
            {
                return DAReestructuracion.ListarCredito(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReestructuracionBusiness", "ListarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Retorna listado de personas a re-estructurar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarPersonas(Usuario pUsuario, String filtro)
        {
            try
            {
                return DAReestructuracion.ListarPersonas(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReestructuracionBusiness", "ListarPersonas", ex);
                return null;
            }
        }


        public Boolean CrearReestructurar(Reestructuracion vReestructuracion, List<Xpinn.FabricaCreditos.Entities.CreditoRecoger> LstRecoge, ref Int64 numero_radicacion, ref string error, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAReestructuracion.CrearReestructurar(vReestructuracion, ref numero_radicacion, ref error, pusuario);
                    if (error.Trim() != "")
                        return false;

                    Xpinn.FabricaCreditos.Data.CreditoRecogerData DACreditoRecoger = new Xpinn.FabricaCreditos.Data.CreditoRecogerData();

                    // Insertar los créditos que se recogen
                    foreach (Xpinn.FabricaCreditos.Entities.CreditoRecoger cRecoger in LstRecoge)
                    {
                        cRecoger.numero_radicacion = numero_radicacion;
                        DACreditoRecoger.CrearCreditoRecoger(cRecoger, pusuario);
                    }

                    Xpinn.FabricaCreditos.Data.codeudoresData DACodeudores = new Xpinn.FabricaCreditos.Data.codeudoresData();

                    // Insertar los codeudores
                    foreach (Xpinn.FabricaCreditos.Entities.codeudores cCodeudores in vReestructuracion.lstCodeudores)
                    {
                        cCodeudores.numero_radicacion = numero_radicacion;
                        DACodeudores.CrearCodeudoresCredito(cCodeudores, pusuario);
                    }

                    // Actualizar la tasa de interés
                    foreach (Xpinn.FabricaCreditos.Entities.Atributos vAtr in vReestructuracion.lstAtributos)
                    {
                        DAReestructuracion.ActualizarAtributos(numero_radicacion, vAtr, ref error, pusuario);
                        if (error.Trim() != "")
                            return false;
                    }

                    // Realizar la liquidación del crédito
                    DAReestructuracion.LiquidarCredito(numero_radicacion, ref error, pusuario);
                    if (error.Trim() != "")
                        return false;

                    ts.Complete();
                }

                return false;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                BOExcepcion.Throw("ReestructuracionBusiness", "CrearReestructurar", ex);
                return false;
            }

        }

    }
}
