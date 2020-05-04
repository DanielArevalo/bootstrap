using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;


namespace Xpinn.Riesgo.Data
{

    public class alertasBusiness : GlobalBusiness
    {
        private AlertasData DAalertas;
        public alertasBusiness()
        {
            DAalertas = new AlertasData();
        }
        public alertas_ries Crearalertas(alertas_ries palertas, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    palertas = DAalertas.Crearalertas(palertas, vUsuario);
                    ts.Complete();
                    return palertas;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("alertasBusiness", "Crearalertas", ex);
                return null;
            }
        }



        public alertas_ries Modificaralertas(alertas_ries palertas, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    palertas = DAalertas.Modificaralertas(palertas, vUsuario);
                    ts.Complete();
                    return palertas;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("alertasBusiness", "Modificaralertas", ex);
                return null;
            }
        }


        public void Eliminaralertas(alertas_ries pAlerta_Riesgo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAalertas.Eliminaralertas(pAlerta_Riesgo, vUsuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("alertasBusiness", "Eliminaralertas", ex);
            }
        }


        public alertas_ries Consultaralertas(alertas_ries pAlertas, Usuario vUsuario)
        {
            try
            {
                pAlertas = DAalertas.Consultaralertas(pAlertas, vUsuario);
                return pAlertas;
            }
            catch (Exception ex)
            {

                BOExcepcion.Throw("alertasBusiness", "Consultaralertas", ex);
                return null;
            }
        }

        public List<alertas_ries> Listaralertas(alertas_ries palertas, Usuario usuario)
        {
            try
            {
                return DAalertas.Listaralertas(palertas, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("alertasBusiness", "Listaralertas", ex);
                return null;
            }
        }


    }
}