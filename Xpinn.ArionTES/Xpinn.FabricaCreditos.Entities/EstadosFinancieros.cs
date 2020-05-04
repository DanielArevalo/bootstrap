using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad EstadosFinancieros
    /// </summary>
    [DataContract]
    [Serializable]
    public class EstadosFinancieros
    {
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember] 
        public Int64 cod_balance { get; set; }
        [DataMember] 
        public Int64 cod_inffin { get; set; }
        [DataMember] 
        public Int64 cod_cuenta { get; set; }
        [DataMember] 
        public double valor { get; set; }
        [DataMember]
        public String descripcion { get; set; }
        [DataMember]
        public String tipoInformacion { get; set; } //Permite seleccionar informacion familiar o de negocio
        [DataMember]
        public String tipo { get; set; }

        // Entidades para agregar toda la informacion financiera
        //Estado de resultados:
        [DataMember]
        public Int64 CodVenTot { get; set; }        
        [DataMember]
        public Double VenTot { get; set; }
        [DataMember]
        public Int64 CodVenCon { get; set; }
        [DataMember]
        public Double VenCon { get; set; }
        [DataMember]
        public Int64 CodVenCre { get; set; }
        [DataMember]
        public Int64 VenCre { get; set; }
        [DataMember]
        public Int64 CodCosVen { get; set; }
        [DataMember]
        public Double CosVen { get; set; }
        [DataMember]
        public Int64 CodUtiBru { get; set; }
        [DataMember]
        public Double UtiBru { get; set; }
        [DataMember]
        public Int64 CodGasPer { get; set; }
        [DataMember]
        public Int64 GasPer { get; set; }
        [DataMember]
        public Int64 CodSerPub { get; set; }
        [DataMember]
        public Int64 SerPub { get; set; }
        [DataMember]
        public Int64 CodArr { get; set; }
        [DataMember]
        public Int64 Arr { get; set; }
        [DataMember]
        public Int64 CodTra { get; set; }
        [DataMember]
        public Int64 Tra { get; set; }
        [DataMember]
        public Int64 CodOtrGas { get; set; }
        [DataMember]
        public Int64 OtrGas { get; set; }
        [DataMember]
        public Int64 CodTotGas { get; set; }
        [DataMember]
        public Int64 TotGas { get; set; }
        [DataMember]
        public Int64 CodUtiOpe { get; set; }
        [DataMember]
        public Double UtiOpe { get; set; }
        [DataMember]
        public Int64 CodCuoPre { get; set; }
        [DataMember]
        public Int64 CuoPre { get; set; }
        [DataMember]
        public Int64 CodCad { get; set; }
        [DataMember]
        public Int64 Cad { get; set; }
        [DataMember]
        public Int64 CodPres { get; set; }
        [DataMember]
        public Int64 Pres { get; set; }
        [DataMember]
        public Int64 CodOtrGasFin { get; set; }
        [DataMember]
        public Int64 OtrGasFin { get; set; }
        [DataMember]
        public Int64 CodTotGasFin { get; set; }
        [DataMember]
        public Int64 TotGasFin { get; set; }
        [DataMember]
        public Int64 CodUtiNet { get; set; }
        [DataMember]
        public Double UtiNet { get; set; }

        //Activos:
        [DataMember]
        public Int64 CodEfeBan { get; set; }
        [DataMember]
        public Int64 EfeBan { get; set; }
        [DataMember]
        public Int64 CodCueCob { get; set; }
        [DataMember]
        public Int64 CueCob { get; set; }
        [DataMember]
        public Int64 CodMerc { get; set; }
        [DataMember]
        public Int64 Merc { get; set; }
        [DataMember]
        public Int64 CodProdProc { get; set; }
        [DataMember]
        public Int64 ProdProc { get; set; }
        [DataMember]
        public Int64 CodProTer { get; set; }
        [DataMember]
        public Int64 ProTer { get; set; }
        [DataMember]
        public Int64 CodTotActCor { get; set; }
        [DataMember]
        public Int64 TotActCor { get; set; }
        [DataMember]
        public Int64 CodTerrEdi { get; set; }
        [DataMember]
        public Int64 TerrEdi { get; set; }
        [DataMember]
        public Int64 CodMaqEqu { get; set; }
        [DataMember]
        public Int64 MaqEqu { get; set; }
        [DataMember]
        public Int64 CodVehi { get; set; }
        [DataMember]
        public Int64 Vehi { get; set; }
        [DataMember]
        public Int64 CodOtr { get; set; }
        [DataMember]
        public Int64 Otr { get; set; }
        [DataMember]
        public Int64 CodTotActFij { get; set; }
        [DataMember]
        public Int64 TotActFij { get; set; }
        [DataMember]
        public Int64 CodTotAct { get; set; }
        [DataMember]
        public Int64 TotAct { get; set; }

        //Pasivo y patrimonio
        [DataMember]
        public Int64 CodPro { get; set; }
        [DataMember]
        public Int64 Pro { get; set; }
        [DataMember]
        public Int64 CodOblFin { get; set; }
        [DataMember]
        public Int64 OblFin { get; set; }
        [DataMember]
        public Int64 CodOtrObl { get; set; }
        [DataMember]
        public Int64 OtrObl { get; set; }
        [DataMember]
        public Int64 CodTotPasCor { get; set; }
        [DataMember]
        public Int64 TotPasCor { get; set; }
        [DataMember]
        public Int64 CodPasLar { get; set; }
        [DataMember]
        public Int64 PasLar { get; set; }
        [DataMember]
        public Int64 CodOtrOblLar { get; set; }
        [DataMember]
        public Int64 OtrOblLar { get; set; }
        [DataMember]
        public Int64 CodTotPas { get; set; }
        [DataMember]
        public Int64 TotPas { get; set; }
        [DataMember]
        public Int64 CodTotPat { get; set; }
        [DataMember]
        public Int64 TotPat { get; set; }
        [DataMember]
        public Int64 CodTotPasPat { get; set; }
        [DataMember]
        public Int64 TotPasPat { get; set; }

        //INFORMACION INGRESOS Y EGRESOS 
        [DataMember]
        public Int64 cod_personaconyuge { get; set; }
        [DataMember]
        public decimal sueldo { get; set; }
        [DataMember]
        public decimal sueldoconyuge { get; set; }
        [DataMember]
        public decimal honorarios { get; set; }
        [DataMember]
        public decimal honorariosconyuge { get; set; }
        [DataMember]
        public decimal arrendamientos { get; set; }
        [DataMember]
        public decimal arrendamientosconyuge { get; set; }
        [DataMember]
        public decimal otrosingresos { get; set; }
        [DataMember]
        public decimal otrosingresosconyuge { get; set; }
        [DataMember]
        public string conceptootros { get; set; }
        [DataMember]
        public string conceptootrosconyuge { get; set; }
        [DataMember]
        public decimal totalingreso { get; set; }
        [DataMember]
        public decimal totalingresoconyuge { get; set; }
        [DataMember]
        public decimal hipoteca { get; set; }
        [DataMember]
        public decimal hipotecaconyuge { get; set; }
        [DataMember]
        public decimal targeta_credito { get; set; }
        [DataMember]
        public decimal targeta_creditoconyuge { get; set; }
        [DataMember]
        public decimal otrosprestamos { get; set; }
        [DataMember]
        public decimal otrosprestamosconyuge { get; set; }
        [DataMember]
        public decimal gastofamiliar { get; set; }
        [DataMember]
        public decimal gastofamiliarconyuge { get; set; }
        [DataMember]
        public decimal decunomina { get; set; }
        [DataMember]
        public decimal decunominaconyuge { get; set; }
        [DataMember]
        public decimal totalegresos { get; set; }
        [DataMember]
        public decimal totalegresosconyuge { get; set; }
        [DataMember]
        public decimal ingresomensual { get; set; }
        [DataMember]
        public decimal egresomensual { get; set; }

        //Entidades necesarias para realizar calculos
        [DataMember]
        public double porCostoVenta { get; set; }
        [DataMember]
        public Int64 cuotasPrestamos { get; set; }

        //Entidades necesarias para llamar resultados desde otros formularios
        [DataMember]
        public String filtro { get; set; }

        [DataMember]
        public Int64 valor_arriendo { get; set; }

        //Agregado para información de manejo de moneda extranjera
        [DataMember]
        public Int64? cod_moneda_ext { get; set; }
        [DataMember]
        public string num_cuenta_ext { get; set; }
        [DataMember]
        public string banco_ext { get; set; }
        [DataMember]
        public string pais { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string moneda { get; set; }
        [DataMember]
        public string desc_operacion { get; set; }
        [DataMember]
        public string tipo_producto { get; set; }

        //Agregado para información del conyuge
        [DataMember]
        public Int64 TotPatConyuge { get; set; }
        [DataMember]
        public Int64 TotPasConyuge { get; set; }
        [DataMember]
        public Int64 TotActConyuge { get; set; }

    }
}