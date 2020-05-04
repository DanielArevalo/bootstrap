<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Page_Asesores_Colocacion_Lista" Title=".: Xpinn - Asesores :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%--<%@ Register assembly="BarcodeX" namespace="Fath" tagprefix="bcx" %>--%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphMain">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:MultiView ID="mvLista" runat="server">
        <asp:View ID="vGridViewCreditos" runat="server">
            
                <table style="width: 100%;">
                    <tr>
                        <td colspan="3" style="text-align: left">
                            <strong>Criterios de Búsqueda</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 15%">
                            Num Credito<br />
                            <asp:TextBox ID="txtNumCredito" runat="server" CssClass="textbox" Width="90%" />
                        </td>
                        <td style="text-align: left; width: 35%">
                            Empresa<br />
                            <asp:TextBox ID="txtEmpresa" runat="server" CssClass="textbox" Width="90%" />
                        </td>
                        <td style="text-align: left; width: 35%">
                            Ciudad de Recidencia<br />
                            <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="textbox" Width="90%" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 130px">
                            Código
                            <br />
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="90%" />
                        </td>
                        <td style="text-align: left">
                            Identificación
                            <br />
                            <asp:TextBox ID="txtidentificacion" runat="server" CssClass="textbox" Width="140px" />
                        </td>
                        <td style="text-align: left">
                            Nombre Cliente<br />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" Width="90%" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr style="width: 100%" />
                        </td>
                    </tr>
                </table>           
            <br />

            <asp:Panel ID="pGrilla" runat="server">
                <asp:UpdatePanel ID="UdpGrilla" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvListaClientesExtracto" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="gridPager"
                            RowStyle-CssClass="gridItem" Width="100%" Height="16px" OnPageIndexChanging="gvListaClientesExtracto_PageIndexChanging">
                            <Columns>
                                <%--<asp:ButtonField CommandName="Seleccionar" Text="Seleccionar" />--%>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco"><HeaderTemplate><asp:CheckBox ID="cbSeleccionarEncabezado" runat="server" Checked="false" OnCheckedChanged="cbSeleccionarEncabezado_CheckedChanged"
                                            AutoPostBack="True" /></HeaderTemplate><ItemTemplate><asp:CheckBox ID="cbSeleccionar" runat="server" Checked="false" /></ItemTemplate><HeaderStyle CssClass="gridIco"></HeaderStyle><ItemStyle CssClass="gridIco"></ItemStyle></asp:TemplateField>
                                <asp:BoundField DataField="NumeroRadicacion" HeaderText="Número de crédito" />
                                <asp:BoundField DataField="CodPersona" HeaderText="Cód persona" />
                                <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="CodigoLineaDeCredito" HeaderText="Cód línea" />
                                <asp:BoundField DataField="Linea" HeaderText="Línea" />
                                <asp:BoundField DataField="SaldoCapital" HeaderText="Saldo capital" />
                                <asp:BoundField DataField="Oficina" HeaderText="Oficina" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:ObjectDataSource ID="ClienteExtracto" runat="server"></asp:ObjectDataSource>
                <asp:Label ID="lblTotalRegs" runat="server" />
                <br />
                <asp:Label ID="lblCreditoSeleccionado" runat="server" Font-Bold="True" />
                <br />
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: left">
                            Fecha Inicial
                            <br />
                            <ucFecha:fecha ID="txtfechaIni" runat="server" />
                        </td>
                        <td style="text-align: left">
                            Fecha final
                            <br />
                            <ucFecha:fecha ID="txtfechaFin" runat="server" />
                        </td>
                        <td style="width: 485px">
                            Motivo de generación del extracto
                            <br />
                            <asp:DropDownList ID="ddlMotivoGeneracionExtracto" runat="server" CssClass="dropdown"
                                Width="334px">
                            </asp:DropDownList>
                            <br />
                        </td>
                    </tr>
                    <tr>                       
                        <td align="left" valign="bottom" colspan="2" style="height: 62px">
                            <strong style="text-align: center">Enviar a</strong>
                            <asp:RadioButtonList ID="rdbEnvioPor" Enabled="false"
                                runat="server" Width="200px" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">Pantalla</asp:ListItem>
                                <asp:ListItem Value="1">E-mail</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="width: 485px; height: 62px;">
                            Observaciones
                            <br />
                            <asp:TextBox ID="txtObservaciones" runat="server" Height="36px" MaxLength="250" TextMode="MultiLine"
                                Width="479px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Label ID="lblmsj" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="bottom" colspan="3">
                            <asp:Button ID="btnGenerarExtracto" runat="server" CssClass="btn8" Height="37px"
                                OnClick="btnGenerarExtracto_Click" Text="Generar extracto" Visible="False" 
                                Width="147px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            &nbsp;<asp:Label ID="lblResultado" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
        </asp:View>

        <asp:View ID="vReporteExtracto" runat="server">
            <table>
                <tr>
                    <td style="width: 100%;text-align:center">
                        <asp:Button ID="btnVerData" runat="server" CssClass="btn8"  Height="40px" Text=" Visualizar Datos "
                            onclick="btnVerData_Click"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <rsweb:ReportViewer ID="rvExtracto" runat="server" Width="100%" Font-Names="Verdana"
                            Font-Size="8pt" InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana"
                            WaitMessageFont-Size="14pt" Height="450px"><LocalReport ReportPath="Page\Asesores\ExtractoCreditos\Extracto.rdlc" EnableExternalImages="True"></LocalReport></rsweb:ReportViewer>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="hdFileName" runat="server" />
                        <asp:HiddenField ID="hdFileNameThumb" runat="server" />
                                 <%--<asp:Image ID="imgCodigoBarras" runat="server" Height="60px" Width="300px" ImageUrl="bcx.aspx?data=0"/>--%>

    <br />
</asp:Content>
