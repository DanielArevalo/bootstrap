<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/ctlSeleccionarPersona.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ImageButton runat="server" ID="btnSiguiente" ImageUrl="../../../Images/btnGuardar.jpg" OnClick="btnSiguiente_Click" style="float:right;" Visible="false" />
    <script type="text/javascript">
        function formatNumber(control, num_decimal) {
            var nums = new Array();
            var simb = "."; //Éste es el separador
            valor = control.value;

            var numeroLetras = valor.length; // numero de caracteres

            if (control.value.indexOf('.') > 0) {
                var posicion_decimal = valor.indexOf('.'); // Posicion del punto en la cadena
                var decimal = valor.substring(posicion_decimal); // Se obtiene el valor numerico decimal sin el punto
                decimal = decimal.replace(".", ""); // Se eliminan todos los puntos
                decimal = decimal.replace(/D/g, "");   //Ésta expresión regular solo permitira ingresar números
                decimal = decimal.substring(0, num_decimal) // Se acota el numero para que se muesten solo la cantidad de decimales especificada
                valor = valor.substring(0, posicion_decimal) // Se elimina el valor decimla del valor numerico
            }

            valor = valor.replace(/D/g, "");   //Ésta expresión regular solo permitira ingresar números
            nums = valor.split(""); //Se vacia el valor en un arreglo
            var long = nums.length - 1; // Se saca la longitud del arreglo
            var patron = 3; //Indica cada cuanto se ponen las comas
            var prox = 2; // Indica en que lugar se debe insertar la siguiente coma
            var res = "";

            while (long > prox) {
                nums.splice((long - prox), 0, simb); //Se agrega la coma
                prox += patron; //Se incrementa la posición próxima para colocar la coma
            }

            for (var i = 0; i <= nums.length - 1; i++) {
                res += nums[i]; //Se crea la nueva cadena para devolver el valor formateado
            }

            if (control.value.indexOf('.') > 0) {
                var enviar = res + "." + decimal
            }
            else {
                var enviar = res
            }

            control.value = enviar;
        }

        function control_clear(control) {
            var valor = control.value;

            if (valor.length > 0) {
                control.value = "";
            }
        }

    </script>
    <asp:Panel ID="pConsulta" runat="server" Style="margin-right: 0px" Width="100%">
        <table cellpadding="1" cellspacing="0" style="width: 98%">
            <tr>
                <td style="height: 15px; text-align: left">
                    <asp:Label ID="LblMensaje" runat="server" Style="color: #FF0000; font-weight: 700; font-size: x-small"
                        Font-Size="Larger"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 100%">
                    <asp:MultiView ID="MvAfiliados" runat="server">
                        <asp:View ID="ViewAfiliados" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <strong>Datos de la Linea</strong>
                                        <asp:Panel ID="pEncLinea" runat="server">
                                            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                                <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                                                    Linea Aporte<br />
                                                    <asp:DropDownList ID="ddlLineaFiltro" runat="server" Style="margin-bottom: 0px" Width="450px"
                                                        CssClass="textbox">
                                                        <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong style="text-align: left">Datos del Titular</strong>
                                        <asp:Panel ID="pBusqueda" runat="server" Height="70px">
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="text-align: left;">
                                                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="ViewAporte" runat="server">
                            <table>
                                <tr>
                                    <td style="text-align: left;" colspan="4">
                                        <strong>Datos de la Línea</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;" colspan="4">Linea Aporte
                                        <br />
                                        <asp:DropDownList ID="DdlLineaAporte" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlLineaAporte_SelectedIndexChanged"
                                            Style="margin-bottom: 0px" Width="450px" CssClass="textbox">
                                            <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;" colspan="4">
                                        <strong>Número de la Cuenta</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width: 194px">Número Aporte
                                        <asp:TextBox ID="txtgrupoaporte" runat="server" CssClass="textbox" Visible="False"
                                            Width="16px"></asp:TextBox>
                                        <br />
                                        <asp:TextBox ID="txtNumAporte" runat="server" CssClass="textbox" Width="185px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 200px">Oficina<asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="False"
                                        Visible="False" Width="25px"></asp:TextBox>
                                        <br />
                                        <asp:TextBox ID="txtOficinaNombre" runat="server" CssClass="textbox" Enabled="False"
                                            Width="194px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;" colspan="2">
                                        <table>
                                            <tr>
                                                <td>Fecha Apertura&nbsp;<br />
                                                    <asp:TextBox ID="txtFecha_apertura" runat="server" Enabled="false" ReadOnly="true" AutoPostBack="True" CssClass="textbox"
                                                        MaxLength="128" OnTextChanged="txtFecha_apertura_TextChanged" Width="157px" />
                                                    <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" Enabled="True"
                                                        Format="dd/MM/yyyy" TargetControlID="txtFecha_apertura">
                                                    </asp:CalendarExtender>
                                                    <span style="font-size: xx-small">
                                                        <asp:RequiredFieldValidator ID="rfvFechaApertura" runat="server" ControlToValidate="txtFecha_apertura"
                                                            Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                            Style="font-size: x-small" ValidationGroup="vgConsultar" />
                                                        <asp:CompareValidator ID="cvFecha_Apertura" runat="server" ControlToValidate="txtFecha_apertura"
                                                            Display="Dynamic" ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red"
                                                            Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: xx-small" ToolTip="Formato fecha"
                                                            Type="Date" ValidationGroup="vgGuardar" Width="105px" />
                                                    </span>
                                                </td>
                                                <td>Estado
                                                    <br />
                                                    <asp:DropDownList ID="DdlEstado" runat="server" Width="160px" CssClass="textbox">
                                                        <asp:ListItem Value="1">ACTIVO</asp:ListItem>
                                                        <asp:ListItem Value="2">INACTIVO</asp:ListItem>
                                                        <asp:ListItem Value="3">CERRADO</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: left;"></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;" colspan="4">
                                        <strong>Titular de la Cuenta</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 6px; text-align: left;">Identificación<asp:TextBox ID="txtCodigoCliente" runat="server" CssClass="textbox"
                                        Visible="False" Width="35px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvIdentificacion0" runat="server" ControlToValidate="txtNumeIdentificacion"
                                            Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                            Style="font-size: xx-small" ValidationGroup="vgConsultar" />
                                        <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox" Width="185px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td style="height: 6px; text-align: left;">Tipo Identificacion<br />
                                        <asp:DropDownList ID="DdlTipoIdentificacion" runat="server" AutoPostBack="True" Width="200px"
                                            CssClass="textbox">
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="2" style="height: 6px; text-align: left;">Nombre<br />
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Enabled="False" Width="385px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;" colspan="4">
                                        <strong>Datos de la Cuenta</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px; text-align: left;">Tipo De Cuota
                                        <br />
                                        <asp:DropDownList ID="DdlTipoCuota" runat="server" AutoPostBack="True" CssClass="textbox"
                                            Width="190px">
                                            <asp:ListItem Value="1">CUOTA FIJA</asp:ListItem>
                                            <asp:ListItem Value="2">RANGOS DE SALARIOS</asp:ListItem>
                                            <asp:ListItem Value="3">DISTRIBUCION</asp:ListItem>
                                            <asp:ListItem Value="4">% DEL SUELDO</asp:ListItem>
                                            <asp:ListItem Value="5">% SMLMV</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 15px; text-align: left;">Valor Cuota<br />
                                        <span style="font-size: xx-small">
                                            <asp:TextBox ID="txtValorCuota" runat="server" onkeypress="return isNumber(event)" AutoPostBack="True" CssClass="textbox"
                                                OnTextChanged="txtValorCuota_TextChanged" Width="150px"></asp:TextBox>
                                        </span>
                                    </td>
                                    <td style="height: 15px; text-align: left;">Periodicicidad<br />
                                        <asp:DropDownList ID="DdlPeriodicidad" runat="server" CssClass="textbox" Width="212px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 15px; text-align: left;">
                                        <asp:Panel ID="panelCuotaActual" runat="server">
                                            Cuota Actual<br />
                                            <asp:TextBox ID="txtCuotaActual" runat="server" AutoPostBack="True" CssClass="textbox"
                                                Enabled="false" OnTextChanged="txtValorCuota_TextChanged" Width="150px" Style="font-size: xx-small" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999"
                                                MaskType="Number" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError" TargetControlID="txtCuotaActual" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblporcentajeApo" runat="server" Text="Porcentaje de aporte %" Visible="false" /><br />
                                        <asp:TextBox ID="txtporcenApo" runat="server" AutoPostBack="True" CssClass="textbox" Visible="false"
                                            OnTextChanged="txtporcenApo_TextChanged" Width="150px" Style="font-size: xx-small" />
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 15px; text-align: left;">
                                        <asp:MultiView ID="MvDistribucion" runat="server">
                                            <asp:View ID="VDistribucion" runat="server">
                                                <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                    GridLines="Horizontal" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDataBound="gvLista_RowDataBound"
                                                    OnSelectedIndexChanged="gvLista_SelectedIndexChanged" PageSize="5" ShowFooter="True"
                                                    ShowHeaderWhenEmpty="True" Style="text-align: left" Width="846px">
                                                    <Columns>
                                                        <asp:BoundField DataField="numero_aporte" HeaderText="Num Aporte" />
                                                        <asp:BoundField DataField="cod_linea_aporte" HeaderText="Linea">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nom_linea_aporte" HeaderText="Nombre Linea">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="porcentaje" HeaderText="%Distribución">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="cuota" DataFormatString="{0:C}" HeaderText="Valor Base"
                                                            SortExpression="cuota">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Principal">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="Chkprincipal" runat="server" Checked='<%#Convert.ToBoolean(Eval("principal")) %>'
                                                                    Enabled="False" EnableViewState="true" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pendiente Crear" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkPendiente" runat="server"
                                                                    Checked='<%#Convert.ToBoolean(Eval("pendiente_crear")) %>' Enabled="False"
                                                                    EnableViewState="true" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="gridHeader" />
                                                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                                    <RowStyle CssClass="gridItem" />
                                                </asp:GridView>
                                            </asp:View>
                                        </asp:MultiView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 6px; text-align: left;">Fecha Prox. Pago<br />
                                        <asp:TextBox ID="txtFecha_Proxppago" runat="server" CssClass="textbox" MaxLength="128"
                                            Width="120px"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtFecha_Proxppago_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd/MM/yyyy" TargetControlID="txtFecha_Proxppago">
                                        </asp:CalendarExtender>
                                        <br />
                                        <span style="font-size: xx-small">
                                            <asp:RequiredFieldValidator ID="rfvFechaProxPago" runat="server" ControlToValidate="txtFecha_Proxppago"
                                                Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                Style="font-size: x-small" ValidationGroup="vgConsultar" />
                                        </span>
                                        <asp:CompareValidator ID="cvFecha_proxpago" runat="server" ControlToValidate="txtFecha_Proxppago"
                                            Display="Dynamic" ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red"
                                            Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: xx-small" ToolTip="Formato fecha"
                                            Type="Date" ValidationGroup="vgGuardar" Width="105px" />
                                    </td>
                                    <td style="height: 6px; text-align: left;">Fecha Interes&nbsp;<br />
                                        <asp:TextBox ID="txtFecha_interes" runat="server" CssClass="textbox" Enabled="False"
                                            MaxLength="128" Width="120px" />
                                        <asp:CalendarExtender ID="txtFecha_interes_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd/MM/yyyy" TargetControlID="txtFecha_interes">
                                        </asp:CalendarExtender>
                                        <br />
                                        <span style="font-size: xx-small">
                                            <asp:RequiredFieldValidator ID="rfvFechaInteres" runat="server" ControlToValidate="txtFecha_interes"
                                                Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                Style="font-size: x-small" ValidationGroup="vgConsultar" />
                                        </span>
                                        <asp:CompareValidator ID="cvFecha_interes" runat="server" ControlToValidate="txtFecha_interes"
                                            Display="Dynamic" ErrorMessage="Formato de Fecha (dd/MM/yyyy)" ForeColor="Red"
                                            Height="16px" Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: xx-small"
                                            ToolTip="Formato fecha" Type="Date" ValidationGroup="vgGuardar" Width="93px" />
                                    </td>
                                    <td style="height: 6px; text-align: left;" colspan="2">
                                        <asp:UpdatePanel ID="updFormapago" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>FormaPago<br />
                                                            <asp:DropDownList ID="DdlFormaPago" runat="server" AutoPostBack="True" CssClass="textbox"
                                                                Width="170px" OnSelectedIndexChanged="DdlFormaPago_SelectedIndexChanged">
                                                                <asp:ListItem Value="1">Caja</asp:ListItem>
                                                                <asp:ListItem Value="2">Nomina</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" /><br />
                                                            <asp:DropDownList ID="ddlEmpresa" runat="server" Width="160px" CssClass="textbox" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="DdlFormaPago" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                        <br />
                                        <strong>Beneficiarios</strong>
                                        <asp:UpdatePanel ID="upBeneficiarios" Visible="false" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnAddRowBeneficio" AutoPostBack="true" runat="server" CssClass="btn8" TabIndex="49" OnClick="btnAddRowBeneficio_Click" Text="+ Adicionar Detalle" />
                                                <asp:GridView ID="gvBeneficiarios"
                                                    runat="server" AllowPaging="True" TabIndex="50" AutoGenerateColumns="false" BackColor="White"
                                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="0" DataKeyNames="idbeneficiario"
                                                    ForeColor="Black" GridLines="Both" OnRowDataBound="gvBeneficiarios_RowDataBound"
                                                    OnRowDeleting="gvBeneficiarios_RowDeleting" PageSize="10" ShowFooter="True" Style="font-size: xx-small"
                                                    Width="80%">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/gr_elim.jpg" ShowDeleteButton="True" />
                                                        <asp:TemplateField HeaderText="Identificación" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdIdBeneficiario" runat="server" Value='<%# Bind("idbeneficiario") %>' />
                                                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                    Text='<%# Bind("identificacion_ben") %>' Width="100px"> </asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombres y Apellidos" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtNombres" runat="server" CssClass="textbox" Style="font-size: xx-small; text-align: left"
                                                                    Text='<%# Bind("nombre_ben") %>' Width="260px"> </asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="260px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fecha Nacimiento" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <uc1:fecha ID="txtFechaNacimientoBen" runat="server" CssClass="textbox" style="font-size: xx-small; text-align: left"
                                                                    Text='<%# Eval("fecha_nacimiento_ben", "{0:" + FormatoFecha() + "}") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sexo" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:DropDownList runat="server" ID="ddlsexo" CssClass="textbox" Text='<%# Bind("sexo") %>'>
                                                                    <asp:ListItem Value="0" Text="<Seleccione un item>" />
                                                                    <asp:ListItem Value="F" Text="Femenino" />
                                                                    <asp:ListItem Value="M" Text="Masculino" />
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                        </asp:TemplateField>                                                        
                                                        <asp:TemplateField HeaderText="Parentesco" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblidbeneficiario" runat="server" Text='<%# Bind("idbeneficiario") %>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblParentesco" runat="server" Text='<%# Bind("parentesco") %>'
                                                                    Visible="false"></asp:Label><asp:DropDownList ID="ddlParentezco" runat="server"
                                                                        AppendDataBoundItems="True" commandargument="<%#Container.DataItemIndex %>" CssClass="textbox"
                                                                        Style="font-size: xx-small; text-align: left" Width="120px">
                                                                    </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="% Ben." ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <uc1:decimalesGridRow ID="txtPorcentaje" runat="server" AutoPostBack_="True" CssClass="textbox" OneventoCambiar="txtPorcentaje_eventoCambiar"
                                                                    Enabled="True" Habilitado="True" Text='<%# Eval("porcentaje_ben") %>' Width_="80" />
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="mvFinal" runat="server">
                            <asp:Panel ID="PanelFinal" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: center; font-size: large;">
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; font-size: large;">
                                            <asp:Label ID="Label1" runat="server" Text="Operacion Realizada Correctamente"
                                                Style="color: #FF3300"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Button ID="btnVolver" CssClass="btn8" Text="Regresar" runat="server" OnClick="btnVolver_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; font-size: large;">
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; font-size: large;"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
