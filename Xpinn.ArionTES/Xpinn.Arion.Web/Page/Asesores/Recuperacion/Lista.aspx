<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Lista.aspx.cs" Inherits="Page_Asesores_Colocacion_Lista" Title=".: Xpinn - Asesores :." %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/General/Controles/mensajeGrabar.ascx" TagName="mensajegrabar"
    TagPrefix="uc4" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphMain">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:Panel ID="pConsulta" runat="server" Width="816px">
        <table style="width: 118%; height: 105px;">
            <tr>
                <td style="text-align: center" colspan="4" class="align-rt">
                    <span style="color: #0099FF;">
                        <strong style="text-align: center; background-color: #FFFFFF;">
                            <asp:Label ID="LblReportes" runat="server"
                                Style="color: #FF0000; font-weight: 700;" Width="350px"></asp:Label>
                            &nbsp;<br />
                            <asp:Button ID="BtnDescargar" runat="server" CssClass="btn8"
                                OnClick="Button1_Click" Text="Descargar Individual (ZIP)" Visible="False" Width="155px" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnDescargarMasivo" runat="server" CssClass="btn8"
                                OnClick="Button2_Click" Text="Descargar Masiva" Visible="False" Width="155px" />
                        </strong></span>
                </td>
                <td style="text-align: center">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center; width: 326px;">Oficina<br />
                    <asp:DropDownList ID="ddlOficina" runat="server" CssClass="textbox"
                        Width="200px">
                    </asp:DropDownList>
                    <br />
                </td>
                <td style="text-align: center">Zona<br />
                    <asp:DropDownList ID="ddlZona" runat="server" CssClass="textbox" Width="200px">
                    </asp:DropDownList>
                    <br />
                </td>
                <td style="text-align: center">Calificación del cliente<br />
                    <asp:DropDownList ID="ddlCalificacion" runat="server" CssClass="dropdown"
                        Height="30px" Width="200px">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem Value="6">TODAS</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: center" colspan="2">Estado<br />
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="dropdown"
                        Height="30px" Width="200px">                     
                        <asp:ListItem Value="1">Desembolsado</asp:ListItem>
                        <asp:ListItem Value="2">Terminado</asp:ListItem>
                        <asp:ListItem Value="3">Todos</asp:ListItem>                      
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; width: 326px;">Edad de mora<br />
                    <asp:DropDownList ID="ddlMora" runat="server" Height="30px" CssClass="dropdown" Width="200px">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="1 and 30">1-30</asp:ListItem>
                        <asp:ListItem Value="31 and 60">31-60</asp:ListItem>
                        <asp:ListItem Value="61 and 90">61-90</asp:ListItem>
                        <asp:ListItem Value="91 and 120">91-120</asp:ListItem>
                        <asp:ListItem Value="120 and 150">120-150</asp:ListItem>
                        <asp:ListItem Value="151 and 180">151-180</asp:ListItem>
                        <asp:ListItem Value="181 and 270">181-270</asp:ListItem>
                        <asp:ListItem Value="271 and 360">271-360</asp:ListItem>
                        <asp:ListItem Value="11">Mayor a 360</asp:ListItem>
                        <asp:ListItem Value="10">TODAS</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </td>
                <td style="text-align: center">Número. Crédito
                <asp:CompareValidator ID="cvNumeroCredito" runat="server"
                    ControlToValidate="txtCredito" Display="Dynamic"
                    ErrorMessage="Solo se admiten números" ForeColor="Red"
                    Operator="DataTypeCheck" SetFocusOnError="True" Style="font-size: xx-small"
                    Type="Integer" ValidationGroup="vgGuardar" Width="150px" />
                    <br />
                    <asp:TextBox ID="txtCredito" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <br />
                </td>
                <td style="text-align: center">Código Cliente
                <asp:CompareValidator ID="cvCodigoLCiente0" runat="server"
                    ControlToValidate="txtCliente" Display="Dynamic"
                    ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                    SetFocusOnError="True" Style="font-size: xx-small" Type="Integer"
                    ValidationGroup="vgGuardar" Width="126px" />
                    <br />
                    <asp:TextBox ID="txtCliente" runat="server" CssClass="textbox" Width="126px"></asp:TextBox>
                </td>
                <td style="text-align: center">Identificación Cliente
                <asp:CompareValidator ID="cvIdentificacionCliente" runat="server"
                    ControlToValidate="txtIdentiCliente" Display="Dynamic"
                    ErrorMessage="Solo se admiten números" ForeColor="Red" Operator="DataTypeCheck"
                    SetFocusOnError="True" Style="font-size: xx-small" Type="Integer"
                    ValidationGroup="vgGuardar" Width="126px" />
                    <br />
                    <asp:TextBox ID="txtIdentiCliente" runat="server" CssClass="textbox"
                        Width="100px"></asp:TextBox>
                </td>
                <td class="tdI" style="text-align: left">Código de nómina<br />
                    <asp:TextBox ID="txtCodigoNomina" runat="server" CssClass="textbox" Width="100px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center; width: 326px;">&nbsp;</td>
                <td style="text-align: center">Ordenar por<br />
                    <asp:DropDownList ID="ddlOrdenar" Height="30px" runat="server" CssClass="dropdown">
                        <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                        <asp:ListItem Value="cod_persona">Codigo cliente</asp:ListItem>
                        <asp:ListItem Value="numero_radicacion">Numero radicación</asp:ListItem>
                        <asp:ListItem Value="cod_linea_credito">Linea</asp:ListItem>
                        <asp:ListItem Value="fecha_solicitud">Fecha solicitud</asp:ListItem>
                        <asp:ListItem Value="monto_aprobado">Monto aprobado</asp:ListItem>
                        <asp:ListItem Value="saldo_capital">Saldo</asp:ListItem>
                        <asp:ListItem Value="valor_cuota">Cuota</asp:ListItem>
                        <asp:ListItem Value="plazo">Plazo</asp:ListItem>
                        <asp:ListItem Value="cuotas_pagadas">Cuotas pagadas</asp:ListItem>
                        <asp:ListItem Value="fecha_proximo_pago">Fecha próximo pago</asp:ListItem>
                        <asp:ListItem Value="dias_mora">Dias mora</asp:ListItem>
                        <asp:ListItem Value="saldo_mora">Saldo en mora</asp:ListItem>
                        <asp:ListItem Value="saldo_atributos_mora">Saldo atributos en mora</asp:ListItem>
                        <asp:ListItem Value="cod_oficina">Oficina</asp:ListItem>
                        <asp:ListItem Value="calificacion_promedio">Calificación promedio</asp:ListItem>
                        <asp:ListItem Value="calificacion_cliente">Calificación cliente</asp:ListItem>
                        <asp:ListItem Value="estado">Estado de credito</asp:ListItem>
                        <asp:ListItem Value="estado_juridico">Estado jurídico</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: center">
                    <span style="color: #0099FF;">
                        <strong style="text-align: center; background-color: #FFFFFF;">
                            <asp:Button ID="btnCargaMasiva" runat="server" CssClass="btn8"
                                OnClick="btnCargaMasiva_Click" Text="Cargar Archivo Masivo" Width="155px" />
                        </strong></span></td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:MultiView ID="mvLista" runat="server">
        <asp:View ID="vGridViewCreditos" runat="server">
            <hr noshade width="100%" />
            <div style="overflow: scroll; width: 100%; height: 520px">
                <asp:GridView ID="gvListaCreditos" runat="server" AllowPaging="True" AutoGenerateColumns="False" HeaderStyle-Height="30px"
                    DataKeyNames="idinforme" GridLines="Horizontal" OnPageIndexChanging="gvLista_PageIndexChanging"
                    OnSelectedIndexChanged="gvLista_SelectedIndexChanged" ShowHeaderWhenEmpty="True"
                    Style="text-align: left; font-size: x-small;" Width="100%" Height="500px">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="gridIco" ItemStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Detalle" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridIco" />
                            <ItemStyle CssClass="gridIco" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo_cliente" HeaderText="Código cliente" />
                        <asp:BoundField DataField="identificacion" HeaderText="Identificación" />
                        <asp:BoundField DataField="numero_radicacion" HeaderText="Número crédito" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre Deudor" />
                        <asp:BoundField DataField="linea_credito" HeaderText="Linea" />
                        <asp:BoundField DataField="fecha_solicitud_string" HeaderText="Fecha solicitud" />
                        <asp:BoundField DataField="monto_aprobado" DataFormatString="{0:C}" HeaderText="Monto aprobado"
                            ItemStyle-Wrap="False">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_capital" DataFormatString="{0:C}" HeaderText="Saldo"
                            ItemStyle-Wrap="False">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="valor_cuota" DataFormatString="{0:C}" HeaderText="Cuota"
                            ItemStyle-Wrap="False">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="plazo" HeaderText="Plazo" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuotas pagadas" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_prox_pago_string" HeaderText="Fecha próximo pago" />
                        <asp:BoundField DataField="dias_mora" HeaderText="Dias mora" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_mora" DataFormatString="{0:C}" HeaderText="Saldo en mora"
                            ItemStyle-Wrap="False">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_atributos_mora" DataFormatString="{0:C}" HeaderText="Saldo atributos en mora"
                            ItemStyle-Wrap="False">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="oficina" HeaderText="Oficina" />
                        <asp:BoundField DataField="calificacion_promedio" HeaderText="Calif. Promedio" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="calificacion_cliente" HeaderText="Calif. Cliente" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado de crédito" />
                        <asp:BoundField DataField="estado_juridico" HeaderText="Estado jurídico" />
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridPager" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
            </div>
            <asp:Label ID="lblTotalRegs" runat="server" />
            <br />
            <br />
            Tipo Documento Generar carta<br />
            <asp:DropDownList ID="ddlTipoDocumento" runat="server"
                CssClass="textbox"
                Width="300" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged">
                <asp:ListItem Value="0"> Seleccione Un Item</asp:ListItem>
                <asp:ListItem Value="1">Habeas Data</asp:ListItem>
                <asp:ListItem Value="2">Habeas Data Con Codeudor</asp:ListItem>
                <asp:ListItem Value="3">Prejuridico</asp:ListItem>
                <asp:ListItem Value="9">Carta_Moras</asp:ListItem>
                <asp:ListItem Value="10">Carta_Moras_Codeudor</asp:ListItem>
                <asp:ListItem Value="4">Prejuridico Codeudor</asp:ListItem>
                <asp:ListItem Value="5">Juridico</asp:ListItem>
                <asp:ListItem Value="6">Juridico Codeudor</asp:ListItem>
                <asp:ListItem Value="7">Campaña</asp:ListItem>
                <asp:ListItem Value="8">Citación</asp:ListItem>
            </asp:DropDownList>
            &nbsp;<asp:Button ID="btnConsultar" runat="server" CssClass="btn8"
                OnClick="btnConsultar_Click1" Text="Generar Cartas" />
            &nbsp;&nbsp;
            <br />
            &nbsp;&nbsp;
            <asp:Button ID="btnInforme" runat="server" CssClass="btn8"
                OnClick="btnInforme_Click" Text="Visualizar informe" />
            &nbsp;
               <asp:Button ID="btnExportar" runat="server" CssClass="btn8"
                   Text="Exportar a excel" OnClick="btnExportar_Click" />
        </asp:View>
        <asp:View ID="vReporteCreditos" runat="server">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"
                Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Colección)"
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Page\Asesores\Recuperacion\ReporteCreditos.rdlc"></LocalReport>
            </rsweb:ReportViewer>

        </asp:View>
    </asp:MultiView>

    <asp:ModalPopupExtender ID="mpeEstructuraArchivo" runat="server"
        PopupControlID="PanelEstructura" TargetControlID="HiddenField1" X="300" Y="300"
        BackgroundCssClass="backgroundColor">
    </asp:ModalPopupExtender>
    <asp:Panel ID="PanelEstructura" runat="server" Width="656px">
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:UpdatePanel ID="upPanelEstructura" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnCargar" />
            </Triggers>
            <ContentTemplate>
                <div id="popupcontainer"
                    style="border: medium groove #0000FF; width: 878px; background-color: #FFFFFF;">
                    <div class="row popupcontainertitle">
                        <div class="gridHeader" style="text-align: center">DILIGENCIAS MASIVAS</div>
                    </div>
                    <div class="row">
                        <div class="cell popupcontainercell">
                            <div id="ordereditcontainer">
                                <div class="row">
                                    <div class="cell" style="text-align: left">
                                        <asp:FileUpload ID="FileUploadCargarArchivo" runat="server"
                                            CssClass="breadcrumb" Width="180px" />
                                        <asp:Button ID="BtnCargar" runat="server" CssClass="btn8"
                                            OnClick="BtnCargar_Click" Text="Cargar" Width="48px" />
                                        <strong style="text-align: center; background-color: #FFFFFF;">
                                            <span style="color: #000000;">&nbsp;<asp:Button ID="btnCloseEstructura" runat="server" CausesValidation="false"
                                                CssClass="button" Height="20px" OnClick="btnCloseEstructura_Click"
                                                Text="Cerrar" />
                                                <asp:Label ID="LblArchivo" runat="server"
                                                    Style="color: #FF0000; font-weight: 700;" Width="526px"></asp:Label>
                                                <br />
                                                <br />
                                            </span><span style="color: #0099FF;">&nbsp;</span><span style="color: #000000;">ESTRUCTURA: 
                                        (Archivo separado por (&#39;;&#39;))&nbsp;&nbsp;
                                        <br />
                                                <br />
                                            </span></strong>
                                        <br />
                                    </div>
                                    <div class="cell" style="width: 436px">
                                        <img src="../../../Images/ArchivoplanoEstruc.jpg" alt="" />
                                    </div>
                                    <div class="cell" style="text-align: right">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:HiddenField ID="HiddenField2" runat="server" />
    <uc4:mensajegrabar ID="ctlMensaje" runat="server" />

</asp:Content>

