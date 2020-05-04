<%@ Page Title=".: Simulación de Crédito :." Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SimulacionCredito.aspx.cs" Inherits="SimulacionCredito" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form-group" style="display:none">
        <div class="col-sm-12">
            <asp:DropDownList ID="txtnumeroSMLMV" AutoPostBack="true" runat="server" CssClass="textbox"
                Width="80px" Enabled="true" TabIndex="7">
                <asp:ListItem Text="SMLMV" Value="1" />
                <asp:ListItem Text="50%" Value="2" />
                <asp:ListItem Text="Otro" Value="3" />
            </asp:DropDownList>            
        </div>
        <div class="col-sm-12">
            <asp:TextBox ID="txtMenosSMLMV" runat="server" CssClass="textbox" Width="150px" Style="text-align: right" />
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-12">
            <div class="col-sm-3 text-left">
                Monto Solicitado :
            </div>
            <div class="col-sm-3">
                <uc3:decimales ID="txtMontoSolicitado" runat="server" cssclass="form-control" Width_="80%" AutoPostBack_="false"/>
            </div>
            <div class="col-sm-6">
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-12">
            <br />
        </div>
        <div class="col-sm-12">
            <div class="col-sm-3 text-left">
                Número de Cuotas :
            </div>
            <div class="col-sm-3">
                <asp:TextBox ID="txtNroCuotas" runat="server" CssClass="form-control" Width="80%"
                    Style="text-align: right" />
                <asp:FilteredTextBoxExtender ID="txtValor_FilteredTextBoxExtender" runat="server"
                    Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtNroCuotas" ValidChars=".">
                </asp:FilteredTextBoxExtender>
            </div>
            <div class="col-sm-6 text-left">
                <asp:CheckBox ID="cbeducativo" Text="&nbsp;&nbsp;&nbsp;Crédito Educativo" runat="server" Enabled="true"
                    TabIndex="11" AutoPostBack="True" OnCheckedChanged="cbeducativo_CheckedChanged" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-12">
            <br />
        </div>
        <div class="col-sm-12">
            <div class="col-sm-3 text-left">
                Periodicidad :
            </div>
            <div class="col-sm-3">
                <asp:DropDownList ID="ddlPeriodicidad" runat="server" CssClass="form-control" Width="80%" />
            </div>
            <div class="col-sm-6 text-left">
                &nbsp;
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-12">
             <hr style="width:100%; border-color:#2780e3;" /> 
        </div>
    </div>

    <asp:UpdatePanel ID="updLineas" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <div class="col-sm-12 text-left">
                    <br />                    
                </div>
                <div class="col-sm-12">
                    <asp:Panel ID="panelLineas" runat="server">
                        <div class="text-left"><strong>Datos de créditos a solicitar</strong></div>
                        <div style="overflow: scroll; max-width: 100%;">
                            <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table"
                                RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="Smaller" HeaderStyle-HorizontalAlign="Center" GridLines="Horizontal"
                                RowStyle-CssClass="table" PageSize="5" AllowPaging="true" OnPageIndexChanging="gvLista_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <cc1:CheckBoxGrid ID="chkSeleccionar" runat="server" CommandArgument='<%#DataBinder.Eval(Container, "RowIndex") %>'
                                                OnCheckedChanged="chkSeleccionar_CheckedChanged" AutoPostBack="true" Checked='<%# Convert.ToBoolean(Eval("idpreanalisis")) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Línea de Crédito">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinea_credito" Text='<%#Eval("cod_linea_credito")%>' runat="server"
                                                Width="30px" /><asp:Label ID="lbnomlinea_credito" Text='<%#Eval("nom_linea_credito")%>'
                                                    runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Monto Máximo" DataField="monto_maximo" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Plazo Máximo" DataField="plazo">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cupo Disponible" DataField="monto" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cuota Estimada" DataField="cuota_credito" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Tasa Int.Cte." DataField="tasa">
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Reciprocidad">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbreciprocidad" runat="server" Enabled="false" Checked='<%#Convert.ToBoolean(Eval("reciprocidad")) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Refinanciar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbrefinancia" runat="server" Enabled="false" Checked='<%#Convert.ToBoolean(Eval("check")) %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Saldo Actual" DataField="saldo_capital" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Auxilio">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmanejaauxilio" Text='<%# Eval("maneja_auxilio") %>' runat="server"
                                                Style="font-size: xx-small" Visible="False" />
                                            <asp:Label ID="lblporcentajeauxilio" Text='<%# String.Format("{0:N2}", Eval("porcentaje_auxilio")) %>'
                                                runat="server" Style="font-size: xx-small" />
                                            <asp:Label ID="lblsimbolo" Text="%" runat="server" Style="font-size: xx-small" Width="10px" />&nbsp;
                                            <asp:Label ID="lblvalorauxilio" Text='<%# String.Format("{0:N}", Eval("valor_auxilio")) %>'
                                                runat="server" Style="font-size: xx-small" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="140px" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagerstyle" />
                                <PagerTemplate>
                                    &nbsp;
                                    <asp:ImageButton ID="btnPrimero" runat="server" CommandName="Page" ToolTip="Prim. Pag"
                                        CommandArgument="First" ImageUrl="~/Imagenes/first.png" />
                                    <asp:ImageButton ID="btnAnterior" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                                        CommandArgument="Prev" ImageUrl="~/Imagenes/previous.png"/>
                                    <asp:ImageButton ID="btnSiguiente" runat="server" CommandName="Page" ToolTip="Sig. página"
                                        CommandArgument="Next" ImageUrl="~/Imagenes/next.png" />
                                    <asp:ImageButton ID="btnUltimo" runat="server" CommandName="Page" ToolTip="Últ. Pag"
                                        CommandArgument="Last" ImageUrl="~/Imagenes/last.png" />
                                </PagerTemplate>
                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
                            </asp:GridView>
                        </div>
                        <div style="text-align: center; width: 100%;">
                            <asp:Label ID="lblTotReg" runat="server" Visible="false" />
                            <asp:Label ID="lblInfo" runat="server" Visible="false" Text="Su consulta no obtuvo ningún resultado." />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

