<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - IngresosFamilia :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../../../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                    <%-- <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdI">
                                Cod_ingreso&nbsp;<asp:CompareValidator ID="cvCOD_INGRESO" runat="server" ControlToValidate="txtCOD_INGRESO"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtCod_ingreso" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Ingresos&nbsp;<asp:CompareValidator ID="cvINGRESOS" runat="server" ControlToValidate="txtINGRESOS"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtIngresos" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Negocio&nbsp;<asp:CompareValidator ID="cvNEGOCIO" runat="server" ControlToValidate="txtNEGOCIO"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtNegocio" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Conyuge&nbsp;<asp:CompareValidator ID="cvCONYUGE" runat="server" ControlToValidate="txtCONYUGE"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtConyuge" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Hijos&nbsp;<asp:CompareValidator ID="cvHIJOS" runat="server" ControlToValidate="txtHIJOS"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtHijos" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Arriendos&nbsp;<asp:CompareValidator ID="cvARRIENDOS" runat="server" ControlToValidate="txtARRIENDOS"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtArriendos" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Pension&nbsp;<asp:CompareValidator ID="cvPENSION" runat="server" ControlToValidate="txtPENSION"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtPension" CssClass="textbox" runat="server" MaxLength="128" />
                            </td>
                            <td class="tdD">
                                Otros&nbsp;<asp:CompareValidator ID="cvOTROS" runat="server" ControlToValidate="txtOTROS"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtOtros" CssClass="textbox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdI">
                                Cod_persona&nbsp;<asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCOD_PERSONA"
                                    ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True"
                                    Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br />
                                <asp:TextBox ID="txtCod_persona" CssClass="textbox" runat="server" MaxLength="128" />
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
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="COD_INGRESO">
                    <Columns>
                        <asp:BoundField DataField="COD_INGRESO" HeaderText="Cod_ingreso" HeaderStyle-CssClass="gridColNo"
                            ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                     <%--   <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
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
                        <asp:BoundField DataField="INGRESOS" HeaderText="Ingresos" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="NEGOCIO" HeaderText="Negocio" DataFormatString="{0:N0}"/>
                        <asp:BoundField DataField="CONYUGE" HeaderText="Conyuge" DataFormatString="{0:N0}"/>
                        <asp:BoundField DataField="HIJOS" HeaderText="Hijos" DataFormatString="{0:N0}"/>
                        <asp:BoundField DataField="ARRIENDOS" HeaderText="Arriendos" DataFormatString="{0:N0}"/>
                        <asp:BoundField DataField="PENSION" HeaderText="Pension" DataFormatString="{0:N0}"/>
                        <asp:BoundField DataField="OTROS" HeaderText="Otros" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="COD_PERSONA" HeaderText="Cod_persona" Visible="false" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <strong>Total Ingresos: </strong>
                <asp:TextBox ID="txtTotalIngresos" runat="server" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                <asp:MaskedEditExtender ID="msktxtTotalIngresos" runat="server" TargetControlID="txtTotalIngresos"
                    Mask="999,999,999" MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft"
                    AcceptNegative="Left" ErrorTooltipEnabled="True" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                <asp:TextBox ID="txtCod_ingreso" runat="server" CssClass="textbox" MaxLength="128"
                    Enabled="False" Visible="False" />
            </td>
            <td class="tdD">
                <asp:TextBox ID="txtIngresos" runat="server" CssClass="textbox" MaxLength="128" Visible="False">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Negocio<br />
                <uc1:decimales ID="txtNegocio" runat="server" />
            </td>
            <td class="tdD">
                Conyuge<br />
                <uc1:decimales ID="txtConyuge" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Hijos<br />
                <uc1:decimales ID="txtHijos" runat="server" />
            </td>
            <td class="tdD">
                Arriendos<br />
                <uc1:decimales ID="txtArriendos" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Pension<br />
                <uc1:decimales ID="txtPension" runat="server" />
            </td>
            <td class="tdD">
                Otros<br />
                <uc1:decimales ID="txtOtros" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" MaxLength="128"
                    Visible="False" />
            </td>
            <td class="tdD">
                &nbsp;
            </td>
    </table>

   <%-- <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_INGRESO').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>