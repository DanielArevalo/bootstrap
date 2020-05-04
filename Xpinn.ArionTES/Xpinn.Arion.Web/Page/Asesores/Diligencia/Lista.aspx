<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Diligencia :." %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                           Código diligencia&nbsp;<br/>
                       <asp:TextBox ID="txtCodigo_diligencia" CssClass="textbox" runat="server" MaxLength="128"/>
                           <br />
                           <asp:CompareValidator ID="cvCODIGO_DILIGENCIA" runat="server" 
                               ControlToValidate="txtCODIGO_DILIGENCIA" Display="Dynamic" 
                               ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                       <td class="tdD">
                           Numero radicación&nbsp;<br/>
                       <asp:TextBox ID="txtNumero_radicacion" CssClass="textbox" runat="server"/>
                           <br />
                           <asp:CompareValidator ID="cvNUMERO_RADICACION" runat="server" 
                               ControlToValidate="txtNUMERO_RADICACION" Display="Dynamic" 
                               ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Fecha diligencia&nbsp;<br/>
                       <asp:TextBox ID="txtFecha_diligencia" CssClass="textbox" runat="server" MaxLength="128"/>
                           <asp:CalendarExtender ID="txtFecha_diligencia_CalendarExtender" runat="server" 
                               Enabled="True" TargetControlID="txtFecha_diligencia" Format="dd/MM/yyyy">
                           </asp:CalendarExtender>
                           <br />
                           <asp:CompareValidator ID="cvFecha_diligencia" runat="server" ControlToValidate="txtFecha_diligencia" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" />
                       </td>
                       <td class="tdD">
                           Tipo diligencia&nbsp;<br/>
                       <asp:TextBox ID="txtTipo_diligencia" CssClass="textbox" runat="server"/>
                           <br />
                           <asp:CompareValidator ID="cvTIPO_DILIGENCIA" runat="server" 
                               ControlToValidate="txtTIPO_DILIGENCIA" Display="Dynamic" 
                               ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Atendió<br/>
                       <asp:TextBox ID="txtAtendio" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Respuesta&nbsp;<br/>
                       <asp:TextBox ID="txtRespuesta" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Acuerdo&nbsp;<br/>
                       <asp:TextBox ID="txtAcuerdo" CssClass="textbox" runat="server" MaxLength="128"/>
                           <br />
                           <asp:CompareValidator ID="cvACUERDO" runat="server" 
                               ControlToValidate="txtACUERDO" Display="Dynamic" 
                               ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                       <td class="tdD">
                           Fecha acuerdo&nbsp;<br/>
                       <asp:TextBox ID="txtFecha_acuerdo" CssClass="textbox" runat="server"/>
                           <asp:CalendarExtender ID="txtFecha_acuerdo_CalendarExtender" runat="server" 
                               Enabled="True" TargetControlID="txtFecha_acuerdo" Format="dd/MM/yyyy">
                           </asp:CalendarExtender>
                           <br />
                           <asp:CompareValidator ID="cvFecha_acuerdo" runat="server" ControlToValidate="txtFecha_acuerdo" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" />
                       
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Valor acuerdo&nbsp;<br/>
                       <asp:TextBox ID="txtValor_acuerdo" CssClass="textbox" runat="server" MaxLength="128"/>
                           <br />
                           <asp:CompareValidator ID="cvVALOR_ACUERDO" runat="server" 
                               ControlToValidate="txtVALOR_ACUERDO" Display="Dynamic" 
                               ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                       <td class="tdD">
                           Código usuario registra<br/>
                           <asp:TextBox ID="txtCodigo_usuario_regis" runat="server" CssClass="textbox" 
                               MaxLength="128" />
                           <br />
                           <asp:CompareValidator ID="cvCODIGO_USUARIO_REGIS" runat="server" 
                               ControlToValidate="txtCODIGO_USUARIO_REGIS" Display="Dynamic" 
                               ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck" 
                               SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" />
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Observación&nbsp;<br/>
                       <asp:TextBox ID="txtObservacion" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           &nbsp;</td>
                       <td class="tdD">
                       &nbsp;
                       </td>
                </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td><hr width="100%" noshade></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" onrowediting="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  DataKeyNames="CODIGO_DILIGENCIA" >
                    <Columns>
                        <asp:BoundField DataField="CODIGO_DILIGENCIA" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg" ToolTip="Detalle"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco"  ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnBorrar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg" ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CODIGO_DILIGENCIA" HeaderText="Código" />
                        <asp:BoundField DataField="NUMERO_RADICACION" HeaderText="Numero radicación" />
                        <asp:BoundField DataField="FECHA_DILIGENCIA" HeaderText="Fecha diligencia" />
                        <asp:BoundField DataField="TIPO_DILIGENCIA" HeaderText="Tipo" />
                        <asp:BoundField DataField="ATENDIO" HeaderText="Atendió" />
                        <asp:BoundField DataField="RESPUESTA" HeaderText="Respuesta" />
                        <asp:BoundField DataField="ACUERDO" HeaderText="Acuerdo" />
                        <asp:BoundField DataField="FECHA_ACUERDO" HeaderText="Fecha acuerdo" />
                        <asp:BoundField DataField="VALOR_ACUERDO" HeaderText="Valor acuerdo" />
                        <asp:BoundField DataField="ANEXO" HeaderText="Anexo" />
                        <asp:BoundField DataField="OBSERVACION" HeaderText="Observación" />
                        <asp:BoundField DataField="CODIGO_USUARIO_REGIS" HeaderText="Codigo usuario" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server" Visible="False"/>
            </td>
        </tr>
    </table>
  <%--  <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCODIGO_DILIGENCIA').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>