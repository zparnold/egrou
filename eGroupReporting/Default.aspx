﻿<%@ Page Title="Service Dashboard" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="//cdn.rawgit.com/Eonasdan/bootstrap-datetimepicker/a549aa8780dbda16f6cff545aeabc3d71073911e/build/css/bootstrap-datetimepicker.css" rel="stylesheet">

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
            <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
    <script type="text/javascript" src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>
    <script src="//cdn.rawgit.com/Eonasdan/bootstrap-datetimepicker/a549aa8780dbda16f6cff545aeabc3d71073911e/src/js/bootstrap-datetimepicker.js"></script>

    <asp:LoginView runat="server" ID="DashboardLoginView">
        <AnonymousTemplate>
            <br />
            <br />
            <div class="col-lg-12">
                <div class="alert alert-danger">
                    <strong>Oh snap!</strong> You're trying to access info without being logged in!
                 <a href="/Account/Login.aspx" class="alert-link">Log In</a> and try viewing this page again.
                </div>
            </div>
            <div class="modal" id="logInModal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title">Oh snap!</h4>
                        </div>
                        <div class="modal-body">
                            <p>Looks like you haven't logged in! Please log in to proceed.</p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" onclick="login_btn_submit()">Log In</button>
                        </div>
                    </div>
                </div>
            </div>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <!-- Aggregate View-->
            <br />
            <br />
            <nav class="navbar navbar-default">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
                            <span class="sr-only">Toggle navigation</span>
                        </button>
                        <a class="navbar-brand" href="#">Aggregate View</a>
                    </div>
                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                        <ul class="nav navbar-nav">
                            <li><a href="#"><i class="fa fa-floppy-o fa-lg"></i></a></li>
                            <li><a href="#"><i class="fa fa-print fa-lg"></i></a></li>
                            <li><a href="#"><i class="fa fa-file-excel-o fa-lg"></i></a></li>
                            <li><a href="#"><i class="fa fa-envelope-o fa-lg"></i></a></li>

                        </ul>
                        <ul class="nav navbar-nav navbar-right">
                            <li style="width: 275px; margin-top: 8px;">
                                <div class='input-group date' id='datetimepicker8'>
                                    <input type='text' class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                </div>
                            </li>
                            <li><a href="#" class="btn btn-primary" onclick="refreshBtnClick" style="color: white" id="refreshBtn"><i class="fa fa-refresh" id="spinIcon"></i>Update Results</a></li>
                            <li><a href="#" class="btn btn-default"><i class="fa fa-filter"></i>Filter</a></li>
                        </ul>
                    </div>
                </div>
            </nav>
            <h4>Utilization Percentage Forecast (Aggregate View)</h4>
            <br />
            <div class="container">
                <div class="col-lg-3">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Team Utilization</h3>
                        </div>
                        <div class="panel-body">
                            <h1 style="text-align: center;">79%</h1>
                        </div>
                    </div>
                </div>
                <div class="col-lg-9">
                    <table class="table table-striped table-hover ">
                        <thead>
                            <tr class="info">
                                <th>Billing Type</th>
                                <th>Last 90 Days</th>
                                <th>Next 90 Days</th>
                                <th>% Change</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Utilization</td>
                                <td>41%</td>
                                <td>68%</td>
                                <td>66% Increase <i class="fa fa-arrow-up fa-lg" style="color: green;"></i></td>
                            </tr>
                            <tr>
                                <td>% Training</td>
                                <td>22%</td>
                                <td>15%</td>
                                <td>32% Decrease <i class="fa fa-arrow-down fa-lg" style="color: red;"></td>
                            </tr>
                            <tr>
                                <td>% PTO</td>
                                <td>18%</td>
                                <td>12%</td>
                                <td>33% Decrease <i class="fa fa-arrow-down fa-lg" style="color: red;"></td>
                            </tr>
                            <tr>
                                <td>% Sales Engineering</td>
                                <td>15%</td>
                                <td>22%</td>
                                <td>46% Increase <i class="fa fa-arrow-up fa-lg" style="color: green;"></i></td>
                            </tr>
                            <tr>
                                <td>% Travel</td>
                                <td>15%</td>
                                <td>22%</td>
                                <td>46% Increase <i class="fa fa-arrow-up fa-lg" style="color: green;"></i></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <!-- END AGGREGATE VIEW -->
            <hr />
            <!-- BEGIN Individual View -->
            <br />
            <br />
            <nav class="navbar navbar-default">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
                            <span class="sr-only">Toggle navigation</span>
                        </button>
                        <a class="navbar-brand" href="#">Individual View</a>
                    </div>
                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
                        <ul class="nav navbar-nav">
                            <li><a href="#"><i class="fa fa-floppy-o fa-lg"></i></a></li>
                            <li><a href="#"><i class="fa fa-print fa-lg"></i></a></li>
                            <li><a href="#"><i class="fa fa-file-excel-o fa-lg"></i></a></li>
                            <li><a href="#"><i class="fa fa-envelope-o fa-lg"></i></a></li>

                        </ul>
                        <ul class="nav navbar-nav navbar-right">
                            <li style="width: 275px; margin-top: 8px;">
                                <div class='input-group date' id='datetimepicker1'>
                                    <input type='text' class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="fa fa-calendar"></span>
                                    </span>
                                </div>
                            </li>
                            <li><a href="#" class="btn btn-primary" onclick="refreshBtnClick" style="color: white" id="refreshBtn2"><i class="fa fa-refresh" id="spinIcon2"></i>Update Results</a></li>
                            <li><a href="#" class="btn btn-default"><i class="fa fa-filter"></i>Filter</a></li>
                        </ul>
                    </div>
                </div>
            </nav>
            <h4>Utilization Percentage Forecast (Individual View)</h4>
            <br />
            <div class="container">
                <div class ="col-sm-2">
                    <p>Key</p>
                    <table>
                        <tr>< 50% Utilized</tr>
                        <tr>50% - 74% Utilized</tr>
                        <tr>75% - 125% Utilized</tr>
                        <tr>> 125% Utilized</tr>
                    </table>
                </div>
                <div class="col-sm-10">
                    
                </div>
            </div>
            <!-- END INDIVIDUAL VIEW -->
        </LoggedInTemplate>
    </asp:LoginView>

    <div id="modal_container" runat="server"></div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#logInModal").modal("show");
        });
        function login_btn_submit() {
            window.location.replace("/Account/Login");
        }

        $("#refreshBtn").on("mouseover", function () {
            $(this).css("color", "black");
            $("#refreshBtn").css("background-color", "white");
        });
        $("#refreshBtn").on("mouseleave", function () {
            $(this).css("color", "white");
            $("#refreshBtn").css("background-color", "#2196f3");
        });
        $("#refreshBtn").on("click", function () {
            $("#spinIcon").addClass("fa-spin");
            $("#refreshBtn").css("background-color", "#2196f3");
            setTimeout(function () {
                $("#spinIcon").removeClass("fa-spin");
            }, 3000);
        });
        $("#refreshBtn2").on("mouseover", function () {
            $(this).css("color", "black");
            $("#refreshBtn2").css("background-color", "white");
        });
        $("#refreshBtn2").on("mouseleave", function () {
            $(this).css("color", "white");
            $("#refreshBtn2").css("background-color", "#2196f3");
        });
        $("#refreshBtn2").on("click", function () {
            $("#spinIcon2").addClass("fa-spin");
            $("#refreshBtn2").css("background-color", "#2196f3");
            setTimeout(function () {
                $("#spinIcon2").removeClass("fa-spin");
            }, 3000);
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker8').datetimepicker({
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-arrow-up",
                    down: "fa fa-arrow-down"
                }
            });
        });
        $(function () {
            $('#datetimepicker1').datetimepicker({
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-arrow-up",
                    down: "fa fa-arrow-down"
                }
            });
        });
    </script>
</asp:Content>