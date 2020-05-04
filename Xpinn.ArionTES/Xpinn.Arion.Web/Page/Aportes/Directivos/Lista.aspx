<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Directivos :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="90%">
        <tr>
            <td>
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 25%">Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="300" Width="70%" />
                        </td>
                        <td style="width: 25%">Estado<br />
                            <asp:DropDownList ID="ddlEstado" runat="server" AppendDataBoundItems="true" Width="70%">
                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                                <asp:ListItem Text="Nombrado" Value="0" />
                                <asp:ListItem Text="Retirado" Value="1" />
                                <asp:ListItem Text="Excluido" Value="2" />
                            </asp:DropDownList>
                        </td>
                        <td style="width: 25%">Tipo Directivo<br />
                            <asp:DropDownList ID="ddlTipoDirectivo" runat="server" AppendDataBoundItems="true" Width="70%">
                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                            </asp:DropDownList>
                        </td>
                        <td style="width: 25%">Calidad<br />
                            <asp:DropDownList ID="ddlCalidad" runat="server" AppendDataBoundItems="true" Width="70%">
                                <asp:ListItem Text="Seleccione un Item" Value=" " />
                                <asp:ListItem Text="Principal" Value="0" />
                                <asp:ListItem Text="Suplente" Value="1" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" AllowPaging="True"
                    AutoGenerateColumns="False" GridLines="Horizontal"
                    PageSize="20" HorizontalAlign="Center"
                    ShowHeaderWhenEmpty="True" Width="100%"
                    OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    DataKeyNames="consecutivo"
                    OnRowCommand="OnRowCommandDeleting" OnRowDeleting="gvLista_RowDeleting"
                    Style="font-size: x-small">
                    <Columns>
                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True">
                            <ItemStyle Width="16px" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select"
                                    ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="desc_tipo_directivo" HeaderText="Tipo Directivo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="desc_calidad" HeaderText="Calidad" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="desc_estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="vigencia_inicio" HeaderText="Vigencia Inicio" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="vigencia_final" HeaderText="Vigencia Final" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="fecha_nombramiento" HeaderText="Fecha Nombramiento"  DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" />

            </td>
        </tr>
    </table>
    <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
</asp:Content>
