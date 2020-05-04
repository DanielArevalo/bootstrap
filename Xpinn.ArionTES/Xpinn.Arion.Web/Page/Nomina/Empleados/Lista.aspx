<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Empleados :." %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
    <table border="0" width="90%">
        <tr>
            <td>
                <table id="tbCriterios" border="0" width="100%">
                    <tr>
                        <td style="width: 25%">Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="300" Width="70%" />
                        </td>
                        <td style="width: 25%">Cod. Persona<br />
                            <asp:TextBox ID="txtCodPersona" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="300" Width="70%" />
                        </td>
                        <td style="width: 25%">Nombre<br />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="300" Width="70%" />
                        </td>
                        <td style="width: 25%">Oficina<br />
                            <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox" AppendDataBoundItems="true" Width="70%">
                                <asp:ListItem Text="Seleccione un Item" Value=" " />
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
                <asp:GridView ID="gvLista" 
                    runat="server" 
                    AllowPaging="True"
                    AutoGenerateColumns="False" 
                    GridLines="Horizontal"
                    PageSize="20" 
                    HorizontalAlign="Center"
                    ShowHeaderWhenEmpty="True" 
                    Width="100%"
                    OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    DataKeyNames="consecutivo"
                    OnRowCommand="OnRowCommandDeleting" 
                    OnRowDeleting="gvLista_RowDeleting"
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
                        <asp:BoundField DataField="cod_persona" HeaderText="Cod.Persona" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="profesion" HeaderText="Profesión" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="email" HeaderText="Email" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="celular" HeaderText="Celular" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="nom_oficina" HeaderText="Oficina" ItemStyle-HorizontalAlign="Center" />
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