<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - CosteoProductos :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI">
                       Cod_margen&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCOD_MARGEN" runat="server" ControlToValidate="txtCod_margen" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><asp:CompareValidator ID="cvCOD_MARGEN" runat="server" ControlToValidate="txtCod_margen" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtCod_margen" runat="server" CssClass="textbox" MaxLength="128" />
                           <br />
                       </td>
                       <td class="tdD">
                       <asp:TextBox ID="txtCod_costeo" runat="server" CssClass="textbox" MaxLength="128" 
                               Visible="False" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Materiaprima&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvMATERIAPRIMA" runat="server" ControlToValidate="txtMateriaprima" ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic"/><br />
                       <asp:TextBox ID="txtMateriaprima" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Unidadcompra&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtUnidadcompra" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Costounidad&nbsp;&nbsp;<asp:CompareValidator ID="cvCOSTOUNIDAD" runat="server" ControlToValidate="txtCostounidad" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtCostounidad" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       Cantidad&nbsp;&nbsp;<asp:CompareValidator ID="cvCANTIDAD" runat="server" ControlToValidate="txtCantidad" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtCantidad" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Costo&nbsp;&nbsp;<asp:CompareValidator ID="cvCOSTO" runat="server" ControlToValidate="txtCosto" ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" /><br />
                       <asp:TextBox ID="txtCosto" runat="server" CssClass="textbox" MaxLength="128" />
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
    </table>
    <%--<script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('ctl00_cphMain_txtCOD_COSTEO').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>