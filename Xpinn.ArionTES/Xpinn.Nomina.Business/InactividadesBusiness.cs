using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class InactividadesBusiness : GlobalBusiness
    {

        private InactividadesData DAInactividades;

        public InactividadesBusiness()
        {
            DAInactividades = new InactividadesData();
        }

        public Inactividades CrearInactividades(Inactividades pInactividades, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pInactividades = DAInactividades.CrearInactividades(pInactividades, pusuario);

                    ts.Complete();

                }

                return pInactividades;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InactividadesBusiness", "CrearInactividades", ex);
                return null;
            }
        }


        public Inactividades ModificarInactividades(Inactividades pInactividades, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pInactividades = DAInactividades.ModificarInactividades(pInactividades, pusuario);

                    ts.Complete();

                }

                return pInactividades;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InactividadesBusiness", "ModificarInactividades", ex);
                return null;
            }
        }


        public void EliminarInactividades(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAInactividades.EliminarInactividades(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InactividadesBusiness", "EliminarInactividades", ex);
            }
        }


        public Inactividades ConsultarInactividades(Int64 pId, Usuario pusuario)
        {
            try
            {
                Inactividades Inactividades = new Inactividades();
                Inactividades = DAInactividades.ConsultarInactividades(pId, pusuario);
                return Inactividades;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InactividadesBusiness", "ConsultarInactividades", ex);
                return null;
            }
        }


        public List<Inactividades> ListarInactividades(string filtro, Usuario pusuario)
        {
            try
            {
                List<Inactividades> listaInactividades = DAInactividades.ListarInactividades(filtro, pusuario);

                DateTimeHelper helper = new DateTimeHelper();
                foreach (Inactividades inactividad in listaInactividades)
                {
                    if (inactividad.fechainicio.HasValue && inactividad.fechaterminacion.HasValue)
                    {
                        inactividad.dias = Convert.ToInt32(helper.DiferenciaEntreDosFechasDias(inactividad.fechaterminacion.Value, inactividad.fechainicio.Value)+1);
                    }
                }

                return listaInactividades;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InactividadesBusiness", "ListarInactividades", ex);
                return null;
            }
        }


    }
}