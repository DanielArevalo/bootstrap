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
                        <td style="text-align: left">Producto<br />
                            <asp:DropDownList ID="ddlTipoProducto" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">
                            <asp:Panel ID="Panel1" runat="server" Width="130px">
                                <asp:Label ID="lblFechaNovedad" runat="server" Text="Fecha Novedad"></asp:Label>
                                <asp:TextBox ID="txtFechaNovedad" MaxLength="10" CssClass="textbox"
                                    runat="server" Width="80px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                    PopupButtonID="Image1"
                                    TargetControlID="txtFechaNovedad"
                                    Format="dd/MM/yyyy">
                                </asp:CalendarExtender>
                                <img id="Image1" alt="Calendario"
                                    src="../../../Images/iconCalendario.png" />
                            </asp:Panel>
                        </td>
                        <td style="text-align: left">Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" onkeypress="return isNumber(event)" runat="server" CssClass="textbox" Width="110px" />
                        </td>
                        <td style="text-align: left">Nombres<br />
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Width="200px" />
                        </td>
                        <td style="text-align: left;">Código de nómina<br />
                            <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" ></asp:TextBox>
                        </td>
                        <td style="text-align: left">Estado<br />
                            <asp:DropDownList ID="ddlEstado" runat="server">
                                <asp:ListItem Text="Solicitado" Value="0" />
                                <asp:ListItem Text="Aprobado" Value="1" />
                                <asp:ListItem Text="Rechazado" Value="2" />
                                <asp:ListItem Text="Todos" Value="3" />
                            </asp:DropDownList>
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
                        <td style="font-size: x-small; text-align: left">
                            <strong>Listado de Novedades para actualizar</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>                                                                                  
                            <asp:GridView ID="gvLista" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" Style="font-size: x-small"
                                PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                RowStyle-CssClass="gridItem" DataKeyNames="id_novedad_cambio" OnRowDeleting="gvLista_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="None">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" Visible='<%# Eval("estado_modificacion").ToString().Trim() != "Solicitado" ? false:true %>' ID="imgBorrar" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                                AutoPostBack="True" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbSeleccionar" Enabled='<%# Eval("estado_modificacion").ToString().Trim() != "Solicitado" ? false:true %>' runat="server" Checked="false" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle CssClass="gridIco" HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="ID Novedad" DataField="id_novedad_cambio" />
                                    <asp:BoundField HeaderText="Fec. Novedad" DataField="fecha_novedad_cambio" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nro. Producto" DataField="numero_aporte" />
                                    <asp:BoundField HeaderText="Identificación" DataField="identificacion" />
                                    <asp:BoundField HeaderText="Nombre" DataField="nombre" />
                                    <asp:BoundField HeaderText="Código de nómina" DataField="cod_nomina" />
                                    <asp:BoundField HeaderText="Tipo Producto" DataField="cod_tipo_producto" />
                                    <asp:BoundField HeaderText="Descripción" DataField="descripcion_tipo_prod" />                                    
                                    <asp:BoundField HeaderText="Cuota Actual" DataField="cuota" DataFormatString="${0:#,##0.00}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cuota Pedida" DataField="nuevo_valor_cuota" DataFormatString="${0:#,##0.00}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Fec. Deseada Mod." DataField="fecha_empieza_cambio" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Observaciones" DataField="observaciones">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Estado Mod." DataField="estado_modificacion">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Asesor" DataField="descripcion">
                                        <ItemStyle HorizontalAlign="Left" />
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
            <center>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                <asp:Label ID="lblInfo" runat="server" Visible="False" Text="Su consulta no obtuvo ningún resultado." /></center>
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
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
    <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />

</asp:Content>

