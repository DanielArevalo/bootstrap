<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/direccion.ascx" TagName="direccion" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/tnumero.ascx" TagName="txtPesos" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>
<%@ Register Src="../../../General/Controles/decimales.ascx" TagName="decimales"
    TagPrefix="uc2" %>
<%@ Register Src="~/General/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
    <asp:MultiView ID="MvDistribucion" runat="server">
        <asp:View ID="VDistribucion" runat="server">
            <asp:Panel ID="pConsulta" runat="server" Style="margin-right: 0px" Width="850px">
                <table cellpadding="5" cellspacing="0" style="width: 82%; margin-right: 0px;">
                    <tr>
                        <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                            <span style="color: #0099FF;"><strong style="text-align: center; background-color: #FFFFFF;">
                                <asp:Label ID="LblMensaje" runat="server" Style="color: #FF0000; font-weight: 700;"></asp:Label>
                            </strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 259px; height: 6px;">Identificación<asp:TextBox ID="txtCodigoCliente" runat="server" CssClass="textbox"
                            Visible="False" Width="35px"></asp:TextBox>
                            <span style="font-size: xx-small">
                                <asp:RequiredFieldValidator ID="rfvIdentificacion1" runat="server" ControlToValidate="txtNumeIdentificacion"
                                    Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                    Style="font-size: x-small" ValidationGroup="vgConsultar" />
                            </span>
                            <br />
                            <asp:TextBox ID="txtNumeIdentificacion" runat="server" CssClass="textbox" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 190px;">Número Aporte
                    <asp:TextBox ID="txtgrupoaporte" runat="server" CssClass="textbox" Visible="False"
                        Width="16px"></asp:TextBox>
                            <br />
                            <asp:TextBox ID="txtNumAporte" runat="server" CssClass="textbox" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="text-align: left;">Fecha Apertura&nbsp;<br />
                            <asp:TextBox ID="txtFecha_apertura" runat="server" CssClass="textbox" Enabled="False"
                                MaxLength="128" Width="157px" />
                            <asp:CalendarExtender ID="txtFecha_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" TargetControlID="txtFecha_apertura">
                            </asp:CalendarExtender>
                            <br />
                        </td>
                        <td style="text-align: left;">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 6px; text-align: left; width: 259px;">Oficina<asp:TextBox ID="txtOficina" runat="server" CssClass="textbox" Enabled="False"
                            Visible="False" Width="25px"></asp:TextBox>
                            <br />
                            <asp:TextBox ID="txtOficinaNombre" runat="server" CssClass="textbox" Enabled="False"
                                Width="240px"></asp:TextBox>
                        </td>
                        <td style="height: 6px; text-align: left; width: 190px;">Tipo Identificacion<br />
                            <asp:DropDownList ID="DdlTipoIdentificacion" runat="server" Width="165px" AutoPostBack="True"
                                CssClass="textbox" Enabled="false" />
                        </td>
                        <td style="height: 6px; text-align: left;" colspan="2">Nombre<br />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="250px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px; text-align: left; width: 259px;">Linea Aporte
                    <asp:DropDownList ID="DdlLineaAporte" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlLineaAporte_SelectedIndexChanged"
                        Style="margin-bottom: 0px" Width="250px" Enabled="False" CssClass="textbox">
                        <asp:ListItem Value="0">Seleccione un item</asp:ListItem>
                    </asp:DropDownList>
                        </td>
                        <td>Estado
                    <br />
                            <asp:DropDownList ID="DdlEstado" runat="server" Width="160px" CssClass="textbox">
                                <asp:ListItem Value="1">ACTIVO</asp:ListItem>
                                <asp:ListItem Value="2">INACTIVO</asp:ListItem>
                                <asp:ListItem Value="3">CERRADO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="height: 15px; text-align: left; width: 190px;">&nbsp;
                        </td>
                        <td style="height: 15px; text-align: left;">&nbsp;
                        </td>
                        <td style="height: 15px; text-align: left;">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 15px; text-align: left;">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        GridLines="Horizontal" OnPageIndexChanging="gvLista_PageIndexChanging" OnRowEditing="gvLista_RowEditing"
                                        OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                                        PageSize="5" ShowFooter="True" DataKeyNames="numero_aporte" ShowHeaderWhenEmpty="True" Width="847px" Style="text-align: left">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Selección">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="numero_aporte" HeaderText="Número Aporte" />
                                            <asp:BoundField DataField="cod_linea_aporte" HeaderText="Linea">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_linea_aporte" HeaderText="Nombre Linea">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="porcentaje" HeaderText="%Distribución">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cuota" HeaderText="Valor Cuota" SortExpression="cuota"
                                                DataFormatString="{0:C}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Principal">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Chkprincipal" runat="server" Checked='<%#Convert.ToBoolean(Eval("principal")) %>'
                                                        Enabled="False" EnableViewState="true" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign ="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" />
                                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                        <RowStyle CssClass="gridItem" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="4">
                            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado."
                                Visible="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table>
                <tr>
                    <td style="text-align: left" colspan="5">
                        <strong>Datos de Aportes a modificar</strong>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 140px">Valor Cuota<br />
                        <asp:TextBox ID="txtValorCuota" runat="server" AutoPostBack="True" CssClass="textbox"
                            OnTextChanged="txtValorCuota_TextChanged" Width="90%"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtValorCuota_MaskedEditExtender" runat="server" AcceptNegative="Left"
                            DisplayMoney="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999"
                            MaskType="Number" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtValorCuota" />
                    </td>
                    <td style="text-align: left; width: 200px">Periodicicidad<br />
                        <asp:DropDownList ID="DdlPeriodicidad" runat="server" Width="90%" CssClass="textbox">
                        </asp:DropDownList>
                        <br />
                    </td>
                    <td style="text-align: left; width: 140px">Fecha Próximo Pago<br />
                        <asp:TextBox ID="txtFecha_Prox_pago" runat="server" CssClass="textbox" MaxLength="10"
                            Width="85%" />
                        <asp:CalendarExtender ID="txtFecha_Prox_pago_CalendarExtender" runat="server" Enabled="True"
                            Format="dd/MM/yyyy" TargetControlID="txtFecha_Prox_pago">
                        </asp:CalendarExtender>
                    </td>
                    <td style="text-align: left; width: 180px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                FormaPago<br />
                                <asp:DropDownList ID="DdlFormaPago" runat="server" Width="90%" AutoPostBack="true"
                                    OnSelectedIndexChanged="DdlFormaPago_SelectedIndexChanged" CssClass="textbox">
                                    <asp:ListItem Value="1">Caja</asp:ListItem>
                                    <asp:ListItem Value="2">Nomina</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: left; width: 180px">
                        <asp:UpdatePanel ID="updFormapago" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" /><br />
                                <asp:DropDownList ID="ddlEmpresa" runat="server" Width="90%" CssClass="textbox">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DdlFormaPago" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">Fecha Apertura&nbsp;<br />
                        <asp:TextBox ID="txtFecha_aperturaModificar" runat="server" CssClass="textbox"
                            MaxLength="128" Width="157px" />
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                            Format="dd/MM/yyyy" TargetControlID="txtFecha_aperturaModificar">
                        </asp:CalendarExtender>
                        <br />
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
        <asp:View runat="server">
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
                            <asp:Label ID="Label1" runat="server" Text="Operación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
    <uc4:mensajegrabar ID="ctlMensajeEliminar" runat="server" />
</asp:Content>
