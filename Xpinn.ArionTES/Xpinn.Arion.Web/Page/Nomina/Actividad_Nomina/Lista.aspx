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
                        <td>Consecutivo<br/>
                            <asp:TextBox ID="txtconsecutivo" runat="server" onkeypress="return isNumber(event)" CssClass="textbox"/>
                        </td>
                        <td>Nombre De Actividad<br />
                            <asp:TextBox ID="txtactividad" runat="server" onkeypress="return isString(event)" CssClass="textbox" />
                        </td>
                        <td>Centro De Costo<br />
                            <asp:TextBox ID="txtcentrocosto" runat="server" onkeypress="return isString(event)" CssClass="textbox"  />
                        </td>
                        <td>Responsable<br />
                            <asp:TextBox ID="ctlPersona" runat="server" onkeypress="return isString(event)" CssClass="textbox"  />
                        </td>
                        </tr>          
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
                        Style="font-size: x-small" 
                        OnRowDataBound="gvLista_RowDataBound" 
                        OnSelectedIndexChanged="gvLista_SelectedIndexChanged" >
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
                        <asp:BoundField DataField="consecutivo" HeaderText="consecutivo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="nombre_actividad" HeaderText="Nombre de actividad" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="objetivo" HeaderText="Objetivo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha De Inicio" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="fecha_terminacion" HeaderText="Fecha De Terminacion" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="cod_persona" HeaderText="Responsable" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="centro_costo" HeaderText="Centro De Costo"  ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                 <HeaderStyle CssClass="gridHeader" />
                 <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                 <RowStyle CssClass="gridItem" />
        </asp:GridView>
        <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
     <asp:Label ID="lblTotalRegs" runat="server" />
</asp:Content>
