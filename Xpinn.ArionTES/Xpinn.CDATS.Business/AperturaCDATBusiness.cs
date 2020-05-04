using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;
using System.Transactions;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;
using Xpinn.Imagenes.Data;



namespace Xpinn.CDATS.Business
{
    public class AperturaCDATBusiness : GlobalBusiness
    {
        AperturaCDATData BAApertura;

        public AperturaCDATBusiness()
        {
            BAApertura = new AperturaCDATData();
        }

        public Xpinn.CDATS.Entities.Cdat CrearAperturaCDAT(Xpinn.CDATS.Entities.Cdat pCdat, Usuario vUsuario, List<Beneficiario> lstBeneficiariosCdat)
        {
            try 
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCdat = BAApertura.CrearAperturaCDAT(pCdat, vUsuario);
                    Int64 cod = pCdat.codigo_cdat;


                    if (pCdat.lstDetalle != null && pCdat.lstDetalle.Count > 0)
                    {
                        foreach (Detalle_CDAT pDeta in pCdat.lstDetalle)
                        {
                            Detalle_CDAT nDetalle = new Detalle_CDAT();
                            pDeta.codigo_cdat = cod;
                            nDetalle = BAApertura.CrearTitularCdat(pDeta, vUsuario, 1);
                        }
                    }

                    BeneficiarioData DABenef = new BeneficiarioData();

                    if (lstBeneficiariosCdat != null)
                        foreach (Beneficiario eBenef in lstBeneficiariosCdat)
                        {
                           eBenef.numero_cdat = pCdat.numero_cdat;
                           Beneficiario nBeneficiario = new Beneficiario();
                            nBeneficiario = DABenef.CrearBeneficiarioCdat(eBenef, vUsuario);
                        }

                    
                    ts.Complete();

                }
                return pCdat;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "CrearAperturaCDAT", ex);
                return null;
            }
        }

        public Xpinn.CDATS.Entities.Cdat ModificarAperturaCDAT(Xpinn.CDATS.Entities.Cdat pCdat, Usuario vUsuario, List<Beneficiario> lstBeneficiariosCdat)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCdat = BAApertura.ModificarAperturaCDAT(pCdat, vUsuario);
                    Int64 cod = pCdat.codigo_cdat;

                    if (pCdat.lstDetalle != null && pCdat.lstDetalle.Count > 0)
                    {
                        foreach (Detalle_CDAT pDeta in pCdat.lstDetalle)
                        {
                            Detalle_CDAT nDetalle = new Detalle_CDAT();
                            pDeta.codigo_cdat = cod;
                            if (pDeta.cod_usuario_cdat > 0 && pDeta.cod_usuario_cdat != null)
                                nDetalle = BAApertura.CrearTitularCdat(pDeta, vUsuario, 2);//Crear
                            else
                                nDetalle = BAApertura.CrearTitularCdat(pDeta, vUsuario, 1);//Modificar
                        }
                    }

                   
                    ts.Complete();
                }


                BeneficiarioData DABenef = new BeneficiarioData();
                if (lstBeneficiariosCdat != null && lstBeneficiariosCdat.Count > 0)
                {

                    Int64[] num = new Int64[lstBeneficiariosCdat.Count];
                    int secuencia = 0;
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        foreach (Beneficiario eBenef in lstBeneficiariosCdat)
                        {
                            eBenef.numero_cdat = pCdat.numero_cdat;
                            Beneficiario nBeneficiario = new Beneficiario();
                            if (eBenef.idbeneficiario <= 0)
                                nBeneficiario = DABenef.CrearBeneficiarioCdat(eBenef, vUsuario);
                            else
                                nBeneficiario = DABenef.ModificarBeneficiarioCdat(eBenef, vUsuario);

                            num[secuencia] = nBeneficiario.idbeneficiario;
                            secuencia += 1;
                        }
                        ts.Complete();
                    }
                }




                    return pCdat;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ModificarAperturaCDAT", ex);
                return null;
            }
        }



        public void EliminarAperturaCdat(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BAApertura.EliminarAperturaCdat(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "EliminarAperturaCdat", ex);
            }
        }


        public List<Xpinn.CDATS.Entities.Cdat> ListarCdat(string filtro, DateTime FechaApe, Usuario vUsuario, DateTime FechaVencimi)
        {
            try
            {
                return BAApertura.ListarCdat(filtro, FechaApe, vUsuario, FechaVencimi);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListarCdat", ex);
                return null;
            }
        }
        public List<Xpinn.CDATS.Entities.Cdat> ListarCdats(string filtro, DateTime FechaApe, Usuario vUsuario)
        {
            try
            {
                return BAApertura.ListarCdats(filtro, FechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListarCdat", ex);
                return null;
            }
        }
        public List<Xpinn.CDATS.Entities.Cdat> Listardatos(string filtro, DateTime FechaApe, Usuario vUsuario)
        {
            try
            {
                return BAApertura.Listardatos(filtro, FechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "Listardatos", ex);
                return null;
            }
        }

        public List<Xpinn.CDATS.Entities.Cdat> Listarsimulacion(Xpinn.CDATS.Entities.Cdat vapertura ,DateTime FechaApe, Usuario vUsuario)
        {
            try
            {
                return BAApertura.Listarsimulacion(vapertura, FechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "Listardatos", ex);
                return null;
            }
        }

        public List<Detalle_CDAT> Liquidar(AdministracionCDAT Liquidacion, Usuario vUsuario)
        {
            try
            {
                return BAApertura.Liquidar(Liquidacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "Liquidacion", ex);
                return null;
            }
        }
        


        //Detalle

        public void EliminarTitularCdat(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BAApertura.EliminarTitularCdat(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "EliminarTitularCdat", ex);
            }
        }



        public List<Xpinn.CDATS.Entities.Cdat> ListarUsuariosAsesores(Xpinn.CDATS.Entities.Cdat pPerso, Usuario vUsuario)
        {
            try
            {
                return BAApertura.ListarUsuariosAsesores(pPerso, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListarUsuariosAsesores", ex);
                return null;
            }
        }


        public List<Xpinn.CDATS.Entities.Cdat> ListarOficinas(Xpinn.CDATS.Entities.Cdat pPerso, Usuario vUsuario)
        {
            try
            {
                return BAApertura.ListarOficinas(pPerso, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListarOficinas", ex);
                return null;
            }
        }
        public List<Xpinn.CDATS.Entities.Cdat> ListarFechaVencimiento(Xpinn.CDATS.Entities.Cdat pPerso, Usuario vUsuario)
        {
            try
            {
                return BAApertura.ListarFechaVencimiento(pPerso, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListarFechaVencimiento", ex);
                return null;
            }
        }
        public Cdat DiasAvisoCDAT(Cdat pPerso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPerso = BAApertura.DiasAvisoCDAT(pPerso, vUsuario);
                    ts.Complete();
                }
                return pPerso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "DiasAvisoCDAT", ex);
                return null;
            }
        }
        public Detalle_CDAT ConsultarPersona(Detalle_CDAT pPerso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPerso = BAApertura.ConsultarPersona(pPerso, vUsuario);
                    ts.Complete();
                }
                return pPerso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ConsultarPersona", ex);
                return null;
            }
        }


        //CONSULTAR DATOS PRINCIPALES

        public Xpinn.CDATS.Entities.Cdat ConsultarApertura( Usuario vUsuario)
        {
            try
            {
                Xpinn.CDATS.Entities.Cdat pConsu = new Xpinn.CDATS.Entities.Cdat();
                pConsu = BAApertura.ConsultarApertura( vUsuario);

                return pConsu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ConsultarPersona", ex);
                return null;
            }
        }

        public Xpinn.FabricaCreditos.Entities.Credito ConsultarAperturas(Usuario vUsuario,string filtro)
        {
            try
            {
                Xpinn.FabricaCreditos.Entities.Credito pConsu = new Xpinn.FabricaCreditos.Entities.Credito();
                pConsu = BAApertura.ConsultarAperturas(vUsuario, filtro);

                return pConsu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ConsultarPersona", ex);
                return null;
            }
        }

        public Xpinn.CDATS.Entities.Cdat ConsultarApertu(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Xpinn.CDATS.Entities.Cdat pConsu = new Xpinn.CDATS.Entities.Cdat();
                pConsu = BAApertura.ConsultarApertu(pId, vUsuario);

                return pConsu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ConsultarPersona", ex);
                return null;
            }
        }

        public Xpinn.CDATS.Entities.Cdat ConsultarApertuXnumcdat(String pId, Usuario vUsuario)
        {
            try
            {
                Xpinn.CDATS.Entities.Cdat pConsu = new Xpinn.CDATS.Entities.Cdat();
                pConsu = BAApertura.ConsultarApertuXnumcdat(pId, vUsuario);

                return pConsu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ConsultarApertuXnumcdat", ex);
                return null;
            }
        }


        public Xpinn.FabricaCreditos.Entities.Credito CrearAperturaCDAT(Xpinn.FabricaCreditos.Entities.Credito pAperturaCDAT, Usuario vUsuario, Xpinn.Tesoreria.Entities.Operacion poperacion)
        {
            try
            {
                return BAApertura.CrearAperturaCDAT(pAperturaCDAT, vUsuario,poperacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "CrearAperturaCDAT", ex);
                return null;
            }
        }

         public List<Xpinn.FabricaCreditos.Entities.Credito> ListarDetalles(Usuario vUsuario, string filtro)
        {
            try
            {
                return BAApertura.ListarDetalles(vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListarDetalleTitulares", ex);
                return null;
            }
        }
        //CONSULTAR DETALLE TITULAR
        public List<Detalle_CDAT> ListarDetalleTitulares(Int64 pCod, Usuario vUsuario)
        {
            try
            {
                return BAApertura.ListarDetalleTitulares(pCod, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListarDetalleTitulares", ex);
                return null;
            }
        }

        public List<Detalle_CDAT> ListarDetalle(Usuario vUsuario)
        {
            try
            {
                return BAApertura.ListarDetalle( vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListarDetalle", ex);
                return null;
            }
        }



        public Xpinn.CDATS.Entities.Cdat ConsultarDiasPeriodicidad(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BAApertura.ConsultarDiasPeriodicidad(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ConsultarDiasPeriodicidad", ex);
                return null;
            }
        }


        public Xpinn.CDATS.Entities.Cdat ConsultarNumeracionCDATS(Xpinn.CDATS.Entities.Cdat pCadt, Usuario vUsuario)
        {
            try
            {
                Xpinn.CDATS.Entities.Cdat pConsu = new Xpinn.CDATS.Entities.Cdat();
                pConsu = BAApertura.ConsultarNumeracionCDATS(pCadt, vUsuario);

                return pConsu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ConsultarNumeracionCDATS", ex);
                return null;
            }
        }



        //GRABAR AUDITORIA
        public CDAT_AUDITORIA CrearAuditoriaCdat(CDAT_AUDITORIA pCdat, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCdat = BAApertura.CrearAuditoriaCdat(pCdat, vUsuario);                   
                    ts.Complete();
                }
                return pCdat;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "CrearAuditoriaCdat", ex);
                return null;
            }
        }


        public CDAT_AUDITORIA ModificarAuditoriaCdat(CDAT_AUDITORIA pCdat, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCdat = BAApertura.ModificarAuditoriaCdat(pCdat, vUsuario);
                    ts.Complete();
                }
                return pCdat;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ModificarAuditoriaCdat", ex);
                return null;
            }
        }

        public string CrearSolicitudCDAT(Cdat pCDAT, Usuario pUsuario, List<FabricaCreditos.Entities.DocumentosAnexos> lstDocumentos = null)
        {
            try
            {
                string codigo = BAApertura.CrearSolicitudCDAT(pCDAT, pUsuario);
                if (!string.IsNullOrEmpty(codigo))
                {
                    if (lstDocumentos != null)
                    {
                        if (lstDocumentos.Count > 0)
                        {
                            Xpinn.Imagenes.Data.ImagenesORAData DADocumento = new Imagenes.Data.ImagenesORAData();
                            foreach (FabricaCreditos.Entities.DocumentosAnexos nDocument in lstDocumentos)
                            {
                                nDocument.numerosolicitud = Convert.ToInt64(codigo);
                                FabricaCreditos.Entities.DocumentosAnexos pEntidad = new FabricaCreditos.Entities.DocumentosAnexos();
                                pEntidad = DADocumento.CrearDocumentosAnexos(nDocument, pUsuario);
                            }
                        }
                    }

                    return codigo;
                }
                return null;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "CrearSolicitudCDAT", ex);
                return null;
            }
        }

        public List<Xpinn.CDATS.Entities.Cdat> ListarTipoLineaCDAT(Xpinn.CDATS.Entities.Cdat pPerso, Usuario vUsuario)
        {
            try
            {
                return BAApertura.ListarTipoLineaCDAT(pPerso, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListarTipoLineaCDAT", ex);
                return null;
            }
        }



        public AdministracionCDAT cierrecdat(AdministracionCDAT traslado_cuenta, Xpinn.Tesoreria.Entities.Operacion poperacion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (traslado_cuenta.numero_cdat != "")
                    {
                        //CREACION DE LA OPERACION
                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                        poperacion = DAOperacion.GrabarOperacion(poperacion, vUsuario);

                        //HACER EL RETIRO DE LA CUENTA. Crear metodo para el retiro llamar USP_XPINN_AHO_RETIRO.

                        AdministracionCDAT Traslado = new AdministracionCDAT();                           
                        Traslado.fecha_vencimiento = DateTime.Now;
                        Traslado.valor = traslado_cuenta.valor;
                        Traslado.cod_persona = traslado_cuenta.cod_persona;
                        Traslado.fecha_vencimiento = poperacion.fecha_oper;
                        Traslado.numero_cdat = traslado_cuenta.numero_cdat;
                        Traslado.valor = traslado_cuenta.valor;
                        Traslado.intereses_cap = traslado_cuenta.intereses_cap;
                        Traslado.intereses = traslado_cuenta.intereses;
                        Traslado.retencion = traslado_cuenta.retencion;
                        Traslado.codigo_gmf = traslado_cuenta.codigo_gmf;

                        ts.Complete();

                        return BAApertura.cierrecdat(traslado_cuenta, poperacion, vUsuario);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "AplicarTraslado", ex);
                return null;
            }

        }



        public AdministracionCDAT RenovacionCdat(Xpinn.CDATS.Entities.Cdat pCdat, AdministracionCDAT traslado_cuenta, Xpinn.Tesoreria.Entities.Operacion poperacion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (traslado_cuenta.numero_cdat != "")
                    {
                        ////CREACION DE LA OPERACION
                        //Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                        //poperacion = DAOperacion.GrabarOperacion(poperacion, vUsuario);

                        ////CIERRE DEL Xpinn.CDATS.Entities.Cdat. 
                        //AdministracionCDAT Traslado = new AdministracionCDAT();
                        //Traslado.fecha_vencimiento = DateTime.Now;
                        //Traslado.valor = traslado_cuenta.valor;
                        //Traslado.cod_persona = traslado_cuenta.cod_persona;
                        //Traslado.fecha_vencimiento = poperacion.fecha_oper;
                        //Traslado.numero_cdat = traslado_cuenta.numero_cdat;
                        //Traslado.valor = traslado_cuenta.valor;
                        //Traslado.intereses_cap = traslado_cuenta.intereses_cap;
                        //Traslado.intereses = traslado_cuenta.intereses;
                        //Traslado.retencion = traslado_cuenta.retencion;
                        //Traslado.codigo_gmf = traslado_cuenta.codigo_gmf;

                        //APERTURA DEL Xpinn.CDATS.Entities.Cdat
                        pCdat = BAApertura.CrearAperturaCDAT(pCdat, vUsuario);
                        Int64 cod = pCdat.codigo_cdat;
                        if (pCdat.lstDetalle != null && pCdat.lstDetalle.Count > 0)
                        {
                            foreach (Detalle_CDAT pDeta in pCdat.lstDetalle)
                            {
                                Detalle_CDAT nDetalle = new Detalle_CDAT();
                                pDeta.codigo_cdat = cod;
                                nDetalle = BAApertura.CrearTitularCdat(pDeta, vUsuario, 1);
                            }
                        }

                      
                      //  return BAApertura.cierrecdat(traslado_cuenta, poperacion, vUsuario);
                        ts.Complete();

                       
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "AplicarTraslado", ex);
                return null;
            }

        }


        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            Xpinn.Comun.Business.FechasBusiness BOFechas = new Comun.Business.FechasBusiness();
            List<Xpinn.Comun.Entities.Cierea> LstCierre = new List<Xpinn.Comun.Entities.Cierea>();
            // Determinar la periodicidad de cierre
            int dias_cierre = 0;
            int tipo_calendario = 0;
            BAApertura.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            // Determinar la fecha del último cierre realizado
            Xpinn.Comun.Entities.Cierea pCierre = new Xpinn.Comun.Entities.Cierea();
            pCierre.tipo = "M";
            pCierre.estado = "D";
            pCierre = BAApertura.FechaUltimoCierre(pCierre, "", pUsuario);
            if (pCierre == null)
            {
                int año = DateTime.Now.Year;
                int mes = DateTime.Now.Month;
                if (mes <= 1)
                {
                    mes = 12;
                    año = año - 1;
                }
                else
                {
                    mes = mes - 1;
                }
                pCierre = new Xpinn.Comun.Entities.Cierea();
                pCierre.fecha = new DateTime(año, mes, 1, 0, 0, 0).AddDays(-1);
            }
            DateTime FecIni = pCierre.fecha;
            if (FecIni == DateTime.MinValue)
                return null;
            if (FecIni > DateTime.Now.AddDays(15))
                return null;
            // Calcular fechas de cierre inicial
            DateTime FecFin = DateTime.MinValue;
            FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);

            if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
            {
                bool control = true;
                do
                {
                    FecFin = FecFin.AddDays(1);
                    if (FecFin.Day == 1)
                    {
                        FecFin = FecFin.AddDays(-1);
                        control = false;
                    }
                } while (control == true);
            }

            // Determinar los periodos pendientes por cerrar
            while (FecFin <= DateTime.Now.AddDays(15))
            {
                Xpinn.Comun.Entities.Cierea cieRea = new Xpinn.Comun.Entities.Cierea();
                cieRea.fecha = FecFin;
                LstCierre.Add(cieRea);
                FecIni = FecFin;
                FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);

                if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
                {
                    bool control = true;
                    do
                    {
                        FecFin = FecFin.AddDays(1);
                        if (FecFin.Day == 1)
                        {
                            FecFin = FecFin.AddDays(-1);
                            control = false;
                        }
                    } while (control == true);
                }
            }
            return LstCierre;
        }

        public Cdat ciCierreHistorico(Cdat pentidad,string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            /*try
             {
                 using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                 {
                     BAApertura.ciCierreHistorico(estado, fecha, cod_usuario, ref serror, pUsuario);
                     ts.Complete();
                 }
             }
             catch (Exception ex)
             {
                 BOExcepcion.Throw("AperturaCDATBusiness", "CierreHistorico", ex);
             }
             */

            try
            {
                return BAApertura.ciCierreHistorico(pentidad, estado, fecha, cod_usuario, ref serror, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "CierreHistorico", ex);
                return null;
            }

        }

        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pUsuario, String filtro)
        {
            try
            {
                return BAApertura.ListarCredito(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListarCredito", ex);
                return null;
            }
        }


        public Xpinn.FabricaCreditos.Entities.Credito ListarCreditos(Usuario pUsuario, String filtro)
        {
            try
            {
                return BAApertura.ListarCreditos(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListarCredito", ex);
                return null;
            }
        }


        public Int32 ObtenerConsecutivo(string pConsulta, Usuario vUsuario)
        {
            try
            {
                return BAApertura.ObtenerConsecutivo(pConsulta, vUsuario);
            }
            catch
            {
                return 1;
            }
        }
        public Xpinn.CDATS.Entities.Cdat ConsultarAfiliacion(String pId, Usuario vUsuario)
        {
            try
            {
                Xpinn.CDATS.Entities.Cdat pConsu = new Xpinn.CDATS.Entities.Cdat();
                pConsu = BAApertura.ConsultarAfiliacion(pId, vUsuario);

                return pConsu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ConsultarAfiliacion", ex);
                return null;
            }
        }

        public DateTime Calcularfecha(DateTime fecha_vencimiento, DateTime fecha, Int32 plazo, Int32 tipocalendario)
        {
            try
            {
                Xpinn.Comun.Business.FechasBusiness fechasBusiness = new Xpinn.Comun.Business.FechasBusiness();
                fecha_vencimiento = fechasBusiness.FecSumDia(fecha, plazo, tipocalendario);

                return fecha_vencimiento;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ConsultarAfiliacion", ex);
                return fecha_vencimiento;
            }
        }

        public void Crearciereap(Xpinn.Comun.Entities.Cierea pcierea, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    BAApertura.Crearcierea(pcierea, vUsuario);
                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "Crearcierea", ex);
                return;
            }
        }
        public List<Cierea> ListarFechaCierreCaus(Usuario pUsuario)
        {
            Xpinn.Comun.Business.FechasBusiness BOFechas = new Comun.Business.FechasBusiness();
            List<Cierea> LstCierre = new List<Cierea>();
            // Determinar la periodicidad de cierre
            int dias_cierre = 0;
            int tipo_calendario = 0;
            BAApertura.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            // Determinar la fecha del último cierre realizado
            Cierea pCierre = new Cierea();
            pCierre.tipo = "K";
            pCierre.estado = "D";
            pCierre = BAApertura.FechaUltimoCierre(pCierre, "", pUsuario);
            if (pCierre == null)
            {
                int año = DateTime.Now.Year;
                int mes = DateTime.Now.Month;
                if (mes <= 1)
                {
                    mes = 12;
                    año = año - 1;
                }
                else
                {
                    mes = mes - 1;
                }
                pCierre = new Cierea();
                pCierre.fecha = new DateTime(año, mes, 1, 0, 0, 0).AddDays(-1);
            }
            DateTime FecIni = pCierre.fecha;
            if (FecIni == DateTime.MinValue)
                return null;
            if (FecIni > DateTime.Now.AddDays(15))
                return null;
            // Calcular fechas de cierre inicial
            DateTime FecFin = DateTime.MinValue;
            FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);

            if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
            {
                bool control = true;
                do
                {
                    FecFin = FecFin.AddDays(1);
                    if (FecFin.Day == 1)
                    {
                        FecFin = FecFin.AddDays(-1);
                        control = false;
                    }
                } while (control == true);
            }

            // Determinar los periodos pendientes por cerrar
            while (FecFin <= DateTime.Now.AddDays(15) || (FecFin.Year == DateTime.Now.Year && FecFin.Month == DateTime.Now.Month))
            {
                Cierea cieRea = new Cierea();
                cieRea.fecha = FecFin;
                LstCierre.Add(cieRea);
                FecIni = FecFin;
                FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);

                if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
                {
                    bool control = true;
                    do
                    {
                        FecFin = FecFin.AddDays(1);
                        if (FecFin.Day == 1)
                        {
                            FecFin = FecFin.AddDays(-1);
                            control = false;
                        }
                    } while (control == true);
                }
            }
            return LstCierre;
        }


        public List<Cdat> ListartodosUsuarios(Cdat pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                return BAApertura.ListartodosUsuarios(pUsuarioAse, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListartodosUsuarios", ex);
                return null;
            }
        }


    }
}
