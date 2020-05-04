using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Business;
using Xpinn.Comun.Entities;
using Xpinn.Util;



namespace Xpinn.CDATS.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AperturaCDATService
    {
        AperturaCDATBusiness BOApertura ;
        ExcepcionBusiness BOException;

        public AperturaCDATService()
        {
            BOApertura = new AperturaCDATBusiness();
            BOException = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220301"; } }
        public string CodigoProgramaCierre { get { return "220316"; } }
        public string CodigoProgramaCertificacion { get { return "220314"; } }
        public string CodigoProgramaSimulaicon { get { return "220315"; } }
        public string codigoprogramarenovacioncdat { get { return "220313"; } }
        public string codigoprogramafusioncdats { get { return "220310"; } }
        public string codigoprogramaCierreHistorico { get { return "220309"; } }
        public string codigoprogramaGarantia { get { return "220312"; } }
        public string codigoprogramaFormatoImpresion { get { return "220313"; } }



        public Cdat CrearAperturaCDAT(Cdat pCdat, Usuario vUsuario, List<Beneficiario> lstBeneficiariosCdat)
        {
            try
            {
                return BOApertura.CrearAperturaCDAT(pCdat, vUsuario,  lstBeneficiariosCdat);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "CrearAperturaCDAT", ex);
                return null;
            }
        }


        public Cdat ModificarAperturaCDAT(Cdat pCdat, Usuario vUsuario, List<Beneficiario> lstBeneficiariosCdat)
        {
            try
            {
                return BOApertura.ModificarAperturaCDAT(pCdat, vUsuario, lstBeneficiariosCdat);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ModificarAperturaCDAT", ex);
                return null;
            }
        }


        public void EliminarAperturaCdat(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOApertura.EliminarAperturaCdat(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "EliminarAperturaCdat", ex);
            }
        }

        public AdministracionCDAT cierrecdat(AdministracionCDAT traslado_cuenta, Xpinn.Tesoreria.Entities.Operacion operacion, Usuario vUsuario)
        {
            try
            {
                BOApertura.cierrecdat(traslado_cuenta, operacion, vUsuario);
                return null;
            }
            catch (Exception ex)
            {
                BOException.Throw("cdatservice", "aplicartranslado", ex);
                return null;
            }
        }


        public List<Detalle_CDAT> Liquidar(AdministracionCDAT Liquidacion, Usuario vUsuario)
        {
            try
            {
                return BOApertura.Liquidar(Liquidacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "liquidar", ex);
                return null;
            }
        }


        public List<Cdat> Listarsimulacion(Cdat vapertura,DateTime FechaApe, Usuario vUsuario)
        {
            try
            {
                return BOApertura.Listarsimulacion(vapertura,FechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "Listardatos", ex);
                return null;
            }
        }

        public List<Cdat> Listardatos(string filtro, DateTime FechaApe, Usuario vUsuario)
        {
            try
            {
                return BOApertura.Listardatos(filtro, FechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "Listardatos", ex);
                return null;
            }
        }

        public List<Cdat> ListarCdat(string filtro, DateTime FechaApe, Usuario vUsuario,DateTime FechaVencimi)
        {
            try
            {
                return BOApertura.ListarCdat(filtro, FechaApe, vUsuario, FechaVencimi);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ListarCdat", ex);
                return null;
            }
        }
        public List<Cdat> ListarCdats(string filtro, DateTime FechaApe, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ListarCdats(filtro, FechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ListarCdats", ex);
                return null;
            }
        }


        //Detalle

        public void EliminarTitularCdat(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOApertura.EliminarTitularCdat(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "EliminarTitularCdat", ex);
            }
        }


        public List<Cdat> ListarUsuariosAsesores(Cdat pPerso, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ListarUsuariosAsesores(pPerso, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ListarUsuariosAsesores", ex);
                return null;
            }
        }


        public List<Cdat> ListarOficinas(Cdat pPerso, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ListarOficinas(pPerso, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ListarOficinas", ex);
                return null;
            }
        }
        public List<Cdat> ListarFechaVencimiento(Cdat pPerso, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ListarFechaVencimiento(pPerso, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ListarFechaVencimiento", ex);
                return null;
            }
        }
        public Cdat DiasAvisoCDAT(Cdat pPerso, Usuario vUsuario)
        {
            try
            {
                return BOApertura.DiasAvisoCDAT(pPerso, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "DiasAvisoCDAT", ex);
                return null;
            }
        }
        public Detalle_CDAT ConsultarPersona(Detalle_CDAT pPerso, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ConsultarPersona(pPerso, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ConsultarPersona", ex);
                return null;
            }
        }

        //CONSULTAR DATOS PRINCIPALES

        public Cdat ConsultarApertu(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ConsultarApertu(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ConsultarApertu", ex);
                return null;
            }
        }

        public Cdat ConsultarApertuXnumcdat(String pId, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ConsultarApertuXnumcdat(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ConsultarApertuXnumcdat", ex);
                return null;
            }
        }
        public Cdat ConsultarApertura( Usuario vUsuario)
        {
            try
            {
                return BOApertura.ConsultarApertura(vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ConsultarApertu", ex);
                return null;
            }
        }


        public Xpinn.FabricaCreditos.Entities.Credito ConsultarAperturas(Usuario vUsuario,string filtro)
        {
            try
            {
                return BOApertura.ConsultarAperturas(vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ConsultarApertu", ex);
                return null;
            }
        }
        
        public List<Detalle_CDAT> ListarDetalle(Usuario vUsuario)
        {
            try
            {
                return BOApertura.ListarDetalle(vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ListarDetalle", ex);
                return null;
            }
        }


        public Xpinn.FabricaCreditos.Entities.Credito CrearAperturaCDAT(Xpinn.FabricaCreditos.Entities.Credito pAperturaCDAT, Usuario vUsuario, Xpinn.Tesoreria.Entities.Operacion poperacion)
        {
            try
            {
                return BOApertura.CrearAperturaCDAT(pAperturaCDAT, vUsuario,poperacion);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATBusiness", "CrearAperturaCDAT", ex);
                return null;
            }
        }

        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarDetalles(Usuario vUsuario,string filtro)
        {
            try
            {
                return BOApertura.ListarDetalles(vUsuario,filtro);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ListarDetalle", ex);
                return null;
            }
        }
        //CONSULTAR DETALLE TITULAR
        public List<Detalle_CDAT> ListarDetalleTitulares(Int64 pCod, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ListarDetalleTitulares(pCod, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ListarDetalleTitulares", ex);
                return null;
            }
        }


        public Cdat ConsultarDiasPeriodicidad(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ConsultarDiasPeriodicidad(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ConsultarDiasPeriodicidad", ex);
                return null;
            }
        }

        public Cdat ConsultarNumeracionCDATS(Cdat pCadt, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ConsultarNumeracionCDATS(pCadt, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ConsultarNumeracionCDATS", ex);
                return null;
            }
        }



        //GRABAR AUDITORIA
        public CDAT_AUDITORIA CrearAuditoriaCdat(CDAT_AUDITORIA pCdat, Usuario vUsuario)
        {
            try
            {
                return BOApertura.CrearAuditoriaCdat(pCdat, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "CrearAuditoriaCdat", ex);
                return null;
            }
        }


        public CDAT_AUDITORIA ModificarAuditoriaCdat(CDAT_AUDITORIA pCdat, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ModificarAuditoriaCdat(pCdat, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ModificarAuditoriaCdat", ex);
                return null;
            }
        }

        public List<Cdat> ListarTipoLineaCDAT(Cdat pPerso, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ListarTipoLineaCDAT(pPerso, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ListarTipoLineaCDAT", ex);
                return null;
            }
        }

        public AdministracionCDAT RenovacionCdat(Cdat pCdatNuevo, AdministracionCDAT pCdatRenovar, Xpinn.Tesoreria.Entities.Operacion pOperacion, Usuario vUsuario)
        {
            try
            {
                return BOApertura.RenovacionCdat(pCdatNuevo, pCdatRenovar, pOperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ListarTipoLineaCDAT", ex);
                return null;
            }
        }


        public Cdat ciCierreHistorico(Cdat pentidad,string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            //  BOApertura.ciCierreHistorico(estado, fecha, cod_usuario, ref serror, pUsuario);

            try
            {
                return BOApertura.ciCierreHistorico(pentidad,estado, fecha, cod_usuario, ref serror, pUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ciCierreHistorico", ex);
                return null;
            }
        }



        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            return BOApertura.ListarFechaCierre(pUsuario);
        }


        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pusuario, string sfiltro)
        {
            try
            {
                return BOApertura.ListarCredito(pusuario, sfiltro);
            }
            catch
            { return null; }
        }
        public Xpinn.FabricaCreditos.Entities.Credito ListarCreditos(Usuario pusuario, string sfiltro)
        {
            try
            {
                return BOApertura.ListarCreditos(pusuario, sfiltro);
            }
            catch
            { return null; }
        }


        public Int32 ObtenerConsecutivo(string pConsulta, Usuario vUsuario)
        {
            try
            {
                return BOApertura.ObtenerConsecutivo(pConsulta, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ObtenerConsecutivo", ex);
                return 0;
            }
        }

        public Cdat ConsultarAfiliacion(String pId, Usuario pUsuario)
        {
            try
            {
                return BOApertura.ConsultarAfiliacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ConsultarAfiliacion", ex);
                return null;
            }
        }

        public DateTime Calcularfecha(DateTime fecha_vencimiento, DateTime fecha, Int32 plazo, Int32 tipocalendario)
        {
            try
            {
                return BOApertura.Calcularfecha(fecha_vencimiento, fecha,plazo,tipocalendario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATBusiness", "Calcularfecha", ex);
                return fecha_vencimiento;
            }
        }


        public void Crearcierea(Xpinn.Comun.Entities.Cierea pcierea, Usuario vUsuario)
        {
            try
            {
                BOApertura.Crearciereap(pcierea, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATBusiness", "Crearcierea", ex);
            }
        }

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierreCausacion(Usuario pUsuario)
        {
            return BOApertura.ListarFechaCierreCaus(pUsuario);
        }


        public List<Cdat> ListartodosUsuarios(Cdat pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                return BOApertura.ListartodosUsuarios(pUsuarioAse, pUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "ListarUsuario", ex);
                return null;
            }
        }

        public string CrearSolicitudCDAT(Cdat pCDAT, Usuario pUsuario, List<FabricaCreditos.Entities.DocumentosAnexos> lstDocumentos = null)
        {
            try
            {
                return BOApertura.CrearSolicitudCDAT(pCDAT, pUsuario, lstDocumentos);
            }
            catch (Exception ex)
            {
                BOException.Throw("AperturaCDATService", "CrearSolicitudCDAT", ex);
                return null;
            }
        }
    }
}
