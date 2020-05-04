<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Detalle" Title=".: Xpinn - Usuario :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <script>
        $(document).keydown(function (event) {
            if (event.keyCode == 123) { // Prevent F12
                return false;
            } else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) { // Prevent Ctrl+Shift+I        
                return false;
            }
        });
    </script>

    <script language=JavaScript>  

        function inhabilitar(){ 
   	        alert ("Esta función está inhabilitada.\n\nPerdonen las molestias.") 
   	        return false 
        } 

        document.oncontextmenu=inhabilitar 

    </script>
    <table border="0" cellpadding="5" cellspacing="0" width="70%" >
        <tr>
            <td class="tdI" style="text-align:left">
                Usuario&nbsp;*<br />
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Enabled="false"/>
            </td>
            <td class="tdI"  style="text-align:left; display:none">
                Identificación&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtIdentdoc" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
            <td class="tdI" style="text-align:left">                
                Código&nbsp;<br />
                <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" Enabled="false"/>
            </td>
        </tr>    
        <tr>
            <td class="tdI" style="text-align:left">
                Nombre&nbsp;*<br />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="false" Width="400px"/>
            </td>
            <td class="tdD" style="text-align:left">
                Estado&nbsp;*<br />
                <asp:DropDownList ID="txtEstado" runat="server" CssClass="dropdown"  Enabled="false">
                    <asp:ListItem Text="Inactivo" Value="0"/>
                    <asp:ListItem Text="Activo" Value="1"/>
                    <asp:ListItem Text="Bloqueado" Value="2"/>
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Direccion&nbsp;<br />
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="textbox" Enabled="false" Width="400px"/>
            </td>
            <td class="tdD" style="text-align:left">
                Telefono&nbsp;<br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Enabled="false"/>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Clave&nbsp;*<br />
                <asp:TextBox ID="txtLogin" runat="server" CssClass="textbox" Enabled="false">**********</asp:TextBox>
            </td>
            <td class="tdD" style="text-align:left">
                Fecha de Creacion&nbsp;*<br />
                <asp:TextBox ID="txtFechacreacion" runat="server" CssClass="textbox" Enabled="false"/>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="text-align:left">
                Perfil&nbsp;*<br />
                <asp:DropDownList ID="txtCodperfil" runat="server" CssClass="dropdown" Enabled="false"/>
            </td>
            <td class="tdD" style="text-align:left">
                Oficina&nbsp;*<br />
                <asp:DropDownList ID="txtCod_oficina" runat="server" CssClass="dropdown" Enabled="false"/>
            </td>
        </tr>
    </table>
</asp:Content>