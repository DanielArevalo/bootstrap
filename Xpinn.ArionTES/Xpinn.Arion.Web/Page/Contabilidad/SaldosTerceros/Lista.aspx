<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Usuario :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../General/Controles/fecha.ascx" tagname="fecha" tagprefix="uc" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Src="../../../General/Controles/ctlPlanCuentas.ascx" TagName="ListadoPlanCtas" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register src="../../../General/Controles/ctlMoneda.ascx" tagname="ddlMoneda" tagprefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   

    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvLista.ClientID%>').gridviewScroll({
                width: 950,
                height: 500,
                freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        } 
       
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvSaldosTer" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <br />
            <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="1" cellspacing="0" style="width: 90%">
                    <tr>
                        <td style="text-align: left">
                            Código de Cuenta
                        </td>                  
                        <td colspan="2" style="text-align: Center">
                            Periodo
                        </td>                   
                        <td style="text-align: left">
                            Centro De Costo
                        </td>                                      
                    </tr>
                    <tr>
                        <td style="text-align: left;">                        
                            <cc1:TextBoxGrid id="txtCodCuenta" runat="server" autopostback="True" cssclass="textbox"
                                style="text-align: left" backcolor="#F4F5FF" width="80px" ontextchanged="txtCodCuenta_TextChanged" />
                            <asp:Button id="btnListadoPlan" cssclass="btnListado" runat="server" text="..."
                                onclick="btnListadoPlan_Click" />
                            <uc1:ListadoPlanCtas ID="ctlListadoPlan" runat="server" />
                        </td>                    
                        <td style="text-align: left;">
                            <uc:fecha ID="txtFechaIni" runat="server" cssClass="textbox" Height="16px" />
                            <asp:Label ID="Lblerrorfechain" runat="server" Style="color: #FF0000; font-size: x-small;"></asp:Label>
                        </td>                   
                        <td style="text-align: left">
                            <uc:fecha ID="txtFechaFin" runat="server" cssClass="textbox" Height="16px" />
                            <asp:Label ID="Lblerrorfechafin" runat="server" Style="color: #FF0000; font-size: x-small;"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlcentrocosto" runat="server" CssClass="textbox" Width="180px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            Nombre de la Cuenta <br/>
                            <cc1:TextBoxGrid ID="txtNomCuenta" runat="server" Style="text-align: left" BackColor="#F4F5FF"
                                Width="180px" Enabled="False" CssClass="textbox" />
                        </td>
                        <td style="text-align: left;" colspan="2">
                            Moneda <br/>
                            <ctl:ddlMoneda ID="ddlMoneda" runat="server" CssClass="dropdown" Width="151px" />
                        </td>
                        <td style="text-align: left;">                        
                            <asp:CheckBox ID="chkConsolidado" runat="server" AutoPostBack="True" OnCheckedChanged="chkConsolidado_CheckedChanged"
                                Style="font-size: x-small" Text="Consolidado" Width="100px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table border="0" cellpadding="0" cellspacing="0"  width="95%" >
                <tr>
                    <td class="tdI">                                    
                        <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                            OnClick="btnInforme_Click" OnClientClick="btnInforme_Click" 
                            Text="Visualizar informe" />
                        &nbsp;
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                            onclick="btnExportar_Click" Text="Exportar a Excel" />
                        <br/>
                        <asp:Label ID="Lblerror" runat="server" style="color: #FF0000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><hr style="width: 100%; text-align: left" /></td>
                </tr>
                <tr>
                    <td>                        
                        <asp:GridView ID="gvLista" runat="server" 
                            AutoGenerateColumns="False" AllowPaging="False" 
                            OnPageIndexChanging="gvLista_PageIndexChanging" PageSize="20" 
                            HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" 
                            RowStyle-CssClass="gridItem" style="font-size: xx-small" Width="95%" >
                            <Columns>                        
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Cod.Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrecuenta" HeaderText="Nom. Cuenta" >
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codtercero" HeaderText="Cod.Tercero" >
                                    <ItemStyle HorizontalAlign="Left"   Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identercero" HeaderText="Iden.Tercero">
                                    <ItemStyle HorizontalAlign="Left"   Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombretercero" HeaderText="Nom. Tercero">
                                    <ItemStyle HorizontalAlign="Left" Width="180px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="centro_costo" HeaderText="C/C" >
                                    <ItemStyle HorizontalAlign="Left"  Width="40px" />
                                </asp:BoundField>              
                                <asp:BoundField DataField="saldoinicial" DataFormatString="{0:c}" HeaderText="Saldo Inicial">
                                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="debito" DataFormatString="{0:c}" HeaderText="Debitos">
                                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="credito" DataFormatString="{0:c}" HeaderText="Creditos">
                                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldofinal" DataFormatString="{0:c}" HeaderText="Saldo Final">
                                    <ItemStyle HorizontalAlign="Right" Width="120px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" 
                            style="font-size: xx-small"/>
                        <span style="font-size: xx-small">
                        <asp:Label ID="lblInfo" runat="server" 
                            Text="Su consulta no obtuvo ningún resultado." Visible="False" />
                        &nbsp; </span>
                        </td>
                </tr>
            </table>
        </asp:View>                  
        <asp:View ID="vwReporte" runat="server">
            <br /><br />
            <hr width="100%" />
            &nbsp;
            <asp:Button ID="btnDatos" runat="server" CssClass="btn8" 
                onclick="btnDatos_Click" Text="Visualizar Datos" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnImprime" runat="server" CssClass="btn8"  Width="130px"
                OnClick="btnImprime_Click" Text="Imprimir"  />
            <rsweb:ReportViewer id="RptReporte" runat="server" Width="95%" Height="550px"><localreport reportpath="Page\Contabilidad\SaldosTerceros\RptSaldosTer.rdlc"></localreport></rsweb:ReportViewer>         
            <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx"
                height="600px" runat="server" style="border-style: dotted; float: left;"></iframe>            
        </asp:View>
    </asp:MultiView> 
   
</asp:Content>