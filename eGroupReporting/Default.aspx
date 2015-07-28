<%@ Page Title="Service Dashboard" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="//cdn.rawgit.com/Eonasdan/bootstrap-datetimepicker/a549aa8780dbda16f6cff545aeabc3d71073911e/build/css/bootstrap-datetimepicker.css" rel="stylesheet">

    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>
    <script src="//cdn.rawgit.com/Eonasdan/bootstrap-datetimepicker/a549aa8780dbda16f6cff545aeabc3d71073911e/src/js/bootstrap-datetimepicker.js"></script>
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
            <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
    <style>
        .lowestqty {
            background-color: white;
        }

        .lowqty {
            background-color: yellow;
        }

        .regqty {
            background-color: lightgreen;
        }

        .highqty {
            background-color: red;
            color: white;
        }
    </style>
    <asp:LoginView runat="server" ID="DashboardLoginView">
        <AnonymousTemplate>
            <div class="modal fade" id="logInModal">
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
            <br />
            <br />
            <div class="col-lg-12">
                <div class="alert alert-danger">
                    <strong>Oh snap!</strong> You're trying to access info without being logged in!
                 <a href="/Account/Login.aspx" class="alert-link">Log In</a> and try viewing this page again.
                </div>
            </div>

        </AnonymousTemplate>
        <LoggedInTemplate>
            <!-- Aggregate View-->
            <br />
            <br />
            <nav class="navbar navbar-default">
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav">
                        <li><a href="#"><i class="fa fa-floppy-o fa-lg"></i></a></li>
                        <li><a href="#"><i class="fa fa-print fa-lg"></i></a></li>
                        <li><a href="#"><i class="fa fa-file-excel-o fa-lg"></i></a></li>
                        <li><a href="#"><i class="fa fa-envelope-o fa-lg"></i></a></li>

                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li>
                            <p style="margin-top: 8px;">Begin: </p>
                        </li>
                        <li style="width: 200px; margin-top: 8px;">
                            <div class='input-group date' id='datetimepicker1'>
                                <input type='text' class="form-control" />
                                <span class="input-group-addon">
                                    <span class="fa fa-calendar"></span>
                                </span>
                            </div>
                        </li>
                        <li><p style="margin-top: 8px;">To: </p></li>
                        <li style="width: 200px; margin-top: 8px;">
                            <div class='input-group date' id='datetimepicker2'>
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
            <div class="container">
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

                <h4>Utilization Percentage Forecast (Individual View)</h4>
                <br />
                <div class="container">
                    <table class="table table-striped table-hover " id="individualTable">
                        <thead>
                            <tr>
                                <th style="width: 200px;">Resource</th>
                                <th>Week 1</th>
                                <th>Week 2</th>
                                <th>Week 3</th>
                                <th>Week 4</th>
                                <th>Week 5</th>
                                <th>Week 6</th>
                                <th>Week 7</th>
                                <th>Week 8</th>
                                <th>Week 9</th>
                                <th>Week 10</th>
                                <th>Week 11</th>
                                <th>Week 12</th>
                                <th>Week 13</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="customValidate">Adam Turner</td>
                                <td class="customValidate">76%</td>
                                <td class="customValidate">100%</td>
                                <td class="customValidate">86%</td>
                                <td class="customValidate">90%</td>
                                <td class="customValidate">55%</td>
                                <td class="customValidate">100%</td>
                                <td class="customValidate">60%</td>
                                <td class="customValidate">62%</td>
                                <td class="customValidate">70%</td>
                                <td class="customValidate">10%</td>
                                <td class="customValidate">1000%</td>
                                <td class="customValidate">149%</td>
                                <td class="customValidate">126%</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
                <div class="container">
                    <div class="col-sm-2">
                        <table class="table table-striped table-hover ">
                            <thead>
                                <tr>
                                    <th>Key</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="lowestqty">< 50% Utilized</td>
                                </tr>
                                <tr>
                                    <td class="lowqty">50% - 74% Utilized</td>
                                </tr>
                                <tr>
                                    <td class="regqty">75% - 125% Utilized</td>
                                </tr>
                                <tr>
                                    <td class="highqty">> 125% Utilized</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="col-sm-10">
                        <p>Utilization Definition:</p>
                        <p>Utilization percentage is defined as the percentage of billable hours to actual hours, where actual hours is comprised of a typical 40-hour work-week less any holidays.</p>
                    </div>
                </div>
                <div id="CWAPITest" runat="server"></div>
                <!-- END INDIVIDUAL VIEW -->
            </div>
        </LoggedInTemplate>
    </asp:LoginView>

    <script type="text/javascript">
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
        $(function () {
            $('#datetimepicker2').datetimepicker({
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-arrow-up",
                    down: "fa fa-arrow-down"
                }
            });
        });
        $(".customValidate").each(function () {
            var value = parseFloat($(this).text()) / 100.0;
            if (value < 0.5) {
                $(this).addClass("lowestqty");
            }
            else if (value >= 0.5 && value < 0.75) {
                $(this).addClass("lowqty");
            }
            else if (value >= 0.75 && value < 1.25) {
                $(this).addClass("regqty");

            } else if (value > 1.25) {
                $(this).addClass("highqty");
            }
        });

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

        $("document").ready(function () {
            $("#logInModal").appendTo("body").modal("show");
        });
        function login_btn_submit() {
            window.location.replace("/Account/Login");
        }
    </script>
</asp:Content>
