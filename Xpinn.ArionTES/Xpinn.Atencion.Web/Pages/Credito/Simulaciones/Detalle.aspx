<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc3" %>
<%@ Register Src="~/Controles/decimalesGridRow.ascx" TagName="decimalesGridRow" TagPrefix="uc3" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tableNormal {
            border-collapse: separate;
            border-spacing: 4px;            
        }        
        .noEditable{
            background-color: white;
            border: none;
        }
    </style>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function ValidNum(e) {
            var keyCode = e.which ? e.which : e.keyCode
            return ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:Button ID="btnConsultar" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />                
     <asp:Button ID="btnLimpiar" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
    <br />
    <br />
    <div class="form-group" style="padding: 0px">
        <asp:UpdatePanel ID="updLineas" runat="server">
            <ContentTemplate>
                <table width="100%" class="tableNormal">
                    <tr>
                        <td>
                            <div class="col-sm-12">
                                <div class="col-sm-3 text-left">
                                    Tipo de crédito :
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="form-control" Width="90%"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged" />
                                </div>
                                <div class="col-sm-5">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="col-sm-12">
                                <div class="col-sm-3 text-left">
                                    Valor solicitado :
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox  ID="txtMontoSolicitado" runat="server" CssClass="form-control" Width="80%" onkeypress="return ValidaNum(event);" onblur="valorCambio(event)"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 text-left">
                                    Valor máximo :
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtMontoMaximo" runat="server" Width_="80%"
                                        AutoPostBack_="false" Enabled="false" CssClass="form-control noEditable" />
                                </div>
                                <div class="col-sm-1">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="col-sm-12">
                                <div class="col-sm-3 text-left">
                                    Num cuotas :
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtNroCuotas" runat="server" CssClass="form-control" Width="80%"
                                        Style="text-align: right" OnTextChanged="txtNroCuotas_TextChanged" />
                                    <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtNroCuotas" ValidChars=".">
                                    </asp:FilteredTextBoxExtender>
                                </div>
                                <div class="col-sm-2 text-left">
                                    Plazo máximo :
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtPlazoMaximo" runat="server" CssClass="form-control noEditable" Width="80%"
                                        Style="text-align: right" Enabled="false" BackColor="White" />
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                        FilterType="Numbers, Custom" TargetControlID="txtPlazoMaximo" ValidChars=".">
                                    </asp:FilteredTextBoxExtender>
                                </div>
                                <div class="col-sm-1">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="col-sm-12 ">
                                <div class="col-sm-3 text-left">
                                    Tasa interés :
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txttasa" runat="server" CssClass="form-control noEditable" Width="80%"
                                        Style="text-align: right" BackColor="White" />                                   
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:Label ID="lblTipoTasa" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="form-control" Width="80%" Visible="false" />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlTipoLiquidacion" runat="server" CssClass="form-control"
                                        Width="90%" Visible="false" />
                                </div>
                            </div>
                        </td>
                    </tr>                    
                    <tr>
                        <td style="width: 100%">
                            <asp:Panel ID="panelComision" runat="server">
                                <div class="col-sm-12 ">
                                    <div class="col-sm-3 text-left">
                                        <asp:Label ID="LblComision" runat="server" Text="Comisión" />
                                    </div>
                                    <div class="col-sm-3 text-left">
                                        <asp:TextBox ID="txtComision" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="meeComision" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                            TargetControlID="txtComision" />
                                    </div>
                                    <div class="col-sm-6">
                                    </div>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:Panel ID="panelAporte" runat="server">
                                <div class="col-sm-12 ">
                                    <div class="col-sm-3 text-left">
                                        <asp:Label ID="lblAporte" runat="server" Text="Aportes" />
                                    </div>
                                    <div class="col-sm-3 text-left">
                                        <asp:TextBox ID="txtAporte" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="meeAporte" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                            TargetControlID="txtAporte" />
                                    </div>
                                    <div class="col-sm-6">
                                    </div>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:Panel ID="panelSeguro" runat="server">
                                <div class="col-sm-12 ">
                                    <div class="col-sm-3 text-left">
                                        <asp:Label ID="lblSeguro" runat="server" Text="Seguro" />
                                    </div>
                                    <div class="col-sm-3 text-left">
                                        <asp:TextBox ID="txtSeguro" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="999,999,999" MaskType="Number"
                                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                            TargetControlID="txtSeguro" />
                                    </div>
                                    <div class="col-sm-6">
                                    </div>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="col-sm-12 ">
                                <div class="col-sm-3 text-left">
                                    <asp:Label ID="lblFecPrimPago" Text="Fecha de Primer Pago" runat="server" Visible="false" />
                                </div>
                                <div class="col-sm-3 text-left">
                                    <ucFecha:fecha ID="txtFechaPrimerPago" runat="server" Width_="80%" Visible="false" />
                                </div>
                                <div class="col-sm-6">
                                    &nbsp;
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="updCuotasExtras" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlCuotaExtra" runat="server">
                    <table width="100%" class="tableNormal">
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <asp:Label ID="lblErrorCuotaExtra" runat="server" CssClass="text-danger text-left"></asp:Label><br />
                                    <label class="text-success">Información de cuotas extras. (Opcional)</label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-5 text-left">
                                        Porcentaje del crédito en cuotas extras
                                    </div>
                                    <div class="col-sm-7 text-left">
                                        <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="form-control" Width="136px" onkeypress="return ValidNum(event);" MaxLength="6" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-5 text-left">
                                        Número de cuotas extras
                                    </div>
                                    <div class="col-sm-7 text-left">
                                        <asp:TextBox ID="txtNumeroCuotaExt" runat="server" CssClass="form-control" Width="136px" onkeypress="return ValidNum(event);" MaxLength="3" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-5 text-left">
                                        Fecha de primera cuota extra
                                    </div>
                                    <div class="col-sm-7 text-left">
                                        <ucFecha:fecha ID="txtFechaCuotaExt" runat="server" cssclass="form-control" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-5 text-left">
                                        Valor cuota extra
                                    </div>
                                    <div class="col-sm-7 text-left">
                                        <uc3:decimales ID="txtValorCuotaExt" runat="server" CssClass="textbox" Width_="160px" AutoPostBack_="false"></uc3:decimales>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-5 text-left">
                                        Forma de pago
                                    </div>
                                    <div class="col-sm-7 text-left">
                                        <asp:DropDownList ID="ddlFormaPagoCuotaExt" runat="server" Width="190px" CssClass="form-control">
                                            <asp:ListItem Value="1">Caja</asp:ListItem>
                                            <asp:ListItem Value="2">Nomina</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-5 text-left">
                                        Periodicidad de cuota extra
                                    </div>
                                    <div class="col-sm-7 text-left">
                                        <asp:DropDownList ID="ddlPeriodicidadCuotaExt" runat="server" CssClass="form-control" Width="190px" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-5 text-left">
                                        Tipo cuota extra
                                    </div>
                                    <div class="col-sm-7 text-left">
                                        <asp:DropDownList ID="ddlCuotaExtTipo" runat="server" CssClass="form-control" Width="190px" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-6 text-right">
                                    <asp:LinkButton ID="btnGenerarCuotaExtra" runat="server" CssClass="btn btn-default text-center" Width="180px" ToolTip="Generar"
                                        Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnGenerarCuotaExtra_Click">
                                        <b>Generar cuotas extras</b>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:LinkButton ID="btnLimpiarCuotaExtra" runat="server" CssClass="btn btn-default text-center" Width="120px" ToolTip="Limpiar"
                                        Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnLimpiarCuotaExtra_Click">
                                        <b>Limpiar</b>
                                    </asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-md-8">
                                    <%----%>
                                    <asp:Panel ID="pnlListaCuotas" runat="server">
                                        <asp:GridView ID="gvCuoExt" runat="server" CssClass="table" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No se encontraron registros." AutoGenerateColumns="False"
                                            GridLines="Vertical" OnRowDeleting="gvCuoExt_RowDeleting" DataKeyNames="valor">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="glyphicon glyphicon-trash" ToolTip="Eliminar" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                                        
                                                        </asp:LinkButton>
                                                        <%--<span aria-hidden="true" class="glyphicon glyphicon-ok"></span>--%>
                                                        <%--<asp:ImageButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            ImageUrl="~/Images/gr_elim.jpg" ToolTip="Eliminar" Width="16px" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Pago">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfechapago" runat="server" Text='<%# Bind("fecha_pago","{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Forma Pago">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblformapago" runat="server" Text='<%# Bind("des_forma_pago") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Valor">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvalor" runat="server" Text='<%# Bind("valor","{0:C}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo Cuota Extra">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltipocuota" runat="server" Text='<%# Bind("des_tipo_cuota") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cod.Forma Pago" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcodformapago" runat="server" Text='<%# Bind("forma_pago") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#337ab7" ForeColor="White" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-4">
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlLinea" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>


        <hr style="width: 100%; border-color: #2780e3; margin-bottom:10px;" />
                <div class="col-sm-12 col-md-6" style="margin-bottom:5px; margin-top:5px;">
                    <div class="col-sm-6 text-left">
                        Cuota calculada
                    </div>
                    <div class="col-sm-6 text-left">                        
                        <asp:TextBox ID="txtCuota" runat="server" Enabled="false" Width="160px" CssClass="form-control noEditable" />
                    </div>                            
                </div>
                <div class="col-sm-12 col-md-6 text-left" style="margin-bottom:5px; margin-top:5px;">
                    <div class="col-sm-6">
                        <asp:Button runat="server" ID="btnAdquirir" Visible="false" Text="Solicitar crédito" CssClass="btn btn-primary form-control" style="height:35px; margin:auto" OnClick="btnAdquirir_Click"/>
                    </div>
                </div>            
        <hr style="width: 100%; border-color: #2780e3; margin-top:10px;" />

        <div class="col-sm-12">
            <asp:Panel ID="panelLineas" runat="server">
                <div class="text-left">
                    <strong>Datos de créditos a solicitar</strong>
                </div>
                <div style="overflow: scroll; max-width: 100%;">
                    <asp:GridView ID="gvLista" runat="server" AllowPaging="True" Width="100%"
                        AutoGenerateColumns="False" GridLines="Both" PageSize="12" CssClass="table"
                        ShowHeaderWhenEmpty="True" RowStyle-Font-Size="Small" HeaderStyle-Font-Size="Small"
                        OnPageIndexChanging="gvLista_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="numerocuota" HeaderText="No" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechacuota" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:dd-MM-yyyy}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sal_ini" HeaderText="Saldo inicial" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="capital" HeaderText="Capital" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_1" HeaderText="Int_1" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_2" HeaderText="Int_2" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_3" HeaderText="Int_3" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_4" HeaderText="Int_4" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_5" HeaderText="Int_5" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_6" HeaderText="Int_6" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_7" HeaderText="Int_7" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_8" HeaderText="Int_8" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_9" HeaderText="Int_9" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_10" HeaderText="Int_10" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_11" HeaderText="Int_11" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_12" HeaderText="Int_12" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_13" HeaderText="Int_13" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_14" HeaderText="Int_14" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_15" HeaderText="Int_15" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:c0}" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sal_fin" HeaderText="Saldo final" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c0}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle CssClass="pagerstyle" />
                        <PagerTemplate>
                            &nbsp;
                                    <asp:ImageButton ID="btnPrimero" runat="server" CommandName="Page" ToolTip="Prim. Pag"
                                        CommandArgument="First" ImageUrl="~/Imagenes/first.png" />
                            <asp:ImageButton ID="btnAnterior" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                CommandArgument="Prev" ImageUrl="~/Imagenes/previous.png" />
                            <asp:ImageButton ID="btnSiguiente" runat="server" CommandName="Page" ToolTip="Sig. página"
                                CommandArgument="Next" ImageUrl="~/Imagenes/next.png" />
                            <asp:ImageButton ID="btnUltimo" runat="server" CommandName="Page" ToolTip="Últ. Pag"
                                CommandArgument="Last" ImageUrl="~/Imagenes/last.png" />
                        </PagerTemplate>
                        <HeaderStyle BackColor="#337ab7" ForeColor="White" />
                    </asp:GridView>
                </div>
                <div style="text-align: center; width: 100%;">
                    <asp:Label ID="lblTotReg" runat="server" Visible="false" />
                    <asp:Label ID="lblInfo" runat="server" Visible="false" Text="Su consulta no obtuvo ningún resultado." />
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
