using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class Nomina_EntidadBusiness : GlobalBusiness
    {

        private Nomina_EntidadData DANomina_Entidad;

        public Nomina_EntidadBusiness()
        {
            DANomina_Entidad = new Nomina_EntidadData();
        }

        public Nomina_Entidad CrearNomina_Entidad(Nomina_Entidad pNomina_Entidad, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pNomina_Entidad = DANomina_Entidad.CrearNomina_Entidad(pNomina_Entidad, pusuario);

                    ts.Complete();

                }

                return pNomina_Entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Nomina_EntidadBusiness", "CrearNomina_Entidad", ex);
                return null;
            }
        }


        public Nomina_Entidad ModificarNomina_Entidad(Nomina_Entidad pNomina_Entidad, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pNomina_Entidad = DANomina_Entidad.ModificarNomina_Entidad(pNomina_Entidad, pusuario);

                    ts.Complete();

                }

                return pNomina_Entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Nomina_EntidadBusiness", "ModificarNomina_Entidad", ex);
                return null;
            }
        }


        public void EliminarNomina_Entidad(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DANomina_Entidad.EliminarNomina_Entidad(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Nomina_EntidadBusiness", "EliminarNomina_Entidad", ex);
            }
        }


        public Nomina_Entidad ConsultarNomina_Entidad(Int64 pId, Usuario pusuario)
        {
            try
            {
                Nomina_Entidad Nomina_Entidad = new Nomina_Entidad();
                Nomina_Entidad = DANomina_Entidad.ConsultarNomina_Entidad(pId, pusuario);
                return Nomina_Entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Nomina_EntidadBusiness", "ConsultarNomina_Entidad", ex);
                return null;
            }
        }

        public Nomina_Entidad ConsultarDatos(string pId, Usuario pusuario)
        {
            try
            {
                Nomina_Entidad Nomina_Entidad = new Nomina_Entidad();
                Nomina_Entidad = DANomina_Entidad.ConsultaDatosEntidad(pId, pusuario);
                return Nomina_Entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Nomina_EntidadBusiness", "ConsultarNomina_Entidad", ex);
                return null;
            }
        }

        public List<Nomina_Entidad> ListarNomina_Entidad(string filtro, Usuario pusuario)
        {
            try
            {
                return DANomina_Entidad.ListarNomina_Entidad(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Nomina_EntidadBusiness", "ListarNomina_Entidad", ex);
                return null;
            }
        }


    }
}


