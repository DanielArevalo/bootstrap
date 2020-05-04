<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitud.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - InformacionNegocio :." %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                       Cod_negocio&nbsp;<asp:CompareValidator ID="cvCOD_NEGOCIO" runat="server" ControlToValidate="txtCOD_NEGOCIO" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_negocio" CssClass="textbox" runat="server" MaxLength="128" 
                               Visible="False"/>
                       </td>
                       <td class="tdD">
                       Cod_persona&nbsp;<asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCOD_PERSONA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_persona" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Direccion&nbsp;<br/>
                       <asp:TextBox ID="txtDireccion" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Telefono&nbsp;<br/>
                       <asp:TextBox ID="txtTelefono" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Localidad&nbsp;<br/>
                       <asp:TextBox ID="txtLocalidad" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Nombrenegocio&nbsp;<br/>
                       <asp:TextBox ID="txtNombrenegocio" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Descripcion&nbsp;<br/>
                       <asp:TextBox ID="txtDescripcion" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Antiguedad&nbsp;<asp:CompareValidator ID="cvANTIGUEDAD" runat="server" ControlToValidate="txtANTIGUEDAD" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtAntiguedad" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Propia&nbsp;<asp:CompareValidator ID="cvPROPIA" runat="server" ControlToValidate="txtPROPIA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtPropia" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Arrendador&nbsp;<br/>
                       <asp:TextBox ID="txtArrendador" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Telefonoarrendador&nbsp;<br/>
                       <asp:TextBox ID="txtTelefonoarrendador" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Codactividad&nbsp;<asp:CompareValidator ID="cvCODACTIVIDAD" runat="server" ControlToValidate="txtCODACTIVIDAD" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCodactividad" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Experiencia&nbsp;<asp:CompareValidator ID="cvEXPERIENCIA" runat="server" ControlToValidate="txtEXPERIENCIA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtExperiencia" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Emplperm&nbsp;<asp:CompareValidator ID="cvEMPLPERM" runat="server" ControlToValidate="txtEMPLPERM" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtEmplperm" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Empltem&nbsp;<asp:CompareValidator ID="cvEMPLTEM" runat="server" ControlToValidate="txtEMPLTEM" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtEmpltem" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Fechacreacion&nbsp;<asp:CompareValidator ID="cvFECHACREACION" runat="server" ControlToValidate="txtFECHACREACION" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtFechacreacion" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Usuariocreacion&nbsp;<br/>
                       <asp:TextBox ID="txtUsuariocreacion" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Fecultmod&nbsp;<asp:CompareValidator ID="cvFECULTMOD" runat="server" ControlToValidate="txtFECULTMOD" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtFecultmod" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Usuultmod&nbsp;<br/>
                       <asp:TextBox ID="txtUsuultmod" CssClass="textbox" runat="server" MaxLength="128"/>
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
                <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True" OnPageIndexChanging="gvLista_PageIndexChanging" onselectedindexchanged="gvLista_SelectedIndexChanged" onrowediting="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem"  DataKeyNames="COD_NEGOCIO" >
                    <Columns>
                        <%--<asp:BoundField DataField="COD_NEGOCIO" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />--%>
                        <asp:BoundField DataField="COD_NEGOCIO" HeaderText="Cod negocio" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"  />
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
                        <asp:BoundField DataField="COD_PERSONA" HeaderText="Cod persona" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                        <asp:BoundField DataField="DIRECCION" HeaderText="Dirección" />
                        <asp:BoundField DataField="TELEFONO" HeaderText="Teléfono" />
                        <asp:BoundField DataField="LOCALIDAD" HeaderText="Localidad" />
                        <asp:BoundField DataField="NOMBRENEGOCIO" HeaderText="Nombre negocio" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" Visible="false" />
                        <asp:BoundField DataField="ANTIGUEDAD" HeaderText="Antiguedad" Visible="false" />
                        <asp:BoundField DataField="PROPIA" HeaderText="Propia" Visible="false" />
                        <asp:BoundField DataField="ARRENDADOR" HeaderText="Arrendador" Visible="false"/>
                        <asp:BoundField DataField="TELEFONOARRENDADOR" HeaderText="Telefono arrendador" Visible="false"/>
                        <asp:BoundField DataField="CODACTIVIDAD" HeaderText="Actividad" Visible="false"/>
                        <asp:BoundField DataField="EXPERIENCIA" HeaderText="Experiencia" Visible="false" />
                        <asp:BoundField DataField="EMPLPERM" HeaderText="Emplperm" Visible="false"/>
                        <asp:BoundField DataField="EMPLTEM" HeaderText="Empltem" Visible="false"/>
                        <asp:BoundField DataField="FECHACREACION" HeaderText="Fechacreacion" Visible="false"/>
                        <asp:BoundField DataField="USUARIOCREACION" HeaderText="Usuariocreacion" Visible="false"/>
                        <asp:BoundField DataField="FECULTMOD" HeaderText="Fecultmod" Visible="false"/>
                        <asp:BoundField DataField="USUULTMOD" HeaderText="Usuultmod" Visible="false" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblTotalRegs" runat="server"/>
            </td>
        </tr>
       
        <tr>
            <td>
                        <asp:ImageButton ID="btnAtras" runat="server" 
                    ImageUrl="~/Images/iconAnterior.jpg" onclick="btnAtras_Click1" />
                        <asp:ImageButton ID="btnAdelante" runat="server" 
                    ImageUrl="~/Images/btnAdelante.jpg" onclick="btnAdelante_Click" />
            </td>
        </tr>
       
        </table>
   <%-- <script type="text/javascript" language="javascript">
        function SetFocus()
        {
            document.getElementById('cphMain_txtCOD_NEGOCIO').focus(); 
        }
        window.onload = SetFocus;
    </script>--%>
</asp:Content>