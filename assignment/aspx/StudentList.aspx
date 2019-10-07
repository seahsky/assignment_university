<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="StudentList.aspx.cs" Inherits="assignment.aspx.StudentList" EnableEventValidation="false" %>

<asp:Content ID="StudentList" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h4 text-gray-900 mb-4">View Students</h1>
    <asp:GridView
        CssClass="table table-bordered table-hover"
        ID="StudentGridView"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="StudentID"
        OnRowDataBound="StudentGridView_RowDataBound"
        OnRowEditing="StudentGridView_RowEditing"
        OnRowUpdating="StudentGridView_RowUpdating"
        OnRowDeleting="StudentGridView_RowDeleting"
        OnRowCancelingEdit="StudentGridView_RowCancelingEdit"
        OnRowCommand="StudentGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="StudentID" HeaderText="StudentID" InsertVisible="False" ReadOnly="True" SortExpression="StudentID" />
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%#Eval("Name") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Student Name is Required." ControlToValidate="txtName"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Text='<%#Eval("Email") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" Text="Student Email is Required." ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Invalid Email." ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9.!#$%&’*+\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"></asp:RegularExpressionValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact No">
                <ItemTemplate>
                    <asp:Label ID="lblContactNo" runat="server" Text='<%#Eval("ContactNo") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" Text='<%#Eval("ContactNo") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Student Contact No. is Required." ControlToValidate="txtContactNo"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Invalid Phone No." ControlToValidate="txtContactNo" ValidationExpression="^(\+?6?01)[0-46-9]-*[0-9]{7,8}$"></asp:RegularExpressionValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Batch">
                <ItemTemplate>
                    <asp:Label ID="lblBatch" runat="server" Text='<%#Eval("Batch") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Class">
                <ItemTemplate>
                    <asp:Label ID="lblClass" runat="server" Text='<%#Eval("Class") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" />
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
                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-info" Text="View Details" PostBackUrl='<%# "~/aspx/StudentForm.aspx?action=view&StudentID=" + Eval("StudentID") %>' />
                    <asp:Button CommandName="Edit" runat="server" CssClass="btn btn-primary" Text="Edit" />
                    <asp:Button CommandName="Delete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this student? All the data related to this student will be lost.')" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button CommandName="Update" runat="server" CssClass="btn btn-secondary" Text="Update" />
                    <asp:Button CommandName="Cancel" runat="server" CssClass="btn btn-warning" Text="Cancel" CausesValidation="false" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
