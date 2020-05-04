﻿<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Balance Terceros :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register src="~/General/Controles/ctlMoneda.ascx" tagname="ddlMoneda" tagprefix="ctl" %>
<%@ Register Src="~/General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   

    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: CalcularAncho(),
                height: 500,
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }

        function CalcularAncho() {
            if (navigator.platform == 'Win32') {
                return screen.width - 310;
            }
            else {
                return 1000;
            }
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvBalance" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:CheckBox ID="chkGenerar" runat="server" AutoPostBack="True" Checked="True" 
                oncheckedchanged="chkCuentasCero_CheckedChanged" 
                style="font-size: x-small; text-align: left;" Visible="False" />
            <br />
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="60%">
                    <tr >            
                        <td class="tdI" style="text-align:left" height="5">
                            Fecha de corte</td>
                        <td class="tdD" style="text-align:left" height="5">
                            <asp:Label ID="lblCentroCosto" runat="server" Text="Centro Costo"></asp:Label></td>
                      <%--  <td class="tdD" colspan="2" height="5" style="text-align:left">
                            Nivel&nbsp;</td>--%>
                        <td class="tdD" height="5">
                            <asp:Label ID="lblMoneda0" runat="server" Text="Moneda"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left">
                            <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="dropdown" 
                                Width="158px" AutoPostBack="True" 
                                onselectedindexchanged="ddlFechaCorte_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align:left">
                            <asp:DropDownList ID="ddlcentrocosto" runat="server" CssClass="dropdown" 
                                Width="168px" />
                        </td>
                       <%-- <td class="tdD" style="text-align:left">
                            <asp:DropDownList ID="ddlNivel" runat="server" CssClass="dropdown" 
                                Width="113px">
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                            </asp:DropDownList>
                        </td>--%>
                        <td class="tdD" colspan="2" style="text-align:left">
                            <ctl:ddlMoneda ID="ddlMoneda" runat="server" CssClass="dropdown" Width="151px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left" colspan="2">
                            Filtrar por:
                        </td>
                        <td class="tdI" colspan="2" style="text-align:left">
                            <table id="tbCriterios0" border="0" cellpadding="0" cellspacing="0" width="60%">
                                <tr>
                                    <td class="tdD" style="text-align:left">
                                        <asp:CheckBox ID="chkmes13" runat="server" AutoPostBack="True" oncheckedchanged="chkmes13_CheckedChanged" style="font-size: x-small" Text="Mes 13" Visible="False" />
                                        <strong>
                                        <asp:Label ID="Lblmensaje" runat="server" ForeColor="Red" style="font-size: x-small"></asp:Label>
                                        </strong></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" style="text-align:left; vertical-align: top" colspan="2" >
                            <asp:Label ID="lblCuentaContable" runat="server" Text="Cuenta Contable"></asp:Label><br />
                            <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" CssClass="textbox"
                                Style="text-align: left" BackColor="#F4F5FF" Width="80px" OnTextChanged="txtCodCuenta_TextChanged"></cc1:TextBoxGrid>
                            <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                                OnClick="btnListadoPlan_Click" />
                            <br />
                            <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                            <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                                Width="95%" Enabled="False" CssClass="textbox"></cc1:TextBoxGrid>
                        </td>
                        <td class="tdD" style="text-align:left; vertical-align: top" colspan="2">
                             <asp:Label ID="lblTercero" runat="server" Text="Identificación"></asp:Label><br />
                            <asp:TextBox ID="ddlTercero" runat="server" CssClass="textbox" Width="100px"/>
                            <asp:Button ID="btnTercero" CssClass="btn8" runat="server" Text="..." Height="26px"
                                OnClick="btnTercero_Click" />
                            <uc1:ListadoPersonas ID="ctlBusquedaTercero" runat="server" />  
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI" colspan="3">                                    
                            <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                                OnClick="btnInforme_Click" OnClientClick="btnInforme_Click" 
                                Text="Visualizar informe" />
                            &nbsp;<asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                                onclick="btnExportar_Click" Text="Exportar a Excel" />
                            <br/>
                        </td>
                        <td class="tdD">
                        </td>
                        <td class="tdD">
                        &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <table border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td>
                        <hr style="width: 100%; text-align: left" />                        
                    </td>
                </tr>
                <tr>
                    <td>                                
                        <asp:GridView ID="gvLista" runat="server" 
                            AutoGenerateColumns="False" AllowPaging="false" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="100" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" Width="80%" style="font-size: xx-small" 
                            onrowdatabound="gvLista_RowDataBound">
                            <Columns>                        
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrecuenta" HeaderText="Nombre Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="tercero" HeaderText="Tercero"  >
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="NombreTercero" HeaderText="Nombre Tercero"  >
                                    <ItemStyle HorizontalAlign="Left"  Width="350px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="SaldoFin" HeaderText="Saldo" DataFormatString="{0:c}" >
                                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                        &nbsp;
                        </td>
                </tr>
            </table>
        </asp:View>                  
        <asp:View ID="vwReporte" runat="server">  
            <br /><br />&nbsp;
            <asp:Button ID="btnDatos" runat="server" CssClass="btn8" 
                onclick="btnDatos_Click" Text="Visualizar Datos" />       
            <rsweb:ReportViewer ID="RptReporte" runat="server" Width="100%" Height="500px">
            <localreport reportpath="Page\Contabilidad\BalanceTerceros\RptbalanceTerceros.rdlc"></localreport></rsweb:ReportViewer>         
        </asp:View>
          </asp:MultiView> 
   
</asp:Content>