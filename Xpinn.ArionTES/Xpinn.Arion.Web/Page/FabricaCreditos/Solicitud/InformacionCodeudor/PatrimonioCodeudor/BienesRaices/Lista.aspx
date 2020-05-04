<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - BienesRaices :." %>
<%@ Register src="../../../../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                    <%-- <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                       Cod_bien&nbsp;<asp:CompareValidator ID="cvCOD_BIEN" runat="server" ControlToValidate="txtCOD_BIEN" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_bien" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Cod_persona&nbsp;<asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCOD_PERSONA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_persona" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipo&nbsp;<asp:CompareValidator ID="cvTIPO" runat="server" ControlToValidate="txtTIPO" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtTipo" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Matricula&nbsp;<br/>
                       <asp:TextBox ID="txtMatricula" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Valorcomercial&nbsp;<asp:CompareValidator ID="cvVALORCOMERCIAL" runat="server" ControlToValidate="txtVALORCOMERCIAL" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtValorcomercial" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Valorhipoteca&nbsp;<asp:CompareValidator ID="cvVALORHIPOTECA" runat="server" ControlToValidate="txtVALORHIPOTECA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtValorhipoteca" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
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
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="COD_BIEN">
                    <Columns>
                        <asp:BoundField DataField="COD_BIEN" HeaderText="Cod_bien" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"/>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
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
                        <asp:BoundField DataField="COD_PERSONA" HeaderText="Cod_persona" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"/>
                        <asp:BoundField DataField="TIPO" HeaderText="Tipo" />
                        <asp:BoundField DataField="MATRICULA" HeaderText="Matricula" />
                        <asp:BoundField DataField="VALORCOMERCIAL" HeaderText="Valor Comercial" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="VALORHIPOTECA" HeaderText="Valor Hipoteca" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                Tipo<br />
                <asp:DropDownList ID="ddlTipoVehiculo" runat="server" CssClass="dropdown">
                    <asp:ListItem Value="1">t1</asp:ListItem>
                    <asp:ListItem Value="2">t2</asp:ListItem>
                    <asp:ListItem Value="3">t3</asp:ListItem>
                </asp:DropDownList>
                <br />
            </td>
            <td class="tdD">
                Matricula&nbsp;&nbsp;<asp:RequiredFieldValidator 
                    ID="rfvMatricula" runat="server" ControlToValidate="txtMatricula"
                    ErrorMessage="Campo Requerido" SetFocusOnError="True" ValidationGroup="vgGuardar"
                    ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtMatricula" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Valor Comercial<br />
                <uc1:decimales ID="txtValorcomercial" runat="server" />
            </td>
            <td class="tdD">
                Valor Hipoteca<br />
                <uc1:decimales ID="txtValorhipoteca" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    Cod_bien&nbsp;*&nbsp;<br />
                    <asp:TextBox ID="txtCod_bien" runat="server" CssClass="textbox" 
    MaxLength="128" Enabled="False" />
                </asp:Panel>
            </td>
            <td class="tdD">
                &nbsp;</td>
        </tr>
    </table>
  <%--  <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_BIEN').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>