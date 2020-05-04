<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Referencia :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="5" cellspacing="0" width="100%" >
                   <tr>
                       <td class="tdI" style="width: 303px">
                           Datos cliente:</td>
                       <td class="tdD">
                           &nbsp;</td>
                       <td class="tdD">
                           &nbsp;</td>
                       <td class="tdD">
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="tdI" style="width: 303px">
                           Nombre&nbsp;<br />
                       <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" MaxLength="128" 
                               Enabled="False" Width="300px" />
                           <br />
                       </td>
                       <td class="tdD">
                           <br />
                       Identificación&nbsp;&nbsp;<br />
                       <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" 
                               MaxLength="128" Enabled="False" />
                           <br />
                           <br />
                       </td>
                       <td class="tdD">
                           Número solicitud<br />
                       <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" 
                               MaxLength="128" Enabled="False" />
                       </td>
                       <td class="tdD">
                           Oficina<br />
                       <asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" 
                               MaxLength="128" Enabled="False" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI" style="width: 303px">
                           Referencia:</td>
                       <td class="tdD">
                           &nbsp;</td>
                       <td class="tdD">
                           &nbsp;</td>
                       <td class="tdD">
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="tdI" colspan="4">

                           <asp:GridView ID="gvLista" runat="server" AllowPaging="True" 
                               AutoGenerateColumns="False" DataKeyNames="cod_referencia" 
                               GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" 
                               PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" 
                               Width="100%">
                               <Columns>
                                   <asp:BoundField DataField="cod_referencia" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                                   <asp:BoundField DataField="Columnas"/>
                                   <asp:BoundField DataField="Valor" HeaderText="Valor" />
                                    <asp:TemplateField HeaderText="Check">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkValidar" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                               </Columns>
                           </asp:GridView>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI" colspan="2">Calificación:<br />
                           <asp:DropDownList ID="ddlCalificacion" runat="server" CssClass="dropdown">
                           </asp:DropDownList>
                           <br />
                <asp:CompareValidator ID="cvCalificacion" runat="server" 
                    ControlToValidate="ddlCalificacion" Display="Dynamic" 
                    ErrorMessage="Seleccione una calificación:" ForeColor="Red" 
                    Operator="GreaterThan" SetFocusOnError="true" Type="Integer" 
                    ValidationGroup="vgGuardar" ValueToCompare="0"></asp:CompareValidator>
                           <br />
                           <br />
                           Verificador:<asp:TextBox ID="txtVerificadorRef" 
                               runat="server" CssClass="textbox" 
                               MaxLength="128" Width="250px" Enabled="False" />
                       </td>                   
                       <td class="tdD" colspan="2">
                           Concepto:<br />
                       <asp:TextBox ID="txtDetalleRef" runat="server" CssClass="textbox" MaxLength="128" 
                               Height="120px" TextMode="MultiLine" Width="350px" />
                           <br />
                           <asp:RequiredFieldValidator ID="rfvDetalle" runat="server" 
                               ControlToValidate="txtDetalleRef" ErrorMessage="Campo Requerido" 
                               SetFocusOnError="True" ValidationGroup="vgGuardar" ForeColor="Red" 
                               Display="Dynamic"/>
                       </td>
                   
                       </table>
    <%--<script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('ctl00_cphMain_txtnumero_radicacion').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>    <%--<script type="text/javascript" language="javascript">
    function ValidacionCondicional() {
    var chkValida = document.getElementById('<%=chkValidar.ClientID %>')
    var resultado = true;
    if (chkValida.checked) {
        if (!Page_ClientValidate("vgGuardar")) {
            resultado = false;
        }
    }
    else {
        Page_ClientValidate("vgNulo")
    }
    return resultado;
}
    </script>--%>
</asp:Content>