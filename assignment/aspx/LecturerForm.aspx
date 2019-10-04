<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="LecturerForm.aspx.cs" Inherits="assignment.aspx.LecturerForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h4 text-gray-900 mb-4">Add a Lecturer</h1>

    <div id="cardSuccess" runat="server" visible="false" class="col-xl-3 col-md-6 mb-4">
        <div class="card border-left-success shadow h-100 py-2">
            <div class="card-body">
                <div class="row no-gutters align-items-center">
                    <i class="fas fa-times fa-1x text-gray-300 ml-auto"></i>
                </div>
                <div class="row no-gutters align-items-center">
                    <div class="col mr-2">
                        <div class="text-xs font-weight-bold text-success text-uppercase mb-1">Success!</div>
                        <div class="h5 mb-0 font-weight-bold text-gray-800">Lecturer added successfully.</div>
                    </div>
                    <div class="col-auto">
                        <i class="fas fa-check fa-2x text-success"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <asp:TextBox ID="txtName" runat="server" CssClass="form-control col-6 mt-4" placeholder="Lecturer Name"></asp:TextBox>
    </div>
    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Lecturer Name is Required." ControlToValidate="txtName"></asp:RequiredFieldValidator>
    <div class="row">
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control col-8 mt-4" placeholder="Lecturer Email"></asp:TextBox>
    </div>
    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" Text="Lecturer Email is Required." ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Invalid Email." ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9.!#$%&’*+\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"></asp:RegularExpressionValidator>
    <div class="row">
        <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control col-4 mt-4" placeholder="Lecturer Contact No."></asp:TextBox>
    </div>
    <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Lecturer Contact No. is Required." ControlToValidate="txtContactNo"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" ErrorMessage="Invalid Phone No." ControlToValidate="txtContactNo" ValidationExpression="^(\+?6?01)[0-46-9]-*[0-9]{7,8}$"></asp:RegularExpressionValidator>
    <div class="row">
        <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="mt-4"></asp:Label>
    </div>
    <div class="row">
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control col-5"></asp:DropDownList>
    </div>
    <div class="row mt-4">
        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-warning mr-2" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Add" OnClick="btnSubmit_Click" />
    </div>
</asp:Content>
