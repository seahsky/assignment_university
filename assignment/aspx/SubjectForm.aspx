<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="SubjectForm.aspx.cs" Inherits="assignment.aspx.SubjectForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h4 text-gray-900 mb-4">Add a Subject</h1>

    <div id="cardSuccess" runat="server" visible="false" class="col-xl-3 col-md-6 mb-4">
        <div class="card border-left-success shadow h-100 py-2">
            <div class="card-body">
                <div class="row no-gutters align-items-center">
                    <i class="fas fa-times fa-1x text-gray-300 ml-auto"></i>
                </div>
                <div class="row no-gutters align-items-center">
                    <div class="col mr-2">
                        <div class="text-xs font-weight-bold text-success text-uppercase mb-1">Success!</div>
                        <div class="h5 mb-0 font-weight-bold text-gray-800">Subject added successfully.</div>
                    </div>
                    <div class="col-auto">
                        <i class="fas fa-check fa-2x text-success"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row mt-4">
        <div class="col-6">
            <asp:Label ID="lblName" runat="server" Text="Subject Name"></asp:Label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Subject Name"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Subject Name is Required." ControlToValidate="txtName"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row mt-4">
        <div class="col-8">
            <asp:Label ID="lblDescription" runat="server" Text="Subject Description"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Subject Description"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" Text="Subject Description is Required." ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row mt-4">
        <div class="col-4">
            <asp:Label ID="lblLecturer" runat="server" Text="Lecturer In Charge"></asp:Label>
            <asp:DropDownList ID="ddlLecturer" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <div class="row mt-4">
        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-warning mr-2" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Add" OnClick="btnSubmit_Click" />
    </div>
</asp:Content>
