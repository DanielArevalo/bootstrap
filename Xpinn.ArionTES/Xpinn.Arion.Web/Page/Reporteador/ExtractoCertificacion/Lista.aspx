<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Page_Asesores_Colocacion_Lista" Title=".: Xpinn - Asesores :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%--<%*@ Register Assembly="BarcodeX" Namespace="Fath" TagPrefix="bcx" /*%>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphMain">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table style="width: 80%;">
                    <tr>
                        <td colspan="3" style="text-align: left">
                            <strong>Criterios de Búsqueda</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">Fecha Corte
                        <br />
                            <asp:DropDownList ID="ddlFechaCorte" runat="server" CssClass="dropdown" Height="30px" Width="158px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left"></td>
                        <td style="width: 50%; text-align: left" colspan="2">Fecha Afiliacion
                            <br />
                            <ucFecha:fecha ID="txtFechaDetaIni" runat="server" />
                            a
                        <ucFecha:fecha ID="txtFechaDetaFin" runat="server" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="2">Código<br />
                            <asp:TextBox ID="txtCodigoDesde" runat="server" CssClass="textbox" Width="130px" />
                            <asp:FilteredTextBoxExtender ID="ftb1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                TargetControlID="txtCodigoDesde" ValidChars="" />
                            &nbsp;a&nbsp;
                        <asp:TextBox ID="txtCodigoHasta" runat="server" CssClass="textbox" Width="130px" />
                            <asp:FilteredTextBoxExtender ID="ftb2" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                TargetControlID="txtCodigoHasta" ValidChars="" />
                        </td>
                        <td style="text-align: left;" colspan="2">Identificación<br />
                            <asp:TextBox ID="txtIdentDesde" runat="server" CssClass="textbox" Width="130px" />&nbsp;a&nbsp;
                        <asp:TextBox ID="txtIdentHasta" runat="server" CssClass="textbox" Width="130px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="2">Nombres<br />
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="88%" />
                        </td>
                        <td style="text-align: left;" colspan="2">Apellidos<br />
                            <asp:TextBox ID="txtApellidos" runat="server" CssClass="textbox" Width="88%" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwListado" runat="server">
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <strong>Lista de Clientes a generar Extracto:</strong>
                        <asp:GridView ID="gvListaClientesExtracto" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                            OnRowEditing="gvListaClientesExtracto_RowEditing"
                            RowStyle-CssClass="gridItem" Width="100%" Height="16px" RowStyle-Font-Size="Small"
                            OnPageIndexChanging="gvListaClientesExtracto_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cod_persona" HeaderText="Código" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="identificacion" HeaderText="Identificación" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="tipo_identificacion_descripcion" HeaderText="Tipo Identificacion" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                                <asp:BoundField DataField="email" HeaderText="E-Mail" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" />
                                <asp:BoundField DataField="fecha_afiliacion" HeaderText="Fecha Afiliacion" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="fecha_retiro" HeaderText="Fecha Retiro" DataFormatString="{0:d}" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblTotalRegs" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vReporteExtracto" runat="server">
            <table>
                <tr>
                    <td style="width: 100%">
                        <br />
                        <br />
                        <rsweb:ReportViewer ID="rvExtracto" runat="server" Width="100%" Font-Names="Verdana"
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Height="500px" AsyncRendering="false">
                            <LocalReport ReportPath="Page\Reporteador\ExtractoCertificacion\ReportExtracto.rdlc">

                            </LocalReport>
                        </rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="hdFileName" runat="server" />
    <asp:HiddenField ID="hdFileNameThumb" runat="server" />
    <%--<asp:ButtonField CommandName="Seleccionar" Text="Seleccionar" />--%>
    <br />
</asp:Content>
