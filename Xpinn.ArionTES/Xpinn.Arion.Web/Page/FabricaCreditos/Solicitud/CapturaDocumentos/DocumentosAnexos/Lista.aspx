<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - DocumentosAnexos :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI">
                                Iddocumento&nbsp;<asp:CompareValidator ID="cvIDDOCUMENTO" runat="server" ControlToValidate="txtIDDOCUMENTO"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtIddocumento" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Numero_radicacion&nbsp;<asp:CompareValidator ID="cvNUMERO_RADICACION" runat="server"
                                    ControlToValidate="txtNUMERO_RADICACION" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtNumero_radicacion" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Tipo_documento&nbsp;<asp:CompareValidator ID="cvTIPO_DOCUMENTO" runat="server" ControlToValidate="txtTIPO_DOCUMENTO"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtTipo_documento" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Cod_asesor&nbsp;<asp:CompareValidator ID="cvCOD_ASESOR" runat="server" ControlToValidate="txtCOD_ASESOR"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtCod_asesor" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Imagen&nbsp;<br />
                                <asp:TextBox ID="txtImagen" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Descripcion&nbsp;<br />
                                <asp:TextBox ID="txtDescripcion" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Fechaanexo&nbsp;<asp:CompareValidator ID="cvFECHAANEXO" runat="server" ControlToValidate="txtFECHAANEXO"
                                    ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True"
                                    ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic"
                                    ForeColor="Red" /><br />
                                <asp:TextBox ID="txtFechaanexo" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                &nbsp;
                            </td>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade>
            </td>
        </tr>
        <tr>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:RequiredFieldValidator ID="fvTipoDocumento" runat="server" 
                    ErrorMessage="Seleccione un tipo de documento" ControlToValidate="rblTipoDocumento" 
                    ForeColor="Red" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                <asp:RadioButtonList ID="rblTipoDocumento" runat="server" 
                    RepeatDirection="Horizontal">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <iframe frameborder="0" height="410px" scrolling="yes" src="../WebCam/TakingMyPictureTestPage.aspx"
                        width="100%" id="webcam"></iframe>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="IDDOCUMENTO">
                    <Columns>
                        <asp:BoundField DataField="IDDOCUMENTO" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                            <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                            <ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Borrar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Tipo Documento" />
                        <asp:TemplateField HeaderText="Imagen"  >
                            <ItemTemplate>
                                <asp:Image ID="Image1"  runat="server" ImageUrl='<%#    "Handler.ashx?id=" + Eval("IDDOCUMENTO")  %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:BoundField DataField="FECHAANEXO" HeaderText="Fecha Anexo" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                    <PagerStyle CssClass="gridPager"></PagerStyle>
                    <RowStyle CssClass="gridItem"></RowStyle>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('cphMain_txtIDDOCUMENTO').focus();
        }
        window.onload = SetFocus;
    </script>
</asp:Content>
