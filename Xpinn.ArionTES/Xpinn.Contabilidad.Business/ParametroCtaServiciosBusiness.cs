using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    public class ParametroCtaServiciosBusiness : GlobalBusiness
    {

        private ParametroCtaServiciosData DApar_cue_linser;

        public ParametroCtaServiciosBusiness()
        {
            DApar_cue_linser = new ParametroCtaServiciosData();
        }

        public par_cue_linser Crearpar_cue_linser(par_cue_linser ppar_cue_linser, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    ppar_cue_linser = DApar_cue_linser.Crearpar_cue_linser(ppar_cue_linser, pusuario);

                    ts.Complete();

                }

                return ppar_cue_linser;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("par_cue_linserBusiness", "Crearpar_cue_linser", ex);
                return null;
            }
        }


        public par_cue_linser Modificarpar_cue_linser(par_cue_linser ppar_cue_linser, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    ppar_cue_linser = DApar_cue_linser.Modificarpar_cue_linser(ppar_cue_linser, pusuario);

                    ts.Complete();

                }

                return ppar_cue_linser;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("par_cue_linserBusiness", "Modificarpar_cue_linser", ex);
                return null;
            }
        }


        public void Eliminarpar_cue_linser(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DApar_cue_linser.Eliminarpar_cue_linser(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("par_cue_linserBusiness", "Eliminarpar_cue_linser", ex);
            }
        }


        public par_cue_linser Consultarpar_cue_linser(Int64 pId, Usuario pusuario)
        {
            try
            {
                par_cue_linser par_cue_linser = new par_cue_linser();
                par_cue_linser = DApar_cue_linser.Consultarpar_cue_linser(pId, pusuario);
                return par_cue_linser;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("par_cue_linserBusiness", "Consultarpar_cue_linser", ex);
                return null;
            }
        }


        public List<par_cue_linser> Listarpar_cue_linser(par_cue_linser ppar_cue_linser, Usuario pusuario)
        {
            try
            {
                return DApar_cue_linser.Listarpar_cue_linser(ppar_cue_linser, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("par_cue_linserBusiness", "Listarpar_cue_linser", ex);
                return null;
            }
        }


    }
}