<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iFrame.aspx.cs" Inherits="UNO.iFrame" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Outputcache Location="none" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.ColorPicker" TagPrefix="obout" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/1.8.23-jquery-ui.min.js" type="text/javascript"></script>
     
<style type="text/css">
#AjaxFileUpload1_Html5DropZone
{
	 display:none;
}
#AjaxFileUpload1_ctl00
{
	 width:250px;
}
</style>

<script type="text/javascript">
    $(function () {
        function SetSession() {
            var imageName = "";
            $.ajax({
                url: "iFrame.aspx/setSession",
                type: "POST",
                dataType: "text",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    //  alert(msg);

                },
                error: function () { alert(arguments[2]); }
            });


        };

        $(this).find('span').click(function () {
            var id = $(this).attr('id');
            if (id == "AjaxFileUpload1_SelectFileContainer") {
                $(".ajax__fileupload_queueContainer").html("");
                $(".ajax__fileupload_queueContainer").hide();
                $(".ajax__fileupload_footer").hide();
                SetSession();
            }
        });

    });
   
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

<asp:AjaxFileUpload ID="AjaxFileUpload1" OnUploadComplete="UploadComplete"  runat="server" MaximumNumberOfFiles="1"  AllowedFileTypes="jpg,jpeg,png,gif," />
   
    </div>
    </form>
</body>
</html>
