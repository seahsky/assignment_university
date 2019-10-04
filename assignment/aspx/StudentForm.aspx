<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="StudentForm.aspx.cs" Inherits="assignment.aspx.StudentForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 id="h1Title" runat="server" class="h4 text-gray-900 mb-4">Add a Student</h1>

    <div id="cardSuccess" runat="server" visible="false" class="col-xl-3 col-md-6 mb-4">
        <div class="card border-left-success shadow h-100 py-2">
            <div class="card-body">
                <div class="row no-gutters align-items-center">
                    <i class="fas fa-times fa-1x text-gray-300 ml-auto"></i>
                </div>
                <div class="row no-gutters align-items-center">
                    <div class="col mr-2">
                        <div class="text-xs font-weight-bold text-success text-uppercase mb-1">Success!</div>
                        <div class="h5 mb-0 font-weight-bold text-gray-800">Student added successfully.</div>
                    </div>
                    <div class="col-auto">
                        <i class="fas fa-check fa-2x text-success"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="add">
        <div class="row mt-4">
            <div class="col-4">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Student Name"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Student Name is Required." ControlToValidate="txtName"></asp:RequiredFieldValidator>
            </div>
            <div class="col-6">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Student Email"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" Text="Student Email is Required." ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Invalid Email." ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9.!#$%&’*+\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-4">
                <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" placeholder="Student Contact No."></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Student Contact No. is Required." ControlToValidate="txtContactNo"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Invalid Phone No." ControlToValidate="txtContactNo" ValidationExpression="^(\+?6?01)[0-46-9]-*[0-9]{7,8}$"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-2">
                <asp:Label ID="lblBatch" runat="server" Text="Batch"></asp:Label>
                <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-2">
                <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="mt-4"></asp:Label>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-4">
                <asp:Label ID="lblClass" runat="server" Text="Class"></asp:Label>
                <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
    </div>
    <%--Edit--%>
    <div id="edit" runat="server" visible="false">
        <h1 class="h4 text-gray-900 mb-4 mt-5">Exam  Results</h1>

        <asp:GridView
            CssClass="table table-bordered"
            ID="StudentResultGridView"
            runat="server"
            AutoGenerateColumns="False"
            DataKeyNames="ResultID"
            OnRowDataBound="StudentResultGridView_RowDataBound"
            OnRowEditing="StudentResultGridView_RowEditing"
            OnRowUpdating="StudentResultGridView_RowUpdating"
            OnRowDeleting="StudentResultGridView_RowDeleting"
            OnRowCancelingEdit="StudentResultGridView_RowCancelingEdit"
            OnRowCommand="StudentResultGridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="ResultID" HeaderText="ResultID" InsertVisible="False" ReadOnly="True" SortExpression="ResultID" />
                <asp:TemplateField HeaderText="Batch">
                    <ItemTemplate>
                        <asp:Label ID="lblBatch" runat="server" Text='<%#Eval("Batch") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtBatch" runat="server" CssClass="form-control" Text='<%#Eval("Batch") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("SubjectName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" Text='<%#Eval("Subject") %>'></asp:TextBox>
                    </EditItemTemplate>
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
                        <asp:TextBox ID="txtGrade" runat="server" CssClass="form-control" Text='<%#Eval("Grade") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <%--                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button CommandName="Edit" runat="server" CssClass="btn btn-primary" Text="Quick Edit" />
                        <asp:Button CommandName="Delete" runat="server" CssClass="btn btn-danger" Text="Delete" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button CommandName="Update" runat="server" CssClass="btn btn-secondary" Text="Update" />
                        <asp:Button CommandName="Cancel" runat="server" CssClass="btn btn-warning" Text="Cancel" CausesValidation="false" />
                    </EditItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
    </div>

    <div class="row mt-4">
        <div class="col-2">
            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-warning mr-2" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Add" OnClick="btnSubmit_Click" />
        </div>
    </div>
</asp:Content>
