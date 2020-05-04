<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - consultasdatacredito :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Numerofactura&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvNUMEROFACTURA" runat="server" ControlToValidate="txtNumerofactura" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvNUMEROFACTURA" runat="server" ControlToValidate="txtNumerofactura" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtNumerofactura" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Fechaconsulta&nbsp;&nbsp;<asp:CompareValidator ID="cvFECHACONSULTA" runat="server" ControlToValidate="txtFechaconsulta" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtFechaconsulta" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Cedulacliente&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtCedulacliente" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Usuario&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtUsuario" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Ip&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtIp" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Oficina&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valorconsulta&nbsp;&nbsp;<asp:CompareValidator ID="cvVALORCONSULTA" runat="server" ControlToValidate="txtValorconsulta" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtValorconsulta" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtNUMEROFACTURA').focus(); 
        }
        window.onload = SetFocus;
    </script>
</asp:Content>