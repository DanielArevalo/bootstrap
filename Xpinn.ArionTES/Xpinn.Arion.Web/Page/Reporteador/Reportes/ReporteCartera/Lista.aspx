<%@ Page Language="C#" MasterPageFile="~/General/Master/site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>



<%@ Register Src="~/General/Controles/fecha.ascx" TagName="fecha" TagPrefix="ucFecha" %>
<%@ Register Src="~/General/Controles/ctlseleccionmultipledropdown.ascx" TagName="dropdownmultiple" TagPrefix="ucDrop" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%; height: 193px;">
        <tr>
            <td style="text-align: left; font-size: large; color: #359AF2; font-weight: bold;" colspan="3">
                REPORTE DE CARTERAS</td>
            <td style="text-align: center; width: 467px;">&nbsp;</td>
            <td class="logo" style="width: 194px">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left; font-size: small;" colspan="3">
                <strong>Criterios de Generación:</strong></td>
            <td style="text-align: center; width: 467px;">&nbsp;</td>
            <td class="logo" style="width: 194px">&nbsp;</td>
        </tr>
        <tr>
            <td style="border-style: none; border-color: #359AF2; text-align: left">
                <asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Inicial Desembolso:"></asp:Label><br />
                <ucFecha:fecha ID="ucFechaInicial" runat="server" style="text-align: center" />
            </td>
            <td style="border-style: none; border-color: #359AF2; text-align: left">
                <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final Desembolso:"></asp:Label><br />
                <ucFecha:fecha ID="ucFechaFinal" runat="server" style="text-align: center" />
            </td>

            <td style="border-style: none; border-color: #359AF2; text-align: left">&nbsp;</td>
            <td style="border-style: none; border-color: #359AF2; text-align: left"><br />
            </td>
            <td style="border-style: none; border-color: #359AF2; text-align: left"><br />
            </td>

        </tr>
        <tr>
            <td style="border-style: none; border-color: #359AF2; text-align: left">
                Oficina:
                <ucDrop:dropdownmultiple ID="ddloficina" runat="server" Height="24px" Width="200px"></ucDrop:dropdownmultiple>
            </td>
            <td style="border-style: none; border-color: #359AF2; text-align: left">
                Categoria:<ucDrop:dropdownmultiple ID="ddlCategoria" runat="server" Height="24px" Width="165px"></ucDrop:dropdownmultiple>
            </td>

            <td style="border-style: none; border-color: #359AF2; text-align: left">&nbsp;</td>
            <td style="border-style: none; border-color: #359AF2; text-align: left">Linea de Crédito:<ucDrop:dropdownmultiple ID="ddlLinea" runat="server" Height="24px" Width="200px"></ucDrop:dropdownmultiple>
            </td>
            <td style="border-style: none; border-color: #359AF2; text-align: left">&nbsp;</td>

        </tr>
        <tr>
            <td style="border-style: none; border-color: #359AF2; text-align: left">
                &nbsp;</td>
            <td style="border-style: none; border-color: #359AF2; text-align: left">
                &nbsp;</td>

            <td style="border-style: none; border-color: #359AF2; text-align: left">&nbsp;</td>
            <td style="border-style: none; border-color: #359AF2; text-align: left">&nbsp;</td>
            <td style="border-style: none; border-color: #359AF2; text-align: left">&nbsp;</td>

        </tr>
    </table>


    <asp:Panel ID="Principal" runat="server">
        <asp:Panel ID="Listado" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td>
                          <div style="overflow: scroll; width: 1040px; max-height: 568px">
                                    
                                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="4"
                                            ForeColor="Black" GridLines="Vertical" PageSize="20"
                                            OnSelectedIndexChanged="gvLista_SelectedIndexChanged"
                                            OnRowEditing="gvLista_RowEditing"
                                            OnPageIndexChanging="gvLista_PageIndexChanging" Font-Size="X-Small">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <%--      <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_info.jpg"
                                            ToolTip="Detalle" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridIco">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" CommandName="Edit" ImageUrl="~/Images/gr_edit.jpg"
                                        ToolTip="Editar" Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Cod.Oficina">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("nombre") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Número Radicación">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("numero_radicacion") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("numero_radicacion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Primer Apellido">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("nombres") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("nombres") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Identificacion">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("identificacion") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("identificacion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Num.Pagare">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("num_pagare") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("num_pagare") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Aprobación">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("fecha_aprobacion") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("fecha_aprobacion", "{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Terminación">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("fecha_vencimiento") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("fecha_vencimiento", "{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Saldo Capital">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("saldo_capital") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("saldo_capital", "{0:N0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Ult.Pago">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("fecha_ultimo_pago") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("fecha_ultimo_pago", "{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Proximo Pago">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("fecha_prox_pago") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("fecha_prox_pago", "{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Días Mora">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("dias_mora") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("dias_mora") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Clasificación">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("clasificacion") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("clasificacion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Linea">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("cod_linea_credito") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("cod_linea_credito") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Periodicidad">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("periodicidad") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("periodicidad") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Modalidad Pago Intereses">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("modalidad_pag_intereses") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("modalidad_pag_intereses") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo Garantia">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("tipo_garantia") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label16" runat="server" Text='<%# Bind("tipo_garantia") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Categoria">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("categoria") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label17" runat="server" Text='<%# Bind("categoria") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="% Provisión">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox18" runat="server" Text='<%# Bind("por_provision") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# Bind("por_provision") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Provisión Capital">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("provision_capital") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label19" runat="server" Text='<%# Bind("provision_capital") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo Tasa">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox20" runat="server" Text='<%# Bind("desc_tasa") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label20" runat="server" Text='<%# Bind("desc_tasa") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tasa Int.Corriente">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox21" runat="server" Text='<%# Bind("tasa_int_corr") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label21" runat="server" Text='<%# Bind("tasa_int_corr") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Formato de la Tasa">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("formato_tasa_int") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label22" runat="server" Text='<%# Bind("formato_tasa_int") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Días Causados">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("dias_causados") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label23" runat="server" Text='<%# Bind("dias_causados") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Interes Cte.Causado">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox24" runat="server" Text='<%# Bind("int_cte_causado") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label24" runat="server" Text='<%# Bind("int_cte_causado") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Provisión int. Corriente">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox25" runat="server" Text='<%# Bind("provisio_int_cte") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label25" runat="server" Text='<%# Bind("provisio_int_cte") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Interes de Orden">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("int_orden") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label26" runat="server" Text='<%# Bind("int_orden") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tasa Int. Mora">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox27" runat="server" Text='<%# Bind("tasa_int_mora") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label27" runat="server" Text='<%# Bind("tasa_int_mora") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Formato de la tasa de Mora">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox28" runat="server" Text='<%# Bind("formato_tasa_int_mor") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label28" runat="server" Text='<%# Bind("formato_tasa_int_mor") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aportes">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("aportes") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label29" runat="server" Text='<%# Bind("aportes", "{0:N0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cod. Cliente">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("cod_cli") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label30" runat="server" Text='<%# Bind("cod_cli") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vr. Cuota Capital">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("valorCap") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label31" runat="server" Text='<%# Bind("valorCap", "{0:N0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Otros Cobros">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("otros_cobros") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label32" runat="server" Text='<%# Bind("otros_cobros", "{0:N0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Provisión Otros">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("provision_otros") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label33" runat="server" Text='<%# Bind("provision_otros", "{0:N0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Teléfonos">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("telefonos") %>'></asp:TextBox>
                                                    </ItemTemplate><ItemStyle Width="60" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label34" runat="server" Text='<%# Bind("telefonos") %>'></asp:Label>
                                                    </ItemTemplate><ItemStyle Width="60" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" />
                                            <HeaderStyle CssClass="gridHeader" />                                            
                                            <PagerStyle CssClass="gridHeader" Font-Bold="False" />
                                            <RowStyle CssClass="gridItem" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                            <SortedAscendingHeaderStyle BackColor="#848384" />
                                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                            <SortedDescendingHeaderStyle BackColor="#575357" />                                           
                                        </asp:GridView>
                                    </div>
                               
                        <asp:Label ID="lblTotalRegs" runat="server" Visible="False" />
                        <asp:Label ID="lblInfo" runat="server" Text="Su consulta no obtuvo ningun resultado." Visible="False" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>



</asp:Content>
