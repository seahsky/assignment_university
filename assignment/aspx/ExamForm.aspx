<%@ Page Title="" Language="C#" MasterPageFile="~/aspx/Site.Master" AutoEventWireup="true" CodeBehind="ExamForm.aspx.cs" Inherits="assignment.aspx.ExamForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h4 text-gray-900 mb-4">Add an Exam</h1>

    <div id="cardSuccess" runat="server" visible="false" class="col-xl-3 col-md-6 mb-4">
        <div class="card border-left-success shadow h-100 py-2">
            <div class="card-body">
                <div class="row no-gutters align-items-center">
                    <i class="fas fa-times fa-1x text-gray-300 ml-auto"></i>
                </div>
                <div class="row no-gutters align-items-center">
                    <div class="col mr-2">
                        <div class="text-xs font-weight-bold text-success text-uppercase mb-1">Success!</div>
                        <div class="h5 mb-0 font-weight-bold text-gray-800">Exam added successfully.</div>
                    </div>
                    <div class="col-auto">
                        <i class="fas fa-check fa-2x text-success"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row mt-4">
        <div class="col-8">
            <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Exam Description"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" CssClass="invalid-feedback" Display="Dynamic" Text="Exam Description is Required." ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row mt-4">
        <div class="col-8">
            <asp:Label ID="lblSubject" runat="server" Text="Subject"></asp:Label>
            <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <div class="row mt-4">
        <div class="col-5">
            <asp:Label ID="lblDate" runat="server" Text="Exam Date"></asp:Label>
            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" placeholder="Exam Date"></asp:TextBox>
        </div>
    </div>
    <div class="row mt-4">
        <div class="col-2">
            <asp:Label ID="lblTime" runat="server" Text="Exam Time"></asp:Label>
            <asp:TextBox ID="txtTime" runat="server" CssClass="form-control" placeholder="Ëxam Time"></asp:TextBox>
        </div>
    </div>
    <div class="row mt-4">
        <div class="col-4">
            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-warning mr-2" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Add" OnClick="btnSubmit_Click" />
        </div>
    </div>

    <script>
        $(function () {
            $("#<%= txtDate.ClientID %>").datepicker();
            $("#<%= txtTime.ClientID %>").timepicker({
                timeFormat: 'h:mm p',
                interval: 60,
                minTime: '10',
                maxTime: '6:00pm',
                startTime: '10:00',
                dynamic: false,
                dropdown: true,
                scrollbar: true
            });
        });
    </script>
</asp:Content>
