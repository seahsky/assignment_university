<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="ExamSubjectList.aspx.cs" Inherits="assignment.aspx.ExamSubjectList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 id="h1Title" runat="server" class="h4 text-gray-900 mb-4">Manage Class Exams</h1>
    <div class="row mt-4">
        <div class="col-12">
            <asp:GridView
                CssClass="table table-bordered table-hover"
                ID="ExamSubjectGridView"
                runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="SubjectID"
                OnRowCommand="ExamSubjectGridView_RowCommand"
                OnRowCancelingEdit="ExamSubjectGridView_RowCancelingEdit"
                OnRowEditing="ExamSubjectGridView_RowEditing"
                OnRowUpdating="ExamSubjectGridView_RowUpdating"
                OnRowDataBound="ExamSubjectGridView_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="ClassSubjectID" HeaderText="ClassSubjectID" InsertVisible="False" ReadOnly="True" SortExpression="ClassSubjectID" Visible="true" />
                    <asp:BoundField DataField="SubjectID" HeaderText="SubjectID" InsertVisible="False" ReadOnly="True" SortExpression="SubjectID" />
                    <asp:TemplateField HeaderText="Subject Name">
                        <ItemTemplate>
                            <asp:Label ID="lblSubjectName" runat="server" Text='<%#Eval("SubjectName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject Description">
                        <ItemTemplate>
                            <asp:Label ID="lblSubjectDescription" runat="server" Text='<%#Eval("SubjectDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exam Date and Time">
                        <ItemTemplate>
                            <asp:Label ID="lblExam" runat="server" Text='<%#Eval("DateTime").ToString().Substring(0, 20) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlDateTime" runat="server" CssClass="form-control"></asp:DropDownList>
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
            <p id="noSubject" runat="server">This class doesn't have any subject!</p>
        </div>
    </div>
</asp:Content>
