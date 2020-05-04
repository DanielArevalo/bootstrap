<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - ComposicionPasivo :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2">
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" onrowediting="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  DataKeyNames="IDPASIVO" >
                    <Columns>
                        <asp:BoundField DataField="IDPASIVO" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>                        
                        <asp:BoundField DataField="COD_INFFIN" HeaderText="Cod inffin" />
                        <asp:BoundField DataField="ACREEDOR" HeaderText="Acreedor" />
                        <asp:BoundField DataField="MONTO_OTORGADO" HeaderText="Monto otorgado" />
                        <asp:BoundField DataField="VALOR_CUOTA" HeaderText="Valor cuota" />
                        <asp:BoundField DataField="PERIODICIDAD" HeaderText="Periodicidad" />
                        <asp:BoundField DataField="CUOTA" HeaderText="Cuota" />
                        <asp:BoundField DataField="PLAZO" HeaderText="Plazo" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
            </td>
        </tr>
        <tr>
            <td style="height: 23px">
                Total Monto Otorgado:</td>
            <td style="height: 23px">
                <asp:TextBox ID="txtTotalMontoOtorgado" runat="server" ReadOnly="True"></asp:TextBox>
                 <asp:maskededitextender ID="msktxtTotalMontoOtorgado" runat="server" targetcontrolid="txtTotalMontoOtorgado" mask="999,999,999"
                    messagevalidatortip="true" masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"
                    displaymoney="Left" errortooltipenabled="True" />
            </td>
        </tr>
        <tr>
            <td>
                Total Valor Cuota :
            </td>
            <td>
                <asp:TextBox ID="txtTotalValorCuota" runat="server" ReadOnly="True"></asp:TextBox>
                 <asp:maskededitextender ID="msktxtTotalValorCuota" runat="server" targetcontrolid="txtTotalValorCuota" mask="999,999,999"
                    messagevalidatortip="true" masktype="Number" inputdirection="RightToLeft" acceptnegative="Left"
                    displaymoney="Left" errortooltipenabled="True" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="height: 27px">
                <asp:TextBox ID="txtIdpasivo" runat="server" CssClass="textbox" MaxLength="128" 
                        Enabled="False" Visible="False" />
            </td>
            <td style="height: 27px">
                <asp:TextBox ID="txtCod_inffin" runat="server" CssClass="textbox" MaxLength="128" 
                        Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                Acreedor&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtAcreedor" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
            <td>
                Monto Otorgado&nbsp;&nbsp;<asp:CompareValidator ID="cvMONTO_OTORGADO0" 
                runat="server" ControlToValidate="txtMonto_otorgado" 
                ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" 
                SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" 
                ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtMonto_otorgado" runat="server" CssClass="textbox" 
                MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td>
                Valor Cuota&nbsp;&nbsp;<asp:CompareValidator ID="cvVALOR_CUOTA0" runat="server" 
                    ControlToValidate="txtValor_cuota" 
                    ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" 
                    SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" 
                    ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtValor_cuota" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
            <td>
                Periodicidad&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtPeriodicidad" runat="server" CssClass="textbox" 
                MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td>
                Saldo a la Fecha&nbsp;&nbsp;<asp:CompareValidator ID="cvCUOTA0" runat="server" 
                    ControlToValidate="txtCuota" 
                    ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" 
                    SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" 
                    ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtCuota" runat="server" CssClass="textbox" MaxLength="128" />
            </td>
            <td>
                Plazo&nbsp;&nbsp;<asp:CompareValidator ID="cvPLAZO0" runat="server" 
                ControlToValidate="txtPlazo" 
                ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck" 
                SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" 
                ForeColor="Red" Display="Dynamic" /><br />
                <asp:TextBox ID="txtPlazo" runat="server" CssClass="textbox" 
                MaxLength="128" />
            </td>
        </tr>
    </table>
</asp:Content>