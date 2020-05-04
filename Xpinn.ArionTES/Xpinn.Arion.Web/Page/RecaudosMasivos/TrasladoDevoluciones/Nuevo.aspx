<%@ Page Title="" Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/ctlProcesoContable.ascx" TagName="procesoContable" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelGeneral" runat="server">
        <asp:MultiView ID="mvAplicar" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwData" runat="server">
                <table style="width: 540px">
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtNum_Devolucion" runat="server" CssClass="textbox" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 140px">
                            Identificación<br />
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="90%" />
                            <asp:TextBox ID="txtcodPersona" runat="server" CssClass="textbox" Visible="false" />
                        </td>
                        <td style="width: 400px; text-align: left">
                            Nombre<br />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="90%"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left">
                            Fecha de Traslado
                            <br />
                            <ucFecha:fecha ID="txtFecha" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="upFormaDesembolso" runat="server" UpdateMode=Always>
                    <ContentTemplate>
                        <asp:Panel ID="panelGrilla" runat="server">
                            <table style="width: 80%">
                                <tr>
                                    <td style="text-align: left">
                                        <strong>Devoluciones</strong><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvDetalle" runat="server" Width="100%" GridLines="Horizontal" AutoGenerateColumns="False"
                                            AllowPaging="True" PageSize="20" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                                            RowStyle-CssClass="gridItem" DataKeyNames="num_devolucion" 
                                            onrowediting="gvDetalle_RowEditing">
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/gr_edit.jpg" ShowEditButton="False" />
                                                <asp:BoundField DataField="num_devolucion" HeaderText="Código">
                                                    <ItemStyle HorizontalAlign="center" Width="60px"  />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}"></asp:BoundField>
                                                <asp:BoundField DataField="concepto" HeaderText="Mótivo Devolución">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="valor" HeaderText="Valor" DataFormatString="{0:N0}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N0}">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Trasladar" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <cc1:CheckBoxGrid ID="chkTraslador" runat="server" AutoPostBack="true" OnCheckedChanged="chkTraslador_CheckedChanged" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vr.Traslado" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <uc1:decimales ID="txtValorTraslado" runat="server" CssClass="textbox" Width="120px" Enabled="true" AutoPostBack_="true" OneventoCambiar="cambiar_valor"/>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader"></HeaderStyle>
                                            <PagerStyle CssClass="gridPager"></PagerStyle>
                                            <RowStyle CssClass="gridItem"></RowStyle>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        Valor a Aplicar :
                                        <uc1:decimales ID="txtValorAaplicar" runat="server" CssClass="textbox" Width="120px" Enabled="False" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 80%">
                            <tr>
                                <td colspan="5">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: left">
                                    <strong>Forma de Pago</strong><br />
                                    <asp:DropDownList ID="DropDownFormaDesembolso" runat="server" Style="margin-left: 0px;
                                        text-align: left" Width="50%" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="DropDownFormaDesembolso_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>            
                        <asp:Panel ID="panelDeDonde" runat="server">
                            <table style="width: 700px">
                                <tr>
                                    <td style="width: 300px; text-align: left">
                                        Banco de donde se Gira<br />
                                        <asp:DropDownList ID="ddlEntidadOrigen" runat="server" Style="margin-left: 0px; text-align: left"
                                            Width="90%" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlEntidadOrigen_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 200px; text-align: left">
                                        Cuenta de donde se Gira<br />
                                        <asp:DropDownList ID="ddlCuentaOrigen" runat="server" Style="margin-left: 0px; text-align: left"
                                            Width="90%" CssClass="textbox">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="panelhaciaDonde" runat="server">
                            <table style="width: 700px">
                                <tr>
                                    <td style="width: 300px; text-align: left">
                                        Entidad Bancaria<br />
                                        <asp:DropDownList ID="DropDownEntidad" runat="server" Style="margin-left: 0px; text-align: left"
                                            Width="90%" CssClass="textbox">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 200px; text-align: left">
                                        Num. Cuenta<br />
                                        <asp:TextBox ID="txtnumcuenta" runat="server" Width="90%" CssClass="textbox" Style="text-align: left"></asp:TextBox>
                                    </td>
                                    <td style="width: 200px; text-align: left">
                                        Tipo de Cuenta<br />
                                        <asp:DropDownList ID="ddlTipoCuenta" runat="server" Style="margin-left: 0px; text-align: left"
                                            Width="90%" CssClass="textbox">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownFormaDesembolso" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlEntidadOrigen" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:View>
            <asp:View ID="vwFinal" runat="server">
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
                            <td style="text-align: center; font-size: large; color: Red">
                                El traslado de devolución fue grabado&nbsp;correctamente.
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; font-size: large;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
    </asp:Panel>
    <asp:Panel ID="panelProceso" runat="server" Width="100%">
        <uc2:procesoContable ID="ctlproceso" runat="server" />  
    </asp:Panel>  
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
