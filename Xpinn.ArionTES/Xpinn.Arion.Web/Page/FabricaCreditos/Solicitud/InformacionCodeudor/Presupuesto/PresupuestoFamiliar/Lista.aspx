<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - PresupuestoFamiliar :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../../../../../General/Controles/decimales.ascx" tagname="decimales" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                    <%-- <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                       Cod_presupuesto&nbsp;<asp:CompareValidator ID="cvCOD_PRESUPUESTO" runat="server" ControlToValidate="txtCOD_PRESUPUESTO" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_presupuesto" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Cod_persona&nbsp;<asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCOD_PERSONA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_persona" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Actividadprincipal&nbsp;<asp:CompareValidator ID="cvACTIVIDADPRINCIPAL" runat="server" ControlToValidate="txtACTIVIDADPRINCIPAL" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtActividadprincipal" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Conyuge&nbsp;<asp:CompareValidator ID="cvCONYUGE" runat="server" ControlToValidate="txtCONYUGE" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtConyuge" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Otrosingresos&nbsp;<asp:CompareValidator ID="cvOTROSINGRESOS" runat="server" ControlToValidate="txtOTROSINGRESOS" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtOtrosingresos" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Consumofamiliar&nbsp;<asp:CompareValidator ID="cvCONSUMOFAMILIAR" runat="server" ControlToValidate="txtCONSUMOFAMILIAR" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtConsumofamiliar" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Obligaciones&nbsp;<asp:CompareValidator ID="cvOBLIGACIONES" runat="server" ControlToValidate="txtOBLIGACIONES" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtObligaciones" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Excedente&nbsp;<asp:CompareValidator ID="cvEXCEDENTE" runat="server" ControlToValidate="txtEXCEDENTE" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtExcedente" CssClass="textbox" runat="server"/>
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
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="COD_PRESUPUESTO">
                    <Columns>
                        <asp:BoundField DataField="COD_PRESUPUESTO" HeaderText="Cod_presupuesto" HeaderStyle-CssClass="gridColNo"
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
                        <asp:BoundField DataField="COD_PERSONA" HeaderText="Cod_persona" HeaderStyle-CssClass="gridColNo"
                            ItemStyle-CssClass="gridColNo" />
                        <asp:BoundField DataField="ACTIVIDADPRINCIPAL" HeaderText="Act. Principal" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="CONYUGE" HeaderText="Conyuge" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="OTROSINGRESOS" HeaderText="Otrosingresos" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="CONSUMOFAMILIAR" HeaderText="Consumofamiliar" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="OBLIGACIONES" HeaderText="Obligaciones" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="EXCEDENTE" HeaderText="Excedente" DataFormatString="{0:N0}" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="5" cellspacing="0" width="100%">

        <tr>
            <td class="tdI">
                Actividad 
                Principal</td>
            <td class="tdD">
               
                <uc1:decimales ID="txtActividadprincipal" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Conyuge</td>
            <td class="tdD">
              
                <uc1:decimales ID="txtConyuge" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Otros Ingresos</td>
            <td class="tdD">
                
                <uc1:decimales ID="txtOtrosingresos" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Consumo Familiar</td>
            <td class="tdD">
               
                <uc1:decimales ID="txtConsumofamiliar" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Obligaciones</td>
            <td class="tdD">
                
                <uc1:decimales ID="txtObligaciones" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI">
                Excedente</td>
            <td class="tdD">
                
                <uc1:decimales ID="txtExcedente" runat="server" Enabled="false" />
            </td>
        </tr>


        <tr>
            <td class="tdI">
                &nbsp;
            </td>
            <td class="tdD">
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    Cod_persona&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCOD_PERSONA" runat="server"
                        ControlToValidate="txtCod_persona" ErrorMessage="Campo Requerido" SetFocusOnError="True"
                        ValidationGroup="vgGuardar" ForeColor="Red" Display="Dynamic" />
                    <asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCod_persona"
                        ErrorMessage="Solo se admiten n&uacute;meros enteros" Operator="DataTypeCheck"
                        SetFocusOnError="True" Type="Integer" ValidationGroup="vgGuardar" ForeColor="Red"
                        Display="Dynamic" />
                    <br />
                    <asp:TextBox ID="txtCod_persona" runat="server" CssClass="textbox" MaxLength="128">1</asp:TextBox>
                    <br />
                    Cod_presupuesto&nbsp;*&nbsp;<br />
                    <asp:TextBox ID="txtCod_presupuesto" runat="server" CssClass="textbox" Enabled="False"
                        MaxLength="128" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    <%-- <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_PRESUPUESTO').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>
