<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Page_Asesores_Colocacion_Lista" Title=".: Xpinn - Asesores :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width: 100%;">
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server" Text="Consultar"></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlConsultar" runat="server" AutoPostBack="True"
                        CssClass="dropdown" OnSelectedIndexChanged="ddlConsultar_SelectedIndexChanged">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="1">CARTERA EN MORA A LA FECHA POR EJECUTIVO</asp:ListItem>
                        <asp:ListItem Value="2">REPORTE DE COBRANZAS </asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:CompareValidator ID="cvConsultar" runat="server"
                        ControlToValidate="ddlConsultar" Display="Dynamic"
                        ErrorMessage="Seleccione un tipo de consulta" ForeColor="Red"
                        Operator="GreaterThan" SetFocusOnError="true" Type="Integer"
                        ValidationGroup="vgGuardar" ValueToCompare="0">
                    </asp:CompareValidator>
                    <br />
                </td>
                <td align="center">
                    <asp:Label ID="Labelejecutivos" runat="server" Text="Ejecutivos"></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlAsesores" runat="server" AutoPostBack="True"
                        CssClass="dropdown"
                        OnSelectedIndexChanged="ddlAsesores_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CheckBox ID="ChkTodos" runat="server" Text="Todos" />
                    <br />
                </td>

            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center" Width="209px">
                        <asp:Label ID="LabelFecha_gara1" runat="server" Text="Fecha Inicial Diligencia"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFechaIni" runat="server" CssClass="textbox" Height="23px"
                            MaxLength="10" Width="106px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="Image1" TargetControlID="txtFechaIni"></asp:CalendarExtender>
                        <asp:Label ID="Label3" runat="server" Style="color: #FF3300"></asp:Label>
                    </asp:Panel>
                </td>
                <td align="center">
                    <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center" Width="250px">
                        <asp:Label ID="LabelFecha_gara0" runat="server" Text="Fecha Final Diligencia"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="textbox" Height="23px"
                            MaxLength="10" Width="106px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="Image2" TargetControlID="txtFechaFin"></asp:CalendarExtender>
                        <asp:Label ID="Label4" runat="server" Style="color: #FF3300"></asp:Label>
                    </asp:Panel>
                </td>
                <td style="margin-left: 60px"></td>
                <td>&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:MultiView ID="mvLista" runat="server" OnActiveViewChanged="mvLista_ActiveViewChanged">
        <asp:View ID="vGridReporteMora" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1000px;">
                <div style="width: 1500px;">

                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                        OnClick="btnExportar_Click" Text="Exportar a excel" />


                    <asp:GridView ID="gvReoirtemora" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="NumRadicacion" HeaderStyle-CssClass="gridHeader"
                        OnRowDataBound="gvReoirtemora_RowDataBound"
                        OnSelectedIndexChanged="gvReoirtemora_SelectedIndexChanged"
                        PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True"
                        Style="font-size: x-small" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                                <HeaderStyle CssClass="gridIco" />
                                <ItemStyle CssClass="gridIco" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="icodigo" HeaderText="Código">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombres" HeaderText="Nombres Cliente">
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificacion" HeaderText="Identificación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NumRadicacion" HeaderText="Número de Radicación">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pagare" HeaderText="Pagare">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="valor_cuota" DataFormatString="{0:C}"
                                HeaderText="Valor Cuota">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dias_mora" HeaderText="Días en Mora">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo_capital" DataFormatString="{0:C}"
                                HeaderText="Saldo a Capital">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="garantia_comunitaria" DataFormatString="{0:C}"
                                HeaderText="Valor G.Comunitaria">
                                <ItemStyle ForeColor="Red" HorizontalAlign="Right" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pendite_cuota" DataFormatString="{0:C}"
                                HeaderText="Valor Pendiente">
                                <ItemStyle HorizontalAlign="left" Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha_cuota" DataFormatString="{0:d}"
                                HeaderText="Fecha Cuota Pendiente">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="direccion_oficina" HeaderText="Dirección Empresa">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="barrio_empresa" HeaderText="Barrio Empresa">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="telefono_empresa" HeaderText="Telefono Empresa">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="direccion_negocio" HeaderText="Direccion Negocio">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="barrio_negocio" HeaderText="Barrio Negocio" />
                            <asp:BoundField DataField="telefono_negocio" HeaderText="Telefono Negocio">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="direccion_residencia"
                                HeaderText="Dirección Residencia">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="barrio_residencia" HeaderText="Barrio Residencia">
                                <ItemStyle HorizontalAlign="center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="telefono_residencia"
                                HeaderText="Telefono Residencia" />
                            <asp:BoundField DataField="celular" HeaderText="Celular" />
                            <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                            <asp:BoundField DataField="oficina" HeaderText="Oficina" />
                            <asp:BoundField DataField="idpromotor" HeaderText="Cod Ejec." />
                            <asp:BoundField DataField="nombre_asesor" HeaderText="Nombre Ejecutivo" />
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                    </asp:GridView>
                </div>
            </div>
            <asp:Label ID="lblTotalRegs" runat="server" />
            <br />
            <br />
            &nbsp;                                                            
        </asp:View>
        <asp:View ID="VGridReporteCobranzas" runat="server">
            <div style="overflow: scroll; height: 500px; width: 1000px;">
                <div style="width: 1500px;">

                    <asp:Button ID="btnExportarDilig" runat="server" CssClass="btn8"
                        OnClick="btnExportarDilig_Click" Text="Exportar a excel" />

                    <div style="text-align: center">
                        <asp:GridView ID="gvRepCobranza" runat="server" AutoGenerateColumns="False"
                            GridLines="Horizontal" HeaderStyle-CssClass="gridHeader" OnRowDataBound ="gvRepCobranza_RowDataBound"
                            PagerStyle-CssClass="gridPager" RowStyle-CssClass="gridItem" ShowFooter="True"
                            Style="font-size: x-small" Width="64%">
                            <Columns>
                                <asp:BoundField DataField="nombre_asesor" HeaderText="COBRADOR">
                                    <ItemStyle HorizontalAlign="center" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_credito" HeaderText="CRÉDITO">
                                    <ItemStyle HorizontalAlign="center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigo" HeaderText="CÓDIGO DEUDOR">
                                    <ItemStyle HorizontalAlign="center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificacion" HeaderText="IDENTIFICACIÓN">
                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombrecliente" HeaderText="NOMBRES CLIENTE">
                                    <ItemStyle HorizontalAlign="left" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha_pago" DataFormatString="{0:d}"
                                    HeaderText="FECHA_PAGO">
                                    <ItemStyle HorizontalAlign="center" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valor_pago" DataFormatString="{0:c}"
                                    HeaderText="VALOR_PAGO">
                                    <ItemStyle HorizontalAlign="center" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cantidad" HeaderText="NO_GESTION_X_COBRADOR">
                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="Acuerdo" HeaderText="Acuerdo" >
                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Forma_pago"
                                    HeaderText="FORMA PAGO">
                                    <ItemStyle HorizontalAlign="center" Width="90px" />
                                </asp:BoundField>

                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblTotalRegscobranzas" runat="server" />
            <br />
            <br />
            &nbsp;                                                            
        </asp:View>
    </asp:MultiView>

</asp:Content>

