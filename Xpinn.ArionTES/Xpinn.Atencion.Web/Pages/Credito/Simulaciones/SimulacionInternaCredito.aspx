<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SimulacionInternaCredito.aspx.cs" Inherits="SimulacionInternaCredito" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc3" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-group" style="display: none">
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
                <uc3:decimales ID="txtMontoSolicitado" runat="server" cssclass="form-control" Width_="80%" AutoPostBack_="false" />
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
            <br />
            <br />
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                <div class="col-sm-3 text-left">
                    Tasa Interes :
                </div>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtInteresMensual" runat="server" CssClass="form-control" Width="80%"
                        Style="text-align: right" />
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                        Enabled="True" FilterType="Numbers, Custom" TargetControlID="txtInteresMensual" ValidChars=".">
                    </asp:FilteredTextBoxExtender>
                </div>
                <div class="col-sm-6">
                </div>
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
            <hr style="width: 100%; border-color: #2780e3;" />
        </div>
    </div>

    <asp:UpdatePanel ID="updLineas" runat="server">
        <ContentTemplate>
            <div class="form-group">                
                <div class="col-sm-12">
                    <asp:Panel ID="panelLineas" runat="server">
                        <div class="text-left"><strong>Datos de créditos a solicitar</strong></div>
                        <div style="overflow: scroll; max-width: 100%;">
                            <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" CssClass="table"
                                RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="Smaller" GridLines="Horizontal"
                                RowStyle-CssClass="table" PageSize="12" AllowPaging="true" OnPageIndexChanging="gvLista_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="numerocuota" HeaderText="No. Cuota"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valorcuota" HeaderText="Valor Cuota"
                                        ItemStyle-HorizontalAlign="Left" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="capital" HeaderText="Capital"
                                        ItemStyle-HorizontalAlign="Left" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="interes" HeaderText="Interes Corriente" DataFormatString="{0:c}"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="sal_pendiente" HeaderText="Saldo Final"
                                        ItemStyle-HorizontalAlign="Left" DataFormatString="{0:c}">
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
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
                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
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

