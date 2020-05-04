<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Concepto :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                           Código<br/>
                           <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" />
                       </td>
                       <td class="tdD">
                           <br/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Nombre<br />
                           <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" MaxLength="128" 
                               Width="574px" />
                           <br/>
                       </td>
                       <td class="tdD">
                           <br/>
                       </td>
                   </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>