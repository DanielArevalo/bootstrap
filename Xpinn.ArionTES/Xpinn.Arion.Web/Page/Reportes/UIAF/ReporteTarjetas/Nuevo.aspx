<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - UIAF Tarjetas :." %>

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
                            <td style="text-align: left; width: 140px">
                                Fecha Inicial<br />
                                <uc2:fecha id="txtFechaIni" runat="server" />
                            </td>
                            <td style="text-align: left; width: 140px">
                                Fecha Final<br />
                                <uc2:fecha id="txtFechaFin" runat="server" />
                            </td>
                            <td style="text-align: left; width: 140px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
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
            <div style="overflow: scroll; height: 400px; width: 100%">                     
                <table border="0" cellpadding="0" cellspacing="0" width="100%">                        
                    <tr>
                        <td style="text-align: left; width:100%"> 
                            <div style="overflow: scroll;width:100%">
                                <asp:GridView ID="gvProductos" runat="server" AllowPaging="False" 
                                    AutoGenerateColumns="False" DataKeyNames="idreporte" GridLines="Horizontal" 
                                    HeaderStyle-CssClass="gridHeader"
                                    PagerStyle-CssClass="gridPager" PageSize="20" RowStyle-CssClass="gridItem" 
                                    Style="font-size: x-small" Width="100%" OnRowDataBound="gvProductos_RowDataBound">
                                    <Columns>                                                                                           
                                        <asp:BoundField DataField="consecutivo" HeaderText="Consecutivo">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                                                
                                        <asp:BoundField DataField="fecha_tran" HeaderText="Fecha Transacción" DataFormatString="{0:yyyy-MM-dd}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                                                
                                        <asp:BoundField DataField="valor_tran" HeaderText="Valor Transacción" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_tran" HeaderText="Tipo transacción">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="pais" HeaderText="País">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="departamento" HeaderText="Código Departamento/Municipio">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="departamento2" HeaderText="Código Departamento/Municipio">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo_tarjeta" HeaderText="Tipo Tarjeta">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="numero_tarjeta" HeaderText="Número Tarjeta" DataFormatString="{0:'}">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                                                
                                        <asp:BoundField DataField="valor_cupo" HeaderText="Valor Cupo" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="franquicia" HeaderText="Franquicia">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>                                                
                                        <asp:BoundField DataField="saldo_tarjeta" HeaderText="Saldo Tarjeta" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                            <asp:BoundField DataField="tipo_identificacion" HeaderText="Tipo Identificación del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                            <asp:BoundField DataField="identificacion" HeaderText="Nro. Identificación del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                            <asp:BoundField DataField="digito_verificacion" HeaderText="Digito Verificación" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="primer_apellido" HeaderText="1er. Apellido del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                            <asp:BoundField DataField="segundo_apellido" HeaderText="2do. Apellido del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="primer_nombre" HeaderText="1er. Nombre del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="segundo_nombre" HeaderText="Otros Nombres del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="razon_social" HeaderText="Razón Social del Titular" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
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
            </div>
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
