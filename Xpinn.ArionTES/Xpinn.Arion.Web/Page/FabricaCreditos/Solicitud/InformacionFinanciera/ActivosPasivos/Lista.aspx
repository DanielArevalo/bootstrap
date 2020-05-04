<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - EstadosFinancieros :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<%@ Register src="~/General/Controles/decimalesGrid.ascx" tagname="decimalesGrid" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <script>

    function click7(TextBox) {      

        var i, CellValue, Row, td;       
        var table = document.getElementById('<%=gvLista.ClientID %>');

        var resultado = parseInt(table.rows[7].cells[6].innerHTML) + parseInt(table.rows[6].cells[6].innerHTML) + parseInt(table.rows[5].cells[6].innerHTML) + parseInt(table.rows[4].cells[6].innerHTML) + parseInt(table.rows[3].cells[6].innerHTML) + parseInt(table.rows[2].cells[6].innerHTML);

        Row = table.rows[7];
        td = Row.cells[3];
        CellValue = td.children[0].attributes[0].value;


        document.getElementById(TextBox.id).value = document.getElementById(CellValue).toString();  //parseInt(resultado);  //document.getElementById('<%=TextBox1.ClientID %>').value;  //

    }

</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server" Visible="false">
    </asp:Panel>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Label ID="lblTipoInformacion" runat="server" Style="font-weight: 700" 
                    Visible="False"></asp:Label>
                
                <asp:TextBox ID="TextBox1" runat="server" Visible="False" 
                    ontextchanged="TextBox1_TextChanged"></asp:TextBox>
                
                <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="btnActivos" runat="server" OnClick="btnActivos_Click">Activos</asp:LinkButton>
                &nbsp;.:.&nbsp;<asp:LinkButton ID="btnPasivos" runat="server" 
                    OnClick="btnPasivos_Click">Pasivos</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowDataBound="gvLista_RowDataBound" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging"                            
                            PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                            RowStyle-CssClass="gridItem" DataKeyNames="COD_BALANCE" >
                            <Columns>
                                <asp:BoundField DataField="COD_BALANCE" HeaderText="COD. BALANCE" 
                                    HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                <HeaderStyle CssClass="gridColNo" />
                                <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:CommandField ShowEditButton="True" Visible="False" />
                                <asp:TemplateField HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                            ToolTip="Borrar" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                                    <ItemStyle CssClass="gridColNo"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="COD_INFFIN" HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="gridColNo"
                                    ItemStyle-CssClass="gridColNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCOD_INFFIN" runat="server" Text='<%# Eval("COD_INFFIN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridColNo" HorizontalAlign="Left" />
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cod. Cuenta">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCod_cuenta" runat="server" Text='<%# Eval("COD_CUENTA") %>' Font-Size="Small"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor" HeaderStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("VALOR") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>                                       
                                        <uc2:decimalesGrid ID="txtValor" runat="server" Text='<%# Eval("VALOR") %>'  />
                                        <asp:TextBox ID="txtValora" runat="server" Text='<%# Eval("VALOR") %>' Visible="False" onclick = "click7(this)" style="text-align:right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="COD_INFFIN" HeaderText="COD_INFFIN" 
                                    HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                                <HeaderStyle CssClass="gridColNo" />
                                <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VALOR" HeaderText="VALOR" 
                                    HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                               
                                <HeaderStyle CssClass="gridColNo" />
                                <ItemStyle CssClass="gridColNo" />
                                </asp:BoundField>
                               
                            </Columns>
                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                            <PagerStyle CssClass="gridPager"></PagerStyle>
                            <RowStyle CssClass="gridItem"></RowStyle>
                        </asp:GridView>
                    </ContentTemplate>
                    <%--<Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnInfoFamiliar" EventName="Click" />
                    </Triggers>--%>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    <asp:MultiView ID="mvInfFinNegocio" runat="server" ActiveViewIndex="0">
        <asp:View ID="mvEdicion" runat="server">
            <asp:Panel ID="Panel2" runat="server" Visible="false">
                <table border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdI">
                            Cod_balance *
                            <asp:TextBox ID="txtCod_balance" runat="server" CssClass="textbox" MaxLength="128"
                                Enabled="False" />
                        </td>
                        <td class="tdD">
                            Cod_inffin *
                            <asp:RequiredFieldValidator ID="rfvCOD_INFFIN" runat="server" ControlToValidate="txtCod_inffin"
                                ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                                ForeColor="Red" Display="Dynamic" /><asp:CompareValidator ID="cvCOD_INFFIN" runat="server"
                                    ControlToValidate="txtCod_inffin" ErrorMessage="Solo se admiten n&uacute;meros enteros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar"
                                    ForeColor="Red" Display="Dynamic" />
                            <asp:TextBox ID="txtCod_inffin" runat="server" CssClass="textbox" MaxLength="128"
                                Enabled="False" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdI">
                            Cod_cuenta *
                            <asp:RequiredFieldValidator ID="rfvCOD_CUENTA" runat="server" ControlToValidate="txtCod_cuenta"
                                ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                                ForeColor="Red" Display="Dynamic" /><asp:CompareValidator ID="cvCOD_CUENTA" runat="server"
                                    ControlToValidate="txtCod_cuenta" ErrorMessage="Solo se admiten n&uacute;meros enteros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar"
                                    ForeColor="Red" Display="Dynamic" />
                            <asp:TextBox ID="txtCod_cuenta" runat="server" CssClass="textbox" MaxLength="128"
                                Enabled="False" />
                        </td>
                        <td class="tdD">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
        <asp:View ID="mvNuevo" runat="server">
            <asp:Panel ID="Panel1" runat="server">
                Cod_cuenta *
                <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txtCodCuenta"
                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                    ValidationGroup="vgGuardar" />
                <asp:CompareValidator ID="CompareValidator44" runat="server" ControlToValidate="txtCodCuenta"
                    Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" />
                <asp:TextBox ID="txtCodCuenta" runat="server" CssClass="textbox" MaxLength="128" />
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <%--<Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnInfoFamiliar" EventName="Click" />
                    </Triggers>--%>
</asp:Content>
