<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="LecturerList.aspx.cs" Inherits="assignment.aspx.LecturerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h4 text-gray-900 mb-4">View Lecturers</h1>
    <asp:GridView
        CssClass="table table-bordered"
        ID="LecturerGridView"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="LecturerID"
        OnRowDataBound="LecturerGridView_RowDataBound"
        OnRowEditing="LecturerGridView_RowEditing"
        OnRowUpdating="LecturerGridView_RowUpdating"
        OnRowDeleting="LecturerGridView_RowDeleting"
        OnRowCancelingEdit="LecturerGridView_RowCancelingEdit"
        OnRowCommand="LecturerGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="LecturerID" HeaderText="LecturerID" InsertVisible="False" ReadOnly="True" SortExpression="LecturerID" />
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%#Eval("Name") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Lecturer Name is Required." ControlToValidate="txtName"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Text='<%#Eval("Email") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" Text="Lecturer Email is Required." ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Invalid Email." ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9.!#$%&’*+\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"></asp:RegularExpressionValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact No">
                <ItemTemplate>
                    <asp:Label ID="lblContactNo" runat="server" Text='<%#Eval("ContactNo") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" Text='<%#Eval("ContactNo") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Lecturer Contact No. is Required." ControlToValidate="txtContactNo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Invalid Phone No." ControlToValidate="txtContactNo" ValidationExpression="^(\+?6?01)[0-46-9]-*[0-9]{7,8}$"></asp:RegularExpressionValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" />
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
