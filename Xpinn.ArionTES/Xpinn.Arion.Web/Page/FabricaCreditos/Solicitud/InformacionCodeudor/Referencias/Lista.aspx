<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Referncias :." %>
<%@ Register src="../../../../../General/Controles/direccion.ascx" tagname="direccion" tagprefix="uc2" %>

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
    <table border="0" cellpadding="5" cellspacing="0" style="width: 130%">
        <tr>
            <td class="tdI" style="width: 721px">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="lblMensjae" runat="server" 
                    
                    Text="Grabar primero datos del codeudor, para grabar las referencias" 
                    ForeColor="#33CCFF" Font-Bold="True"></asp:Label>
            </td>                
            <td class="tdD">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI" style="width: 721px">
                <strong>DATOS DE LA REFERENCIA</strong>
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tdI" style="width: 721px">               
                <asp:TextBox ID="txtCodreferencia" runat="server" CssClass="textbox" MaxLength="128"
                    Enabled="False" Visible="False" />
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                Tipo Referencia&nbsp;*<br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblTipoReferencia" runat="server" 
                            RepeatDirection="Horizontal" AutoPostBack="True" 
                            onselectedindexchanged="rblTipoReferencia_SelectedIndexChanged">
                            <asp:ListItem Value="1" Selected="True">Familiar</asp:ListItem>
                            <asp:ListItem Value="2">Personal</asp:ListItem>
                            <asp:ListItem Value="3">Comercial</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </td>
            <td class="tdD">
                Nombres&nbsp;*<asp:RequiredFieldValidator ID="rfvNOMBRES" runat="server" ControlToValidate="txtNombres"
                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                    ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" MaxLength="128" 
                    Width="347px" />
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="2" style="text-align:left">
                <strong>Dirección Domicilio</strong><br />
                <uc2:direccion ID="direccion" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Parentesco<br />
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlParentesco" runat="server" 
                            onselectedindexchanged="ddlParentesco_SelectedIndexChanged" Height="16px" 
                            Width="423px">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rblTipoReferencia" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </td>
            <td class="tdD">
                Celular&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtCelular" runat="server" CssClass="textbox" MaxLength="128" />
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Teléfono&nbsp;Domicilio&nbsp;<br />
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" MaxLength="128" 
                    Width="149px" />
            </td>
            <td class="tdD">
                Teléfono Oficina<br />
                <asp:TextBox ID="txtTeloficina" runat="server" CssClass="textbox" MaxLength="128" />
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
            </td>
            <td class="tdD">
                <asp:RequiredFieldValidator ID="rfvESTADO" runat="server" ControlToValidate="txtEstado"
                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                    ForeColor="Red" Display="Dynamic" />&nbsp;
                <asp:CompareValidator ID="cvESTADO" runat="server" ControlToValidate="txtEstado"
                    ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck"
                    SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red"
                    Display="Dynamic" />
                <asp:TextBox ID="txtEstado" runat="server" CssClass="textbox" MaxLength="128" Visible="False">1</asp:TextBox>
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
    </table>
</asp:Content>
