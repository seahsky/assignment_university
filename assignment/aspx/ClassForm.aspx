<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="ClassForm.aspx.cs" Inherits="assignment.aspx.ClassForm" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 id="h1Title" runat="server" class="h4 text-gray-900 mb-4">Add a Class</h1>

    <div id="cardSuccess" runat="server" visible="false" class="col-xl-3 col-md-6 mb-4">
        <div class="card border-left-success shadow h-100 py-2">
            <div class="card-body">
                <div class="row no-gutters align-items-center">
                    <i class="fas fa-times fa-1x text-gray-300 ml-auto"></i>
                </div>
                <div class="row no-gutters align-items-center">
                    <div class="col mr-2">
                        <div class="text-xs font-weight-bold text-success text-uppercase mb-1">Success!</div>
                        <div class="h5 mb-0 font-weight-bold text-gray-800">Class added successfully.</div>
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
            <div class="col-6">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Class Name"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Class Name is Required." ControlToValidate="txtName"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-12">
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Class Description"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" Text="Class Description is Required." ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-5">
                <asp:Label ID="lblBatch" runat="server" Text="Batch" CssClass="mt-4"></asp:Label>
                <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control col-5"></asp:DropDownList>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-5">
                <asp:Label ID="lblLecturer" runat="server" Text="Lecturer in Charge" CssClass="mt-4"></asp:Label>
                <asp:DropDownList ID="ddlLecturer" runat="server" CssClass="form-control col-5"></asp:DropDownList>
            </div>
        </div>
        <div id="addBtns" runat="server" class="row mt-4">
            <div class="col-4">
                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-warning mr-2" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Add" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
    <%--View--%>
    <div id="view" runat="server" visible="false">
        <div class="row mt-4">
            <div class="col-12">
                <asp:Label ID="lblClassSubject" runat="server" Text="Subjects Included:"></asp:Label>
                <asp:GridView
                    CssClass="table table-bordered"
                    ID="ClassSubjectGridView"
                    runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="SubjectID"
                    OnRowCommand="ClassSubjectGridView_RowCommand"
                    OnRowDataBound="ClassSubjectGridView_RowDataBound"
                    OnRowDeleting="ClassSubjectGridView_RowDeleting"
                    OnRowCancelingEdit="ClassSubjectGridView_RowCancelingEdit"
                    OnRowUpdating="ClassSubjectGridView_RowUpdating"
                    OnRowEditing="ClassSubjectGridView_RowEditing">
                    <Columns>
                        <asp:BoundField DataField="ClassSubjectID" HeaderText="ClassSubjectID" InsertVisible="False" ReadOnly="True" SortExpression="ClassSubjectID" Visible="true" />
                        <asp:BoundField DataField="SubjectID" HeaderText="SubjectID" InsertVisible="False" ReadOnly="True" SortExpression="SubjectID" />
                        <asp:TemplateField HeaderText="Subject Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSubjectName" runat="server" Text='<%#Eval("SubjectName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject Description">
                            <ItemTemplate>
                                <asp:Label ID="lblSubjectDescription" runat="server" Text='<%#Eval("SubjectDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
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
                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalAddSubject">
                    Add Subject
                </button>

                <%--Modal--%>
                <div class="modal fade" id="modalAddSubject" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Add a Subject</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                <asp:Button ID="addSubjectBtn" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="addSubjectBtn_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
