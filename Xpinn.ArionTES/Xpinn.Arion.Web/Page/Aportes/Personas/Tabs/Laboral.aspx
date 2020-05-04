<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Laboral.aspx.cs" Inherits="Page_Aportes_Personas_Tabs_Laboral" %>

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

        .required:invalid {
            border: 1px solid #ff6a00; /*ff6a00  F28C00*/
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <asp:Label ID="lblerror" Text="" runat="server" />
        </div>
        <div id="contenido">
            <table style="text-align: left; padding-top: 15px; width: 100%;">
                <tr style="text-align: center;">
                    <td colspan="8" style="color: #FFFFFF; background-color: #5295fa; height: 30px; width: 100%;">
                        <strong>Información Laboral</strong>
                    </td>
                </tr>
            </table>
            <table style="text-align: left; padding-top: 15px; width: 100%;">
                <tr>
                    <td style="text-align: left; width: 60%;" colspan="3">
                        <strong>Información Laboral</strong>
                    </td>
                </tr>
                <tr>
                    <td class="datoEmpleado" style="text-align: left; width: 35%">Tipo Empresa *										
                        <br />
                        <asp:DropDownList ID="ddlTipoEmpresa" runat="server" Width="170px" CssClass="textbox required"
                            AppendDataBoundItems="True" TabIndex="1" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                        <%--         <asp:DropDownList ID="ddlTipoEmpresa" runat="server" Width="170px" CssClass="textbox required"
                            AppendDataBoundItems="True" TabIndex="3" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>--%>
                    </td>
                    <td class="datoEmpleado" style="text-align: left; width: 35%">Empresa *										
                        <br />
                        <asp:TextBox ID="txtEmpresa" runat="server" CssClass="textbox required" MaxLength="120" Style="text-transform: uppercase"
                            Width="90%" TabIndex="2" required="required"></asp:TextBox>
                    </td>
                    <td class="datoEmpleado" style="text-align: left; width: 35%">Nit Empresa										
                        <br />
                        <asp:TextBox ID="txtNitEmpresa" runat="server" CssClass="textbox" MaxLength="120" Style="text-transform: uppercase"
                            Width="90%" TabIndex="3"></asp:TextBox><asp:FilteredTextBoxExtender ID="ft0n" runat="server"
                                Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtNitEmpresa" ValidChars="-()" />
                    </td>
                </tr>
                <tr>                   
                    <td class="datoEmpleado" style="text-align: left;" runat="server" id="pruebasd">Actividad CIIU *
                        <br />
                        <asp:TextBox ID="txtCIIUEmpresa" CssClass="textbox required" runat="server" Width="170px" TabIndex="4" required="required"></asp:TextBox>
                        <asp:HiddenField ID="hfActEmpresa" runat="server" ClientIDMode="Static" />
                        <asp:PopupControlExtender ID="PopupControlExtender3" runat="server"
                            Enabled="True" ExtenderControlID="" TargetControlID="txtCIIUEmpresa"
                            PopupControlID="pActEmpresa" OffsetY="22">
                        </asp:PopupControlExtender>
                        <asp:Panel ID="pActEmpresa" runat="server" Height="200px" Width="400px"
                            BorderStyle="Solid" BorderWidth="2px" Direction="LeftToRight"
                            ScrollBars="Auto" BackColor="#CCCCCC" Style="display: none">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 30%">
                                        <asp:TextBox ID="txtBuscarCodigo2" onkeypress="return isNumber(event)" CssClass="textbox" runat="server" Width="90%" placeholder="Código"></asp:TextBox>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:TextBox ID="txtBuscarDescripcion2" CssClass="textbox" runat="server" Width="90%" placeholder="Descripción"></asp:TextBox>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:ImageButton ID="imgBuscar2"  ImageUrl="../../../../Images/Lupa.jpg"	 Height="25px" runat="server" OnClick="imgBuscar2_Click" CausesValidation="false" UseSubmitBehavior="false" />
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvActEmpresa" runat="server" Width="100%" AutoGenerateColumns="False"
                                HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" DataKeyNames="ListaId" OnRowDataBound="gvActEmpresa_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_codigo" runat="server" Text='<%# Bind("ListaIdStr") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_descripcion" runat="server" Text='<%# Bind("ListaDescripcion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridIco" HeaderText="Principal">
                                        <ItemTemplate>
                                            <cc1:CheckBoxGrid ID="chkPrincipal" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                                AutoPostBack="false" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                    <td class="datoEmpleado" style="text-align: left;">Actividad Económica *<br />
                        <asp:DropDownList ID="ddlActividadE0" runat="server" Width="170px" CssClass="textbox required"
                            AppendDataBoundItems="True" TabIndex="5" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                    <td rowspan="6" style="text-align: left; vertical-align: sub">Empresa Recaudo
                        <br />
                        <div style="overflow: scroll; max-height: 260px;">
                            <asp:Label Visible="false" runat="server" Text="Empresas para Recaudo:" ID="lblempresas"></asp:Label><br />
                            <asp:CheckBox ID="ckOrdenarPagadurias" runat="server" Text="Ordenar por orden alfabetico" OnCheckedChanged="ordenarPagadurias" AutoPostBack="true" TabIndex="23"></asp:CheckBox>
                            <asp:GridView ID="gvEmpresaRecaudo" runat="server" AllowPaging="False" TabIndex="6"
                                AutoGenerateColumns="false" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                BorderWidth="1px" CellPadding="0" DataKeyNames="idempresarecaudo" ForeColor="Black"
                                GridLines="Both" PageSize="10" ShowFooter="True" Style="font-size: xx-small"
                                Width="95%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblidempresarecaudo" runat="server" Text='<%# Bind("idempresarecaudo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Activ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcodempresa" runat="server" Text='<%# Bind("cod_empresa") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Empresa" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                Text='<%# Bind("descripcion") %>' Width="170px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="updcheck" runat="server">
                                                <ContentTemplate>
                                                    <cc1:CheckBoxGrid ID="chkSeleccionar" runat="server" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("seleccionar")) %>'
                                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' OnCheckedChanged="chkSeleccionar_CheckedChanged" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="chkSeleccionar" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="100px" />
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
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="datoEmpleado" style="text-align: left; width: 35%">Cargo *										
                        <br />
                        <asp:DropDownList ID="ddlCargo" runat="server" CssClass="textbox required" Width="170px" TabIndex="6" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="datoEmpleado" style="text-align: left; width: 35%">Tipo Contrato *<br />
                        <asp:DropDownList ID="ddlTipoContrato" runat="server" Width="170px" CssClass="textbox required"
                            AppendDataBoundItems="True" TabIndex="7" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="datoEmpleado" style="text-align: left;">Fec. Ingreso*<br />
                        <asp:TextBox ID="txtFechaIngreso" class="required" type="date" runat="server" onblur="tiempoMeses(this.value)" name="user_date" TabIndex="8" required="required" />
                    </td>
                    <td class="datoEmpleado" style="text-align: left;">Antigüedad *<br />
                        <asp:TextBox ID="txtAntiguedadlugarEmpresa" runat="server" CssClass="textbox"
                            Enabled="false" MaxLength="128" Width="158px"></asp:TextBox>
                        Meses</td>
                </tr>
                <tr>
                    <td class="datoEmpleado" style="text-align: left;">Sector *
                        <br />
                        <asp:DropDownList ID="ddlSector" runat="server" Width="170px" CssClass="textbox required"
                            AppendDataBoundItems="True" TabIndex="9" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="datoEmpleado" style="text-align: left;">Codigo Nomina
                        <br />
                        <asp:TextBox ID="txtCodigoEmpleado" runat="server" CssClass="textbox" MaxLength="120" Style="text-transform: uppercase"
                            Width="170px" TabIndex="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="datoEmpleado" style="text-align: left;">Escalafón Salarial *
                        <br />
                        <asp:DropDownList ID="ddlescalafon" runat="server" Width="170px" CssClass="textbox required"
                            AppendDataBoundItems="True" TabIndex="12" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="datoEmpleado" style="text-align: left">
                        <asp:CheckBox ID="chkEmpleadoEntidad" runat="server" TabIndex="13" Text="Es empleado de la entidad solidaria?"
                            TextAlign="Left" />
                    </td>
                    <td class="datoEmpleado" style="text-align: left">Jornada Laboral *
                        <asp:RadioButtonList ID="rblJornadaLaboral" runat="server" TabIndex="14" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="1">Tiempo Total</asp:ListItem>
                            <asp:ListItem Value="2">Tiempo Parcial</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="datoEmpleado" colspan="3" style="text-align: left"><strong>Ubicación de la Empresa</strong> </td>
                </tr>
                <tr>
                    <td class="datoEmpleado" style="text-align: left;">Tipo de Ubicación *
                        <br />
                        <asp:DropDownList ID="ddlTipoUbicEmpresa" runat="server" AppendDataBoundItems="True"
                            CssClass="textbox required" Width="170px" TabIndex="15" AutoPostBack="False" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Urbana"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Rural"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="datoEmpleado" style="text-align: left">Dirección de la empresa *
                        <br />
                        <asp:TextBox ID="txtDireccionEmpresa" runat="server" Width="90%" CssClass="textbox required"
                            TabIndex="16" required="required"></asp:TextBox>
                    </td>
                    <td class="datoEmpleado" style="text-align: left">Ciudad *
                        <br />
                        <asp:DropDownList ID="ddlCiu0" runat="server" Width="170px" CssClass="textbox required"
                            AppendDataBoundItems="True" TabIndex="17" required="required">
                            <asp:ListItem Value="" Text="Seleccione un item"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="datoEmpleado" style="text-align: left">Celular *
                        <br />
                        <asp:TextBox ID="txtTelCell0" runat="server" CssClass="textbox required" MaxLength="128" TabIndex="18"
                            Width="170px" required="required"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte60" runat="server"
                                Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtTelCell0" ValidChars="-()" />
                    </td>
                    <td class="datoEmpleado" style="text-align: left">Tel. Empresa
                        <br />
                        <asp:TextBox ID="txtTelefonoempresa" runat="server" CssClass="textbox" TabIndex="19"
                            MaxLength="128" Width="170px"></asp:TextBox><asp:FilteredTextBoxExtender ID="fte58"
                                runat="server" FilterType="Numbers, Custom" TargetControlID="txtTelefonoempresa"
                                ValidChars="-()" />
                    </td>
                    <td class="datoEmpleado" style="text-align: left">
                        <asp:Label Visible="false" runat="server" Text="Email y/o Sitio Web" ID="lblemailweb"></asp:Label>
                        <asp:TextBox ID="txtWebEmp" runat="server" Width="90%" CssClass="textbox" TabIndex="20" Visible="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Button runat="server" ID="GuardarLaboral" Text="Guardar Inf.Laboral" Style="display: none;" OnClick="btnGuardarLaboral_click" UseSubmitBehavior="false" />
        </div>
    </form>
</body>
<script type="text/javascript">

    function checkDate(sender, args) {
        if (sender._selectedDate > new Date()) {
            var hoy = new Date();
            alert("Eliga una fecha inferior a la Actual! " + hoy.toDateString());
            sender._selectedDate = new Date();
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
        }
    }

    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }

    function tiempoMeses(Fecha) {
        fecha = new Date(Fecha)
        hoy = new Date()
        ed = parseInt((hoy - fecha) / 30 / 24 / 60 / 60 / 1000) // o / 1000 / 60 / 60 / 24 / 30 )
        var txta = document.getElementById('<%=txtAntiguedadlugarEmpresa.ClientID %>');
        txta.value = ed;
    }

    function MostrarCIIUPrincipalEmp(pCodigo, pDescripcion) {
        document.getElementById('<%= txtCIIUEmpresa.ClientID %>').value = pDescripcion;
        document.getElementById('<%=hfActEmpresa.ClientID%>').value = pCodigo;
    }
</script>

</html>
