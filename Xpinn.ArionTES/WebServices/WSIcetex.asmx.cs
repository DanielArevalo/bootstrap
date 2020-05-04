using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Icetex.Entities;
using Xpinn.Icetex.Services;

namespace WebServices
{
    /// <summary>
    /// Descripción breve de WSIcetex
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSIcetex : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Xpinn.Icetex.Entities.ConvocatoriaRequerido> ValidacionRequisitos(Int64 pCod_Persona, DateTime pFecha, string sec)
        {
            Xpinn.Icetex.Services.ConvocatoriaServices BOIcetex = new Xpinn.Icetex.Services.ConvocatoriaServices();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario = conexion.DeterminarUsuarioOficina(sec);
            if (pUsuario == null)
                return null;

            List<Xpinn.Icetex.Entities.ConvocatoriaRequerido> lstRequisitos = new List<Xpinn.Icetex.Entities.ConvocatoriaRequerido>();
            lstRequisitos = BOIcetex.ValidacionRequisitos(pCod_Persona, pFecha, pUsuario);

            return lstRequisitos;
        }

        [WebMethod]
        public ConvocatoriaRequerido CrearConvocatoriaRequerido(ConvocatoriaRequerido pRequerido, string sec)
        {
            Xpinn.Icetex.Services.ConvocatoriaServices BOIcetex = new Xpinn.Icetex.Services.ConvocatoriaServices();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario = conexion.DeterminarUsuarioOficina(sec);
            if (pUsuario == null)
                return null;

            ConvocatoriaRequerido pEntidad = new ConvocatoriaRequerido();
            pEntidad = BOIcetex.CrearConvocatoriaRequerido(pRequerido, pUsuario);
            return pEntidad;
        }

        [WebMethod]
        public CreditoIcetex CrearCreditoIcetex(CreditoIcetex pCreditoIcetex, List<CreditoIcetexDocumento> lstDocumentos, int pOpcion, string sec)
        {
            Xpinn.Icetex.Services.ConvocatoriaServices BOIcetex = new Xpinn.Icetex.Services.ConvocatoriaServices();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario = conexion.DeterminarUsuarioOficina(sec);
            if (pUsuario == null)
                return null;

            CreditoIcetex pEntidad = new CreditoIcetex();
            pEntidad = BOIcetex.CrearCreditoIcetex(pCreditoIcetex, lstDocumentos, pOpcion,  pUsuario);             
            return pEntidad;
        }


        [WebMethod]
        public Boolean CrearCreditoIcetexDocumento(List<CreditoIcetexDocumento> lstDocumentos, string sec)
        {
            Xpinn.Icetex.Services.ConvocatoriaServices BOIcetex = new Xpinn.Icetex.Services.ConvocatoriaServices();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario = conexion.DeterminarUsuarioOficina(sec);
            if (pUsuario == null)
                return false;

            Boolean rpta = false;
            rpta = BOIcetex.CrearCreditoIcetexDocumento(lstDocumentos, pUsuario);
            return rpta;
        }


        [WebMethod]
        public List<IcetexDocumentos> ListarConvocatoriaDocumentos(string pFiltro, string sec)
        {
            Xpinn.Icetex.Services.ConvocatoriaServices BOIcetex = new Xpinn.Icetex.Services.ConvocatoriaServices();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario = conexion.DeterminarUsuarioOficina(sec);
            if (pUsuario == null)
                return null;

            List<IcetexDocumentos> lstDatos = new List<IcetexDocumentos>();
            lstDatos = BOIcetex.ListarConvocatoriaDocumentos(pFiltro, pUsuario);
            return lstDatos;
        }

        [WebMethod]
        public List<CreditoIcetex> ListarCreditosIcetex(string pFiltro, string sec)
        {
            Xpinn.Icetex.Services.ConvocatoriaServices BOIcetex = new Xpinn.Icetex.Services.ConvocatoriaServices();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario = conexion.DeterminarUsuarioOficina(sec);
            if (pUsuario == null)
                return null;


            List<CreditoIcetex> lstDatos = new List<CreditoIcetex>();
            lstDatos = BOIcetex.ListarCreditosIcetex(pFiltro, pUsuario);
            return lstDatos;
        }

        [WebMethod]
        public ConvocatoriaIcetex ConsultarConvocatoriaIcetex(Int64 pId, string sec)
        {
            Xpinn.Icetex.Services.ConvocatoriaServices BOIcetex = new Xpinn.Icetex.Services.ConvocatoriaServices();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario = conexion.DeterminarUsuarioOficina(sec);
            if (pUsuario == null)
                return null;

            ConvocatoriaIcetex pResult = new ConvocatoriaIcetex();
            pResult = BOIcetex.ConsultarConvocatoriaIcetex(pId, pUsuario);
            return pResult;
        }

        [WebMethod]
        public CreditoIcetex ConsultarCreditoIcetex(string pFiltro, string sec)
        {
            Xpinn.Icetex.Services.ConvocatoriaServices BOIcetex = new Xpinn.Icetex.Services.ConvocatoriaServices();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario = conexion.DeterminarUsuarioOficina(sec);
            if (pUsuario == null)
                return null;

            return BOIcetex.ConsultarCreditoIcetex(pFiltro, pUsuario);            
        }

        [WebMethod]
        public List<ListadosIcetex> ListarDocumentosIcetex(string pFiltro, string sec)
        {
            AprobacionServices BOIcetex = new AprobacionServices();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario pUsuario = new Xpinn.Util.Usuario();
            pUsuario = conexion.DeterminarUsuarioOficina(sec);
            if (pUsuario == null)
                return null;

            string pError = string.Empty;
            return BOIcetex.ListarDocumentosIcetex(pFiltro, ref pError, pUsuario);
        }

    }
}
