<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - UIAF Productos :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ID="mvCuentasxPagar" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwDatos" runat="server">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table style="width: 800px;">
                        <tr>
                            <td style="font-size: x-small; text-align: left" colspan="3">
                                <strong>Filtrar por :</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px; text-align: left">
                                Código<br />
                                <asp:TextBox ID="txtIdReporte" runat="server" CssClass="textbox" Width="121px"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 140px">
                                Fecha Inicial<br />
                                <uc2:fecha id="txtFechaIni" runat="server" />
                            </td>
                            <td style="text-align: left; width: 140px">
                                Fecha Final<br />
                                <uc2:fecha id="txtFechaFin" runat="server" />
                            </td>
                            <td style="text-align: left; width: 140px">
                                <asp:CheckBox ID="chkFiltro" Text="Sin filtro de fechas" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
                <div style="text-align:left">
                     <asp:Button ID="btnExportar" runat="server" CssClass="btn8" onclick="btnExportar_Click" Text="Exportar a excel" />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnArchivo" runat="server" CssClass="btn8" onclick="btnArchivo_Click" Text="Exportar a CSV" /><br />
                </div>
                <asp:UpdatePanel ID="Panelgrilla" runat="server" RenderMode="Inline" UpdateMode="Conditional"><ContentTemplate>                
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">                        
                          <tr>
                                <td style="text-align: left; width:100%"> 
                                    <div style="overflow: scroll;width:100%">
                                        <asp:GridView ID="gvProductos" runat="server" AllowPaging="True" 
                                            AutoGenerateColumns="False" DataKeyNames="idreporteproductos" GridLines="Horizontal" 
                                            HeaderStyle-CssClass="gridHeader" OnRowDataBound="gvProductos_RowDatabound"
                                            PagerStyle-CssClass="gridPager" PageSize="20" RowStyle-CssClass="gridItem" 
                                            Style="font-size: x-small" Width="100%" OnDataBound="gvProductos_DataBound">
                                            <Columns>
                                                <%--<asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" 
                                                    ShowDeleteButton="True" />--%>                                                
                                                <asp:BoundField DataField="consecutivo" HeaderText="Nro"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="idreporteproductos" HeaderText="Codigo" />
                                                <asp:BoundField DataField="numero_producto" HeaderText="Nro Producto"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="fecha_apertura" HeaderText="Fecha Vinculación" DataFormatString="{0:d}"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="tipo_producto" HeaderText="Tipo Producto"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="tipo_tran" HeaderText="Tipo transacción"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="departamento" HeaderText="Código Ciudad"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                               <asp:BoundField DataField="departamento2" HeaderText="Código Ciudad"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="tipo_identificacion1" HeaderText="Tipo Identificación Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="identificacion1" HeaderText="Identificación Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="primer_apellido1" HeaderText="Primer Apellido Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="segundo_apellido1" HeaderText="Segundo Apellido Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="primer_nombre1" HeaderText="Primer Nombre Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="segundo_nombre1" HeaderText="Segundo Nombre Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="razon_social1" HeaderText="Razón Social Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="tipo_identificacion2" HeaderText="Tipo Identificación Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="identificacion2" HeaderText="Identificación Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="primer_apellido2" HeaderText="Primer Apellido Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="segundo_apellido2" HeaderText="Segundo Apellido Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="primer_nombre2" HeaderText="Primer Nombre Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="segundo_nombre2" HeaderText="Segundo Nombre Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="razon_social2" HeaderText="Razón Social Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridPager" />
                                            <RowStyle CssClass="gridItem" />
                                        </asp:GridView>

                                        <asp:GridView ID="gvProducto_vista" runat="server" AllowPaging="false" 
                                            AutoGenerateColumns="False" GridLines="Horizontal" 
                                            HeaderStyle-CssClass="gridHeader"    OnRowDataBound="gvProducto_vista_RowDatabound"
                                            PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" 
                                            Style="font-size: x-small" Width="100%" DataKeyNames="numero_producto" 
                                            onrowdeleting="gvProducto_vista_RowDeleting">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" 
                                                    ShowDeleteButton="True" />
                                               <%-- <asp:BoundField DataField="idreporteproductos" HeaderText="ID" />--%>
                                                <asp:BoundField DataField="consecutivo" HeaderText="Nro"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="numero_producto" HeaderText="Nro Producto" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="fecha_apertura" HeaderText="Fecha Vinculación" DataFormatString="{0:d}"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="tipo_producto_vista" HeaderText="Tipo Producto"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="tipo_tran" HeaderText="Tipo transacción"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="departamento" HeaderText="Código Ciudad"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="departamento2" HeaderText="Código Ciudad"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                              <asp:BoundField DataField="tipo_identificacion1_vista" HeaderText="Tipo Identificación Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="identificacion1" HeaderText="Identificación Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="primer_apellido1" HeaderText="Primer Apellido Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="segundo_apellido1" HeaderText="Segundo Apellido Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="primer_nombre1" HeaderText="Primer Nombre Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="segundo_nombre1" HeaderText="Segundo Nombre Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="razon_social1" HeaderText="Razón Social Titular 1"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="tipo_identificacion2_vista" HeaderText="Tipo Identificación Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="identificacion2" HeaderText="Identificación Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="primer_apellido2" HeaderText="Primer Apellido Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="segundo_apellido2" HeaderText="Segundo Apellido Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="primer_nombre2" HeaderText="Primer Nombre Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="segundo_nombre2" HeaderText="Segundo Nombre Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                <asp:BoundField DataField="razon_social2" HeaderText="Razón Social Titular 2"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" />
                                            <PagerStyle CssClass="gridPager" />
                                            <RowStyle CssClass="gridItem" />
                                        </asp:GridView>
                                    </div>
                                    <asp:Label ID="lblTotalRegs1" runat="server" />
                                    <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False"/>
                                </td>
                            </tr>
                    </table>
                 </ContentTemplate></asp:UpdatePanel>
        </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large; color:Red">
                            Datos
                            <asp:Label ID="lblMsj" runat="server"></asp:Label>
                            Correctamente
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
