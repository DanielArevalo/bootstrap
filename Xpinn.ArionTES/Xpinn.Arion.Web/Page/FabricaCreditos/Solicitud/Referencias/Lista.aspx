<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Referncias :." %>

<%@ Register Src="../../../../General/Controles/direccion.ascx" TagName="direccion"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">                    
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="CODREFERENCIA">
                    <Columns>
                        <asp:BoundField DataField="CODREFERENCIA" HeaderText="Codreferencia" HeaderStyle-CssClass="gridColNo"
                            ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="COD_PERSONA" HeaderText="Cod_persona" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:BoundField DataField="TIPOREFERENCIA" HeaderText="Tipo Referencia" />
                        <asp:BoundField DataField="NOMBRES" HeaderText="Nombres y Apellidos" />
                        <asp:BoundField DataField="CODPARENTESCO" HeaderText="Codparentesco" Visible="false" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Parentesco" />
                        <asp:BoundField DataField="DIRECCION" HeaderText="Dirección Domicilio" />
                        <asp:BoundField DataField="TELEFONO" HeaderText="Telefono Domicilio" />
                        <asp:BoundField DataField="TELOFICINA" HeaderText="Telefono Oficina" />
                        <asp:BoundField DataField="CELULAR" HeaderText="Celular" />
                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" Visible="false" />
                        <asp:BoundField DataField="CODUSUVERIFICA" HeaderText="Codusuverifica" Visible="false" />
                        <asp:BoundField DataField="FECHAVERIFICA" HeaderText="Fechaverifica" Visible="false" />
                        <asp:BoundField DataField="CALIFICACION" HeaderText="Calificacion" Visible="false" />
                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" Visible="false" />
                        <asp:BoundField DataField="NUMERO_RADICACION" HeaderText="Numero_radicacion" Visible="false" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="80%">
        <tr>
            <td class="tdI">
                <asp:TextBox ID="txtCodreferencia" runat="server" CssClass="textbox" MaxLength="128"
                    Enabled="False" Visible="False" Width="50%" />
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI" colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="tdI" align="left" colspan="2">
                <strong>Tipo Referencia</strong>&nbsp;*
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblTipoReferencia" runat="server" Width="51%"
                            RepeatDirection="Horizontal" AutoPostBack="True" 
                            onselectedindexchanged="rblTipoReferencia_SelectedIndexChanged" 
                            style="text-align: left">
                            <asp:ListItem Value="1" Selected="True">Familiar</asp:ListItem>
                            <asp:ListItem Value="2">Personal</asp:ListItem>
                            <asp:ListItem Value="3">Comercial</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="tdI" align="center" style="text-align: left">
                <strong>Datos de la Referencia</strong>&nbsp;&nbsp;</td>
            <td class="tdD">
                &nbsp;</td>
        </tr>    
        <tr>
            <td class="tdI" align="center" style="text-align: left">
                Nombres&nbsp;*<asp:RequiredFieldValidator ID="rfvNOMBRES" runat="server" ControlToValidate="txtNombres"
                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                    ForeColor="Red" Display="Dynamic" />
                
            </td>
            <td class="tdD" style="text-align:left">
                Parentesco
            </td>
        </tr>    
        <tr>
            <td class="tdI" style="text-align:left">
                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" MaxLength="128" Width="90%"  style="text-transform:uppercase" />
                <br />
            </td>
            <td class="tdD" style="text-align:left">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlParentesco" runat="server" Width="70%" AppendDataBoundItems="True"
                            onselectedindexchanged="ddlParentesco_SelectedIndexChanged" CssClass="textbox">
                            <asp:ListItem Value="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvParentesco" runat="server" 
                            ControlToValidate="ddlParentesco" Display="Dynamic" 
                            ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True" 
                            ValidationGroup="vgGuardar" style="font-size: xx-small" 
                            InitialValue="Seleccione un item" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rblTipoReferencia" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>   
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="70%">     
        <tr>
            <td class="logo" style="width: 200px; text-align:left">
                Teléfono Domicilio<br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Width="70%"
                    MaxLength="100" />
            </td>
            <td class="logo" style="width: 200px; text-align:left">
                Teléfono Oficina<br />
                <asp:TextBox ID="txtTeloficina" runat="server" CssClass="textbox" Width="70%"
                    MaxLength="100" />
            </td>
            <td class="tdD" style="width: 200px; text-align:left">
                Celular<br />
                <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" Width="70%"
                    MaxLength="100" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">     
        <tr>
            <td style="text-align:left">
                <strong>Dirección Domicilio</strong>
             </td>
        </tr>
        <tr>
            <td colspan="3">
                <uc1:direccion ID="direccion" runat="server" Requerido="True" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <asp:CompareValidator ID="cvNUMERO_RADICACION" runat="server" ControlToValidate="txtNumero_radicacion"
                    ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck"
                    SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red"
                    Display="Dynamic" /><br />
                <asp:TextBox ID="txtNumero_radicacion" runat="server" CssClass="textbox" MaxLength="128"
                    Visible="False" />
                <asp:TextBox ID="txtObservaciones" runat="server" CssClass="textbox" MaxLength="128"
                    Visible="False" />
                <asp:TextBox ID="txtCalificacion" runat="server" CssClass="textbox" MaxLength="128"
                    Visible="False" />
            </td>
            <td class="tdD" colspan="2">
                <asp:RequiredFieldValidator ID="rfvESTADO" runat="server" ControlToValidate="txtEstado"
                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                    ForeColor="Red" Display="Dynamic" />&nbsp;
                <asp:CompareValidator ID="cvESTADO" runat="server" ControlToValidate="txtEstado"
                    ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck"
                    SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red"
                    Display="Dynamic" />
                <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" MaxLength="128" Visible="False">0</asp:TextBox>
                <br />
                <asp:CompareValidator ID="cvCODUSUVERIFICA" runat="server" ControlToValidate="txtCodusuverifica"
                    ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck"
                    SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red"
                    Display="Dynamic" />
                <asp:TextBox ID="txtCodusuverifica" runat="server" CssClass="textbox" MaxLength="128"
                    Visible="False">1</asp:TextBox>
                <br />
                <asp:CompareValidator ID="cvFECHAVERIFICA" runat="server" ControlToValidate="txtFechaverifica"
                    ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True"
                    ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" ForeColor="Red"
                    Display="Dynamic" />
                <asp:TextBox ID="txtFechaverifica" runat="server" CssClass="textbox" MaxLength="128"
                    Visible="False">01/01/2000</asp:TextBox>
            </td>
        </tr>
    </table>
    <%-- <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCODREFERENCIA').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>
