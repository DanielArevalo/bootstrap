using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para el libro mayor
    /// </summary>
    public class LibroMayorBusiness : GlobalBusiness
    {
        private LibroMayorData DALibroMayor;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public LibroMayorBusiness()
        {
            DALibroMayor = new LibroMayorData();
        }

        public List<LibroMayor> ListarLibroMayor(LibroMayor pEntidad, ref Double TotDeb, ref Double TotCre, Usuario vUsuario, bool isNiif)
        {
            return DALibroMayor.ListarLibroMayor(pEntidad, ref TotDeb, ref TotCre, vUsuario, isNiif);
        }

        public List<LibroMayor> ListarFechaCierre(Usuario vUsuario, bool isNiif = false)
        {
            // Listas fechas de períodos ya cerrados
            List<LibroMayor> lstLibroMayor = new List<LibroMayor>();
            lstLibroMayor = isNiif == false ? DALibroMayor.ListarFechaCierre(vUsuario) : DALibroMayor.ListarFechaCierre(vUsuario, isNiif==false?"C":"G");
            // Insertar fechas de períodos pendientes
            List<Cierremensual> lstCierreMen = new List<Cierremensual>();
            CierreMensualBusiness cierreMensual = new CierreMensualBusiness();
            lstCierreMen = isNiif == false ? cierreMensual.ListarFechaCierre(vUsuario) : cierreMensual.ListarFechaCierre(vUsuario, isNiif == false ? "C" : "G");
            foreach (Cierremensual cieMen in lstCierreMen)
            {
                LibroMayor libMay = new LibroMayor();
                libMay.fecha = cieMen.fecha;
                lstLibroMayor.Insert(0, libMay);
            }
            return lstLibroMayor;
        }

        /// <summary>
        /// Método para determinar datos de la empresa
        /// </summary>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public void DatosEmpresa(ref string empresa, ref string nit, Usuario pUsuario)
        {
            DALibroMayor.DatosEmpresa(ref empresa, ref nit, pUsuario);
        }


    }
}

