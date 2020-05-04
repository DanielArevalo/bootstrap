<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    
    <table style="width: 100%">    
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                Nombre Meta                
                <br />
                <asp:TextBox ID="txtNombMeta" runat="server" CssClass="textbox"></asp:TextBox>
            </td>
            <td>
                Formato Meta
                <asp:DropDownList ID="ddlformatoMeta" runat="server" Width="50px">
                    <asp:ListItem Value="1">#</asp:ListItem>
                    <asp:ListItem Value="2">%</asp:ListItem>
                    <asp:ListItem Value="3">$</asp:ListItem>
                </asp:DropDownList>
                <br />
            </td>
        </tr>     
    </table>
</asp:Content>
