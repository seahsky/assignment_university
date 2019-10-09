<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="ExamSelectList.aspx.cs" Inherits="assignment.aspx.ExamSelectList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 id="h1Title" runat="server" class="h4 text-gray-900 mb-4">Manage Class Exams</h1>
    <div class="row mt-4">
        <div class="col-12">
            <asp:GridView
                CssClass="table table-bordered"
                ID="ExamSelectGridView"
                runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="ExamID"
                OnRowCommand="ExamSelectGridView_RowCommand"
                OnRowDataBound="ExamSelectGridView_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:RadioButton ID="RowSelector" runat="server" onclick="checkRadioBtn(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ExamID" HeaderText="ExamID" InsertVisible="False" ReadOnly="True" SortExpression="ExamID" Visible="true" />
                    <asp:BoundField DataField="Description" HeaderText="Description" InsertVisible="False" ReadOnly="True" SortExpression="Description" />
                    <asp:TemplateField HeaderText="Batch">
                        <ItemTemplate>
                            <asp:Label ID="lblBatch" runat="server" Text='<%#Eval("Batch") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Time">
                        <ItemTemplate>
                            <asp:Label ID="lblTime" runat="server" Text='<%#Eval("Time") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <p id="noExam" runat="server">This subject doesn't have any exam!</p>
        </div>
    </div>
</asp:Content>
