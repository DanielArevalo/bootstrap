<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Confirmación :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="numero" TagPrefix="ucnumero" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../../Scripts/PCLBryan.js"></script>
    <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwData" runat="server">
            <asp:Panel ID="pConsulta" runat="server">
                <table style="width: 80%;">
                    <tr>
                        <td style="text-align: left; font-size: x-small" colspan="5">
                            <strong>Criterios de Búsqueda</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">Destino<br />
                            <asp:DropDownList ID="ddlTipoSol" runat="server" CssClass="textbox">
                                <asp:ListItem Text="Todos" Value="0" />
                                <asp:ListItem Text="Aportes" Value="1" />
                                <asp:ListItem Text="Créditos" Value="2" />
                                <asp:ListItem Text="Ahorros a la vista" Value="3" />
                                <asp:ListItem Text="Servicios" Value="4" />
                                <asp:ListItem Text="Ahorro programado" Value="9" />
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">Origen<br />
                            <asp:DropDownList ID="ddlOrigen" runat="server" CssClass="textbox">
                                <asp:ListItem Text="Todos" Value="2" />
                                <asp:ListItem Text="Aportes" Value="1" />
                                <asp:ListItem Text="Ahorros a la vista" Value="3" />
                                <asp:ListItem Text="PSE" Value="0" />
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Width="110px" />
                        </td>
                        <td style="text-align: left">Nombres<br />
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="200px" />
                        </td>
                        </tr>
                        <tr>
                        <td style="text-align: left">Fecha inicio<br />
                            <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td style="text-align: left">Fecha fin<br />
                            <asp:TextBox ID="txtFechaFin" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td style="text-align: left">Zonas<br />
                            <asp:DropDownList ID="ddlZona" runat="server" CssClass="textbox">                                
                            </asp:DropDownList>
                        </td>
                        <asp:Panel runat="server" ID="pnlAse" Visible="false">
                            <td style="text-align: left">Asesor<br />
                                <asp:DropDownList ID="ddlAsesores" runat="server" CssClass="textbox">                                
                                </asp:DropDownList>
                            </td>
                        </asp:Panel>
                        <td style="text-align: left;display: flex;align-items: center;width: 150px;">&nbsp<br />
                            <br /><br />
                            <asp:CheckBox runat="server" ID="chkSinAsesor" Text="Incluir sin asesor" />
                        </td>              
                    </tr>
                    <tr>
                         <td style="font-size: x-small; text-align: left">
                             <asp:Button ID="btnExportar" runat="server" CssClass="btn8" onclick="btnExportar_Click" Text="Exportar a excel" />
                        </td>
                    </tr>

                </table>
                <hr style="width: 100%" />
            </asp:Panel>
            <asp:Panel ID="pDatos" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" Style="font-size: x-small"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="id">
                                <Columns>                                                              
                                    <asp:BoundField HeaderText="ID" DataField="id" />
                                    <asp:BoundField HeaderText="Cod persona" DataField="cod_persona" />
                                    <asp:BoundField HeaderText="Nombre" DataField="nombres" />
                                    <asp:BoundField HeaderText="Identificación" DataField="identificacion" />
                                    <asp:BoundField HeaderText="Producto" DataField="nom_tipo_destino" />
                                    <asp:BoundField HeaderText="Linea" DataField="nom_linea_destino" />
                                    <asp:BoundField HeaderText="Referencia" DataField="destino_id_producto" />
                                    <asp:BoundField HeaderText="Valor" DataField="valor" DataFormatString="{0:c}" />
                                    <asp:BoundField HeaderText="Fecha" DataField="fecha" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Origen" DataField="nom_tipo_origen" />
                                    <asp:BoundField HeaderText="Linea Origen" DataField="nom_linea_origen" />
                                    <asp:BoundField HeaderText="Número" DataField="origen_id_producto" />
                                    <asp:BoundField HeaderText="Asesor" DataField="asesor" />                                 
                                </Columns>
                                <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                <PagerStyle CssClass="gridPager"></PagerStyle>
                                <RowStyle CssClass="gridItem"></RowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <center>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Visible="False" Text="Su consulta no obtuvo ningún resultado." />
            </center>
        </asp:View>
        <asp:View ID="mvFinal" runat="server">
            <table style="width: 100%; text-align: center">
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;"></td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <asp:Label ID="lblMensajeGrabar" runat="server" Text="Registros modificados correctamente"></asp:Label><br />

                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; font-size: large;">
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>    
</asp:Content>

