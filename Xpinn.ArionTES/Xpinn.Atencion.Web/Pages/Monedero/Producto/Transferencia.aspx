<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" enableEventValidation="true" AutoEventWireup="true" CodeFile="Transferencia.aspx.cs" Inherits="Transferencia" %>

<script runat="server">    
</script>


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
              <asp:AsyncPostBackTrigger controlid="txtIddestino"
                    eventname="TextChanged" />
            </Triggers>
            <ContentTemplate>
    <asp:Panel ID="pnlTran" runat="server" CssClass="panelseccion">        
        <div style="text-align:center;font-weight: bold;font-size: 2rem;">
              Transacción
        </div>
        <hr style="border: .3px solid #b0b1b3;margin: 5px 20px;" />
        <br />
        <div class="container">                      
          <div class="row">
            <div class="col-sm-6 col-md-1">
              Nombre:
            </div>
            <div class="col-sm-6 col-md-3">
              <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control inSeccion" ReadOnly="true"></asp:TextBox>
            </div>
              <div class="col-sm-6 col-md-1">
                  &nbsp
              </div>
            <div class="col-sm-6 col-md-1">
              Disponible:
            </div>              
            <div class="col-sm-6 col-md-3">
              <asp:TextBox runat="server" ID="txtSaldo" CssClass="form-control inSeccion" ReadOnly="true"></asp:TextBox>
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
        <br />
        <br />
        <div style="text-align:center;font-weight: bold;font-size: 2rem;">
              Enviar a
        </div>
        <hr style="border:.5px solid #b0b1b3; margin: 5px 20px;" />
        <br />
        <div class="container">                      
          <div class="row">
            <div class="col-sm-6 col-md-1">
              Identificacion:
            </div>
            <div class="col-sm-6 col-md-3">
              <asp:TextBox runat="server" ID="txtIddestino" CssClass="form-control inSeccion" OnTextChanged="txtIddestino_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>    
              <div class="col-sm-6 col-md-1">
                  &nbsp
              </div>
            <div class="col-sm-6 col-md-1">
              Nombre:
            </div>
            <div class="col-sm-6 col-md-3">
              <asp:TextBox runat="server" ID="txtNomDestino" CssClass="form-control inSeccion" ReadOnly="true"></asp:TextBox>
            </div>
          </div>
            <br />
          <div class="row">
            <div class="col-sm-6 col-md-1">
              Valor:
            </div>
            <div class="col-sm-6 col-md-3">
              <asp:TextBox runat="server" ID="txtValorCarga" onkeypress="return ValidaNum(event);" onblur="valorCambio(event)" CssClass="form-control inSeccion" AutoPostBack="true" OnTextChanged="txtValorCarga_TextChanged"></asp:TextBox>
            </div>            
            <div class="col-sm-6 col-md-1">
              &nbsp
            </div>
            <div class="col-sm-6 col-md-3">
                &nbsp
            </div>
          </div>            
          <div class="row" style="display:none;">
            <div class="">
              Código:
            </div>
            <div class="">
              <asp:TextBox runat="server" ID="txtCodDestino" CssClass="form-control inSeccion" ReadOnly="true"></asp:TextBox>
            </div>            
          </div>
        </div>
                <br />
                <div>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                </div>        

        <div class="container" style="width:100%;">            
            <br />     
            <br />
            <div class="row fa-align-center text-center" runat="server" id="divbtns">
                <div class="col-sm-12 col-md-12">
                  <asp:Button runat="server" ID="btnCargar" Text="         Enviar          "  CssClass="btn-success btn-success-2" OnClick="btnCargar_Click" Enabled="false" />
                  <asp:Button runat="server" ID="btnVolverMon" Text="         Cancelar          "  CssClass="btn-danger btn-success-2" OnClick="btnVolverMon_Click" />
                </div>
            </div>
        </div>                
    </asp:Panel>
    <br />        
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
                            La transacción se realizó exitosamente
                            <br />
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

