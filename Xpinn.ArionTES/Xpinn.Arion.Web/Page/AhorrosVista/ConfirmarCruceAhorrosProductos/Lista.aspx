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
                        <td style="text-align: left">
                            <asp:Panel ID="Panel1" runat="server" Width="130px">
                                <asp:Label ID="lblFechaNovedad" runat="server" Text="Fecha Solicitud"></asp:Label>
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
                        <td style="text-align: left">Estado<br />
                            <asp:DropDownList ID="ddlEstado" runat="server">
                                <asp:ListItem Text="Solicitado" Value="0" />
                                <asp:ListItem Text="Aprobado" Value="1" />
                                <asp:ListItem Text="Rechazado" Value="2" />
                                <asp:ListItem Text="Todos" Value="3" />
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: left">Solicitudes sin asesor<br />
                            <asp:CheckBox runat="server" ID="chkSinAsesor" />
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
                                RowStyle-CssClass="gridItem" DataKeyNames="idcruceahorro" OnRowEditing="gvLista_RowEditing"
                                        OnSelectedIndexChanged="gvLista_SelectedIndexChanged" >
                                <Columns>
                                            <asp:TemplateField HeaderText="Rechazar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" Visible='<%# Eval("nom_estado").ToString().Trim() != "Solicitado" ? false:true %>' ID="imgRechazar" CommandName="Edit" ImageUrl="~/Images/gr_elim.jpg" />                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aprobar">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" Visible='<%# Eval("nom_estado").ToString().Trim() != "Solicitado" ? false:true %>' ID="imgAprobar" CommandName="Select" ImageUrl="~/Images/gr_credit.jpg" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    <asp:BoundField HeaderText="ID Solicitud" DataField="idcruceahorro" />
                                    <asp:BoundField HeaderText="Fec. Solicitud" DataField="fecha_pago" DataFormatString="{0:d}">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Prod. Destino" DataField="nom_tipo_producto" />
                                    <asp:BoundField HeaderText="Nro. Producto" DataField="num_producto" />
                                    <asp:BoundField HeaderText="Transacción" DataField="nom_tipo_tran"/>
                                    <asp:BoundField HeaderText="Cta. Origen" DataField="numero_cuenta" />                                    
                                    <asp:BoundField HeaderText="Valor" DataField="valor_pago"/>
                                    <asp:BoundField HeaderText="Identificación" DataField="identificacion" />
                                    <asp:BoundField HeaderText="Nombre" DataField="nombre" />
                                    <asp:BoundField HeaderText="Estado Sol." DataField="nom_estado">
                                        <ItemStyle HorizontalAlign="Center" />
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

