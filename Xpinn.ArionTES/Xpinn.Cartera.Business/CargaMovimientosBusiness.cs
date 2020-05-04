using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using System.Text;
using System.Transactions;
using Xpinn.Cartera.Data;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Cartera.Business
{
    public class CargaMovimientosBusiness
    {
        private ExcepcionBusiness BOExcepcion;
        private CargaMovimientosData CargaMovimientos = new CargaMovimientosData();
        private Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();

        public Boolean CargaMasivoMovimientos(List<CargaMovimientos> carga, Usuario pUsuario, Operacion pOperacion, ref string Error)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
                    vOpe = DAOperacion.GrabarOperacion(pOperacion, pUsuario);

                    CargaMovimientos movimientos = new CargaMovimientos();
                    foreach (CargaMovimientos item in carga)
                    {
                        movimientos.NumeroProducto = item.NumeroProducto;
                        movimientos.CodPersona = item.CodPersona;
                        movimientos.Valor = item.Valor;
                        movimientos.TipoProducto = item.TipoProducto;
                        movimientos.TipoMovimiento = Convert.ToString(item.TipoMovimiento == "C" ? 1 : 2);
                        movimientos.TipoNota = movimientos.TipoMovimiento == "1" ? 1 : 0;
                        movimientos.CodOperacion = Convert.ToInt32(vOpe.cod_ope);
                        CargaMovimientos.CargaMasivoMovimientos(movimientos, pUsuario, ref Error);
                    }
                    return true;
                }
                catch (Exception e)
                {
                    BOExcepcion.Throw("CargaMovimientos", "CargaMovimientosBusiness", e);
                    return false;
                }
            }
        }

        public TipoProducto ConsultarProducto(string tipo_producto, Usuario pusuUsuario)
        {
            try
            {
                return CargaMovimientos.ConsultarProducto(tipo_producto, pusuUsuario);
            }
            catch (Exception e)
            {
                BOExcepcion.Throw("CargaMovimientos", "CargaMovimientosBusiness", e);
                return null;
            }

        }

        public TipoProducto ConsultaNProducto(string Query, Usuario pUsuario)
        {
            try
            {
                return CargaMovimientos.ConsultaNProducto(Query, pUsuario);
            }
            catch (Exception e)
            {
                BOExcepcion.Throw("CargaMovimientos", "ConsultaNProducto", e);
                return null;
            }
        }

        public TipoProducto ConsultaSaldo(Int64 numero_radicacion, Usuario pUsuario)
        {
            try
            {
                return CargaMovimientos.ConsultaSaldo(numero_radicacion, pUsuario);
            }
            catch (Exception e)
            {
                BOExcepcion.Throw("CargaMovimientos", "ConsultaSaldo", e);
                return null;
            }
        }
    }



}
