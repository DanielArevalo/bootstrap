<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - BienesRaices :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Cod_bien&nbsp;*&nbsp;<br />
                       <asp:TextBox ID="txtCod_bien" runat="server" CssClass="textbox" MaxLength="128" 
                               Enabled="False" />
                       </td>
                       <td class="tdD">
                       Cod_persona&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCOD_PERSONA" runat="server" ControlToValidate="txtCod_persona" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCod_persona" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipo&nbsp;&nbsp;<asp:CompareValidator ID="cvTIPO" runat="server" ControlToValidate="txtTipo" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtTipo" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Matricula&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtMatricula" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valorcomercial&nbsp;&nbsp;<asp:CompareValidator ID="cvVALORCOMERCIAL" runat="server" ControlToValidate="txtValorcomercial" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtValorcomercial" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Valorhipoteca&nbsp;&nbsp;<asp:CompareValidator ID="cvVALORHIPOTECA" runat="server" ControlToValidate="txtValorhipoteca" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtValorhipoteca" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
    </table>
   <%-- <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_BIEN').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>