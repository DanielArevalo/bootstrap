using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    /// <summary>
    /// Objeto de negocio para Atributo
    /// </summary>
    public class DiasNoHabilesBusiness : GlobalBusiness
    {
        private DiasNoHabilesData DADias;

        /// <summary>
        /// Constructor del objeto de negocio para Atributo
        /// </summary>
        public DiasNoHabilesBusiness()
        {
            DADias = new DiasNoHabilesData();
        }

       
        public Dias_no_habiles CrearDiasNoHabiles(Dias_no_habiles pDias, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pDias.lstDias != null && pDias.lstDias.Count > 0)
                    {
                        Dias_no_habiles nValidacion = new Dias_no_habiles();
                        //nValidacion = DADias.ConsultarDiasNoHabiles(pDias, vUsuario);
                        nValidacion.mes = pDias.lstDias[0].mes;
                        nValidacion.ano = pDias.lstDias[0].ano;
                        DADias.EliminarDiasNoHabiles(nValidacion, vUsuario);
                        foreach (Dias_no_habiles pDiasNoHab in pDias.lstDias)
                        {                           
                            if (nValidacion.consecutivo == 0)
                            {
                                Dias_no_habiles nDiasNoHabiles = new Dias_no_habiles();
                                nDiasNoHabiles.dia_festivo = pDiasNoHab.dia_festivo;
                                nDiasNoHabiles.ano = pDiasNoHab.ano;
                                nDiasNoHabiles.mes = pDiasNoHab.mes;
                                nDiasNoHabiles = DADias.ConsultarDiasNoHabiles(nDiasNoHabiles, vUsuario);
                                if (nDiasNoHabiles.consecutivo == 0)
                                {
                                    nDiasNoHabiles = new Dias_no_habiles();
                                    pDiasNoHab.consecutivo = DADias.ObtenerSiguienteCodigo(vUsuario);
                                    nDiasNoHabiles = DADias.CrearDiasNoHabiles(pDiasNoHab, vUsuario);
                                }
                            }
                        }
                    }
                    ts.Complete();
                }

                return pDias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiasNoHabilesBusiness", "CrearDiasNoHabiles", ex);
                return null;
            }
        }



        public List<Dias_no_habiles> ListarDiasNoHabiles(Dias_no_habiles pDias, Usuario vUsuario)
        {
            try
            {
                return DADias.ListarDiasNoHabiles(pDias, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DiasNoHabilesBusiness", "ListarDiasNoHabiles", ex);
                return null;
            }
        }

    }
}