<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Credito.aspx.cs" Inherits="Credito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="~/Css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="~/Css/JaLoAdmin.min.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" type="image/x-icon" href="~/favicon.ico" />
    <style type="text/css">
        .tableNormal {
            border-collapse: separate;
            border-spacing: 4px;
        }

        .mensaje {
            background-color: rgba(184, 233, 148,1.0);
            padding: 5px;
            margin: 0 50px;
            font-weight: 500;
        }

        .noEditable {
            background-color: white;
            border: none;
        }
    </style>
    <script src="<%=ResolveUrl("~/Scripts/PCLBryan.js")%>"></script>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function ValidNum(e) {
            var keyCode = e.which ? e.which : e.keyCode
            return ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
        }

        function ocultarMostrarPanel(prueba) {
            var panel = document.getElementById(prueba);
            panel.style.display = (panel.style.display == 'none') ? 'block' : 'none';
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>
    <%@ Register Src="~/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
    <%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <div class="col-md-12">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10 col-md-12 col-sm-12">
            <div class="col-xs-12" style="padding-bottom: 20px;">
                <asp:Label ID="lblError" runat="server" Style="color: Red; font-size: 13px" />
            </div>
        </div>
        <div class="col-lg-1">
        </div>
    </div>
    <asp:Panel ID="panelData" runat="server">
        <div class="col-md-12">
            <div class="col-lg-1">
            </div>
            <div class="col-lg-10 col-md-12 col-sm-12">
                <div class="col-md-12">
                    <div class="col-xs-12">
                        <div class="col-xs-7 text-left">
                            <table class="tableNormal">
                                <tr>
                                    <td style="text-align: left; width: 150px">FECHA:
                                    </td>
                                    <td style="text-align: center">Día
                                    </td>
                                    <td style="text-align: left; width: 60px">
                                        <asp:TextBox ID="txtDiaEncabezado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center">Mes
                                    </td>
                                    <td style="text-align: left; width: 150px">
                                        <asp:DropDownList ID="ddlMesEncabezado" runat="server" CssClass="form-control" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: center">Año
                                    </td>
                                    <td style="text-align: left; width: 100px">
                                        <asp:TextBox ID="txtAnioEncabezado" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lblid" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblcod_persona" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-xs-5 text-right">
                            <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary" Width="120px" ToolTip="Save"
                                Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnGuardar_Click">
                                    <div class="pull-left" style="padding-left:10px">
                                    <span class="glyphicon glyphicon-floppy-disk"></span></div>Grabar
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger" Width="120px" ToolTip="Cancel"
                                Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnCancelar_Click">
                                    <div class="pull-left" style="padding-left:10px">
                                    <span class="glyphicon glyphicon-arrow-left"></span></div>Cancelar
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <br />
                    </div>
                </div>
            </div>
            <div class="col-lg-1">
            </div>
        </div>
        <div onclick="ocultarMostrarPanel('datos')" style='cursor: pointer;'>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="col-md-12">
                        <table class="tableNormal" width="100%" style="background-color: #0099CC; color: #fff;">
                            <tr style="margin: auto">
                                <td style="width: 30%; text-align: center;">
                                    <asp:Label ID="LbldatosPersonales" runat="server" Text="Informaci&oacute;n Financiera" Style="text-align: center;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-md-12" id="datos">
            <div class="col-lg-2 col-md-2 col-sm-12">
            </div>
            <div class="col-lg-8 col-md-8 col-sm-12">
                <table class="tableNormal" width="100%">
                    <tr>
                        <td style="text-align: right;">Línea de crédito:&nbsp;&nbsp;<span style="color: red; font-weight: 600">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlLinea" runat="server" Width="80%" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlDestinacion" Visible="false">
                        <tr>
                            <td style="text-align: right;">Destinación
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlDestinacion" runat="server" required Width="80%" CssClass="form-control"></asp:DropDownList>
                            </td>
                        </tr>
                        <asp:DropDownList ID="DropDownList1" Width="80%" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPeriodicidad_SelectedIndexChanged" Visible="false">
                        </asp:DropDownList>
                    </asp:Panel>
                    <tr>
                        <td style="text-align: right;">Valor del crédito:&nbsp;&nbsp;<span style="color: red; font-weight: 600">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtVrCredito" onkeypress="return ValidNum(event);" runat="server" Width="80%" CssClass="form-control decimalConSigno" AutoPostBack="true" OnTextChanged="txtVrCredito_TextChanged"></asp:TextBox>
                            <%--$<uc1:decimales ID="asdf" runat="server" Width_="77%" AutoPostBack="True" OnTextChanged="txtPlazo_TextChanged" Class_="form-control decimalConSigno" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Plazo(Meses):&nbsp;&nbsp;<span style="color: red; font-weight: 600">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPlazo" runat="server" CssClass="form-control" Width="80%" AutoPostBack="True" OnTextChanged="txtPlazo_TextChanged" />
                            <cc1:FilteredTextBoxExtender ID="ftePlazo" runat="server" Enabled="True"
                                FilterType="Numbers, Custom" TargetControlID="txtPlazo" ValidChars=""></cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Forma Pago:&nbsp;&nbsp;<span style="color: red; font-weight: 600">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="txtFomaPago" runat="server" Width="80%" CssClass="form-control">
                                <asp:ListItem Value="1">Caja</asp:ListItem>
                                <asp:ListItem Value="2">Nomina</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlAfinacol" Visible="false">
                        <tr>
                            <td style="text-align: right;">Requiere fianza
                            </td>
                            <td style="text-align: left;">
                                <asp:CheckBox runat="server" ID="chkAfiancol" Style="width: 50px;" />
                            </td>
                        </tr>
                        <asp:DropDownList ID="ddlPeriodicidad" Width="80%" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPeriodicidad_SelectedIndexChanged" Visible="false">
                        </asp:DropDownList>
                    </asp:Panel>
                    <tr>
                        <td style="text-align: right;">Monto máximo:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMontoMaximo" runat="server" Width="80%" Enabled="false" CssClass="form-control decimalConSigno noEditable"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Plazo máximo:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPlazoMaximo" runat="server" CssClass="form-control noEditable" Width="80%" ReadOnly="true" BackColor="White" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Tasa MVSS
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtTasa" runat="server" CssClass="form-control" Width="80%" AutoPostBack="True" Enabled="false" Style="border: none; background-color: white;" />
                        </td>
                    </tr>
                    <asp:Panel runat="server" Visible="false">
                        <tr>
                            <td style="text-align: right;">Cuenta Bancaria:
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="form-control" Width="80%" TextAlign="Left">
                                    <asp:ListItem Value="0" Selected="True">&nbsp;&nbsp;Ahorros&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="1">&nbsp;&nbsp;Corriente&nbsp;</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">N° de cuenta:
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtNumCuenta" runat="server" Width="80%" CssClass="form-control"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="fte1" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                    TargetControlID="txtNumCuenta" ValidChars="-." />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Entidad:
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlEntidad" runat="server" Width="80%" CssClass="form-control" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td style="text-align: right;">Valor aproximado de la cuota:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtCuotaAproximada" runat="server" Width="80%" AutoPostBack="True" Enabled="false" CssClass="form-control decimalConSigno noEditable" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-12">
            </div>
        </div>
        <asp:UpdatePanel ID="updCuotasExt" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlCuotaExtra" runat="server">
                    <div class="col-md-12" onclick="ocultarMostrarPanel('dvInfoCuotaExt')" style='cursor: pointer;'>
                        <asp:Panel ID="panel4" runat="server" Style="background-color: #0099CC; color: #fff; text-align: center; padding-bottom: 5px; padding-top: 5px; margin-bottom: 5px;"
                            Width="100%">
                            <asp:Label ID="Label4" runat="server" Text="Cuotas extras (Opcional)" Style="text-align: center;"></asp:Label>
                        </asp:Panel>
                    </div>
                    <div id="dvInfoCuotaExt" class="col-md-12">
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-10 col-md-12 col-sm-12">
                            <table id="tblCuotasExt" width="100%" class="tableNormal">
                                <tr>
                                    <td>
                                        <div class="col-sm-12">
                                            <asp:Label ID="lblErrorCuotaExtra" runat="server" CssClass="text-danger text-left"></asp:Label><br />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 text-left">
                                                Porcentaje del crédito en cuotas extras
                                            </div>
                                            <div class="col-sm-7 text-left">
                                                <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="form-control" Width="136px" onkeypress="return ValidNum(event);" MaxLength="6" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 text-left">
                                                Número de cuotas extras
                                            </div>
                                            <div class="col-sm-7 text-left">
                                                <asp:TextBox ID="txtNumeroCuotaExt" runat="server" CssClass="form-control" Width="136px" onkeypress="return ValidNum(event);" MaxLength="3" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 text-left">
                                                Fecha de primera cuota extra
                                            </div>
                                            <div class="col-sm-7 text-left">
                                                <asp:TextBox ID="txtFechaCuotaExt" runat="server" CssClass="form-control" Width="120px" placeholder="DD/MM/AAAA"></asp:TextBox>
                                                <%--<cc1:CalendarExtender ID="calExtFechaIngreso" runat="server"  Format="dd/MM/yyyy" TargetControlID="txtFechaCuotaExt" Animated="True">
                                            </cc1:CalendarExtender>--%>
                                                <cc1:MaskedEditExtender ID="MEEfecha" runat="server" TargetControlID="txtFechaCuotaExt" Mask="99/99/9999"
                                                    MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" />
                                                <%--<ucFecha:fecha ID="txtFechaCuotaExt" runat="server" cssclass="form-control" />--%>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 text-left">
                                                Valor cuota extra
                                            </div>
                                            <div class="col-sm-7 text-left">
                                                <uc1:decimales ID="txtValorCuotaExt" runat="server" cssclass="textbox" Width_="160px" AutoPostBack_="false"></uc1:decimales>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 text-left">
                                                Forma de pago
                                            </div>
                                            <div class="col-sm-7 text-left">
                                                <asp:DropDownList ID="ddlFormaPagoCuotaExt" runat="server" Width="190px" CssClass="form-control">
                                                    <asp:ListItem Value="1">Caja</asp:ListItem>
                                                    <asp:ListItem Value="2">Nomina</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 text-left">
                                                Periodicidad de cuota extra
                                            </div>
                                            <div class="col-sm-7 text-left">
                                                <asp:DropDownList ID="ddlPeriodicidadCuotaExt" runat="server" CssClass="form-control" Width="190px" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 text-left">
                                                Tipo cuota extra
                                            </div>
                                            <div class="col-sm-7 text-left">
                                                <asp:DropDownList ID="ddlCuotaExtTipo" runat="server" CssClass="form-control" Width="190px" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="col-sm-6 text-right">
                                            <asp:LinkButton ID="btnGenerarCuotaExtra" runat="server" CssClass="btn btn-default text-center" Width="180px" ToolTip="Generar"
                                                Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnGenerarCuotaExtra_Click">
                                        <b>Generar cuotas extras</b>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:LinkButton ID="btnLimpiarCuotaExtra" runat="server" CssClass="btn btn-default text-center" Width="120px" ToolTip="Limpiar"
                                                Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnLimpiarCuotaExtra_Click">
                                        <b>Limpiar</b>
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="col-md-8">
                                            <asp:Panel ID="pnlListaCuotas" runat="server">
                                                <asp:GridView ID="gvCuoExt" runat="server" CssClass="table" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No se encontraron registros." AutoGenerateColumns="False"
                                                    GridLines="Vertical" OnRowDeleting="gvCuoExt_RowDeleting" DataKeyNames="valor">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="glyphicon glyphicon-trash" ToolTip="Eliminar" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fecha Pago">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfechapago" runat="server" Text='<%# Bind("fecha_pago","{0:d}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Forma Pago">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblformapago" runat="server" Text='<%# Bind("des_forma_pago") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Valor">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblvalor" runat="server" Text='<%# Bind("valor","{0:C}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tipo Cuota Extra">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltipocuota" runat="server" Text='<%# Bind("des_tipo_cuota") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cod.Forma Pago" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcodformapago" runat="server" Text='<%# Bind("forma_pago") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#337ab7" ForeColor="White" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-lg-1">
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div onclick="ocultarMostrarPanel('propiedades')" style='cursor: pointer; display: none'>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="col-md-12">
                        <table class="tableNormal" width="100%" style="background-color: #0099CC; color: #fff;">
                            <tr style="margin: auto">
                                <td style="width: 30%; text-align: center;">
                                    <asp:Label ID="Lblrelacionbienes" runat="server" Text="Relaci&oacute;n de bienes de su propiedad:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="col-md-12" id="propiedades" style='display: none;'>
            <div class="col-lg-2 col-md-2 col-sm-12">
            </div>
            <div class="col-lg-8 col-md-8 col-sm-12">

                <table class="tableNormal" width="100%">
                    <tr>
                        <td style="text-align: right;">Vivienda:
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlTipoVivienda" runat="server"
                                CssClass="form-control" AutoPostBack="true"
                                TextAlign="Left" Width="80%"
                                OnSelectedIndexChanged="cblTipoVivienda_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="P">&nbsp;Propia&nbsp;</asp:ListItem>
                                <asp:ListItem Value="A">&nbsp;Arrendada&nbsp;</asp:ListItem>
                                <asp:ListItem Value="F">&nbsp;Familiar&nbsp;</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Nombre Arrendatario
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtNombreArrendatario" Width="80%" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Tel. Arrendatario
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtTelArrendatario" Width="80%" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Propiedad<span style="color: red; font-weight: 600">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlPropiedad" runat="server" Width="80%" CssClass="form-control" TextAlign="Left"
                                AutoPostBack="true" OnSelectedIndexChanged="cblPropiedad_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="C">&nbsp;Casa&nbsp;</asp:ListItem>
                                <asp:ListItem Value="A">&nbsp;Apartamento&nbsp;</asp:ListItem>
                                <asp:ListItem Value="F">&nbsp;Finca&nbsp;</asp:ListItem>
                                <asp:ListItem Value="L">&nbsp;Lote&nbsp;</asp:ListItem>
                                <asp:ListItem Value="O">&nbsp;Otro&nbsp;</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Otro<span style="color: red; font-weight: 600">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtOtroPropiedad" Width="80%" runat="server" CssClass="form-control"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Direcci&oacute;n:&nbsp;&nbsp;<span style="color: red; font-weight: 600">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtDirecPropiedad" Width="80%" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Ciudad:&nbsp;&nbsp;<span style="color: red; font-weight: 600">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlCiudadPropiedad" Width="80%" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Escritura N°
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtEscrituraNro" runat="server" Width="80%" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="fte2" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                TargetControlID="txtEscrituraNro" ValidChars="-" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Notaria:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtNotaria" runat="server" Width="80%" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Hipoteca:
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlHipoteca" runat="server" Width="80%" CssClass="form-control"
                                TextAlign="Left" AutoPostBack="true" OnSelectedIndexChanged="rblHipoteca_SelectedIndexChanged">
                                <asp:ListItem Value="1">&nbsp;Si&nbsp;</asp:ListItem>
                                <asp:ListItem Selected="True" Value="0">&nbsp;No&nbsp;</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Matricula Inmov:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMatriculaInmov" runat="server" Width="80%" CssClass="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="fte3" runat="server" Enabled="True" FilterType="Numbers, Custom"
                                TargetControlID="txtMatriculaInmov" ValidChars="-" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Valor Comercial:
                        </td>
                        <td style="text-align: left;">
                            <uc1:decimales ID="txtVrComercial" runat="server" Width_="80%" AutoPostBack_="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Valor Hipoteca:
                        </td>
                        <td style="text-align: left;">
                            <uc1:decimales ID="txtVrHipoteca" runat="server" Width_="80%" AutoPostBack_="false" Enabled="false" ClientIDMode="Static" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-12">
            </div>
        </div>
        <div class="col-md-12" onclick="ocultarMostrarPanel('gastos')" style='cursor: pointer; display: none'>
            <table class="tableNormal" width="100%">
                <tr>
                    <td style="text-align: left; width: 50%">
                        <asp:Panel ID="panelGastosGral" runat="server" Style="background-color: #0099CC; color: #fff; text-align: center; padding-bottom: 5px; padding-top: 5px"
                            Width="100%">
                            <asp:Label ID="Lblgastosmensuales" runat="server" Text="Gastos Mensuales Generales"
                                Style="text-align: center;"></asp:Label>
                        </asp:Panel>
                    </td>
                    <td style="text-align: left; width: 50%">
                        <asp:Panel ID="panel3" runat="server" Style="background-color: #0099CC; color: #fff; text-align: center; padding-bottom: 5px; padding-top: 5px"
                            Width="100%">
                            <asp:Label ID="lblVehiculos" runat="server" Text="Veh&iacute;culos" Style="text-align: center;"></asp:Label>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
        <div id="gastos" style='display: none;'>
            <asp:UpdatePanel ID="upCalculo" runat="server">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-10 col-md-12 col-sm-12">
                            <table class="tableNormal" width="100%">
                                <tr>
                                    <td style="text-align: left; width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: left; width: 60%">Arriendo/cuota Vivienda:
                                                </td>
                                                <td style="text-align: right; width: 40%">
                                                    <uc1:decimales ID="txtArriendoViv" runat="server" Width_="80%" AutoPostBack_="true" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: left; width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: left; width: 60%">Marca/Modelo:
                                                </td>
                                                <td style="text-align: right; width: 40%">
                                                    <asp:TextBox ID="txtMarcaModelo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table class="tableNormal" width="100%">
                                <tr>
                                    <td style="text-align: left; width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: left; width: 60%">Gastos de Sostenimiento:
                                                </td>
                                                <td style="text-align: right; width: 40%">
                                                    <uc1:decimales ID="txtGastosSos" runat="server" Width_="80%" AutoPostBack_="true" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: left; width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: left; width: 60%">Valor Comercial:
                                                </td>
                                                <td style="text-align: right; width: 40%">
                                                    <uc1:decimales ID="txtVrComercVehi" runat="server" Width_="100%" AutoPostBack_="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table class="tableNormal" width="100%">
                                <tr>
                                    <td style="text-align: left; width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: left; width: 60%">Otros Gastos:
                                                </td>
                                                <td style="text-align: right; width: 40%">
                                                    <uc1:decimales ID="txtOtrosGastos" runat="server" Width_="80%" AutoPostBack_="true" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: left; width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: left; width: 60%">Pignorado:
                                                </td>
                                                <td style="text-align: right; width: 40%">
                                                    <asp:UpdatePanel ID="upPignorado" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="rbllPignorado" runat="server" CssClass="form-control"
                                                                AutoPostBack="true" TextAlign="Left" OnSelectedIndexChanged="rbllPignorado_SelectedIndexChanged">
                                                                <asp:ListItem Value="1">&nbsp;Si&nbsp;</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="0">&nbsp;No&nbsp;</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table class="tableNormal" width="100%">
                                <tr>
                                    <td style="text-align: left; width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: left; width: 60%">Total gastos generales:
                                                </td>
                                                <td style="text-align: right; width: 40%">
                                                    <asp:TextBox ID="txtTotalGastos" runat="server" CssClass="form-control" Width="80%"
                                                        placeholder="$" Enabled="false" Style="text-align: right"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: left; width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: left; width: 60%">Valor Pignorado:
                                                </td>
                                                <td style="text-align: right; width: 40%">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <uc1:decimales ID="txtValorPignorado" runat="server" Width_="100%" AutoPostBack_="false" Enabled="false" />
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="rbllPignorado" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-lg-1">
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div onclick="ocultarMostrarPanel('creditos')" style='cursor: pointer; display: none'>
            <div class="col-md-12">
                <asp:Panel ID="panel2" runat="server" Style="background-color: #0099CC; color: #fff; text-align: center; padding-bottom: 5px; padding-top: 5px"
                    Width="100%">
                    <asp:Label ID="Lblcreditos" runat="server" Text="Cr&eacute;ditos con otras entidades" Style="text-align: center;"></asp:Label>
                </asp:Panel>
            </div>
        </div>
        <div class="col-md-12" id="creditos" style='display: none;'>
            <div class="col-lg-1">
            </div>
            <div class="col-lg-10 col-md-12 col-sm-12">
                <asp:UpdatePanel ID="upCalculaCred" runat="server">
                    <ContentTemplate>
                        <table class="tableNormal" width="100%">
                            <tr>
                                <td style="text-align: left; width: 25%">Entidad del cr&eacute;dito:
                                    <asp:DropDownList ID="ddlEntidadCredito" runat="server" CssClass="form-control" />
                                </td>
                                <td style="text-align: center; width: 12%">Fecha Vencimiento:
                                </td>
                                <td style="text-align: left; width: 120px">Mes
                                    <asp:DropDownList ID="ddlMesVencimiento" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; width: 50px">Año:
                                    <asp:TextBox ID="txtAnioVenc" runat="server" CssClass="form-control text-center" onkeypress="return isNumber(event)" MaxLength="4"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 14%">Saldo a la fecha:
                                    <uc1:decimales ID="txtSaldofecha" runat="server" Width_="100%" AutoPostBack_="true" />
                                </td>
                                <td style="text-align: left; width: 14%">Valor cuota:
                                    <uc1:decimales ID="txtValorcuota" runat="server" Width_="100%" AutoPostBack_="true" />
                                </td>
                            </tr>
                        </table>
                        <hr style="width: 100%; margin-top: 4px; box-shadow: 0px 1px 0.5px rgba(0,0,0,.5);" />
                        <table class="tableNormal" width="100%">
                            <tr>
                                <td style="text-align: left; width: 25%">Entidad del cr&eacute;dito:
                                    <asp:DropDownList ID="ddlEntidadCredito2" runat="server" CssClass="form-control" />
                                </td>
                                <td style="text-align: center; width: 12%">Fecha Vencimiento:
                                </td>
                                <td style="text-align: left; width: 120px">Mes
                                    <asp:DropDownList ID="ddlVencimientoMes2" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; width: 50px">Año:
                                    <asp:TextBox ID="txtAnioVenc2" runat="server" CssClass="form-control text-center" onkeypress="return isNumber(event)" MaxLength="4"></asp:TextBox>
                                </td>
                                <td style="text-align: left; width: 14%">Saldo a la fecha:
                                    <uc1:decimales ID="txtSaldofecha2" runat="server" Width_="100%" AutoPostBack_="true" />
                                </td>
                                <td style="text-align: left; width: 14%">Valor cuota:
                                    <uc1:decimales ID="txtValorcuota2" runat="server" Width_="100%" AutoPostBack_="true" />
                                </td>
                            </tr>
                        </table>
                        <hr style="width: 100%; margin-top: 4px; box-shadow: 0px 1px 0.5px rgba(0,0,0,.5);" />
                        <table class="tableNormal" width="100%">
                            <tr>
                                <td style="text-align: left; width: 24%">&nbsp;
                                </td>
                                <td style="text-align: left; width: 12%">Total de Saldo:
                                </td>
                                <td style="text-align: left; width: 14%">
                                    <asp:TextBox ID="txtSaldoConsoli" runat="server" CssClass="form-control" placeholder="$"
                                        Enabled="false" Style="text-align: right" />
                                </td>
                                <td style="text-align: center; width: 12%">Total Cuota por Mes:
                                </td>
                                <td style="text-align: left; width: 14%">
                                    <asp:TextBox ID="txtCuotaXmes" runat="server" CssClass="form-control" placeholder="$"
                                        Enabled="false" Style="text-align: right" />
                                </td>
                                <td style="text-align: center; width: 24%">&nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-lg-1">
            </div>
        </div>
        <div class="col-md-12">
            <br />
        </div>
    </asp:Panel>

    <asp:Panel ID="panelDocumentos" runat="server" Visible="true">
        <div class="col-md-12">
            <br />
        </div>
        <div class="col-md-12">
            <asp:Panel ID="panel1" runat="server" Style="background-color: #0099CC; color: #fff; text-align: center; padding-bottom: 5px; padding-top: 5px"
                Width="100%">
                <asp:Label ID="Label3" runat="server" Text="Documentos Requeridos" Style="text-align: center;"></asp:Label>
            </asp:Panel>
        </div>
        <div class="col-md-12">
            <div class="col-lg-1">
            </div>
            <div class="col-lg-10 col-md-10 col-sm-12">
                <div class="col-md-10 text-left">
                    <br />
                    <asp:Label runat="server" Text="Tamaño máximo del archivo a cargar (2 MB)"
                        Style="padding-left: 15px; font-size: x-small; color: Green; font-weight: 700;" /><br />
                    <asp:Label runat="server" Style="padding-left: 15px; font-size: x-small;" Text="Extensiones válidas *.pdf, *.jpg, *.jpeg, *.bmp, *.png"></asp:Label><br />
                    <asp:Label runat="server" Style="padding-left: 15px;">Verifique si están cargados todos sus documentos antes de continuar</asp:Label>
                </div>
                <div class="col-md-12">
                    <br />
                    <asp:GridView ID="gvDocumentosReq" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-inverse"
                        GridLines="Horizontal" ShowHeaderWhenEmpty="True" DataKeyNames="tipo_documento, descripcion"
                        Width="90%">
                        <Columns>
                            <asp:BoundField DataField="tipo_documento" HeaderText="Cod Tipo Doc" Visible="False">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Tipo de Documento">
                                <ItemTemplate>
                                    <asp:Label ID="lblNomDocumento" runat="server" Text='<%# Bind("descripcion") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="60%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Archivo">
                                <ItemTemplate>
                                    <asp:FileUpload ID="fuArchivo" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="40%" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-lg-1">
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="panelFinal" runat="server" Visible="false">
        <div class="col-xs-12">
            <div class="col-lg-1">
            </div>
            <div class="col-lg-10 col-md-12 col-xs-12" style="margin-top: 27px">
                <div class="col-xs-12">
                    <asp:Label ID="Label2" runat="server" Text="Tu solicitud se generó con el código: "
                        Style="color: #66757f; font-size: 28px;" />
                    <asp:Label ID="lblCodigoGenerado" runat="server" Style="color: Red; font-size: 28px;" />
                    <br />
                    <asp:Label runat="server" Text="Tu comercial asignada se pondrá en contacto en el transcurso de las siguientes 24 horas hábiles."
                        Style="color: #66757f; font-size: 28px;" />
                </div>
                <div class="col-xs-12">
                    &nbsp;
                </div>
                <div class="col-xs-12">
                    &nbsp;
                </div>
                <div class="col-xs-12">
                    <asp:LinkButton ID="btnInicio" runat="server" CssClass="btn btn-primary" Width="170px" ToolTip="Home" Visible="false"
                        Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnInicio_Click">
                            <div class="pull-left" style="padding-left:10px">
                            <span class="fa fa-home"></span></div>&#160;&#160;Regresar al Inicio
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-1">
            </div>
        </div>
    </asp:Panel>
    <uc4:mensajeGrabar ID="ctlMensaje" runat="server" />
</asp:Content>

