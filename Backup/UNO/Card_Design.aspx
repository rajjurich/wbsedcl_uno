<%@ Page Title="" Language="C#" MasterPageFile="~/ModuleMain.master" AutoEventWireup="true"
    CodeBehind="Card_Design.aspx.cs" Inherits="UNO.Card_Design" %>

<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.ColorPicker" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="Styles/jquery-ui.css" rel="stylesheet" type="text/css" />--%>
    <%-- <script src="Scripts/Jquery.min.1.8.2.js" type="text/javascript"></script>
  <script src="Scripts/1.8.23-jquery-ui.min.js" type="text/javascript"></script>
    --%>
    <%--//For Ruler--%>
    <%-- <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>--%>
    <!--[if lt IE 9]>   
           <script type="text/javascript" src="js/html5.js"></script>
    <![endif]-->
    <script type="text/javascript">

        function colorToHex(color) {
            if (color.substr(0, 1) === '#') {
                return color;
            }
            var digits = /(.*?)rgb\((\d+), (\d+), (\d+)\)/.exec(color);

            var red = parseInt(digits[2]);
            var green = parseInt(digits[3]);
            var blue = parseInt(digits[4]);

            var rgb = blue | (green << 8) | (red << 16);
            return digits[1] + '#' + rgb.toString(16);
        }
        function colorChanged(sender) {
            var color = sender.getColor()
            var element = document.getElementById('TextBox1');
            element.value = color.replace("#", "");


            if ($('#rbdBackSide').is(':checked')) {
                var div = document.getElementById('droppable1');
                div.style.backgroundColor = color;
            }
            else {
                var div = document.getElementById('droppable');
                div.style.backgroundColor = color;
            }

            //rbdBackSide

        }
        function colorChanged1(sender) {
            var color = sender.getColor()
            var element = document.getElementById('TextBox3');
            element.value = color.replace("#", "");
            var div = document.getElementById('droppable1');
            div.style.backgroundColor = color;

        }
        function colorChanged2(sender) {
            var element = document.getElementById(sender.get_element().id);
            var color = sender.getColor().replace("#", "");
            element.value = color;
            element.style.backgroundColor = "#FFF";
        }
        function colorChanged3(sender) {
            var getSelectedControl = $("#getControl").val();
            var color = $("#" + sender.get_element().id).val();

            if (getSelectedControl != "") {
                var varControl = getSelectedControl.split(',');
                $.each(varControl, function (index, value) {
                    if (value != "") {
                        if (sender.get_element().id == "litxtbgColor") {
                            $("#" + value).css({ "backgroundColor": sender.getColor() });
                        }
                        else {
                            $("#" + value).css({ "color": sender.getColor() });
                        }

                    }

                });
                getSelectedControl = "";
            }
            else {
                alert("Please select atleast one control.");
            }
        };

        function ConfirmReset(sender) {
            var x;
            var r = confirm("Do you want to Reset Template!");
            if (r == true) {
                window.location = "Card_Design.aspx";
                return true;
            }
            else {
                return false;
            }
        }
        function ConfirmSave(sender) {
            return false;
        }
 
      
    </script>
    <style type="text/css">
        #draggable
        {
            float: left;
            width: 60%;
            background: #47A3DA;
            padding: 2px;
            color: Black;
            margin: 10px;
            list-style-type: none;
        }
        #draggable li
        {
            padding: 3px;
            margin: 3px;
            background-color: white;
            cursor: pointer;
            text-decoration: none;
        }
        .ModalPopupBG
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .HellowWorldPopup
        {
            min-width: 200px;
            min-height: 150px;
            background: white;
            border: 1px solid black;
            border-bottom-style: groove;
            border-radius: 20px;
            padding: 10px;
            box-shadow: inset 0 -5px 15px rgba(255,255,255,0.4), inset -2px -1px 40px rgba(0,0,0,0.4), 0 0 1px #000;
        }
        .ModalButton
        {
            display: none;
        }
        .card
        {
            position: absolute;
        }
        .card1
        {
            position: absolute;
        }
        
        .setBorder
        {
            border: 1px dashed black !important;
        }
        .Psize
        {
            max-height: 8.5cm;
            max-width: 5.4cm;
        }
        .Lsize
        {
            max-height: 5.4cm;
            max-width: 8.5cm;
        }
        .comment-text
        {
            word-wrap: break-word;
        }
    </style>
    <%--Hide Show controls--%>
    <script type="text/jscript">
        var source;
        var destination;
        var uniqueIdentity;
        var getSelectedControl = "";
        $(window).bind('beforeunload', function ()
        {
            return '>>>>>Before You Go<<<<<<<< \n Your custom message go here';
        });

        function HideShow(uniqueIdentity)
        {

            if (uniqueIdentity == 1)
            {

                $("#ddltxtValue").show();
                $("#chkHindi").show();                
                $("#trfieldName").show();
                $("#trFontSize").show();
                $("#trFontType").show();
                $("#trForeColor").show();
                $("#txtValue").hide();
                // $("#trWidth").hide();
                $("#trheight").hide();
                $("#trWidth").show();
                $("#DynamicImg").hide();
                $("#staticImg").hide();
                $("#trTransparent").hide();

            }
            else if (uniqueIdentity == 2)
            {
                $("#txtValue").show();
                $("#chkHindi").hide();
                $("#ddltxtValue").hide();
                $("#chkHindi").hide();
                $("#trfieldName").show();
                $("#trFontSize").show();
                $("#trFontType").show();
                $("#trForeColor").show();
                $("#trWidth").hide();
                $("#trheight").hide();
                $("#DynamicImg").hide();
                $("#staticImg").hide();
                $("#trTransparent").hide();
            }
            else if (uniqueIdentity == 4 || uniqueIdentity == 3 || uniqueIdentity == 5 || uniqueIdentity == 6)
            {
                $("#trfieldName").hide();
                $("#chkHindi").hide();
                $("#txtValue").hide();
                $("#trFontSize").hide();
                $("#trFontType").hide();
                $("#trForeColor").hide();
                $("#trWidth").show();
                $("#trheight").show();
                $("#DynamicImg").hide();
                $("#staticImg").hide();
                $("#trTransparent").show();
                if (uniqueIdentity == 3)
                {
                    $("#DynamicImg").show();
                    $("#staticImg").show();
                    $("#trTransparent").hide();
                }

            }
        };
        function HideShowBackSide(uniqueIdentity)
        {
          
            if (uniqueIdentity == 1)
            {
              
                $("#ddltxtValue1").show();
                $("#chkHindi1").show();
                $("#trfieldName1").show();
                $("#trFontSize1").show();
                $("#trFontType1").show();
                $("#trForeColor1").show();
                $("#txtValue1").hide();
                // $("#trWidth1").hide();
                $("#trheight1").hide();
                $("#trWidth1").show();
                $("#DynamicImg1").hide();
                $("#staticImg1").hide();
                $("#trTransparent1").hide();
            }
            else if (uniqueIdentity == 2)
            {

                $("#txtValue1").show();
                $("#chkHindi1").hide();
                $("#trfieldName1").show();
                $("#trFontSize1").show();
                $("#trFontType1").show();
                $("#trForeColor1").show();
                $("#ddltxtValue1").hide();
                $("#chkHindi1").hide();
                $("#trWidth1").hide();
                $("#trheight1").hide();
                $("#DynamicImg1").hide();
                $("#staticImg1").hide();
                $("#trTransparent1").hide();
            }
            else if (uniqueIdentity == 4 || uniqueIdentity == 3 || uniqueIdentity == 5 || uniqueIdentity == 6)
            {
                $("#trfieldName1").hide();
                $("#chkHindi1").hide();
                $("#txtValue1").hide();
                $("#trFontSize1").hide();
                $("#trFontType1").hide();
                $("#trForeColor1").hide();
                $("#trWidth1").show();
                $("#trheight1").show();
                $("#DynamicImg1").hide();
                $("#staticImg1").hide();
                $("#trTransparent1").show();
                if (uniqueIdentity == 3)
                {
                    $("#DynamicImg1").show();
                    $("#staticImg1").show();
                    $("#trTransparent1").hide();
                }

            }
        };
        // get image name from iFrame 
        function getSession()
        {
            var imageName = "";
            $.ajax({
                url: "Card_Design.aspx/GetUserId",
                type: "POST",
                dataType: "text",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (msg)
                {

                    if (msg != "")
                    {

                        imageName = msg.split("\"");
                    }

                },
                error: function () { alert(arguments[2]); }
            });
            return imageName[3];

        };
        // set image name 
        function SetSession()
        {
            var imageName = "";
            $.ajax({
                url: "iFrame.aspx/setSession",
                type: "POST",
                dataType: "text",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (msg)
                {
                    //  alert(msg);

                },
                error: function () { alert(arguments[2]); }
            });


        };
        function colorToHex(color)
        {
            var HexColor = "";

            if (navigator.appName == "Microsoft Internet Explorer")
            {

                HexColor = color;

            }
            else
            {
                if (color.substr(0, 1) === '#')
                {
                    return color;
                }
                var digits = /(.*?)rgb\((\d+), (\d+), (\d+)\)/.exec(color);

                var red = parseInt(digits[2]);
                var green = parseInt(digits[3]);
                var blue = parseInt(digits[4]);

                var rgb = blue | (green << 8) | (red << 16);
                HexColor = digits[1] + '#' + rgb.toString(16);

            }
            return HexColor;
        };
    </script>
   <%-- DragAndDoubleClickEvent For Front Side  Edit--%>
    <script type="text/jscript">
        function DragAndDoubleClickEvent()
        {

            $("#droppable").each(function ()
            {
                // $(this).find('span,div,img,input').css("position", "relative");

                $(this).find('span,div').draggable({ containment: "parent", cursor: 'move' }).addClass("noclick").removeClass("setBorder");
                $(this).find('img,input').draggable({ cancel: null, containment: "parent", cursor: "move" });
                getSelectedControl = "";

                $(this).find('span,div,input,img').unbind('click').bind('click', function (e)
                {
                    var id = $(this).attr('id');
                    if (!$(this).hasClass("noclick"))
                    {
                        //   alert($(this).hasClass("setBorder"));
                        if ($(this).hasClass("setBorder"))
                        {
                            $("#" + id).removeClass("setBorder");
                            getSelectedControl = getSelectedControl.replace(id + ",", "");
                            $("#getControl").val(getSelectedControl);
                            if (getSelectedControl == "")
                            {
                                $("#FontSize").show();

                            } else
                            {
                                if (getSelectedControl.contains("Div") || getSelectedControl.contains("sep") || getSelectedControl.contains("img"))
                                {
                                    $("#FontSize").hide();
                                }
                            }
                            if (getSelectedControl.contains("lbl") || getSelectedControl.contains("txt"))
                            {
                                $("#FontSize").show();
                            }
                        }
                        else
                        {
                            $("#" + id).addClass("setBorder");
                            getSelectedControl = id + "," + getSelectedControl;
                            $("#getControl").val(getSelectedControl);
                            if (getSelectedControl.contains("Div") || getSelectedControl.contains("sep") || getSelectedControl.contains("img"))
                            {
                                $("#FontSize").hide();
                            }

                        }
                    }
                    else
                    {
                        $("#" + id).removeClass("setBorder noclick");
                        getSelectedControl = getSelectedControl.replace(id + ",", "");
                        $("#getControl").val(getSelectedControl);


                    }
                    if (getSelectedControl.contains("lbl") || getSelectedControl.contains("txt"))
                    {
                        $("#FontSize").show();
                    }
                    e.stopPropagation();
                    return false;
                });
                //For Maintain ID of label Control
                var spanLength = 0;
                $(this).find('span').each(function ()
                {
                    var id = $(this).attr('id');
                    if (id.length > spanLength)
                    {

                        spanLength = id.length;
                        $("#label1").html(id + 1);
                    }
                });
                //on label double click
                $(this).find('span').dblclick(function (obj)
                {
                    var id = $(this).attr("id");

                    uniqueIdentity = "2";
                    $("#GetId").val(id);

                    var css = $('#' + id).attr('style');

                    $("#btnDelete").css({ "display": "block" })
                    $("#" + id).css({ "border": "none" });
                    $("#txtValue").val($(this).text());
                    var splitCss = css.split(';');
                    $.each(splitCss, function (key, value)
                    {

                        if (value != "")
                        {
                            var spiltvalue = value.split(':');
                            //  alert(spiltvalue[0]);
                            if (spiltvalue[0].indexOf("background-color") >= 0)
                            {
                                $("#txtBgColor").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                            }
                            else if (spiltvalue[0].indexOf("color") >= 0)
                            {
                                $("#txtForeColor").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                            }
                            else if (spiltvalue[0].indexOf("font-size") >= 0)
                            {
                                if (spiltvalue[1] != "")
                                {
                                    $("#ddlSize option[value=" + spiltvalue[1].replace("px", "") + "]").attr("selected", "selected");
                                }
                            }
                            else if (spiltvalue[0].indexOf("font-family") >= 0)
                            {

                                $('#LstFont').val(spiltvalue[1].toLowerCase().trim());
                                //  $("#LstFont option[value=" + spiltvalue[1] + "]").attr("selected", "selected");
                            }

                        }
                    });
                    HideShow(uniqueIdentity);
                    $find('ModalPopupExtender1').show();

                }); // end label double click
                //on TextBox double click
                var inputLength = 0;
                $(this).find('input').each(function ()
                {
                    var id = $(this).attr('id');
                    if (id.length > inputLength)
                    {

                        inputLength = id.length;
                        $("#TextBox2").val(id + 1);
                      
                    }
                });
                $(this).find('input').dblclick(function ()
                {

                    var id = $(this).attr('id');

                    uniqueIdentity = "1";
                    $("#GetId").val(id);
                    $("#btnDelete").css({ "display": "block" })
                    var css = $('#' + id).attr('style');


                    var ddlvalue = $(this).val();
                    if (ddlvalue.contains('^') == true)
                    {
                        $("#ddltxtValue").val(ddlvalue.substring(1));
                        $("#chkHindi").attr('checked', true);
                    }
                    else
                    {
                        $("#ddltxtValue").val($(this).val());
                        $("#chkHindi").attr('checked', false);
                    }
                    var splitCss = css.split(';');

                    $.each(splitCss, function (key, value)
                    {

                        if (value != "")
                        {
                            var spiltvalue = value.split(':');
                            if (spiltvalue[0].indexOf("background-color") >= 0)
                            {
                                if (spiltvalue[1].toString().trim() != "transparent")
                                {

                                    $("#txtBgColor").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                }
                            }
                            else if (spiltvalue[0].indexOf("color") >= 0)
                            {

                                $("#txtForeColor").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                            }
                            else if (spiltvalue[0].indexOf("font-size") >= 0)
                            {
                                if (spiltvalue[1] != "")
                                {
                                    $("#ddlSize option[value=" + spiltvalue[1].replace("px", "") + "]").attr("selected", "selected");
                                }
                            }
                            else if (spiltvalue[0].indexOf("font-family") >= 0)
                            {


                                $('#LstFont').val(spiltvalue[1].toLowerCase().trim());

                                //$("#LstFont option[value =" + spiltvalue[1].toLowerCase() + "]").attr("selected", "selected");
                            }
                            else if (spiltvalue[0].indexOf("width") >= 0)
                            {
                                $("#txtWidth").val(spiltvalue[1].replace("px", ""));
                            }
                        }
                    });

                    HideShow(uniqueIdentity);

                    $find('ModalPopupExtender1').show();

                }); // end textbox double click
                //For Maintain ID of Div Control
                var DivLength = 0;
                var DiVSepLength = 0;
                $(this).find('div').each(function ()
                {
                    var id = $(this).attr('id');
                    if (id.indexOf("sep") >= 0)
                    { // div which behave as a separator
                        if (id.length > DiVSepLength)
                        {

                            DiVSepLength = id.length;
                            $("#label4").html(id + 1);
                        }

                    }
                    else
                    { //Normal Div
                        if (id.length > DivLength)
                        {

                            DivLength = id.length;
                            $("#label2").html(id + 1);
                        }
                    }

                });
                //on Div double click
                $(this).find('div').dblclick(function ()
                {
                    var id = $(this).attr('id');
                    if (id.indexOf("sep") >= 0)
                    {

                        uniqueIdentity = "5";
                     
                    } else if (id.indexOf("vsep") >= 0)
                    {

                        uniqueIdentity = "6";
                      
                    }
                    else
                    {
                        uniqueIdentity = "4";
                    }

                    $("#GetId").val(id);
                    $("#btnDelete").css({ "display": "block" })
                    var css = $('#' + id).attr('style');

                    var splitCss = css.split(';');
                    $.each(splitCss, function (key, value)
                    {
                        if (value != "")
                        {
                            var spiltvalue = value.split(':');
                            if (spiltvalue[0].indexOf("background-color") >= 0)
                            {
                                if (spiltvalue[1].trim() != "transparent")
                                {
                                    $("#txtBgColor").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                } else
                                {

                                    $("#txtBgColor").val("");
                                }
                            }
                            else if (spiltvalue[0].indexOf("width") >= 0)
                            {
                                if (spiltvalue[1] != "")
                                {
                                    $("#txtWidth").val(spiltvalue[1].replace("px", ""));
                                }
                            }
                            else if (spiltvalue[0].indexOf("height") >= 0)
                            {
                                $("#txtHeight").val(spiltvalue[1].replace("px", ""));

                            }
                        }
                    });
                    HideShow(uniqueIdentity);

                    $find('ModalPopupExtender1').show();

                }); // end Div double click
                var imgLength = 0;

                $(this).find('img').each(function ()
                {
                    var id = $(this).attr('id');

                    if (id.length > imgLength)
                    {

                        imgLength = id.length;
                        $("#label3").html(id + 1);
                    }

                });
                //on image double click
                $(this).find('img').dblclick(function ()
                {

                    var id = $(this).attr('id');
                    uniqueIdentity = "3";
                    $("#GetId").val(id);
                    $("#btnDelete").css({ "display": "block" })
                    var css = $('#' + id).attr('style');
                    var splitCss = css.split(';');
                    var e = $('#iFrame');
                    e.attr("src", e.attr("src"));
                    SetSession();
                    $.each(splitCss, function (key, value)
                    {
                        if (value != "")
                        {
                            var spiltvalue = value.split(':');
                            if (spiltvalue[0].indexOf("background-color") >= 0)
                            {
                                $("#txtBgColor").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                            }
                            else if (spiltvalue[0].indexOf("width") >= 0)
                            {
                                if (spiltvalue[1] != "")
                                {
                                    $("#txtWidth").val(spiltvalue[1].replace("px", ""));
                                }
                            }
                            else if (spiltvalue[0].indexOf("height") >= 0)
                            {
                                $("#txtHeight").val(spiltvalue[1].replace("px", ""));

                            }
                        }
                    });
                    HideShow(uniqueIdentity);
                    $find('ModalPopupExtender1').show();

                }); // end Div double click
                if (uniqueIdentity == 1)
                {

                    $("#TextBox2").val($("#TextBox2").val() + 1);
                }
                else if (uniqueIdentity == 2)
                {
                    $("#label1").html($("#label1").html() + 1);
                }
                else if (uniqueIdentity == 4)
                {
                    $("#label2").html($("#label2").html() + 1);
                }
                else if (uniqueIdentity == 3)
                {
                    $("#label3").html($("#label3").html() + 1);
                }
                else if (uniqueIdentity == 5)
                {
                    $("#label4").html($("#label4").html() + 1);
                }
                else if (uniqueIdentity == 6)
                {
                    $("#label5").html($("#label5").html() + 1);
                }


            });


        }
    </script>
    <%-- DragAndDoubleClickEvent1 For Back Side  Edit--%>
    <script type="text/jscript">

        function DragAndDoubleClickEvent1()
        {

            $("#droppable1").each(function ()
            {
                // $(this).find('span,div,img,input').css("position", "relative");

                $(this).find('span,div').draggable({ containment: "parent", cursor: 'move' }).addClass("noclick").removeClass("setBorder");
                $(this).find('img,input').draggable({ cancel: null, containment: "parent", cursor: "move" });
                getSelectedControl = "";

                $(this).find('span,div,input,img').unbind('click').bind('click', function (e)
                {
                    var id = $(this).attr('id');
                    if (!$(this).hasClass("noclick"))
                    {
                        //   alert($(this).hasClass("setBorder"));
                        if ($(this).hasClass("setBorder"))
                        {
                            $("#" + id).removeClass("setBorder");
                            getSelectedControl = getSelectedControl.replace(id + ",", "");
                            $("#getControl").val(getSelectedControl);
                            if (getSelectedControl == "")
                            {
                                $("#FontSize").show();

                            } else
                            {
                                if (getSelectedControl.contains("Div") || getSelectedControl.contains("sep") || getSelectedControl.contains("img"))
                                {
                                    $("#FontSize").hide();
                                }
                            }
                            if (getSelectedControl.contains("lbl") || getSelectedControl.contains("txt"))
                            {
                                $("#FontSize").show();
                            }
                        }
                        else
                        {
                            $("#" + id).addClass("setBorder");
                            getSelectedControl = id + "," + getSelectedControl;
                            $("#getControl").val(getSelectedControl);
                            if (getSelectedControl.contains("Div") || getSelectedControl.contains("sep") || getSelectedControl.contains("img"))
                            {
                                $("#FontSize").hide();
                            }

                        }
                    }
                    else
                    {
                        $("#" + id).removeClass("setBorder noclick");
                        getSelectedControl = getSelectedControl.replace(id + ",", "");
                        $("#getControl").val(getSelectedControl);


                    }
                    if (getSelectedControl.contains("lbl") || getSelectedControl.contains("txt"))
                    {
                        $("#FontSize").show();
                    }
                    e.stopPropagation();
                    return false;
                });
                //For Maintain ID of label Control
                var spanLength = 0;
                $(this).find('span').each(function ()
                {
                    var id = $(this).attr('id');
                    if (id.length > spanLength)
                    {

                        spanLength = id.length;
                        $("#label1").html(id + 1);
                    }
                });
                //on label double click
                $(this).find('span').dblclick(function (obj)
                {
                    var id = $(this).attr("id");

                    uniqueIdentity = "2";
                    $("#GetId").val(id);

                    var css = $('#' + id).attr('style');

                    $("#btnDelete").css({ "display": "block" })
                    $("#" + id).css({ "border": "none" });
                    $("#txtValue1").val($(this).text());
                    var splitCss = css.split(';');
                    $.each(splitCss, function (key, value)
                    {

                        if (value != "")
                        {
                            var spiltvalue = value.split(':');
                            //  alert(spiltvalue[0]);
                            if (spiltvalue[0].indexOf("background-color") >= 0)
                            {
                                $("#txtBgColor1").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                            }
                            else if (spiltvalue[0].indexOf("color") >= 0)
                            {
                                $("#txtForeColor1").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                            }
                            else if (spiltvalue[0].indexOf("font-size") >= 0)
                            {
                                if (spiltvalue[1] != "")
                                {
                                    $("#ddlSize1 option[value=" + spiltvalue[1].replace("px", "") + "]").attr("selected", "selected");
                                }
                            }
                            else if (spiltvalue[0].indexOf("font-family") >= 0)
                            {

                                $('#LstFont1').val(spiltvalue[1].toLowerCase().trim());
                                //  $("#LstFont option[value=" + spiltvalue[1] + "]").attr("selected", "selected");
                            }

                        }
                    });
                    HideShowBackSide(uniqueIdentity);
                    $find('ModalPopupExtender2').show();

                }); // end label double click
                //on TextBox double click
                var inputLength = 0;
                $(this).find('input').each(function ()
                {
                    var id = $(this).attr('id');
                    if (id.length > inputLength)
                    {

                        inputLength = id.length;
                        $("#TextBox2").val($("#TextBox2").val() + 1);

                    }
                });
                $(this).find('input').dblclick(function ()
                {

                    var id = $(this).attr('id');

                    uniqueIdentity = "1";
                    $("#GetId").val(id);
                    $("#btnDelete").css({ "display": "block" })
                    var css = $('#' + id).attr('style');



                    var ddlvalue = $(this).val();

                    if (ddlvalue.contains('^') == true)
                    {
                      
                        $("#ddltxtValue1").val(ddlvalue.substring(1));
                        $("#chkHindi1").attr('checked', true);
                    }
                    else
                    {
                        $("#ddltxtValue1").val($(this).val());
                        $("#chkHindi1").attr('checked', false);
                    }
                    //  $("#ddltxtValue1").val($(this).val());
                    var splitCss = css.split(';');

                    $.each(splitCss, function (key, value)
                    {

                        if (value != "")
                        {
                            var spiltvalue = value.split(':');
                            if (spiltvalue[0].indexOf("background-color") >= 0)
                            {
                                if (spiltvalue[1].toString().trim() != "transparent")
                                {

                                    $("#txtBgColor1").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                }
                            }
                            else if (spiltvalue[0].indexOf("color") >= 0)
                            {

                                $("#txtForeColor1").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                            }
                            else if (spiltvalue[0].indexOf("font-size") >= 0)
                            {
                                if (spiltvalue[1] != "")
                                {
                                    $("#ddlSize1 option[value=" + spiltvalue[1].replace("px", "") + "]").attr("selected", "selected");
                                }
                            }
                            else if (spiltvalue[0].indexOf("font-family") >= 0)
                            {


                                $('#LstFont1').val(spiltvalue[1].toLowerCase().trim());

                                //$("#LstFont option[value =" + spiltvalue[1].toLowerCase() + "]").attr("selected", "selected");
                            }
                            else if (spiltvalue[0].indexOf("width") >= 0)
                            {
                                $("#txtWidth1").val(spiltvalue[1].replace("px", ""));
                            }
                        }
                    });

                    HideShowBackSide(uniqueIdentity);
               
                    $find('ModalPopupExtender2').show();

                }); // end textbox double click
                //For Maintain ID of Div Control
                var DivLength = 0;
                var DiVSepLength = 0;
                $(this).find('div').each(function ()
                {
                    var id = $(this).attr('id');
                    if (id.indexOf("sep") >= 0)
                    { // div which behave as a separator
                        if (id.length > DiVSepLength)
                        {

                            DiVSepLength = id.length;
                            $("#label4").html(id + 1);
                        }

                    }
                    else
                    { //Normal Div
                        if (id.length > DivLength)
                        {

                            DivLength = id.length;
                            $("#label2").html(id + 1);
                        }
                    }

                });
                //on Div double click
                $(this).find('div').dblclick(function ()
                {
                    var id = $(this).attr('id');
                    if (id.indexOf("sep") >= 0)
                    {

                        uniqueIdentity = "5";
                       
                    } else if (id.indexOf("vsep") >= 0)
                    {

                        uniqueIdentity = "6";
                        
                    }
                    else
                    {
                        uniqueIdentity = "4";
                    }

                    $("#GetId").val(id);
                    $("#btnDelete").css({ "display": "block" })
                    var css = $('#' + id).attr('style');

                    var splitCss = css.split(';');
                    $.each(splitCss, function (key, value)
                    {
                        if (value != "")
                        {
                            var spiltvalue = value.split(':');
                            if (spiltvalue[0].indexOf("background-color") >= 0)
                            {
                                if (spiltvalue[1].trim() != "transparent")
                                {
                                    $("#txtBgColor1").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                } else
                                {

                                    $("#txtBgColor1").val("");
                                }
                            }
                            else if (spiltvalue[0].indexOf("width") >= 0)
                            {
                                if (spiltvalue[1] != "")
                                {
                                    $("#txtWidth1").val(spiltvalue[1].replace("px", ""));
                                }
                            }
                            else if (spiltvalue[0].indexOf("height") >= 0)
                            {
                                $("#txtHeight1").val(spiltvalue[1].replace("px", ""));

                            }
                        }
                    });
                    HideShowBackSide(uniqueIdentity);

                    $find('ModalPopupExtender2').show();

                }); // end Div double click
                var imgLength = 0;

                $(this).find('img').each(function ()
                {
                    var id = $(this).attr('id');

                    if (id.length > imgLength)
                    {

                        imgLength = id.length;
                        $("#label3").html(id + 1);
                    }

                });
                //on image double click
                $(this).find('img').dblclick(function ()
                {

                    var id = $(this).attr('id');
                    uniqueIdentity = "3";
                    $("#GetId").val(id);
                    $("#btnDelete").css({ "display": "block" })
                    var css = $('#' + id).attr('style');
                    var splitCss = css.split(';');
                    var e = $('#iFrame1');
                    e.attr("src", e.attr("src"));
                    SetSession();
                    $.each(splitCss, function (key, value)
                    {
                        if (value != "")
                        {
                            var spiltvalue = value.split(':');
                            if (spiltvalue[0].indexOf("background-color") >= 0)
                            {
                                $("#txtBgColor1").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                            }
                            else if (spiltvalue[0].indexOf("width") >= 0)
                            {
                                if (spiltvalue[1] != "")
                                {
                                    $("#txtWidth1").val(spiltvalue[1].replace("px", ""));
                                }
                            }
                            else if (spiltvalue[0].indexOf("height") >= 0)
                            {
                                $("#txtHeight1").val(spiltvalue[1].replace("px", ""));

                            }
                        }
                    });
                    HideShowBackSide(uniqueIdentity);
                    $find('ModalPopupExtender2').show();

                }); // end Div double click
                if (uniqueIdentity == 1)
                {
                    $("#TextBox2").val($("#TextBox2").val() + 1);
                }
                else if (uniqueIdentity == 2)
                {
                    $("#label1").html($("#label1").html() + 1);
                }
                else if (uniqueIdentity == 4)
                {
                    $("#label2").html($("#label2").html() + 1);
                }
                else if (uniqueIdentity == 3)
                {
                    $("#label3").html($("#label3").html() + 1);
                }
                else if (uniqueIdentity == 5)
                {
                    $("#label4").html($("#label4").html() + 1);
                }
                else if (uniqueIdentity == 6)
                {
                    $("#label5").html($("#label5").html() + 1);
                }


            });


        }
    </script>
      <%-- Ruler--%>
   
    <script type="text/jscript">
        try {



            $(document).ready(function ready()
            {

                $('#Div3').ruler({
                    vRuleSize: 18,
                    hRuleSize: 18,
                    showCrosshair: true,
                    showMousePos: false
                });

                var image = "";
                var count = 1;
                var count1 = 1
                // color converter rgb to hex

                DragAndDoubleClickEvent();
                DragAndDoubleClickEvent1();

                $("#rdbBack").attr('checked', 'checked');
                $("#rbdFrontSide").attr('checked', 'checked');

                // function for drop element clone into to div(for Front Side)
                var fn = function (event, ui)
                {

                    toDrop = $(ui.draggable).clone();


                    if (toDrop.attr("uniqueIdentity") == "1" || toDrop.attr("uniqueIdentity") == "2" || toDrop.attr("uniqueIdentity") == "3" || toDrop.attr("uniqueIdentity") == "4" || toDrop.attr("uniqueIdentity") == "5" || toDrop.attr("uniqueIdentity") == "6")
                    {

                        $("#droppable").append(toDrop);

                        uniqueIdentity = toDrop.attr("uniqueIdentity");
                        $("#chkTransparent").removeAttr('checked');

                        $("#btnDelete").css({ "display": "none" })
                        HideShow(uniqueIdentity);

                        $("#txtBgColor,#txtWidth,#txtHeight").val("");
                        $("#chkHindi").removeAttr('checked');
                        //set iFrame session and refresh it
                        if (toDrop.attr("uniqueIdentity") == "3")
                        {
                            var e = $('#iFrame')
                            e.attr("src", e.attr("src"));
                            SetSession();
                        }
                        $find('ModalPopupExtender1').show();
                        $("#txtValue").focus();


                    }
                };

                // function for drop element clone into to div(for Back Side)
                var fn1 = function (event, ui)
                {

                    toDrop = $(ui.draggable).clone();
                    if (toDrop.attr("uniqueIdentity") == "1" || toDrop.attr("uniqueIdentity") == "2" || toDrop.attr("uniqueIdentity") == "3" || toDrop.attr("uniqueIdentity") == "4" || toDrop.attr("uniqueIdentity") == "5" || toDrop.attr("uniqueIdentity") == "6")
                    {
                        $("#droppable1").append(toDrop);
                        uniqueIdentity = toDrop.attr("uniqueIdentity");
                        HideShowBackSide(uniqueIdentity);
                        $("#chkTransparent1").removeAttr('checked')


                        $("#txtBgColor1,#txtWidth1,#txtHeight1,#txtValue1").val("");

                        $("#chkHindi1").attr('checked', false);
                        //set iFrame session and refresh it
                        if (toDrop.attr("uniqueIdentity") == "3")
                        {

                            var e = $('#iFrame1');
                            e.attr("src", e.attr("src"));
                            SetSession();
                        }
                        $("#btnDelete1").css({ "display": "none" });

                        $find('ModalPopupExtender2').show();
                        $('#txtValue1').focus();
                    }
                };

                try
                {
                    $("#draggable li").draggable({
                        helper: 'clone',
                        cursor: 'move',
                        cancel: null

                    });
                }
                catch (e)
                {
                    alert(e);
                }



                $("#droppable").droppable({ drop: fn });
                $("#droppable1").droppable({ drop: fn1 });


                $('#liLeft,#liPortrait,#liLandScape,#Li1,#Li2,#Li3,#Li4,#FontSize').draggable("destroy");

                //start okay click event(For Front Side)
                $("#btnOkay").click(function setOk()
                {

                    if ($("#txtValue").val() == "" && $("#txtValue").css("display") != "none")
                    {
                        alert("Please enter field name.");
                        setTimeout(function () { $find('ModalPopupExtender1').show(); }, 0);
                    }
                    else
                    {
                        var getid = $("#GetId").val();

                        if (getid == "")
                        {
                            // set New element css 
                            var buffer = "";

                            $('#droppable li #l1').text($("#txtValue").val());

                            if ($("#txtBorderWidth").val() == "")
                            {
                                $("#txtBorderWidth").val("1");
                            }

                            if ($("#txtBorderColor").val() == "")
                            {
                                $('#droppable li #l1').css({ "backgroundColor": "#" + $("#txtBgColor").val() + "", "color": "#" + $("#txtForeColor").val() + "", "font-size": "" + $('#ddlSize :selected').text() + "px", "font-family": "" + $('#LstFont :selected').text() });
                                $('#droppable li #t1').css({ "backgroundColor": "#" + $("#txtBgColor").val() + "", "color": "#" + $("#txtForeColor").val() + "", "font-size": "" + $('#ddlSize :selected').text() + "px", "font-family": "" + $('#LstFont :selected').text() });
                                $('#droppable li #i1').css({ "backgroundColor": "#" + $("#txtBgColor").val() + "", "width": "" + $("#txtWidth").val() + "px", "height": "" + $("#txtHeight").val() + "px" });
                                if ($("#chkTransparent").attr('checked') == 'checked')
                                {
                                    $('#droppable li #d1').css({ "border-width": +$("#txtBorderWidth").val() + "px", "backgroundColor": "transparent", "width": "" + $("#txtWidth").val() + "px", "height": "" + $("#txtHeight").val() + "px" });
                                }
                                else

                                    $('#droppable li #d1').css({ "border-width": +$("#txtBorderWidth").val() + "px", "backgroundColor": "#" + $("#txtBgColor").val() + "", "width": "" + $("#txtWidth").val() + "px", "height": "" + $("#txtHeight").val() + "px" });

                            }
                            else
                            {
                                $('#droppable li #l1').css({ "border": "1px solid #" + $("#txtBorderColor").val() + "", "border-width": +$("#txtBorderWidth").val() + "px", "backgroundColor": "#" + $("#txtBgColor").val() + "", "color": "#" + $("#txtForeColor").val() + "", "font-size": "" + $('#ddlSize :selected').text() + "px", "font-family": "" + $('#LstFont :selected').text() });
                                $('#droppable li #t1').css({ "border": "1px solid #" + $("#txtBorderColor").val() + "", "border-width": +$("#txtBorderWidth").val() + "px", "backgroundColor": "#" + $("#txtBgColor").val() + "", "color": "#" + $("#txtForeColor").val() + "", "font-size": "" + $('#ddlSize :selected').text() + "px", "font-family": "" + $('#LstFont :selected').text() });
                                $('#droppable li #i1').css({ "border": "1px solid #" + $("#txtBorderColor").val() + "", "border-width": +$("#txtBorderWidth").val() + "px", "backgroundColor": "#" + $("#txtBgColor").val() + "", "width": "" + $("#txtWidth").val() + "px", "height": "" + $("#txtHeight").val() + "px" });
                                if ($("#chkTransparent").attr('checked') == 'checked')
                                {
                                    $('#droppable li #d1').css({ "border-width": +$("#txtBorderWidth").val() + "px", "border-color": "#" + $("#txtBorderColor").val() + "", "backgroundColor": "transparent", "width": "" + $("#txtWidth").val() + "px", "height": "" + $("#txtHeight").val() + "px" });
                                }
                                else
                                    $('#droppable li #d1').css({ "border-width": +$("#txtBorderWidth").val() + "px", "border-color": "#" + $("#txtBorderColor").val() + "", "backgroundColor": "#" + $("#txtBgColor").val() + "", "width": "" + $("#txtWidth").val() + "px", "height": "" + $("#txtHeight").val() + "px" });

                            }


                            if ($('#rdStatic').attr('checked') == 'checked')
                            {

                                var imageName = getSession();

                                $('#droppable li #i1').attr('src', "uploadImg/" + imageName);
                            }
                            else
                            {
                                var img = $('#ddlImages').val();

                                $('#droppable li #i1').attr('src', "img/" + img + ".jpeg");
                            }
                            $('#droppable li #line1').css({ "backgroundColor": "#" + $("#txtBgColor").val() + "", "width": "" + $("#txtWidth").val() + "px", "height": "" + $("#txtHeight").val() + "px" });
                            $('#droppable li #vline').css({ "backgroundColor": "#" + $("#txtBgColor").val() + "", "width": "" + $("#txtWidth").val() + "px", "height": "" + $("#txtHeight").val() + "px" });
                            $("#droppable li").each(function ()
                            {
                                $('#droppable li #t1').attr('id', $("#TextBox2").val());
                                $('#droppable li #l1').attr('id', $("#label1").text());
                                $('#droppable li #d1').attr('id', $("#label2").text());
                                $('#droppable li #i1').attr('id', $("#label3").text());
                                $('#droppable li #line1').attr('id', $("#label4").text());
                                $('#droppable li #vline').attr('id', $("#label5").text());
                                buffer = buffer + $('#droppable').find('li').html();
                                $('#droppable').find('li').detach();
                            });

                            $("#droppable").append(buffer);


                            if ($('#chkHindi').attr('checked') == 'checked')
                            {
                                $('#' + $("#TextBox2").val()).attr("value", '^' + $("#ddltxtValue").val());
                            }
                            else
                            {
                                $('#' + $("#TextBox2").val()).attr("value", $("#ddltxtValue").val());
                            }

                           
                            // $('#' + $("#TextBox2").val()).attr("data", $("#ddltxtValue").val());

                            $("#droppable").click(function ()
                            {

                                var varControl = getSelectedControl.split(',');
                                $.each(varControl, function (index, value)
                                {
                                    if (value != "")
                                    {
                                        $("#" + value).removeClass("setBorder");
                                    }
                                });
                                getSelectedControl = "";
                                $("#getControl").val("");
                            });

                            $("#droppable").each(function ()
                            {
                                // $(this).find('span,div,img,input').css("position", "relative");
                                // $(this).find('span,div,img').resizable();

                                $(this).find('span,div').draggable({ containment: "parent", cursor: 'move', preventCollision: false }).addClass("noclick").removeClass("setBorder");
                                $(this).find('img,input').draggable({ cancel: null, containment: "parent", cursor: "move", preventCollision: false });

                                getSelectedControl = "";

                                $(this).find('span,div,input,img').unbind('click').bind('click', function (e)
                                {
                                    var id = $(this).attr('id');


                                    if (!$(this).hasClass("noclick"))
                                    {
                                        //   alert($(this).hasClass("setBorder"));
                                        if ($(this).hasClass("setBorder"))
                                        {
                                            $("#" + id).removeClass("setBorder");
                                            getSelectedControl = getSelectedControl.replace(id + ",", "");
                                            $("#getControl").val(getSelectedControl);

                                            if (getSelectedControl == "")
                                            {
                                                $("#FontSize").show();

                                            } else
                                            {
                                                if (getSelectedControl.contains("Div") || getSelectedControl.contains("sep") || getSelectedControl.contains("img"))
                                                {
                                                    $("#FontSize").hide();
                                                }
                                            }
                                            if (getSelectedControl.contains("lbl") || getSelectedControl.contains("txt"))
                                            {
                                                $("#FontSize").show();
                                            }
                                        }
                                        else
                                        {
                                            $("#" + id).addClass("setBorder");
                                            getSelectedControl = id + "," + getSelectedControl;
                                            $("#getControl").val(getSelectedControl);
                                            // alert(getSelectedControl);
                                            if (getSelectedControl.contains("Div") || getSelectedControl.contains("sep") || getSelectedControl.contains("img"))
                                            {
                                                $("#FontSize").hide();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        $("#" + id).removeClass("setBorder noclick");
                                        getSelectedControl = getSelectedControl.replace(id + ",", "");
                                        $("#getControl").val(getSelectedControl);
                                    }

                                    if (getSelectedControl.contains("lbl") || getSelectedControl.contains("txt"))
                                    {
                                        $("#FontSize").show();
                                    }
                                    // alert(getSelectedControl);
                                    e.stopPropagation();
                                    return false;
                                });


                                //on label double click
                                $(this).find('span').dblclick(function (obj)
                                {
                                    var id = $(this).attr("id");
                                    /// var id = $(obj).attr("id");

                                    //if (id.indexOf("txt") >= 0) {
                                    //uniqueIdentity = "1";
                                    //} else {
                                    uniqueIdentity = "2";
                                    // }

                                    $("#GetId").val(id);

                                    var css = $('#' + id).attr('style');

                                    $("#btnDelete").css({ "display": "block" })
                                    $("#" + id).css({ "border": "none" });
                                    $("#txtValue").val($(this).text());
                                    var splitCss = css.split(';');
                                    $.each(splitCss, function (key, value)
                                    {

                                        if (value != "")
                                        {
                                            var spiltvalue = value.split(':');
                                            //  alert(spiltvalue[0]);
                                            if (spiltvalue[0].indexOf("background-color") >= 0)
                                            {
                                                $("#txtBgColor").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                            }
                                            else if (spiltvalue[0].indexOf("color") >= 0)
                                            {
                                                $("#txtForeColor").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                            }
                                            else if (spiltvalue[0].indexOf("font-size") >= 0)
                                            {
                                                if (spiltvalue[1] != "")
                                                {
                                                    $("#ddlSize option[value=" + spiltvalue[1].replace("px", "") + "]").attr("selected", "selected");
                                                }
                                            }
                                            else if (spiltvalue[0].indexOf("font-family") >= 0)
                                            {

                                                $('#LstFont').val(spiltvalue[1].toLowerCase().trim());
                                                //  $("#LstFont option[value=" + spiltvalue[1] + "]").attr("selected", "selected");
                                            }

                                        }
                                    });
                                    HideShow(uniqueIdentity);
                                    $find('ModalPopupExtender1').show();

                                }); // end label double click
                                //on TextBox double click
                                $(this).find('input').dblclick(function ()
                                {

                                    var id = $(this).attr('id');
                                    uniqueIdentity = "1";
                                    $("#GetId").val(id);
                                    $("#btnDelete").css({ "display": "block" })
                                    var css = $('#' + id).attr('style');
                                    var value = $(this).val();
                                    if (value.contains('^') == true)
                                    {
                                        $("#ddltxtValue").val(value.substring(1));
                                        $("#chkHindi").attr('checked', true);
                                    }
                                    else
                                    {
                                        $("#ddltxtValue").val($(this).val());
                                        $("#chkHindi").attr('checked', false);
                                    }


                                    var splitCss = css.split(';');
                                    $.each(splitCss, function (key, value)
                                    {
                                        if (value != "")
                                        {
                                            var spiltvalue = value.split(':');
                                            if (spiltvalue[0].indexOf("background-color") >= 0)
                                            {
                                                if (spiltvalue[1].toString().trim() != "transparent")
                                                {

                                                    $("#txtBgColor").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                                }
                                            }
                                            else if (spiltvalue[0].indexOf("color") >= 0)
                                            {

                                                $("#txtForeColor").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                            }
                                            else if (spiltvalue[0].indexOf("font-size") >= 0)
                                            {
                                                if (spiltvalue[1] != "")
                                                {
                                                    $("#ddlSize option[value=" + spiltvalue[1].replace("px", "") + "]").attr("selected", "selected");
                                                }
                                            }
                                            else if (spiltvalue[0].indexOf("font-family") >= 0)
                                            {


                                                $('#LstFont').val(spiltvalue[1].toLowerCase().trim());

                                                //$("#LstFont option[value =" + spiltvalue[1].toLowerCase() + "]").attr("selected", "selected");
                                            }
                                            else if (spiltvalue[0].indexOf("width") >= 0)
                                            {
                                                $("#txtWidth").val(spiltvalue[1].replace("px", ""));
                                            }
                                        }
                                    });
                                    HideShow(uniqueIdentity);
                                    $find('ModalPopupExtender1').show();

                                }); // end textbox double click
                                //on Div double click
                                $(this).find('div').dblclick(function ()
                                {
                                    var id = $(this).attr('id');
                                    if (id.indexOf("sep") >= 0)
                                    {

                                        uniqueIdentity = "5";

                                    } else if (id.indexOf("vsep") >= 0)
                                    {

                                        uniqueIdentity = "6";

                                    }
                                    else
                                    {
                                        uniqueIdentity = "4";
                                    }

                                    $("#GetId").val(id);
                                    $("#btnDelete").css({ "display": "block" })
                                    var css = $('#' + id).attr('style');

                                    var splitCss = css.split(';');
                                    $.each(splitCss, function (key, value)
                                    {
                                        if (value != "")
                                        {
                                            var spiltvalue = value.split(':');
                                            if (spiltvalue[0].indexOf("background-color") >= 0)
                                            {
                                                if (spiltvalue[1].trim() != "transparent")
                                                {
                                                    $("#txtBgColor").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                                } else
                                                {

                                                    $("#txtBgColor").val("");
                                                }
                                            }
                                            else if (spiltvalue[0].indexOf("width") >= 0)
                                            {
                                                if (spiltvalue[1] != "")
                                                {
                                                    $("#txtWidth").val(spiltvalue[1].replace("px", ""));
                                                }
                                            }
                                            else if (spiltvalue[0].indexOf("height") >= 0)
                                            {
                                                $("#txtHeight").val(spiltvalue[1].replace("px", ""));

                                            }
                                        }
                                    });
                                    HideShow(uniqueIdentity);

                                    $find('ModalPopupExtender1').show();

                                }); // end Div double click
                                //on image double click
                                $(this).find('img').dblclick(function ()
                                {

                                    var id = $(this).attr('id');
                                    uniqueIdentity = "3";
                                    $("#GetId").val(id);
                                    $("#btnDelete").css({ "display": "block" })
                                    var css = $('#' + id).attr('style');
                                    var splitCss = css.split(';');
                                    var e = $('#iFrame');
                                    e.attr("src", e.attr("src"));
                                    SetSession();
                                    $.each(splitCss, function (key, value)
                                    {
                                        if (value != "")
                                        {
                                            var spiltvalue = value.split(':');
                                            if (spiltvalue[0].indexOf("background-color") >= 0)
                                            {
                                                $("#txtBgColor").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                            }
                                            else if (spiltvalue[0].indexOf("width") >= 0)
                                            {
                                                if (spiltvalue[1] != "")
                                                {
                                                    $("#txtWidth").val(spiltvalue[1].replace("px", ""));
                                                }
                                            }
                                            else if (spiltvalue[0].indexOf("height") >= 0)
                                            {
                                                $("#txtHeight").val(spiltvalue[1].replace("px", ""));

                                            }
                                        }
                                    });
                                    HideShow(uniqueIdentity);
                                    $find('ModalPopupExtender1').show();

                                }); // end Div double click
                            });
                            if (uniqueIdentity == 1)
                            {
                                $("#TextBox2").val($("#TextBox2").val() + 1);
                            }
                            else if (uniqueIdentity == 2)
                            {
                                $("#label1").html($("#label1").html() + 1);
                            }
                            else if (uniqueIdentity == 4)
                            {
                                $("#label2").html($("#label2").html() + 1);
                            }
                            else if (uniqueIdentity == 3)
                            {
                                $("#label3").html($("#label3").html() + 1);
                            }
                            else if (uniqueIdentity == 5)
                            {
                                $("#label4").html($("#label4").html() + 1);
                            }
                            else if (uniqueIdentity == 6)
                            {
                                $("#label5").html($("#label5").html() + 1);
                            }
                        }
                        else
                        {

                            // edit  element css 
                            var border = "";

                            if ($("#txtBorderColor").val() == "")
                            {
                                border = "none";
                            }
                            else
                            {
                                border = "1px solid #" + $("#txtBorderColor").val();
                            }

                            if (uniqueIdentity == 1)
                            {

                                if ($('#chkHindi').attr('checked') == 'checked')
                                {
                                    $('#' + getid).attr("value", '^' + $("#ddltxtValue").val());
                                }
                                else
                                {
                                    $('#' + getid).attr("value", $("#ddltxtValue").val());
                                }

                                $('#' + getid).css({ "border": "" + border + "", "border-width": +$("#txtBorderWidth").val() + "px", "backgroundColor": "#" + $("#txtBgColor").val() + "", "color": "#" + $("#txtForeColor").val() + "", "font-size": "" + $('#ddlSize :selected').text() + "px", "font-family": "" + $('#LstFont :selected').text() + "", "width": "" + $("#txtWidth").val() + "px" + "" });
                            }
                            if (uniqueIdentity == 2)
                            {

                                $('#' + getid).text($("#txtValue").val());
                                $('#' + getid).css({ "border": "" + border + "", "border-width": +$("#txtBorderWidth").val() + "px", "backgroundColor": "#" + $("#txtBgColor").val() + "", "color": "#" + $("#txtForeColor").val() + "", "font-size": "" + $('#ddlSize :selected').text() + "px", "font-family": "" + $('#LstFont :selected').text() + "" });
                            }
                            if (uniqueIdentity == 4 || uniqueIdentity == 3 || uniqueIdentity == 5 || uniqueIdentity == 6)
                            {
                                if ($('#rdStatic').attr('checked') == 'checked')
                                {
                                    var imageName = getSession();

                                    if (imageName != "add_image.png")
                                    {
                                        $('#' + getid).attr('src', "uploadImg/" + imageName);
                                    }
                                }
                                else
                                {
                                    var img = $('#ddlImages').val();
                                    $('#' + getid).attr('src', "img/" + img + ".jpeg");
                                }
                                if ($("#chkTransparent").attr('checked') == 'checked' && uniqueIdentity != 3)
                                {

                                    $('#' + getid).css({ "border-color": +$("#txtBorderColor").val() + "px", "backgroundColor": "transparent", "width": "" + $("#txtWidth").val() + "px", "height": "" + $("#txtHeight").val() + "px" });

                                }
                                else
                                    $('#' + getid).css({ "border": "" + border + "", "border-width": +$("#txtBorderWidth").val() + "px", "backgroundColor": "#" + $("#txtBgColor").val() + "", "width": "" + $("#txtWidth").val() + "px", "height": "" + $("#txtHeight").val() + "px" });
                            }
                            getid = "";
                            $("#GetId").val("");
                        }
                    }
                });
                //end btnOkay
                // start  btnOkay1 (for Back side)
                $("#btnOkay1").click(function ()
                {

                    if ($("#txtValue1").val() == "" && $("#txtValue1").css("display") != "none")
                    {
                        alert("Please enter field name.");
                        setTimeout(function () { $find('ModalPopupExtender2').show(); }, 0);

                    }
                    else
                    {
                        var getid = $("#GetId").val();

                        if (getid == "")
                        {
                            var buffer = "";
                            $('#droppable1 li #l1').text($("#txtValue1").val());
                            //   $('#droppable1 li #t1').text($("#ddltxtValue1").val());
                            if ($("#txtBorderWidth").val() == "")
                            {
                                $("#txtBorderWidth").val("1");
                            }
                            if ($("#txtBorderColor1").val() == "")
                            {
                                $('#droppable1 li #l1').css({ "backgroundColor": "#" + $("#txtBgColor1").val() + "", "color": "#" + $("#txtForeColor1").val() + "", "font-size": "" + $('#ddlSize1 :selected').text() + "px", "font-family": "" + $('#LstFont1 :selected').text() + "" });
                                $('#droppable1 li #t1').css({ "backgroundColor": "#" + $("#txtBgColor1").val() + "", "color": "#" + $("#txtForeColor1").val() + "", "font-size": "" + $('#ddlSize1 :selected').text() + "px", "font-family": "" + $('#LstFont1 :selected').text() + "" });
                                if ($("#chkTransparent1").attr('checked') == 'checked')
                                {
                                    $('#droppable li #d1').css({ "border-width": "#" + $("#txtBorderWidth1").val() + "px", "backgroundColor": "transparent", "width": "" + $("#txtWidth").val() + "px", "height": "" + $("#txtHeight").val() + "px" });
                                }
                                $('#droppable1 li #d1').css({ "border-width": "#" + $("#txtBorderWidth1").val() + "px", "backgroundColor": "#" + $("#txtBgColor1").val() + "", "width": "" + $("#txtWidth1").val() + "px", "height": "" + $("#txtHeight1").val() + "px" });
                                $('#droppable1 li #i1').css({ "backgroundColor": "#" + $("#txtBgColor1").val() + "", "width": "" + $("#txtWidth1").val() + "px", "height": "" + $("#txtHeight1").val() + "px" });

                            }
                            else
                            {
                                $('#droppable1 li #l1').css({ "border": "1px solid #" + $("#txtBorderColor1").val() + "", "backgroundColor": "#" + $("#txtBgColor1").val() + "", "color": "#" + $("#txtForeColor1").val() + "", "font-size": "" + $('#ddlSize1 :selected').text() + "px", "font-family": "" + $('#LstFont1 :selected').text() + "" });
                                $('#droppable1 li #t1').css({ "border": "1px solid #" + $("#txtBorderColor1").val() + "", "backgroundColor": "#" + $("#txtBgColor1").val() + "", "color": "#" + $("#txtForeColor1").val() + "", "font-size": "" + $('#ddlSize1 :selected').text() + "px", "font-family": "" + $('#LstFont1 :selected').text() + "" });
                                if ($("#chkTransparent1").attr('checked') == 'checked')
                                {
                                    $('#droppable li #d1').css({ "border-width": "#" + $("#txtBorderWidth1").val() + "px", "border-color": "#" + $("#txtBorderColor").val() + "", "backgroundColor": "transparent", "width": "" + $("#txtWidth").val() + "px", "height": "" + $("#txtHeight").val() + "px" });
                                }
                                $('#droppable1 li #d1').css({ "border-width": "#" + $("#txtBorderWidth1").val() + "px", "border": "1px solid #" + $("#txtBorderColor1").val() + "", "backgroundColor": "#" + $("#txtBgColor1").val() + "", "width": "" + $("#txtWidth1").val() + "px", "height": "" + $("#txtHeight1").val() + "px" });
                                $('#droppable1 li #i1').css({ "border": "1px solid #" + $("#txtBorderColor1").val() + "", "backgroundColor": "#" + $("#txtBgColor1").val() + "", "width": "" + $("#txtWidth1").val() + "px", "height": "" + $("#txtHeight1").val() + "px" });

                            }

                            if ($('#rdStatic1').attr('checked') == 'checked')
                            {
                                var imageName = getSession();
                                $('#droppable1 li #i1').attr('src', "uploadImg/" + imageName);
                            }
                            else
                            {
                                var img = $('#ddlImages1').val();
                                $('#droppable1 li #i1').attr('src', "img/" + img + ".jpeg");
                            }
                            $('#droppable1 li #line1').css({ "backgroundColor": "#" + $("#txtBgColor1").val() + "", "width": "" + $("#txtWidth1").val() + "px", "height": "" + $("#txtHeight1").val() + "px" });
                            $('#droppable1 li #vline').css({ "backgroundColor": "#" + $("#txtBgColor1").val() + "", "width": "" + $("#txtWidth1").val() + "px", "height": "" + $("#txtHeight1").val() + "px" });
                            $("#droppable1 li").each(function ()
                            {
                                $('#droppable1 li #t1').attr('id', $("#TextBox2").val());
                                $('#droppable1 li #l1').attr('id', $("#label1").text());
                                $('#droppable1 li #d1').attr('id', $("#label2").text());
                                $('#droppable1 li #i1').attr('id', $("#label3").text());
                                $('#droppable1 li #line1').attr('id', $("#label4").text());
                                $('#droppable1 li #vline').attr('id', $("#label5").text());
                                buffer = buffer + $('#droppable1').find('li').html();
                                $('#droppable1').find('li').detach();
                            });

                            $("#droppable1").append(buffer);
                            if ($('#chkHindi1').attr('checked') == 'checked')
                            {
                                $('#' + $("#TextBox2").val()).attr("value", '^' + $("#ddltxtValue1").val());
                            }
                            else
                            {
                                $('#' + $("#TextBox2").val()).attr("value", $("#ddltxtValue1").val());
                            }

                            //$('#' + $("#TextBox2").val()).attr("data", $("#ddltxtValue1").val());

                            $("#droppable1").each(function ()
                            {
                                $(this).find('span,div').draggable({ containment: "parent", cursor: 'move' }).addClass("noclick").removeClass("setBorder");

                                $(this).find('img,input').draggable({ cancel: null, containment: "parent", cursor: 'move' });

                                //Click event start
                                $(this).find('span,div,img,input').unbind('click').bind('click', function (e)
                                {
                                    var id = $(this).attr('id');

                                    if (!$(this).hasClass("noclick"))
                                    {
                                        if ($(this).hasClass("setBorder"))
                                        {
                                            $("#" + id).removeClass("setBorder");
                                            getSelectedControl = getSelectedControl.replace(id + ",", "");
                                            $("#getControl").val(getSelectedControl);

                                            if (getSelectedControl == "")
                                            {
                                                $("#FontSize").show();

                                            } else
                                            {
                                                if (getSelectedControl.contains("Div") || getSelectedControl.contains("sep") || getSelectedControl.contains("img"))
                                                {
                                                    $("#FontSize").hide();
                                                }
                                            }
                                            if (getSelectedControl.contains("lbl") || getSelectedControl.contains("txt"))
                                            {
                                                $("#FontSize").show();
                                            }
                                        }
                                        else
                                        {
                                            $("#" + id).addClass("setBorder");
                                            getSelectedControl = id + "," + getSelectedControl;
                                            $("#getControl").val(getSelectedControl);

                                            if (getSelectedControl.contains("Div") || getSelectedControl.contains("sep") || getSelectedControl.contains("img"))
                                            {
                                                $("#FontSize").hide();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        $("#" + id).removeClass("setBorder noclick");
                                        getSelectedControl = getSelectedControl.replace(id + ",", "");
                                        $("#getControl").val(getSelectedControl);

                                    }
                                    if (getSelectedControl.contains("lbl") || getSelectedControl.contains("txt"))
                                    {
                                        $("#FontSize").show();
                                    }
                                    e.stopPropagation();
                                    return false;
                                });
                                //Click event end
                                //on double click
                                $(this).find('span').dblclick(function ()
                                {

                                    var id = $(this).attr('id');
                                    // if (id.indexOf("txt") >= 0) {
                                    // uniqueIdentity = "1";
                                    //  } else {
                                    uniqueIdentity = "2";
                                    // }
                                    $("#GetId").val(id);
                                    $("#btnDelete1").css({ "display": "block" })
                                    $("#txtValue1").val($(this).text());
                                    var css = $('#' + id).attr('style');
                                    var splitCss = css.split(';');
                                    $.each(splitCss, function (key, value)
                                    {

                                        if (value != "")
                                        {
                                            var spiltvalue = value.split(':');

                                            if (spiltvalue[0].indexOf("background-color") >= 0)
                                            {
                                                $("#txtBgColor1").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                            }
                                            else if (spiltvalue[0].indexOf("color") >= 0)
                                            {
                                                $("#txtForeColor1").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                            }
                                            else if (spiltvalue[0].indexOf("font-size") >= 0)
                                            {
                                                if (spiltvalue[1] != "")
                                                {
                                                    $("#ddlSize1 option[value=" + spiltvalue[1].replace("px", "") + "]").attr("selected", "selected");
                                                }
                                            }
                                            else if (spiltvalue[0].indexOf("font-family") >= 0)
                                            {
                                                $('#LstFont1').val(spiltvalue[1].toLowerCase().trim());
                                                //  $("#LstFont1 option[value=" + spiltvalue[1] + "]").attr("selected", "selected");
                                            }
                                        }
                                    });
                                    HideShowBackSide(uniqueIdentity);
                                    $find('ModalPopupExtender2').show();

                                });
                                //on TextBox double click
                                $(this).find('input').dblclick(function ()
                                {
                                    var id = $(this).attr('id');
                                    uniqueIdentity = "1";
                                    $("#GetId").val(id);
                                    $("#btnDelete1").css({ "display": "block" });
                                    var value = $(this).val();
                                    if (value.contains('^') == true)
                                    {
                                        $("#ddltxtValue1").val(value.substring(1));
                                        $("#chkHindi1").attr('checked', true);
                                    }
                                    else
                                    {
                                        $("#ddltxtValue1").val($(this).val());
                                        $("#chkHindi1").attr('checked', false);
                                    }

                                    var css = $('#' + id).attr('style');
                                    var splitCss = css.split(';');
                                    $.each(splitCss, function (key, value)
                                    {

                                        if (value != "")
                                        {
                                            var spiltvalue = value.split(':');

                                            if (spiltvalue[0].indexOf("background-color") >= 0)
                                            {
                                                if (spiltvalue[1].toString().trim() != "transparent")
                                                {
                                                    $("#txtBgColor1").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                                }
                                            }
                                            else if (spiltvalue[0].indexOf("color") >= 0)
                                            {
                                                $("#txtForeColor1").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                            }
                                            else if (spiltvalue[0].indexOf("font-size") >= 0)
                                            {
                                                if (spiltvalue[1] != "")
                                                {
                                                    $("#ddlSize1 option[value=" + spiltvalue[1].replace("px", "") + "]").attr("selected", "selected");
                                                }
                                            }
                                            else if (spiltvalue[0].indexOf("font-family") >= 0)
                                            {
                                                $('#LstFont1').val(spiltvalue[1].toLowerCase().trim());
                                                //  $("#LstFont1 option[value=" + spiltvalue[1] + "]").attr("selected", "selected");
                                            }
                                            else if (spiltvalue[0].indexOf("width") >= 0)
                                            {
                                                $("#txtWidth1").val(spiltvalue[1].replace("px", ""));
                                            }
                                        }
                                    });
                                    HideShowBackSide(uniqueIdentity);
                                    $find('ModalPopupExtender2').show();

                                }); // end textbox double click
                                //on Div double click
                                $(this).find('div').dblclick(function ()
                                {
                                    var id = $(this).attr('id');
                                    if (id.indexOf("sep") >= 0)
                                    {

                                        uniqueIdentity = "5";

                                    } else if (id.indexOf("vsep") >= 0)
                                    {

                                        uniqueIdentity = "6";

                                    }
                                    else
                                    {
                                        uniqueIdentity = "4";
                                    }
                                    $("#GetId").val(id);
                                    $("#btnDelete1").css({ "display": "block" })
                                    // $("#ddltxtValue").val();
                                    var css = $('#' + id).attr('style');
                                    var splitCss = css.split(';');
                                    $.each(splitCss, function (key, value)
                                    {

                                        if (value != "")
                                        {
                                            var spiltvalue = value.split(':');


                                            if (spiltvalue[0].indexOf("background-color") >= 0)
                                            {

                                                if (spiltvalue[1].trim() != "transparent")
                                                {
                                                    $("#txtBgColor1").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                                } else
                                                {

                                                    $("#txtBgColor1").val("");
                                                }
                                            }
                                            else if (spiltvalue[0].indexOf("width") >= 0)
                                            {
                                                if (spiltvalue[1] != "")
                                                {
                                                    $("#txtWidth1").val(spiltvalue[1].replace("px", ""));
                                                }
                                            }
                                            else if (spiltvalue[0].indexOf("height") >= 0)
                                            {
                                                $("#txtHeight1").val(spiltvalue[1].replace("px", ""));

                                            }
                                        }
                                    });
                                    HideShowBackSide(uniqueIdentity);
                                    $find('ModalPopupExtender2').show();

                                }); // end Div double click
                                //on image double click
                                $(this).find('img').dblclick(function ()
                                {
                                    var id = $(this).attr('id');
                                    uniqueIdentity = "3";
                                    $("#GetId").val(id);
                                    $("#btnDelete1").css({ "display": "block" })
                                    var e = $('#iFrame1');
                                    e.attr("src", e.attr("src"));
                                    SetSession();
                                    var css = $('#' + id).attr('style');
                                    var splitCss = css.split(';');

                                    $.each(splitCss, function (key, value)
                                    {

                                        if (value != "")
                                        {
                                            var spiltvalue = value.split(':');

                                            if (spiltvalue[0].indexOf("background-color") >= 0)
                                            {
                                                if (spiltvalue[1].trim() != "transparent")
                                                {
                                                    $("#txtBgColor1").val(colorToHex(spiltvalue[1]).replace("#", "").toUpperCase())
                                                } else { $("#txtBgColor1").val(""); }
                                            }
                                            else if (spiltvalue[0].indexOf("width") >= 0)
                                            {
                                                if (spiltvalue[1] != "")
                                                {
                                                    $("#txtWidth1").val(spiltvalue[1].replace("px", ""));
                                                }
                                            }
                                            else if (spiltvalue[0].indexOf("height") >= 0)
                                            {
                                                $("#txtHeight1").val(spiltvalue[1].replace("px", ""));

                                            }

                                        }
                                    });
                                    HideShowBackSide(uniqueIdentity);
                                    $find('ModalPopupExtender2').show();

                                }); // end image double click

                            });
                            if (uniqueIdentity == 1)
                            {
                                $("#TextBox2").val($("#TextBox2").val() + 1);
                            }
                            else if (uniqueIdentity == 2)
                            {
                                $("#label1").html($("#label1").html() + 1);
                            }
                            else if (uniqueIdentity == 4)
                            {
                                $("#label2").html($("#label2").html() + 1);
                            }
                            else if (uniqueIdentity == 3)
                            {
                                $("#label3").html($("#label3").html() + 1);
                            }
                            else if (uniqueIdentity == 5)
                            {
                                $("#label4").html($("#label4").html() + 1);
                            }
                            else if (uniqueIdentity == 6)
                            {
                                $("#label5").html($("#label5").html() + 1);
                            }
                        }
                        else
                        {
                            var border1 = "";

                            if ($("#txtBorderColor1").val() == "")
                            {
                                border1 = "none";
                            }
                            else
                            {
                                border1 = "1px solid #" + $("#txtBorderColor1").val();
                            }
                            if (uniqueIdentity == 1)
                            {
                                if ($('#chkHindi1').attr('checked') == 'checked')
                                {
                                    $('#' + getid).attr("value", '^' + $("#ddltxtValue1").val());
                                }
                                else
                                {
                                    $('#' + getid).attr("value", $("#ddltxtValue1").val());
                                }

                                $('#' + getid).css({ "border": "" + border1 + "", "backgroundColor": "#" + $("#txtBgColor1").val() + "", "color": "#" + $("#txtForeColor1").val() + "", "font-size": "" + $('#ddlSize1 :selected').text() + "px", "font-family": "" + $('#LstFont1 :selected').text() + "", "width": "" + $("#txtWidth1").val() + "px" + "" });
                            }
                            if (uniqueIdentity == 2)
                            {
                                $('#' + getid).text($("#txtValue1").val());
                                $('#' + getid).css({ "border": "" + border1 + "", "backgroundColor": "#" + $("#txtBgColor1").val() + "", "color": "#" + $("#txtForeColor1").val() + "", "font-size": "" + $('#ddlSize1 :selected').text() + "px", "font-family": "" + $('#LstFont1 :selected').text() + "" });
                            }
                            if (uniqueIdentity == 4 || uniqueIdentity == 3 || uniqueIdentity == 5 || uniqueIdentity == 6)
                            {
                                if ($('#rdStatic1').attr('checked') == 'checked')
                                {
                                    var imageName = getSession();
                                    if (imageName != "add_image.png")
                                    {
                                        $('#' + getid).attr('src', "uploadImg/" + imageName);
                                    }
                                }
                                else
                                {
                                    var img = $('#ddlImages1').val();
                                    $('#' + getid).attr('src', "img/" + img + ".jpeg");
                                }
                                if ($("#chkTransparent1").attr('checked') == 'checked' && uniqueIdentity != 3)
                                {

                                    $('#' + getid).css({ "border-color": "#" + $("#txtBorderColor1").val() + "", "backgroundColor": "transparent", "width": "" + $("#txtWidth1").val() + "px", "height": "" + $("#txtHeight1").val() + "px" });

                                }
                                else
                                    $('#' + getid).css({ "border": "" + border1 + "", "backgroundColor": "#" + $("#txtBgColor1").val() + "", "width": "" + $("#txtWidth1").val() + "px", "height": "" + $("#txtHeight1").val() + "px" });
                            }
                            getid = "";
                            $("#GetId").val("");
                        }
                    }
                });
                // ok1 end
                //Radio Button click event
                $("#rdbFront").click(function ()
                {
                    $("#SelectField,#BackField,#tdSaveBack,#tdSaveBoth").hide();
                    $("#FrontField").show();
                    $("#rdSaveFront").attr('checked', 'checked');

                });
                $("#rdbBack").click(function ()
                {
                    $("#rbdFrontSide,#rdSaveBoth").attr('checked', 'checked');
                    $("#rdSaveFront").removeAttr('checked');
                    $("#SelectField,#tdSaveBack,#tdSaveBoth").show();
                });
                $("#rbdFrontSide").click(function ()
                {
                    $("#FrontField").show();
                    $("#BackField").hide();
                });
                $("#rbdBackSide").click(function ()
                {
                    $("#FrontField").hide();
                    $("#BackField").show();
                    $('#Div3').ruler({
                        vRuleSize: 18,
                        hRuleSize: 18,
                        showCrosshair: true,
                        showMousePos: false
                    });
                });

                $("#btnCancel").click(function ()
                {
                    $find('ModalPopupExtender1').hide();
                    $('#droppable li').remove();
                    $('#GetId').val("");
                });
                $("#btnCancel1").click(function ()
                {
                    $('#droppable1 li').remove();
                    $('#GetId').val("");
                });
                $("#btnDelete").click(function ()
                {
                    $('#' + $("#GetId").val()).remove();
                    $('#GetId').val("");
                    $find('ModalPopupExtender1').hide();

                });
                $("#btnDelete1").click(function ()
                {
                    $('#' + $("#GetId").val()).remove();
                    $('#GetId').val("");
                    $find('ModalPopupExtender2').hide();
                });
                $("#btnSave").click(function ()
                {
                    $("#trEdit,#trEdit1").hide();
                    $("#trNew").show();
                    $('#rdNew').attr('checked', 'checked');
                    $find('ModalPopupExtender3').show();

                });
                // save Template 
                $("#btnTemplateSave").click(function ()
                {

                    $('#droppable,#droppable1').css({ 'overflow': 'hidden' });
                    var hfFrontVal = $('#ContentPlaceHolder1_ContentPlaceHolder1_saveDiv').html();
                    var hfBackVal = $('#ContentPlaceHolder1_ContentPlaceHolder1_saveDiv1').html();
                    $('#droppable,#droppable1').css({ 'overflow': 'visible' });
                    var selectedVal = "";
                    var selected = $("input[type='radio'][name='Save']:checked");
                    if (selected.length > 0)
                    {
                        selectedVal = selected.val();
                    }
                    var Flag = "New";
                    var tepName = "";
                    if ($('#rdEdit').attr('checked') == 'checked')
                    {
                        tepName = $('#drpTemplate').val();

                        if (tepName == "0" && $("#drpTemplate").css("display") != "none")
                        {
                            alert("Please select Template name.");
                            setTimeout(function () { $find('ModalPopupExtender3').show(); }, 0);
                            return false;
                        }
                        Flag = "Edit";
                    } else
                    {
                        tepName = $('#txtTemplateName').val();

                        if (tepName == "" && $("#txtTemplateName").css("display") != "none")
                        {
                            alert("Please enter Template name.");
                            setTimeout(function () { $find('ModalPopupExtender3').show(); }, 0);
                            return false;

                        }

                        Flag = "New";

                    }

                    $.ajax({
                        url: "Card_Design.aspx/SaveToDataBase",
                        type: "POST",
                        dataType: "json",
                        // data: dataValue,
                        data: "{'TemplateName':'" + tepName + "', 'divFronthtml': '" + hfFrontVal + "','divBackhtml': '" + hfBackVal + "','empid': '" + $('#UserId').val() + "','SaveTemplate': '" + selectedVal + "','Flag': '" + Flag + "','Category':'" + $('#drpCategory').val() + "','CatFlag':'False'}",
                        contentType: "application/json; charset=utf-8",
                        success: function (msg)
                        {
                            if (msg.d == '1')
                            {
                                alert("Success");

                                // window.location = "SaveTemplate.aspx";
                            }
                            else
                            {
                                var conf = confirm("This category already linked with other template, do you want to unlinked ?");
                                if (conf == true)
                                {

                                    $.ajax({
                                        url: "Card_Design.aspx/SaveToDataBase",
                                        type: "POST",
                                        dataType: "json",
                                        // data: dataValue,
                                        data: "{'TemplateName':'" + tepName + "', 'divFronthtml': '" + hfFrontVal + "','divBackhtml': '" + hfBackVal + "','empid': '" + $('#UserId').val() + "','SaveTemplate': '" + selectedVal + "','Flag': '" + Flag + "','Category':'" + $('#drpCategory').val() + "','CatFlag':'True'}",
                                        contentType: "application/json; charset=utf-8",
                                        success: function (msg)
                                        {
                                            alert("Success");
                                        }
                                    });

                                }
                                else
                                {
                                    return false;
                                }

                            }
                        },
                        error: function () { alert(arguments[2]); }
                    });
                });
                $("#liPortrait").click(function ()
                {
                    $("#liPortrait").css({ "color": "red" });
                    $("#liLandScape").css({ "color": "black" });
                    $("#droppable ,#droppable1").css({ "width": "5.4cm", "height": "8.5cm" });
                    $("#FrontField ,#BackField").css({ "width": "6cm" });
                    $("#d1").addClass("Psize").removeClass("Lsize");

                    $('#Div3').ruler({
                        vRuleSize: 18,
                        hRuleSize: 18,
                        showCrosshair: true,
                        showMousePos: false
                    });


                });
                $("#liLandScape").click(function ()
                {
                    $("#liLandScape").css({ "color": "red" });
                    $("#liPortrait").css({ "color": "Black" });
                    $("#droppable,#droppable1").css({ "width": "8.5cm", "height": "5.4cm" });
                    $("#d1").addClass("Lsize").removeClass("Psize");

                    $('#Div3').ruler({
                        vRuleSize: 18,
                        hRuleSize: 18,
                        showCrosshair: true,
                        showMousePos: false
                    });


                });
                $("#ibtnDelete").click(function ()
                {
                    if (getSelectedControl != "")
                    {
                        var varControl = getSelectedControl.split(',');
                        $.each(varControl, function (index, value)
                        {
                            if (value != "")
                            {
                                $("#" + value).remove();
                            }
                        });
                        getSelectedControl = "";
                    }
                    else
                    {
                        alert("Please select atleast one control.");
                    }
                });
                $("#lidrpFontFamily").change(function ()
                {
                    if (getSelectedControl != "")
                    {
                        var varControl = getSelectedControl.split(',');
                        $.each(varControl, function (index, value)
                        {
                            if (value != "")
                            {
                                $("#" + value).css({ "font-family": $("#lidrpFontFamily").val() });
                            }
                        });
                    }
                    else
                    {
                        alert("Please select atleast one control.");
                    }

                });
                $("#lidrpFontSize").change(function ()
                {
                    if (getSelectedControl != "")
                    {
                        var varControl = getSelectedControl.split(',');
                        $.each(varControl, function (index, value)
                        {
                            if (value != "")
                            {
                                $("#" + value).css({ "font-size": $("#lidrpFontSize").val() + "px" });
                            }
                        });
                    }
                    else
                    {
                        alert("Please select atleast one control.");
                    }

                });
                $("#rdStatic").click(function ()
                {
                    $("#rdDynamic").attr('checked', false);
                    $("#rdStatic").attr('checked', 'checked');
                    $("#spanStatic").show();
                    $("#SpanDynamic").hide();
                });
                $("#rdDynamic").click(function ()
                {

                    $("#rdDynamic").attr('checked', 'checked');
                    $("#rdStatic").attr('checked', false);
                    $("#SpanDynamic").show();
                    $("#spanStatic").hide();
                });
                $("#rdStatic1").click(function ()
                {
                    $("#spanStatic1").show();
                    $("#SpanDynamic1").hide();
                });
                $("#rdDynamic1").click(function ()
                {
                    $("#SpanDynamic1").show();
                    $("#spanStatic1").hide();
                });
                $("#rdNew").click(function ()
                {
                    $("#trNew").show();
                    $("#trEdit").hide();
                    $("#trEdit1").hide();
                });
                $("#rdEdit").click(function ()
                {
                    $("#trNew").hide();
                    $("#trEdit").show();
                    $("#trEdit1").show();
                });
                $("#btnLeft").click(function ()
                {
                    if (getSelectedControl != "")
                    {
                        var varControl = getSelectedControl.split(',');
                        $.each(varControl.reverse(), function (index, value)
                        {
                            if (value != "")
                            {
                              
                                var css = $('#' + value).attr('style');
                                var splitCss = css.split(';');

                              
                            }
                        });
                    }
                    else
                    {
                        alert("Please select atleast one control.");
                    }
                });

            });
        }
        catch (e) {
            alert(e);
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="DivBody">
        <asp:HiddenField ID="hf" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="UserId" runat="server" ClientIDMode="Static" />
        <%--    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>--%>
        <obout:ColorPickerExtender runat="server" ID="ColorPickerExtender1" TargetProperty="style.backgroundColor"
            PickButton="false" PopupButtonID="btnPicker" TargetControlID="TextBox1" OnClientPicked="colorChanged" />
        <obout:ColorPickerExtender runat="server" ID="ColorPickerExtender2" TargetProperty="style.backgroundColor"
            PickButton="false" PopupButtonID="btnPicker1" TargetControlID="TextBox3" OnClientPicked="colorChanged1" />
        <obout:ColorPickerExtender runat="server" ID="ColorPickerExtender7" TargetProperty="style.backgroundColor"
            PickButton="false" PopupButtonID="ibtnbgColor" TargetControlID="litxtbgColor"
            OnClientPicked="colorChanged3" />
        <obout:ColorPickerExtender runat="server" ID="ColorPickerExtender8" TargetProperty="style.backgroundColor"
            PickButton="false" PopupButtonID="ibtnForeColor" TargetControlID="litxtforeColor"
            OnClientPicked="colorChanged3" />
        <div style="width: 30%">
            <ul id="draggable">
                Tools
                <li uniqueidentity="1" title="Dynamic Text">
                    <input value="TextBox" id="t1" style="border: 1px; background-color: transparent;
                        overflow: hidden; width: 150px;" wrap="false"  />
                    <%--<asp:TextBox runat="server" ID="t1" Text="TextBox"  style="border:1px;background-color: transparent; width:150px;height:auto;"></asp:TextBox>--%></li>
                <li uniqueidentity="2" title="Static Text">
                    <asp:Label runat="server" ClientIDMode="Static" ID="l1" Text="Label" Style="border: 1px; color:#000;
                        width: auto; height: auto; white-space: nowrap;" class="comment-text "></asp:Label></li>
                <li uniqueidentity="3" title="Image Box">
                    <img style="width: 35px; height: 25px;" id="i1" src="img/add_image.png" data="findImg"
                        class="Lsize" />
                    <%-- <asp:Image runat="server" ImageUrl="~/img/add_image.png"  ID = "i1" data="abc"/>--%></li>
                <li uniqueidentity="4" >
                    <div style="width: 30px; height: 20px; border: 1px solid black" id="d1" class="Lsize">
                    </div>
                </li>
                <li uniqueidentity="5" title="Horizontal Separator" >
                    <div style="width: 30px; height: 3px; border: 1px solid black; background-color: #000;"
                        id="line1" class="Lsize" >
                    </div>
                </li>
                <li uniqueidentity="6" title="Vertical Separator">
                    <div style="width: 3px; height: 30px; border: 1px solid black; background-color: #000;"
                        id="vline" class="Lsize" >
                    </div>
                </li>
                Card Orientation
                <li id="liPortrait">
                    <asp:TextBox runat="server" ID="txtPortrait" Style="width: 4px; height: 8px; border: 1px solid black"
                        ClientIDMode="Static"></asp:TextBox>
                    Portrait</li>
                <li id="liLandScape" style="color: Red">
                    <asp:TextBox runat="server" ID="TextBox4" Style="width: 10px; height: 4px; border: 1px solid black"
                        ClientIDMode="Static"></asp:TextBox>
                    Landscape</li>
                Control Box
                <%--   <li id="aligntop">Align left</li>--%>
                <li id="Li1" title="Background color">
                    <asp:TextBox runat="server" ID="litxtbgColor" Style="width: 0px; height: 0px; border: 1px solid white;"
                        ClientIDMode="Static"></asp:TextBox>
                    <asp:ImageButton ID="ibtnBgColor" runat="server" ImageUrl="~/img/bgcolor1.jpeg" Width="20px"
                        ClientIDMode="Static" Height="20px" /></li>
                <li id="Li2" title="Font color">
                    <asp:TextBox runat="server" ID="litxtforeColor" Style="width: 0px; height: 0px; border: 1px solid white;"
                        ClientIDMode="Static"></asp:TextBox>
                    <asp:ImageButton ID="ibtnForeColor" runat="server" ImageUrl="~/img/ForeColor1.jpeg"
                        ClientIDMode="Static" Width="20px" Height="20px" />
                </li>
                <li id="Li3" title="Delete">
                    <img id="ibtnDelete" alt="Delete" src="img/delete.jpg" style="margin-left: 5px" width="20px"
                        height="20px" />
                </li>
                <li id="FontSize">
                    <table>
                        <tr>
                            <td>
                                <div>
                                    <asp:DropDownList ID="lidrpFontFamily" runat="server" Width="60%" ClientIDMode="Static">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="lidrpFontSize" runat="server" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                    </table>
                </li>
                <li id="liLeft"  style="display:none;">
                    <table>
                        <tr>
                            <td>
                                <div>
                                <input  id="btnLeft" value="Left" type="button"/>
                                </div>
                            </td>
                        </tr>
                    </table>
                </li>
                <li id="Li4">
                    <table>
                        <tr>
                            <td>
                                <div>
                                    <asp:Button ID="btnPicker" runat="server" Text="Background Color" />
                                    <asp:TextBox ID="TextBox1" runat="server" ClientIDMode="Static" Style="width: 0px;
                                        height: 0px; border: 1px solid white;"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </li>
                <%-- <img  runat="server" id="img1" />--%>
                <asp:Button ID="btnSave" Text="Save" runat="server" OnClientClick="return ConfirmSave()"
                    CssClass="ButtonControl" ClientIDMode="Static" />
                <div style="float: right;">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClientClick="return ConfirmReset()"
                        CssClass="ButtonControl" ClientIDMode="Static" OnClick="btnReset_Click" /></div>
                <br />
                <br />
                <asp:Button ID="btnPrint" runat="server" Text="Print" Width="100%" OnClick="btnPrint_Click"
                    ClientIDMode="Static" CssClass="ButtonControl" />
            </ul>
        </div>
        <div style="float: left; margin-right: 30px; margin: 10px; width: 70%">
            <%--  <input  id="btnNewTemplate" value="New Template" type="radio"   name="Template"  checked="checked" onclick="return ConfirmReset()"    /> New Template
               <input  id="btnEditTemplate" value="Edit Template" type="radio" name="Template" onclick="EditTemplate()" /> Edit Template--%>
            <br />
            <asp:RadioButton ID="rdbFront" runat="server" GroupName="Side" Text="One Side" ClientIDMode="Static" />
            <asp:RadioButton ID="rdbBack" runat="server" GroupName="Side" Text="Two Side" Checked="true"
                ClientIDMode="Static" />
            <fieldset id="SelectField" style="padding: 3px 3px 3px 3px;">
                <legend>Select Side </legend>
                <div>
                    <asp:RadioButton ID="rbdFrontSide" runat="server" GroupName="SelectSide" Text="Front Side"
                        ClientIDMode="Static" Checked="true" />
                    <asp:RadioButton ID="rbdBackSide" runat="server" GroupName="SelectSide" Text="Back Side"
                        ClientIDMode="Static" />
                </div>
            </fieldset>
            <div id="Div3" style="padding-left: 0px; width: 9cm; height: auto;">
                <fieldset id="FrontField" style="padding: 9px 9px 9px 9px; width: 9cm; margin: 18px;
                    margin-left: 22px;">
                    <legend>Front Side </legend>
                    <div id="CrossHair" >
                    <div id="saveDiv" runat="server">
                        <div id="droppable" style="border: 1px solid black; width: 8.5cm; height: 5.4cm;
                            background-color: #CCCCCC;">
                        </div>
                        </div>
                    </div>
                </fieldset>
                <fieldset id="BackField" style="display: none; padding: 9px 9px 9px 9px; margin: 18px;
                    margin-left: 22px;">
                    <legend>Back Side </legend>
                    <div style="float: right; display: none;">
                        <asp:Button ID="btnPicker1" runat="server" Text="Choose Color" />
                        <asp:TextBox ID="TextBox3" runat="server" ClientIDMode="Static"></asp:TextBox></div>
                    <div id="CrossHair1" >
                    <div id="saveDiv1" runat="server">
                        <div id="droppable1" style="border: 1px solid black; width: 8.5cm; height: 5.4cm;
                            background-color: #CCCCCC;">
                        </div>
                    </div>
                    </div>
                </fieldset>
            </div>
        </div>
        <asp:TextBox runat="server" ID="TextBox2" Text="txt" CssClass="ModalButton" ClientIDMode="Static"></asp:TextBox>
        <asp:Label runat="server" ID="label1" Text="lbl" CssClass="ModalButton" ClientIDMode="Static"></asp:Label>
        <asp:Label runat="server" ID="label2" Text="DynamicDiv" CssClass="ModalButton" ClientIDMode="Static"></asp:Label>
        <asp:Label runat="server" ID="label3" Text="img" CssClass="ModalButton" ClientIDMode="Static"></asp:Label>
        <asp:Label runat="server" ID="label4" Text="sep" CssClass="ModalButton" ClientIDMode="Static"></asp:Label>
        <asp:Label runat="server" ID="label5" Text="vsep" CssClass="ModalButton" ClientIDMode="Static"></asp:Label>
        <asp:HiddenField runat="server" ID="GetId" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="getControl" ClientIDMode="Static" />
        <asp:Button ID="Button1" runat="server" Text="Button" CssClass="ModalButton" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancel"
            ClientIDMode="Static" OkControlID="btnOkay" TargetControlID="Button1" PopupControlID="Panel1"
            PopupDragHandleControlID="PopupHeader" BackgroundCssClass="ModalPopupBG">
        </ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" CancelControlID="btnCancel1"
            ClientIDMode="Static" OkControlID="btnOkay1" TargetControlID="Button1" PopupControlID="Panel2"
            PopupDragHandleControlID="PopupHeader" BackgroundCssClass="ModalPopupBG">
        </ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" CancelControlID="btntmpClose"
            ClientIDMode="Static" OkControlID="btnTemplateSave" TargetControlID="Button1"
            PopupControlID="Panel3" PopupDragHandleControlID="PopupHeader" BackgroundCssClass="ModalPopupBG">
        </ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" CancelControlID="btnEditClose"
            ClientIDMode="Static" OkControlID="btnDemo" TargetControlID="Button1" PopupControlID="Panel4"
            PopupDragHandleControlID="PopupHeader" BackgroundCssClass="ModalPopupBG">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="Panel1" Style="display: none" runat="server" CssClass="PopupPanel">
            <div>
                <div class="PopupHeader" id="PopupHeader">
                    SET VALUE</div>
                <div class="PopupBody">
                    <table>
                        <tr id="trfieldName">
                            <td>
                                Enter Field Name : <font color="red">*</font>
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtValue" TabIndex="0" ClientIDMode="Static"></asp:TextBox>
                                <%--  <input id="txtValue" type="text" tabindex="0" value="" />--%>
                                <asp:DropDownList runat="server" ID="ddltxtValue" CssClass="ModalButton" ClientIDMode="Static">
                                </asp:DropDownList>
                                <input  id="chkHindi" type="checkbox" value="Is Hindi"/> Is Hindi
                                <asp:Label ID="lblerror" runat="server" ClientIDMode="Static"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trForeColor">
                            <td>
                                Fore color :
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtForeColor" ClientIDMode="Static"></asp:TextBox>
                                <obout:ColorPickerExtender runat="server" ID="ColorPickerExtender4" TargetProperty="style.backgroundColor"
                                    PickButton="false" PopupButtonID="txtForeColor" TargetControlID="txtForeColor"
                                    OnClientPicked="colorChanged2" />
                            </td>
                        </tr>
                        <tr id="trBorderColor">
                            <td>
                                Border color :
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtBorderColor" ClientIDMode="Static"></asp:TextBox>
                                <obout:ColorPickerExtender runat="server" ID="ColorPickerExtender10" TargetProperty="style.backgroundColor"
                                    PickButton="false" PopupButtonID="txtBorderColor" TargetControlID="txtBorderColor"
                                    OnClientPicked="colorChanged2" />
                            </td>
                        </tr>
                        <tr id="trBorderWidth">
                            <td>
                                Border Width :
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtBorderWidth" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Background color :
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtBgColor" ClientIDMode="Static"></asp:TextBox>
                                <obout:ColorPickerExtender runat="server" ID="ColorPickerExtender3" TargetProperty="style.backgroundColor"
                                    PickButton="false" PopupButtonID="txtBgColor" TargetControlID="txtBgColor" OnClientPicked="colorChanged2" />
                            </td>
                        </tr>
                        <tr id="trTransparent">
                            <td colspan="3">
                                <input type="checkbox" id="chkTransparent" value="Transparent" />
                                Transparent
                            </td>
                        </tr>
                        <tr id="trFontType">
                            <td>
                                Font Type :
                            </td>
                            <td colspan="2">
                                <asp:ListBox runat="server" ID="LstFont" ClientIDMode="Static"></asp:ListBox>
                            </td>
                        </tr>
                        <tr id="trFontSize">
                            <td>
                                Font Size :
                            </td>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="ddlSize" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trWidth">
                            <td>
                                Width (px):
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtWidth" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trheight">
                            <td>
                                Height (px):
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtHeight" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="staticImg">
                            <td>
                                <input type="radio" name="state" value="static" id="rdStatic" checked="checked" />Static
                            </td>
                            <td colspan="2">
                                <span id="spanStatic">
                                    <iframe src="iFrame.aspx" id="iFrame" style="border: none; height: 110px; width: auto;
                                        overflow: visible" scrolling="no"></iframe>
                                </span>
                            </td>
                        </tr>
                        <tr id="DynamicImg">
                            <td>
                                <input type="radio" name="state" value="Dynamic" id="rdDynamic" />Dynamic
                            </td>
                            <td>
                                <span id="SpanDynamic" style="display: none">
                                    <asp:DropDownList ID="ddlImages" runat="server" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input id="btnOkay" type="button" value="Done" runat="server" clientidmode="Static" />
                            </td>
                            <td>
                                <input id="btnCancel" type="button" value="Cancel" />
                            </td>
                            <td>
                                <input id="btnDelete" type="button" value="Delete" style="display: none" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel2" Style="display: none" runat="server" CssClass="PopupPanel">
            <div>
                <div class="PopupHeader" id="Div1">
                    SET VALUE</div>
                <div class="PopupBody">
                    <table>
                        <tr id="trfieldName1">
                            <td>
                                Enter Field Name :<font color="red"> *</font>
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtValue1" ClientIDMode="Static"></asp:TextBox>
                                <asp:DropDownList runat="server" ID="ddltxtValue1" CssClass="ModalButton" ClientIDMode="Static">
                                </asp:DropDownList>
                                <input  type="checkbox" id="chkHindi1" value="Is Hindi"/> Is Hindi
                            </td>
                        </tr>
                        <tr id="trForeColor1">
                            <td>
                                Fore color :
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtForeColor1" ClientIDMode="Static"></asp:TextBox>
                                <obout:ColorPickerExtender runat="server" ID="ColorPickerExtender5" TargetProperty="style.backgroundColor"
                                    PickButton="false" PopupButtonID="txtForeColor1" TargetControlID="txtForeColor1"
                                    OnClientPicked="colorChanged2" />
                            </td>
                        </tr>
                        <tr id="trBorderColor1">
                            <td>
                                Border color :
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtBorderColor1" ClientIDMode="Static"></asp:TextBox>
                                <obout:ColorPickerExtender runat="server" ID="ColorPickerExtender9" TargetProperty="style.backgroundColor"
                                    PickButton="false" PopupButtonID="txtBorderColor1" TargetControlID="txtBorderColor1"
                                    OnClientPicked="colorChanged2" />
                            </td>
                        </tr>
                        <tr id="trBorderWidth1">
                            <td>
                                Border Width :
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtBorderWidth1" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Background color :
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtBgColor1" ClientIDMode="Static"></asp:TextBox>
                                <obout:ColorPickerExtender runat="server" ID="ColorPickerExtender6" TargetProperty="style.backgroundColor"
                                    PickButton="false" PopupButtonID="txtBgColor1" TargetControlID="txtBgColor1"
                                    OnClientPicked="colorChanged2" />
                            </td>
                        </tr>
                        <tr id="trTransparent1">
                            <td colspan="3">
                                <input type="checkbox" id="chkTransparent1" value="Transparent" />
                                Transparent
                            </td>
                        </tr>
                        <tr id="trFontType1">
                            <td>
                                Font Type :
                            </td>
                            <td colspan="2">
                                <asp:ListBox runat="server" ID="LstFont1" ClientIDMode="Static"></asp:ListBox>
                            </td>
                        </tr>
                        <tr id="trFontSize1">
                            <td>
                                Font Size :
                            </td>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="ddlSize1" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trWidth1">
                            <td>
                                Width (px):
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtWidth1" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trheight1">
                            <td>
                                Height (px):
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtHeight1" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="staticImg1">
                            <td>
                                <input type="radio" name="state1" value="static" id="rdStatic1" checked="checked" />Static
                            </td>
                            <td colspan="2">
                                <span id="spanStatic1">
                                    <iframe src="iFrame.aspx" id="iFrame1" style="border: none; height: 110px; width: auto;
                                        overflow: visible" scrolling="no"></iframe>
                                </span>
                            </td>
                        </tr>
                        <tr id="DynamicImg1">
                            <td>
                                <input type="radio" name="state1" value="Dynamic" id="rdDynamic1" />Dynamic
                            </td>
                            <td>
                                <span id="SpanDynamic1" style="display: none">
                                    <asp:DropDownList ID="ddlImages1" runat="server">
                                    </asp:DropDownList>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input id="btnOkay1" type="button" value="Done" runat="server" clientidmode="Static" />
                            </td>
                            <td>
                                <input id="btnCancel1" type="button" value="Cancel" />
                            </td>
                            <td>
                                <input id="btnDelete1" type="button" value="Delete" style="display: none" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel3" Style="display: none" runat="server" CssClass="PopupPanel">
            <div>
                <div class="PopupHeader" id="Div2">
                    SAVE TEMPLATE
                    <br />
                </div>
                <div class="PopupBody">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <input type="radio" name="edit" id="rdNew" checked="checked" />New
                                    </td>
                                    <td>
                                        <input type="radio" name="edit" id="rdEdit" />Edit
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr id="trNew">
                                    <td colspan="3">
                                        Template Name:
                                        <asp:TextBox runat="server" ID="txtTemplateName" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trCat">
                                    <td colspan="3">
                                        Select Category:
                                        <asp:DropDownList ID="drpCategory" runat="server" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trEdit1">
                                    <td colspan="3">
                                        Select Template:
                                        <asp:DropDownList ID="drpTemplate" runat="server" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trEdit">
                                    <td>
                                        <input type="radio" name="Save" id="rdSaveFront" value="Front" />Front
                                    </td>
                                    <td id="tdSaveBack">
                                        <input type="radio" name="Save" id="rdSaveBack" value="Back" />Back
                                    </td>
                                    <td id="tdSaveBoth">
                                        <input type="radio" name="Save" id="rdSaveBoth" value="Both" checked="checked" />Both
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <input id="btnTemplateSave" type="button" value="Save" runat="server" class="ButtonControl"
                                            clientidmode="Static" />
                                    </td>
                                    <td>
                                        <input id="btntmpClose" type="button" value="Cancel" class="ButtonControl" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:Panel>
        <asp:Button runat="server" ID="btnDemo" Style="display: none;" />
        <asp:Panel runat="server" Style="display: none;" ID="Panel4" CssClass="PopupPanel">
            <div>
                <div class="PopupHeader" id="Div4">
                    Select Template:
                    <br />
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="PopupBody">
                            <table>
                                <tr id="tr2">
                                    <td colspan="3">
                                        Select Template:
                                        <asp:DropDownList ID="drpEditTemp" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%--   <tr id="tr3">
                            <td>
                                <input type="radio" name="Save" id="rdbEditFront" value="Front" />Front
                            </td>
                            <td id="td1">
                                <input type="radio" name="Save" id="rbdEditBack" value="Back" />Back
                            </td>
                            <td id="td2">
                                <input type="radio" name="Save" id="rbdEditBoth" value="Both" checked="checked" />Both
                            </td>
                        </tr>--%>
                                <tr>
                                    <td style="padding-top: 10%">
                                    </td>
                                    <td style="padding-top: 10%">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="ButtonControl"
                                            OnClick="btnSubmit_Click" />
                                    </td>
                                    <td style="padding-top: 10%">
                                        <input id="btnEditClose" type="button" value="Cancel" class="ButtonControl" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSubmit" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
        <%-- </div>--%>
    </div>
</asp:Content>
