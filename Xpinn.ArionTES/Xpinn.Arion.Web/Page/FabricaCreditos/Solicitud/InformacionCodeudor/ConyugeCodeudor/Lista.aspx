<%@ Page Language="C#" MasterPageFile="~/General/Master/solicitudSub.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" Title=".: Xpinn - Persona1 :." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../../../General/Controles/direccion.ascx" TagName="direccion"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Panel ID="pConsulta" runat="server" Visible="false">
                    <%--   <table id="tbCriterios" border="0" cellpadding="5" cellspacing="0" width="100%">
                   <tr>
                       <td class="tdI">
                           Cod persona&nbsp;<asp:CompareValidator ID="cvCOD_PERSONA" runat="server" ControlToValidate="txtCOD_PERSONA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_persona" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                           Tipo persona&nbsp;<br/>
                       <asp:TextBox ID="txtTipo_persona" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Identificación&nbsp;<br/>
                       <asp:TextBox ID="txtIdentificacion" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                           Digito verificación&nbsp;<asp:CompareValidator ID="cvDIGITO_VERIFICACION" runat="server" ControlToValidate="txtDIGITO_VERIFICACION" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtDigito_verificacion" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Tipo identificación&nbsp;<asp:CompareValidator ID="cvTIPO_IDENTIFICACION" runat="server" ControlToValidate="txtTIPO_IDENTIFICACION" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtTipo_identificacion" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                           Fecha expedición&nbsp;<asp:CompareValidator ID="cvFECHAEXPEDICION" runat="server" ControlToValidate="txtFECHAEXPEDICION" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtFechaexpedicion" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                           Cod ciudadexpedicion&nbsp;<asp:CompareValidator ID="cvCODCIUDADEXPEDICION" runat="server" ControlToValidate="txtCODCIUDADEXPEDICION" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCodciudadexpedicion" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Sexo&nbsp;<br/>
                       <asp:TextBox ID="txtSexo" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Primer_nombre&nbsp;<br/>
                       <asp:TextBox ID="txtPrimer_nombre" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Segundo_nombre&nbsp;<br/>
                       <asp:TextBox ID="txtSegundo_nombre" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Primer_apellido&nbsp;<br/>
                       <asp:TextBox ID="txtPrimer_apellido" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Segundo_apellido&nbsp;<br/>
                       <asp:TextBox ID="txtSegundo_apellido" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Razon_social&nbsp;<br/>
                       <asp:TextBox ID="txtRazon_social" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Fechanacimiento&nbsp;<asp:CompareValidator ID="cvFECHANACIMIENTO" runat="server" ControlToValidate="txtFECHANACIMIENTO" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtFechanacimiento" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Codciudadnacimiento&nbsp;<asp:CompareValidator ID="cvCODCIUDADNACIMIENTO" runat="server" ControlToValidate="txtCODCIUDADNACIMIENTO" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCodciudadnacimiento" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Codestadocivil&nbsp;<asp:CompareValidator ID="cvCODESTADOCIVIL" runat="server" ControlToValidate="txtCODESTADOCIVIL" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCodestadocivil" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Codescolaridad&nbsp;<asp:CompareValidator ID="cvCODESCOLARIDAD" runat="server" ControlToValidate="txtCODESCOLARIDAD" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCodescolaridad" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Codactividad&nbsp;<asp:CompareValidator ID="cvCODACTIVIDAD" runat="server" ControlToValidate="txtCODACTIVIDAD" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCodactividad" CssClass="textbox" runat="server"/>
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
                       Codciudadresidencia&nbsp;<asp:CompareValidator ID="cvCODCIUDADRESIDENCIA" runat="server" ControlToValidate="txtCODCIUDADRESIDENCIA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCodciudadresidencia" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Antiguedadlugar&nbsp;<asp:CompareValidator ID="cvANTIGUEDADLUGAR" runat="server" ControlToValidate="txtANTIGUEDADLUGAR" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtAntiguedadlugar" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Tipovivienda&nbsp;<br/>
                       <asp:TextBox ID="txtTipovivienda" CssClass="textbox" runat="server" MaxLength="128"/>
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
                       Celular&nbsp;<br/>
                       <asp:TextBox ID="txtCelular" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Email&nbsp;<br/>
                       <asp:TextBox ID="txtEmail" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Empresa&nbsp;<br/>
                       <asp:TextBox ID="txtEmpresa" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Telefonoempresa&nbsp;<br/>
                       <asp:TextBox ID="txtTelefonoempresa" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Codcargo&nbsp;<asp:CompareValidator ID="cvCODCARGO" runat="server" ControlToValidate="txtCODCARGO" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCodcargo" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Codtipocontrato&nbsp;<asp:CompareValidator ID="cvCODTIPOCONTRATO" runat="server" ControlToValidate="txtCODTIPOCONTRATO" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCodtipocontrato" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Cod_asesor&nbsp;<asp:CompareValidator ID="cvCOD_ASESOR" runat="server" ControlToValidate="txtCOD_ASESOR" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_asesor" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Residente&nbsp;<br/>
                       <asp:TextBox ID="txtResidente" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Fecha_residencia&nbsp;<asp:CompareValidator ID="cvFECHA_RESIDENCIA" runat="server" ControlToValidate="txtFECHA_RESIDENCIA" ErrorMessage="Formato de Fecha (dd/mm/aaaa)" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtFecha_residencia" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Cod_oficina&nbsp;<asp:CompareValidator ID="cvCOD_OFICINA" runat="server" ControlToValidate="txtCOD_OFICINA" ErrorMessage="Solo se admiten n&uacute;meros" Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="vgGuardar" Display="Dynamic" ForeColor="Red" /><br/>
                       <asp:TextBox ID="txtCod_oficina" CssClass="textbox" runat="server" MaxLength="128"/>
                       </td>
                       <td class="tdD">
                       Tratamiento&nbsp;<br/>
                       <asp:TextBox ID="txtTratamiento" CssClass="textbox" runat="server"/>
                       </td>
                   </tr>
                   <tr>
                       <td class="tdI">
                       Estado&nbsp;<br/>
                       <asp:TextBox ID="txtEstado" CssClass="textbox" runat="server" MaxLength="128"/>
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
                </table>--%>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td class="tdI">
                <strong>Buscar</strong></td>
            <td class="tdI" style="text-align: right">
                <asp:ImageButton ID="btnGuardarConyuge" runat="server" 
                    ImageUrl="~/Images/btnGuardar.jpg" onclick="btnGuardar_Click1" 
                    ValidationGroup="vgGuardar" />
                <asp:ImageButton ID="btnConsultarConyuge" runat="server" 
                    ImageUrl="~/Images/btnConsultar.jpg" onclick="btnConsultar_Click1" />
            </td>
        </tr>
       
        <tr>
            <td class="tdI" style="height: 51px;" colspan="2">
                <asp:GridView ID="gvListaConyuge" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnRowDataBound="gvLista_RowDataBound" OnRowDeleting="gvLista_RowDeleting" AllowPaging="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                    OnRowEditing="gvLista_RowEditing" PageSize="20" HeaderStyle-CssClass="gridHeader"
                    PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" 
                    DataKeyNames="COD_PERSONA">
                    <Columns>
                        <asp:BoundField DataField="COD_PERSONA" HeaderText="Cod_persona" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo"/>
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
                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" ImageUrl="~/Images/gr_elim.jpg"
                                    ToolTip="Borrar" />
                            </ItemTemplate>
                            <ItemStyle CssClass="gI" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TIPO_PERSONA" HeaderText="Tipo_persona" Visible="false" />
                        <asp:BoundField DataField="IDENTIFICACION" HeaderText="Identificacion" />
                        <asp:BoundField DataField="DIGITO_VERIFICACION" HeaderText="Digito_verificacion" Visible="false" />
                        <asp:BoundField DataField="TIPO_IDENTIFICACION" HeaderText="Tipo_identificacion" Visible="false" />
                        <asp:BoundField DataField="FECHAEXPEDICION" HeaderText="Fechaexpedicion" Visible="false" />
                        <asp:BoundField DataField="CODCIUDADEXPEDICION" HeaderText="Codciudadexpedicion" Visible="false" />
                        <asp:BoundField DataField="SEXO" HeaderText="Sexo" Visible="false" />
                        <asp:BoundField DataField="PRIMER_NOMBRE" HeaderText="Primer_nombre" />
                        <asp:BoundField DataField="SEGUNDO_NOMBRE" HeaderText="Segundo_nombre" />
                        <asp:BoundField DataField="PRIMER_APELLIDO" HeaderText="Primer_apellido" />
                        <asp:BoundField DataField="SEGUNDO_APELLIDO" HeaderText="Segundo_apellido" />
                        <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Razon_social" Visible="false" />
                        <asp:BoundField DataField="FECHANACIMIENTO" HeaderText="Fechanacimiento" Visible="false" />
                        <asp:BoundField DataField="CODCIUDADNACIMIENTO" HeaderText="Codciudadnacimiento" Visible="false" />
                        <asp:BoundField DataField="CODESTADOCIVIL" HeaderText="Codestadocivil" Visible="false" />
                        <asp:BoundField DataField="CODESCOLARIDAD" HeaderText="Codescolaridad" Visible="false" />
                        <asp:BoundField DataField="CODACTIVIDAD" HeaderText="Codactividad" Visible="false" />
                        <asp:BoundField DataField="DIRECCION" HeaderText="Direccion" Visible="false" />
                        <asp:BoundField DataField="TELEFONO" HeaderText="Telefono" Visible="false" />
                        <asp:BoundField DataField="CODCIUDADRESIDENCIA" HeaderText="Codciudadresidencia" Visible="false" />
                        <asp:BoundField DataField="ANTIGUEDADLUGAR" HeaderText="Antiguedadlugar" Visible="false" />
                        <asp:BoundField DataField="TIPOVIVIENDA" HeaderText="Tipovivienda" Visible="false" />
                        <asp:BoundField DataField="ARRENDADOR" HeaderText="Arrendador" Visible="false" />
                        <asp:BoundField DataField="TELEFONOARRENDADOR" HeaderText="Telefonoarrendador" Visible="false" />
                        <asp:BoundField DataField="CELULAR" HeaderText="Celular" Visible="false" />
                        <asp:BoundField DataField="EMAIL" HeaderText="Email" Visible="false" />
                        <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" Visible="false" />
                        <asp:BoundField DataField="TELEFONOEMPRESA" HeaderText="Telefonoempresa" Visible="false" />
                        <asp:BoundField DataField="CODCARGO" HeaderText="Codcargo" Visible="false" />
                        <asp:BoundField DataField="CODTIPOCONTRATO" HeaderText="Codtipocontrato" Visible="false" />
                        <asp:BoundField DataField="COD_ASESOR" HeaderText="Cod_asesor" Visible="false" />
                        <asp:BoundField DataField="RESIDENTE" HeaderText="Residente" Visible="false" />
                        <asp:BoundField DataField="FECHA_RESIDENCIA" HeaderText="Fecha_residencia" Visible="false" />
                        <asp:BoundField DataField="COD_OFICINA" HeaderText="Cod_oficina" Visible="false" />
                        <asp:BoundField DataField="TRATAMIENTO" HeaderText="Tratamiento" Visible="false" />
                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" Visible="false" />
                        <asp:BoundField DataField="FECHACREACION" HeaderText="Fechacreacion" Visible="false" />
                        <asp:BoundField DataField="USUARIOCREACION" HeaderText="Usuariocreacion" Visible="false" />
                        <asp:BoundField DataField="FECULTMOD" HeaderText="Fecultmod" Visible="false" />
                        <asp:BoundField DataField="USUULTMOD" HeaderText="Usuultmod" Visible="false" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="tdI">
                <asp:Label ID="lblTotalRegsConyuge" runat="server" Visible="False" />
            </td>
            <td class="tdD" >
            </td>
        </tr>
        <tr>
            <td class="tdI" >
                Primer nombre&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtPrimer_nombreConyuge" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
            <td class="tdD" style="height: 51px">
                Segundo nombre&nbsp;&nbsp;<br />
                <asp:TextBox ID="txtSegundo_nombreConyuge" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="height: 51px; width: 984px;">
                Primer apellido&nbsp;&nbsp;<asp:TextBox ID="txtPrimer_apellidoConyuge" 
                    runat="server" CssClass="textbox"
                    MaxLength="128" />
            </td>
            <td class="tdD" style="height: 51px">
                Segundo apellido<asp:TextBox ID="txtSegundo_apellidoConyuge" 
                    runat="server" CssClass="textbox"
                    MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="height: 51px; width: 984px;">
                Identificación *&nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2Conyuge"
                    runat="server" ControlToValidate="txtIdentificacionConyuge" Display="Dynamic" ErrorMessage="Campo Requerido"
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgConsultar" />
                <br />
                <asp:TextBox ID="txtIdentificacionConyuge" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
            <td class="tdD" style="height: 51px">
                Ciudad expedición<br />
                <asp:DropDownList ID="ddlLugarExpedicionConyuge" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="height: 51px; width: 984px;">
                Ciudadnacimiento&nbsp;<br />
                <asp:DropDownList ID="ddlLugarNacimientoConyuge" runat="server">
                </asp:DropDownList>
            </td>
            <td class="tdD" style="height: 51px">
                Fecha nacimiento&nbsp;&nbsp;<asp:CompareValidator 
                    ID="cvFECHANACIMIENTO0Conyuge" runat="server"
                    ControlToValidate="txtFechanacimientoConyuge" Display="Dynamic" ErrorMessage="Formato de Fecha (dd/mm/aaaa)"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha"
                    Type="Date" ValidationGroup="vgGuardar" />
                <br />
                <asp:TextBox ID="txtFechanacimientoConyuge" runat="server" CssClass="textbox" 
                    MaxLength="128" />
                <asp:CalendarExtender ID="txtFechanacimientoConyuge_CalendarExtender" 
                    runat="server" DaysModeTitleFormat="dd/MM/yyyy"
                    Enabled="True" Format="dd/MM/yyyy" 
                    TargetControlID="txtFechanacimientoConyuge" TodaysDateFormat="dd/MM/yyyy">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 984px">
                Edad<br />
                <asp:Label ID="lblEdadConyuge" runat="server"></asp:Label>
            </td>
            <td class="tdD">
                Dirección<uc1:direccion ID="txtDireccionConyuge" 
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 984px">
                Barrio<br />
                <asp:DropDownList ID="ddlBarrioConyuge" runat="server">
                </asp:DropDownList>
            </td>
            <td class="tdD">
                Teléfono&nbsp;&nbsp;<asp:CompareValidator 
                    ID="cvtxtTelefonoConyuge" runat="server" ControlToValidate="txtTelefonoConyuge"
                    Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                    ValidationGroup="vgGuardar" />
                <asp:TextBox ID="txtTelefonoConyuge" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 984px">
                Tipo vivienda
                <asp:UpdatePanel ID="upTipoViviendaConyuge" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rblTipoViviendaConyuge" runat="server" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rblTipoVivienda_SelectedIndexChanged" 
                            AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="P">Propia</asp:ListItem>
                            <asp:ListItem Value="A">Arrendada</asp:ListItem>
                            <asp:ListItem Value="F">Familiar</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
               
            </td>
            <td class="tdD">
                Arrendador&nbsp;&nbsp;<br />
                <asp:UpdatePanel ID="upArrendadorConyuge" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtArrendadorConyuge" runat="server" CssClass="textbox" 
                            MaxLength="128" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rblTipoViviendaConyuge" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 984px">
                Teléfono Arrendador&nbsp;&nbsp;<asp:CompareValidator ID="cvtxtTelefonoarrendador1Conyuge"
                    runat="server" ControlToValidate="txtTelefonoarrendadorConyuge" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                    ValidationGroup="vgGuardar" />
                <br />
                <asp:UpdatePanel ID="upTelefonoarrendadorConyuge" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtTelefonoarrendadorConyuge" runat="server" 
                            CssClass="textbox" MaxLength="128" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rblTipoViviendaConyuge" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
                
                
            </td>
            <td class="tdD">
                Valor Arriendo<br />
                <asp:UpdatePanel ID="upValorArriendoConyuge" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtValorArriendoConyuge" runat="server" CssClass="textbox" 
                            MaxLength="128" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rblTipoViviendaConyuge" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 984px">
                Estado Civil<br />
                <asp:DropDownList ID="ddlEstadoCivilConyuge" runat="server">
                </asp:DropDownList>
            </td>
            <td class="tdD">
                Numero Hijos
                <asp:CompareValidator ID="cvNumeroHijosConyuge" runat="server" ControlToValidate="txtNumeroHijosConyuge"
                    Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                    ValidationGroup="vgGuardar" />
                <br />
                <asp:TextBox ID="txtNumeroHijosConyuge" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 984px">
                Numero Personas a Cargo
                <asp:CompareValidator ID="cvNumeroPersonasCargoConyuge" runat="server" 
                    ControlToValidate="txtNumeroPersonasCargoConyuge" Display="Dynamic" 
                    ErrorMessage="Solo se admiten números enteros" ForeColor="Red" 
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                    ValidationGroup="vgGuardar" />
                <br />
                <asp:TextBox ID="txtNumeroPersonasCargoConyuge" runat="server" 
                    CssClass="textbox" MaxLength="128" />
            </td>
            <td class="tdD">
                Nivel Academico<br />
                <asp:DropDownList ID="ddlNivelEscolaridadConyuge" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 984px">
                Ocupación<br />
                <asp:TextBox ID="txtOcupacionConyuge" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
            <td class="tdD">
                Empresa Donde Trabaja<br />
                <asp:TextBox ID="txtEmpresaConyuge" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 984px">
                Cargo&nbsp;<br />
                <asp:DropDownList ID="ddlCargoConyuge" runat="server">
                </asp:DropDownList>
            </td>
            <td class="tdD">
                Salario
                <asp:CompareValidator ID="cvSalarioConyuge" runat="server"
                    ControlToValidate="txtSalarioConyuge" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                    ValidationGroup="vgGuardar" />
                <br />
                <asp:TextBox ID="txtSalarioConyuge" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 984px">
                Antiguedad Laboral
                <asp:CompareValidator ID="cvAntiguedadLaboralConyuge" runat="server"
                    ControlToValidate="txtAntiguedadLaboralConyuge" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                    ValidationGroup="vgGuardar" />
                <br />
                <asp:TextBox ID="txtAntiguedadLaboralConyuge" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
            <td class="tdD">
                Tipo de Contrato<br />
                <asp:DropDownList ID="ddlTipoContratoConyuge" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 984px">
                Direccion Trabajo<uc1:direccion ID="txtDireccionTrabajoConyuge" 
                    runat="server" />
            </td>
            <td class="tdD">
                <br />
                Teléfono Trabajo&nbsp;&nbsp;<asp:CompareValidator 
                    ID="cvtxtTelefonoempresaConyuge" runat="server"
                    ControlToValidate="txtTelefonoempresaConyuge" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                    ValidationGroup="vgGuardar" />
                <br />
                <asp:TextBox ID="txtTelefonoempresaConyuge" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 984px">
                Celular&nbsp;<asp:CompareValidator ID="cvtxtCelularConyuge" runat="server" ControlToValidate="txtCelularConyuge"
                    Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                    ValidationGroup="vgGuardar" />
                &nbsp;<asp:TextBox ID="txtCelularConyuge" runat="server" CssClass="textbox" 
                    MaxLength="128" />
            </td>
            <td class="tdD">
                Parentesco con el Titular del Credito<br />
                <asp:DropDownList ID="ddlParentescoConyuge" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdI" style="width: 984px">
                Opinion que Tiene del Titular del Crédito<asp:RadioButtonList 
                    ID="rblOpinionConyuge" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="B">Buena</asp:ListItem>
                    <asp:ListItem Value="R">Regular</asp:ListItem>
                    <asp:ListItem Value="M">Mala</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="tdD">
                Sabe la Responsabilidad que Asume Como Codeudor del Crédito<asp:RadioButtonList ID="rblResponsabilidadConyuge"
                    runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:RadioButtonList>
                <asp:TextBox ID="txtCodConyuge" runat="server" Height="22px"></asp:TextBox>
                <asp:TextBox ID="txtCodCodeudor" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdI" colspan="2">
                <asp:Panel ID="Panel3" runat="server" Visible="False">
                    <table border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdD">
                                <br />
                                Tipo identificación&nbsp;&nbsp;<br />
                                <asp:DropDownList ID="ddlTipoConyuge" runat="server">
                                </asp:DropDownList>
                                <br />
                                Ciudadresidencia<br />
                                <asp:DropDownList ID="ddlLugarResidenciaConyuge" runat="server">
                                </asp:DropDownList>
                                <br />
                                Tipo persona<asp:RadioButtonList ID="rblTipoPersonaConyuge" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="N">Natural</asp:ListItem>
                                    <asp:ListItem Value="J">Juridica</asp:ListItem>
                                </asp:RadioButtonList>
                                Razon social<br />
                                <asp:TextBox ID="txtRazon_socialConyuge" runat="server" CssClass="textbox" 
                                    MaxLength="128" />
                                <br />
                                Fecha expedición&nbsp;&nbsp;<asp:CompareValidator 
                                    ID="cvFECHAEXPEDICION0Conyuge" runat="server"
                                    ControlToValidate="txtFechaexpedicionConyuge" Display="Dynamic" ErrorMessage="Formato de Fecha (dd/mm/aaaa)"
                                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha"
                                    Type="Date" ValidationGroup="vgGuardar" />
                                <br />
                                <asp:TextBox ID="txtFechaexpedicionConyuge" runat="server" CssClass="textbox" 
                                    MaxLength="128" />
                                <br />
                                Actividad<br />
                                <asp:DropDownList ID="ddlActividadConyuge" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:DropDownList ID="ddlEstadoConyuge" runat="server">
                                </asp:DropDownList>
                                Cod oficina&nbsp;*&nbsp;<asp:RequiredFieldValidator ID="rfvCOD_OFICINA2Conyuge" runat="server"
                                    ControlToValidate="txtCod_oficinaConyuge" Display="Dynamic" ErrorMessage="Campo Requerido"
                                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar" />
                                <asp:CompareValidator ID="cvCOD_OFICINA0Conyuge" runat="server" ControlToValidate="txtCod_oficinaConyuge"
                                    Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                                    ValidationGroup="vgGuardar" />
                                <br />
                                <asp:TextBox ID="txtCod_oficinaConyuge" runat="server" CssClass="textbox" 
                                    MaxLength="128">1</asp:TextBox>
                                <br />
                                Cod asesor&nbsp;&nbsp;<asp:CompareValidator ID="cvCOD_ASESORConyuge" 
                                    runat="server" ControlToValidate="txtCod_asesorConyuge"
                                    Display="Dynamic" ErrorMessage="Solo se admiten números enteros" ForeColor="Red"
                                    Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer" 
                                    ValidationGroup="vgGuardar" />
                                <br />
                                <asp:TextBox ID="txtCod_asesorConyuge" runat="server" CssClass="textbox" 
                                    MaxLength="128" />
                                <br />
                                Fecha residencia&nbsp;&nbsp;<asp:CompareValidator 
                                    ID="cvFECHA_RESIDENCIAConyuge" runat="server"
                                    ControlToValidate="txtFecha_residenciaConyuge" Display="Dynamic" ErrorMessage="Formato de Fecha (dd/mm/aaaa)"
                                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" ToolTip="Formato fecha"
                                    Type="Date" ValidationGroup="vgGuardar" />
                                <br />
                                <asp:TextBox ID="txtFecha_residenciaConyuge" runat="server" CssClass="textbox" 
                                    MaxLength="128" />
                                <br />
                                Residente&nbsp;&nbsp;<asp:RadioButtonList ID="rblResidenteConyuge" 
                                    runat="server" AutoPostBack="True"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="S">Si</asp:ListItem>
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                </asp:RadioButtonList>
                                Email&nbsp;&nbsp;<asp:TextBox ID="txtEmailConyuge" runat="server" 
                                    CssClass="textbox" MaxLength="128" />
                                <br />
                                Tratamiento&nbsp;&nbsp;<br />
                                <asp:TextBox ID="txtTratamientoConyuge" runat="server" CssClass="textbox" 
                                    MaxLength="128" />
                                <br />
                                Antiguedad lugar&nbsp;&nbsp;<asp:CompareValidator ID="cvANTIGUEDADLUGAR0Conyuge" runat="server"
                                    ControlToValidate="txtAntiguedadlugarConyuge" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                    ValidationGroup="vgGuardar" />
                                <br />
                                <asp:TextBox ID="txtAntiguedadlugarConyuge" runat="server" CssClass="textbox" 
                                    MaxLength="128" />
                                <br />
                                Sexo&nbsp;&nbsp;<asp:RadioButtonList ID="rblSexoConyuge" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">F</asp:ListItem>
                                    <asp:ListItem>M</asp:ListItem>
                                </asp:RadioButtonList>
                                Digito verificación&nbsp;&nbsp;<asp:CompareValidator ID="cvDIGITO_VERIFICACION0Conyuge"
                                    runat="server" ControlToValidate="txtDigito_verificacionConyuge" Display="Dynamic" ErrorMessage="Solo se admiten números enteros"
                                    ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                    ValidationGroup="vgGuardar" />
                                <br />
                                <asp:TextBox ID="txtDigito_verificacionConyuge" runat="server" 
                                    CssClass="textbox" MaxLength="128" />
                            </td>
                        </tr>
                       
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <%-- <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                    ToolTip="Modificar" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
</asp:Content>
