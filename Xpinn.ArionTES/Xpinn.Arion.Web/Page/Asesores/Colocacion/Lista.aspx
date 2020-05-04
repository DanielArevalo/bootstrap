<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" EnableEventValidation="true" CodeFile="Lista.aspx.cs" Inherits="Page_Asesores_Colocacion_Lista" Title=".: Xpinn - Asesores :."%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" contentplaceholderid="cphMain"  runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pConsulta" runat="server">
        <table style="width:100%;">
        <tr>
            <td colspan="2">
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:left">
              Tipo de Consultar<br />
                <asp:DropDownList ID="ddlConsultar" runat="server" CssClass="dropdown" 
                    AutoPostBack="True" 
                    onselectedindexchanged="ddlConsultar_SelectedIndexChanged">
                    <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                    <asp:ListItem Value="1">Clientes potenciales - Nuevos créditos</asp:ListItem>
                    <asp:ListItem Value="2">Clientes - Créditos paralelos</asp:ListItem>
                    <asp:ListItem Value="3">Clientes - Cupos preferenciales</asp:ListItem>
                    <asp:ListItem Value="4">Créditos - Renovación</asp:ListItem>                    
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
            <td>
                <asp:Label ID="lblOrdenar" runat="server" Text="Ordenar por" Visible="False"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlOrdenar" runat="server" CssClass="dropdown" 
                    Visible="False">
                    <asp:ListItem Value="0">&lt;Seleccione un Item&gt;</asp:ListItem>
                    <asp:ListItem Value="cod_persona">Codigo cliente</asp:ListItem>
                    <asp:ListItem Value="numero_radicacion">Numero radicación</asp:ListItem>
                    <asp:ListItem Value="cod_linea_credito">Linea</asp:ListItem>
                    <asp:ListItem Value="fecha_solicitud">Fecha solicitud</asp:ListItem>
                    <asp:ListItem Value="monto_aprobado">Monto aprobado</asp:ListItem>
                    <asp:ListItem Value="saldo_capital">Saldo</asp:ListItem>
                    <asp:ListItem Value="valor_cuota">Cuota</asp:ListItem>
                    <asp:ListItem Value="otros_saldos">Atributos</asp:ListItem>
                    <asp:ListItem Value="plazo">Plazo</asp:ListItem>
                    <asp:ListItem Value="cuotas_pagadas">Cuotas pagadas</asp:ListItem>
                    <asp:ListItem Value="fecha_proximo_pago">Fecha próximo pago</asp:ListItem>
                    <asp:ListItem Value="cod_oficina">Oficina</asp:ListItem>
                    <asp:ListItem Value="calificacion_promedio">Calificación promedio</asp:ListItem>
                    <asp:ListItem Value="calificacion_cliente">Calificación cliente</asp:ListItem>
                    <asp:ListItem Value="porc_renovacion_cuotas">Renovación cuotas</asp:ListItem>
                    <asp:ListItem Value="porc_renovacion_montos">Renovación montos</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
                <tr>
            <td style="text-align:left">
                Zona<br />
                <asp:DropDownList ID="ddlZona" runat="server" CssClass="dropdown">
                </asp:DropDownList>
                <asp:Label ID="lblLinea" runat="server">Linea</asp:Label>
                <asp:DropDownList ID="ddlLinea" runat="server" CssClass="dropdown">
                </asp:DropDownList>
            </td>
            <td style="text-align:left">

                <br />
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" CssClass="btn8" onclick="Button1_Click" 
                    Text="Agenda" />
            </td>
        </tr>
        </table>
    </asp:Panel>
    <asp:MultiView ID="mvLista" runat="server">        
                <asp:View ID="vGridClientes" runat="server">
                   
                    <asp:Label ID="lblClientes" runat="server" Text="REPORTE DE CLIENTES"></asp:Label>
                    <br />
                    <br />
                    
                    <asp:Label ID="lblTotalRegs" runat="server" />
                    <br />
                    <asp:Button ID="btnInforme" runat="server" CssClass="btn8" 
                        onclick="btnPrintPagina_Click" Text="Visualizar informe" /> &nbsp;
                    <asp:Button ID="btnExportar" runat="server" CssClass="btn8" 
                        onclick="btnExportar_Click" Text="Exportar a excel" />
                        <asp:GridView ID="gvListaClientes" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" 
                    OnRowCommand="gvLista_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="COD_PERSONA" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo">
<HeaderStyle CssClass="gridColNo"></HeaderStyle>

<ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnIdCliente" runat="server" ImageUrl="~/Images/gr_cancelar.jpg"
                                    ToolTip="Estado Cuenta" CommandName="EstadoCuenta" CommandArgument='<%#Eval("COD_PERSONA")%>' />
                                    
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Observacion" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"
                                     CommandArgument='<%#Eval("COD_PERSONA")%>' />
                            </ItemTemplate>

<HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                       <asp:BoundField DataField="IdCliente" HeaderText="Código cliente" />
                            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="NombreZona" HeaderText="Zona" />
                            <asp:BoundField DataField="Calificacion" HeaderText="Calificación" />
                           <asp:TemplateField HeaderStyle-CssClass="gridIco">
                           

<HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                            <asp:BoundField DataField="observacion" HeaderText="Observacion Cliente" />
                            
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>


                        </asp:View>

                         <asp:View ID="VGclientespotenciales" runat="server">

                         <asp:GridView ID="gvListaClientespotenciales" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" 
                    OnRowCommand="gvLista_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="COD_PERSONA" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo">
<HeaderStyle CssClass="gridColNo"></HeaderStyle>

<ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                
                                    
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Observacion" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"
                                     CommandArgument='<%#Eval("IdCliente")%>' />
                            </ItemTemplate>

<HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                       <asp:BoundField DataField="IdCliente" HeaderText="Código cliente" />
                            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="NombreZona" HeaderText="Zona" />
                            <asp:BoundField DataField="Calificacion" HeaderText="Calificación" />
                           <asp:TemplateField HeaderStyle-CssClass="gridIco">
                           

<HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                            <asp:BoundField DataField="observacion" HeaderText="Observacion Cliente" />
                            
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
                           </asp:View> 





        <asp:View ID="vGridCreditos" runat="server">
                        <hr  width="100%">
            <asp:Label ID="lblCreditos" runat="server" Text="REPORTE DE CRÉDITOS"></asp:Label>
                        <br />
            <br />
            <asp:GridView ID="gvListaCreditos" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="20" GridLines="Horizontal" ShowHeaderWhenEmpty="True"
                    OnPageIndexChanging="gvLista_PageIndexChanging" 
                    OnRowCommand="gvLista_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="COD_PERSONA" HeaderStyle-CssClass="gridColNo" 
                            ItemStyle-CssClass="gridColNo">
<HeaderStyle CssClass="gridColNo"></HeaderStyle>

<ItemStyle CssClass="gridColNo"></ItemStyle>
                        </asp:BoundField>
                        
                        <asp:TemplateField HeaderStyle-CssClass="gridIco">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnIdCliente" runat="server" ImageUrl="~/Images/gr_info.jpg"
                                    ToolTip="Estado Cuenta" CommandName="EstadoCuenta" CommandArgument='<%#Eval("COD_PERSONA")%>' />
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Observacion" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Modificar"
                                     CommandArgument='<%#Eval("COD_PERSONA")%>' />
                            </ItemTemplate>

<HeaderStyle CssClass="gridIco"></HeaderStyle>
                        </asp:TemplateField>
                       <asp:BoundField DataField="idinforme" HeaderStyle-CssClass="gridColNo" ItemStyle-CssClass="gridColNo" />
                    <asp:BoundField DataField="codigo_cliente" HeaderText="Código cliente" />
                    <asp:BoundField DataField="numero_radicacion" HeaderText="Numero crédito" />
                    <asp:BoundField DataField="linea_credito" HeaderText="Línea" />
                    <asp:BoundField DataField="fecha_solicitud_string" HeaderText="Fecha solicitud" />
                    <asp:BoundField DataField="monto_aprobado" HeaderText="Monto aprobado" />
                    <asp:BoundField DataField="saldo_capital" HeaderText="Saldo" />
                    <asp:BoundField DataField="valor_cuota" HeaderText="Cuota" />
                    <asp:BoundField DataField="otros_saldos" HeaderText="Atributos" />
                    <asp:BoundField DataField="plazo" HeaderText="Plazo" />
                    <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuotas pagadas" />
                    <asp:BoundField DataField="fecha_prox_pago_string" HeaderText="Fecha próximo pago" />
                    <asp:BoundField DataField="oficina" HeaderText="Oficina" />
                    <asp:BoundField DataField="calificacion_promedio" 
                        HeaderText="Calif. Promedio" />
                    <asp:BoundField DataField="calificacion_cliente" HeaderText="Calif. Cliente" />
                    <asp:BoundField DataField="porc_renovacion_cuotas" 
                        HeaderText="%Renov. Cuotas" />
                    <asp:BoundField DataField="porc_renovacion_montos" 
                        HeaderText="%Renov. Montos" />
                        <asp:BoundField DataField="observacion" HeaderText="Observacion" />
                        
                </Columns>
                    <HeaderStyle CssClass="gridHeader" />
                    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                    <RowStyle CssClass="gridItem" />
                </asp:GridView>
            <asp:Label ID="lblTotalRegs2" runat="server" />
            <br />
            <asp:Button ID="btnInforme0" runat="server" CssClass="btn8" 
                onclick="btnInforme0_Click" Text="Visualizar informe" /> &nbsp;
            <asp:Button ID="btnExportar0" runat="server" CssClass="btn8" 
                onclick="btnExportar0_Click" Text="Exportar a excel" />





                        <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>





        </asp:View>




        <asp:View ID="vReporteClientes" runat="server">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                <localreport reportpath="Page\Asesores\Colocacion\ReporteClientes.rdlc">
                <datasources>
               <rsweb:ReportDataSource />
                </datasources>
                </localreport>
            </rsweb:ReportViewer>
        </asp:View>
        <asp:View ID="vReporteCreditos" runat="server">
            <rsweb:ReportViewer ID="ReportViewer2" runat="server" Font-Names="Verdana" 
                Font-Size="8pt" InteractiveDeviceInfos="(Colección)" 
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                <localreport reportpath="Page\Asesores\Colocacion\ReporteCreditos.rdlc">
                <datasources>
                 <rsweb:ReportDataSource />
                </datasources>
                </localreport>
            </rsweb:ReportViewer>
        </asp:View>

    </asp:MultiView> 

    <asp:HiddenField ID="HiddenField1" runat="server" />
    
    <asp:ModalPopupExtender ID="mpeNuevo" runat="server" 
        PopupControlID="panelActividadReg" TargetControlID="HiddenField1"
         BackgroundCssClass="backgroundColor" >
        
 </asp:ModalPopupExtender>

    

    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" 
        style="text-align: left" BorderStyle="Solid" Width="452px">
    
                <div id="popupcontainer" style="width:450px">                    
                <div class="row popupcontainertitle">
                    <div class="cell popupcontainercell" style="text-align: center">
                             Observación
                    </div>  
                </div>
                <div class="row">
                        <div class="cell popupcontainercell">
                            <div id="ordereditcontainer">
                            <asp:UpdatePanel ID="upActividadReg" runat="server">
        <ContentTemplate>   
                                <div class="row">
                                    <div class="cell ordereditcell">Cliente&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="Txtcliente" runat="server" AutoPostBack="True" Width="309px"></asp:TextBox>
                                        <br />
                                        Identificación
                                        <asp:TextBox ID="Txtidentificacion" runat="server" AutoPostBack="True"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp; Fecha
                                        <asp:TextBox ID="Txtfecha" runat="server"></asp:TextBox>


                                        <br>
                                              </div>
                                </div>
                                <div class="row">
                                    <div class="cell ordereditcell">Descripción</div>
                                    <div class="cell">
                                    <asp:TextBox ID="txtDescripcionReg" runat="server" CssClass="textbox" 
                                            MaxLength="128" Height="49px" TextMode="MultiLine" Width="438px" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell ordereditcell"></div>
                                </div>
                                     </ContentTemplate>
    </asp:UpdatePanel>

                                <div class="row">
                                   <div class="cell" style="text-align:right">
                                         <asp:Button ID="btnGuardarReg" runat="server" Text="Guardar" style="margin-right:10px;"
                                                 CssClass="button"  
                                             ValidationGroup="vgActividadReg" Height="20px" 
                                             onclick="btnGuardarReg_Click1" />
                                         <asp:Button ID="btnCloseReg" runat="server" Text="Cerrar" 
                                                CssClass="button"  CausesValidation="false" 
                                             Height="20px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
                </div>
   
    </asp:Panel>
</asp:Content>

