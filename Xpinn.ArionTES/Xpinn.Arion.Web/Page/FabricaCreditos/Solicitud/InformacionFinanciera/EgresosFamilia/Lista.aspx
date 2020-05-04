<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - EgresosFamilia :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                    <%--  <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                       Cod_egreso&nbsp;<asp:CompareValidator ID="cvCOD_EGRESO" runat="server" ControlToValidate="txtCOD_EGRESO" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_egreso" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Cod_persona&nbsp;<asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCOD_PERSONA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_persona" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Egresos&nbsp;<asp:CompareValidator ID="cvEGRESOS" runat="server" ControlToValidate="txtEGRESOS" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtEgresos" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Alimentacion&nbsp;<asp:CompareValidator ID="cvALIMENTACION" runat="server" ControlToValidate="txtALIMENTACION" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtAlimentacion" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Vivienda&nbsp;<asp:CompareValidator ID="cvVIVIENDA" runat="server" ControlToValidate="txtVIVIENDA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtVivienda" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Educacion&nbsp;<asp:CompareValidator ID="cvEDUCACION" runat="server" ControlToValidate="txtEDUCACION" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtEducacion" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Serviciospublicos&nbsp;<asp:CompareValidator ID="cvSERVICIOSPUBLICOS" runat="server" ControlToValidate="txtSERVICIOSPUBLICOS" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtServiciospublicos" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Transporte&nbsp;<asp:CompareValidator ID="cvTRANSPORTE" runat="server" ControlToValidate="txtTRANSPORTE" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtTransporte" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Pagodeudas&nbsp;<asp:CompareValidator ID="cvPAGODEUDAS" runat="server" ControlToValidate="txtPAGODEUDAS" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtPagodeudas" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Otros&nbsp;<asp:CompareValidator ID="cvOTROS" runat="server" ControlToValidate="txtOTROS" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtOtros" CssClass="textbox" runat="server"/>
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
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="COD_EGRESO">
                    <Columns>
                        <asp:BoundField DataField="COD_EGRESO" HeaderText="Cod_egreso" HeaderStyle-CssClass="gridColNo"
                            ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
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
                        <asp:BoundField DataField="COD_PERSONA" HeaderText="Cod Persona" Visible="false" />
                        <asp:BoundField DataField="EGRESOS" HeaderText="Egresos" Visible="false" />
                        <asp:BoundField DataField="ALIMENTACION" HeaderText="Alimentación" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="VIVIENDA" HeaderText="Vivienda" DataFormatString="{0:N0}"/>
                        <asp:BoundField DataField="EDUCACION" HeaderText="Educación" DataFormatString="{0:N0}"/>
                        <asp:BoundField DataField="SERVICIOSPUBLICOS" HeaderText="Servicios Publicos" DataFormatString="{0:N0}"/>
                        <asp:BoundField DataField="TRANSPORTE" HeaderText="Transporte" DataFormatString="{0:N0}"/>
                        <asp:BoundField DataField="PAGODEUDAS" HeaderText="Pago Deudas" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="OTROS" HeaderText="Otros" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <strong>Total Egresos:</strong>
                <asp:TextBox ID="txtTotalEgresos" runat="server" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                <asp:MaskedEditExtender ID="msktxtTotalEgresos" runat="server" TargetControlID="txtTotalEgresos"
                    Mask="999,999,999" MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft"
                    AcceptNegative="Left" ErrorTooltipEnabled="True" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">
        <tr>
            <td class="tdI">
                <asp:TextBox ID="txtCod_egreso" runat="server" CssClass="textbox" MaxLength="128"
                    Enabled="False" Visible="False" />
                <asp:TextBox ID="txtEgresos" runat="server" CssClass="textbox" MaxLength="128" Visible="False" />
            </td>
            <td class="tdD">
                <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" MaxLength="128"
                    Visible="False">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Pago Deudas<br />
                <uc1:decimales ID="txtPagodeudas" runat="server" />
                <br />
            </td>
            <td class="tdD">
                Alimentación<br />
                <uc1:decimales ID="txtAlimentacion" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Vivienda<br />
                <uc1:decimales ID="txtVivienda" runat="server" />
            </td>
            <td class="tdD">
                Educación<br />
                <uc1:decimales ID="txtEducacion" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Servicios Publicos<br />
                <uc1:decimales ID="txtServiciospublicos" runat="server" />
            </td>
            <td class="tdD">
                Transporte<br />
                <uc1:decimales ID="txtTransporte" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Otros<br />
                <uc1:decimales ID="txtOtros" runat="server" />
            </td>
            <td class="tdD">
                &nbsp;
            </td>
        </tr>
    </table>

    <%-- <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_EGRESO').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>
