<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="SubjectList.aspx.cs" Inherits="assignment.aspx.SubjectList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h4 text-gray-900 mb-4">View Subjects</h1>
    <asp:GridView
        CssClass="table table-bordered"
        ID="SubjectGridView"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="SubjectID"
        OnRowDataBound="SubjectGridView_RowDataBound"
        OnRowEditing="SubjectGridView_RowEditing"
        OnRowUpdating="SubjectGridView_RowUpdating"
        OnRowDeleting="SubjectGridView_RowDeleting"
        OnRowCancelingEdit="SubjectGridView_RowCancelingEdit"
        OnRowCommand="SubjectGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="SubjectID" HeaderText="SubjectID" InsertVisible="False" ReadOnly="True" SortExpression="SubjectID" />
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%#Eval("Name") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Subject Name is Required." ControlToValidate="txtName"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" Text='<%#Eval("Description") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" Text="Subject Description is Required." ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Invalid Description." ControlToValidate="txtDescription" ValidationExpression="^[a-zA-Z0-9.!#$%&’*+\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"></asp:RegularExpressionValidator>
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
