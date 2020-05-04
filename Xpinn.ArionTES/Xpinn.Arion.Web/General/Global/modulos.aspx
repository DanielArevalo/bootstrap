<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modulos.aspx.cs" Inherits="modulos" %>
<%@ Register src="../Controles/header.ascx" tagname="header" tagprefix="uc1" %>
<!DOCTYPE HTML>

<html>
<head id="Head1" runat="server">
<title>Financial Software Web</title>
<link rel="stylesheet" href="../../Styles/Styles.css"/>
    <link rel="stylesheet" href="../../Styles/StyleHeader.css"/>
<script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!--ESTILOS BOOTSTRAP-->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>




<style type="text/css">
    #form1
    {
        height: 100%;
    }
	
.mr-rt {
	margin-right: 10px;
}
.contentModulos
{
    width: 1250px;
    margin: 15px auto;  
    height: auto;
}
.fl-lt {
	margin-bottom: 10px;
}

.TitleModulo {
    font-family:Verdana;
    font-weight:600;
    z-index:1000;
    display:block;
    float:none;
    margin-top:-35px;
    margin-bottom:10%;
    font-size:14px;
}
</style>
</head>
<body class="modulo">
    <form id="form1" runat="server" style="height:100%">
        <uc1:header ID="header1" runat="server" />
        <div class="contentModulos" >
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnFabrica" AlternateText="Fabrica de Creditos" 
                    runat="server" ImageUrl="~/Images/iconFabrica.jpg" 
                    onclick="btnFabrica_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="FabricaCreditos" Text="<%$  Resources:Resource,Fabrica_Creditos%>" />
            </div>
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnAsesores" AlternateText="Asesores comerciales" 
                    runat="server" ImageUrl="~/Images/IconAsesores.jpg"
                    onclick="btnAsesores_Click"  style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="AsesoresComerciales" Text="<%$  Resources:Resource,Asesores_Comerciales%>" />
            </div>
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnScoring" AlternateText="Scoring" runat="server" 
                    ImageUrl="~/Images/iconScoring.jpg" 
                    onclick="btnScoring_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="Scoring" Text="<%$  Resources:Resource,Scoring%>" />
            </div>
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnContabilidad" AlternateText="Contabilidad" runat="server" 
                    ImageUrl="~/Images/iconContabilidad.jpg" 
                    onclick="btnContabilidad_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="Contabilidad" Text="<%$  Resources:Resource,Contabilidad%>" />
            </div>
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnReportes" AlternateText="Reportes" runat="server" 
                    ImageUrl="~/Images/iconReporteador.jpg"
                    onclick="btnReportes_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="Reporteador" Text="<%$  Resources:Resource,Reporteador%>" />
            </div>
            <div class="fl-lt rollover">
                <asp:ImageButton ID="btnRiesgo" AlternateText="Gestion de Riesgo" runat="server" 
                    ImageUrl="~/Images/IconRiesgos.jpg" 
                    onclick="btnRiesgo_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="GestionRiesgo" Text="<%$  Resources:Resource,Gestion_Riesgo%>" />
            </div>
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnObligaciones" AlternateText="Obligaciones financieras" 
                    runat="server" ImageUrl="~/Images/iconObligaciones.jpg"
                    onclick="btnObligaciones_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="ObligacionesFinancieras" Text="<%$  Resources:Resource,Obligaciones_Financieras%>" />
            </div>
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnIndicadores" AlternateText="Indicadores gerenciales" 
                    runat="server" ImageUrl="~/Images/iconIndicadores.jpg" 
                    onclick="btnIndicadores_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="IndicadoresGerenciales" Text="<%$  Resources:Resource,Indicadores_Gerenciales%>" />
            </div>
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnCaja" AlternateText="Caja financiera" runat="server" 
                    ImageUrl="~/Images/iconCaja.jpg" 
                    onclick="btnCaja_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="CajaFinanciera" Text="<%$  Resources:Resource,Caja_Financiera%>" />
            </div>
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnCartera" AlternateText="Cartera" runat="server" 
                    ImageUrl="~/Images/iconCartera.jpg"
                    onclick="btnCartera_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="Cartera" Text="<%$  Resources:Resource,Cartera%>" />
            </div>
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnActivos" AlternateText="Activos Fijos" runat="server" 
                    ImageUrl="~/Images/iconActivos.jpg" 
                    onclick="btnActivos_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="ActivosFijos" Text="<%$  Resources:Resource,Activos_fijos%>" />
            </div>
            <div class="fl-lt rollover">     
                <asp:ImageButton ID="btnNomina" AlternateText="NIC/NIIF" runat="server" 
                    ImageUrl="~/Images/IconNomina.jpg" 
                    onclick="btnNomina_Click" style="border-radius:8%; z-index:999;" /><br />     
                <asp:Label class="TitleModulo" runat="Server" ID ="NominaEmpleados" Text="<%$  Resources:Resource,Nomina_Empleados%>" />     
            </div>
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnTesoreria" AlternateText="Tesoreria" 
                    runat="server" ImageUrl="~/Images/iconTesoreria.jpg" 
                    onclick="btnTesoreria_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="Tesoreria" Text="<%$  Resources:Resource,Tesoreria%>" />
            </div>
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnRecaudos" AlternateText="Recaudos Masivos" runat="server" 
                    ImageUrl="~/Images/iconrecaudos.jpg" 
                    onclick="btnRecaudos_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="RecaudosMasivos" Text="<%$  Resources:Resource,Recaudos_Masivos%>" />
            </div>
            <div class="fl-lt mr-rt rollover">
                <asp:ImageButton ID="btnAportes" AlternateText="Aportes Sociales" runat="server" 
                    ImageUrl="~/Images/iconaporte.jpg" 
                    onclick="btnAportes_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="AportesSociales" Text="<%$  Resources:Resource,Aportes_Sociales%>" />
            </div>
            <div class="fl-lt mr-rt rollover">     
                <asp:ImageButton ID="btnNIIF" AlternateText="NIC/NIIF" runat="server" 
                    ImageUrl="~/Images/iconNIIF.jpg"  
                    onclick="btnNIIF_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="NIIF" Text="<%$  Resources:Resource,NIIF%>" />           
            </div>
            <div class="fl-lt mr-rt rollover">     
                <asp:ImageButton ID="btnDepositos" AlternateText="Depósitos" runat="server" 
                    ImageUrl="~/Images/iconDepositos.jpg" 
                    onclick="btnDepositos_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="Depositos" Text="<%$  Resources:Resource,Depositos%>" />                
            </div>
            <div class="fl-lt rollover">     
                <asp:ImageButton ID="btnTransferencia" AlternateText="Transferencia Solidaria" runat="server" 
                    ImageUrl="~/Images/IconTransferenciaSolidaria.jpg" 
                    onclick="btnTransferencia_Click" style="border-radius:8%; z-index:999;" /><br />
                <asp:Label class="TitleModulo" runat="Server" ID ="TransferenciaSolidaria" Text="<%$  Resources:Resource,Transferencia_Solidaria%>" />             
            </div>
            <div class="clear space"></div>
        </div>
        <div class="container">
            <div class="utilitario">
                <asp:Label ID="lblDireccionIP" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <img alt="Utilitarios" src="../../Images/iconUtilitarios.jpg" valign="bottom" />
                <asp:LinkButton ID="hlkCambio" NavigateUrl="#" runat="server" onclick="hlkCambio_Click">
                        <asp:Literal runat="Server" ID ="CambiarContraseña" Text="<%$  Resources:Resource,Cambiar_Contraseña%>" />
                </asp:LinkButton> | 
                <asp:LinkButton ID="hlkSeguridad" NavigateUrl="#" runat="server" onclick="hlkSeguridad_Click" Visible="False">
                        <asp:Literal runat="Server" ID ="Seguridad" Text="<%$  Resources:Resource,Seguridad%>" />
                </asp:LinkButton>
            </div>
        </div>
        <footer>
            ©2014 Expinn Technology
        </footer>
    </form>

</body>
</html>
   