﻿<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="ExamList.aspx.cs" Inherits="assignment.aspx.ExamList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h4 text-gray-900 mb-4">View Exams</h1>
    <asp:GridView
        CssClass="table table-bordered"
        ID="ExamGridView"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ExamID"
        OnRowDataBound="ExamGridView_RowDataBound"
        OnRowEditing="ExamGridView_RowEditing"
        OnRowUpdating="ExamGridView_RowUpdating"
        OnRowDeleting="ExamGridView_RowDeleting"
        OnRowCancelingEdit="ExamGridView_RowCancelingEdit"
        OnRowCommand="ExamGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="ExamID" HeaderText="ExamID" InsertVisible="False" ReadOnly="True" SortExpression="ExamID" />
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" Text='<%#Eval("Description") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Exam Description is Required." ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Subject">
                <ItemTemplate>
                    <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("SubjectName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control" />
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
