<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn -  Programados Cuentas :." %>
    <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <asp:MultiView ID="MvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewCuentas" runat="server">
    <asp:Panel ID="pConsulta" runat="server">
        <table cellspacing="4" style="width: 763px; height: 157px">
            <tr>
                <td colspan="3" style="font-size: x-small;text-align: left">
                    <strong>Criterios de Búsqueda</strong>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Cuenta<br />
                    <asp:TextBox ID="txtCuenta" runat="server" CssClass="textbox" Width="120px" />
                </td>
                <td style="text-align: left;">
                    Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" 
                        Width="180px" />
                </td>
                <td style="text-align: left;">
                    Identificación
                    <br />
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                        Width="120px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    Línea<br />
                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="textbox" 
                        Width="220px" />
                </td>
                <td style="text-align: left;">
                    Fecha Apertura<br />
                    <ucFecha:fecha ID="txtFechaApert" runat="server" CssClass="textbox" />
                </td>
                <td style="text-align: left;">
                    Estado<br />
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" 
                        Width="160px" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <hr style="100%" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelGrilla" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left">
                    <strong>Listado de Cuentas de Ahorro Programado</strong><br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                        PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                        RowStyle-CssClass="gridItem" DataKeyNames="numero_programado" OnRowDeleting="gvLista_RowDeleting"
                        Style="font-size: x-small">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="True" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:BoundField DataField="numero_programado" HeaderText="Cuenta" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomlinea" HeaderText="Línea" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomoficina" HeaderText="Oficina" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="F. Apertura" DataFormatString="{0:d}"
                                HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nommotivo_progra" HeaderText="Motivo Apertura" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Prox Pago" DataFormatString="{0:d}"
                                HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="plazo" HeaderText="Plazo" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="nomforma_pago" HeaderText="Forma Pago" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nom_estado" HeaderText="Estado" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                        <PagerStyle CssClass="gridPager"></PagerStyle>
                        <RowStyle CssClass="gridItem"></RowStyle>
                    </asp:GridView>
                </td>
            </tr>            
        </table>
    </asp:Panel>
    
    <table width="100%">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado."
                    Visible="False" />
            </td>
        </tr>
    </table>
      </asp:View>   
      
    </asp:MultiView>
                        <asp:Button ID="btnDatos" runat="server" CssClass="btn8"  
        Height="25px" Width="130px"
                            onclick="btnDatos_Click" Text="Visualizar Datos" />
        <asp:MultiView ID="mvReporte" runat="server" ActiveViewIndex="0">
                 <asp:View ID="vwReporte" runat="server">
                     <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                         <tr>
                             <td>
                                 <rsweb:ReportViewer ID="rvReporte" runat="server" enabled="false" 
                                     font-names="Verdana" font-size="8pt" Height="500px" 
                                     interactivedeviceinfos="(Colección)" waitmessagefont-names="Verdana" 
                                     waitmessagefont-size="10pt" width="100%" CssClass="aspNetDisabled">
                                     <localreport reportpath="Page\Programado\Cuentas\RptCuentas.rdlc">
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

    <br />
    <br />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
