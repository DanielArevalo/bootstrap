<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlSeleccionarPersona.ascx.cs" Inherits="ctlSeleccionarPersona" Debug="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Panel ID="pConsulta" runat="server">
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function isOnlyLetter(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (!(charCode >= 65 && charCode <= 120) && (charCode != 32 && charCode != 0)) {
                return false;
            }
            return true
        }
    </script>
    <asp:Label ID="lblMensaje" runat="server" Style="font-size: small; color: #FF3300" />
    <asp:Panel ID="pEncBusqueda" runat="server" CssClass="collapsePanelHeader" Height="30px">
        <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
            <div style="float: left; color: #0066FF; font-size: small">Búsqueda Avanzada</div>
            <div style="float: left; margin-left: 5px; color: #0066FF; font-size: small">
                <asp:Label ID="lblMostrarDetalles" runat="server">(Mostrar Detalles...)</asp:Label>
            </div>
            <div style="float: right; vertical-align: middle;">
                <asp:ImageButton ID="imgExpand" runat="server" ImageUrl="~/Images/expand.jpg" AlternateText="(Show Details...)" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pBusqueda" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="height: 15px; text-align: left; font-size: x-small;" colspan="4">
                    <strong>Criterios de Búsqueda:</strong></td>
            </tr>
            <tr>
                <td style="text-align: left;" width="14%">Tipo<br />
                    <asp:DropDownList ID="ddlTipoPersona" runat="server" Width="59%" Height="25px"
                        CssClass="textbox" AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="N">Natural</asp:ListItem>
                        <asp:ListItem Value="J">Jurídica</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left;" width="14%">Código<br />
                    <asp:TextBox ID="txtCodigo" Width="55%" runat="server" onkeypress="return isNumber(event)" CssClass="textbox"></asp:TextBox>
                </td>
                <td style="text-align: left;" width="14%">Razón Social<br />
                    <asp:TextBox ID="txtRazonSocial" Width="55%" runat="server" CssClass="textbox"
                        Enabled="true"></asp:TextBox>
                </td>
                <td style="text-align: left;" width="14%">Tipo de Rol<br />
                    <asp:DropDownList ID="ddlTipoRol" Width="59%" runat="server" CssClass="textbox" AppendDataBoundItems="True" Height="25px">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="A" Text="Asociado"></asp:ListItem>
                        <asp:ListItem Value="T" Text="Tercero"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: left;" width="14%">Ciudad<br />
                    <asp:DropDownList ID="ddlCiudad" Width="59%" runat="server" Height="25px"
                        CssClass="textbox" AppendDataBoundItems="True">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" width="14%">Identificación<br />
                    <asp:TextBox ID="txtNumeIdentificacion" Width="55%" onkeypress="return isNumber(event)" runat="server" CssClass="textbox"
                        AutoPostBack="True"
                        OnTextChanged="txtNumeIdentificacion_TextChanged"></asp:TextBox>
                </td>
                <td style="text-align: left;" width="14%">Primer Nombre<br />
                    <asp:TextBox Width="55%" ID="txtNombres" onkeypress="return isOnlyLetter(event)" runat="server" CssClass="textbox"
                        Enabled="true"></asp:TextBox>
                </td>
                <td style="text-align: left;" width="14%">Segundo Nombre<br />
                    <asp:TextBox ID="txtSegundoNombre" Width="55%" onkeypress="return isOnlyLetter(event)" runat="server" CssClass="textbox"
                        Enabled="true"></asp:TextBox>
                </td>
                <td style="text-align: left;" width="14%">Primer Apellido<br />
                    <asp:TextBox ID="txtApellidos" Width="55%" onkeypress="return isOnlyLetter(event)" runat="server" CssClass="textbox"
                        Enabled="true"></asp:TextBox>
                </td>
                <td style="text-align: left;" width="14%">Segundo Apellido<br />
                    <asp:TextBox ID="txtSegundoApellido" Width="55%" onkeypress="return isOnlyLetter(event)" runat="server" CssClass="textbox"
                        Enabled="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" width="14%">Código de nómina<br />
                    <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="55%" ></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:CollapsiblePanelExtender ID="cpeDemo" runat="Server"
        TargetControlID="pBusqueda"
        ExpandControlID="pEncBusqueda"
        CollapseControlID="pEncBusqueda"
        Collapsed="False"
        TextLabelID="lblMostrarDetalles"
        ImageControlID="imgExpand"
        ExpandedText="(Click Aqui para Ocultar Detalles...)"
        CollapsedText="(Click Aqui para Mostrar Detalles...)"
        ExpandedImage="~/Images/collapse.jpg"
        CollapsedImage="~/Images/expand.jpg"
        SuppressPostBack="true"
        SkinID="CollapsiblePanelDemo" />
</asp:Panel>
<hr width="100%" />
Ordenar Por&nbsp;
<table style="width: 60%">
    <tr>
        <td style="width: 45%">
            <asp:DropDownList ID="ddlOrdenar" runat="server" CssClass="dropdown"
                Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlOrdenar_SelectedIndexChanged">
                <asp:ListItem Value=""></asp:ListItem>
                <asp:ListItem Value="1" Text="Código"></asp:ListItem>
                <asp:ListItem Value="2" Text="Tipo Persona"></asp:ListItem>
                <asp:ListItem Value="3" Text="Identificación"></asp:ListItem>
                <asp:ListItem Value="4" Text="Dígito Verificación"></asp:ListItem>
                <asp:ListItem Value="5" Text="Tipo de Identificación"></asp:ListItem>
                <asp:ListItem Value="6" Text="Fecha de Expedición"></asp:ListItem>
                <asp:ListItem Value="7" Text="Ciudad de Expedición"></asp:ListItem>
                <asp:ListItem Value="8" Text="Sexo"></asp:ListItem>
                <asp:ListItem Value="9" Text="Primer Nombre"></asp:ListItem>
                <asp:ListItem Value="10" Text="Segundo Nombre"></asp:ListItem>
                <asp:ListItem Value="11" Text="Primer Apellido"></asp:ListItem>
                <asp:ListItem Value="12" Text="Segundo Apellido"></asp:ListItem>
                <asp:ListItem Value="13" Text="Razón Social"></asp:ListItem>
                <asp:ListItem Value="14" Text="Fecha de Nacimiento"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<div class="AlphabetPager" style="text-align: left">
    <asp:Repeater ID="rptAlphabets" runat="server">
        <ItemTemplate>
            <asp:LinkButton ID="lbtAlphabet" runat="server" Text='<%#Eval("Value")%>'
                Visible='<%# !Convert.ToBoolean(Eval("Selected"))%>' OnClick="Alphabet_Click"
                OnClientClick="Alphabet_Click" />
            <asp:Label ID="lblAlphabet" runat="server" Text='<%#Eval("Value")%>' Visible='<%# Convert.ToBoolean(Eval("Selected"))%>' />
        </ItemTemplate>
    </asp:Repeater>
</div>
<table style="width: 100%">
    <tr>
        <td style="text-align: center">
            <asp:GridView ID="gvLista" runat="server" Width="100%" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                OnPageIndexChanging="gvLista_PageIndexChanging" OnRowDeleting="gvLista_RowDeleting"
                OnRowDataBound="gvLista_RowDataBound" DataKeyNames="cod_persona,tipo_identificacion,identificacion"
                Style="font-size: x-small" OnSelectedIndexChanged="gvLista_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="gridIco">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                ToolTip="Detalle" Width="16px" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridIco"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="cod_persona" HeaderText="Código">
                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nom_tipo_persona" HeaderText="Tipo Persona">
                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="digito_verificacion" HeaderText="D.V.">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="primer_nombre" HeaderText="Primer Nombre">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="segundo_nombre" HeaderText="Segundo Nombre">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="primer_apellido" HeaderText="Primer Apellido">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="segundo_apellido" HeaderText="Segundo Apellido">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="razon_social" HeaderText="Razón Social">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="codciudadexpedicion" HeaderText="Ciudad">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="direccion" HeaderText="Dirección">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="telefono" HeaderText="Teléfono">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="email" HeaderText="E-Mail">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="valor_afiliacion" HeaderText="Valor Afiliación" DataFormatString="{0:n0}">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tipo_identificacion" HeaderText="Tipo Identificación" Visible="false">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="gridHeader" />
                <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                <RowStyle CssClass="gridItem" />
            </asp:GridView>
            <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
            <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningún resultado." Visible="False" />
        </td>
    </tr>
</table>
<asp:Label ID="Label1" runat="server" Visible="False" />
<asp:Label ID="Label2" runat="server" Style="text-align: center;" Text="Su consulta no obtuvo ningun resultado." Visible="False" />
<div style="font-size: x-small">
    Mostrar&nbsp;<asp:DropDownList ID="ddlPageSize" runat="server" CssClass="dropdown" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
        Width="74px" AutoPostBack="True" />&nbsp;Registros por Página
</div>
<br />
<br />
<br />
<br />

