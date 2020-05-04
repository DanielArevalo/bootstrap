<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - CosteoProductos :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                    <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI">
                                Cod_costeo&nbsp;<asp:CompareValidator ID="cvCOD_COSTEO" runat="server" ControlToValidate="txtCOD_COSTEO"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtCod_costeo" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Cod_margen&nbsp;<asp:CompareValidator ID="cvCOD_MARGEN" runat="server" ControlToValidate="txtCOD_MARGEN"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtCod_margen" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Materiaprima&nbsp;<br />
                                <asp:TextBox ID="txtMateriaprima" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Unidadcompra&nbsp;<br />
                                <asp:TextBox ID="txtUnidadcompra" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Costounidad&nbsp;<asp:CompareValidator ID="cvCOSTOUNIDAD" runat="server" ControlToValidate="txtCOSTOUNIDAD"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtCostounidad" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Cantidad&nbsp;<asp:CompareValidator ID="cvCANTIDAD" runat="server" ControlToValidate="txtCANTIDAD"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtCantidad" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Costo&nbsp;<asp:CompareValidator ID="cvCOSTO" runat="server" ControlToValidate="txtCOSTO"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtCosto" CssClass="textbox" runat="server" MaxLength="128" />
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
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="COD_COSTEO">
                    <Columns>
                        <asp:BoundField DataField="COD_COSTEO" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Modificar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="COD_MARGEN" HeaderText="Cod_margen" />
                        <asp:BoundField DataField="MATERIAPRIMA" HeaderText="Materiaprima" />
                        <asp:BoundField DataField="UNIDADCOMPRA" HeaderText="Unidadcompra" />
                        <asp:BoundField DataField="COSTOUNIDAD" HeaderText="Costounidad" />
                        <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" />
                        <asp:BoundField DataField="COSTO" HeaderText="Costo" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <strong>Totales</strong>
            </td>
        </tr>
        <tr>
            <td>
                Costo Total Materiales:&nbsp;
                <asp:TextBox ID="txtCostoTotalMateriales" runat="server" ReadOnly="True"></asp:TextBox>
                <asp:maskededitextender runat="server" targetcontrolid="txtCostoTotalMateriales" mask="999,999,999"
                    messagevalidatortip="true" masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"
                    displaymoney="Left" errortooltipenabled="True" />
            </td>
        </tr>
        <tr>
            <td>
                        <asp:ImageButton ID="btnAtras" runat="server" 
                    ImageUrl="~/Images/iconAnterior.jpg" onclick="btnAtras_Click" />
                        <asp:ImageButton ID="btnAdelante" runat="server" 
                    ImageUrl="~/Images/btnAdelante.jpg" onclick="btnAdelante_Click" 
                    onclientclick="LoadingList()" />
            </td>
        </tr>
    </table>
    <%--<script type="text/javascript" language="javascript">
        function SetFocus() {
            document.getElementById('ctl00_cphMain_txtCOD_COSTEO').focus();
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>
