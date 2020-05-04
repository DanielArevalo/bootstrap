<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - LineasCredito :." %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    <table id="Table1" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                Cod_linea_credito&nbsp;<br />
                                <asp:TextBox ID="txtCod_linea_credito" CssClass="textbox" runat="server" 
                                    MaxLength="128" ClientIDMode="AutoID" />
                            </td>
                            <td>
                                Nombre&nbsp;<br />
                                <asp:TextBox ID="txtNombre" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Tipo_linea&nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtTIPO_LINEA"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox3" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Tipo_liquidacion&nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server"
                                    ControlToValidate="txtTIPO_LIQUIDACION" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox4" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Tipo_cupo&nbsp;<asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtTIPO_CUPO"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox5" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Recoge_saldos&nbsp;<asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtRECOGE_SALDOS"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox6" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Cobra_mora&nbsp;<asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtCOBRA_MORA"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox7" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Tipo_refinancia&nbsp;<asp:CompareValidator ID="CompareValidator6" runat="server"
                                    ControlToValidate="txtTIPO_REFINANCIA" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox8" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Minimo_refinancia&nbsp;<asp:CompareValidator ID="CompareValidator7" runat="server"
                                    ControlToValidate="txtMINIMO_REFINANCIA" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox9" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Maximo_refinancia&nbsp;<asp:CompareValidator ID="CompareValidator8" runat="server"
                                    ControlToValidate="txtMAXIMO_REFINANCIA" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox10" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Maneja_pergracia&nbsp;<br />
                                <asp:TextBox ID="TextBox11" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Periodo_gracia&nbsp;<asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtPERIODO_GRACIA"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox12" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Tipo_periodic_gracia&nbsp;<br />
                                <asp:TextBox ID="TextBox13" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Modifica_datos&nbsp;<br />
                                <asp:TextBox ID="TextBox14" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Modifica_fecha_pago&nbsp;<br />
                                <asp:TextBox ID="TextBox15" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Garantia_requerida&nbsp;<br />
                                <asp:TextBox ID="TextBox16" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Tipo_capitalizacion&nbsp;<asp:CompareValidator ID="CompareValidator10" runat="server"
                                    ControlToValidate="txtTIPO_CAPITALIZACION" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox17" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Cuotas_extras&nbsp;<asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="txtCUOTAS_EXTRAS"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox18" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Cod_clasifica&nbsp;<asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="txtCOD_CLASIFICA"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox19" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Numero_codeudores&nbsp;<asp:CompareValidator ID="CompareValidator13" runat="server"
                                    ControlToValidate="txtNUMERO_CODEUDORES" ErrorMessage="Solo se admiten n&uacute;meros"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar"
                                    Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox20" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Cod_moneda&nbsp;<asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtCOD_MONEDA"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox21" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Porc_corto&nbsp;<asp:CompareValidator ID="CompareValidator15" runat="server" ControlToValidate="txtPORC_CORTO"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox22" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Tipo_amortiza&nbsp;<asp:CompareValidator ID="CompareValidator16" runat="server" ControlToValidate="txtTIPO_AMORTIZA"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox23" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Estado&nbsp;<asp:CompareValidator ID="CompareValidator17" runat="server" ControlToValidate="txtESTADO"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="TextBox24" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                    </table>                  
                </asp:Panel>
                <asp:Panel ID="pConsulta" runat="server">                  
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="50%">
                        <tr>
                            <td>
                                Codigo Linea Credito&nbsp;<br />
                                <asp:TextBox ID="txtCod_linea_credito1" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td>
                                Nombre&nbsp;<br />
                                <asp:TextBox ID="txtNombre1" CssClass="textbox" runat="server" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <hr width="100%" noshade />
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
            document.getElementById('cphMain_txtCOD_LINEA_CREDITO').focus();
        }
        window.onload = SetFocus;
    </script>
    <div style="overflow:scroll;height:500px;width:100%;">        
        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" Width="2000px"
            OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
            OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
            OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
            PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" 
            DataKeyNames="COD_LINEA_CREDITO" style="font-size: x-small">
            <Columns>
                <asp:BoundField DataField="COD_LINEA_CREDITO" HeaderStyle-CssClass="gridColNo" 
                    ItemStyle-CssClass="gridColNo" >
                    <HeaderStyle CssClass="gridColNo"></HeaderStyle>
                    <ItemStyle CssClass="gridColNo"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                            ToolTip="Modificar" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                    <ItemStyle CssClass="gridIco"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                            ToolTip="Borrar" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="gridIco"></HeaderStyle>
                    <ItemStyle CssClass="gI" />
                </asp:TemplateField>
                <asp:BoundField DataField="COD_LINEA_CREDITO" HeaderText="Código" />
                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre de Línea" >
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="tipo_linea" HeaderText="Tipo_linea" />
                <asp:BoundField DataField="tipo_liquidacion" HeaderText="Tipo_liquidacion" />
                <asp:BoundField DataField="tipo_cupo" HeaderText="Tipo_cupo" />
                <asp:BoundField DataField="RECOGE_SALDOS" HeaderText="Recoge_saldos" />
                <asp:BoundField DataField="COBRA_MORA" HeaderText="Cobra_mora" />
                <asp:BoundField DataField="TIPO_REFINANCIA" HeaderText="Tipo_refinancia" />
                <asp:BoundField DataField="MINIMO_REFINANCIA" HeaderText="Minimo_refinancia" />
                <asp:BoundField DataField="MAXIMO_REFINANCIA" HeaderText="Maximo_refinancia" />
                <asp:BoundField DataField="MANEJA_PERGRACIA" HeaderText="Maneja_pergracia" />
                <asp:BoundField DataField="PERIODO_GRACIA" HeaderText="Periodo_gracia" />
                <asp:BoundField DataField="TIPO_PERIODIC_GRACIA" HeaderText="Tipo_periodic_gracia" />
                <asp:BoundField DataField="MODIFICA_DATOS" HeaderText="Modifica_datos" />
                <asp:BoundField DataField="MODIFICA_FECHA_PAGO" HeaderText="Modifica_fecha_pago" />
                <asp:BoundField DataField="GARANTIA_REQUERIDA" HeaderText="Garantia_requerida" />
                <asp:BoundField DataField="TIPO_CAPITALIZACION" HeaderText="Tipo_capitalizacion" />
                <asp:BoundField DataField="CUOTAS_EXTRAS" HeaderText="Cuotas_extras" />
                <asp:BoundField DataField="COD_CLASIFICA" HeaderText="Cod_clasifica" />
                <asp:BoundField DataField="NUMERO_CODEUDORES" HeaderText="Numero_codeudores" />
                <asp:BoundField DataField="COD_MONEDA" HeaderText="Cod_moneda" />
                <asp:BoundField DataField="PORC_CORTO" HeaderText="Porc_corto" />
                <asp:BoundField DataField="TIPO_AMORTIZA" HeaderText="Tipo_amortiza" />
                <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
            </Columns>
            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
            <PagerStyle CssClass="gridPager"></PagerStyle>
            <RowStyle CssClass="gridItem"></RowStyle>
        </asp:GridView>
    </div>
</asp:Content>
