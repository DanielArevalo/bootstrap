<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - TiposDocumento :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Tipo_documento&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvTIPO_DOCUMENTO" runat="server" ControlToValidate="txtTipo_documento" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvTIPO_DOCUMENTO" runat="server" ControlToValidate="txtTipo_documento" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtTipo_documento" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Descripcion&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvDESCRIPCION" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                       <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipo&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtTipo" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtTIPO_DOCUMENTO').focus(); 
        }
        window.onload = SetFocus;
    </script>
</asp:Content>