<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" 
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server"> 
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <table>
                <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="6" >
                <strong>
                    Criterios de Búsqueda:</strong></td>
            </tr>
        </table>
            <Table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 5%">Consecutivo<br />
                            <asp:TextBox ID="txtconsecutivo" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="300" Width="85%" />
                        </td>
                        <td style="width: 5%">Descripción<br />
                            <asp:TextBox ID="txtdescripcion" runat="server" onkeypress="return isString(event)" CssClass="textbox" MaxLength="300" Width="85%" />
                        </td>
                        <td style="width: 5%">Ciudad<br />
                            <asp:DropDownList ID="ddlciudad" CssClass="textbox" runat="server" onkeypress="return isString(event)" MaxLength="300" Width="85%" />
                        </td>
                        <td style="width: 5%">Dirección<br />
                            <asp:TextBox ID="txtdir" runat="server" onkeypress="return isString(event)" CssClass="textbox" MaxLength="300" Width="85%" />
                        </td>
                        <td style="width: 5%">Teléfono<br />
                            <asp:TextBox ID="txttel" runat="server" onkeypress="return isNumber(event)" CssClass="textbox" MaxLength="300" Width="85%" />
                        </td>
        </Table>

        <asp:GridView ID="gvContent" runat="server" 
                        AllowPaging="True"
                        AutoGenerateColumns="False" 
                        GridLines="Horizontal"
                        PageSize="20" 
                        HorizontalAlign="Center"
                        ShowHeaderWhenEmpty="True" 
                        Width="100%"
                        DataKeyNames="consecutivo"
                        OnRowEditing="gvLista_RowEditing"
                        OnRowDeleting="gvLista_RowDeleting"
                        OnRowDataBound="gvLista_RowDataBound" 
                        OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                        Style="font-size: x-small" >
                     <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnElim" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="consecutivo" HeaderText="Código" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                 <HeaderStyle CssClass="gridHeader" />
                 <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                 <RowStyle CssClass="gridItem" />
        </asp:GridView>
        <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
     <asp:Label ID="lblTotalRegs" runat="server" />
</asp:Content>

