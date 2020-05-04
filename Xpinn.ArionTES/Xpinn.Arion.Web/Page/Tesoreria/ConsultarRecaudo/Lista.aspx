﻿<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista"  %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>    
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="80%">
                   <tr>
                       <td class="tdD" style="width: 238px; text-align: left">
                           Número Recaudo<br/>
                           <asp:TextBox ID="txtNumeroRecaudo" runat="server" CssClass="textbox" />
                       </td>
                       <td class="tdD" style="text-align:left">
                           Fecha Recaudo<br/>
                           <ucfecha:fecha ID="txtFechaRecaudo" runat="server" />
                       </td>
                       <td class="tdD" style="text-align:left">
                           Empresa<br/>
                           <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="dropdown" 
                               Width="208px" />
                       </td>
                       <td class="tdD" style="text-align:left">
                           Estado<br/>
                           <asp:DropDownList ID="ddlEstado" runat="server" CssClass="dropdown" 
                               Width="104px">
                           <asp:ListItem Text="Todos" Value="" Selected="True"/>
                           <asp:ListItem Text="1 - Pendiente" Value="1"/>
                           <asp:ListItem Text="2 - Aplicado" Value="2"/>
                           <asp:ListItem Text="3 - Anulado" Value="3"/>
                           </asp:DropDownList>
                       </td>
                       <td class="tdD">
                           &nbsp;
                       </td>
                   </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td><hr /></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" onrowediting="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  DataKeyNames="numero_recaudo" >
                    <Columns>
                        <asp:BoundField DataField="numero_recaudo" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo" >
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gridIco"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco"></HeaderStyle>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="numero_recaudo" HeaderText="No.Recaudo" />
                        <asp:BoundField DataField="nom_tipo_recaudo" HeaderText="Tipo Recaudo" />
                        <asp:BoundField DataField="nom_empresa" HeaderText="Empresa" />
                        <asp:BoundField DataField="periodo_corte" HeaderText="Período" 
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="fecha_recaudo" HeaderText="Fecha Recaudo" 
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="fecha_aplicacion" HeaderText="Fecha Aplicacion" 
                            DataFormatString="{0:d}" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                        <asp:BoundField DataField="nom_estado" HeaderText="Desc.Estado" />
                        <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>                
            </td>
        </tr>
    </table>
    <asp:Label ID="lblTotalRegs" runat="server" Visible="True"/>
</asp:Content>