<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlanPagos.ascx.cs" Inherits="cPlanPagos" %>

<asp:GridView ID="gvPlanPagos" runat="server" AllowPaging="True" AutoGenerateColumns="False" GridLines="Horizontal" 
    ShowHeaderWhenEmpty="True" style="font-size: small" Width="927px" onpageindexchanging="gvPlanPagos_PageIndexChanging">
    <Columns>
        <asp:BoundField DataField="numerocuota" HeaderText="No"  
            ItemStyle-HorizontalAlign="Left" >
        <ItemStyle HorizontalAlign="Left" Width="20px" />
        </asp:BoundField>
        <asp:BoundField DataField="fechacuota" HeaderText="Fecha"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:dd-MM-yyyy}" >
        <ItemStyle HorizontalAlign="Right" Width="10px" />
        </asp:BoundField>
        <asp:BoundField DataField="sal_ini" HeaderText="Saldo Inicial"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
        <ItemStyle HorizontalAlign="Right" Width="4px" />
        </asp:BoundField>
        <asp:BoundField DataField="capital" HeaderText="Capital"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_1" HeaderText="Int_1"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_2" HeaderText="Int_2"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_3" HeaderText="Int_3"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_4" HeaderText="Int_4"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_5" HeaderText="Int_5"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_6" HeaderText="Int_6"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_7" HeaderText="Int_7"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_8" HeaderText="Int_8"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_9" HeaderText="Int_9"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_10" HeaderText="Int_10"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_11" HeaderText="Int_11"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_12" HeaderText="Int_12"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_13" HeaderText="Int_13"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_14" HeaderText="Int_14"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="int_15" HeaderText="Int_15"  
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" Visible="false">
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:c}"  
            ItemStyle-HorizontalAlign="Right" >
        <ItemStyle HorizontalAlign="Right" Width="3px" />
        </asp:BoundField>
        <asp:BoundField DataField="sal_fin" HeaderText="Saldo Final"   
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" >
        <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
    </Columns>
    <HeaderStyle HorizontalAlign="Center" CssClass="gridHeader" />
    <PagerStyle CssClass="gridHeader" Font-Bold="False" />
    <RowStyle CssClass="gridItem" />
</asp:GridView>

<asp:UpdatePanel ID="UPPlanExcel" runat="server" >
<ContentTemplate>
     <asp:GridView ID="gvPlanPagos0" runat="server"  AutoGenerateColumns="False" GridLines="Horizontal" ShowHeaderWhenEmpty="True" 
        style="font-size: small" Width="927px">
        <Columns>
            <asp:BoundField DataField="numerocuota" HeaderText="No" 
                ItemStyle-HorizontalAlign="Left">
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="fechacuota" DataFormatString="{0:dd-MM-yyyy}" 
                HeaderText="Fecha" ItemStyle-HorizontalAlign="Right">
            <ItemStyle HorizontalAlign="Right"  />
            </asp:BoundField>
            <asp:BoundField DataField="sal_ini" DataFormatString="{0:c}" 
                HeaderText="Saldo Inicial" ItemStyle-HorizontalAlign="Right">
            <ItemStyle HorizontalAlign="Right"  />
            </asp:BoundField>
            <asp:BoundField DataField="capital" DataFormatString="{0:c}" 
                HeaderText="Capital" ItemStyle-HorizontalAlign="Right">
            <ItemStyle HorizontalAlign="Right"  />
            </asp:BoundField>
            <asp:BoundField DataField="int_1" DataFormatString="{0:c}" HeaderText="Int_1" 
                ItemStyle-HorizontalAlign="Right">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="int_2" DataFormatString="{0:c}" HeaderText="Int_2" 
                ItemStyle-HorizontalAlign="Right">
            <ItemStyle HorizontalAlign="Right"  />
            </asp:BoundField>
            <asp:BoundField DataField="int_3" DataFormatString="{0:c}" HeaderText="Int_3" 
                ItemStyle-HorizontalAlign="Right">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="int_4" DataFormatString="{0:c}" HeaderText="Int_4" 
                ItemStyle-HorizontalAlign="Right">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="int_5" DataFormatString="{0:c}" HeaderText="Int_5" 
                ItemStyle-HorizontalAlign="Right">
            <ItemStyle HorizontalAlign="Right"  />
            </asp:BoundField>
            <asp:BoundField DataField="int_6" DataFormatString="{0:c}" HeaderText="Int_6" 
                ItemStyle-HorizontalAlign="Right" Visible="false">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="int_7" DataFormatString="{0:c}" HeaderText="Int_7" 
                ItemStyle-HorizontalAlign="Right" Visible="false">
            <ItemStyle HorizontalAlign="Right"  />
            </asp:BoundField>
            <asp:BoundField DataField="int_8" DataFormatString="{0:c}" HeaderText="Int_8" 
                ItemStyle-HorizontalAlign="Right" Visible="false">
            <ItemStyle HorizontalAlign="Right"  />
            </asp:BoundField>
            <asp:BoundField DataField="int_9" DataFormatString="{0:c}" HeaderText="Int_9" 
                ItemStyle-HorizontalAlign="Right" Visible="false">
            <ItemStyle HorizontalAlign="Right"  />
            </asp:BoundField>
            <asp:BoundField DataField="int_10" DataFormatString="{0:c}" HeaderText="Int_10" 
                ItemStyle-HorizontalAlign="Right" Visible="false">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="int_11" DataFormatString="{0:c}" HeaderText="Int_11" 
                ItemStyle-HorizontalAlign="Right" Visible="false">
            <ItemStyle HorizontalAlign="Right"  />
            </asp:BoundField>
            <asp:BoundField DataField="int_12" DataFormatString="{0:c}" HeaderText="Int_12" 
                ItemStyle-HorizontalAlign="Right" Visible="false">
            <ItemStyle HorizontalAlign="Right"  />
            </asp:BoundField>
            <asp:BoundField DataField="int_13" DataFormatString="{0:c}" HeaderText="Int_13" 
                ItemStyle-HorizontalAlign="Right" Visible="false">
            <ItemStyle HorizontalAlign="Right"  />
            </asp:BoundField>
            <asp:BoundField DataField="int_14" DataFormatString="{0:c}" HeaderText="Int_14" 
                ItemStyle-HorizontalAlign="Right" Visible="false">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="int_15" DataFormatString="{0:c}" HeaderText="Int_15" 
                ItemStyle-HorizontalAlign="Right" Visible="false">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="total" DataFormatString="{0:c}" HeaderText="Total" 
                ItemStyle-HorizontalAlign="Right">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="sal_fin" DataFormatString="{0:c}" 
                HeaderText="Saldo Final" ItemStyle-HorizontalAlign="Right">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="gridHeader" HorizontalAlign="Center" />
        <PagerStyle CssClass="gridHeader" Font-Bold="False" />
        <RowStyle CssClass="gridItem" />
    </asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>