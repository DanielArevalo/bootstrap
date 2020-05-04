<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Vehiculos :." %>

<%@ Register src="~/General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                    <%--  <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                       Cod_vehiculo&nbsp;<asp:CompareValidator ID="cvCOD_VEHICULO" runat="server" ControlToValidate="txtCOD_VEHICULO" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_vehiculo" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Cod_persona&nbsp;<asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCOD_PERSONA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_persona" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Marca&nbsp;<asp:CompareValidator ID="cvMARCA" runat="server" ControlToValidate="txtMARCA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtMarca" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Placa&nbsp;<br/>
                       <asp:TextBox ID="txtPlaca" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Modelo&nbsp;<asp:CompareValidator ID="cvMODELO" runat="server" ControlToValidate="txtMODELO" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtModelo" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Valorcomercial&nbsp;<asp:CompareValidator ID="cvVALORCOMERCIAL" runat="server" ControlToValidate="txtVALORCOMERCIAL" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtValorcomercial" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valorprenda&nbsp;<asp:CompareValidator ID="cvVALORPRENDA" runat="server" ControlToValidate="txtVALORPRENDA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtValorprenda" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       &nbsp;
                       </td>
                </table>--%>
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
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="COD_VEHICULO">
                    <Columns>
                        <asp:BoundField DataField="COD_VEHICULO" HeaderText="Cod_vehiculo" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"/>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                      <%--  <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Modificar" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="COD_PERSONA" HeaderText="Cod_persona" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:BoundField DataField="MARCA" HeaderText="Marca" />
                        <asp:BoundField DataField="PLACA" HeaderText="Placa" />
                        <asp:BoundField DataField="MODELO" HeaderText="Modelo" />
                        <asp:BoundField DataField="VALORCOMERCIAL" HeaderText="Valor Comercial" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="VALORPRENDA" HeaderText="Valor Prenda" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                Marca&nbsp;&nbsp;<br />
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="dropdown" Visible="false">
                    <asp:ListItem Value="1">m1</asp:ListItem>
                    <asp:ListItem Value="2">m2</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtmarca" runat="server" CssClass="textbox" MaxLength="128" />
                <br />
            </td>
            <td class="tdD">
                Placa&nbsp;&nbsp;<asp:RequiredFieldValidator 
                    ID="rfvPlaca" runat="server" ControlToValidate="txtPlaca"
                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                    ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtPlaca" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Modelo&nbsp;&nbsp;<br />
                <asp:DropDownList ID="ddlModelo" runat="server" CssClass="dropdown">
                    <asp:ListItem Value="1">md1</asp:ListItem>
                    <asp:ListItem Value="2">md2</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdD">
                Valor Comercial<br />
                <uc1:decimales ID="txtValorcomercial" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Valor Prenda<br />
                <uc1:decimales ID="txtValorprenda" runat="server" />
            </td>
            <td class="tdD">
                &nbsp;<asp:Panel ID="Panel1" runat="server" Visible="False">
                    Cod_vehiculo&nbsp;*&nbsp;<br />
                    <asp:TextBox ID="txtCod_vehiculo" runat="server" 
    CssClass="textbox" MaxLength="128"
                    Enabled="False" />
                </asp:Panel>
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
