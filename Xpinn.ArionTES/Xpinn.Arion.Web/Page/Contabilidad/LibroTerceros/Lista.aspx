<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Libro Terceros :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentasNif.ascx" TagName="ListadoPlanCtasNiif" TagPrefix="uc2" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlMoneda.ascx" TagName="ddlMoneda" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvLibroTerceros" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="0" cellspacing="0" width="780px">
                    <tr>
                        <td class="tdI" style="text-align: left;width:200px">
                            Centro de Costo<br />
                            <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="dropdown" Width="190px"
                                Height="25px">
                            </asp:DropDownList>
                        </td>
                        <td class="tdD" style="text-align: left;width:200px">
                            &nbsp;
                        </td>
                        <td class="tdD" style="text-align: left;width:140px">
                            &nbsp;
                        </td>
                        <td class="tdD" style="text-align: left;width:140px">
                            &nbsp;
                        </td>                        
                    </tr>
                    <tr>
                        <td class="tdD" style="text-align: left">
                            <asp:Label ID="lblCuentaContable" runat="server" Text="Cuenta Contable"></asp:Label><br />                            
                            <cc1:TextBoxGrid ID="txtCodCuenta" runat="server" AutoPostBack="True" CssClass="textbox"
                                Style="text-align: left" BackColor="#F4F5FF" Width="80px"
                                OnTextChanged="txtCodCuenta_TextChanged"></cc1:TextBoxGrid>
                            <cc1:ButtonGrid ID="btnListadoPlan" CssClass="btnListado" runat="server" Text="..."
                                OnClick="btnListadoPlan_Click" /><br />
                            <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                            <uc2:ListadoPlanCtasNiif ID="listPlanCtasNIIF" runat="server" />
                            <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF" 
                                Width="200px" Enabled="False" CssClass="textbox"></cc1:TextBoxGrid>
                        </td>
                        <td class="tdD" style="text-align: left">                          
                            <asp:Label ID="lblCuentaContableFin" runat="server" Text="Hasta Cuenta Contable"></asp:Label><br />                            
                                <cc1:TextBoxGrid ID="txtCodCuentaFin" runat="server" AutoPostBack="True" CssClass="textbox"
                                Style="text-align: left" BackColor="#F4F5FF" Width="80px"
                                OnTextChanged="txtCodCuentaFin_TextChanged"></cc1:TextBoxGrid>
                            <cc1:ButtonGrid ID="btnListadoPlan1" CssClass="btnListado" runat="server" Text="..."
                                OnClick="btnListadoPlan1_Click" /><br />
                            <uc1:ListadoPlanCtas ID="ListadoPlanCtas1" runat="server" />
                            <uc2:ListadoPlanCtasNiif ID="listPlanCtasNIIF2" runat="server" />
                            <cc1:TextBoxGrid ID="txtNomCuenta1" runat="server" Style="text-align: left" BackColor="#F4F5FF" 
                                Width="200px" Enabled="False" CssClass="textbox"></cc1:TextBoxGrid>                          
                        </td>
                        <td class="tdD" style="text-align: left;width:250px">
                            Organizar por:<br />
                            <asp:CheckBox ID="cbCuenta" runat="server" Text="Por Cuenta" AutoPostBack="True"
                                OnCheckedChanged="cbCuenta_CheckedChanged" Font-Size="Small" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="cbTercero" runat="server" Text="Por Tercero" AutoPostBack="True"
                                OnCheckedChanged="cbTercero_CheckedChanged" Font-Size="Small" />
                        </td>
                        <td style="text-align: left">
                        Moneda<br />
                        <ctl:ddlMoneda ID="ddlTipoMoneda" runat="server" Width="100px"/>
                        </td>                                          
                    </tr>
                    <tr>
                        <td class="tdD" style="text-align: left">
                            <asp:Label ID="lblTercero" runat="server" Text="Identificación"></asp:Label><br />
                            <asp:TextBox ID="ddlTerceroIni" runat="server" CssClass="textbox" Width="100px"/>
                            <asp:Button ID="btnTerceroIni" CssClass="btn8" runat="server" Text="..." Height="26px"
                                OnClick="btnTerceroIni_Click" />
                            <uc1:ListadoPersonas ID="ctlBusquedaTerceroIni" runat="server" />
                        </td>
                        <td class="tdD" style="text-align: left">
                            <asp:Label ID="lblTerceroFin" runat="server" Text="Hasta Identificación"></asp:Label><br />
                            <asp:TextBox ID="ddlTerceroFin" runat="server" CssClass="textbox" Width="100px"/>
                            <asp:Button ID="btnTerceroFin" CssClass="btn8" runat="server" Text="..." Height="26px"
                                OnClick="btnTerceroFin_Click" />
                            <uc1:ListadoPersonas ID="ctlBusquedaTerceroFin" runat="server" />
                        </td>
                        <td class="tdD" colspan="2" style="width:280px; text-align: left">
                            Período<br />
                            <uc:fecha ID="txtFecIni" runat="server" CssClass="textbox" Width="85px" />
                            &nbsp;&nbsp;a&nbsp;&nbsp;
                            <uc:fecha ID="txtFecFin" runat="server" CssClass="textbox" Width="85px" />
                        </td>                        
                    </tr>
                </table>
            </asp:Panel>
            <hr width="100%" />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                            onclick="btnExportar_Click" Text="Exportar a Excel" />
                        &nbsp;
                        <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                            onclick="btnInforme_Click" Text="Visualizar Informe" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <div style="max-width: 1200px; max-height: 700px; overflow: scroll">
                            <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound"
                                OnPageIndexChanging="gvLista_PageIndexChanging" HeaderStyle-CssClass="gridHeader"
                                PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" Style="font-size: xx-small"
                                ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" />
                                    <asp:BoundField DataField="nombre_cuenta" HeaderText="Nombre Cuenta">
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo" HeaderText="Naturaleza" />
                                    <asp:BoundField DataField="codigo" HeaderText="Cod.Tercero" />
                                    <asp:BoundField DataField="identificacion" HeaderText="Ident.Tercero" />
                                    <asp:BoundField DataField="tipo_iden" HeaderText="T.Id" Visible="False" />
                                    <asp:BoundField DataField="nom_tipo_iden" HeaderText="T.Ident" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre Tercero" />
                                    <asp:BoundField DataField="tipo_persona" HeaderText="Tipo" />
                                    <asp:BoundField DataField="regimen" HeaderText="Regimen" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="num_comp" HeaderText="Num.Comp." />
                                    <asp:BoundField DataField="tipo_comp" HeaderText="Tipo Comp." />
                                    <asp:BoundField DataField="detalle" HeaderText="Detalle">
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_mov" HeaderText="Tipo Mov" />
                                    <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:c2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                                    <asp:BoundField DataField="tipo_benef" HeaderText="T.Ben." />
                                    <asp:BoundField DataField="num_sop" HeaderText="No.Sop" />
                                    <asp:BoundField DataField="centro_costo" HeaderText="C/C" />
                                        <asp:BoundField DataField="depende_de" HeaderText="Depende" />
                                </Columns>
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                            </asp:GridView>
                        </div>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwReporte" runat="server">
            <br />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <hr width="100%" />
                        &nbsp;
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8" 
                            onclick="btnDatos_Click" Text="Visualizar Datos" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvLibAux" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt" Width="100%" 
                            Height="500px">
                            <LocalReport ReportPath="Page\Contabilidad\LibroTerceros\ReporteLibroTerceros.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>                    
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>

</asp:Content>