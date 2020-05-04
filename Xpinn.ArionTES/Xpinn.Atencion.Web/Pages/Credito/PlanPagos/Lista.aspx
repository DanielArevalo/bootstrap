<%@ Page Title=".: Plan Pagos :." Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tableNormal
        {
            border-collapse: separate;
            border-spacing: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-group">
        <div class="col-sm-12 text-left tableNormal">
            <br />
        </div>
        <div class="col-sm-12">
            <asp:Panel ID="panelGrid" runat="server">
                <div class="text-left">
                    <strong>Listado de créditos</strong>
                </div>
                <div style="overflow: scroll; max-width: 100%;">
                    <br />
                    <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        GridLines="Horizontal" ShowHeaderWhenEmpty="True" CssClass="table table-hover table-inverse"
                        Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="numero_producto"
                        Style="text-align: left" OnRowEditing="gvLista_RowEditing">
                        <Columns>
                            <asp:CommandField ButtonType="Image" EditImageUrl="~/Imagenes/gr_info.jpg" ShowEditButton="True" />
                            <asp:BoundField HeaderText="Nro. Crédito" DataField="numero_producto" />
                            <asp:BoundField HeaderText="Estado" DataField="estado">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Descripción" DataField="linea" />
                            <asp:BoundField HeaderText="Monto Aprobado" DataField="monto" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Fecha Aprobada" DataField="fechaapertura" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Plazo" DataField="Plazo">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Cuotas Pagadas" DataField="CuotasPagadas">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Cuota" DataField="valorcuota" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Saldo" DataField="saldo" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Fec. Prox Pago" DataField="fechaproximopago" DataFormatString="{0:d}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Valor a Pagar" DataField="valorapagar" DataFormatString="{0:n}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
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
                    </asp:GridView>
                </div>
            </asp:Panel>
            <div style="text-align: center; width: 100%;">
                <asp:Label ID="lblTotReg" runat="server" Visible="false" />
                <asp:Label ID="lblInfo" runat="server" Visible="false" Text="Su consulta no obtuvo ningún resultado." />
            </div>
        </div>
    </div>
</asp:Content>
