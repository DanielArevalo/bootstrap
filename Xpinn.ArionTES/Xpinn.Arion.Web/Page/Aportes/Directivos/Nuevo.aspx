<%@ Page Title=".: Directivos :." Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" %>

<%@ Register Src="~/General/Controles/ctlBusquedaRapida.ascx" TagName="ListadoPersonas" TagPrefix="uc1" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar" TagPrefix="uc4" %>
<%@ Register Src="~/General/Controles/fechaeditable.ascx" TagName="fecha" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <br />
    <br />
    <br />
    <asp:MultiView ID="mvFinal" ActiveViewIndex="0" runat="server">
        <asp:View runat="server">
            <table style="width: 80%;">
                <tr>
                    <td style="width: 250px; text-align: left">Tipo Directivo
                    </td>
                    <td style="text-align: left; width: 250px">
                        <asp:DropDownList ID="ddlTipoDirectivo" AutoPostBack="true" runat="server" CssClass="textbox" Height="27px"
                            Width="182px" OnSelectedIndexChanged="ddlTipoDirectivo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 200px; text-align: left">Identificación
                    </td>
                    <td style="width: 260px; text-align: left">
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="textbox" Width="110px"
                            MaxLength="12" ReadOnly="true"></asp:TextBox>
                        <asp:Button ID="btnConsultaPersonas" CssClass="btn8" runat="server" Text="..." Height="26px"
                            OnClick="btnConsultaPersonas_Click" />
                        <uc1:ListadoPersonas ID="ctlBusquedaPersonas" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodCliente" runat="server" CssClass="textbox"
                            Enabled="False" MaxLength="12" Visible="False" Width="10px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 250px; text-align: left">Nombres y Apellidos
                    </td>
                    <td colspan="4" style="text-align: left">
                        <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="textbox" Enabled="false"
                            Width="542px"></asp:TextBox>
                    </td>
                </tr>
            </table>

            <table style="width: 90%;">
                <tr>
                    <td style="width: 30%; text-align: left">Estado
                        <br />
                        <asp:DropDownList ID="ddlEstado" runat="server" AppendDataBoundItems="true" Width="80%">
                            <asp:ListItem Text="Nombrado" Value="1" />
                            <asp:ListItem Text="Retirado" Value="2" />
                            <asp:ListItem Text="Excluido" Value="3" />
                        </asp:DropDownList>
                    </td>
                    <td style="width: 30%; text-align: left">Calidad
                        <br />
                        <asp:DropDownList ID="ddlCalidad" runat="server" AppendDataBoundItems="true" Width="90%">
                            <asp:ListItem Text="Principal" Value="1" />
                            <asp:ListItem Text="Suplente" Value="2" />
                        </asp:DropDownList>
                    </td>
                    <td colspan="2" style="width: 40%; text-align: left">
                        <table>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkPariente" Text="Tiene Parientes en la entidad?" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkVinculos" Text="Tiene Vínculos con Org. Solidarias?" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>Vigencia Inicial
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <uc1:fecha ID="txtVigenciaInicial" runat="server" Enabled="True" Habilitado="True"
                                    style="font-size: xx-small; text-align: right" TipoLetra="XX-Small" Width_="80" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>Vigencia Final
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <uc1:fecha ID="txtVigenciaFinal" runat="server" Enabled="True" Habilitado="True"
                                    style="font-size: xx-small; text-align: right" TipoLetra="XX-Small" Width_="80" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>Fecha Nombramiento
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <uc1:fecha ID="txtFechaNombramiento" runat="server" Enabled="True" Habilitado="True"
                                    style="font-size: xx-small; text-align: right" TipoLetra="XX-Small" Width_="80" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>Fecha Posesión
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <uc1:fecha ID="txtFechaPosesion" runat="server" Enabled="True" Habilitado="True"
                                    style="font-size: xx-small; text-align: right" TipoLetra="XX-Small" Width_="80" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNumeroRadi" Text="Numero Radicación de Posesión"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:TextBox runat="server" Width="100%" ID="txtNumeroRadi" />
                    </td>
                    <td>Email Institucional
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="100%" ID="txtEmail" />
                        <asp:RegularExpressionValidator ID="revTxtEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="E-Mail no valido!" ForeColor="Red" Style="font-size: x-small" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblEmpresa" Visible="false" Text="Empresa"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:Panel ID="pnlEmpresa" Visible="false" runat="server">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCodEmpresa" Visible="false" runat="server" />
                                    <asp:TextBox runat="server" Width="80%" ReadOnly="true" ID="txtEmpresa" />
                                    <asp:Button ID="btnConsultaEmpresa" CssClass="btn8" runat="server" Text="..." Height="26px"
                                        OnClick="btnConsultaEmpresa_Click" />
                                    <uc1:ListadoPersonas ID="ctlListarEmpresa" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnConsultaEmpresa" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblTarjetaProfesional" Visible="false" Text="Numero Tarjeta Profesional"></asp:Label>
                    </td>
                    <td style="width: 25%; text-align: left">
                        <asp:TextBox runat="server" Width="100%" ID="txtTarjetaProfesional" Visible="false" />
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
                            <asp:Label ID="lblMensaje" runat="server" Text="Operación Realizada Correctamente"
                                Style="color: #FF3300"></asp:Label>
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
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />
</asp:Content>
