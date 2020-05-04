<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas"
    TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <style type="text/css">
        
        .style1
        {
            width: 100%;
        }
        
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:MultiView ActiveViewIndex="0" runat="server" ID="multPrincipal">
        <asp:View runat="server" ID="multivGrid">
            <asp:Panel ID="panelLibreta" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10px">
                            Numero Cuenta
                            <asp:TextBox ID="txtNumeroCuenta" CssClass="textbox" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 10px">
                            Fecha Apertura
                            <ucFecha:fecha runat="server" ID="txtFechaAperturaBus" />
                        </td>
                        <td style="width: 10px">
                            Linea
                            <asp:DropDownList ID="ddlLinea" ClientIDMode="Static" CssClass="textbox" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10px">
                            Identificacion
                            <asp:TextBox ID="txtIdentificacion" CssClass="textbox" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 10px">
                            Nombre
                            <asp:TextBox ID="txtNombre" CssClass="textbox" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 10px">
                            Oficina
                            <asp:DropDownList ID="ddlOficina" CssClass="textbox" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table style="width: 100%">
                <tr>
                    <td>
                        Listados De Libretas
                        <asp:GridView ID="gvDetalle" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="numero_cuenta"
                            Style="font-size: xx-small"  OnRowEditing="gvDetalle_RowEditing"> <%-- onselectedindexchanged="gvDetalle_SelectedIndexChanged"--%>
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Editar" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="numero_cuenta" HeaderText="No.Cuenta">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_asignacion" HeaderText="Fech. Asignacion" DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Linea" HeaderText="Linea">
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="Identificacion">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estado" HeaderText="Estado">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                            Visible="False" />
                    </td>
                </tr>
            </table>
       </asp:View>
            <asp:View runat="server" ID="mtviCrear">
                <asp:Panel ID="panelInfor" runat="server">
                    <table style="width: 100%; height: 92px;">
                        <tr>
                            <%--                <td>--%>
                            <td style="text-align: left">
                                Fecha Asignacion:
                                <ucFecha:fecha ID="txtfechaAsig2" runat="server" Enabled="false"/>
                            </td>
                            <caption>
                                <br />
                                <hr width="100%" />
                                <table class="style1">
                                    <tr>
                                        <td style="text-align: left">
                                            NumeroCuenta
                                            <br />
                                            <asp:TextBox ID="txtNunCuentaG" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            Fecha Apertura
                                            <br />
                                            <ucFecha:fecha ID="txtFechaApeG" Enabled="false" runat="server" />
                                        </td>
                                        <td class="style2" style="text-align: left">
                                            Linea
                                            <br />
                                            <asp:TextBox ID="TxtLineaG" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            Saldo Total
                                            <br />
                                            <asp:TextBox ID="txtSaldoTotalG" runat="server" CssClass="textbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            Identificacion
                                            <br />
                                            <asp:TextBox ID="txtIdentifG" runat="server" CssClass="textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            Tipo Identificacion
                                            <br />
                                            <asp:TextBox ID="txtTipoIdenG" runat="server" CssClass="textbox"></asp:TextBox>
                                        </td>
                                        <td class="style2" style="text-align: left">
                                            Nombre
                                            <br />
                                            <asp:TextBox ID="txtNombreG" runat="server" CssClass="textbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            Libreta Anterior
                                            <br />
                                            <asp:TextBox ID="txtLibretaAnG" Enabled="false" runat="server" CssClass="textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            Desprendible
                                            <br />
                                            <asp:TextBox ID="txtDesprendible" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                        </td>
                                        <td class="style2" style="text-align: left">
                                            a
                                            <asp:TextBox ID="txtDesGa" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            Estado Libreta
                                            <br />
                                              <asp:DropDownList ID="ddlEstado" runat="server" CssClass="textbox" 
                                                            DataSource="<%#ListaEstadosLibreta() %>" DataTextField="descripcion" 
                                                            DataValueField="codigo" Enabled="false" 
                                                           Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                    </asp:Panel>
                                <asp:Panel ID="panelnueva" runat="server"> 
                                <table class="style1">
                                    <caption title="Datos de la libreta">
                                        <hr width="100%" />
                                        <tr>
                                            <td style="text-align: left; width: 1px">
                                                Numero Libreta
                                                <br />
                                                <asp:TextBox ID="txtNumeLibreta1" runat="server" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 1px">
                                                Desprendibles
                                                <br />
                                                <asp:TextBox ID="txtDesprendible2" runat="server" CssClass="textbox" 
                                                    ontextchanged="txtDesprendible2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 0px">
                                                a
                                                <asp:TextBox ID="txtaDes" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 1px">
                                                Valor Libreta
                                                <br />
                                                <asp:TextBox ID="txtValorLibreta" runat="server" Enabled="false" CssClass="textbox"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 1px">
                                                Motivo Asignacion
                                                <br />
                                                <asp:DropDownList ID="ddlMotivo" runat="server" CssClass="textbox" 
                                                    onselectedindexchanged="ddlMotivo_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </caption>
                                </table>
                           </asp:Panel>
                                <%-- <br />--%>
                                </td>
                            </caption>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
        <asp:View ID="vwFinal" runat="server">
            <asp:Panel ID="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large; height: 34px;">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large; color: Red">
                            La libreta&nbsp; fue
                            <asp:Label ID="lblmsj" runat="server"></asp:Label>
                            &nbsp;correctamente.
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
