<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Detalle.aspx.cs" Inherits="Detalle" %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc3" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="ctlFecha" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tableNormal {
            border-collapse: separate;
            border-spacing: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../../Scripts/PCLBryan.js"></script>
    <div class="form-group">
        <asp:Panel ID="panelGeneral" runat="server">
            <asp:Label ID="txtCodPersona" runat="server" visible="false" />
            <asp:Label ID="txtCambio" runat="server" visible="false" />
            <asp:FormView ID="frvData" runat="server" Width="100%" OnDataBound="frvData_DataBound" visible="false">
                <ItemTemplate>
                    <table class="col-sm-12 tableNormal">
                        <tr>
                            <td>
                                <h5 class="text-primary text-left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Información del Asociado</h5>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left">
                                        Identificación
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="txtIdentificacion" runat="server" Text='<%# Eval("NumeroDocumento") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        Nombre
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:Label ID="txtNombre" runat="server" Text='<%# Eval("PrimerNombre") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left">
                                        Ciudad
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="txtCiudad" runat="server" Text='<%# Eval("SegundoNombre") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        Dirección
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:Label ID="txtDireccion" runat="server" Text='<%# Eval("Direccion") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="col-sm-12">
                                    <div class="col-sm-2 text-left">
                                        Fecha Afiliación
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Label ID="lblFechaAfiliacion" runat="server" Text='<%# Eval("FechaAfiliacion", "{0:d}") %>' />
                                    </div>
                                    <div class="col-sm-2 text-left">
                                        Tipo Cliente
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lblTipoCliente" runat="server" Text='<%# Eval("TipoCliente") %>' />
                                        <asp:Label ID="lblEmail" Visible="false" runat="server" Text='<%# Eval("Email") %>' />
                                        <asp:Label ID="lblMotivo" Visible="false" runat="server" Text='<%# Eval("nommotivo") %>' />
                                        <asp:Label ID="Ciudad" Visible="false" runat="server" Text='<%# Eval("TipoCliente") %>' />
                                        <asp:Label ID="lblEstado" Visible="false" runat="server" Text='<%# Eval("TipoCliente") %>' />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr style="border-color: #2780e3;" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>

            <asp:Panel ID="panelAporte" runat="server">
                <div class="form-group">
                    <ul class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#aportes">Aportes Sociales</a></li>
                        <li id="tabAhoVista"><a data-toggle="tab" href="#ahorros"><span id="tabVista">Ahorros Vista</span></a></li>
                        <li id="tabProgramado"><a data-toggle="tab" href="#programado">Ahorro Programado</a></li>
                    </ul>
                    <div style="background-color: #FFFFFF">
                        <div class="tab-content wi">
                            <div id="aportes" class="tab-pane fade in active">
                                <h3 class="text-primary">Aportes Sociales</h3>
                                <div style="overflow: scroll; width: 100%; max-height: 550px;">
                                    <asp:GridView ID="gvAportes" runat="server" AutoGenerateColumns="False" CssClass="table"
                                        GridLines="Horizontal" RowStyle-CssClass="table" OnRowEditing="gvAportes_RowEditing"
                                        DataKeyNames="Saldo,cuota,valor_a_pagar">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="Modificar Cuota" EditImageUrl="~/Imagenes/gr_edit.jpg" ShowEditButton="True" />
                                            <asp:BoundField HeaderText="Nro. Aporte" DataField="numero_aporte" />
                                            <asp:BoundField HeaderText="Linea" DataField="cod_linea_aporte" />
                                            <asp:BoundField HeaderText="Descripción" DataField="nom_linea_aporte" />
                                            <asp:BoundField HeaderText="Cuota" DataField="cuota" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Saldo" DataField="Saldo" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Fec. Prox Pago" DataField="fecha_proximo_pago" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Valor a Pagar" DataField="valor_a_pagar" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Fec. Ult Modificacion" DataField="fecha_ultima_mod" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Estado Modificación" DataField="estado_modificacion">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br />
                                <center>
                                    <asp:Label ID="lblMsj1" runat="server" CssClass="text-center" ForeColor="Red" /></center>
                                <div class="form-group col-sm-12">
                                    <div class="col-lg-1">
                                        &nbsp;
                                    </div>
                                    <div class="col-lg-10 col-sm-12">
                                        <div class="col-sm-4">
                                            Total Saldos<br />
                                            <asp:TextBox ID="txtTotalAportes" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                        <div class="col-sm-4">
                                            Total Cuotas Aportes<br />
                                            <asp:TextBox ID="txtTotalCuotasAportes" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                        <div class="col-sm-4">
                                            Valor Pendiente a Pagar<br />
                                            <asp:TextBox ID="txtAPortesPendientesporPagar" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                    </div>
                                    <div class="col-lg-1">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <div id="ahorros" class="tab-pane fade">
                            <h3 class="text-primary"><span id="spnTitleVista">Ahorros Vista</span></h3>
                            <asp:Panel ID="panelAhorros" runat="server">
                                <div style="overflow: scroll; width: 100%; max-height: 550px">
                                    <asp:GridView ID="gvAhorros" runat="server" AutoGenerateColumns="False" CssClass="table"
                                        GridLines="Horizontal" RowStyle-CssClass="table" OnRowEditing="gvAhorros_RowEditing"
                                        DataKeyNames="saldo_total,valor_cuota,saldo_canje,valor_acumulado,numero_cuenta, tipo_registro">
                                        <Columns>

                                            <asp:CommandField ButtonType="Image" HeaderText="Modificar Cuota" EditImageUrl="~/Imagenes/gr_edit.jpg" ShowEditButton="True" />
                                            <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPagar" runat="server" CssClass="btn btn-sm btn-primary btn-circle" CommandName="Edit"
                                                        ToolTip="Realizar Pago"><div style="color:white">
                                                        <span class="fa fa-money"></div></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="numero_cuenta" HeaderText="Num. Cuenta">
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_linea" HeaderText="Nom. Linea">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_apertura" DataFormatString="{0:d}" HeaderText="Fec. Apertura">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo_canje" HeaderText="Saldo en Canje" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_formapago" HeaderText="Forma Pago">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valor_cuota" HeaderText="Valor Cuota" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_proximo_pago" DataFormatString="{0:d}" HeaderText="F. Próx. Pago">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                              <asp:BoundField HeaderText="Rendimientos Causados" DataField="valor_acumulado" DataFormatString="{0:n}" >
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                             <asp:BoundField HeaderText="Valor + Rendim.Causados" DataField="valor_total_acumu"   DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Nombre">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBeneficiario" runat="server" Text='<%# Eval("nombres") %>' Width="160px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Tipo" DataField="tipo_registro" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                                 
                                <div class="form-group col-sm-12">
                                    <div class="col-lg-1">
                                        &nbsp;
                                    </div>
                                    <div class="col-lg-10 col-sm-12">
                                        <div class="col-sm-3">
                                            Total Saldos<br />
                                            <asp:TextBox ID="txtTotalAhorros" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                        <div class="col-sm-3">
                                            Total Cuotas Ahorros<br />
                                            <asp:TextBox ID="txtTotalCuotasAhorros" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>

                                    </div>
                                    <div class="col-lg-1">
                                        &nbsp;
                                    </div>
                                </div>

                            </asp:Panel>
                            <table style="width: 100%">
                                <tr>
                                    <td class="col-sm-12 text-center">
                                        <asp:Label ID="lblTotRegAhorro" runat="server" Visible="false" />
                                        <asp:Label ID="lblInfoAhorro" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                             <table>

                      
                    </table>
                        </div>
                            <div id="programado" class="tab-pane fade">
                            <h3 class="text-primary">Ahorro Programado</h3>
                            <asp:Panel ID="panelProgra" runat="server">
                                <div style="overflow: scroll; width: 100%; max-height: 550px">
                                    <asp:GridView ID="gvAhoProgra" runat="server" AutoGenerateColumns="False" CssClass="table"
                                        GridLines="Horizontal" RowStyle-CssClass="table" OnRowEditing="gvAhoProgra_RowEditing">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="Modificar Cuota" EditImageUrl="~/Imagenes/gr_edit.jpg" ShowEditButton="True" />
                                            <%--<asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnInfo" runat="server" CommandName="Select" ImageUrl="~/Images/gr_general.jpg"
                                                        ToolTip="Detalle" Width="16px" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="gridIco" />
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="numero_programado" HeaderText="Num Cuenta">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomlinea" HeaderText="Línea">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>    
                                                 <asp:BoundField DataField="plazo" HeaderText="Plazo">
                                                <HeaderStyle Width="10px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>                                       
                                            <asp:BoundField DataField="nomoficina" HeaderText="Oficina">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_apertura" HeaderText="F. Apertura" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nom_estado" HeaderText="Estado">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="saldo" HeaderText="Saldo Total" DataFormatString="{0:c}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_ultimo_pago" HeaderText="F. Ult Mov" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_proximo_pago" HeaderText="F. Prox Pago" DataFormatString="{0:d}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nomforma_pago" HeaderText="Forma Pago">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_vencimiento" HeaderText="F.Vencimie." DataFormatString="{0:d}">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>                                                                                                                                
                                            <asp:BoundField DataField="valor_cuota" HeaderText="Cuota" DataFormatString="{0:c}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="nom_periodicidad" HeaderText="Periodicidad">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="cuotas_pagadas" HeaderText="Cuotas Pagas">
                                                <HeaderStyle Width="10px" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tasa_interes" HeaderText="Tasa Interés">
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>                                           
                                            
                                              <asp:BoundField HeaderText="Rendimientos Causados" DataField="valor_acumulado" DataFormatString="{0:n}" >
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                               <asp:BoundField HeaderText="Valor + Rendim.Causados" DataField="valor_total_acumu"   DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                          

                        </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div style="text-align: center">
                <asp:Panel ID="pnlCambioCuota" Visible="false" runat="server" BackColor="White" Style="text-align: left; margin: auto auto" Width="50%">
                    <div class="modal-content">
                        <div class="col-sm-12 container">
                            <br />
                            <center>
                    <asp:Image ID="Image1" runat="server" ImageUrl="../../../Imagenes/logoEmpresa.jpg" Width="40px"
                        Height="40px" /><b><h4 class="modal-title text-primary">
                            Solicitud Modificación</h4>
                        </b>
                </center>
                            <center>
                    <asp:Label runat="server" ID="lblError" Font-Size="Small" Visible="false" Font-Bold="true"  class="modal-title text-primary" style="color:red" ></asp:Label>
                    </center>
                            <hr style="width: 100%" />
                        </div>                        
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Num. Producto
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtNumeroProducto" Enabled="false" runat="server" CssClass="form-control" />
                                <br />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Descripción
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtDescripcion" Enabled="false" runat="server" CssClass="form-control" />
                                <br />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Ult. Modificación
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtUltimaMod" Enabled="false" runat="server" CssClass="form-control" />
                                <br />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Cuota
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtCuota" Enabled="false" runat="server" CssClass="form-control" />
                                <br />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Fecha Cambio
                            </div>
                            <div class="col-sm-8">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <ctl:ctlFecha runat="server" ID="txtFechaEmpiezaCambio" />
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Nuevo Valor Cuota
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtNuevoValorCuota" onkeypress="return isNumber(event)" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <br />
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Observaciones:
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox TextMode="MultiLine" ID="txtObservaciones" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <br />
                        </div>
                        <div class="modal-footer">
                            <br />
                            <asp:Button ID="btnGuardarCambioCuota" CssClass="btn btn-primary" Style="padding: 3px 15px; border-radius: 0px; width: 110px" OnClientClick="return controlClickeoLocosDesactivarBoton();" runat="server" Text="Guardar"
                                OnClick="btnGuardarCambioCuota_Click" />
                            &nbsp &nbsp


                <asp:Button ID="btnCerrarCambioCuota" CssClass="btn btn-primary" OnClick="btnCerrarCambioCuota_Click" runat="server" Text="Regresar"
                    Style="padding: 3px 15px; border-radius: 0px; width: 110px" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </asp:Panel>
    </div>
    <script type="text/javascript">
        var contador = 1;
        function controlClickeoLocosDesactivarBoton() {
            if (contador == 0) {
                return false;
            }

            contador = 0;
            return true;
        }
    </script>
</asp:Content>
