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
                        <td> Código <br/>
                            <asp:TextBox ID="txtconsecutivo" runat="server" onkeypress="return isNumber(event)" CssClass="textbox"/>
                        </td>
                        <td> Tipo Cotizante<br />
                                   
                            <asp:DropDownList runat="server" ID="ddlTipoCotizante" AppendDataBoundItems="true" CssClass="dropdown" Width="90%">
                            <asp:ListItem Text="Seleccione un Item" Value=" " />
                        </asp:DropDownList>

                        
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
                        Style="font-size: x-small" 
                        OnSelectedIndexChanged="gvLista_SelectedIndexChanged" >
                     <Columns>

                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="consecutivo" HeaderText="Consecutivo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                 <HeaderStyle CssClass="gridHeader" />
                 <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                 <RowStyle CssClass="gridItem" />
        </asp:GridView>
        <uc4:mensajegrabar ID="ctlMensajeBorrar" runat="server" />
     <asp:Label ID="lblTotalRegs" runat="server" />
</asp:Content>
