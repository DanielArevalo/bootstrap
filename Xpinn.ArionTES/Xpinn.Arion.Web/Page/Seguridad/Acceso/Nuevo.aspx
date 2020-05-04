<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../../General/Controles/ctlMensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="ctl" %>
<%@ Reference VirtualPath="~/Page/FabricaCreditos/ModificacionCredito/Nuevo.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <script type="text/javascript">
        function PanelClick(sender, e) {
        }

        function ActiveTabChanged(sender, e) {
        }

        function mpeSeleccionOnOk() {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }

        function mpeSeleccionOnCancel() {
        }



    </script>


    <table style="width: 100%">
        <tr>
            <td style="text-align: left; width: 118px;">
                <asp:Label ID="Labelerror" runat="server"
                    Style="color: #FF0000; font-weight: 700; font-size: x-small;" colspan="5"
                    Text=""></asp:Label>
            </td>
            <td style="text-align: right; width: 7px;">&nbsp;</td>
            <td style="text-align: right"></td>
        </tr>
        <tr>
            <td style="text-align: left">
                <span style="font-weight: bold;">Datos del Perfil</span>
            </td>
            <td style="width: 7px">&nbsp;
            </td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: left">Código
                <br />
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" title="Código del Perfil"></asp:TextBox>
            </td>
            <td style="width: 7px">&nbsp;
            </td>
            <td style="text-align: left">Nombre                
                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="Campo Requerido"
                    ControlToValidate="txtDescripcion" Display="Dynamic" ForeColor="Red"
                    ValidationGroup="vgGuardar" Style="font-size: xx-small" />
                <br />
                <asp:TextBox ID="txtDescripcion" runat="server"
                    CssClass="textbox" title="Descripción o nombre del Perfil"
                    Width="350px" Style="text-transform: uppercase"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 118px; text-align: left" colspan="3">
                <span style="font-weight: bold;">Permisos Asignados</span>
            </td>
        </tr>
        <tr>
            <td style="width: 118px; text-align: left">Modulo
            </td>
            <td style="width: 118px; text-align: left" colspan="2">
                <asp:DropDownList ID="ddlModulo" runat="server" CssClass="textbox" Width="200px" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlModulo_SelectedIndexChanged" />
            </td>
        </tr>
    </table>
    <table style="width: 90%">
        <tr>

            <td colspan="3">
                <div style="overflow: scroll; height: 271px; overflow-x: hidden;">
                    <asp:GridView ID="gvLista" runat="server" Width="80%"
                        AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                        OnRowDataBound="gvLista_RowDataBound" OnRowCommand="gvLista_OnRowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("cod_opcion") %>' ID="cod_opcion"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="nombreopcion" HeaderText="Descripción">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="400px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Consultar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chbconsulta" runat="server" Checked='<%# Convert.ToBoolean(Eval("consultar")) %>' Width="40px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crear">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chbcreacion" runat="server" Checked='<%# Convert.ToBoolean(Eval("insertar")) %>' Width="40px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modificar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chbmodificacion" runat="server" Checked='<%# Convert.ToBoolean(Eval("modificar")) %>' Width="40px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Borrar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chbborrado" runat="server" Checked='<%# Convert.ToBoolean(Eval("borrar")) %>' Width="40px" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Permisos" ImageUrl="~/Images/gr_info.jpg"
                                        ToolTip="Detalle" Width="16px" Visible="False" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="gridIco"></HeaderStyle>
                                <ItemStyle VerticalAlign="Top" Width="20px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <%-- Popup modificacion de opciones --%>

    <asp:HiddenField ID="hfDocAnexo" runat="server" Visible="True" />
    <asp:ModalPopupExtender ID="mpeDocAnexo" runat="server" Enabled="True" BackgroundCssClass="backgroundColor"
        PopupControlID="Panl1" TargetControlID="hfDocAnexo" CancelControlID="BtnCerrarCampos" >
    </asp:ModalPopupExtender>

    <asp:Panel ID="Panl1" runat="server" CssClass="modalPopup ext" Style="background: rgba(184, 180, 185, .5); padding: 50px;">
        <div runat="server" id="Campos" style="background: rgba(184, 180, 185, .5); margin: 0 auto; margin-top: -40px; overflow: scroll; height: 30pc">
            <strong><span style="font-size: small">Campos Editables</span></strong>
            <br />
            <asp:GridView ID="gvCampos" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="black" Width="100" Height="100"
                BorderWidth="1px" ForeColor="Black" GridLines="Both" Visible="False"
                CellPadding="0" ShowFooter="True" Style="background: #fff;" OnDataBound="gvCampos_OnDataBound"
                AutoPostBack="false">
                <Columns>
                    <asp:TemplateField HeaderText="Campo">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("Campo") %>' ID="lblCampo"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Visualizar">
                        <ItemTemplate>
                            <asp:CheckBox ID="chVisaulizar" runat="server" Checked='<%# Convert.ToBoolean(Eval("Visible")) %>' Width="40px" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <asp:CheckBox ID="chEditable" runat="server" Checked='<%# Convert.ToBoolean(Eval("Editable")) %>' Width="40px" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="lightseagreen" />
            </asp:GridView>

        </div>
        <asp:Button ID="btnGuardarCampos" runat="server" Text="Guardar" CssClass="button" OnClick="btnGuardarCampos_Click"/>
        &nbsp;&nbsp;&nbsp;    
        <asp:Button ID="BtnCerrarCampos" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrarCampos_Click" CausesValidation="false" />
        &nbsp;&nbsp;&nbsp;    
    </asp:Panel>

    <ctl:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
