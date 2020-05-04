<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AhorroVista.aspx.cs" Inherits="AhorroVista" %>

<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>
<%@ Register Src="~/Controles/decimales.ascx" TagName="decimales" TagPrefix="uc3" %>
<%@ Register Src="~/Controles/fecha.ascx" TagName="ctlFecha" TagPrefix="ctl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tableNormal {
            border-collapse: separate;
            border-spacing: 4px;
        }

        .resumen{
            background-color: rgba(178, 190, 195,.4);
            padding: 10px;
            margin-bottom: 50px;
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

            <asp:Panel ID="panelAhorro" runat="server">
                <div id="ahorros" class="resumen">
                    <asp:Panel ID="panelAhorros" runat="server">
                        <div style="overflow: scroll; width: 100%; max-height: 350px">
                            <asp:GridView ID="gvAhorros" runat="server" AutoGenerateColumns="False" CssClass="table"
                                GridLines="Horizontal" RowStyle-CssClass="table" OnRowEditing="gvAhorros_RowEditing"
                                DataKeyNames="saldo_total,valor_cuota,saldo_canje,valor_acumulado,numero_cuenta, tipo_registro">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnModificar" runat="server" CssClass="btn btn-primary" style="margin:1px; padding: 0px 2px" CommandName="Edit"
                                                        ToolTip="Modificar cuota"><div style="color:white">
                                                        <span class="fa fa-pencil fa-lg"></div></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    <asp:BoundField DataField="numero_cuenta" HeaderText="Num. Cuenta">
                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_linea" HeaderText="Nombre">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Beneficiario" DataField="nombres_ben" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:c0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_estado" HeaderText="Estado" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nom_formapago" HeaderText="Forma Pago" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="valor_cuota" HeaderText="Cuota" DataFormatString="{0:C0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_proximo_pago" DataFormatString="{0:d}" HeaderText="Próximo Pago">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>                        
                    </asp:Panel>

                </div>
            </asp:Panel>
            <div style="text-align: center">
                <asp:Panel ID="pnlCambioCuota" Visible="false" runat="server" BackColor="White" Style="text-align: left; margin: auto auto" Width="50%">
                    <div class="modal-content">
                        <div class="col-sm-12 container">
                            <br />
                            <center><h4 class="modal-title text-primary">
                            Solicitud modificación</h4>
                        </b>
                </center>
                            <center>
                    <asp:Label runat="server" ID="lblError" Font-Size="Small" Visible="false" Font-Bold="true"  class="modal-title text-primary" style="color:red" ></asp:Label>
                    </center>
                            <hr style="width: 100%" />
                        </div>                        
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Num. producto
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
                                Cuota
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtCuota" Enabled="false" runat="server" CssClass="form-control" />
                                <br />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-left">
                                Fecha cambio
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
                                Nuevo valor cuota
                            </div>
                            <div class="col-sm-8">                                
                                <asp:TextBox ID="txtNuevoValorCuota" runat="server" CssClass="form-control" Width="100%" onkeypress="return ValidaNum(event);" onblur="valorCambio(event)"></asp:TextBox>
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
        <asp:Panel ID="panelFinal" runat="server" Visible="false">
            <div class="col-xs-12">
                <div class="col-lg-1">
                </div>
                <div class="col-lg-10 col-md-12 col-xs-12" style="margin-top: 27px">
                    <div class="col-xs-12">
                      <asp:Label ID="Label2" runat="server" Text="Tu solicitud se generó con el código: "                            
                            Style="color: #66757f; font-size: 28px;" />          
                        <asp:Label ID="lblCodigoGenerado" runat="server" Style="color: Red; font-size: 28px;" />          
                        <asp:Label ID="Label1" runat="server" Text="Si realizaste la solicitud hasta el día 30, el cambio quedará aplicado el próximo mes. "                            
                            Style="color: #66757f; font-size: 28px;" />                                                            
                    </div>
                    <div class="col-xs-12">
                        &nbsp;
                    </div>
                    <div class="col-xs-12">
                        &nbsp;
                    </div>
                    <div class="col-xs-12">
                        <asp:Button ID="btnVolver" CssClass="btn btn-primary" runat="server" Text="Volver" Style="padding: 3px 15px; width: 110px; margin: 0 auto" OnClick="btnCerrarCambioCuota_Click" />
                    </div>
                </div>
                <div class="col-lg-1">
                </div>
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
