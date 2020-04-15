<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="UNO.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="UNO Home Page" />
    <meta name="keywords" content="UNO" />
    <meta name="author" content="CMS" />
    <link rel="shortcut icon" href="../favicon.ico">
    <link rel="stylesheet" type="text/css" href="Styles/demo.css" />
    <link rel="stylesheet" type="text/css" href="Styles/style.css" />
    <link rel="stylesheet" type="text/css" href="Styles/jquery.jscrollpane.css" media="all" />
    <link href='http://fonts.googleapis.com/css?family=PT+Sans+Narrow&v1' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Coustard:900' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Rochester' rel='stylesheet' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top: 50px;">
        <div id="ca-container" class="ca-container">
            <div class="ca-wrapper">
                <div class="ca-item ca-item-1">
                    <div class="ca-item-main">
                        <div class="ca-icon">
                        </div>
                        <h3>
                            File Management</h3>
                        <h4>
                            <span class="ca-quote">&ldquo;</span> <span>Core module for employee information gathering.</span>
                        </h4>
                        <a href="#" class="ca-more">more...</a>
                    </div>
                    <div class="ca-content-wrapper">
                        <div class="ca-content">
                            <h6>
                                <asp:LinkButton ID="lnkFileManagement" runat="server">System Configuration</asp:LinkButton>
                            </h6>
                            <a href="#" class="ca-close">close</a>
                            <div class="ca-content-text">
                                <p>
                                   The core of the suite which holds data of all organizational entities. This module is referenced by all the modules in the suite. 
                                </p>
                            </div>
                            <ul>
                                <%--<li><a href="#">Read more</a></li>--%>
                                <%--<li><a href="#">Share this</a></li>--%>
                                <%--<li><a href="#">Become a member</a></li>--%>
                                <%--<li><a href="#">Donate</a></li>--%>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="ca-item ca-item-2">
                    <div class="ca-item-main">
                        <div class="ca-icon">
                        </div>
                        <h3>
                            Sentinel</h3>
                        <h4>
                            <span class="ca-quote">&ldquo;</span> <span>A comprehensive Access control System for
                                controlling employee movement on the campus. </span>
                        </h4>
                        <a href="#" class="ca-more">more...</a>
                    </div>
                    <div class="ca-content-wrapper">
                        <div class="ca-content">
                            <h6>
                                Sentinel</h6>
                            <a href="#" class="ca-close">close</a>
                            <div class="ca-content-text">
                                <p>
                                   Sentinel is a powerful, reliable access control and security management application. 
Being totally web-based, The software allows to manage permissions from any location easily.  Sentinel, being part of a suite, has a synergetic integration with other modules like Tempus (Time Attendance), Invictus (Visitor Management) etc.
                                </p>
                            </div>
                            <ul>
                                <%--<li><a href="#">Read more</a></li>--%>
                                <%--<li><a href="#">Share this</a></li>--%>
                                <%--<li><a href="#">Become a member</a></li>--%>
                                <%--<li><a href="#">Donate</a></li>--%>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="ca-item ca-item-3">
                    <div class="ca-item-main">
                        <div class="ca-icon">

                        </div>
                        <h3>
                            Tempus</h3>
                        <h4>
                            <span class="ca-quote">&ldquo;</span> <span>An intelligent and user-friendly Time Attendance
                                system</span>
                        </h4>
                        <a href="#" class="ca-more">more...</a>
                    </div>
                    <div class="ca-content-wrapper">
                        <div class="ca-content">
                            <h6>
                                Tempus</h6>
                            <a href="#" class="ca-close">close</a>
                            <div class="ca-content-text">
                                <p>
                                    Tempus module, seamlessly integrated with the UNO Framework, for catering
                                    to attendance needs of small,mid and large businesses.
                                </p>
                                <p>
                                    With Tempus at your disposal, You have an easy interface to time attendance calculations,
                                    shift scheduling, absence monitoring and management.</p>
                                <p>
                                    Tempus provides a quick and easy interface for doing various HR tasks relating to
                                    maintaining workforce attendance. It automatically calculates and makes available
                                    crucial data at your fingertips for facilitating informed decisions.</p>
                                <%--<p>
                                    Some of the features :
                                    <ol>
                                        <li>Employee Self Service</li>
                                        <li>Absence Management</li>
                                        <li>Workforce Scheduling</li>
                                        <li>Leave, Tour and Outpass</li>
                                        <li>Accurate reports</li>
                                    </ol>
                                </p>--%>
                                <%--<p>
                                    Organizations of all sizes use time and attendance systems to record when employees
                                    start and stop work.Tempus allows easy allocation of manpower where it is needed.
                                    Tempus makes big saving in terms of elimination of wastage in productivity</p>--%>
                                <%--<p>
                                    A time and attendance system provides many benefits to organizations. It enables
                                    an employer to have full control of all employees working hours. It helps control
                                    labor costs by reducing over-payments, which are often caused by transcription error,
                                    interpretation error and intentional error. Manual processes are also eliminated
                                    as well as the staff needed to maintain them. It is often difficult to comply with
                                    labor regulation, but a time and attendance system is invaluable for ensuring compliance
                                    with labor regulations regarding proof of attendance.</p>--%>
                                <%--<p>
                                    Companies with large employee numbers might need to install several time clock stations
                                    in order to speed up the process of getting all employees to clock in or out quickly
                                    or to record activity in dispersed locations.</p>--%>
                            </div>
                            <ul>
                                <%--<li><a href="#">Read more</a></li>--%>
                                <%--<li><a href="#">Share this</a></li>--%>
                                <%--<li><a href="#">Become a member</a></li>--%>
                                <%--<li><a href="#">Donate</a></li>--%>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="ca-item ca-item-4">
                    <div class="ca-item-main">
                        <div class="ca-icon">
                        </div>
                        <h3>
                            Perso</h3>
                        <h4>
                            <span class="ca-quote">&ldquo;</span> <span>An Identity Management System for Biometric
                                enrollment, Card Personalization and printing</span>
                        </h4>
                        <a href="#" class="ca-more">more...</a>
                    </div>
                    <div class="ca-content-wrapper">
                        <div class="ca-content">
                            <h6>
                                Perso</h6>
                            <a href="#" class="ca-close">close</a>
                            <div class="ca-content-text">
                                <p>
                                  Perso (Personalization) helps you simplify the ID management process by bringing together all ID related activities at a single window. Perso can help you design card templates in a WYSIWYG editor, print cards, enroll fingerprints, write card holder data into card with ease.</p>
                                <%--<p>
                                    When, while the lovely valley teems with vapour around me, and the meridian sun
                                    strikes the upper surface of the impenetrable foliage of my trees, and but a few
                                    stray gleams steal into the inner sanctuary, I throw myself down among the tall
                                    grass by the trickling stream;</p>--%>
                                <%--<p>
                                    She packed her seven versalia, put her initial into the belt and made herself on
                                    the way.</p>--%>
                            </div>
                            <ul>
                                <%--<li><a href="#">Read more</a></li>--%>
                                <%--<li><a href="#">Share this</a></li>--%>
                                <%--<li><a href="#">Become a member</a></li>--%>
                                <%--<li><a href="#">Donate</a></li>--%>
                            </ul>
                        </div>
                    </div>
                </div>
                <%--<div class="ca-item ca-item-5">
                    <div class="ca-item-main">
                        <div class="ca-icon">
                        </div>
                        <h3>
                            Card Management</h3>
                        <h4>
                            <span class="ca-quote">&ldquo;</span> <span>A weak man is just by accident. A strong
                                but non-violent man is unjust by accident.</span>
                        </h4>
                        <a href="#" class="ca-more">more...</a>
                    </div>
                    <div class="ca-content-wrapper">
                        <div class="ca-content">
                            <h6>
                                Card Management</h6>
                            <a href="#" class="ca-close">close</a>
                            <div class="ca-content-text">
                                <p>
                                    I am so happy, my dear friend, so absorbed in the exquisite sense of mere tranquil
                                    existence, that I neglect my talents. I should be incapable of drawing a single
                                    stroke at the present moment; and yet I feel that I never was a greater artist than
                                    now.</p>
                                <p>
                                    When, while the lovely valley teems with vapour around me, and the meridian sun
                                    strikes the upper surface of the impenetrable foliage of my trees, and but a few
                                    stray gleams steal into the inner sanctuary, I throw myself down among the tall
                                    grass by the trickling stream;</p>
                                <p>
                                    She packed her seven versalia, put her initial into the belt and made herself on
                                    the way.</p>
                            </div>
                            <ul>
                                <li><a href="#">Read more</a></li>
                                <li><a href="#">Share this</a></li>
                                <li><a href="#">Become a member</a></li>
                                <li><a href="#">Donate</a></li>
                            </ul>
                        </div>
                    </div>
                </div>--%>
                <div class="ca-item ca-item-5">
                    <div class="ca-item-main">
                        <div class="ca-icon">
                        </div>
                        <h3>
                            Invictus</h3>
                        <h4>
                            <span class="ca-quote">&ldquo;</span> <span>Lobby management system for campus </span>
                        </h4>
                        <a href="#" class="ca-more">more...</a>
                    </div>
                    <div class="ca-content-wrapper">
                        <div class="ca-content">
                            <h6>
                                Invictus</h6>
                            <a href="#" class="ca-close">close</a>
                            <div class="ca-content-text">
                                <p>
                                    Any organization has a number of visitors in the form of Business partners, Vendors, Contractors etc. visiting the organization at different points of time. Managing these visitors becomes a tedious and time-consuming activity for the security and the receptionist. 
Invictus helps solving this problem by providing a easy and simple interface to managing visitor entry. Some of the features include – Visitor Pre-registration, Gatepass/Visitor slip printing, Monitoring check-in and check-out etc.</p>
                                <p>
                                    <%--<p>
                                    She packed her seven versalia, put her initial into the belt and made herself on
                                    the way.</p>--%>
                            </div>
                            <ul>
                                <%--<li><a href="#">Read more</a></li>--%>
                                <%--<li><a href="#">Share this</a></li>--%>
                                <%--<li><a href="#">Become a member</a></li>--%>
                                <%--<li><a href="#">Donate</a></li>--%>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="ca-item ca-item-6">
                    <div class="ca-item-main">
                        <div class="ca-icon">
                        </div>
                        <h3>
                            Relish</h3>
                        <h4>
                            <span class="ca-quote">&ldquo;</span> <span>Canteen Management System</span>
                        </h4>
                        <a href="#" class="ca-more">more...</a>
                    </div>
                    <div class="ca-content-wrapper">
                        <div class="ca-content">
                            <h6>
                                Relish</h6>
                            <a href="#" class="ca-close">close</a>
                            <div class="ca-content-text">
                                <p>
                                    Relish is a complete canteen management meant to be used for automation of Cafeterias
                                    within campus.</p>
                            </div>
                            <ul>
                                <%--<li><a href="#">Read more</a></li>--%>
                                <%--<li><a href="#">Share this</a></li>--%>
                                <%--<li><a href="#">Become a member</a></li>--%>
                                <%--<li><a href="#">Donate</a></li>--%>
                            </ul>
                        </div>
                    </div>
                </div>
                <%--<div class="ca-item ca-item-8">
                    <div class="ca-item-main">
                        <div class="ca-icon">
                        </div>
                        <h3>
                            Time And Expense Management</h3>
                        <h4>
                            <span class="ca-quote">&ldquo;</span> <span>A nation's culture resides in the hearts
                                and in the soul of its people.</span>
                        </h4>
                        <a href="#" class="ca-more">more...</a>
                    </div>
                    <div class="ca-content-wrapper">
                        <div class="ca-content">
                            <h6>
                                Time And Expense Management</h6>
                            <a href="#" class="ca-close">close</a>
                            <div class="ca-content-text">
                                <p>
                                    I am so happy, my dear friend, so absorbed in the exquisite sense of mere tranquil
                                    existence, that I neglect my talents. I should be incapable of drawing a single
                                    stroke at the present moment; and yet I feel that I never was a greater artist than
                                    now.</p>
                                <p>
                                    When, while the lovely valley teems with vapour around me, and the meridian sun
                                    strikes the upper surface of the impenetrable foliage of my trees, and but a few
                                    stray gleams steal into the inner sanctuary, I throw myself down among the tall
                                    grass by the trickling stream;</p>
                                <p>
                                    She packed her seven versalia, put her initial into the belt and made herself on
                                    the way.</p>
                            </div>
                            <ul>
                                <li><a href="#">Read more</a></li>
                                <li><a href="#">Share this</a></li>
                                <li><a href="#">Become a member</a></li>
                                <li><a href="#">Donate</a></li>
                            </ul>
                        </div>
                    </div>
                </div>--%>
                <%--<div class="ca-item ca-item-9">
                    <div class="ca-item-main">
                        <div class="ca-icon">
                        </div>
                        <h3>
                            TempForce</h3>
                        <h4>
                            <span class="ca-quote">&ldquo;</span> <span>A nation's culture resides in the hearts
                                and in the soul of its people.</span>
                        </h4>
                        <a href="#" class="ca-more">more...</a>
                    </div>
                    <div class="ca-content-wrapper">
                        <div class="ca-content">
                            <h6>
                                TempForce</h6>
                            <a href="#" class="ca-close">close</a>
                            <div class="ca-content-text">
                                <p>
                                    I am so happy, my dear friend, so absorbed in the exquisite sense of mere tranquil
                                    existence, that I neglect my talents. I should be incapable of drawing a single
                                    stroke at the present moment; and yet I feel that I never was a greater artist than
                                    now.</p>
                                <p>
                                    When, while the lovely valley teems with vapour around me, and the meridian sun
                                    strikes the upper surface of the impenetrable foliage of my trees, and but a few
                                    stray gleams steal into the inner sanctuary, I throw myself down among the tall
                                    grass by the trickling stream;</p>
                                <p>
                                    She packed her seven versalia, put her initial into the belt and made herself on
                                    the way.</p>
                            </div>
                            <ul>
                                <li><a href="#">Read more</a></li>
                                <li><a href="#">Share this</a></li>
                                <li><a href="#">Become a member</a></li>
                                <li><a href="#">Donate</a></li>
                            </ul>
                        </div>
                    </div>
                </div>--%>
                <%--<div class="ca-item ca-item-10">
                    <div class="ca-item-main">
                        <div class="ca-icon">
                        </div>
                        <h3>
                            Asset Management</h3>
                        <h4>
                            <span class="ca-quote">&ldquo;</span> <span>A nation's culture resides in the hearts
                                and in the soul of its people.</span>
                        </h4>
                        <a href="#" class="ca-more">more...</a>
                    </div>
                    <div class="ca-content-wrapper">
                        <div class="ca-content">
                            <h6>
                                Asset Management</h6>
                            <a href="#" class="ca-close">close</a>
                            <div class="ca-content-text">
                                <p>
                                    I am so happy, my dear friend, so absorbed in the exquisite sense of mere tranquil
                                    existence, that I neglect my talents. I should be incapable of drawing a single
                                    stroke at the present moment; and yet I feel that I never was a greater artist than
                                    now.</p>
                                <p>
                                    When, while the lovely valley teems with vapour around me, and the meridian sun
                                    strikes the upper surface of the impenetrable foliage of my trees, and but a few
                                    stray gleams steal into the inner sanctuary, I throw myself down among the tall
                                    grass by the trickling stream;</p>
                                <p>
                                    She packed her seven versalia, put her initial into the belt and made herself on
                                    the way.</p>
                            </div>
                            <ul>
                                <li><a href="#">Read more</a></li>
                                <li><a href="#">Share this</a></li>
                                <li><a href="#">Become a member</a></li>
                                <li><a href="#">Donate</a></li>
                            </ul>
                        </div>
                    </div>
                </div>--%>
                <%--<div class="ca-item ca-item-11">
                    <div class="ca-item-main">
                        <div class="ca-icon">
                        </div>
                        <h3>
                            Vehicle Management</h3>
                        <h4>
                            <span class="ca-quote">&ldquo;</span> <span>A nation's culture resides in the hearts
                                and in the soul of its people.</span>
                        </h4>
                        <a href="#" class="ca-more">more...</a>
                    </div>
                    <div class="ca-content-wrapper">
                        <div class="ca-content">
                            <h6>
                                Vehicle Management</h6>
                            <a href="#" class="ca-close">close</a>
                            <div class="ca-content-text">
                                <p>
                                    I am so happy, my dear friend, so absorbed in the exquisite sense of mere tranquil
                                    existence, that I neglect my talents. I should be incapable of drawing a single
                                    stroke at the present moment; and yet I feel that I never was a greater artist than
                                    now.</p>
                                <p>
                                    When, while the lovely valley teems with vapour around me, and the meridian sun
                                    strikes the upper surface of the impenetrable foliage of my trees, and but a few
                                    stray gleams steal into the inner sanctuary, I throw myself down among the tall
                                    grass by the trickling stream;</p>
                                <p>
                                    She packed her seven versalia, put her initial into the belt and made herself on
                                    the way.</p>
                            </div>
                            <ul>
                                <li><a href="#">Read more</a></li>
                                <li><a href="#">Share this</a></li>
                                <li><a href="#">Become a member</a></li>
                                <li><a href="#">Donate</a></li>
                            </ul>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.easing.1.3.js"></script>
    <!-- the jScrollPane script -->
    <script type="text/javascript" src="Scripts/jquery.mousewheel.js"></script>
    <script type="text/javascript" src="Scripts/jquery.contentcarousel.js"></script>
    <script type="text/javascript">
        $('#ca-container').contentcarousel();
    </script>
</asp:Content>
