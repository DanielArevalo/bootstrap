using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Confecoop.Entities;
using Xpinn.Util;
using Xpinn.Comun.Business;

namespace Xpinn.Confecoop.Business
{
    public class ConfecoopBusiness : GlobalData
    {
         private Xpinn.Confecoop.Data.ConfecoopData DAPuc;

         public ConfecoopBusiness()
         {
             DAPuc = new Xpinn.Confecoop.Data.ConfecoopData();
         }


         public List<PUC> ListarFechaCierreGLOBAL(string tipo, string estado, Usuario pUsuario)
         {
             try
             {
                 return DAPuc.ListarFechaCierreGLOBAL(tipo,estado, pUsuario);
             }
             catch (Exception ex)
             {
                 BOExcepcion.Throw("ConfecoopBusiness", "ListarFechaCierreGLOBAL", ex);
                 return null;
             }
         }



         public List<PUC> ListarTEMP_SUPERSOLIDARIA(PUC pPuc, ref string pError, Usuario vUsuario, bool estado, Int32 pTipoNorma)
         {
             try
             {
                 return DAPuc.ListarTEMP_SUPERSOLIDARIA(pPuc, ref pError, vUsuario, estado, pTipoNorma);
             }
             catch (Exception ex)
             {
                 pError = ex.Message;
                 //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERSOLIDARIA", ex);
                 return null;
             }
         }



         public List<PUC> ListarTEMP_SUPERSOLI_APORTES(PUC pPuc, ref string pError, Usuario vUsuario)
         {
             try
             {
                 return DAPuc.ListarTEMP_SUPERSOLI_APORTES(pPuc, ref pError, vUsuario);
             }
             catch (Exception ex)
             {
                 pError = ex.Message;
                 //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERSOLIDARIA", ex);
                 return null;
             }
         }

         public List<PUC> ListarTEMP_SUPERSOLI_CARTERA(PUC pPuc, ref string pError, Usuario vUsuario)
         {
             try
             {
                 return DAPuc.ListarTEMP_SUPERSOLI_CARTERA(pPuc, ref pError, vUsuario);
             }
             catch (Exception ex)
             {
                 pError = ex.Message;
                 //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERSOLIDARIA", ex);
                 return null;
             }
         }

         public List<PUC> ListarTEMP_SUPERSOLI_ACTIVOSFIJOS(PUC pPuc, ref string pError, Usuario vUsuario)
         {
             try
             {
                 return DAPuc.ListarTEMP_SUPERSOLI_ACTIVOSFIJOS(pPuc, ref pError, vUsuario);
             }
             catch (Exception ex)
             {
                 pError = ex.Message;
                 //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERSOLIDARIA", ex);
                 return null;
             }
         }


         public List<PUC> ListarTEMP_SUPER_ASOCIADOS(PUC pPuc, ref string pError, Usuario vUsuario)
         {
             try
             {
                 return DAPuc.ListarTEMP_SUPER_ASOCIADOS(pPuc, ref pError, vUsuario);
             }
             catch (Exception ex)
             {
                 pError = ex.Message;
                 //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPER_ASOCIADOS", ex);
                 return null;
             }
         }

        public List<PUC> ListarTEMP_POSICIONNETADIRECTIVOS(PUC pPuc, ref string pError, Usuario usuario)
        {
            try
            {
                return DAPuc.ListarTEMP_POSICIONNETADIRECTIVOS(pPuc, ref pError, usuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_POSICIONNETANEGATIVA(PUC pPuc, ref string pError, Usuario usuario)
        {
            try
            {
                return DAPuc.ListarTEMP_POSICIONNETANEGATIVA(pPuc, ref pError, usuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_POSICIONNETAPOSITIVA(PUC pPuc, ref string pError, Usuario usuario)
        {
            try
            {
                return DAPuc.ListarTEMP_POSICIONNETAPOSITIVA(pPuc, ref pError, usuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERCAPTACIONESS(PUC pPuc, ref string pError, Usuario vUsuario)
         {
             try
             {
                 return DAPuc.ListarTEMP_SUPERCAPTACIONESS(pPuc, ref pError, vUsuario);
             }
             catch (Exception ex)
             {
                 pError = ex.Message;
                 //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERCAPTACIONESS", ex);
                 return null;
             }
         }

        public List<PUC> ListarTEMP_SUPER_DIRECTIVOS(PUC pPuc, ref string pError, Usuario usuario)
        {
            try
            {
                return DAPuc.ListarTEMP_SUPER_DIRECTIVOS(pPuc, ref pError, usuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERiesgoLiquidez(PUC pPuc, ref string pError, Usuario vUsuario)
         {
             try
             {
                 return DAPuc.ListarTEMP_SUPERiesgoLiquidez(pPuc, ref pError, vUsuario);
             }
             catch (Exception ex)
             {
                 pError = ex.Message;
                 //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERCAPTACIONESS", ex);
                 return null;
             }
         }


         public List<PUC> ListarTEMP_SUPERObligacionFinanciera(PUC pPuc, ref string pError, Usuario vUsuario)
         {
             try
             {
                 return DAPuc.ListarTEMP_SUPERObligacionFinanciera(pPuc, ref pError, vUsuario);
             }
             catch (Exception ex)
             {
                 pError = ex.Message;
                 //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERCAPTACIONESS", ex);
                 return null;
             }
         }

         public List<PUC> ListarTEMP_SUPERedOficinas(PUC pPuc, ref string pError, Usuario vUsuario)
         {
             try
             {
                 return DAPuc.ListarTEMP_SUPERedOficinas(pPuc, ref pError, vUsuario);
             }
             catch (Exception ex)
             {
                 pError = ex.Message;
                 //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERCAPTACIONESS", ex);
                 return null;
             }
         }

         public List<PUC> ListarTEMP_SUPERSOLI_ESTADISTICAS(PUC pPuc, ref string pError, Usuario vUsuario)
         {
             try
             {
                 return DAPuc.ListarTEMP_SUPERSOLI_ESTADISTICAS(pPuc, ref pError, vUsuario);
             }
             catch (Exception ex)
             {
                 pError = ex.Message;
                 return null;
             }
         }


         public List<PUC> ListarFechaCierre(Usuario pUsuario)
         {
             FechasBusiness BOFechas = new FechasBusiness();
             List<PUC> LstCierre = new List<PUC>();
             DateTime FecIni = DAPuc.FechaUltimoCierre(pUsuario);
             if (FecIni == DateTime.MinValue)
                 return null;
             int dias_cierre = 0;
             int tipo_calendario = 0;
             DAPuc.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
             DateTime FecCieIni = DateTime.MinValue;
             DateTime FecCie = DateTime.MinValue;
             if (dias_cierre == 30)
             {
                 FecCieIni = BOFechas.FecUltDia(FecIni).AddDays(1);
                 FecIni = FecCieIni;
                 FecCieIni = BOFechas.SumarMeses(FecCieIni, 1);
                 FecCie = FecCieIni.AddDays(-1);
             }
             else
             {
                 FecCieIni = BOFechas.FecSumDia(FecIni, dias_cierre, tipo_calendario);
             }
             while (FecCieIni <= DateTime.Now.AddDays(28))
             {
                 if (dias_cierre == 30)
                 {
                     PUC CieMen = new PUC();
                     FecCieIni = FecCieIni.AddDays(-1);
                     CieMen.fecha = FecCieIni;
                     LstCierre.Add(CieMen);
                     FecCieIni = BOFechas.SumarMeses(FecCieIni.AddDays(1), 1);
                 }
                 else
                 {
                     PUC CieMen = new PUC();
                     CieMen.fecha = FecCieIni;
                     LstCierre.Add(CieMen);
                     FecCieIni = BOFechas.FecSumDia(FecCieIni, dias_cierre, tipo_calendario);
                 }
             }
             return LstCierre;
         }


         public List<PUC> ListarTEMP_SUPERSOLI_OPERACIONES(PUC pPuc, ref string pError, Usuario vUsuario)
         {
             try
             {
                 return DAPuc.ListarTEMP_SUPERSOLI_OPERACIONES(pPuc, ref pError, vUsuario);
             }
             catch (Exception ex)
             {
                 pError = ex.Message;
                 return null;
             }
         }


        public List<PUC> ListarTEMP_SUPER_FDO_LIQUIDEZ(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAPuc.ListarTEMP_SUPER_FDO_LIQUIDEZ(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERCotitulares(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAPuc.ListarTEMP_SUPERCotitulares(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_CUENTAXPAGAR(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAPuc.ListarTEMP_SUPERSOLI_CUENTAXPAGAR(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_CONVENIO(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAPuc.ListarTEMP_SUPERSOLI_CONVENIO(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_PATRIMONIO(PUC pPuc, ref string pError, Usuario vUsuario, Int32 pTipoNorma)
        {
            try
            {
                return DAPuc.ListarTEMP_SUPERSOLI_PATRIMONIO(pPuc, ref pError, vUsuario, pTipoNorma);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }
        public List<PUC> ListarTEMP_SUPERSOLI_TERCEROS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAPuc.ListarTEMP_SUPERSOLI_TERCEROS(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_PATRONALES(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAPuc.ListarTEMP_SUPERSOLI_PATRONALES(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLIDARIA_SALDOS(PUC pPuc, ref string pError, Usuario vUsuario, bool estado, Int32 pTipoNorma)
        {
            try
            {
                return DAPuc.ListarTEMP_SUPERSOLIDARIA_SALDOS(pPuc, ref pError, vUsuario, estado, pTipoNorma);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return null;
            }
        }

        public List<PUC> ListarTEMP_SUPERSOLI_CASTIGOS(PUC pPuc, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAPuc.ListarTEMP_SUPERSOLI_CASTIGOS(pPuc, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ConfecoopBusiness", "ListarTEMP_SUPERSOLIDARIA", ex);
                return null;
            }
        }


    }
}
