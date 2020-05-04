<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Monedero.aspx.cs" Inherits="Monedero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .lst-cards{
            display: flex;
            flex-wrap: wrap;
        }
        .card-ope{              
            padding: 2%;
        }

        .card-ope > div{ 
           background-color: white;
           margin: 0 auto;
           text-align: center;
           padding: 10%;
           border-radius: 5px;
        }

        .card-ope > div > i{ 
           font-size: 8rem;
        }

    </style>

        <script type="text/javascript">

        function EvitarClickeoLocos() {
            if (contadorClickGuardar == 0) {
                contadorClickGuardar += 1;
                return true;
            }
            return false;
        }

        var contadorClickGuardar = 0;
        $(document).ready(function () {
            $("#btnPse").click(EvitarClickeoLocos);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>    
    <asp:UpdatePanel runat="server" ID="updTransferencia" UpdateMode="Conditional">
            <Triggers>
              <asp:AsyncPostBackTrigger controlid="btnRecargar"
                    eventname="click" />
            </Triggers>
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                </div>
    <asp:Panel ID="pnlApertura" runat="server" Visible="false" CssClass="panelseccion">
        <asp:Button runat="server" ID="btnAbrir" Text="Abrir monedero" CssClass="btn-success btn-success-1" OnClick="btnAbrir_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false">
        <span class="glyphicon glyphicon-alert text-green"></span> &nbsp;&nbsp;&nbsp; <asp:Label ID="lblContent" runat="server" CssClass="text-green"></asp:Label>
        <br /><br />
    </asp:Panel>
    <asp:Panel ID="pnlMonedero" runat="server" Visible="false" CssClass="panelseccion">
        <div class="container">
          <div class="row">
            <div class="col-sm-6 col-md-1">
              Nombre:
            </div>
            <div class="col-sm-6 col-md-3">
              <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control inSeccion" ReadOnly="true"></asp:TextBox>
            </div>            
            <div class="col-sm-6 col-md-1">
              Saldo:
            </div>
            <div class="col-sm-6 col-md-2">
              <asp:TextBox runat="server" ID="txtSaldo" CssClass="form-control inSeccion" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-sm-12 col-md-2">
                <asp:Button runat="server" ID="btnRecargar" Text="Recargar cuenta"  CssClass="btn-success btn-success-2" OnClick="btnRecargar_Click" />
                <asp:LinkButton ID="btnHistory" style="background-color: white;color: #5cb85c;" runat="server" OnClick="btnHistory_Click" CssClass="btn btn-success"><i class="fa fa-clock-o"></i></asp:LinkButton>
            </div>
          </div>
          <div class="row" style="display:none;">
            <div class="">
              Código:
            </div>
            <div class="">
              <asp:TextBox runat="server" ID="txtId" CssClass="form-control inSeccion" ReadOnly="true"></asp:TextBox>
            </div>            
          </div>
        </div>
    </asp:Panel>
                <br />
    <asp:Panel ID="pnlHistorico" runat="server" Visible="false" CssClass="panelseccion">
        <asp:GridView ID="gvHistory" runat="server" AutoGenerateColumns="False" CssClass="table"
            GridLines="Horizontal" RowStyle-CssClass="table">
            <Columns>
                <asp:BoundField HeaderText="Referencia" DataField="num_tran">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>                
                <asp:BoundField HeaderText="Valor" DataField="valor" Visible="true" DataFormatString="{0:c0}">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>                
                <asp:BoundField HeaderText="Fecha" DataField="fecha" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" Visible="true" />
                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Descripción" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lbltipo" runat="server" Text='<%# Eval("tipo_tran").ToString() == "1" ? "Carga desde: " + Eval("descripcion").ToString() : "Pago: " + Eval("descripcion").ToString() %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button runat="server" ID="btnVolverMon2" Text="         Volver          "  CssClass="btn-danger btn-success-2" OnClick="btnVolverMon_Click" />
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlOperaciones" runat="server" Visible="false" CssClass="panelseccion">
        <div class="row lst-cards">
            <div class="col-md-3 col-sm-6 card-ope" runat="server" id="card_recargas" visible="false">
                <div style="border: 1px solid dodgerblue;">
                    <i class="fa fa-mobile" aria-hidden="true"></i>
                    <h4 class="card-title">Recargas</h4>
                    <p class="card-text"><small class="text-muted">Compra recargas y paquetes para tu móvil</small></p>
                    <asp:Button runat="server" ID="btnRecarga" Text="     ir     " CssClass="btn btn-info" OnClick="btnRecarga_Click" />                    
                </div>
            </div>
            <div class="col-md-3 col-sm-6 card-ope" runat="server" id="card_transferencias" visible="false">
                <div style="border: 1px solid orange;">
                    <i class="fa fa-share" aria-hidden="true"></i>
                    <h4 class="card-title">Transferencias</h4>
                    <p class="card-text"><small class="text-muted">Realiza transferencias a otros asociados</small></p>
                    <asp:Button runat="server" ID="btnTransferencia" Text="     ir     " CssClass="btn btn-info" OnClick="btnTransferencia_Click" />
                </div>
            </div>            
            <div class="col-md-3 col-sm-6 card-ope" runat="server" id="card_prueba" visible="false">
                <div style="border: 1px solid orange;">
                    <i class="fa fa-share" aria-hidden="true"></i>
                    <h4 class="card-title">Transferencias</h4>
                    <p class="card-text"><small class="text-muted">Realiza transferencias a otros asociados</small></p>
                    <asp:Button runat="server" ID="Button2" Text="     ir     " CssClass="btn btn-info" />
                </div>
            </div>            
        </div>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlCargar" runat="server" Visible="false" CssClass="panelseccion">
        <div class="container" style="width:100%;">            
          <div class="row">            
              <div class="col-sm-12 col-md-2">
                  &nbsp
                  </div>
              <div class="col-sm-6 col-md-1">
              Origen:
            </div>
            <div class="col-sm-6 col-md-3">
                <asp:DropDownList runat="server" ID="ddlOrigen" CssClass="form-control inSeccion" AutoPostBack="true" OnSelectedIndexChanged="ddlOrigen_SelectedIndexChanged">
                </asp:DropDownList>
            </div>                                   
                <div class="col-sm-6 col-md-1">
                  Valor:
                </div>
                <div class="col-sm-6 col-md-3">
                  <asp:TextBox runat="server" ID="txtValorCarga" onkeypress="return ValidaNum(event);" onblur="valorCambio(event)" CssClass="form-control inSeccion"></asp:TextBox>
                </div>  
              </div>
            <br />
            <div runat="server" class="row"  id="divDisponible" visible="false">
              <div>
              <div class="col-sm-12 col-md-2">
              &nbsp
              </div>
              <div class="col-sm-6 col-md-1">
              Número producto
              </div>
              <div class="col-sm-6 col-md-3">
                <asp:TextBox runat="server" ID="txtNumProducto" CssClass="form-control inSeccion" ReadOnly="true"></asp:TextBox>
              </div>                                   
              <div class="col-sm-6 col-md-1">
                disponible:
              </div>
            <div class="col-sm-6 col-md-3">
                <asp:TextBox runat="server" ID="txtDisponible" CssClass="form-control inSeccion" ReadOnly="true"></asp:TextBox>
            </div>                         
            </div>
            </div>
            <div runat="server" id="divDatosOcultos" visible="false">
              <div>
              <div class="col-sm-12 col-md-2">
              &nbsp
              </div>
              <div class="col-sm-6 col-md-1">
              TipoProducto
              </div>
              <div class="col-sm-6 col-md-3">
                <asp:TextBox runat="server" ID="txtTipoProducto" CssClass="form-control inSeccion" ReadOnly="true"></asp:TextBox>
              </div>                                   
              <div class="col-sm-6 col-md-1">
                &nbsp
              </div>
            <div class="col-sm-6 col-md-3">
                &nbsp
            </div>                         
            </div>
            </div>
            <div class="row fa-align-center text-center" runat="server" id="divPSE" visible="false">
                <div class="col-sm-12 col-md-12" style="display: flex;align-items: center;justify-content: center;">
                    <asp:ImageButton runat="server" ID="btnPse" ClientIDMode="Static" ImageUrl="~/Imagenes/LogoPSE.png" Width="70px" Height="55px" OnClick="btnPse_Click" />
                    <asp:Button runat="server" ID="Button3" Text="         Cancelar          "  CssClass="btn-danger btn-success-2" OnClick="btnVolverMon_Click" />                    
                </div>
            </div>
            <br />     
            <br />
            <div class="row fa-align-center text-center" runat="server" id="divbtns" visible="true">
                <div class="col-sm-12 col-md-12">
                  <asp:Button runat="server" ID="btnCargar" Text="Recargar monedero"  CssClass="btn-success btn-success-2" OnClick="btnCargar_Click" />
                  <asp:Button runat="server" ID="btnVolverMon" Text="         Cancelar          "  CssClass="btn-danger btn-success-2" OnClick="btnVolverMon_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="panelFinal" runat="server" Visible="false" CssClass="panelseccion">
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
                            Para mayor información comuníquese con nosotros o acérquese a alguna de nuestras oficinas.
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
                </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

