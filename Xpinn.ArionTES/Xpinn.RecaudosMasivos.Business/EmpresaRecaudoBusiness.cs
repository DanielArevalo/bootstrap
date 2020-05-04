using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    public class EmpresaRecaudoBusiness : GlobalData
    {

        private EmpresaRecaudoData DAEmpresaRecaudo;
        private EmpresaEstructuraCargaData DAEmpresaRecaudoCarga;

        public EmpresaRecaudoBusiness()
        {
            DAEmpresaRecaudo = new EmpresaRecaudoData();
            DAEmpresaRecaudoCarga = new EmpresaEstructuraCargaData();
        }

        public EmpresaRecaudo CrearEmpresaRecaudo(EmpresaRecaudo pEmpresaRecaudo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Int64 cod;
                    pEmpresaRecaudo = DAEmpresaRecaudo.CrearEmpresaRecaudo(pEmpresaRecaudo, vUsuario);

                    cod = pEmpresaRecaudo.cod_empresa;

                    if (pEmpresaRecaudo.lstProgramacion != null)
                    {
                        int num = 0;
                        foreach (EmpresaRecaudo_Programacion eProg in pEmpresaRecaudo.lstProgramacion)
                        {
                            EmpresaRecaudo_Programacion nPrograma = new EmpresaRecaudo_Programacion();
                            eProg.cod_empresa = cod;
                            nPrograma = DAEmpresaRecaudo.CrearProgramacionRecaudo(eProg, vUsuario);
                            num += 1;
                        }
                    }

                    if (pEmpresaRecaudo.lstConcepto != null)
                    {
                        int num = 0;
                        foreach (EMPRESARECAUDO_CONCEPTO eConcep in pEmpresaRecaudo.lstConcepto)
                        {
                            EMPRESARECAUDO_CONCEPTO nPrograma = new EMPRESARECAUDO_CONCEPTO();
                            eConcep.cod_empresa = cod;
                            nPrograma = DAEmpresaRecaudo.CrearConceptoRecaudo(eConcep, vUsuario);
                            num += 1;
                        }
                    }

                    if (pEmpresaRecaudo.lstEstructura != null)
                    {
                        int num = 0;
                        foreach (EmpresaEstructuraCarga eConcep in pEmpresaRecaudo.lstEstructura)
                        {
                            EmpresaEstructuraCarga nPrograma = new EmpresaEstructuraCarga();
                            eConcep.cod_empresa = cod;
                            nPrograma = DAEmpresaRecaudoCarga.CrearEmpresaEstructuraCarga(eConcep, vUsuario);
                            num += 1;
                        }
                    }

                    if (pEmpresaRecaudo.lstExcluyente != null && pEmpresaRecaudo.lstExcluyente.Count > 0)
                    {
                        EmpresaExcluyenteData DAEmpresaExcluyente = new EmpresaExcluyenteData();
                        foreach (EmpresaExcluyente eExcl in pEmpresaRecaudo.lstExcluyente)
                        {
                            EmpresaExcluyente nExcluyente = new EmpresaExcluyente();
                            eExcl.cod_empresa = Convert.ToInt32(cod);
                            nExcluyente = DAEmpresaExcluyente.CrearEmpresaExcluyente(eExcl, vUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pEmpresaRecaudo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosBusiness", "CrearEmpresaRecaudo", ex);
                return null;
            }
        }

        public EmpresaRecaudo ModificarEmpresaRecaudo(EmpresaRecaudo pEmpresaRecaudo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpresaRecaudo = DAEmpresaRecaudo.ModificarEmpresaRecaudo(pEmpresaRecaudo, vUsuario);

                    Int64 Cod;
                    Cod = pEmpresaRecaudo.cod_empresa;

                    if (pEmpresaRecaudo.lstProgramacion != null)
                    {
                        int num = 0;
                        foreach (EmpresaRecaudo_Programacion eProgra in pEmpresaRecaudo.lstProgramacion)
                        {
                            eProgra.cod_empresa = Cod;
                            EmpresaRecaudo_Programacion nProgramacion = new EmpresaRecaudo_Programacion();
                            if (eProgra.idprogramacion <= 0 || eProgra.idprogramacion == null)
                                nProgramacion = DAEmpresaRecaudo.CrearProgramacionRecaudo(eProgra, vUsuario);
                            else
                                nProgramacion = DAEmpresaRecaudo.ModificarProgramacionRecaudo(eProgra, vUsuario);
                            num += 1;
                        }
                    }

                    if (pEmpresaRecaudo.lstConcepto != null)
                    {
                        int num = 0;
                        foreach (EMPRESARECAUDO_CONCEPTO eConce in pEmpresaRecaudo.lstConcepto)
                        {
                            eConce.cod_empresa = Cod;
                            EMPRESARECAUDO_CONCEPTO nConcepto = new EMPRESARECAUDO_CONCEPTO();
                            if (eConce.idempconcepto <= 0 || eConce.idempconcepto == null)
                            {
                                nConcepto = DAEmpresaRecaudo.CrearConceptoRecaudo(eConce, vUsuario);
                            }
                            else
                                nConcepto = DAEmpresaRecaudo.ModificarConceptoRecaudo(eConce, vUsuario);
                            num += 1;
                        }
                    }

                    if (pEmpresaRecaudo.lstEstructura != null)
                    {
                        int num = 0;
                        foreach (EmpresaEstructuraCarga eConce in pEmpresaRecaudo.lstEstructura)
                        {
                            eConce.cod_empresa = Cod;
                            EmpresaEstructuraCarga nConcepto = new EmpresaEstructuraCarga();
                            if (eConce.codemparchivo <= 0 || eConce.codemparchivo == null)
                            {
                                nConcepto = DAEmpresaRecaudoCarga.CrearEmpresaEstructuraCarga(eConce, vUsuario);
                            }
                            else
                                nConcepto = DAEmpresaRecaudoCarga.ModificarEmpresaEstructuraCarga(eConce, vUsuario);
                            num += 1;
                        }
                    }

                    if (pEmpresaRecaudo.lstExcluyente != null)
                    {
                        EmpresaExcluyenteData DAEmpresaExcluyente = new EmpresaExcluyenteData();
                        DAEmpresaExcluyente.EliminarEmpresaExcluyente(Convert.ToInt32(Cod), vUsuario);
                        foreach (EmpresaExcluyente eExcl in pEmpresaRecaudo.lstExcluyente)
                        {
                            EmpresaExcluyente nExcluyente = new EmpresaExcluyente();
                            eExcl.cod_empresa = Convert.ToInt32(Cod);
                            nExcluyente = DAEmpresaExcluyente.CrearEmpresaExcluyente(eExcl, vUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pEmpresaRecaudo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosBusiness", "ModificarEmpresaRecaudo", ex);
                return null;
            }
        }

        public bool VerificarQueYaNoSeHallaCargadoLaMismaNovedad(EmpresaRecaudo empresa, Usuario usuario)
        {
            try
            {
                return DAEmpresaRecaudo.VerificarQueYaNoSeHallaCargadoLaMismaNovedad(empresa, usuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosBusiness", "VerificarQueYaNoSeHallaCargadoLaMismaNovedad", ex);
                return false;
            }
        }

        public void EliminarEmpresaRecaudo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEmpresaRecaudo.EliminarEmpresaRecaudo(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosBusiness", "EliminarEmpresaRecaudo", ex);
            }
        }


        public List<EmpresaRecaudo> ListarEmpresaRecaudo(EmpresaRecaudo pEmpresaRecaudo, Usuario pUsuario)
        {
            try
            {
                return DAEmpresaRecaudo.ListarEmpresaRecaudo(pEmpresaRecaudo, pUsuario);
            }
            catch
            {
                return null;
            }
        }


        public List<EmpresaRecaudo> ListarEmpresaRecaudoPersona(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAEmpresaRecaudo.ListarEmpresaRecaudoPersona(pId, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public EmpresaRecaudo ConsultarEMPRESARECAUDO(EmpresaRecaudo pEntidad, Usuario pUsuario)
        {
            try
            {
                return DAEmpresaRecaudo.ConsultarEMPRESARECAUDO(pEntidad, pUsuario);

            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosBusiness", "ConsultarEMPRESARECAUDO", ex);
                return null;
            }
        }

        public EmpresaRecaudo ConsultarEMPRESARECAUDO(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAEmpresaRecaudo.ConsultarEMPRESARECAUDO(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosBusiness", "ConsultarEMPRESARECAUDO", ex);
                return null;
            }
        }


        public List<EMPRESARECAUDO_CONCEPTO> ListarEMPRESACONCEPTO(EMPRESARECAUDO_CONCEPTO pConcepto, Usuario vUsuario)
        {
            try
            {
                return DAEmpresaRecaudo.ListarEMPRESACONCEPTO(pConcepto, vUsuario);

            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosBusiness", "ListarEMPRESACONCEPTO", ex);
                return null;
            }
        }


        public List<EmpresaRecaudo_Programacion> ListarEMPRESAPROGRAMACION(EmpresaRecaudo_Programacion pProgramacion, Usuario vUsuario)
        {
            try
            {
                return DAEmpresaRecaudo.ListarEMPRESAPROGRAMACION(pProgramacion, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosBusiness", "ListarEMPRESAPROGRAMACION", ex);
                return null;
            } 
        }


        public void EliminarEmpresaPrograma(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEmpresaRecaudo.EliminarEmpresaPrograma(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosBusiness", "EliminarEmpresaPrograma", ex);
            }
        }

        public void EliminarEmpresaConcepto(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEmpresaRecaudo.EliminarEmpresaConcepto(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosBusiness", "EliminarEmpresaConcepto", ex);
            }
        }

        public List<EmpresaRecaudo_Programacion> GenerarPeriodos(Int64 pCodEmpresa, Int64 pTipoLista, DateTime pFecha, Usuario pUsuario)
        {
            // Determinar los datos de la empresa
            List<EmpresaRecaudo_Programacion> lstPeriodos = new List<EmpresaRecaudo_Programacion>();
            Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new EmpresaRecaudo();
            empresa = DAEmpresaRecaudo.ConsultarEMPRESARECAUDO(pCodEmpresa, pUsuario);
            // Determinar la programación de la empresa para el tipo de lista dado
            Xpinn.Tesoreria.Entities.EmpresaRecaudo_Programacion programacion = new EmpresaRecaudo_Programacion();
            programacion.cod_empresa = pCodEmpresa;
            programacion.tipo_lista = Convert.ToInt32(pTipoLista);
            programacion = DAEmpresaRecaudo.ConsultarEMPRESAPROGRAMACION(programacion, pUsuario);
            if (programacion == null)
                return null;
            // Determinar la periodicidad de la generación de los períodos
            Xpinn.FabricaCreditos.Entities.Periodicidad periodicidad = new FabricaCreditos.Entities.Periodicidad();
            Xpinn.FabricaCreditos.Data.PeriodicidadData DAperiodicidad = new FabricaCreditos.Data.PeriodicidadData();
            periodicidad = DAperiodicidad.ConsultarPeriodicidad(Convert.ToInt32(programacion.cod_periodicidad), pUsuario);
            if (periodicidad == null)
                return null;
            // Determinar los períodos a generar              
            int control = 0;
            Xpinn.Comun.Business.FechasBusiness BOFechas = new Comun.Business.FechasBusiness();
            pFecha = BOFechas.FecSumDia(pFecha, Convert.ToInt32(empresa.dias_novedad) + 10, 1);
            if (programacion.fecha_inicio != null && programacion.fecha_inicio != DateTime.MinValue)
            {
                DateTime periodo = Convert.ToDateTime(programacion.fecha_inicio);
                do
                {
                    // Consultar si el período ya fue generado para la empresa
                    EmpresaNovedadData DANovedad = new EmpresaNovedadData();
                    EmpresaNovedad novedad = new EmpresaNovedad();
                    List<EmpresaNovedad> lstNovedad = new List<EmpresaNovedad>();
                    novedad.cod_empresa = pCodEmpresa;
                    novedad.tipo_lista = Convert.ToInt32(pTipoLista);
                    novedad.periodo_corte = periodo;
                    lstNovedad = DANovedad.ListarRecaudo(novedad, "", pUsuario);
                    if (lstNovedad.Count <= 0)
                    {
                        // Adicionar el período a la lista
                        EmpresaRecaudo_Programacion entidad = new EmpresaRecaudo_Programacion();
                        entidad.cod_empresa = pCodEmpresa;
                        entidad.fecha_inicio = periodo;
                        lstPeriodos.Add(entidad);
                    }
                    periodo = BOFechas.FecSumDia(periodo, Convert.ToInt32(periodicidad.numero_dias), Convert.ToInt32(periodicidad.tipo_calendario));
                    control += 1;
                } while (periodo <= pFecha && control <= 240);
            }
            return lstPeriodos;
        }

        public EmpresaRecaudo_Programacion ConsultarEMPRESAPROGRAMACION(Int64 pCodEmpresa, Int64 pTipoLista, Usuario pUsuario)
        {
            try
            {
                Xpinn.Tesoreria.Entities.EmpresaRecaudo_Programacion programacion = new EmpresaRecaudo_Programacion();
                programacion.cod_empresa = pCodEmpresa;
                programacion.tipo_lista = Convert.ToInt32(pTipoLista);
                programacion = DAEmpresaRecaudo.ConsultarEMPRESAPROGRAMACION(programacion, pUsuario);
                return programacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosBusiness", "ConsultarEMPRESAPROGRAMACION", ex);
                return null;
            }
        }

        public DateTime? CalcularFechaInicialNovedad(Int64 pCodEmpresa, Int64 pTipoLista, DateTime pFecha, Usuario pUsuario)
        {
            DateTime? pFechaIni = null; 
            try
            {
                Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new EmpresaRecaudo();
                empresa = DAEmpresaRecaudo.ConsultarEMPRESARECAUDO(pCodEmpresa, pUsuario);
                // Determinar la programación de la empresa para el tipo de lista dado
                Xpinn.Tesoreria.Entities.EmpresaRecaudo_Programacion programacion = new EmpresaRecaudo_Programacion();
                programacion.cod_empresa = pCodEmpresa;
                programacion.tipo_lista = Convert.ToInt32(pTipoLista);
                programacion.principal = 1;
                programacion = DAEmpresaRecaudo.ConsultarEMPRESAPROGRAMACION(programacion, pUsuario);
                if (programacion == null)
                    return null;
                // Determinar la periodicidad de la generación de los períodos
                Xpinn.FabricaCreditos.Entities.Periodicidad periodicidad = new FabricaCreditos.Entities.Periodicidad();
                Xpinn.FabricaCreditos.Data.PeriodicidadData DAperiodicidad = new FabricaCreditos.Data.PeriodicidadData();
                periodicidad = DAperiodicidad.ConsultarPeriodicidad(Convert.ToInt32(programacion.cod_periodicidad), pUsuario);
                if (periodicidad == null)
                    return null;
                Xpinn.Comun.Business.FechasBusiness BOFechas = new Comun.Business.FechasBusiness();
                //pFecha = BOFechas.FecSumDia(pFecha, Convert.ToInt32(empresa.dias_novedad) + 10, 1);
                if (programacion.fecha_inicio != null && programacion.fecha_inicio != DateTime.MinValue)
                {
                    DateTime periodo = Convert.ToDateTime(programacion.fecha_inicio);
                    DateTime periodoAnt;
                    do
                    {
                        periodoAnt = periodo;
                        periodo = BOFechas.FecSumDia(periodo, Convert.ToInt32(periodicidad.numero_dias), Convert.ToInt32(periodicidad.tipo_calendario));
                    } while (periodo < pFecha);

                    int mes = periodoAnt.Month;
                    if (mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12)
                        periodoAnt = periodoAnt.AddDays(2);
                    else
                        periodoAnt = periodoAnt.AddDays(1);
                    pFechaIni = periodoAnt;
                }

                return pFechaIni;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosBusiness", "CalcularFechaInicialNovedad", ex);
                return null;
            }
        }


    }
}


