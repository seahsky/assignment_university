<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="BatchList.aspx.cs" Inherits="assignment.aspx.BatchList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h4 text-gray-900 mb-4">View Batchs</h1>
    <asp:GridView
        CssClass="table table-bordered"
        ID="BatchGridView"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyYears="BatchID"
        OnRowDataBound="BatchGridView_RowDataBound"
        OnRowEditing="BatchGridView_RowEditing"
        OnRowUpdating="BatchGridView_RowUpdating"
        OnRowDeleting="BatchGridView_RowDeleting"
        OnRowCancelingEdit="BatchGridView_RowCancelingEdit"
        OnRowCommand="BatchGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="BatchID" HeaderText="BatchID" InsertVisible="False" ReadOnly="True" SortExpression="BatchID" />
            <asp:TemplateField HeaderText="Year">
                <ItemTemplate>
                    <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" Text='<%#Eval("Year") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Batch Year is Required." ControlToValidate="txtYear"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button CommandName="Edit" runat="server" CssClass="btn btn-primary" Text="Edit" />
                    <asp:Button CommandName="Delete" runat="server" CssClass="btn btn-danger" Text="Delete" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button CommandName="Update" runat="server" CssClass="btn btn-secondary" Text="Update" />
                    <asp:Button CommandName="Cancel" runat="server" CssClass="btn btn-warning" Text="Cancel" CausesValidation="false" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
