using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
using System.Transactions;

namespace Xpinn.Aportes.Business
{
    public class ParametrosAporteBusiness : GlobalData
    {

        private ParametrosAporteData DAActividad;


        public ParametrosAporteBusiness()
        {
            DAActividad = new ParametrosAporteData();
        }


        public ParametrosAporte CrearPar_Cue_LinApo(ParametrosAporte pParametro, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParametro = DAActividad.CrearPar_Cue_LinApo(pParametro, vUsuario,opcion);

                    ts.Complete();
                }

                return pParametro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAporteBusiness", "CrearPar_Cue_LinApo", ex);
                return null;
            }
        }


        public List<ParametrosAporte> ListarParametrosAporte(string filtro, string orden, Usuario vUsuario)
        {
            try
            {
                return DAActividad.ListarParametrosAporte(filtro, orden, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAporteBusiness", "ListarParametrosAporte", ex);
                return null;
            }
        }



        public void EliminarParametroAporte(string pId, int opcion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAActividad.EliminarParametroAporte(pId,opcion, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAporteBusiness", "EliminarParametroAporte", ex);
            }
        }


        public Int32 ObtenerSiguienteCodigo(int opcion, Usuario pUsuario)
        {
            try
            {
                return DAActividad.ObtenerSiguienteCodigo(opcion,pUsuario);
            }
            catch
            {
                return 1;
            }
        }


        public ParametrosAporte ConsultarParametrosAporte(string pId, int opcion, Usuario vUsuario)
        {
            try
            {
                return DAActividad.ConsultarParametrosAporte(pId, opcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAporteBusiness", "ConsultarParametrosAporte", ex);
                return null;
            }
        }


    }
}
