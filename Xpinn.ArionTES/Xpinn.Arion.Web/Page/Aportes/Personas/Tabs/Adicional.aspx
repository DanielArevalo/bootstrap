<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Adicional.aspx.cs" Inherits="Page_Aportes_Personas_Tabs_Personal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlNumero.ascx" TagName="numero" TagPrefix="uc5" %>
<%@ Register Src="~/General/Controles/ctlFormaPago.ascx" TagName="Forma" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/ctlFormatoDocum.ascx" TagName="FormatoDocu" TagPrefix="uc4" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        * {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }

        .btn8 {
            background-color: #0099ff;
            color: #fff;
            border: 2px solid #0099ff;
            margin: 10px auto;
            font-size: 12px;
        }
    </style>
    <title></title>
    <script>
        //var Hoy = new Date();
        //document.getElementById('txtFechaAfili').value = Hoy.format("yyyy-MM-dd");
    </script>
</head>
<body>


    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="text-align: right">
                    <asp:ImageButton runat="server" ID="GuardarAdicional" ImageUrl="~/Images/btnGuardar.jpg" ToolTip="Guardar" OnClientClick="validar_empre" OnClick="btnGuardarAdicional_click" UseSubmitBehavior="false" TabIndex="27" />
                </div>
        <asp:UpdatePanel ID="UdpContenido" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="text-align: right">
                    <asp:Button runat="server" ID="btnActEmpresas" Text="Guardar Inf.Beneficiarios" Style="display: none;" OnClick="ActListEmpresa_click" UseSubmitBehavior="false" />
                </div>
                <div>
                    <asp:Label ID="lblerror" Text="" runat="server" />
                </div>
                <div id="contenido">
<%--                    <table style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
                        <tr style="text-align: center;">
                            <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                                <strong>Documentos</strong>
                            </td>
                        </tr>
                    </table>--%>
                    <%--<asp:Button ID="btnAgregarDocumento" runat="server" CssClass="btn8" TabIndex="1" OnClick="btnAgregarFilaDocumento_Click" Text="+ Adicionar Detalle" />
                    <br />
                    <asp:GridView ID="gvDocumentos"
                        runat="server" AllowPaging="True" TabIndex="2" AutoGenerateColumns="false" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idimagen"
                        ForeColor="Black" GridLines="Both" OnRowDataBound="gvDocumentos_RowDataBound"
                        OnRowDeleting="gvDocumentos_RowDeleting" PageSize="10" ShowFooter="True"
                        Style="font-size: xx-small" Width="50%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                            <asp:TemplateField HeaderText="Archivo">
                                <ItemTemplate>
                                    <asp:FileUpload ID="FluArchivo" runat="server" />
                                    <asp:Label ID="lblArchivo" runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="txtNombreArchivo" runat="server" Text='<%# Bind("idimagen") %>'></asp:Label>
                                    <cc1:DropDownListGrid ID="ddlTipo" runat="server"
                                            AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                            Style="text-align: left" Width="145px">
                                    </cc1:DropDownListGrid>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkView" runat="server" Text="Ver" OnClick="lnkView_Click" CommandArgument='<%#Eval("idimagen") %>'>
                                                              
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>--%>
                    <table cellpadding="0" cellspacing="0" style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
                        <tr style="text-align: center;">
                            <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                                <strong>Actividades Culturales, Sociales y Deportivas</strong>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnDetalle" runat="server" CssClass="btn8" TabIndex="3" OnClick="btnDetalle_Click"
                        Text="+ Adicionar Detalle" />
                    <asp:GridView ID="gvActividades"
                        runat="server" AllowPaging="True" TabIndex="4" AutoGenerateColumns="false" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idactividad"
                        ForeColor="Black" GridLines="Both" OnRowDataBound="gvActividades_RowDataBound"
                        OnRowDeleting="gvActividades_RowDeleting" OnRowEditing="gvActividades_RowEditing"
                        PageSize="10" ShowFooter="True" Style="font-size: xx-small" Width="80%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="../../../Images/gr_edit.jpg" ShowEditButton="true"
                                Visible="false" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center"/>
                            <asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblactividad" runat="server" Text='<%# Bind("idactividad") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <uc1:fecha ID="txtfecha" runat="server" CssClass="textbox" Enabled="True" Habilitado="True"
                                        style="font-size: xx-small; text-align: left" Text='<%# Eval("fecha_realizacion", "{0:" + FormatoFecha() + "}") %>'
                                        TipoLetra="XX-Small" Width_="80" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actividad" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoActividad" runat="server" Text='<%# Bind("tipo_actividad") %>'
                                        Visible="false"></asp:Label><cc1:DropDownListGrid ID="ddlActividad" runat="server"
                                            AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                            Style="text-align: left" Width="145px">
                                        </cc1:DropDownListGrid>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripción" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                        Text='<%# Bind("descripcion") %>' Width="185px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="190px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Participante" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblParticipante" runat="server" Text='<%# Bind("participante") %>'
                                        Visible="false"></asp:Label><cc1:DropDownListGrid ID="ddlParticipante" runat="server"
                                            AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                            Style="text-align: left" Width="140px">
                                        </cc1:DropDownListGrid>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="145px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Calificación" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCalificacion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                        Text='<%# Bind("calificacion") %>' Width="95px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duración" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDuracion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                        Text='<%# Bind("duracion") %>' Width="95px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                    <table style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
                        <tr style="text-align: center;">
                            <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                                <strong>Información adicional</strong>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvInfoAdicional" runat="server" AllowPaging="True" AutoGenerateColumns="false" TabIndex="4"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" OnPageIndexChanging="gvInfoAdicional_PageIndexChanging"
                        CellPadding="0" DataKeyNames="" ForeColor="Black" GridLines="Both" OnRowDataBound="gvInfoAdicional_RowDataBound"
                        PageSize="10" ShowFooter="False" ShowHeader="False" ShowHeaderWhenEmpty="False"
                        Style="font-size: xx-small" Width="80%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblidinfadicional" runat="server" Text='<%# Bind("idinfadicional") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="codigo" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblcod_infadicional" runat="server" Text='<%# Bind("cod_infadicional") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripcion" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbldescripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Control" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblopcionaActivar" runat="server" Text='<%# Bind("tipo") %>' Visible="false"></asp:Label><asp:TextBox ID="txtCadena" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                        Text='<%# Bind("valor") %>' Visible="false" Width="240px"></asp:TextBox><asp:TextBox ID="txtNumero" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                            Text='<%# Bind("valor") %>' Visible="false" Width="150px"> </asp:TextBox><asp:FilteredTextBoxExtender ID="ftb1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                                TargetControlID="txtNumero" ValidChars="" />
                                    <uc1:fecha ID="txtctlfecha" runat="server" CssClass="textbox" Enabled="True" Habilitado="True"
                                        style="font-size: xx-small; text-align: left" Text='<%# Eval("valor", "{0:" + FormatoFecha() + "}") %>'
                                        TipoLetra="xx-Small" Visible="false" />
                                    <asp:Label ID="lblValorDropdown" runat="server" Text='<%# Bind("valor") %>' Visible="false"></asp:Label><asp:Label ID="lblDropdown" runat="server" Text='<%# Bind("items_lista") %>' Visible="false"></asp:Label><cc1:DropDownListGrid
                                        ID="ddlDropdown" runat="server" AppendDataBoundItems="True" CommandArgument="<%#Container.DataItemIndex %>"
                                        CssClass="textbox" Style="font-size: xx-small; text-align: left" Visible="false"
                                        Width="160px">
                                    </cc1:DropDownListGrid>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="gridHeader" />
                        <HeaderStyle CssClass="gridHeader" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                    </asp:GridView>
                    <table style="text-align: left; width: 100%; padding-top: 15px; padding-bottom: 10px;">
                        <tr style="text-align: center;">
                            <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                                <strong>Afiliación</strong>
                            </td>
                        </tr>
                    </table>
                    <div style="text-align: left">
                        <strong>Tipo Cliente</strong> &#160;&#160;&#160;&#160;
					<asp:CheckBox ID="chkAsociado" runat="server" Text=" Asociado " Checked="false" onChange="datosAfili()" TabIndex="5" />
                    </div>

                    <asp:Panel ID="DatosAfi" runat="server" Style="display: none">
                        <table border="0">
                            <tr>
                                <td colspan="6">
                                    <asp:UpdatePanel ID="upPEPS" runat="server" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <table border="0">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:CheckBox ID="chkPEPS" runat="server" Text=" PEPS " OnChange="PanelPeps()" />
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:UpdatePanel ID="panelPEPS" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <table border="0">
                                                                    <tr>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblCargoPEPS" runat="server" Text="Cargo" TabIndex="6" /><br />
                                                                            <asp:DropDownList ID="ddlCargoPEPS" runat="server" CssClass="textbox" Style="text-align: left" Visible="true" Width="250px" MaxLength="200" />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblInstitucion" runat="server" Text="Institución" TabIndex="7" /><br />
                                                                            <asp:TextBox ID="txtInstitucion" runat="server" CssClass="textbox" Style="text-align: left" Visible="true" Width="200px" MaxLength="200" />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblFechaVinculacionPEPS" runat="server" Text="Fec. Vinculación" TabIndex="8" /><br />
                                                                            <asp:TextBox ID="txtFechaVinculacionPEPS" type="date" runat="server" name="user_date" />
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblFechaDesvinculacionPEPS" runat="server" Text="Fec. Desvinculación" TabIndex="9" /><br />
                                                                            <asp:TextBox ID="txtFechaDesVinculacionPEPS" type="date" runat="server" name="user_date" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <%--                                                         <asp:AsyncPostBackTrigger ControlID="txtIdent_cony" EventName="TextChanged" />--%>
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:CheckBox ID="chkAdmiRecursosPublicos" runat="server" Text=" Administra Recursos Públicos " TabIndex="10" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:CheckBox ID="chkMiembroAministracion" runat="server" Text="Es Miembro de Administración" />
                                                    </td>
                                                </tr>
                                                <tr>                                                                
                                                    <td style="text-align: left">
                                                        <asp:CheckBox ID="ChkMiembroControl" runat="server" Text="Es Miembro de Control" />
                                                    </td>
                                                </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 155px">Fec. de Afiliación<br />
                                    <asp:TextBox ID="txtcodAfiliacion" runat="server" CssClass="textbox" Style="text-align: right"
                                        Visible="false" Width="100px" />
                                    <asp:TextBox ID="txtFechaAfili" type="date" required runat="server" name="user_date" TabIndex="11" />
                                    <br />
                                </td>
                                <td style="text-align: left; width: 170px">Estado<br />
                                    <asp:DropDownList ID="ddlEstadoAfi" runat="server" required AppendDataBoundItems="True" TabIndex="12" onChange="Estado(this.value)"
                                        CssClass="textbox" Width="160px">
                                    </asp:DropDownList></td>
                                <td style="text-align: left; width: 140px">
                                    <asp:Label ID="lblFechaRet" runat="server" Text="Fec. de Rétiro" /><br />
                                    <asp:TextBox ID="txtFechaRetiro" type="date" runat="server" name="user_date" TabIndex="13" />

                                </td>
                                <td style="text-align: left; width: 170px">Forma de Pago<br />
                                    <asp:DropDownList ID="ddlFormPag" runat="server" onChange="empresa(this.value)"
                                        Width="95%" TabIndex="14" AutoPostBack="true" OnSelectedIndexChanged="ddlFormPag_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Caja</asp:ListItem>
                                        <asp:ListItem Value="2">Nomina</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" /><br />
                                    <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="textbox" Width="180px" TabIndex="15">
                                        <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblAsociadosEspeciales" runat="server" Text="Asociados especiales" /><br />
                                    <asp:DropDownList ID="ddlAsociadosEspeciales" runat="server" CssClass="textbox" Width="180px" TabIndex="16">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">Valor<br />
                                    <uc1:decimales ID="txtValorAfili" runat="server" style="text-align: right;" TabIndex="17" />
                                </td>
                                <td style="text-align: left;">Fec. de 1er Pago<br />
                                    <asp:TextBox ID="txtFecha1Pago" type="date" runat="server" name="user_date" TabIndex="18" />
                                </td>
                                <td style="text-align: left;">Nro Cuotas<br />
                                    <asp:TextBox ID="txtCuotasAfili" runat="server" CssClass="textbox" Style="text-align: right"
                                        Width="100px" TabIndex="19" /><asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender"
                                            runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtCuotasAfili"
                                            ValidChars="" />
                                </td>
                                <td style="text-align: left;">Periodicidad<br />
                                    <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="textbox" Width="95%" TabIndex="20"></asp:DropDownList></td>
                                <td style="text-align: left;">
                                    <asp:CheckBox ID="chkAsistioUltAsamblea" runat="server" Text="Asistio a la Ultima Asamblea"
                                        TextAlign="Left" TabIndex="21" /></td>
                                <td style="text-align: left;">Asesor Comercial:
											<br />
                                    <asp:DropDownList ID="ddlAsesor" runat="server" Width="95%" CssClass="textbox" AppendDataBoundItems="true" TabIndex="22">
                                        <asp:ListItem Value="">Seleccione un Item</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <asp:CheckBox ID="ckAfiliadoOtraOS" runat="server" Text="¿Es asociado a otra O.S.?" OnCheckedChanged="ckAfiliadoOtraOS_CheckedChanged" AutoPostBack="true" TabIndex="23"></asp:CheckBox>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblOtraOS" runat="server" Text="Nombre de la Entidad" Visible="false" />
                                    <asp:TextBox ID="txtOtraOS" runat="server" CssClass="textbox" Style="text-align: right;" Visible="false" Width="100px" TabIndex="24" />
                                </td>
                                <td style="text-align: left;">
                                    <asp:CheckBox ID="ckCargosOS" runat="server" Text="¿Ha ocupado cargos directivos en O.S.?" OnCheckedChanged="ckCargosOS_CheckedChanged" AutoPostBack="true" TabIndex="25" />
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblCargoOS" runat="server" Text="Cargo Ocupado" Visible="false" />
                                    <asp:TextBox ID="txtCargosDirectivos" runat="server" CssClass="textbox" Style="text-align: right;" Visible="false" Width="100px" TabIndex="25" />
                                </td>
                                <td style="text-align: left;">No. de Asistencias a Asambleas
						            <asp:TextBox ID="txtNoAsistencias" runat="server" CssClass="textbox" Style="text-align: right" Width="100px" TabIndex="26" />
                                    <asp:FilteredTextBoxExtender ID="fteAsistencias" runat="server" Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtNoAsistencias"
                                        ValidChars="" />
                                </td>
                                <td style="text-align: left;"></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>                
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnDetalle" EventName="Click" />
<%--                <asp:AsyncPostBackTrigger ControlID="btnAgregarDocumento" EventName="Click" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
<script type="text/javascript" >
    //Oculta controles   
    document.getElementById('DatosAfi').style.display = "none";
    document.getElementById('lblFechaRet').style.display = "none";
    document.getElementById('txtFechaRetiro').style.display = "none";
    //asigna valores    
    //document.getElementById('ddlFormaPago').value = "1";{
    if (document.getElementById('ddlEstadoAfi').value == "0") {
        document.getElementById('ddlEstadoAfi').value = "A";
    }

    if (document.getElementById('chkAsociado').checked) {
        document.getElementById('DatosAfi').style.display = "inline";
    }

    //Funciones
    function datosAfili() {
        if (document.getElementById('chkAsociado').checked) {
            document.getElementById('DatosAfi').style.display = "inline";

        } else {
            document.getElementById('DatosAfi').style.display = "none";
        }
    }
    function PanelPeps() {
        if (document.getElementById('chkPEPS').checked) {
            document.getElementById('panelPEPS').style.display = "inline";
        } else {
            document.getElementById('panelPEPS').style.display = "none";
        }
    }

    function empresa(valor) {
        if (valor == "2") {
            document.getElementById('ddlEmpresa').style.display = "inline";
            document.getElementById('lblEmpresa').style.display = "inline";
        } else {
            document.getElementById('ddlEmpresa').style.display = "none";
            document.getElementById('lblEmpresa').style.display = "none";
        }
        PanelPeps();
    }

    function Estado(valor) {
        if (valor != "R") {
            document.getElementById('lblFechaRet').style.display = "none";
            document.getElementById('txtFechaRetiro').style.display = "none";
        } else {
            document.getElementById('lblFechaRet').style.display = "inline";
            document.getElementById('txtFechaRetiro').style.display = "inline";
        }
    }

    function validar_empre() {
        var formpag = document.getElementById('ddlFormaPago').value;
        var emp = document.getElementById('ddlEmpresa').value;
        if (formpag == "2") {
            if (emp == "" || emp == "0") {
                alert("Debe selecccionar una empresa");
                return false;
            }
        }
        return true;
    }

</script>
</html>
