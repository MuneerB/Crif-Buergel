﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuccessStoriesRequestDetails_UserControl.ascx.cs" Inherits="CrifAustria.UserControls.Dashboard.SuccessStoriesRequestDetails_UserControl" %>
<div class="block">
        <style type="text/css">
        .contactDetails.detailView tr td {
            text-align: left;
        }

        td {
            text-align: center;
        }

        a {
            cursor: pointer;
        }
    </style>
    <div class="top-content"> 
         
        <span class="download"><span>
            <asp:Label ID="Label1" runat="server" Text="Download CSV"></asp:Label></span><span></span>
            <asp:ImageButton ID="ibDownload" runat="server" AlternateText="Download CSV" OnClick="ibDownload_Click" ImageUrl="~/images/exel-icon1.png" Visible="true" />
        </span>
    </div>
    <br />

    <table width="100%">
        <tr>
            <td colspan="3">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdateProgress runat="server" ID="updateProgress">
                    <ProgressTemplate>
                        &nbsp;
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td valign="top" width="45%">
                <asp:UpdatePanel runat="server" ID="updatePanel">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSourceMain"
                            AllowPaging="True" Width="100%"
                            AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="contactList grid" DataKeyNames="id"
                            GridLines="None" CellPadding="4" ForeColor="#333333" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="20px" HeaderText="#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="First_Name" HeaderText="First Name" SortExpression="First_Name" />
                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                <asp:BoundField DataField="ProductName" HeaderText="Resource" SortExpression="ProductName" />
                                <asp:BoundField DataField="CreatedDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy HH:mm}" SortExpression="CreatedDate" />
                                <asp:TemplateField ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" ToolTip="View full details" ImageUrl="~/images/view.png" runat="server" CommandName="Select" AlternateText="View" />
                                        <asp:ImageButton ID="deleteButton" ToolTip="Delete" ImageUrl="~/images/delete.png" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <AlternatingRowStyle CssClass="altrow" BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            <EmptyDataTemplate>
                                There is no data available to display!
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td></td>
            <td valign="top" width="52%">
                <asp:UpdatePanel runat="server" ID="updatePanel1">
                    <ContentTemplate>
                        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataSourceID="SqlDataSourceDetails"
                            CssClass="contactDetails detailView" GridLines="None" OnDataBound="DetailsView_DataBound" Width="100%"
                            CellPadding="4" ForeColor="#333333">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                            <EditRowStyle BackColor="#999999" />
                            <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                            <Fields>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <h2><strong>Details</strong></h2>

                                        <strong>Name:</strong>
                                        <%#Eval("First_Name")%> <%#Eval("Last_Name") %>
                                        <br />
                                        <strong>Email:</strong>
                                        <asp:HyperLink ID="emailLink" runat="server" NavigateUrl='<%# "mailto:" + Eval("Email") %>'><%# Eval("Email")%></asp:HyperLink>
                                        <br />
                                        <strong>Phone:</strong>
                                        <%#Eval("Telephone")%>
                                        <br />   
                                          <strong>Industry:</strong> 
                                        <%#Eval("Industry")%>
                                        <br /> 
                                         <strong>Company:</strong>
                                        <%#Eval("Company")%>
                                        <br /> 
                                         <strong>Job Title:</strong> 
                                        <%#Eval("JobTitle")%>
                                        <br /> 
                                         <strong>Resource:</strong> 
                                        <%#Eval("ProductName")%>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        </asp:DetailsView>
                        <asp:SqlDataSource ID="SqlDataSourceDetails" runat="server" SelectCommand="SELECT [Id],[First_Name],[Last_Name],[Email],[Telephone],[Company],[JobTitle],[Industry],[ProductName],
                            [CreatedDate] FROM [dbo].[SuccessStoryRequests] WHERE ([Id] = @Id)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="GridView1" Name="id" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSourceMain" runat="server"
                        SelectCommand="SELECT [Id],[First_Name],[Last_Name],[Email],[Telephone],[Company],[JobTitle],[Industry],[ProductName],
                            [CreatedDate] FROM [dbo].[SuccessStoryRequests] ORDER BY [CreatedDate] DESC"
                        DeleteCommand="DELETE FROM [dbo].[SuccessStoryRequests] WHERE [Id]=@Id"></asp:SqlDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
</div>