<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true" CodeFile="NuevoCompleto.cs" Inherits="Nuevo" Title=".: Xpinn - Presupuesto :." %>
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

        function click7(TextBox) {
            var i, CellValue, Row, td;
            var table = document.getElementById('<%=gvColocacion.ClientID %>');
            var resultado = parseInt(table.rows[7].cells[6].innerHTML) + parseInt(table.rows[6].cells[6].innerHTML) + parseInt(table.rows[5].cells[6].innerHTML) + parseInt(table.rows[4].cells[6].innerHTML) + parseInt(table.rows[3].cells[6].innerHTML) + parseInt(table.rows[2].cells[6].innerHTML);
            Row = table.rows[7];
            td = Row.cells[3];
            CellValue = td.children[0].attributes[0].value;
            document.getElementById(TextBox.id).value = document.getElementById(CellValue).toString();
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
                    <td class="tdI" colspan="2" style="text-align:left">
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
<%--                                            <asp:ImageButton ID="btnModificar" runat="server"
                                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="Edit" 
                                                ImageUrl="~/Images/gr_info.jpg" ToolTip="Editar" Width="16px"/>   --%>                                                                                                                               
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
            <asp:ImageButton ID="btnCartera" runat="server" 
                ImageUrl="~/Images/btnCartera.jpg" onclick="btnCartera_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnNomina" runat="server" 
                ImageUrl="~/Images/btnNomina.jpg" onclick="btnNomina_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnActivosFij" runat="server" 
                ImageUrl="~/Images/btnActivosFij.jpg" onclick="btnActivosFij_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnObligaciones" runat="server" 
                ImageUrl="~/Images/btnObligaciones.jpg" onclick="btnObligaciones_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnDiferidos" runat="server" 
                ImageUrl="~/Images/btnDiferidos.jpg" onclick="btnDiferidos_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnTecnologia" runat="server" 
                ImageUrl="~/Images/btnTecnologia.jpg" onclick="btnTecnologia_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnOtros" runat="server" 
                ImageUrl="~/Images/btnOtros.jpg" onclick="btnOtros_Click" />
       
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

        <asp:View ID="vwCartera" runat="server">
            <br />
            <div class="divExcelCol" style="text-align:left;">
            <asp:Button ID="btnExpColocacion" runat="server" CssClass="btn8" 
                onclick="btnExpColocacion_Click" onclientclick="btnExpColocacion_Click" 
                Text="Exportar a excel" /> 
            </div>
            <asp:UpdatePanel ID="UpdatePanelDatos" runat="server">
            <ContentTemplate>
                <asp:Panel ID="IngCol_header" runat="server" style="cursor: pointer;">
                    <div class="heading" style="text-align:left;">
                        <asp:ImageButton ID="IngCol_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse" />
                        <strong style="text-align: center;color: #FFFFFF; background-color: #0066FF">DATOS DEL PRESUPUESTO</strong>
                    </div>
                </asp:Panel>
                <asp:Panel id="IngCol_content" runat="server" style="overflow:hidden; text-align:left">
                    Por Favor Ingresar los Datos del Presupuesto y Modifique la Columna de <span style="font-style:italic">Colocacion por Ejecutivo</span>
                    <table id="Table3" border="0" cellpadding="5" cellspacing="0" width="90%">
                        <tr>
                            <td style="text-align:left; font-size: x-small;">
                                Fecha de Corte<br /><uc1:fecha ID="txtFechaCorte" runat="server" Enabled="false" />
                            </td>
                            <td style="text-align:left; font-size: x-small;">
                                Vr Promedio x Credito<br />
                                <uc2:decimales ID="txtValorPorCredito" runat="server" Enabled="true" />
                            </td>
                            <td style="text-align:left; font-size: x-small;">
                                % Polizas Vendidas<br />
                                <asp:TextBox ID="txtPorPoliza" runat="server" onclick = "click7(this)" MaxLength="11" 
                                    style="text-align:right" Width="80px" Font-Size="Small" BackColor="#F4F5FF" AutoPostBack="True" 
                                    ontextchanged="txtPorPoliza_TextChanged"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbePorPoliza" runat="server" TargetControlID="txtPorPoliza"         
                                    FilterType="Custom, Numbers" ValidChars="0123456789.," />
                            </td>
                            <td style="text-align:left; font-size: x-small;">
                                Vr Unitario de Poliza<br />
                                <uc2:decimales ID="txtValPoliza" runat="server" Enabled="true" />
                            </td>
                            <td style="text-align:left; font-size: x-small;">
                                % Comision Poliza<br />
                                <asp:TextBox ID="txtComision" runat="server" onclick = "click7(this)" MaxLength="11" 
                                    style="text-align:right" Width="80px" Font-Size="Small" BackColor="#F4F5FF" AutoPostBack="True" 
                                    ontextchanged="txtComision_TextChanged"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbetxtComision" runat="server" TargetControlID="txtComision"         
                                    FilterType="Custom, Numbers" ValidChars="0123456789.," />
                            </td>
                            <td style="text-align:left; font-size: x-small;">
                                %LeyMiPYME<br />
                                <asp:TextBox ID="txtLeyMiPYME" runat="server" onclick = "click7(this)" MaxLength="11" 
                                    style="text-align:right" Width="80px" Font-Size="Small" BackColor="#F4F5FF" AutoPostBack="True" 
                                    ontextchanged="txtLeyMiPYME_TextChanged"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbetxtLeyMiPYME" runat="server" TargetControlID="txtLeyMiPYME"         
                                    FilterType="Custom, Numbers" ValidChars="0123456789.," />
                            </td>
                            <td style="text-align:left; font-size: x-small;">
                                %Provision<br />
                                <asp:TextBox ID="txtPorProvision" runat="server" onclick = "click7(this)" MaxLength="11" 
                                    style="text-align:right" Width="80px" Font-Size="Small" BackColor="#F4F5FF" AutoPostBack="True" 
                                    ontextchanged="txtPorProvision_TextChanged"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbetxtPorProvision" runat="server" TargetControlID="txtPorProvision"         
                                    FilterType="Custom, Numbers" ValidChars="0123456789.," />
                            </td>
                            <td style="text-align:left; font-size: x-small;">
                                %Prov.General<br />
                                <asp:TextBox ID="txtPorProvisionGeneral" runat="server" onclick = "click7(this)" MaxLength="11" 
                                    style="text-align:right" Width="80px" Font-Size="Small" BackColor="#F4F5FF" AutoPostBack="True" 
                                    ontextchanged="txtPorProvisionGeneral_TextChanged"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbetxtPorProvisionGeneral" runat="server" TargetControlID="txtPorProvisionGeneral"         
                                    FilterType="Custom, Numbers" ValidChars="0123456789.," />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <ajaxToolkit:CollapsiblePanelExtender ID="cpeDatos" runat="Server"
                    TargetControlID="IngCol_content" ExpandControlID="IngCol_header"
                    CollapseControlID="IngCol_header" Collapsed="False" ExpandedImage="~/images/collapse.jpg"
                    CollapsedImage="~/images/expand.jpg" ImageControlID="IngCol_ToggleImage" /> 

                <asp:Panel ID="IngColP_header" runat="server" style="cursor: pointer;">
                    <div class="heading" style="text-align:left;">
                        <asp:ImageButton ID="IngColP_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse" />
                        <strong style="text-align: center;color: #FFFFFF; background-color: #0066FF">PRESUPUESTO DE COLOCACION</strong>
                    </div>
                </asp:Panel>
                <asp:Panel id="IngColP_content" runat="server" style="overflow:hidden;" ScrollBars="Horizontal">                               
                    <div style="overflow:scroll;width:auto;">
                        <asp:GridView ID="gvColocacion" runat="server" 
                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Style="font-size: xx-small" DataKeyNames="codigo" 
                            onrowediting="gvColocacion_RowEditing" OnRowCancelingEdit="gvColocacion_RowCancelingEdit" onrowupdating="gvColocacion_RowUpdating" 
                            EditRowStyle-Width="20" EditRowStyle-Height="20" EditRowStyle-BackColor="#FFFF99" Font-Size="XX-Small" 
                            onrowdatabound="gvColocacion_RowDataBound">
                            <AlternatingRowStyle Width="20px" />
                            <Columns>
                                <asp:TemplateField HeaderText="#" ItemStyle-Width="16">
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                        <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <span>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                        </span>
                                    </ItemTemplate>
                                    <ItemStyle Width="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("descripcion") %>' Width="160"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="160px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Número de Ejecutivos">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumEjecutivos" runat="server" Text='<%# Bind("num_ejecutivos") %>'></asp:Label>
                                        <asp:TextBox ID="txtNumEjecutivos" runat="server" 
                                            Text='<%# Eval("num_ejecutivos") %>' Visible="False" onclick = "click7(this)" 
                                            style="text-align:right" Width="30" Font-Size="XX-Small" AutoPostBack="True"
                                            ontextchanged="txtNumEjecutivos_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeNumEjecutivos" runat="server" TargetControlID="txtNumEjecutivos"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                </asp:TemplateField>                                        
                            </Columns>
                            <FooterStyle BackColor="#FFFFCC" />
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle Font-Size="XX-Small" />
                            <EditRowStyle Font-Size="XX-Small" Width="160px" Font-Italic="True" />
                        </asp:GridView>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeColoca" runat="Server"
                    TargetControlID="IngColP_content" ExpandControlID="IngColP_header"
                    CollapseControlID="IngColP_header" Collapsed="False" ExpandedImage="~/images/collapse.jpg"
                    CollapsedImage="~/images/expand.jpg" ImageControlID="IngColP_ToggleImage" /> 
            </ContentTemplate>
            </asp:UpdatePanel>  

            <br />
            <div class="regresar" style="text-align:center;">
                <asp:ImageButton ID="btnRegresarCAR" runat="server" 
                    ImageUrl="~/Images/btnCargarPresupuesto.jpg" onclick="btnRegresarCAR_Click" />
            </div>

            <br />
            <asp:UpdatePanel ID="UpdatePanelSaldos" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Saldos_HeaderPanel" runat="server" style="cursor: pointer;">
                    <div class="heading" style="text-align:left;">
                        <asp:ImageButton ID="Saldos_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse" />
                        <strong style="text-align: center;color: #FFFFFF; background-color: #0066FF">SALDOS DE CARTERA</strong>
                    </div>
                </asp:Panel>
                <asp:Panel id="Saldos_ContentPanel" runat="server" style="overflow:hidden;">
	                <p>
                    <table id="Table6" border="0" cellpadding="5" cellspacing="0" width="100%">
                        <tr>
                            <td class="columnForm50" style="width: 169px" colspan="2">
                                <asp:GridView ID="gvSaldos" runat="server" 
                                    ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                                    Style="font-size: x-small" PageSize="5" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="cod_clasifica" HeaderText="Cod." Visible="False" ><ItemStyle HorizontalAlign="Left" Width="20px" /></asp:BoundField>
                                        <asp:BoundField DataField="descripcion" HeaderText="Línea de Crédito" ><ItemStyle HorizontalAlign="Left" Width="120px" /></asp:BoundField>
                                        <asp:BoundField DataField="cod_oficina" HeaderText="Cod.Ofi" Visible="False" ><ItemStyle HorizontalAlign="Left" Width="20px" /></asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Oficina" ><ItemStyle HorizontalAlign="Left" Width="120px" /></asp:BoundField>
                                        <asp:BoundField DataField="saldo" HeaderText="Saldo Cap." 
                                            DataFormatString="{0:N}" ><ItemStyle HorizontalAlign="Right" Width="80px" /></asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#FFFFCC" />
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                    <SelectedRowStyle Font-Size="XX-Small" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
 	                </p>
	                <br />
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeSaldos" runat="Server"
                    TargetControlID="Saldos_ContentPanel" ExpandControlID="Saldos_HeaderPanel"
                    CollapseControlID="Saldos_HeaderPanel" Collapsed="False" ExpandedImage="~/images/collapse.jpg"
                    CollapsedImage="~/images/expand.jpg" ImageControlID="Saldos_ToggleImage" /> 
            </ContentTemplate>
            </asp:UpdatePanel>

        </asp:View>

        <asp:View ID="mvNomina" runat="server">       
            <br /><br />
            <ajaxToolkit:TabContainer runat="server" ID="tcNomina" 
                ActiveTabIndex="0" Width="100%" OnClientActiveTabChanged="ActiveTabChanged"
                style="margin-right: 30px; text-align: left">               
                <ajaxToolkit:TabPanel ID="tpNomina" runat="server" HeaderText="Nomina">
                    <HeaderTemplate>                                                                        
                        Nomina                                      
                    </HeaderTemplate>                                        
                    <ContentTemplate>
                        <asp:Button ID="btnExpNomina" runat="server" CssClass="btn8" 
                            onclick="btnExpNomina_Click" onclientclick="btnExpNomina_Click" 
                            Text="Exportar a Excel Nomina" />
                        <asp:Button ID="btnExpTotalNomina" runat="server" CssClass="btn8" 
                            onclick="btnExpTotalNomina_Click" onclientclick="btnExpTotalNomina_Click" 
                            Text="Exportar a Excel Totales" />   
                        <asp:UpdatePanel ID="upNomina" runat="server">                    
                            <ContentTemplate>                           
                            <asp:Button ID="btnIncrementoGeneral" runat="server" CssClass="btn8" onclick="btnIncrementoGeneral_Click" onclientclick="btnIncrementoGeneral_Click" Text="Incremento General"  />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCumplimientoGeneral" runat="server" CssClass="btn8" onclick="btnCumplimientoGeneral_Click" onclientclick="btnCumplimientoGeneral_Click" Text="Cumplimiento General"  />    
                            <div id="table-container-nom" style="overflow:scroll; height:400px; width:100%">    
                                <asp:GridView ID="gvNomina" runat="server" Height="280px" ShowHeaderWhenEmpty="True" CssClass="gridTitulos"
                                    AutoGenerateColumns="False" Style="font-size: xx-small" DataKeyNames="codigo" ShowFooter="True" CellPadding="1"
                                    onrowediting="gvNomina_RowEditing" OnRowCancelingEdit="gvNomina_RowCancelingEdit" onrowupdating="gvNomina_RowUpdating" 
                                    onrowdatabound="gvNomina_RowDataBound" onrowdeleting="gvNomina_RowDeleting" onrowcommand="gvNomina_RowCommand" >
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle Width="60px" />
                                            <FooterStyle Width="60px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                                <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew" ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" ValidationGroup="vgGuardarNuevo" />                                
                                            </FooterTemplate>
                                            <HeaderStyle CssClass="gridIco" />
                                        </asp:TemplateField> 
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                                    ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Código" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtCodigoF" runat="server" Text='<%# Bind("CODIGO") %>' style="font-size: x-small" Width="60" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCodigo" runat="server" TargetControlID="txtCodigoF"         
                                                    FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                <asp:RequiredFieldValidator ID="rfvCodigoF" runat="server" ControlToValidate="txtCodigoF"
                                                    Display="Dynamic"  ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                    ValidationGroup="vgGuardarNuevo" style="font-size: xx-small" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="60"  />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="60" Enabled="False" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre del Empleado" >
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNombreF" runat="server" Text='<%# Bind("NOMBRE") %>' style="font-size: x-small; text-transform :uppercase" MaxLength="200" Width="150" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("NOMBRE") %>' style="font-size: x-small; text-transform :uppercase"  Width="150" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtNombre" runat="server" Text='<%# Eval("NOMBRE") %>' style="font-size: x-small; text-transform :uppercase"  Width="150" />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="140px" />
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha de Ingreso" >
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFechaIngresoF" runat="server" Text='<%# Bind("FECHA_INGRESO") %>' style="font-size: x-small" Width="60" />
                                                <ajaxToolkit:CalendarExtender ID="txtFechaIngresoF_CalendarExtender" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="txtFechaIngresoF">
                                                </ajaxToolkit:CalendarExtender>
                                                <ajaxToolkit:MaskedEditExtender ID="txtFechaIngresoF_MaskedEditExtender" 
                                                    runat="server"  TargetControlID="txtFechaIngresoF" Mask="99/99/9999" 
                                                    clearmaskonlostfocus="false" MaskType="None" UserDateFormat="MonthDayYear">
                                                </ajaxToolkit:MaskedEditExtender>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaIngreso" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("FECHA_INGRESO")) %>'  style="font-size: x-small" Width="60" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtFechaIngreso" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("FECHA_INGRESO")) %>'  style="font-size: x-small" Width="60" />
                                                <ajaxToolkit:CalendarExtender ID="txtFechaIngreso_CalendarExtender" runat="server" 
                                                    Format="dd/MM/yyyy" TargetControlID="txtFechaIngreso">
                                                </ajaxToolkit:CalendarExtender>
                                                <ajaxToolkit:MaskedEditExtender ID="txtFechaIngreso_MaskedEditExtender" 
                                                    runat="server"  TargetControlID="txtFechaIngreso" Mask="99/99/9999" 
                                                    clearmaskonlostfocus="false" MaskType="None" UserDateFormat="DayMonthYear">
                                                </ajaxToolkit:MaskedEditExtender>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod.Ofi" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlCodOficinaF" runat="server" 
                                                    DataSource="<%# ListaOficinas() %>" DataTextField="nombre" DataValueField="cod_oficina" SelectedValue='<%# Bind("COD_OFICINA") %>' 
                                                    style="font-size: x-small; text-align: left" Width="100" Height="20px" AppendDataBoundItems="True" >
                                                    <asp:ListItem Value="0">Sin Oficina</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodOficina" runat="server" Text='<%# String.Format("{0:N0}", Eval("OFICINA")) %>'  style="font-size: x-small; text-align: left" Width="100" />
                                            </ItemTemplate>
                                            <EditItemTemplate>                                                    
                                                <asp:DropDownList ID="ddlCodOficina" runat="server" 
                                                    DataSource="<%# ListaOficinas() %>" DataTextField="nombre" DataValueField="cod_oficina" SelectedValue='<%# Bind("COD_OFICINA") %>' 
                                                    style="font-size: xx-small; text-align: left" Width="100px" Height="20px" AppendDataBoundItems="True" >
                                                    <asp:ListItem Value="0">Sin Oficina</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tipo Salario" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlTipoSalarioF" runat="server" style="font-size: x-small; text-align: left" Width="80" >
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem Value="1">Normal</asp:ListItem>
                                                    <asp:ListItem Value="2">Integral</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTipoSalario" runat="server" Text='<%# Eval("NOM_TIPO_SALARIO") %>'  style="font-size: x-small; text-align: center" Width="80" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlTipoSalario" runat="server" SelectedValue='<%# Bind("TIPO_SALARIO") %>' 
                                                    style="font-size: x-small; text-align: left"  Width="80" AppendDataBoundItems="True" >
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem Value="1">Normal</asp:ListItem>
                                                    <asp:ListItem Value="2">Integral</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salario Act." >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtSalarioF" runat="server" Text='<%# Bind("SALARIO") %>' style="font-size: x-small" Width="65" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeSalarioF" runat="server" TargetControlID="txtSalarioF"         
                                                    FilterType="Custom, Numbers" ValidChars="0123456789" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSalario" runat="server" Text='<%# String.Format("{0:N}", Eval("SALARIO")) %>' style="font-size: xx-small"  Width="70" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtSalario" runat="server" Text='<%# Eval("SALARIO") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeSalario" runat="server" TargetControlID="txtSalario"         
                                                    FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cargo" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlCargoF" runat="server" 
                                                    DataSource="<%# ListarCargos() %>" DataTextField="nom_cargo" DataValueField="cod_cargo" SelectedValue='<%# Bind("CARGO") %>' 
                                                    style="font-size: xx-small; text-align: left" Width="80px" AppendDataBoundItems="True">
                                                    <asp:ListItem Value="0">Sin Cargo</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCargo" runat="server" Text='<%# String.Format("{0:N0}", Eval("NOM_CARGO")) %>'  style="font-size: xx-small; text-align: left" Width="80" />
                                            </ItemTemplate>
                                            <EditItemTemplate>    
                                                <asp:DropDownList ID="ddlCargo" runat="server" 
                                                    DataSource="<%# ListarCargos() %>" DataTextField="nom_cargo" DataValueField="cod_cargo" SelectedValue='<%# Bind("CARGO") %>' 
                                                    style="font-size: xx-small; text-align: left" Width="80px" Height="20px" AppendDataBoundItems="True" >
                                                    <asp:ListItem Value="0">Sin Cargo</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Incremento" >   
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtIncrementoF" runat="server" Text='<%# Eval("INCREMENTO") %>' style="font-size: x-small" Width="50" /> 
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbIncrementoF" runat="server" TargetControlID="txtIncrementoF" FilterType="Custom, Numbers" ValidChars="0123456789," />                                               
                                            </FooterTemplate>  
                                            <ItemTemplate>
                                                <asp:Label ID="lblIncremento" runat="server" Text='<%# String.Format("{0:N2}", Eval("INCREMENTO")) %>' style="font-size: x-small" Width="50" />
                                            </ItemTemplate>                                               
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtIncremento" runat="server" Text='<%# String.Format("{0:N6}", Eval("INCREMENTO")) %>' style="font-size: x-small" Width="50" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbIncremento" runat="server" TargetControlID="txtIncremento" FilterType="Custom, Numbers" ValidChars="0123456789," />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="40px" />                            
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salario Presup." >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblsalario_nuevo" runat="server" Text='<%# String.Format("{0:N}", Eval("salario_nuevo")) %>'  style="font-size: xx-small" Width="70" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>  
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aux.Trans." >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblaux_trans" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_trans")) %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtaux_trans" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_trans")) %>'  style="font-size: x-small; text-align: right" Width="65" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%Cumplimiento" >
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtCumplimientoF" runat="server" Text='<%# String.Format("{0:N}", Eval("CUMPLIMIENTO")) %>' style="font-size: x-small" Width="65" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCumplimientoF" runat="server" TargetControlID="txtCumplimientoF"         
                                                    FilterType="Custom, Numbers" ValidChars="0123456789,." />
                                            </FooterTemplate> 
                                            <ItemTemplate>
                                                <asp:Label ID="lblCumplimiento" runat="server" Text='<%# Eval("CUMPLIMIENTO") %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate>                                       
                                            <EditItemTemplate>                                    
                                                <asp:TextBox ID="txtCumplimiento" runat="server" Text='<%# String.Format("{0:N2}", Eval("CUMPLIMIENTO")) %>' style="font-size: x-small" Width="50" />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCumplimiento" runat="server" TargetControlID="txtCumplimiento"         
                                                    FilterType="Custom, Numbers" ValidChars="0123456789,." />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="40px"/>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comisiones" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblComision" runat="server" Text='<%# Eval("COMISIONES") %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate>                                       
                                            <EditItemTemplate></EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="40px"/>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aux.Telef.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblaux_tel" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_tel")) %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate> 
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtaux_tel" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_tel")) %>'  style="font-size: x-small; text-align: right" Width="65" />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aux.Gasol." >
                                            <ItemTemplate>
                                                <asp:Label ID="lblaux_gas" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_gas")) %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate> 
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtaux_gas" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_gas")) %>'  style="font-size: x-small; text-align: right" Width="65" />
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cesantias" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblcesantias" runat="server" Text='<%# String.Format("{0:N}", Eval("cesantias")) %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate> 
                                            <EditItemTemplate></EditItemTemplate><ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Int.Cesantias" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblint_ces" runat="server" Text='<%# String.Format("{0:N}", Eval("int_ces")) %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate> 
                                            <EditItemTemplate></EditItemTemplate><ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prima" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblprima" runat="server" Text='<%# String.Format("{0:N}", Eval("prima")) %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate> 
                                            <EditItemTemplate></EditItemTemplate><ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vacaciones"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblvacaciones" runat="server" Text='<%# String.Format("{0:N}", Eval("vacaciones")) %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate> 
                                            <EditItemTemplate></EditItemTemplate><ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dotación" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbldotacion" runat="server" Text='<%# String.Format("{0:N}", Eval("dotacion")) %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate> 
                                            <EditItemTemplate></EditItemTemplate><ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apo.Salud" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblsalud" runat="server" Text='<%# String.Format("{0:N}", Eval("salud")) %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate> 
                                            <EditItemTemplate></EditItemTemplate><ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apo.Pension" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblpension" runat="server" Text='<%# String.Format("{0:N}", Eval("pension")) %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate> 
                                            <EditItemTemplate></EditItemTemplate><ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Riesgos Prof." >
                                            <ItemTemplate>
                                                <asp:Label ID="lblarp" runat="server" Text='<%# String.Format("{0:N}", Eval("arp")) %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate> 
                                            <EditItemTemplate></EditItemTemplate><ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Caja Comp." >
                                            <ItemTemplate>
                                                <asp:Label ID="lblcaja_comp" runat="server" Text='<%# String.Format("{0:N}", Eval("caja_comp")) %>'  style="font-size: x-small" Width="65" />
                                            </ItemTemplate> 
                                            <EditItemTemplate></EditItemTemplate><ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Mes" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotal" runat="server" Text='<%# String.Format("{0:N}", Eval("total")) %>'  style="font-size: xx-small" Width="75" />
                                            </ItemTemplate> 
                                            <EditItemTemplate></EditItemTemplate><ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                    <FooterStyle CssClass="gridHeader" />
                                    <SelectedRowStyle Font-Size="XX-Small" />
                                </asp:GridView>
                            </div>
                            <br />
                            <strong>TOTALES</strong>
                            <div style="overflow:scroll;width:auto"> 
                                <asp:GridView ID="gvTotalesNomina" runat="server" ShowHeaderWhenEmpty="True" 
                                    AutoGenerateColumns="False" Style="font-size: xx-small" ShowFooter="True" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Cod.Ofi" >
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodOficina" runat="server" Text='<%# Eval("COD_OFICINA") %>' style="font-size: x-small"  Width="50"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre de Oficina" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblOficina" runat="server" Text='<%# Eval("OFICINA") %>' style="font-size: x-small; text-transform :uppercase"  Width="160" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="160px" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salario Act." >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSalario" runat="server" Text='<%# String.Format("{0:N}", Eval("SALARIO")) %>' style="font-size: x-small"  Width="85" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Incremento" >   
                                            <ItemTemplate>
                                                <asp:Label ID="lblIncremento" runat="server" Text='<%# String.Format("{0:N2}", Eval("INCREMENTO")) %>' style="font-size: x-small" Width="50" />
                                            </ItemTemplate>                                               
                                            <ItemStyle HorizontalAlign="Right" Width="40px" />                            
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salario Presup." >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblsalario_nuevo" runat="server" Text='<%# String.Format("{0:N}", Eval("salario_nuevo")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aux.Trans." >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblaux_trans" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_trans")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%Cumplimiento" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblCumplimiento" runat="server" Text='<%# Eval("CUMPLIMIENTO") %>'  style="font-size: x-small" Width="50" />
                                            </ItemTemplate>                                       
                                            <ItemStyle HorizontalAlign="Right" Width="50px"/>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comisiones" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblComision" runat="server" Text='<%# Eval("COMISIONES") %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate>                                       
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aux.Telef.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblaux_tel" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_tel")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Aux.Gasol." >
                                            <ItemTemplate>
                                                <asp:Label ID="lblaux_gas" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_gas")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cesantias" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblcesantias" runat="server" Text='<%# String.Format("{0:N}", Eval("cesantias")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Int.Cesantias" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblint_ces" runat="server" Text='<%# String.Format("{0:N}", Eval("int_ces")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prima" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblprima" runat="server" Text='<%# String.Format("{0:N}", Eval("prima")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vacaciones"  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblvacaciones" runat="server" Text='<%# String.Format("{0:N}", Eval("vacaciones")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dotación" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbldotacion" runat="server" Text='<%# String.Format("{0:N}", Eval("dotacion")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apo.Salud" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblsalud" runat="server" Text='<%# String.Format("{0:N}", Eval("salud")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apo.Pension" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblpension" runat="server" Text='<%# String.Format("{0:N}", Eval("pension")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Riesgos Prof." >
                                            <ItemTemplate>
                                                <asp:Label ID="lblarp" runat="server" Text='<%# String.Format("{0:N}", Eval("arp")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Caja Comp." >
                                            <ItemTemplate>
                                                <asp:Label ID="lblcaja_comp" runat="server" Text='<%# String.Format("{0:N}", Eval("caja_comp")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Mes" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotal" runat="server" Text='<%# String.Format("{0:N}", Eval("total")) %>'  style="font-size: x-small" Width="85" />
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridHeader" />
                                    <PagerStyle CssClass="gridPager" />
                                    <RowStyle CssClass="gridItem" />
                                    <FooterStyle CssClass="gridHeader" />
                                    <SelectedRowStyle Font-Size="XX-Small" />
                                </asp:GridView>
                            </div>       
                            </ContentTemplate>  
                        </asp:UpdatePanel>                        
                    </ContentTemplate>                                                    
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpParametros" runat="server" HeaderText="Parámetros">
                    <HeaderTemplate>                                                                       
                    Parámetros                                      
                    </HeaderTemplate>                                        
                    <ContentTemplate>                             
                        <br /><strong>CARGOS</strong><br />
                        <asp:Button ID="btnExpCargos" runat="server" CssClass="btn8" 
                            onclick="btnExpCargos_Click" onclientclick="btnExpCargos_Click" 
                            Text="Exportar a excel" />
                        <div id="divCargos" style="overflow:scroll; width:100%">   
                            <asp:GridView ID="gvCargos" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Style="font-size: xx-small" DataKeyNames="cod_cargo" Font-Size="XX-Small" 
                                onrowediting="gvCargos_RowEditing" OnRowCancelingEdit="gvCargos_RowCancelingEdit" onrowupdating="gvCargos_RowUpdating" 
                                onrowdatabound="gvCargos_RowDataBound" ShowFooter="True" onrowcommand="gvCargos_RowCommand" Width="90%">
                                <AlternatingRowStyle Width="20px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle Width="30px" />
                                        <FooterStyle Width="30px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                            <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew" ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" ValidationGroup="vgCargoNuevo" />                                
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="gridIco" />
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Cód">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("cod_cargo") %>' Width="50"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCodigoF" runat="server" Text='<%# Bind("cod_cargo") %>' style="font-size: x-small" Width="50" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCodigo" runat="server" TargetControlID="txtCodigoF"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                            <asp:RequiredFieldValidator ID="rfvCodigoF" runat="server" ControlToValidate="txtCodigoF"
                                                Display="Dynamic"  ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                                style="font-size: xx-small" ValidationGroup="vgCargoNuevo" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripcion">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("nom_cargo") %>' Width="120"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDescripcionF" runat="server" Text='<%# Bind("nom_cargo") %>' style="font-size: x-small; text-transform: uppercase" Width="120" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Comisión Coloc. Anterior">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomision_colocacion_ant" runat="server" Text='<%# String.Format("{0:N}", Eval("comision_colocacion_ant")) %>'  style="font-size: xx-small" Width="65" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtcomision_colocacion_ant" runat="server" 
                                                Text='<%# String.Format("{0:N0}", Eval("comision_colocacion_ant")) %>' onclick = "click7(this)" 
                                                style="text-align:right" Width="60" Font-Size="XX-Small" ></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbecomision_colocacion_ant" runat="server" TargetControlID="txtcomision_colocacion_ant"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtcomision_colocacion_antF" runat="server" Text='<%# String.Format("{0:N}", Eval("comision_colocacion_ant")) %>' style="font-size: x-small" Width="60" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbecomision_colocacion_ant" runat="server" TargetControlID="txtcomision_colocacion_antF"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="% Incremento Com.Colocación">
                                        <ItemTemplate>
                                            <asp:Label ID="lblincremento_colocacion" runat="server" Text='<%# String.Format("{0:N2}", Eval("incremento_colocacion")) %>'  style="font-size: xx-small" Width="65" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtincremento_colocacion" runat="server" 
                                                Text='<%# String.Format("{0:N0}", Eval("incremento_colocacion")) %>' onclick = "click7(this)" 
                                                style="text-align:right" Width="60" Font-Size="XX-Small" ></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeincremento_colocacion" runat="server" TargetControlID="txtincremento_colocacion"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtincremento_colocacionF" runat="server" Text='<%# String.Format("{0:N}", Eval("incremento_colocacion")) %>' style="font-size: x-small" Width="60" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeincremento_colocacion" runat="server" TargetControlID="txtincremento_colocacionF"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Comisión Colocación">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomision_colocacion" runat="server" Text='<%# String.Format("{0:N}", Eval("comision_colocacion")) %>'  style="font-size: xx-small" Width="65" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comisión Cartera Anterior">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomision_cartera_ant" runat="server" Text='<%# String.Format("{0:N}", Eval("comision_cartera_ant")) %>'  style="font-size: xx-small" Width="65" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtcomision_cartera_ant" runat="server" 
                                                Text='<%# String.Format("{0:N0}", Eval("comision_cartera_ant")) %>' onclick = "click7(this)" 
                                                style="text-align:right" Width="60" Font-Size="XX-Small" ></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbecomision_cartera_ant" runat="server" TargetControlID="txtcomision_cartera_ant"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtcomision_cartera_antF" runat="server" Text='<%# String.Format("{0:N}", Eval("comision_cartera_ant")) %>' style="font-size: x-small" Width="60" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbecomision_cartera_antF" runat="server" TargetControlID="txtcomision_cartera_antF"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="% Incremento Com.Cartera">
                                        <ItemTemplate>
                                            <asp:Label ID="lblincremento_cartera" runat="server" Text='<%# String.Format("{0:N2}", Eval("incremento_cartera")) %>'  style="font-size: xx-small" Width="65" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtincremento_cartera" runat="server" 
                                                Text='<%# String.Format("{0:N0}", Eval("incremento_cartera")) %>' onclick = "click7(this)" 
                                                style="text-align:right" Width="60" Font-Size="XX-Small" ></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeincremento_cartera" runat="server" TargetControlID="txtincremento_cartera"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtincremento_carteraF" runat="server" Text='<%# String.Format("{0:N}", Eval("incremento_cartera")) %>' style="font-size: x-small" Width="60" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeincremento_carteraF" runat="server" TargetControlID="txtincremento_carteraF"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comisión Cartera">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomision_cartera" runat="server" Text='<%# String.Format("{0:N}", Eval("comision_cartera")) %>'  style="font-size: xx-small" Width="65" />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:TemplateField>                                     
                                    <asp:TemplateField HeaderText="Aux. Gasol. Anterior">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaux_gas_ant" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_gas_ant")) %>'  style="font-size: xx-small" Width="65" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtaux_gas_ant" runat="server" 
                                                Text='<%# String.Format("{0:N0}", Eval("aux_gas_ant")) %>' onclick = "click7(this)" 
                                                style="text-align:right" Width="60" Font-Size="XX-Small" ></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeaux_gas_ant" runat="server" TargetControlID="txtaux_gas_ant"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtaux_gas_antF" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_gas_ant")) %>' style="font-size: x-small" Width="60" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeaux_gas_antF" runat="server" TargetControlID="txtaux_gas_antF"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="% Incremento Aux.Gas.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblincremento_aux_gas" runat="server" Text='<%# String.Format("{0:N2}", Eval("incremento_aux_gas")) %>'  style="font-size: xx-small" Width="65" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtincremento_aux_gas" runat="server" 
                                                Text='<%# String.Format("{0:N0}", Eval("incremento_aux_gas")) %>' onclick = "click7(this)" 
                                                style="text-align:right" Width="60" Font-Size="XX-Small" ></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeincremento_aux_gas" runat="server" TargetControlID="txtincremento_aux_gas"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtincremento_aux_gasF" runat="server" Text='<%# String.Format("{0:N}", Eval("incremento_aux_gas")) %>' style="font-size: x-small" Width="60" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeincremento_aux_gasF" runat="server" TargetControlID="txtincremento_aux_gasF"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Aux. Gasol.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaux_gas" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_gas")) %>'  style="font-size: xx-small" Width="65" />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Aux.Tel./Celular Anterior">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaux_tel_ant" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_tel_ant")) %>'  style="font-size: xx-small" Width="65" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtaux_tel_ant" runat="server" 
                                                Text='<%# String.Format("{0:N0}", Eval("aux_tel_ant")) %>' onclick = "click7(this)" 
                                                style="text-align:right" Width="60" Font-Size="XX-Small" ></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeaux_tel_ant" runat="server" TargetControlID="txtaux_tel_ant"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtaux_tel_antF" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_tel_ant")) %>' style="font-size: x-small" Width="60" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeaux_tel_antF" runat="server" TargetControlID="txtaux_tel_antF"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="% Incremento Aux.Tel.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblincremento_aux_tel" runat="server" Text='<%# String.Format("{0:N2}", Eval("incremento_aux_tel")) %>'  style="font-size: xx-small" Width="65" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtincremento_aux_tel" runat="server" 
                                                Text='<%# String.Format("{0:N0}", Eval("incremento_aux_tel")) %>' onclick = "click7(this)" 
                                                style="text-align:right" Width="60" Font-Size="XX-Small" ></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeincremento_aux_tel" runat="server" TargetControlID="txtincremento_aux_tel"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtincremento_aux_telF" runat="server" Text='<%# String.Format("{0:N}", Eval("incremento_aux_tel")) %>' style="font-size: x-small" Width="60" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbeincremento_aux_telF" runat="server" TargetControlID="txtincremento_aux_telF"         
                                                FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Aux.Tel./Celular">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaux_tel" runat="server" Text='<%# String.Format("{0:N}", Eval("aux_tel")) %>'  style="font-size: xx-small" Width="65" />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                                    </asp:TemplateField>     
                                </Columns>
                                <FooterStyle BackColor="#FFFFCC" />
                                <HeaderStyle CssClass="gridHeader" />
                                <PagerStyle CssClass="gridPager" />
                                <RowStyle CssClass="gridItem" />
                                <SelectedRowStyle Font-Size="XX-Small" />
                                <EditRowStyle Font-Size="XX-Small" Width="160px" Font-Italic="True" 
                                    BackColor="#FFFF99" Height="20px" />
                            </asp:GridView>
                        </div>   
                    </ContentTemplate>                                                    
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer> 
                        
            <asp:Panel ID="panelIncremento" runat="server" BackColor="#ccccff" Style="text-align: left; width:470px">
                <div id="popupcontainerinc" style="width: 470px">
                <div class="row popupcontainertitleinc">
                <div class="cell popupcontainercellinc" style="text-align: center; width: 470px;">
                    INCREMENTO GENERAL
                    <asp:UpdatePanel ID="upIncremento" runat="server">                    
                    <ContentTemplate>
                    <table border="1" style="width: 470px">
                        <tr>
                            <td style="width: 176px; text-align:center; font-weight: 700; font-size: x-small;">
                                % Incremento&nbsp;&nbsp;<asp:TextBox ID="txtIncrementoGen" runat="server" Width="60px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeIncrementoGen" runat="server" TargetControlID="txtIncrementoGen"         
                                    FilterType="Custom, Numbers" ValidChars="0123456789,." />
                                <asp:RequiredFieldValidator ID="rfvIncrementoGen" runat="server" ControlToValidate="txtIncrementoGen"
                                    Display="Dynamic"  ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="vgIncremento" style="font-size: xx-small" />
                            </td>
                        </tr>
                        <tr>
                            <asp:Button ID="btnIncrementoAceptar" runat="server" Text="Generar" CssClass="button" CausesValidation="false" OnClick="btnIncrementoAceptar_Click" style="height: 26px" ValidationGroup="vgIncremento" />
                            <asp:Button ID="btnIncrementoCancelar" runat="server" CausesValidation="false" CssClass="button" OnClick="btnIncrementoCancelar_Click" Text="Cerrar" style="height: 26px" />
                        </tr>
                    </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>                   
                </div>
                </div>
                </div>
            </asp:Panel>                  
            <asp:HiddenField ID="hfIncremento" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeIncremento" runat="server" PopupControlID="panelIncremento"
                TargetControlID="hfIncremento" BackgroundCssClass="backgroundColor">                            
            </ajaxToolkit:ModalPopupExtender>      
            
            <asp:Panel ID="panelCumplimiento" runat="server" BackColor="#ccccff" Style="text-align: left; width:470px">
                <div id="popupcontainercum" style="width: 470px">
                <div class="row popupcontainertitlecum">
                <div class="cell popupcontainercellcum" style="text-align: center; width: 470px;">
                    % CUMPLIMIENTO GENERAL
                    <asp:UpdatePanel ID="uCumplimiento" runat="server">                    
                    <ContentTemplate>
                    <table border="1" style="width: 470px">
                        <tr>
                            <td style="width: 176px; text-align:center; font-weight: 700; font-size: x-small;">
                                % Cumplimiento&nbsp;&nbsp;<asp:TextBox ID="txtCumplimientoGen" runat="server" Width="60px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCumplimiento" runat="server" TargetControlID="txtCumplimientoGen"         
                                    FilterType="Custom, Numbers" ValidChars="0123456789,." />
                                <asp:RequiredFieldValidator ID="rfvtxtCumplimientoGen" runat="server" ControlToValidate="txtCumplimientoGen"
                                    Display="Dynamic"  ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="vgCumplimiento" style="font-size: xx-small" />
                            </td>
                        </tr>
                        <tr>
                            <asp:Button ID="btnCumplimientoAceptar" runat="server" Text="Generar" CssClass="button" CausesValidation="false" OnClick="btnCumplimientoAceptar_Click" style="height: 26px" ValidationGroup="vgCumplimiento" />
                            <asp:Button ID="btnCumplimientoCancelar" runat="server" CausesValidation="false" CssClass="button" OnClick="btnCumplimientoCancelar_Click" Text="Cerrar" style="height: 26px" />
                        </tr>
                    </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>                   
                </div>
                </div>
                </div>
            </asp:Panel>                  
            <asp:HiddenField ID="hfCumplimiento" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeCumplimiento" runat="server" PopupControlID="panelCumplimiento"
                TargetControlID="hfCumplimiento" BackgroundCssClass="backgroundColor">                            
            </ajaxToolkit:ModalPopupExtender>                                                                           

            <div class="regresar" style="text-align:center;">
                <asp:ImageButton ID="btnRegresarNOM" runat="server" 
                    ImageUrl="~/Images/btnCargarPresupuesto.jpg" onclick="btnRegresarNOM_Click" />
            </div>
        </asp:View>

        <asp:View ID="mvActivosFij" runat="server">       
            <br /><br />
            <div class="divExcelAct" style="text-align:left;">
            <asp:Button ID="btnExpActivos" runat="server" CssClass="btn8" 
                onclick="btnExpActivos_Click" onclientclick="btnExpActivos_Click" 
                Text="Exportar a excel" />  
            </div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div style="overflow:scroll;width:auto;">    
                    <asp:GridView ID="gvActivosFij" runat="server" ShowHeaderWhenEmpty="True" 
                        AutoGenerateColumns="False" Style="font-size: x-small" DataKeyNames="codigo" ShowFooter="True" 
                        onrowediting="gvActivosFij_RowEditing" OnRowCancelingEdit="gvActivosFij_RowCancelingEdit" onrowupdating="gvActivosFij_RowUpdating" 
                        onrowdatabound="gvActivosFij_RowDataBound" onrowdeleting="gvActivosFij_RowDeleting" onrowcommand="gvActivosFij_RowCommand" >
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                    <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                    ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" ValidationGroup="vgGuardarActFij" />                                
                                </FooterTemplate>
                                <HeaderStyle CssClass="gridIco" />
                            </asp:TemplateField> 
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                        ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                </ItemTemplate>                            
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Código" >
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCodigoF" runat="server" Text='<%# Bind("CODIGO") %>' style="font-size: x-small" Width="50" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCodigo" runat="server" TargetControlID="txtCodigoF"         
                                        FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    <asp:RequiredFieldValidator ID="rfvCodigoF" runat="server" ControlToValidate="txtCodigoF"
                                        Display="Dynamic"  ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="vgGuardarActFij" style="font-size: xx-small" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="50"  />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="50" Enabled="False" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripciòn" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDescripcionF" runat="server" Text='<%# Bind("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase" MaxLength="300" Width="300" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase"  Width="300" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase"  Width="300" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="140px" />
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cod.Ofi" >
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlCodOficinaF" runat="server" DataSource="<%# ListaOficinas() %>" DataTextField="nombre" DataValueField="cod_oficina" 
                                    SelectedValue='<%# Bind("COD_OFICINA") %>' style="font-size: x-small; text-align: left" Width="40" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCodOficina" runat="server" Text='<%# String.Format("{0:N0}", Eval("COD_OFICINA")) %>'  style="font-size: x-small; text-align: center" Width="40" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlCodOficina" runat="server" DataSource="<%# ListaOficinas() %>" DataTextField="nombre" DataValueField="cod_oficina" 
                                        SelectedValue='<%# Bind("COD_OFICINA") %>' style="font-size: x-small; text-align: left"  Width="40" AppendDataBoundItems="True" >
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vr.Compra" >
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:TextBox ID="txtVrcompraF" runat="server" Text='<%# Bind("VRCOMPRA") %>' style="font-size: x-small" Width="65" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeVrcompraF" runat="server" TargetControlID="txtVrcompraF"         
                                        FilterType="Custom, Numbers" ValidChars="0123456789" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblVrcompra" runat="server" Text='<%# String.Format("{0:N}", Eval("VRCOMPRA")) %>' style="font-size: x-small"  Width="65" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVrcompra" runat="server" Text='<%# Eval("VRCOMPRA") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeVrcompra" runat="server" TargetControlID="txtVrcompra"         
                                        FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                </EditItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Fecha de Compra" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFechaCompraF" runat="server" Text='<%# Bind("FECHA_COMPRA") %>' style="font-size: x-small" Width="60" />
                                    <ajaxToolkit:CalendarExtender ID="txtFechaCompraF_CalendarExtender" runat="server" 
                                        Format="dd/MM/yyyy" TargetControlID="txtFechaCompraF">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:MaskedEditExtender ID="txtFechaCompraF_MaskedEditExtender" 
                                        runat="server"  TargetControlID="txtFechaCompraF" Mask="99/99/9999" 
                                        clearmaskonlostfocus="false" MaskType="None" UserDateFormat="MonthDayYear">
                                    </ajaxToolkit:MaskedEditExtender>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFechaCompra" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("FECHA_COMPRA")) %>'  style="font-size: x-small" Width="60" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFechaCompra" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("FECHA_COMPRA")) %>'  style="font-size: x-small" Width="60" />
                                    <ajaxToolkit:CalendarExtender ID="txtFechaCompra_CalendarExtender" runat="server" 
                                        Format="dd/MM/yyyy" TargetControlID="txtFechaCompra">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:MaskedEditExtender ID="txtFechaCompra_MaskedEditExtender" 
                                        runat="server"  TargetControlID="txtFechaCompra" Mask="99/99/9999" 
                                        clearmaskonlostfocus="false" MaskType="None" UserDateFormat="DayMonthYear">
                                    </ajaxToolkit:MaskedEditExtender>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo Activo" >
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlTipoActivoF" runat="server" style="font-size: x-small; text-align: left" Width="80" >
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="1">EDIFICACIONES</asp:ListItem>
                                        <asp:ListItem Value="2">MUEBLES Y EQUIPOS DE OFICINA</asp:ListItem>
                                        <asp:ListItem Value="3">EQUIPO DE COMPUTO Y COMUNICACION</asp:ListItem>
                                        <asp:ListItem Value="4">VEHICULOS</asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoActivo" runat="server" Text='<%# Eval("NOM_TIPO_ACTIVO") %>'  style="font-size: x-small; text-align: center" Width="80" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTipoActivo" runat="server" SelectedValue='<%# Bind("TIPO_ACTIVO") %>' 
                                        style="font-size: x-small; text-align: left"  Width="80" AppendDataBoundItems="True" >
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="1">EDIFICACIONES</asp:ListItem>
                                        <asp:ListItem Value="2">MUEBLES Y EQUIPOS DE OFICINA</asp:ListItem>
                                        <asp:ListItem Value="3">EQUIPO DE COMPUTO Y COMUNICACION</asp:ListItem>
                                        <asp:ListItem Value="4">VEHICULOS</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                        <FooterStyle CssClass="gridHeader" />
                        <SelectedRowStyle Font-Size="XX-Small" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            <div class="regresar" style="text-align:center;">
                <asp:ImageButton ID="btnRegresarACF" runat="server" 
                    ImageUrl="~/Images/btnCargarPresupuesto.jpg" onclick="btnRegresarACF_Click" />
            </div>
        </asp:View>

        <asp:View ID="mvObligaciones" runat="server">  
            <br /><br />
            <ajaxToolkit:TabContainer runat="server" ID="tcObligaciones" ActiveTabIndex="0" 
                Width="100%" style="margin-right: 30px; text-align: left" >
                <ajaxToolkit:TabPanel ID="tpOblSaldos" runat="server" HeaderText="Obligaciones-Saldos">
                    <HeaderTemplate>
                    Obligaciones-Saldos
                    </HeaderTemplate>
                    <ContentTemplate>
                    <strong style="text-align: center;color: #FFFFFF; background-color: #0066FF">OBLIGACIONES FINANCIERAS - SALDOS </strong>                         
                <div style="overflow:scroll;width:auto; text-align: left;"> 
                    <asp:Button ID="btnExpObligaciones" runat="server" CssClass="btn8" 
                        onclick="btnExpObligaciones_Click" onclientclick="btnExpObligaciones_Click" 
                        Text="Exportar a excel" />                                                         
                    <asp:GridView ID="gvObligaciones" runat="server"  ShowFooter="True"
                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Style="font-size: xx-small" DataKeyNames="codigo"                             
                        EditRowStyle-Width="20" EditRowStyle-Height="20" EditRowStyle-BackColor="#FFFF99" Font-Size="XX-Small">
                        <AlternatingRowStyle Width="20px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Código">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("codigo") %>' Width="40"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripcion">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("descripcion") %>' Width="90"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Saldo">
                                <ItemTemplate>
                                    <asp:Label ID="lblSaldo" runat="server" Text='<%# String.Format("{0:N0}", Eval("saldo")) %>' Width="70" ></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                            </asp:TemplateField> 
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle Font-Size="XX-Small" />
                        <EditRowStyle Font-Size="XX-Small" Width="100px" Font-Italic="True" />
                        <FooterStyle BackColor="#FFFFCC" />
                    </asp:GridView> 	                        
                </div>
                </ContentTemplate>
                </ajaxToolkit:TabPanel>
                
                <ajaxToolkit:TabPanel ID="tlOblPagos" runat="server" HeaderText="Obligaciones-Pagos">
                    <HeaderTemplate>
                    Obligaciones-Pagos
                    </HeaderTemplate>
                    <ContentTemplate>
                    <strong style="text-align: center;color: #FFFFFF; background-color: #0066FF">PAGO CAPITAL/INTERESES OBLIGACIONES FINANCIERAS</strong>
                    <div style="overflow:scroll;width:auto; text-align: left;">
                    <asp:Button ID="btnExpObligacionesPagos" runat="server" CssClass="btn8" 
                        onclick="btnExpObligacionesPagos_Click" onclientclick="btnExpObligacionesPagoss_Click" 
                        Text="Exportar a excel" />   
                    <asp:GridView ID="gvObligacionesPagos" runat="server" 
                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Style="font-size: xx-small" DataKeyNames="codigo"                             
                        EditRowStyle-Width="20" EditRowStyle-Height="20" EditRowStyle-BackColor="#FFFF99" Font-Size="XX-Small">
                        <AlternatingRowStyle Width="20px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Código">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Bind("codigo") %>' Width="40"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripcion">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("descripcion") %>' Width="90"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Componente">
                                <ItemTemplate>
                                    <asp:Label ID="lblComponente" runat="server" Text='<%# Bind("Componente") %>' Width="90"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle Font-Size="XX-Small" />
                        <EditRowStyle Font-Size="XX-Small" Width="100px" Font-Italic="True" />
                    </asp:GridView> 	                        
                    </div>
                </ContentTemplate>
                </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel ID="tblOblNuevas" runat="server" HeaderText="Obligaciones-Nuevas">
                    <HeaderTemplate>
                    Obligaciones-Nuevas
                    </HeaderTemplate>
                    <ContentTemplate>
                    <strong style="text-align: center;color: #FFFFFF; background-color: #0066FF">OBLIGACIONES FINANCIERAS NUEVAS</strong><br />
                    <div style="overflow:scroll;width:auto;"> 
                    <asp:Button ID="btnExpObligacionesNuevas" runat="server" CssClass="btn8" 
                        onclick="btnExpObligacionesNuevas_Click" onclientclick="btnExpObligacionesNuevas_Click" 
                        Text="Exportar a Excel Obligaciones Nuevas" />                       
                    <asp:UpdatePanel ID="upObligaciones" runat="server">
                    <ContentTemplate>
                        <div style="text-align: left">
                            <strong>FLUJO DE CAJA FINAL</strong>
                        </div>
                        <br />                     
                        <asp:GridView ID="gvRequerido" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="codigo" EditRowStyle-BackColor="#FFFF99" EditRowStyle-Width="20" 
                            Font-Size="XX-Small" ShowFooter="True" ShowHeaderWhenEmpty="True" 
                            Style="font-size: xx-small">
                            <Columns>
                                <asp:TemplateField HeaderText="Código">
                                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigo" runat="server" style="font-size: x-small" 
                                            Text='<%# Eval("CODIGO") %>' Width="50" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripciòn">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" 
                                            style="font-size: x-small; text-transform :uppercase" 
                                            Text='<%# Eval("DESCRIPCION") %>' Width="100" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle Font-Size="XX-Small" />
                            <FooterStyle CssClass="gridHeader" />
                        </asp:GridView>
                        <br />
                        <div style="text-align: left">
                            <strong>OBLIGACIONES NUEVAS</strong>
                        </div>   
                        <asp:GridView ID="gvObligacionesNuevas" runat="server" 
                            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                            Style="font-size: xx-small" DataKeyNames="codigo"                             
                            EditRowStyle-Width="20" EditRowStyle-BackColor="#FFFF99" 
                            Font-Size="XX-Small" ShowFooter="True"
                            onrowediting="gvObligacionesNuevas_RowEditing" 
                            OnRowCancelingEdit="gvObligacionesNuevas_RowCancelingEdit" onrowupdating="gvObligacionesNuevas_RowUpdating" 
                            onrowdatabound="gvObligacionesNuevas_RowDataBound" 
                            onrowdeleting="gvObligacionesNuevas_RowDeleting" 
                            onrowcommand="gvObligacionesNuevas_RowCommand"                              
                            CellPadding="1" CellSpacing="1" >
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                        <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew" ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" ValidationGroup="vgGuardarObl"  />                                
                                    </FooterTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                </asp:TemplateField> 
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                            ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código" >
                                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtCodigoF" runat="server" Text='<%# Bind("CODIGO") %>' style="font-size: x-small" Width="50" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCodigo" runat="server" TargetControlID="txtCodigoF"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        <asp:RequiredFieldValidator ID="rfvCodigoF" runat="server" ControlToValidate="txtCodigoF"
                                            Display="Dynamic"  ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="vgGuardarObl" style="font-size: xx-small" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="50"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="50" Enabled="False" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripciòn" >
                                    <FooterTemplate>
                                        <%--<asp:TextBox ID="txtDescripcionF" runat="server" Text='<%# Bind("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase" MaxLength="200" Width="100" />--%>
                                        <asp:DropDownList ID="ddlDescripcionF" runat="server" DataSource="<%# ListaBanco() %>" DataTextField="nombrebanco" 
                                            DataValueField="nombrebanco" SelectedValue='<%# Bind("DESCRIPCION") %>' style="font-size: x-small; text-align: left" Width="100" AppendDataBoundItems="True" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase"  Width="100" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase"  Width="100" />
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cupo" >
                                    <ItemStyle HorizontalAlign="Right" Width="95px" />
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtCupoF" runat="server" Text='<%# Bind("CUPO") %>' style="font-size: x-small" Width="90" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCupoF" runat="server" TargetControlID="txtCupoF"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCupo" runat="server" Text='<%# String.Format("{0:N}", Eval("CUPO")) %>' style="font-size: x-small"  Width="90" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCupo" runat="server" Text='<%# Eval("CUPO") %>' style="font-size: x-small; text-align:right;"  Width="90" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCupo" runat="server" TargetControlID="txtCupo"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tasa" >
                                    <ItemStyle HorizontalAlign="Right" Width="45px" />
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTasaF" runat="server" Text='<%# Bind("TASA") %>' style="font-size: x-small" Width="45" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeTasaF" runat="server" TargetControlID="txtTasaF"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTasa" runat="server" Text='<%# String.Format("{0:N2}", Eval("TASA")) %>' style="font-size: x-small"  Width="45" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTasa" runat="server" Text='<%# Eval("TASA") %>' style="font-size: x-small; text-align:right;"  Width="45" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeTasa" runat="server" TargetControlID="txtTasa"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Periodicidad" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlPeriodicidadF" runat="server" DataSource="<%# ListaPeriodicidad() %>" DataTextField="descripcion" 
                                            DataValueField="codigo" SelectedValue='<%# Bind("COD_PERIODICIDAD") %>' style="font-size: x-small; text-align: left" Width="40" AppendDataBoundItems="True" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriodicidad" runat="server" Text='<%# String.Format("{0:N0}", Eval("COD_PERIODICIDAD")) %>'  style="font-size: x-small; text-align: center" Width="40" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlPeriodicidad" runat="server" DataSource="<%# ListaPeriodicidad() %>" DataTextField="descripcion" 
                                            DataValueField="codigo" SelectedValue='<%# Bind("COD_PERIODICIDAD") %>' style="font-size: x-small; text-align: left"  Width="40" AppendDataBoundItems="True" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plazo" >
                                    <ItemStyle HorizontalAlign="Left" Width="45px" />
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtPlazoOblF" runat="server" Text='<%# Bind("PLAZO") %>' style="font-size: x-small" Width="45" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbePlazoOblF" runat="server" TargetControlID="txtPlazoOblF"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlazoObl" runat="server" Text='<%# String.Format("{0:N2}", Eval("PLAZO")) %>' style="font-size: x-small"  Width="45" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPlazoObl" runat="server" Text='<%# Eval("PLAZO") %>' style="font-size: x-small; text-align:right;"  Width="45" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbePlazoObl" runat="server" TargetControlID="txtPlazoObl"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gracia" >
                                    <ItemStyle HorizontalAlign="Left" Width="45px" />
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtGraciaF" runat="server" Text='<%# Bind("GRACIA") %>' style="font-size: x-small" Width="45" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeGraciaF" runat="server" TargetControlID="txtGraciaF"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGracia" runat="server" Text='<%# String.Format("{0:N2}", Eval("GRACIA")) %>' style="font-size: x-small"  Width="45" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGracia" runat="server" Text='<%# Eval("GRACIA") %>' style="font-size: x-small; text-align:right;"  Width="45" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeGracia" runat="server" TargetControlID="txtGracia"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle Font-Size="XX-Small" />
                            <FooterStyle CssClass="gridHeader" />
                        </asp:GridView>                    
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
                </ajaxToolkit:TabPanel>
            
                <ajaxToolkit:TabPanel ID="tbOblTotales" runat="server" HeaderText="Obligaciones-Totales">
                    <HeaderTemplate>
                    Obligaciones-Totales
                    </HeaderTemplate>
                    <ContentTemplate> 
                    <strong style="text-align: center;color: #FFFFFF; background-color: #0066FF">OBLIGACIONES FINANCIERAS - TOTAL SALDOS </strong>                         
                    <div style="overflow:scroll;width:auto; text-align: left;"> 
                    <asp:Button ID="btnExpObligacionesTotales" runat="server" CssClass="btn8" 
                        onclick="btnExpObligacionesTotales_Click" onclientclick="btnExpObligacionesTotales_Click" 
                        Text="Exportar a excel" />                                                         
                    <asp:GridView ID="gvObligacionesTot" runat="server"  ShowFooter="True"
                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Style="font-size: xx-small" 
                        EditRowStyle-Width="20" EditRowStyle-Height="20" EditRowStyle-BackColor="#FFFF99" Font-Size="XX-Small">
                        <AlternatingRowStyle Width="20px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Descripcion">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("descripcion") %>' Width="90"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Saldo">
                                <ItemTemplate>
                                    <asp:Label ID="lblSaldo" runat="server" Text='<%# String.Format("{0:N0}", Eval("saldo")) %>' Width="90" ></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                            </asp:TemplateField> 
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle Font-Size="XX-Small" />
                        <EditRowStyle Font-Size="XX-Small" Width="100px" Font-Italic="True" />
                        <FooterStyle BackColor="#FFFFCC" />
                    </asp:GridView>
                    </div>
                    <br />
                    <strong style="text-align: center;color: #FFFFFF; background-color: #0066FF">OBLIGACIONES FINANCIERAS - TOTAL PAGOS </strong>                         
                    <div style="overflow:scroll;width:auto; text-align: left;">                     
                    <asp:Button ID="btnExpObligacionesTotalesPagos" runat="server" CssClass="btn8" 
                        onclick="btnExpObligacionesTotalesPagos_Click" onclientclick="btnExpObligacionesTotalesPagos_Click" 
                        Text="Exportar a excel" />   
                    <asp:GridView ID="gvObligacionesTotPagos" runat="server"  ShowFooter="True"
                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Style="font-size: xx-small" 
                        EditRowStyle-Width="20" EditRowStyle-Height="20" EditRowStyle-BackColor="#FFFF99" Font-Size="XX-Small">
                        <AlternatingRowStyle Width="20px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Descripcion">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("descripcion") %>' Width="90"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Componente">
                                <ItemTemplate>
                                    <asp:Label ID="lblComponente" runat="server" Text='<%# Bind("Componente") %>' Width="120"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                        <SelectedRowStyle Font-Size="XX-Small" />
                        <EditRowStyle Font-Size="XX-Small" Width="100px" Font-Italic="True" />
                        <FooterStyle BackColor="#FFFFCC" />
                    </asp:GridView> 	        
                    </div>
                </ContentTemplate>
                </ajaxToolkit:TabPanel>            
           </ajaxToolkit:TabContainer> 
            <%--<div class="regresar" style="text-align:center;">--%>
                <asp:ImageButton ID="btnRegresarOBL" runat="server" 
                    ImageUrl="~/Images/btnCargarPresupuesto.jpg" onclick="btnRegresarOBL_Click" />
            <%--</div>--%>
        </asp:View>

        <asp:View ID="mvDiferidos" runat="server">       
           <br /><br />
           <div class="divExcelDif" style="text-align:left;">
            <asp:Button ID="btnExpDiferidos" runat="server" CssClass="btn8" 
                onclick="btnExpDiferidos_Click" onclientclick="btnExpDiferidos_Click" 
                Text="Exportar a excel" />    
            </div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div style="overflow:scroll;width:auto;">    
                    <asp:GridView ID="gvDiferidos" runat="server" ShowHeaderWhenEmpty="True" 
                        AutoGenerateColumns="False" Style="font-size: x-small" DataKeyNames="codigo" ShowFooter="True" 
                        onrowediting="gvDiferidos_RowEditing" OnRowCancelingEdit="gvDiferidos_RowCancelingEdit" onrowupdating="gvDiferidos_RowUpdating" 
                        onrowdatabound="gvDiferidos_RowDataBound" onrowdeleting="gvDiferidos_RowDeleting" onrowcommand="gvDiferidos_RowCommand" >
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                    <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew" ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />                                
                                </FooterTemplate>
                                <HeaderStyle CssClass="gridIco" />
                            </asp:TemplateField> 
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                        ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                </ItemTemplate>                            
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Código" >
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCodigoF" runat="server" Text='<%# Bind("CODIGO") %>' style="font-size: x-small" Width="50" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCodigo" runat="server" TargetControlID="txtCodigoF"         
                                        FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    <asp:RequiredFieldValidator ID="rfvCodigoF" runat="server" ControlToValidate="txtCodigoF"
                                        Display="Dynamic"  ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="vgGuardar" style="font-size: xx-small" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="50"  />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="50" Enabled="False" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripciòn" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDescripcionF" runat="server" Text='<%# Bind("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase" MaxLength="300" Width="300" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase"  Width="300" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase"  Width="300" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="140px" />
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cod.Ofi" >
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlCodOficinaF" runat="server" DataSource="<%# ListaOficinas() %>" DataTextField="nombre" 
                                        DataValueField="cod_oficina" SelectedValue='<%# Bind("COD_OFICINA") %>' style="font-size: x-small; text-align: left" Width="40" AppendDataBoundItems="True" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCodOficina" runat="server" Text='<%# String.Format("{0:N0}", Eval("COD_OFICINA")) %>'  style="font-size: x-small; text-align: center" Width="40" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlCodOficina" runat="server" DataSource="<%# ListaOficinas() %>" DataTextField="nombre" 
                                        DataValueField="cod_oficina" SelectedValue='<%# Bind("COD_OFICINA") %>' style="font-size: x-small; text-align: left"  Width="40" AppendDataBoundItems="True" >
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="0">Sin Oficina</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor" >
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:TextBox ID="txtValorF" runat="server" Text='<%# Bind("VALOR") %>' style="font-size: x-small" Width="65" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeValorF" runat="server" TargetControlID="txtValorF"         
                                        FilterType="Custom, Numbers" ValidChars="0123456789" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblValor" runat="server" Text='<%# String.Format("{0:N}", Eval("VALOR")) %>' style="font-size: x-small"  Width="65" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtValor" runat="server" Text='<%# Eval("VALOR") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeValor" runat="server" TargetControlID="txtValor"         
                                        FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Plazo" >
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPlazoF" runat="server" Text='<%# Bind("PLAZO") %>' style="font-size: x-small" Width="65" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbePlazoF" runat="server" TargetControlID="txtPlazoF"         
                                        FilterType="Custom, Numbers" ValidChars="0123456789" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPlazo" runat="server" Text='<%# String.Format("{0:N}", Eval("PLAZO")) %>' style="font-size: x-small"  Width="65" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPlazo" runat="server" Text='<%# Eval("PLAZO") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbePlazo" runat="server" TargetControlID="txtPlazo"         
                                        FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Diferido" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFechaDiferidoF" runat="server" Text='<%# Bind("FECHA_DIFERIDO") %>' style="font-size: x-small" Width="60" />
                                    <ajaxToolkit:CalendarExtender ID="txtFechaDiferidoF_CalendarExtender" runat="server" 
                                        Format="dd/MM/yyyy" TargetControlID="txtFechaDiferidoF">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:MaskedEditExtender ID="txtFechaDiferidoF_MaskedEditExtender" 
                                        runat="server"  TargetControlID="txtFechaDiferidoF" Mask="99/99/9999" 
                                        clearmaskonlostfocus="false" MaskType="None" UserDateFormat="MonthDayYear">
                                    </ajaxToolkit:MaskedEditExtender>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFechaDiferido" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("FECHA_DIFERIDO")) %>'  style="font-size: x-small" Width="60" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFechaDiferido" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("FECHA_DIFERIDO")) %>'  style="font-size: x-small" Width="60" />
                                    <ajaxToolkit:CalendarExtender ID="txtFechaDiferido_CalendarExtender" runat="server" 
                                        Format="dd/MM/yyyy" TargetControlID="txtFechaDiferido">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:MaskedEditExtender ID="txtFechaDiferido_MaskedEditExtender" 
                                        runat="server"  TargetControlID="txtFechaDiferido" Mask="99/99/9999" 
                                        clearmaskonlostfocus="false" MaskType="None" UserDateFormat="DayMonthYear">
                                    </ajaxToolkit:MaskedEditExtender>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                        <FooterStyle CssClass="gridHeader" />
                        <SelectedRowStyle Font-Size="XX-Small" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            <div class="regresar" style="text-align:center;">
                <asp:ImageButton ID="btnRegresarDIF" runat="server" 
                    ImageUrl="~/Images/btnCargarPresupuesto.jpg" onclick="btnRegresarDIF_Click" />
            </div>
        </asp:View>

        <asp:View ID="mvOtros" runat="server">       
           <br /><br />
           <div class="divExcelOtr" style="text-align:left;">  
            <asp:Button ID="btnExpOtros" runat="server" CssClass="btn8" 
                onclick="btnExpOtros_Click" onclientclick="btnExpOtros_Click" 
                Text="Exportar a excel Otros" />   
            </div>
            <asp:UpdatePanel ID="UpdatePanelOtros" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Otros_HeaderPanel" runat="server" style="cursor: pointer;">
                    <div class="heading" style="text-align:left;">
                        <asp:ImageButton ID="Otros_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse" />
                        <strong style="text-align: center;color: #FFFFFF; background-color: #0066FF">ARRIENDOS, SERVICIOS PUBLICOS Y VIGILANCIA</strong>
                    </div>
                </asp:Panel>
                <asp:Panel id="Otros_ContentPanel" runat="server" style="overflow:hidden;">
                    <div style="overflow:scroll;width:auto;">    
                        <asp:GridView ID="gvOtros" runat="server" ShowHeaderWhenEmpty="True" 
                            AutoGenerateColumns="False" Style="font-size: x-small" DataKeyNames="codigo" ShowFooter="True" 
                            onrowediting="gvOtros_RowEditing" OnRowCancelingEdit="gvOtros_RowCancelingEdit" onrowupdating="gvOtros_RowUpdating" 
                            onrowdatabound="gvOtros_RowDataBound"  >
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                        <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                                    </EditItemTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                </asp:TemplateField> 
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                            ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                    </ItemTemplate>                            
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="50"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="50" Enabled="False" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripciòn" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase"  Width="300" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase"  Width="300" />
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="140px" />
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Arriendo Anterior" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblArriendo_ant" runat="server" Text='<%# String.Format("{0:N}", Eval("ARRIENDO_ANT")) %>' style="font-size: x-small"  Width="65" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtArriendo_ant" runat="server" Text='<%# Eval("ARRIENDO_ANT") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeArriendo_ant" runat="server" TargetControlID="txtArriendo_ant"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% Incremento Arriendo" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIncremento_Arriendo" runat="server" Text='<%# String.Format("{0:N2}", Eval("INCREMENTO_ARRIENDO")) %>' style="font-size: x-small"  Width="65" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtIncremento_Arriendo" runat="server" Text='<%# Eval("INCREMENTO_ARRIENDO") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeIncremento_Arriendo" runat="server" TargetControlID="txtIncremento_Arriendo"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Arriendo Actual" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblArriendo" runat="server" Text='<%# String.Format("{0:N}", Eval("ARRIENDO")) %>' style="font-size: x-small"  Width="65" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Servicios Públicos Anterior" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblServicios_ant" runat="server" Text='<%# String.Format("{0:N}", Eval("SERVICIOS_ANT")) %>' style="font-size: x-small"  Width="65" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtServicios_ant" runat="server" Text='<%# Eval("SERVICIOS_ANT") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeServicios_ant" runat="server" TargetControlID="txtServicios_ant"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% Incremento Servicios Públicos" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIncremento_Servicios" runat="server" Text='<%# String.Format("{0:N}", Eval("INCREMENTO_SERVICIOS")) %>' style="font-size: x-small"  Width="65" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtIncremento_Servicios" runat="server" Text='<%# Eval("INCREMENTO_SERVICIOS") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeIncremento_Servicios" runat="server" TargetControlID="txtIncremento_Servicios"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Servicios Públicos" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblServicios" runat="server" Text='<%# String.Format("{0:N}", Eval("SERVICIOS")) %>' style="font-size: x-small"  Width="65" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vigilancia Anterior" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblVigilancia_ant" runat="server" Text='<%# String.Format("{0:N}", Eval("VIGILANCIA_ANT")) %>' style="font-size: x-small"  Width="65" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtVigilancia_ant" runat="server" Text='<%# Eval("VIGILANCIA_ANT") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeVigilancia_ant" runat="server" TargetControlID="txtVigilancia_ant"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% Incremento Vigilancia" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIncremento_Vigilancia" runat="server" Text='<%# String.Format("{0:N}", Eval("INCREMENTO_VIGILANCIA")) %>' style="font-size: x-small"  Width="65" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtIncremento_Vigilancia" runat="server" Text='<%# Eval("INCREMENTO_VIGILANCIA") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeIncremento_Vigilancia" runat="server" TargetControlID="txtIncremento_Vigilancia"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vigilancia" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblVigilancia" runat="server" Text='<%# String.Format("{0:N}", Eval("VIGILANCIA")) %>' style="font-size: x-small"  Width="65" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>                                                      
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                            <SelectedRowStyle Font-Size="XX-Small" />
                            <FooterStyle BackColor="#FFFFCC" />
                        </asp:GridView>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeOtros" runat="Server"
                    TargetControlID="Otros_ContentPanel" ExpandControlID="Otros_HeaderPanel"
                    CollapseControlID="Otros_HeaderPanel" Collapsed="False" ExpandedImage="~/images/collapse.jpg"
                    CollapsedImage="~/images/expand.jpg" ImageControlID="Otros_ToggleImage" /> 
            </ContentTemplate>
            </asp:UpdatePanel>

            <br />
            <div class="divExcelHon" style="text-align:left;">
            <asp:Button ID="btnExpHonorarios" runat="server" CssClass="btn8" 
                onclick="btnExpHonorarios_Click" onclientclick="btnExpHonorarios_Click" 
                Text="Exportar a excel Honorarios" />
            </div>
            <asp:UpdatePanel ID="UpdatePanelHonorarios" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Honorarios_HeaderPanel" runat="server" style="cursor: pointer;">
                    <div class="heading" style="text-align:left;">
                        <asp:ImageButton ID="Honorarios_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg" AlternateText="collapse" />
                        <strong style="text-align: center;color: #FFFFFF; background-color: #0066FF">HONORARIOS</strong>
                    </div>
                </asp:Panel>
                <asp:Panel id="Honorarios_ContentPanel" runat="server" style="overflow:hidden;">
                    <div style="overflow:scroll;">      
                        <asp:GridView ID="gvHonorarios" runat="server" ShowHeaderWhenEmpty="True" onrowcommand="gvHonorarios_RowCommand"
                            AutoGenerateColumns="False" Style="font-size: x-small" DataKeyNames="codigo" ShowFooter="True" 
                            onrowediting="gvHonorarios_RowEditing" OnRowCancelingEdit="gvHonorarios_RowCancelingEdit" onrowdeleting="gvHonorarios_RowDeleting" 
                            onrowupdating="gvHonorarios_RowUpdating" onrowdatabound="gvHonorarios_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                        <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"  ValidationGroup="vgGuardarHon" 
                                        ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" />                                
                                    </FooterTemplate>
                                    <HeaderStyle CssClass="gridIco" />
                                </asp:TemplateField> 
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                            ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                    </ItemTemplate>                            
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código" >
                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtCodigoF" runat="server" Text='<%# Bind("CODIGO") %>' style="font-size: x-small" Width="50" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCodigo" runat="server" TargetControlID="txtCodigoF"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                        <asp:RequiredFieldValidator ID="rfvCodigoF" runat="server" ControlToValidate="txtCodigoF"
                                            Display="Dynamic"  ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="vgGuardarHon" style="font-size: xx-small" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="50"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="50" Enabled="False" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción" >
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtDescripcionF" runat="server" Text='<%# Bind("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase" Width="300" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase"  Width="300" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase"  Width="300" />
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="140px" />
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor Anterior" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtValorAntF" runat="server" Text='<%# Bind("VALOR_ANT") %>' style="font-size: x-small" Width="65" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeValorAntF" runat="server" TargetControlID="txtValorAntF"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblValorAnt" runat="server" Text='<%# String.Format("{0:N}", Eval("VALOR_ANT")) %>' style="font-size: x-small"  Width="65" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtValorAnt" runat="server" Text='<%# Eval("VALOR_ANT") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeValorAnt" runat="server" TargetControlID="txtValorAnt"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Incremento" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtIncrementoF" runat="server" Text='<%# Bind("INCREMENTO") %>' style="font-size: x-small" Width="65" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeIncrementoF" runat="server" TargetControlID="txtIncrementoF"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIncremento" runat="server" Text='<%# String.Format("{0:N}", Eval("INCREMENTO")) %>' style="font-size: x-small"  Width="65" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtIncremento" runat="server" Text='<%# Eval("INCREMENTO") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeIncremento" runat="server" TargetControlID="txtIncremento"         
                                            FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                    </EditItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Valor" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterTemplate>                                                   
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblValor" runat="server" Text='<%# String.Format("{0:N}", Eval("VALOR")) %>' style="font-size: x-small"  Width="65" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridItem" />
                            <FooterStyle CssClass="gridHeader" />
                            <SelectedRowStyle Font-Size="XX-Small" />
                        </asp:GridView>
                    </div>                           
	                <br />
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeHonorarios" runat="Server"
                    TargetControlID="Honorarios_ContentPanel" ExpandControlID="Honorarios_HeaderPanel"
                    CollapseControlID="Honorarios_HeaderPanel" Collapsed="False" ExpandedImage="~/images/collapse.jpg"
                    CollapsedImage="~/images/expand.jpg" ImageControlID="Honorarios_ToggleImage" /> 
            </ContentTemplate>
            </asp:UpdatePanel>

            <div class="regresar" style="text-align:center;">
                <asp:ImageButton ID="btnRegresarOTR" runat="server" 
                    ImageUrl="~/Images/btnCargarPresupuesto.jpg" onclick="btnRegresarOTR_Click" />
            </div>
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

        <asp:View ID="mvTecnologia" runat="server">       
            <br /><br />
            <div class="divExcelTecnologia" style="text-align:left;">
            <asp:Button ID="btnExpTecnologia" runat="server" CssClass="btn8" 
                onclick="btnExpTecnologia_Click" onclientclick="btnExpTecnologia_Click" 
                Text="Exportar a Excel" />  
            </div>
            <asp:UpdatePanel ID="upTecnologia" runat="server">
            <ContentTemplate>
                <strong>PRESUPUESTO DE TECNOLOGIA</strong>
                <div style="overflow:scroll;width:auto;">    
                    <asp:GridView ID="gvTecnologia" runat="server" ShowHeaderWhenEmpty="True" 
                        AutoGenerateColumns="False" Style="font-size: x-small" DataKeyNames="codigo" ShowFooter="True" 
                        onrowediting="gvTecnologia_RowEditing" OnRowCancelingEdit="gvTecnologia_RowCancelingEdit" onrowupdating="gvTecnologia_RowUpdating" 
                        onrowdatabound="gvTecnologia_RowDataBound" onrowdeleting="gvTecnologia_RowDeleting" onrowcommand="gvTecnologia_RowCommand" >
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg" ToolTip="Editar" Width="16px" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="btnActualizar" runat="server" CommandName="Update" ImageUrl="~/Images/gr_guardar.jpg" ToolTip="Actualizar" Width="16px" />
                                    <asp:ImageButton ID="btnCancelar" runat="server" CommandName="Cancel" ImageUrl="~/Images/gr_cancelar.jpg" ToolTip="Cancelar" Width="16px" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:ImageButton ID="btnNuevo" runat="server" CausesValidation="False" CommandName="AddNew"
                                    ImageUrl="~/Images/gr_nuevo.jpg" ToolTip="Crear Nuevo" Width="16px" ValidationGroup="vgGuardarActFij" />                                
                                </FooterTemplate>
                                <HeaderStyle CssClass="gridIco" />
                            </asp:TemplateField> 
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                        ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />
                                </ItemTemplate>                            
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Código" >
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCodigoF" runat="server" Text='<%# Bind("CODIGO") %>' style="font-size: x-small" Width="50" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeCodigo" runat="server" TargetControlID="txtCodigoF"         
                                        FilterType="Custom, Numbers" ValidChars="0123456789" />
                                    <asp:RequiredFieldValidator ID="rfvCodigoF" runat="server" ControlToValidate="txtCodigoF"
                                        Display="Dynamic"  ErrorMessage="Campo Requerido" ForeColor="Red" SetFocusOnError="True"
                                        ValidationGroup="vgGuardarActFij" style="font-size: xx-small" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="50"  />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCodigo" runat="server" Text='<%# Eval("CODIGO") %>' style="font-size: x-small"  Width="50" Enabled="False" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripciòn" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDescripcionF" runat="server" Text='<%# Bind("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase" MaxLength="300" Width="300" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase"  Width="300" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("DESCRIPCION") %>' style="font-size: x-small; text-transform :uppercase"  Width="300" />
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="140px" />
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cod.Ofi" >
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlCodOficinaF" runat="server" DataSource="<%# ListaOficinas() %>" DataTextField="nombre" DataValueField="cod_oficina" 
                                    SelectedValue='<%# Bind("COD_OFICINA") %>' style="font-size: x-small; text-align: left" Width="40" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCodOficina" runat="server" Text='<%# String.Format("{0:N0}", Eval("COD_OFICINA")) %>'  style="font-size: x-small; text-align: center" Width="40" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlCodOficina" runat="server" DataSource="<%# ListaOficinas() %>" DataTextField="nombre" DataValueField="cod_oficina" 
                                        SelectedValue='<%# Bind("COD_OFICINA") %>' style="font-size: x-small; text-align: left"  Width="40" AppendDataBoundItems="True" >
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vr.Compra" >
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:TextBox ID="txtVrcompraF" runat="server" Text='<%# Bind("VALOR") %>' style="font-size: x-small" Width="65" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeVrcompraF" runat="server" TargetControlID="txtVrcompraF"         
                                        FilterType="Custom, Numbers" ValidChars="0123456789" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblVrcompra" runat="server" Text='<%# String.Format("{0:N}", Eval("VALOR")) %>' style="font-size: x-small"  Width="65" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVrcompra" runat="server" Text='<%# Eval("VALOR") %>' style="font-size: x-small; text-align:right;"  Width="65" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeVrcompra" runat="server" TargetControlID="txtVrcompra"         
                                        FilterType="Custom, Numbers" ValidChars="0123456789.," />
                                </EditItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Fecha de Compra" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtFechaCompraF" runat="server" Text='<%# Bind("FECHA_COMPRA") %>' style="font-size: x-small" Width="60" />
                                    <ajaxToolkit:CalendarExtender ID="txtFechaCompraF_CalendarExtender" runat="server" 
                                        Format="dd/MM/yyyy" TargetControlID="txtFechaCompraF">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:MaskedEditExtender ID="txtFechaCompraF_MaskedEditExtender" 
                                        runat="server"  TargetControlID="txtFechaCompraF" Mask="99/99/9999" 
                                        clearmaskonlostfocus="false" MaskType="None" UserDateFormat="MonthDayYear">
                                    </ajaxToolkit:MaskedEditExtender>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFechaCompra" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("FECHA_COMPRA")) %>'  style="font-size: x-small" Width="60" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFechaCompra" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("FECHA_COMPRA")) %>'  style="font-size: x-small" Width="60" />
                                    <ajaxToolkit:CalendarExtender ID="txtFechaCompra_CalendarExtender" runat="server" 
                                        Format="dd/MM/yyyy" TargetControlID="txtFechaCompra">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:MaskedEditExtender ID="txtFechaCompra_MaskedEditExtender" 
                                        runat="server"  TargetControlID="txtFechaCompra" Mask="99/99/9999" 
                                        clearmaskonlostfocus="false" MaskType="None" UserDateFormat="DayMonthYear">
                                    </ajaxToolkit:MaskedEditExtender>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo Concepto" >
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlTipoActivoF" runat="server" style="font-size: x-small; text-align: left" Width="80" >
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="1">Licencias (nuevas)</asp:ListItem>
                                        <asp:ListItem Value="2">Renovacion Software</asp:ListItem>
                                        <asp:ListItem Value="3">Equipos y Gastos</asp:ListItem>
                                        <asp:ListItem Value="4">Comunicaciones</asp:ListItem>
                                        <asp:ListItem Value="5">Mantenimiento</asp:ListItem>
                                        <asp:ListItem Value="6">Cursos y eventos especiales</asp:ListItem>
                                        <asp:ListItem Value="7">Otros Gastos</asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoActivo" runat="server" Text='<%# Eval("NOM_TIPO_CONCEPTO") %>'  style="font-size: x-small; text-align: center" Width="80" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTipoActivo" runat="server" SelectedValue='<%# Bind("TIPO_CONCEPTO") %>' 
                                        style="font-size: x-small; text-align: left"  Width="80" AppendDataBoundItems="True" >
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="1">Licencias (nuevas)</asp:ListItem>
                                        <asp:ListItem Value="2">Renovacion Software</asp:ListItem>
                                        <asp:ListItem Value="3">Equipos y Gastos</asp:ListItem>
                                        <asp:ListItem Value="4">Comunicaciones</asp:ListItem>
                                        <asp:ListItem Value="5">Mantenimiento</asp:ListItem>
                                        <asp:ListItem Value="6">Cursos y eventos especiales</asp:ListItem>
                                        <asp:ListItem Value="7">Otros Gastos</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridHeader" />
                        <PagerStyle CssClass="gridPager" />
                        <RowStyle CssClass="gridItem" />
                        <FooterStyle CssClass="gridHeader" />
                        <SelectedRowStyle Font-Size="XX-Small" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            <div class="regresar" style="text-align:center;">
                <asp:ImageButton ID="btnRegresarTEC" runat="server" 
                    ImageUrl="~/Images/btnCargarPresupuesto.jpg" onclick="btnRegresarTEC_Click" />
            </div>
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