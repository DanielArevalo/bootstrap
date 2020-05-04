<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="Nuevo.aspx.cs" Inherits="Nuevo" Title=".: Xpinn - Presupuesto :." %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../../General/Controles/fecha.ascx" TagName="fecha" TagPrefix="uc1" %>
<%@ Register src="../../../General/Controles/decimalesGridRow.ascx" tagname="decimales" tagprefix="uc2" %>
<%@ Register src="~/General/Controles/porcentajeGrid.ascx" tagname="porcentaje" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
    
    <script type="text/javascript" src="../../../Scripts/gridviewScroll.min.js"></script>   

    <script type="text/javascript">

        function pageLoad() {
            $('#<%=gvProyeccion.ClientID%>').gridviewScroll({
                width: 1000,
                height: 400,
                freezesize: 3,
                startVertical: $("#<%=hfgvProyeccionSV.ClientID%>").val(),
                startHorizontal: $("#<%=hfgvProyeccionSH.ClientID%>").val(),
                onScrollVertical: function (delta) {
                    $("#<%=hfgvProyeccionSV.ClientID%>").val(delta);
                },
                onScrollHorizontal: function (delta) {
                    $("#<%=hfgvProyeccionSH.ClientID%>").val(delta);
                },
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
        }
        
        function ActiveTabChanged(sender, e) {
            $('#<%=gvFlujo.ClientID%>').gridviewScroll({
                width: 1000,
                height: 400,
                freezesize: 2,
                arrowsize: 30,
                varrowtopimg: "../../../Images/arrowvt.png",
                varrowbottomimg: "../../../Images/arrowvb.png",
                harrowleftimg: "../../../Images/arrowhl.png",
                harrowrightimg: "../../../Images/arrowhr.png"
            });
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

        function ToggleHidden(value) {
            $find('<%=Tabs.ClientID%>').get_tabs()[2].set_enabled(value);
        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:HiddenField ID="hfgvProyeccionSV" runat="server" /> 
    <asp:HiddenField ID="hfgvProyeccionSH" runat="server" /> 

    <asp:MultiView ID="mvPresupuesto" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEncabezado" runat="server">
            <table id="tbEncabezado" border="0" cellpadding="5" cellspacing="0" width="70%">
                <tr>
                    <td style="width: 169px; text-align:left" colspan="2">
                        Código<br/>
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox" Width="104px" 
                            Enabled="False" />
                    </td>
                    <td colspan="2" style="text-align:left">
                        Tipo de Presupuesto<br />
                        <asp:DropDownList ID="ddlTipoPresupuesto" runat="server" CssClass="dropdown" 
                            Width="303px" Height="25px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdI" colspan="2" style="text-align:left">
                        Centro de Costo<br />
                        <asp:DropDownList ID="ddlCentroCosto" runat="server" CssClass="dropdown" 
                            Width="181px" Height="25px" AppendDataBoundItems="True">
                            <asp:ListItem Selected="True" Value="0">Consolidado</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="tdI" colspan="6" style="text-align:left">
                        Descripción<br />
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="128" 
                            Width="666px" Height="55px" TextMode="MultiLine" />
                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server"
                            TargetControlID="txtDescripcion"
                            WatermarkText="Digite la descripción del presupuesto"
                            WatermarkCssClass="watermarked" />
                        <br/>
                    </td>
                </tr>
            </table>
            <table id="Table2" border="0" cellpadding="5" cellspacing="0" width="70%">
                <tr>
                    <td style="text-align:left; width: 208px;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            Número de Períodos<br />
                            <asp:TextBox ID="txtNumPeriodos" runat="server" Width="91px" style="text-align:center"
                                CssClass="textbox" Height="22px" />
                            <asp:ImageButton ID="img1" runat="server" ImageUrl="~/images/down.gif"
                                AlternateText="Down" Width="15" Height="15"/>
                            <asp:ImageButton ID="img2" runat="server" ImageUrl="~/images/up.gif"
                                AlternateText="Up" Width="15" Height="15"/>
                            <ajaxToolkit:NumericUpDownExtender ID="nudPeriodos" runat="server" 
                                TargetControlID="txtNumPeriodos" Tag="" Width="120" 
                                ServiceUpPath="" ServiceDownPath="" 
                                ServiceUpMethod="" ServiceDownMethod=""
                                RefValues="" TargetButtonDownID="img1" TargetButtonUpID="img2" 
                                Maximum="100" Minimum="0" />                    
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="tdI" colspan="2" style="width: 93px; text-align:left">
                        Periodicidad<br />
                        <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="dropdown" 
                            Width="329px" Height="25px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdI" colspan="2" style="text-align:left; width: 268435456px;">
                        Fecha Inicial<br />
                        <uc1:fecha ID="txtPeriodoInicial" runat="server" Width="126px" />
                    </td>
                    <td class="tdI">
                        &nbsp;&nbsp;</td>
                </tr>
            </table>
            <br />
            <hr />
            <table id="Table1" border="0" cellpadding="5" cellspacing="0" width="100%">
                <tr>
                    <td class="tdI" style="width: 151px; text-align:left">
                        Fecha Elaboración<br />
                        <uc1:fecha ID="txtFechaElabora" runat="server" Enabled="False"></uc1:fecha>
                    </td>
                    <td class="tdI" style="text-align:left">
                        Elaborado Por<br />
                        <asp:TextBox ID="txtElaborador" runat="server" CssClass="textbox" 
                            Width="435px" Enabled="False" />
                    </td>
                    <td class="tdD">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdI" style="width: 151px; text-align:left">
                        Fecha Aprobación<br />
                        <uc1:fecha ID="txtFechaAprobacion" runat="server"></uc1:fecha>
                    </td>
                    <td class="tdI" style="text-align:left">
                        Aprobado Por<br />
                        <asp:TextBox ID="txtAprobador" runat="server" CssClass="textbox" 
                            Width="440px" />
                    </td>
                    <td class="tdD">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdI" colspan="3" style="text-align:center">
                        <asp:TextBox ID="txtNivel" Visible="false" runat="server" CssClass="textbox" Height="22px" style="text-align:center" Width="91px" />
                         <asp:ImageButton ID="btnSiguiente" runat="server" 
                            ImageUrl="~/Images/btnSiguiente.jpg" onclick="btnSiguiente_Click" />
                    </td>
                </tr>
            </table>
        </asp:View>

        <asp:View ID="vwDetalle" runat="server">
            <br />
            <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="1010" 
                style="text-align: left" OnClientActiveTabChanged="ActiveTabChanged">               
                <ajaxToolkit:TabPanel ID="tabPyG" runat="server" HeaderText="P y G">
                    <HeaderTemplate>                                                                       
                        P y G Presupuestado                                        
                    </HeaderTemplate>                                        
                    <ContentTemplate>                                                
                        <asp:Button ID="btnExpPresupuesto" runat="server" CssClass="btn8" 
                            onclick="btnExpPresupuesto_Click" onclientclick="btnExpPresupuesto_Click" 
                            Text="Exportar a excel" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<strong style="text-align:left; color: #FFFFFF; background-color: #0066FF">P Y G PRESUPUESTADO</strong>
                        <asp:GridView ID="gvProyeccion" runat="server" AutoGenerateColumns="False" Font-Size="XX-Small" PageSize="20" Style="font-size: x-small"
                            OnRowCommand="gvProyeccion_RowCommand" OnRowDataBound="gvProyeccion_RowDataBound" OnRowEditing="gvProyeccion_RowEditing" 
                            OnRowCancelingEdit="gvProyeccion_RowCancelingEdit" OnRowUpdating="gvProyeccion_RowUpdating" DataKeyNames="cod_cuenta">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" 
                                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="Select" 
                                                ImageUrl="~/Images/gr_edit.jpg" ToolTip="Nuevo" Width="16px" />                                                                                                                               
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                        <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                                    </EditItemTemplate>
                                    <ControlStyle Width="20px" />
                                    <ItemStyle Width="20px" />
                                    </asp:TemplateField>
                                <asp:BoundField DataField="cod_cuenta" HeaderText="Código" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Cuentas" ReadOnly="True">
                                    <ControlStyle Width="180px" />
                                    <ItemStyle HorizontalAlign="Left" Width="180px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="depende_de" HeaderText="Depende De" Visible="False" 
                                    ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipo" HeaderText="Tipo" Visible="False" 
                                    ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_promedio" DataFormatString="{0:N}" 
                                    HeaderText="Saldo Promedio" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo_final" DataFormatString="{0:N}" 
                                    HeaderText="Saldo Actual" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="incremento" DataFormatString="{0:N2}" 
                                    HeaderText="Incremento" ReadOnly="True">                                        
                                    <ItemStyle HorizontalAlign="Right" Width="100px" />                                        
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#FFFF99" Height="20px" Width="20px" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle Font-Size="XX-Small" />
                        </asp:GridView>                                      
                    </ContentTemplate>                                
                    </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tabFlujoCaja" runat="server" HeaderText="Flujo de Caja">
                    <HeaderTemplate>                                                                    
                        Flujo de Caja                                           
                    </HeaderTemplate>                                        
                    <ContentTemplate>                                                                      
                        <asp:Button ID="btnExpFlujo" runat="server" CssClass="btn8" 
                            onclick="btnExpFlujo_Click" onclientclick="btnExpFlujo_Click" 
                            Text="Exportar a excel" />&nbsp;&nbsp;&nbsp;&nbsp;<strong style="text-align:left; color: #FFFFFF; background-color: #0066FF">FLUJO PRESUPUESTADO</strong>
                        <strong>Flujo Inicial --></strong>
                        <asp:TextBox ID="txtFlujoInicial" runat="server" onclick = "click7(this)" MaxLength="11" 
                            style="text-align:right" Width="80px" Font-Size="Small" BackColor="#F4F5FF" AutoPostBack="True" 
                            ontextchanged="txtFlujoInicial_TextChanged"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeFlujoInicial" runat="server" TargetControlID="txtFlujoInicial"         
                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                        <%--<div id="dvflu" style="overflow:scroll; height:400px; width:100%">--%>
                            <asp:GridView ID="gvFlujo" runat="server" AutoGenerateColumns="False" Font-Size="XX-Small" PageSize="20" 
                                ShowHeaderWhenEmpty="True" Style="font-size: x-small" >
                                <Columns>
                                    <asp:BoundField DataField="cod_cuenta" HeaderText="Código"><ItemStyle 
                                        HorizontalAlign="Left" Width="40px" /></asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Cuentas"><ItemStyle 
                                        HorizontalAlign="Left" Width="180px" /></asp:BoundField>
                                    <asp:BoundField DataField="depende_de" HeaderText="Depende De" Visible="False"><ItemStyle 
                                        HorizontalAlign="Left" /></asp:BoundField>
                                    <asp:BoundField DataField="tipo" HeaderText="Tipo" Visible="False"><ItemStyle 
                                        HorizontalAlign="Left" /></asp:BoundField>
                                </Columns>
                                <EditRowStyle BackColor="#FFFF99" Height="20px" Width="20px" />
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />                              
                                <SelectedRowStyle Font-Size="XX-Small" />
                            </asp:GridView>
                       <%-- </div>--%>                                          
                    </ContentTemplate>                                                
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer> 

            <br />
            <asp:ImageButton ID="btnAnterior" runat="server" 
                ImageUrl="~/Images/btnRegresar.jpg" onclick="btnAnterior_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
       
            <asp:Panel ID="panelDetalle" runat="server" BackColor="#ccccff" Style="text-align: left; width:470px">
                <div id="popupcontainer" style="width: 470px">
                <div class="row popupcontainertitle">
                <div class="cell popupcontainercell" style="text-align: center; width: 470px;">
                    DETALLE DEL PRESUPUESTO
                    <asp:UpdatePanel ID="upDetalle" runat="server">                    
                    <ContentTemplate>                   
                    <table border="1" style="width: 470px">
                        <tr>
                            <td style="width: 176px; text-align:left; font-weight: 700; font-size: x-small;">
                                Cod. Cuenta
                            </td>
                            <td style="width: 150px; text-align:left; font-size: x-small;">
                                <asp:Label ID="lblcodcuenta" runat="server" Text="" Enable="False"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align:left; font-size: x-small;">                                    
                                C/C <asp:Label ID="dropcc" runat="server" Text="" Enable="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 176px; text-align:left; font-weight: 700; font-size: x-small;">
                                Nombre Cuenta
                            </td>
                            <td style="width: 150px; text-align:left; font-size: x-small;" colspan="2">
                                <asp:Label ID="lblNomCuenta" runat="server" Text="" Enable="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 176px; text-align:left; font-weight: 700; font-size: x-small;">
                                Saldo Cuenta
                            </td>
                            <td colspan="2" 
                                style="width: 150px; text-align:left; font-size: x-small;  text-align:left;">
                                <asp:Label ID="lblSaldoCuenta" runat="server" Text="" Enable="False"></asp:Label>
                                <asp:CheckBox ID="cbSaldoCuenta" runat="server" Checked="False" 
                                    AutoPostBack="True" oncheckedchanged="cbSaldoCuenta_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 176px; text-align:left; font-weight: 700; font-size: x-small;">
                                Saldo Promedio
                            </td>
                            <td colspan="2" 
                                style="width: 150px; text-align:left; font-size: x-small; text-align:left;">
                                <asp:Label ID="lblSaldoPromedio" runat="server" Text="" Enable="False" 
                                    style="text-align: left" ></asp:Label>
                                <asp:CheckBox ID="cbSaldoPromedio" runat="server" Checked="True" 
                                    AutoPostBack="True" oncheckedchanged="cbSaldoPromedio_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 176px; text-align:left; font-weight: 700; font-size: x-small;">
                                Movimiento Ultimo Periodo
                            </td>
                            <td colspan="2" 
                                style="width: 150px; text-align:left; font-size: x-small; text-align:left;">
                                <asp:Label ID="lblSaldoPeriodo" runat="server" Text="" Enable="False" ></asp:Label>
                                <asp:CheckBox ID="cbSaldoPeriodo" runat="server" Checked="False" AutoPostBack="True" oncheckedchanged="cbSaldoPeriodo_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 176px; text-align:left; font-weight: 700; font-size: x-small;">                                
                                Tipo Movimiento
                            </td>
                            <td style="width: 150px; text-align:left" colspan="2">                                
                                <asp:DropDownList ID="ddlTipo" runat="server" Style="font-size: x-small" 
                                    Width="150px" CssClass="dropdown" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTipo_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Valor Fijo</asp:ListItem>
                                    <asp:ListItem Value="2">Incremento Porcentual</asp:ListItem>
                                    <asp:ListItem Value="3">Manual</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 176px; text-align:left; font-weight: 700; font-size: x-small;">                                
                                <asp:Label ID="lbValor" runat="server" Text="Valor"></asp:Label>
                            </td>
                            <td style="width: 120px; text-align:left; font-size: x-small;" colspan="2">                                
                                <asp:TextBox MaxLength="10" ID="txtValor" runat="server" Width="150px" Style="text-align:right"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="logo" colspan="2">                                        
                                <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" 
                                    Font-Size="XX-Small" PageSize="20" ShowHeaderWhenEmpty="True" 
                                    Style="font-size: x-small" Visible="False" Width="230px">
                                    <Columns>
                                        <asp:BoundField DataField="fecha" HeaderText="Período" DataFormatString="{0:dd/MM/yyy}"><ItemStyle 
                                            HorizontalAlign="Left" /></asp:BoundField>
                                        <asp:TemplateField HeaderText="Valor Presupuestado">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtValorPresupuesto" runat="server" 
                                                    Text='<%# Eval("valor_presupuestado") %>' Visible="True" onclick = "click7(this)" 
                                                    style="text-align:right" Width="60" Font-Size="XX-Small" AutoPostBack="True" ></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:TemplateField>     
                                    </Columns>
                                    <EditRowStyle BackColor="#FFFF99" Width="80px" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle Font-Size="XX-Small" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    </ContentTemplate>        
                    </asp:UpdatePanel>
                    <table border="1" width="470px">
                        <tr>
                            <td style="text-align:left" colspan="3">
                                <asp:Button ID="btnPresupuestar" runat="server" Text="Guardar" CssClass="button" CausesValidation="false" OnClick="btnPresupuestar_Click" style="height: 26px" />
                                <asp:Button ID="btnCerrar" runat="server" CausesValidation="false" CssClass="button" OnClick="btnCerrar_Click" Text="Cerrar" style="height: 26px" />
                            </td>
                        </tr>
                    </table>
                </div>
                </div>
                </div>
            </asp:Panel>                  
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeNuevo" runat="server" PopupControlID="panelDetalle"
                TargetControlID="HiddenField1" BackgroundCssClass="backgroundColor">                            
            </ajaxToolkit:ModalPopupExtender>   
        </asp:View>

        <asp:View ID="mvFinal" runat="server">
            <asp:Panel id="PanelFinal" runat="server">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br /><br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Label ID="lblMensaje" runat="server" 
                                Text="Presupuesto Grabado Correctamente" style="color: #FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            <asp:Button ID="btnFinal" runat="server" Text="Finalizar" 
                                onclick="btnFinalClick" />&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>

    </asp:MultiView>

    <asp:HiddenField ID="HiddenField2" runat="server" />

    <ajaxToolkit:ModalPopupExtender ID="mpeGrabar" runat="server" 
        PopupControlID="panelActividadReg" TargetControlID="HiddenField2"
        BackgroundCssClass="backgroundColor" >
    </ajaxToolkit:ModalPopupExtender>
   
    <asp:Panel ID="panelActividadReg" runat="server" BackColor="White" Style="text-align: right" BorderWidth="1px" Width="500px" >
        <div id="Div1" style="width: 500px">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align:center">
                        Esta Seguro de Grabar el Presupuesto ?
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="text-align:center">
                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar"
                            CssClass="btn8"  Width="182px" onclick="btnContinuar_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnParar" runat="server" Text="Cancelar" CssClass="btn8" 
                            Width="182px" onclick="btnParar_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>       
        </div>
    </asp:Panel>

</asp:Content>