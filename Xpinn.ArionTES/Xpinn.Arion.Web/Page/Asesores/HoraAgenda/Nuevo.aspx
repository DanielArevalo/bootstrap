<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - AgendaHora :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Hora&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtHora" runat="server" CssClass="textbox" MaxLength="128" />
                           <br />
                           <asp:CompareValidator ID="cvHora" runat="server" ControlToValidate="txtHora" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
                       </td>
                       <td class="tdD">
                       Tipo&nbsp;&nbsp;<br />
                           <asp:DropDownList ID="ddlTipo" runat="server" CssClass="dropdown">
                               <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                               <asp:ListItem Value="1">a.m.</asp:ListItem>
                               <asp:ListItem Value="2">p.m.</asp:ListItem>
                           </asp:DropDownList>
                           <br />
                <asp:CompareValidator ID="cvTipo" runat="server" 
                    ControlToValidate="ddlTipo" Display="Dynamic" 
                    ErrorMessage="Seleccione un tipo" ForeColor="Red" 
                    Operator="GreaterThan" SetFocusOnError="true" Type="Integer" 
                    ValidationGroup="vgGuardar" ValueToCompare="0"></asp:CompareValidator>
                       </td>
                   </tr>
                   </table>
    <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtIdHora').focus(); 
        }
        window.onload = SetFocus;
    </script>
</asp:Content>