<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="ExamResultList.aspx.cs" Inherits="assignment.aspx.ExamResultList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h4 text-gray-900 mb-4">Enter Results</h1>
    <asp:GridView
        CssClass="table table-bordered"
        ID="ExamResultGridView"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="StudentID"
        OnRowDataBound="ExamResultGridView_RowDataBound"
        OnRowEditing="ExamResultGridView_RowEditing"
        OnRowUpdating="ExamResultGridView_RowUpdating"
        OnRowDeleting="ExamResultGridView_RowDeleting"
        OnRowCancelingEdit="ExamResultGridView_RowCancelingEdit"
        OnRowCommand="ExamResultGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="StudentID" HeaderText="StudentID" InsertVisible="False" ReadOnly="True" SortExpression="StudentID" />
            <asp:TemplateField HeaderText="Student Name">
                <ItemTemplate>
                    <asp:Label ID="lblStudentName" runat="server" Text='<%#Eval("StudentName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Class Name">
                <ItemTemplate>
                    <asp:Label ID="lblClassName" runat="server" Text='<%#Eval("ClassName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mark">
                <ItemTemplate>
                    <asp:Label ID="lblMark" runat="server" Text='<%#Eval("Mark") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMark" runat="server" CssClass="form-control" Text='<%#Eval("Mark") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Grade">
                <ItemTemplate>
                    <asp:Label ID="lblGrade" runat="server" Text='<%#Eval("Grade") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control"></asp:DropDownList>
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
    <p id="noStudent" runat="server">This exam doesn't have any student!</p>
</asp:Content>
