<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - SolicitudCreditosRecogidos :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Idsolicitudrecoge&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvIDSOLICITUDRECOGE" runat="server" ControlToValidate="txtIdsolicitudrecoge" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvIDSOLICITUDRECOGE" runat="server" ControlToValidate="txtIdsolicitudrecoge" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtIdsolicitudrecoge" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Numerosolicitud&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvNUMEROSOLICITUD" runat="server" ControlToValidate="txtNumerosolicitud" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvNUMEROSOLICITUD" runat="server" ControlToValidate="txtNumerosolicitud" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtNumerosolicitud" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Numero_recoge&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvNUMERO_RECOGE" runat="server" ControlToValidate="txtNumero_recoge" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvNUMERO_RECOGE" runat="server" ControlToValidate="txtNumero_recoge" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtNumero_recoge" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Fecharecoge&nbsp;&nbsp;<asp:CompareValidator ID="cvFECHARECOGE" runat="server" ControlToValidate="txtFecharecoge" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtFecharecoge" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valorrecoge&nbsp;&nbsp;<asp:CompareValidator ID="cvVALORRECOGE" runat="server" ControlToValidate="txtValorrecoge" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtValorrecoge" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Fechapago&nbsp;&nbsp;<asp:CompareValidator ID="cvFECHAPAGO" runat="server" ControlToValidate="txtFechapago" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtFechapago" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Saldocapital&nbsp;&nbsp;<asp:CompareValidator ID="cvSALDOCAPITAL" runat="server" ControlToValidate="txtSaldocapital" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtSaldocapital" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Saldointcorr&nbsp;&nbsp;<asp:CompareValidator ID="cvSALDOINTCORR" runat="server" ControlToValidate="txtSaldointcorr" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtSaldointcorr" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Saldointmora&nbsp;&nbsp;<asp:CompareValidator ID="cvSALDOINTMORA" runat="server" ControlToValidate="txtSaldointmora" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtSaldointmora" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Saldomipyme&nbsp;&nbsp;<asp:CompareValidator ID="cvSALDOMIPYME" runat="server" ControlToValidate="txtSaldomipyme" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtSaldomipyme" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Saldoivamipyme&nbsp;&nbsp;<asp:CompareValidator ID="cvSALDOIVAMIPYME" runat="server" ControlToValidate="txtSaldoivamipyme" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtSaldoivamipyme" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Saldootros&nbsp;&nbsp;<asp:CompareValidator ID="cvSALDOOTROS" runat="server" ControlToValidate="txtSaldootros" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtSaldootros" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtIDSOLICITUDRECOGE').focus(); 
        }
        window.onload = SetFocus;
    </script>
</asp:Content>