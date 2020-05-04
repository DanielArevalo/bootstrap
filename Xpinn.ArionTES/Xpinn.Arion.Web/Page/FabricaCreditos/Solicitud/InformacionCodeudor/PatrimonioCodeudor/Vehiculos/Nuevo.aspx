<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Vehiculos :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Cod_vehiculo&nbsp;*&nbsp;<br />
                       <asp:TextBox ID="txtCod_vehiculo" runat="server" CssClass="textbox" MaxLength="128" 
                               Enabled="False" />
                       </td>
                       <td class="tdD">
                       Cod_persona&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCOD_PERSONA" runat="server" ControlToValidate="txtCod_persona" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCod_persona" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Marca&nbsp;&nbsp;<asp:CompareValidator ID="cvMARCA" runat="server" ControlToValidate="txtMarca" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtMarca" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Placa&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtPlaca" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Modelo&nbsp;&nbsp;<asp:CompareValidator ID="cvMODELO" runat="server" ControlToValidate="txtModelo" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtModelo" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Valorcomercial&nbsp;&nbsp;<asp:CompareValidator ID="cvVALORCOMERCIAL" runat="server" ControlToValidate="txtValorcomercial" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtValorcomercial" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valorprenda&nbsp;&nbsp;<asp:CompareValidator ID="cvVALORPRENDA" runat="server" ControlToValidate="txtValorprenda" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtValorprenda" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
    </table>
   <%-- <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_VEHICULO').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>