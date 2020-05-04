using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Data;
using Xpinn.Util;

namespace Xpinn.Auxilios.Business
{
    public class LineaAuxilioBusiness : GlobalBusiness
    {
        protected LineaAuxilioData BAAuxilio;

        public LineaAuxilioBusiness() 
        {
            BAAuxilio = new LineaAuxilioData();
        }


        public LineaAuxilio CrearLineaAuxilio(LineaAuxilio pAuxilio, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAuxilio = BAAuxilio.CrearLineaAuxilio(pAuxilio, pusuario);

                    ts.Complete();

                }

                return pAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuxilioBusiness", "CrearAuxilio", ex);
                return null;
            }
        }


        public LineaAuxilio ModificarLineaAuxilio(LineaAuxilio pAuxilio, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAuxilio = BAAuxilio.ModificarLineaAuxilios(pAuxilio, pusuario);

                    ts.Complete();

                }

                return pAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuxilioBusiness", "ModificarAuxilio", ex);
                return null;
            }
        }


        public void EliminarLineaAuxilios(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BAAuxilio.EliminarLineaAuxilios(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuxilioBusiness", "EliminarAuxilio", ex);
            }
        }

        public LineaAuxilio ConsultarAuxilio(Int64 pId, Usuario pusuario)
        {
            try
            {
                LineaAuxilio Auxilio = new LineaAuxilio();
                Auxilio = BAAuxilio.ConsultarLineaAuxilio(pId, pusuario);
                return Auxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuxilioBusiness", "ConsultarAuxilio", ex);
                return null;
            }
        }
 


        public LineaAuxilio CrearLineaAuxilio(LineaAuxilio pAuxilio, Usuario vUsuario, int Opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    string cod;
                    pAuxilio = BAAuxilio.Crear_Modi_LineaAuxilio(pAuxilio, vUsuario, Opcion);

                    cod = pAuxilio.cod_linea_auxilio;

                    if (pAuxilio.lstRequisitos != null)
                    {
                        int num = 0;
                        foreach (RequisitosLineaAuxilio eAux in pAuxilio.lstRequisitos)
                        {
                            RequisitosLineaAuxilio nAuxilios = new RequisitosLineaAuxilio();
                            eAux.cod_linea_auxilio = cod;
                            nAuxilios = BAAuxilio.CrearRequisitosDeLineaAux(eAux, vUsuario);
                            num += 1;
                        }
                    }

                    ts.Complete();
                }

                return pAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioBusiness", "CrearLineaAuxilio", ex);
                return null;
            }
        }


        public LineaAuxilio ModificarLineaAuxilio(LineaAuxilio pAuxilio, Usuario vUsuario,int Opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAuxilio = BAAuxilio.Crear_Modi_LineaAuxilio(pAuxilio, vUsuario, Opcion);

                    string Cod;
                    Cod = pAuxilio.cod_linea_auxilio;

                    if (pAuxilio.lstRequisitos != null)
                    {
                        int num = 0;
                        foreach (RequisitosLineaAuxilio eAux in pAuxilio.lstRequisitos)
                        {
                            eAux.cod_linea_auxilio = Cod;
                            RequisitosLineaAuxilio nAuxilio = new RequisitosLineaAuxilio();
                            if (eAux.codrequisitoaux <= 0 || eAux.codrequisitoaux == null)
                                nAuxilio = BAAuxilio.CrearRequisitosDeLineaAux(eAux, vUsuario);
                            else
                                nAuxilio = BAAuxilio.ModificarRequisitosDeLineaAux(eAux, vUsuario);
                            num += 1;
                        }
                    }

                    ts.Complete();
                }

                return pAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioBusiness", "ModificarLineaAuxilio", ex);
                return null;
            }
        }



        public List<LineaAuxilio> ListarLineaAuxilio(LineaAuxilio pAuxilio, Usuario vUsuario, string filtro)
        {
            try
            {
                return BAAuxilio.ListarLineaAuxilio(pAuxilio,vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioBusiness", "ListarLineaAuxilio", ex);
                return null;
            }            
        }


        public void EliminarLineaAuxilio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BAAuxilio.EliminarLineaAuxilio(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioBusiness", "EliminarLineaAuxilio", ex);
            }
        }

        public LineaAuxilio ConsultarLineaAUXILIO(string pId, Usuario vUsuario)
        {
            try
            {
                return BAAuxilio.ConsultarLineaAUXILIO(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioBusiness", "ConsultarLineaAUXILIO", ex);
                return null;
            }   
        }




        //Detalle

        public void EliminarDETALLELineaAux(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BAAuxilio.EliminarDETALLELineaAux(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioBusiness", "EliminarDETALLELineaAux", ex);
            }
        }
 

        public List<RequisitosLineaAuxilio> ConsultarDETALLELineaAux(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BAAuxilio.ConsultarDETALLELineaAux(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAuxilioBusiness", "ConsultarDETALLELineaAux", ex);
                return null;
            }   
        }



    }
}
