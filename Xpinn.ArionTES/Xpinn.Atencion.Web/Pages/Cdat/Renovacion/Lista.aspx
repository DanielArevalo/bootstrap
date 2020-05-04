<%@ Page Title=".: Plan Pagos :." Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Lista.aspx.cs" Inherits="Lista" %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc3" %>
<%@ Register Src="~/Controles/mensajeGrabar.ascx" TagName="mensajeGrabar" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tableNormal {
            border-collapse: separate;
            border-spacing: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-group">
        <asp:Panel ID="panelGeneral" runat="server" Style="padding: 15px" Visible="false">
            <div class="col-sm-12">
                <asp:Panel ID="panelGrid" runat="server">
                    <div class="text-left">
                        <strong>Listado de Cdats a renovar</strong><br />
                        <asp:Label runat="server" Text="Si desea puede realizar modificaciones a su Cdat por vencer, los datos modificables son: La Línea y el plazo del Cdat a renovar"
                            Style="font-size: x-small; color: Green; font-weight: 700;" />
                    </div>
                    <div style="overflow: scroll; max-width: 100%;">
                        <br />
                        <asp:GridView ID="gvLista" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            GridLines="Horizontal" ShowHeaderWhenEmpty="True" CssClass="table table-hover table-inverse"
                            Width="100%" OnPageIndexChanging="gvLista_PageIndexChanging" DataKeyNames="codigo_cdat, numero_cdat"
                            Style="text-align: left">
                            <Columns>
                                <%--OnRowEditing="gvLista_RowEditing" <asp:CommandField ButtonType="Image" EditImageUrl="~/Imagenes/gr_info.jpg" ShowEditButton="True" />--%>
                                <asp:TemplateField HeaderText="Renovar">
                                    <ItemTemplate>
                                        <cc1:CheckBoxGrid ID="chkSeleccion" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="codigo_cdat" HeaderText="Código">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="numero_cdat" HeaderText="Num Cdat">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Oficina">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOficina" runat="server" Text='<%# Eval("nomoficina") %>' Width="110px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Apertura">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaAper" runat="server" Text='<%# Eval("fecha_apertura", "{0:dd/MM/yyyy}") %>' Width="100px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Inicio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFecIni" runat="server" Text='<%# Eval("fecha_inicio", "{0:dd/MM/yyyy}") %>' Width="100px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Vencimiento">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFecVenc" runat="server" Text='<%# Eval("fecha_vencimiento", "{0:dd/MM/yyyy}") %>' Width="100px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNomEstado" runat="server" Text='<%# Eval("nom_estado") %>' Width="150px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblValor" runat="server" Text='<%# Eval("valor", "{0:c}") %>' Width="140px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tasa Interes">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTasa" runat="server" Text='<%# Eval("tasa_interes") %>' Width="100px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Línea">
                                    <ItemTemplate>
                                        <cc1:DropDownListGrid ID="ddlLinea" runat="server" Width="160px" DataSource="<%# ListarLineas() %>" CssClass="form-control"
                                            DataTextField="descripcion" DataValueField="idconsecutivo" SelectedValue='<%# Bind("cod_lineacdat") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plazo">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPlazo" runat="server" CssClass="form-control text-right" Text='<%# Eval("plazo") %>' Width="50px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Observación">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control" Width="190px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
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
        </asp:Panel>
        <asp:Panel ID="panelFinal" runat="server">
            <div class="col-xs-12">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10 col-md-12 col-xs-12" style="margin-top: 27px">
                    <div class="col-xs-12">
                        <asp:Label ID="Label2" runat="server" Text="Su transacción se generó correctamente."
                            Style="color: #66757f; font-size: 28px;" />
                    </div>
                    <div class="col-xs-12">
                        <p style="margin-top: 36px">
                            La recordamos que una vez aprobada la transacción realizada, se verá reflejado los cambios en su Cdat. Para mayor información comuníquese con nosotros o acérquese a alguna de nuestras oficinas.
                        </p>
                    </div>
                    <div class="col-xs-12">
                        &nbsp;
                    </div>
                    <div class="col-xs-12">
                        &nbsp;
                    </div>
                    <div class="col-xs-12">
                        <asp:LinkButton ID="btnInicio" runat="server" CssClass="btn btn-primary" Width="170px" ToolTip="Home"
                            Style="border-radius: 0px; padding-left: 5px; padding-right: 5px; padding-top: 7px; padding-bottom: 7px" OnClick="btnInicio_Click">
                            <div class="pull-left" style="padding-left:10px">
                            <span class="fa fa-home"></span></div>&#160;&#160;Regresar al Inicio
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="col-lg-1">
                </div>
            </div>
        </asp:Panel>
    </div>

    <uc4:mensajeGrabar ID="ctlMensaje" runat="server" />

</asp:Content>
