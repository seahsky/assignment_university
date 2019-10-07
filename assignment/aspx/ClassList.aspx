<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="ClassList.aspx.cs" Inherits="assignment.aspx.ClassList" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h4 text-gray-900 mb-4">View Classes</h1>
    <asp:GridView
        CssClass="table table-bordered table-hover"
        ID="ClassGridView"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ClassID"
        OnRowDataBound="ClassGridView_RowDataBound"
        OnRowEditing="ClassGridView_RowEditing"
        OnRowUpdating="ClassGridView_RowUpdating"
        OnRowDeleting="ClassGridView_RowDeleting"
        OnRowCancelingEdit="ClassGridView_RowCancelingEdit"
        OnRowCommand="ClassGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="ClassID" HeaderText="ClassID" InsertVisible="False" ReadOnly="True" SortExpression="ClassID" />
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%#Eval("Name") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Class Name is Required." ControlToValidate="txtName"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" Text='<%#Eval("Description") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" Text="Class Description is Required." ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Lecturer In Charge">
                <ItemTemplate>
                    <asp:Label ID="lblLecturer" runat="server" Text='<%#Eval("LecturerName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlLecturer" runat="server" CssClass="form-control" />
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
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-info" Text="View Subjects" PostBackUrl='<%# "~/aspx/ClassForm.aspx?action=view&ClassID=" + Eval("ClassID") %>' />
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
