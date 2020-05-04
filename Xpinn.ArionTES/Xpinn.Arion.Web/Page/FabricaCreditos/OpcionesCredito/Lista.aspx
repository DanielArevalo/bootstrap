<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"   CodeFile="Lista.aspx.cs" Inherits="Lista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlMensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <table style="width: 100%">
        <tr>
            <td style="text-align:left; width: 118px;">                         
                <asp:Label ID="Labelerror" runat="server" 
                    style="color: #FF0000; font-weight: 700; font-size: x-small;" colspan="5" 
                    Text=""></asp:Label>
            </td>
            <td style="text-align:right; width: 7px;" colspan="2">
                &nbsp;</td>
            <td style="text-align:right" colspan="9">                
            </td>
        </tr>
        <tr>
            <td style="text-align: left" colspan="2">                
                <span style="font-weight: bold;">Datos Clasificación</span>
                &nbsp;
            </td>
            <td style="text-align: left" colspan="2">                
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>    
        <tr>
            <td style="text-align: left" colspan="2">                
                <asp:DropDownList ID="ddlCod_clasifica" align="center" runat="server" CssClass="textbox" 
                    Width="117px">
                </asp:DropDownList>
            </td>
            <td style="text-align: left" colspan="2">                
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>    
        <tr>
            <td style="width: 118px; text-align: left" colspan="12">
                <span style="font-weight: bold;">Permisos Asignados</span>
            </td>
        </tr>                    
        <tr>
            <td style="width: 118px; text-align: left">
                Modulo
            </td>
            <td style="width: 118px; text-align: left" colspan="11">
                <asp:DropDownList ID="ddlModulo" runat="server" CssClass="textbox" 
                    Width="200px" AutoPostBack="True" 
                    onselectedindexchanged="ddlModulo_SelectedIndexChanged" Enabled="False" />
            </td>
        </tr>   
        <tr>
            <td colspan="12">
                <asp:GridView ID="gvLista" runat="server" Width="50%" 
                    AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" 
                    onrowdatabound="gvLista_RowDataBound" >
                    <Columns>                    
                        <asp:TemplateField HeaderText="Opcion">
                            <ItemTemplate>
                                <asp:Label ID="lblcodigo" runat="server" Text='<%# Bind("cod_opcion") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("cod_opcion") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />   
                    <RowStyle CssClass="gridItem" />     
                </asp:GridView>
            </td>
        </tr>        
    </table>
    <ctl:mensajegrabar ID="ctlMensaje" runat="server"/>
</asp:Content>
