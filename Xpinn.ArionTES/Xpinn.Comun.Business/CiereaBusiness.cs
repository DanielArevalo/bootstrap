using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;
using System.Web;

namespace Xpinn.Comun.Business
{
    public class CiereaBusiness : GlobalData
    {
        
        private CiereaData DACierea;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public CiereaBusiness()
        {
            DACierea = new CiereaData();
        }

        /// <summary>
        /// Listar cierres realizados
        /// </summary>
        /// <param name="pTipo"></param>
        /// <returns></returns>
        public List<Cierea> ListarCierea(Cierea pTipo, Usuario pUsuario)
        {
            return DACierea.ListarCierea(pTipo, pUsuario);
        }

        /// <summary>
        /// Verificar si el cierre a la fecha dada existe
        /// </summary>
        /// <param name="pfecha"></param>
        /// <param name="ptipo"></param>
        public Boolean ExisteCierre(DateTime pfecha, string ptipo, Usuario pUsuario)
        {
            return DACierea.ExisteCierre(pfecha, ptipo, pUsuario);
        }

        public DateTime FechaUltimoCierre(string ptipo, Usuario pUsuario)
        {
            return DACierea.FechaUltimoCierre(ptipo, pUsuario);
        }

        public List<Cierea> ConsultaGeneralCierea(String pFiltro, Usuario pUsuario)
        {
            return DACierea.ConsultaGeneralCierea(pFiltro, pUsuario);
        }


        /// <summary>
        /// Listar fechas de cierres 
        /// </summary>
        /// <param name="pTipo"></param>
        /// <param name="pEstado"></param>
        /// <returns></returns>
        public List<Cierea> ListarCiereaFecha(String pTipo, String pEstado, Usuario pUsuario)
        {
            return DACierea.ListarCiereaFecha(pTipo, pEstado, pUsuario);
        }

        //Agregado para seguimiento de cierres
        public Cierea ConsultarSigCierre(Usuario pUsuario)
        {
            return DACierea.ConsultarSigCierre(pUsuario);
        }

        public List<Cierea> ListarControlCierres(string filtro, Usuario pUsuario)
        {
            return DACierea.ListarControlCierres(filtro, pUsuario);
        }

        public List<string> ListarPeriodosCierres(Usuario pUsuario)
        {
            return DACierea.ListarPeriodosCierres(pUsuario);
        }

        public List<Cierea> ListarCiereaFechaComp(String pTipo, String pEstado, Usuario pUsuario)
        {
            return DACierea.ListarCiereaFechaComp(pTipo, pEstado, pUsuario);
        }


    }
}
