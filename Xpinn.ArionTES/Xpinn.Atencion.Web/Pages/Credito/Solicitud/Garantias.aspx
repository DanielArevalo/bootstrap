<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Garantias.aspx.cs" Inherits="Garantias" %>

<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="garantias" class="resumen">
                            <h3 class="text-primary">Créditos</h3>
                            <asp:Panel ID="panelGarantias" runat="server">
                                <div style="overflow: scroll; width: 100%; max-height: 350px">
                                    <asp:GridView ID="gvGarantias" runat="server" AutoGenerateColumns="False"
                                        RowStyle-CssClass="table table-hover table-striped" CssClass="table" GridLines="Horizontal" OnDataBound="gvGarantias_DataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Requerido" HeaderStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkRequerido" runat="server"
                                                            Checked='<%#Convert.ToBoolean(Eval("requerido")) %>'
                                                            Enabled='<%#!Convert.ToBoolean(Eval("requerido")) %>'/>
                                                    </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>                                                                                                                       
                                            <asp:BoundField HeaderText="tipo_documento" DataField="tipo_documento" Visible="false" />
                                            <asp:BoundField HeaderText="Documento" DataField="descripcion"/>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="center" HeaderText="Formato">
                                                <ItemTemplate>
                                                        <a id="download" class="btn btn-default navbar-btn" href="<%# Eval("Garantia_Requerida") %>" download="<%# Eval("descripcion") %>.pdf">Descargar</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                <br />                                                              
                            </asp:Panel>
                        </div>
    <hr />
    <asp:Panel runat="server" ID="pnlDistribucion" Visible="false">
        <div id="distribucion" class="resumen">
                            <h3 class="text-primary">Distribución de Giros</h3>
                            <asp:Panel ID="panel1" runat="server">
                                    <asp:GridView ID="gvGiros" runat="server" AutoGenerateColumns="False"
                                        RowStyle-CssClass="table table-hover table-striped" CssClass="table" GridLines="Horizontal">
                                        <Columns>
                                            <asp:BoundField HeaderText="Identificación" DataField="identificacion" />
                                            <asp:BoundField HeaderText="Nombre" DataField="nombre" />
                                            <asp:BoundField HeaderText="Valor" DataField="valor" />
                                        </Columns>
                                        <RowStyle CssClass="table table-hover table-striped" />
                                    </asp:GridView>
                                <br />                                
                            </asp:Panel>
                        </div>        
    </asp:Panel>        
</asp:Content>

