<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - MotivosCambio :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdD">
                       Descripción&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" />
                           <br />
                       <asp:RequiredFieldValidator ID="descripcion" runat="server" 
                               ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" 
                               SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" 
                               Display="Dynamic"/>
                       </td>
                   </tr>
    </table>
    <%--<script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtcod_motivo_cambio').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>