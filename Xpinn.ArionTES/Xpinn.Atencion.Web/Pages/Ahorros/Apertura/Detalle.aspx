<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Detalle.aspx.cs" Inherits="Pages_Ahorros_Apertura_Detalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Xpinn.Util" Namespace="Xpinn.Util" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <style type="text/css">

        .thcenter {
            text-align: center;
        }

        .paddingCero {
            padding: 1px;
        }
        .tableNormal {
            border-collapse: separate;
            border-spacing: 4px;
        }

        .btn-circle, .btn-circle-3d {
            border-radius: 50% !important;
        }

        .btn-primary.btn-circle {
            -webkit-box-shadow: 0px 0px 3px 1px #245580;
            -moz-box-shadow: 0px 0px 3px 1px #245580;
            box-shadow: 0px 0px 3px 1px #245580;
        }

        .btn-info.btn-circle {
            -webkit-box-shadow: 0px 0px 3px 1px #28a4c9;
            -moz-box-shadow: 0px 0px 3px 1px #28a4c9;
            box-shadow: 0px 0px 3px 1px #28a4c9;
        }

        .btn-success.btn-circle {
            -webkit-box-shadow: 0px 0px 3px 1px #3e8f3e;
            -moz-box-shadow: 0px 0px 3px 1px #3e8f3e;
            box-shadow: 0px 0px 3px 1px #3e8f3e;
        }

        .btn-danger.btn-circle {
            -webkit-box-shadow: 0px 0px 3px 1px #b92c28;
            -moz-box-shadow: 0px 0px 3px 1px #b92c28;
            box-shadow: 0px 0px 3px 1px #b92c28;
        }

        .btn-warning.btn-circle {
            -webkit-box-shadow: 0px 0px 3px 1px #e38d13;
            -moz-box-shadow: 0px 0px 3px 1px #e38d13;
            box-shadow: 0px 0px 3px 1px #e38d13;
        }

        .btn-default.btn-circle {
            -webkit-box-shadow: 0px 0px 3px 1px #ccc;
            -moz-box-shadow: 0px 0px 3px 1px #ccc;
            box-shadow: 0px 0px 3px 1px #ccc;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="../../../Scripts/PCLBryan.js"></script>    

    <div class="form-group">
        <asp:Panel ID="panelGeneral" runat="server">
            <asp:FormView ID="frvData" runat="server" Width="100%" visible="false">
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
            <asp:Label ID="lblTituloAhorroVista" runat="server" Visible="false"/>
            <div class="form-group">
                <ul class="nav nav-tabs">
                   
                    <li id="tabAhoVista" class="active"><a data-toggle="tab" href="#ahorros"><span id="tabVista">Ahorros Vista</span></a></li>
      
                    <li id="tabProgramado"><a data-toggle="tab" href="#programado">Ahorro Programado</a></li>
                   
                    <li id="tabCdat"><a data-toggle="tab" href="#cdats">CDATS</a></li>
                </ul>
                <div style="background-color: #FFFFFF">
                    <div class="tab-content wi">
                     
                     
                        <div id="ahorros" class="tab-pane fade">
                            <h3 class="text-primary"><span id="spnTitleVista">Ahorros Vista</span></h3>
                            <asp:Panel ID="panelAhorros" runat="server">
                                <div style="overflow: scroll; width: 100%; max-height: 550px">
                                    <asp:GridView ID="gvAhorros" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                                        GridLines="Horizontal" CssClass="table"
                                        ShowHeaderWhenEmpty="True" Width="100%">
                                        <Columns>
                                            <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPagar" runat="server" CssClass="btn btn-sm btn-primary btn-circle" CommandName="Edit"
                                                        ToolTip="Realizar Pago"><div style="color:white">
                                                        <span class="fa fa-money"></div></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="IDSOLICITUD" HeaderText="Num. Solicitud">
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nom_Linea" HeaderText="Nom. Linea">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                         <%--   <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="FECHA" DataFormatString="{0:d}" HeaderText="Fec. Apertura">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                       <%--     <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>--%>
                                            <%--<asp:BoundField DataField="saldo_canje" HeaderText="Saldo en Canje" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="ESTADOS" HeaderText="Estado">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FORMA_PAGO" HeaderText="Forma Pago">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VALOR_CUOTA" HeaderText="Valor Cuota" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                          <%--  <asp:BoundField DataField="fecha_proximo_pago" DataFormatString="{0:d}" HeaderText="F. Próx. Pago">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                              <asp:BoundField HeaderText="Rendimientos Causados" DataField="valor_acumulado" DataFormatString="{0:n}" >
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                             <asp:BoundField HeaderText="Valor + Rendim.Causados" DataField="valor_total_acumu"   DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>--%>

                                         <%--   <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Nombre">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBeneficiario" runat="server" Text='<%# Eval("nombres") %>' Width="160px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Tipo" DataField="tipo_registro" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>--%>

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
                                        <div class="col-sm-3">
                                            Valor Total Canje<br />
                                            <asp:TextBox ID="txtTotalCanjeAhorros" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>

                                        <div class="col-sm-3">
                                            Valor Total Rend. Causados<br />
                                            <asp:TextBox ID="txtTotalCausacionAhorros" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>

                                           <div class="col-sm-3">
                                            Total Saldos + Rendimientos Causados<br />
                                            <asp:TextBox ID="txtTotalAhmasRendCausados" runat="server" CssClass="form-control text-center"
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
                                    <asp:GridView ID="gvAhoProgra" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowPaging="false" CssClass="table" GridLines="Horizontal" DataKeyNames="IDSOLICITUD">
                                        <Columns>
                                             <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPagar" runat="server" CssClass="btn btn-sm btn-primary btn-circle" CommandName="Edit"
                                                        ToolTip="Realizar Pago"><div style="color:white">
                                                        <span class="fa fa-money"></div></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="IDSOLICITUD" HeaderText="Num. Solicitud">
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nom_Linea" HeaderText="Nom. Linea">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                         <%--   <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="FECHA" DataFormatString="{0:d}" HeaderText="Fec. Apertura">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                       <%--     <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>--%>
                                            <%--<asp:BoundField DataField="saldo_canje" HeaderText="Saldo en Canje" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="ESTADOS" HeaderText="Estado">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FORMA_PAGO" HeaderText="Forma Pago">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VALOR_CUOTA" HeaderText="Valor Cuota" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                          <%--  <asp:BoundField DataField="fecha_proximo_pago" DataFormatString="{0:d}" HeaderText="F. Próx. Pago">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                              <asp:BoundField HeaderText="Rendimientos Causados" DataField="valor_acumulado" DataFormatString="{0:n}" >
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                             <asp:BoundField HeaderText="Valor + Rendim.Causados" DataField="valor_total_acumu"   DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>--%>

                                         <%--   <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Nombre">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBeneficiario" runat="server" Text='<%# Eval("nombres") %>' Width="160px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Tipo" DataField="tipo_registro" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <table style="width: 100%">
                                <tr>
                                    <td class="col-sm-12 text-center">
                                        <asp:Label ID="lblTotRegProg" runat="server" Visible="false" />
                                        <asp:Label ID="lblInfoProg" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                          
                                 <div class="col-lg-10 col-sm-12">
                                        <div class="col-sm-4">
                                            Total Saldos <br />
                                            <asp:TextBox ID="txtTotalAhoProgramado" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                        <div class="col-sm-4">
                                            Total Cuotas <br />
                                            <asp:TextBox ID="txtTotalCuotasAhoProgra" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>

                                     <div class="col-sm-4">
                                            Total Rendimientos Causados<br />
                                            <asp:TextBox ID="txtTotalCausacionProgramado" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                       
                                          <div class="col-sm-3">
                                            Total Saldos + Rendim. Causados<br />
                                            <asp:TextBox ID="txtTotalProgmasRendCausados" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                    </div>

                        </div>
                             <div id="cdats" class="tab-pane fade">
                            <h3 class="text-primary">CDATS</h3>
                             <asp:Panel ID="panelCdat" runat="server">
                                <div style="overflow: scroll; width: 100%; max-height: 550px">
                                    <asp:GridView ID="gvCDATS" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowPaging="false" DataKeyNames="IDSOLICITUD" GridLines="Horizontal" CssClass="table">
                                        <Columns>
                                            <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPagar" runat="server" CssClass="btn btn-sm btn-primary btn-circle" CommandName="Edit"
                                                        ToolTip="Realizar Pago"><div style="color:white">
                                                        <span class="fa fa-money"></div></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="IDSOLICITUD" HeaderText="Num. Solicitud">
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nom_Linea" HeaderText="Nom. Linea">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                         <%--   <asp:BoundField DataField="nom_oficina" HeaderText="Oficina">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="FECHA" DataFormatString="{0:d}" HeaderText="Fec. Apertura">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                       <%--     <asp:BoundField DataField="saldo_total" HeaderText="Saldo Total" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>--%>
                                            <%--<asp:BoundField DataField="saldo_canje" HeaderText="Saldo en Canje" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="ESTADOS" HeaderText="Estado">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FORMA_PAGO" HeaderText="Forma Pago">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VALOR_CUOTA" HeaderText="Valor Cuota" DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                          <%--  <asp:BoundField DataField="fecha_proximo_pago" DataFormatString="{0:d}" HeaderText="F. Próx. Pago">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                              <asp:BoundField HeaderText="Rendimientos Causados" DataField="valor_acumulado" DataFormatString="{0:n}" >
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>

                                             <asp:BoundField HeaderText="Valor + Rendim.Causados" DataField="valor_total_acumu"   DataFormatString="{0:n}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>--%>

                                         <%--   <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Nombre">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBeneficiario" runat="server" Text='<%# Eval("nombres") %>' Width="160px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Tipo" DataField="tipo_registro" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <table style="width: 100%">
                                <tr>
                                    <td class="col-sm-12 text-center">
                                        <asp:Label ID="lblTotRegCdat" runat="server" Visible="false" />
                                        <asp:Label ID="lblInfoCdat" runat="server" Text="Su consulta no obtuvo ningún resultado" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                            <div class="form-group col-sm-12">
                                    <div class="col-lg-1">
                                        &nbsp;
                                    </div>
                                    <div class="col-lg-10 col-sm-12">
                                        <div class="col-sm-4">
                                            Total Saldos<br />
                                            <asp:TextBox ID="txtTotalCdats" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                         <div class="col-sm-4">
                                            Total Rendimientos Causados<br />
                                            <asp:TextBox ID="txtTotalCausacionCdat" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>

                                          <div class="col-sm-4">
                                              Total Saldos + Rendimientos Causados<br />
                                            <asp:TextBox ID="txtTotalCdatRendCausados" runat="server" CssClass="form-control text-center"
                                                Enabled="False" Width="90%" />
                                        </div>
                                    </div>
                                    <div class="col-lg-1">
                                        &nbsp;
                                    </div>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
       
    </div>

</asp:Content>

