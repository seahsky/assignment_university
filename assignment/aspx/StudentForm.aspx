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
        <div id="addBtns" runat="server" class="row mt-4">
            <div class="col-2">
                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-warning mr-2" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Add" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
    <%--Edit--%>
    <div id="edit" runat="server" visible="false">
        <h1 class="h4 text-gray-900 mb-4 mt-5">Class Informations</h1>

        <div class="row mt-4">
            <div class="col-4">
                <asp:Label ID="lblClassName" runat="server" Text="Class Name"></asp:Label>
                <asp:TextBox ID="txtClassName" runat="server" CssClass="form-control" placeholder="Class Name" disabled="true"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-12">
                <asp:Label ID="lblClassDescription" runat="server" Text="Class Description"></asp:Label>
                <asp:TextBox ID="txtClassDescription" runat="server" CssClass="form-control" placeholder="Class Description" disabled="true"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-2">
                <asp:Label ID="lblLecturerInCharge" runat="server" Text="Lecturer in Charge"></asp:Label>
                <asp:TextBox ID="txtLecturerInCharge" runat="server" CssClass="form-control" placeholder="Lecturer in Charge" disabled="true"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-2">
                <asp:Label ID="lblClassBatch" runat="server" Text="Batch"></asp:Label>
                <asp:TextBox ID="txtClassBatch" runat="server" CssClass="form-control" placeholder="Batch" disabled="true"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-6">
                <asp:Label ID="lblClassSubject" runat="server" Text="Subjects Included:"></asp:Label>
                <asp:GridView
                    CssClass="table table-bordered"
                    ID="ClassSubjectGridView"
                    runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="SubjectID"
                    OnRowDataBound="ClassSubjectGridView_RowDataBound"
                    OnRowEditing="ClassSubjectGridView_RowEditing"
                    OnRowUpdating="ClassSubjectGridView_RowUpdating"
                    OnRowDeleting="ClassSubjectGridView_RowDeleting"
                    OnRowCancelingEdit="ClassSubjectGridView_RowCancelingEdit"
                    OnRowCommand="ClassSubjectGridView_RowCommand">
                    <Columns>
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
            </div>
        </div>

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
            </Columns>
        </asp:GridView>
        <div class="row mt-4">
            <div class="col-2">
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger mr-2" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-info" Text="Update" OnClick="btnUpdate_Click" />
            </div>
        </div>
    </div>
</asp:Content>
