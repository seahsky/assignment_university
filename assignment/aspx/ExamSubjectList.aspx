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
                </Columns>
            </asp:GridView>
            <p id="noSubject" runat="server">This class doesn't have any subject!</p>
        </div>
    </div>
</asp:Content>
