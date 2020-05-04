<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Desembolso Masivo :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
<asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
<asp:View ID="vwData" runat="server">

  <table style="width: 780px">
        <tr>
            <td colspan="3">
                <strong>Criterios de Búsqueda</strong>
            </td>
        </tr>
        <tr>
            <td class="logo" style="width: 120px; text-align: left">
                Código :<br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" />
            </td>
            <td style="text-align: left; width:200px">
               Estado<br />
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" Width="95%" />
            </td>       
            <td style="text-align: left; width:460px">
                Fecha Vencimiento<br />
                <ucFecha:fecha ID="txtVencimientoIni" runat="server" CssClass="textbox"/>
                &nbsp; a &nbsp;
                <ucFecha:fecha ID="txtVencimientoFin" runat="server" CssClass="textbox"/>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr style="width: 100%" />
            </td>
        </tr>
    </table>   
            
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="codigo_factura" 
                        onrowdeleting="gvLista_RowDeleting" onrowcommand="gvLista_RowCommand">
                        <Columns>                            
                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/Images/gr_imp.gif" ToolTip="Imprimir"
                                        CommandName="Imprimir" CommandArgument='<%# Container.DataItemIndex%>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            </asp:TemplateField>                          
                            <asp:BoundField DataField="codigo_factura" HeaderText="Código">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numero_factura" HeaderText="Num_Factura">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_ingreso" HeaderText="F. Ingreso" 
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_factura" HeaderText="F. Factura" 
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_radicacion" HeaderText="F. Radicación" 
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_vencimiento" HeaderText="F. Vencimiento" 
                                DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tiponom" HeaderText="Tipo">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="doc_equivalente" HeaderText="Doc. Equivalente">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField HeaderText="Valor total" DataField="valortotal" 
                                DataFormatString="{0:c}" />
                            <asp:BoundField HeaderText="Valor Neto" DataField="valorneto" 
                                DataFormatString="{0:c}" />
                            <asp:BoundField HeaderText="Maneja Anticipo" DataField="manejadscto" />
                            <asp:BoundField HeaderText="Vr. Anticipo" DataField="manejaanti" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                    <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:View>
<asp:View ID="vwReporte" runat="server">
  <asp:Button ID="btnDatos" runat="server" CssClass="btn8" Height="28px"
                onclick="btnDatos_Click" Text="Visualizar Datos" />
            <br /><br />
            <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                <tr>
                    <td>
                         <iframe id="frmPrint" name="IframeName" width="100%" src="../../Reportes/Reporte.aspx" height="500px"
                               runat="server" style="border-style: groove; float: left;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="rvCuentasXpagar" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" Enabled="false"
                            InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="10pt"
                            Width="100%">
                            <localreport reportpath="Page\Tesoreria\ReporteCuentasPorPagar\rptCuentasPorCobrar.rdlc">
                            <datasources>
                            <rsweb:ReportDataSource />
                            </datasources>
                            </localreport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
</asp:View>
</asp:MultiView>
  

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server"/>


</asp:Content>
