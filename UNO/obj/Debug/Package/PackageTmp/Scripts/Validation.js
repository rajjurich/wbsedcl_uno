
function isString(a) {
    return typeof a == 'string';
}

function IsValidTime(timeStr) {
    var timePat = /^(\d{1,2}):(\d{2})(:(\d{2}))?(\s?(AM|am|PM|pm))?$/;

    var matchArray = timeStr.value.match(timePat);
    if (matchArray == null) {
        alert("Time is not in a valid format.");
        return false;
    }
    hour = matchArray[1];
    minute = matchArray[2];
    second = matchArray[4];
    ampm = matchArray[6];

    if (second == "") { second = null; }
    if (ampm == "") { ampm = null }

    if (hour < 0 || hour > 23) {
        alert("Hour must be between 0 and 23.");
        return false;
    }
    if (minute < 0 || minute > 59) {
        alert("Minute must be between 0 and 59.");
        return false;
    }
    if (second != null && (second < 0 || second > 59)) {
        alert("Second must be between 0 and 59.");
        return false;
    }
    return true;
}

function isEmail(emailStr) {
    alert("email");
    var split1;
    var split2;

    split1 = emailStr.split("@")
    if (split1.length == 2) {
        if (split1[0].length != 0 || split1[1].length != 0) {


            split2 = split1[1].split(".")
            if (split2.length == 2 || split2.length == 3) {

                if (split2[0].length != 0 || split2[1].length != 0 && split2[1].length != 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        else
            return false;
    }
    else
        return false;
}

function notNull(str) {

    if (str.length == 0)
        return false
    else {
        return true
    }
}

// ---------------------------------------------------------------------------------------------------------------------------

// FLOAT VALUE
function isNumber1(str) {

    numdecs = 0;

    for (i = 0; i < str.length; i++) {
        mychar = str.charAt(i)
        if (i == 0) {
            if ((mychar >= "0" && mychar <= "9") || mychar == "." || mychar == "-") {
                if (mychar == ".")
                    numdecs++
            }
            else {
                return false;
            }
        }
        else {
            if ((mychar >= "0" && mychar <= "9") || mychar == ".") {
                if (mychar == ".")
                    numdecs++
            }
            else {
                return false;
            }
        }
    }

    if (numdecs > 1) {
        return false
    }
    return true

}


// ---------------------------------------------------------------------------------------------------------------------------

// INTEGER VALUE
function isNumber(str) {

    numdecs = 0;

    for (i = 0; i < str.length; i++) {

        mychar = str.charAt(i)

        if ((mychar >= "0" && mychar <= "9")) {
            if (mychar == ".")
                numdecs++
        }
        else {
            return false;
        }
    }

    if (numdecs > 0) {
        return false
    }
    return true
}


// ---------------------------------------------------------------------------------------------------------------------------


function notBlank(str) {

    for (i = 0; i < str.length; i++) {

        if (str.charAt(i) != " ")
            return true
    }
    return false
}


// ---------------------------------------------------------------------------------------------------------------------------


function isDigits(str) {

    var i

    for (i = 0; i < str.length; i++) {

        mychar = str.charAt(i)
        if (mychar < "0" || mychar > "9")
            return false
    }
    return true

}

function isPhoneNumber(str) {

    numdecs = 0;

    for (i = 0; i < str.length; i++) {
        mychar = str.charAt(i)
        if ((mychar >= "0" && mychar <= "9") || mychar == "-" || mychar == "(" || mychar == ")" || mychar == "+") {
            if (mychar == ".")
                numdecs++
        }
        else {
            return false;
        }
    }

    if (numdecs > 1) {
        return false
    }

    return true
}


// ---------------------------------------------------------------------------------------------------------------------------


function validateString(myfield, message) {
    if (notNull(myfield.value) && notBlank(myfield.value))
        return true
    else {
        if (message.length != 0) {
            myfield.focus()
            alert(message)
        }
        return false
    }
}

function getDateErrorMsg(dtStr, dateformat) {

    var dateMessage;
    var daysInMonth = DaysArray(12);
    var pos1 = dtStr.indexOf(dtCh);
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1);
    var strMonth, strDay, strYear;

    if (dateformat == "dd/mm/yyyy") {
        strMonth = dtStr.substring(pos1 + 1, pos2);
        strDay = dtStr.substring(0, pos1);
        strYear = dtStr.substring(pos2 + 1);
        strYr = strYear;
    }
    else {
        strMonth = dtStr.substring(0, pos1);
        strDay = dtStr.substring(pos1 + 1, pos2);
        strYear = dtStr.substring(pos2 + 1);
        strYr = strYear;
    }
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
    }
    month = parseInt(strMonth);
    day = parseInt(strDay);
    year = parseInt(strYr);
    if (pos1 == -1 || pos2 == -1) {

        if (dateformat == "dd/mm/yyyy")
            dateMessage = "The date format should be : dd/mm/yyyy";
        else
            dateMessage = "The date format should be : mm/dd/yyyy";
        return dateMessage;
    }
    if (strMonth.length < 1 || month < 1 || month > 12) {
        dateMessage = "Please enter a valid month";
        return dateMessage;
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        dateMessage = "Please enter a valid day";
        return dateMessage;
    }
    if (strYear.length != 4) {
        dateMessage = "Please enter a valid 4 digit year ";
        return dateMessage;
    }
    if (year == 0 || year < minYear || year > maxYear) {
        dateMessage = "Please enter year between " + minYear + " and " + maxYear;
        return dateMessage;
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
        dateMessage = "Please enter a valid date";
        return dateMessage;
    }
    return dateMessage

}
// ---------------------------------------------------------------------------------------------------------------------------


//function isValidateDDMMYYYYDate(oSrc, args) {
//    alert("1");
//    var val = args.Value;
//    alert(val);
//    args.IsValid = this.checkValidDate(val, "dd/mm/yyyy", oSrc);
//    alert("2");
//}
function isValidateInteger(oSrc, args) {
    var val = args.Value;
    args.IsValid = this.isNumber(val);
}

function isValidateFloat(oSrc, args) {
    var val = args.Value;
    args.IsValid = this.isNumber1(val);
}
function fnSlash(ctrl, e) {

    var unicode = e.keyCode

    if (unicode != 8) {
        if (ctrl.getAttribute && ctrl.value.length == 2) {
            ctrl.value = ctrl.value + "/";
        }

        if (ctrl.getAttribute && ctrl.value.length == 5) {
            ctrl.value = ctrl.value + "/";
        }
    }
} //For function fnSlash(ctrl)

function date_dash(field, e) {
    //alert(e.keyCode);
    if (e.keyCode == 8 || e.keyCode == 46)
        return;
    if (Trim(field.value).length == 2)
        field.value = Trim(field.value) + '/';
    if (Trim(field.value).length == 5)
        field.value = Trim(field.value) + '/';
    if (Trim(field.value).length > 10)
        field.value = field.value.substring(0, 10);
}
function findspace(evnt) {

    var keyASCII = (evnt.which) ? evnt.which : evnt.keyCode;
    var keyValue = String.fromCharCode(keyASCII);

    if (!(keyASCII >= '48' && keyASCII <= '57')) {
        window.evnt.keyCode = 0;
    }
}

var dtCh = "/";
var minYear = 1900;
var maxYear = 2100;

function daysInFebruary(year) {
    // February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
}

function fnColon(ctrl, e) {
    var unicode = e.keyCode
    if (unicode != 8) {
        if (ctrl.getAttribute && ctrl.value.length == 2) {
            ctrl.value = ctrl.value + ":";
        }
    }
}

function ValidateTime(timeStr) {
    if ((timeStr.search(/^\d{1,2}:\d{2}([ap]m)?$/) != -1) &&
            (timeStr.substr(0, 2) >= 0 && timeStr.substr(0, 2) <= 24) &&
            (timeStr.substr(3, 2) >= 0 && timeStr.substr(3, 2) <= 59))
        return true;
    else
        return false;

}

function isInteger(s) {
    var i;
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    return true;
}

function stripCharsInBag(s, bag) {
    var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function isValidateDDMMYYYYDate(oSrc, args) {
    //alert("kaps");
    var val = args.Value;
    args.IsValid = this.checkValidDate(val, "dd/mm/yyyy", oSrc);
    //alert("ss");
    // alert(args.IsValid);
}

function DaysArray(n) {
    for (var i = 1; i <= n; i++) {
        this[i] = 31
        if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
        if (i == 2) { this[i] = 29 }
    }
    return this
}
function checkValidDate(dtStr, dateformat, oSrc) {

    var msg = getDateErrorMsg(dtStr, dateformat);

    if (msg != null) {
        oSrc.innerHTML = msg;
        return false;
    }
    else {
        return true;
    }
}
function isValidateDate(oSrc, args, dateFormat) {

    var val = args.Value;
    args.IsValid = this.isDate(val, dateFormat);
}

function isValidateString(oSrc, args) {
    var val = args.Value;
    args.IsValid = this.isString(val);
}

function IsChar(evnt) {

    //alert("In Fun");
    var charCode = (evnt.which) ? evnt.which : event.keyCode
    if ((charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90) ||

			 (charCode == 32) || (charCode == 8)) {
        return true
    }
    else {
        return false
    }
}

function compareDate() {

    var start = document.formName.begindate.value;
    var end = document.formName.enddate.value;

    var stDate = new Date(start);
    var enDate = new Date(end);
    var compDate = enDate - stDate;

    if (compDate >= 0)
        return true;
    else {
        alert("Please Enter the correct date ");
        return false;
    }
}

function Trim(STRING) {
    STRING = LTrim(STRING);
    return RTrim(STRING);
}

function RTrim(STRING) {
    while (STRING.charAt((STRING.length - 1)) == " ") {
        STRING = STRING.substring(0, STRING.length - 1);
    }
    return STRING;
}

function LTrim(STRING) {
    while (STRING.charAt(0) == " ") {
        STRING = STRING.replace(STRING.charAt(0), "");
    }
    return STRING;
}

function date_dash(field, e) {
    //alert(e.keyCode);
    if (e.keyCode == 8 || e.keyCode == 46)
        return;
    if (Trim(field.value).length == 2)
        field.value = Trim(field.value) + '/';
    if (Trim(field.value).length == 5)
        field.value = Trim(field.value) + '/';
    if (Trim(field.value).length > 10)
        field.value = field.value.substring(0, 10);
}
function IsNumber(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode
    if ((charCode >= 48 && charCode <= 57) || (charCode == 8)) {
        return true
    }
    else {
        return false
    }
}


function IsNumeric(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode
    if ((charCode >= 48 && charCode <= 57) || (charCode == 8) || (charCode == 46)) {
        return true
    }
    else {
        return false
    }
}

function IsAlphanumeric(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode
    if ((charCode >= 65 && charCode <= 90) || (charCode >= 48 && charCode <= 57) ||
			(charCode >= 97 && charCode <= 122) || (charCode == 8) || (charCode == 45) || (charCode == 32) || (charCode == 38)) {
        return true
    }
    else {
        return false
    }
}
function IsAlphanumericWithoutspace(evnt) {
    var charCode = (evnt.which) ? evnt.which : evnt.keyCode
    //        if ((charCode >= 65 && charCode <= 90) || (charCode >= 48 && charCode <= 57) ||
    //			(charCode >= 97 && charCode <= 122) || (charCode == 8) || (charCode == 45)) {
    //            return true
    //        }
    //        else {
    //            return false
    //        }
    //Added by Pooja

    if (evnt.keyCode == 9) {
        //shift was down when tab was pressed
        return true;
    }
    if (48 <= charCode && charCode <= 57)
        return true;
    if (65 <= charCode && charCode <= 90)
        return true;
    if (97 <= charCode && charCode <= 122)
        return true;
    //Backspace
    if (charCode == 8)
        return true;
    return false;
}


function TimeValidate(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode
    if ((charCode >= 48 && charCode <= 58) || (charCode == 8)) {
        return true
    }
    else {
        return false
    }
}

//Added by Pooja Yadav start as on 24th December 2015
function handleDelete(gvLevels) {


    if (!validateCheckBoxes(gvLevels))
        return false;

    var msg = confirm("Record(s) marked for Deletion. Continue? ");
    if (msg == false) {
        clearGridCheckBoxes(gvLevels);
        return false;
    }
    return true;
}

function clearGridCheckBoxes(gvLevels) {
    var gridRef = document.getElementById(gvLevels); ;
    var inputElementArray = gridRef.getElementsByTagName('input');
    var cntchk = 0;
    for (var i = 0; i < inputElementArray.length; i++) {
        var elementRef = inputElementArray[i];

        if ((elementRef.type == 'checkbox')) {
            elementRef.checked = false;
        }
    }
}


function validateCheckBoxes(gvLevels) {


    var isValid = false;

    var gridView = document.getElementById(gvLevels);

    var inputs = gridView.getElementsByTagName('input');

    for (var i = 0; i < inputs.length; i++) {

        var elementRef = inputs[i];

        if ((elementRef.type == 'checkbox' || elementRef.type == "radio") && (elementRef.checked == true)) {
            isValid = true;

            return true;


        }
    }
    alert("Please select record");
    return false;
}
//End

//Added by Pooja Yadav
function CompareDates(d1, d2) {
    var start = d1.value.toUpperCase();
    var end = d2.value.toUpperCase();
    var arrDate = start.split('/');
    var arrDate1 = end.split('/');
    var date1 = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
    var date2 = new Date(arrDate1[2], arrDate1[1] - 1, arrDate1[0]);
    if (date1 > date2) {
        //alert("End date must be lessthan startdate");
        return false;
    }
    return true;
}

function checkLength(textBox, length) {
    var mLen = length;

    var maxLength = parseInt(mLen);

    if (textBox.value.length > maxLength) {
        textBox.value = textBox.value.substring(0, maxLength);
    }
}

/////
function navigateToUrl(url) {
    var f = document.createElement("FORM");
    f.action = url;

    var indexQM = url.indexOf("?");
    if (indexQM >= 0) {
        // the URL has parameters => convert them to hidden form inputs
        var params = url.substring(indexQM + 1).split("&");
        for (var i = 0; i < params.length; i++) {
            var keyValuePair = params[i].split("=");
            var input = document.createElement("INPUT");
            input.type = "hidden";
            input.name = keyValuePair[0];
            input.value = keyValuePair[1];
            f.appendChild(input);
        }
    }

    document.body.appendChild(f);
    f.submit();
}

///
function validateChosen() {
    var config = {
        '.chosen-select': {},
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-single': { disable_search_threshold: 10 },
        '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        '.chosen-select-width': { width: "95%" }
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }
}

//Added by Pooja Yadav
var OkFlag = "False";
function EntitySpecificDetails(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData, lstDCompany, lstDLocation, lstDDivision, lstDDepartment, lstDCategory, btnSave, lstEmployeeDummy) {
    
    var LoginUser = document.getElementById('lblUserName').innerHTML;
    LoginUser = LoginUser.replace("(", "").replace(")", "").replace(" ", "");
    //alert(LoginUser+"fgggg");
    var strcom, strloc, strdiv, strdept, strcat, strShift, strSec, strGrade, strchecked = "";
    var lBox, LDummy;
    var myData = [];

    strcom = document.getElementById(strcomany).value;
    strloc = document.getElementById(strlocation).value;
    strdiv = document.getElementById(strdivision).value;
    strdept = document.getElementById(strdepartment).value;

    if (strcategory != "")
        strcat = document.getElementById(strcategory).value;


    strchecked = strVal;
    if (strcom == '')
        strcom = "'N'";
    if (strloc == '')
        strloc = "'N'";
    if (strdiv == '')
        strdiv = "'N'";
    if (strdept == '')
        strdept = "'N'";
    if (strcat == '')
        strcat = "'N'";
    strShift = "'N'";
    strSec = "'N'";
    strGrade = "'N'";
    if (strchecked == 'EMP') {

        if (RbdCategory != "")
            $('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory].join(', #')).find('input').prop('disabled', true);
        else
            $('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);

        if ($('#' + RbdEmp + ' input:checked').val() == '1') {
            $('#' + LstEmployee).empty();
            var list = document.getElementById(LstEmployee);
            var list1 = document.getElementById(lstEmployeeDummy);
            var j = 0;
            for (i = 0; i <= list1.options.length - 1; i++) {
                list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                j++;
            }
            showEmpLoyee(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData);
            document.getElementById(btnSave).disabled = true;

        }
        return true;

    }


    if (strchecked == 'COM') {

        if (RbdCategory != "") {
            $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdCategory].join(', #')).find('input').prop('disabled', true);
            $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
        }
        else {
            $('#' + [RbdEmp, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
        }

        if ($('#' + RbdCompany + ' input:checked').val() == '1') {
            showCompany(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData);
       

            if (LoginUser == 'admin') {
                lBox = $('#' + LstCompany);
                DummylBox = $('#' + lstDCompany);
                $('#' + LstCompany).empty();
                $('#' + lstDCompany).empty();
            }
            document.getElementById(btnSave).disabled = true;
        }
    }

    if (strchecked == 'LOC') {

        if (RbdCategory != "") {

            $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdCategory].join(', #')).find('input').prop('disabled', true);
            $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
        }
        else {
            $('#' + [RbdEmp, RbdCompany, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);

        }
        if ($('#' + RbdLocation + ' input:checked').val() == '1') {
            showLocation(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData);

            if (LoginUser == 'admin') {
                lBox = $('#' + LstLocation);
                LDummy = $('#' + lstDLocation);
                $("#" + LstLocation).empty();
                $("#" + lstDLocation).empty();
            }
            document.getElementById(btnSave).disabled = true;
        }

    }
    else if (strchecked == 'DIV') {

        if (RbdCategory != "") {
            $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdCategory].join(', #')).find('input').prop('disabled', true);
            $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', false);
        }
        else {
            $('#' + [RbdEmp, RbdCompany, RbdLocation].join(', #')).find('input').prop('disabled', true);
            $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', false);
        }
        if ($('#' + RbdDivision + ' input:checked').val() == '1') {
            showDivision(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData);

            if (LoginUser == 'admin') {
                lBox = $('#' + LstDivision);
                LDummy = $('#' + lstDDivision);
                $("#" + LstDivision).empty();
                $("#" + lstDDivision).empty();
            }
            document.getElementById(btnSave).disabled = true;
        }
    }
    else if (strchecked == 'DEP') {

        if (RbdCategory != "") {

            $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            $('#' + [RbdCategory].join(', #')).find('input').prop('disabled', false);
        }
        else {
            $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
        }

        if ($('#' + RbdDepartment + ' input:checked').val() == '1') {
            showDepartment(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData);
            if (LoginUser == 'admin') {
                lBox = $('#' + LstDepartment);
                LDummy = $('#' + lstDDepartment);
                $("#" + LstDepartment).empty();
                $("#" + lstDDepartment).empty();
            }
            document.getElementById(btnSave).disabled = true;
        }
    }
    else if (strchecked == 'CAT') {

        $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
        if ($('#' + RbdCategory + ' input:checked').val() == '1') {
            showCategory(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData);
            if (LoginUser == 'admin') {
                lBox = $('#' + LstCategory);
                LDummy = $('#' + lstDCategory);
                $("#" + LstCategory).empty();
                $("#" + lstDCategory).empty();
            }
            document.getElementById(btnSave).disabled = true;
        }
    }



    if (LoginUser == 'admin') {
        //alert('ffff');
        $.ajax({
            url: "Filter/Filter.asmx/FillEntitySpecificDetails",
            type: "POST",
            dataType: "json",
            timeout: 10000,
            data: "{'strcom':'" + escape(strcom) + "','strloc':'" + escape(strloc) + "','strdiv':'" + escape(strdiv) + "','strdept':'" + escape(strdept) + "','strcat':'" + escape(strcat) + "','strchecked':'" + escape(strchecked) + "','strshift':'" + escape('') + "','strSec':'" + escape('') + "','strGRD':'" + escape('') + "'}",
           // data: "{'strcom':'" + escape(strcom) + "','strloc':'" + escape(strloc) + "','strdiv':'" + escape(strdiv) + "','strdept':'" + escape(strdept) + "','strcat':'" + escape(strcat) + "','strchecked':'" + escape(strchecked) + "'}",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (msg) {

                myData = $.parseJSON(msg.d);
                if (myData.length > 0 && myData.length != null) {
                    var listItems = [];
                    for (var i = 0; i < myData.length; i++) {
                        listItems.push('<option value="' + myData[i].ID + '">' + myData[i].Value + '</option>');
                    }
                }
                $(lBox).append(listItems.join(''));
                $(DummylBox).append(listItems.join(''));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

                alert("Error...\n" + textStatus + "\n" + errorThrown);
            }
        });
    }
    else {

        //alert('not admin');

    }
}


//Entities


function showEmpLoyee(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData) {


    //Show
    document.getElementById(btnOK).style.display = "inline";
    document.getElementById(Button4).style.display = "inline";
    document.getElementById(LstEmployee).style.display = "inline";
    document.getElementById(Btntd).style.display = "inline";
    document.getElementById(LstEntitySelected).style.display = "inline";
    //Hide
    document.getElementById(LstCompany).style.display = "none";
    document.getElementById(LstLocation).style.display = "none";
    document.getElementById(LstDivision).style.display = "none";
    document.getElementById(LstDepartment).style.display = "none";
    if (LstCategory != "")
        document.getElementById(LstCategory).style.display = "none";

    document.getElementById(LstEntitySelected).options.length = 0;
    document.getElementById('detail').style.display = "inline";
    document.getElementById(txtSearchEmp).style.display = "inline";
    document.getElementById(txtCompany).style.display = "none";
    document.getElementById(txtDivision).style.display = "none";
    if (txtShift != "")
        document.getElementById(txtShift).style.display = "none";
    document.getElementById(txtDepartment).style.display = "none";
    document.getElementById(txtLocation).style.display = "none";
    document.getElementById(lblAvailableData).style.display = "inline";
    document.getElementById(blSeletcedData).style.display = "inline";

}

function showCompany(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData) {


    //Show
    document.getElementById(btnOK).style.display = "inline";
    document.getElementById(Button4).style.display = "inline";
    document.getElementById(LstCompany).style.display = "inline";
    document.getElementById(LstEntitySelected).style.display = "inline";
    document.getElementById(Btntd).style.display = "inline";

    //Hide
    document.getElementById(LstEmployee).style.display = "none";
    document.getElementById(LstLocation).style.display = "none";
    document.getElementById(LstDivision).style.display = "none";
    document.getElementById(LstDepartment).style.display = "none";
    if (LstCategory != "")
        document.getElementById(LstCategory).style.display = "none";

    document.getElementById(LstEntitySelected).options.length = 0;
    document.getElementById('detail').style.display = "inline";
    document.getElementById(txtSearchEmp).style.display = "none";
    document.getElementById(txtCompany).style.display = "inline";
    document.getElementById(txtDivision).style.display = "none";
    if (txtShift != "")
        document.getElementById(txtShift).style.display = "none";
    document.getElementById(txtDepartment).style.display = "none";
    document.getElementById(txtLocation).style.display = "none";
    document.getElementById(lblAvailableData).style.display = "inline";
    document.getElementById(blSeletcedData).style.display = "inline";

}


function showLocation(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData) {


    if ($('#' + RbdCompany + ' input:checked').val() == '1') {

        if (document.getElementById(strcomany).value == "") {
            document.getElementById(LstCompany).style.display = "none";
            document.getElementById(RbdCompany + '_0').checked = true;
        }
    }

    //show
    document.getElementById(btnOK).style.display = "inline";
    document.getElementById(Button4).style.display = "inline";
    document.getElementById(LstLocation).style.display = "inline";
    document.getElementById(Btntd).style.display = "inline";
    document.getElementById(LstEntitySelected).style.display = "inline";
    document.getElementById(txtLocation).style.display = "inline";
    document.getElementById(lblAvailableData).style.display = "inline";
    document.getElementById(blSeletcedData).style.display = "inline";
    document.getElementById('detail').style.display = "inline";

    //Hide

    document.getElementById(LstEmployee).style.display = "none";
    document.getElementById(LstCompany).style.display = "none";
    document.getElementById(LstDivision).style.display = "none";
    document.getElementById(LstDepartment).style.display = "none";
    if (LstCategory != "")
        document.getElementById(LstCategory).style.display = "none";


    document.getElementById(LstEntitySelected).options.length = 0;

    document.getElementById(txtSearchEmp).style.display = "none";
    document.getElementById(txtCompany).style.display = "none";
    document.getElementById(txtDivision).style.display = "none";
    if (txtShift != "")
        document.getElementById(txtShift).style.display = "none";
    document.getElementById(txtDepartment).style.display = "none";


}

function showDivision(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData) {


    if ($('#' + RbdLocation + ' input:checked').val() == '1') {

        if (document.getElementById(strlocation).value == "") {
            document.getElementById(LstLocation).style.display = "none";
            document.getElementById(RbdLocation + '_0').checked = true;
        }

    }

    //Show
    document.getElementById(btnOK).style.display = "inline";
    document.getElementById(Button4).style.display = "inline";
    document.getElementById(Btntd).style.display = "inline";
    document.getElementById(LstDivision).style.display = "inline";
    document.getElementById(LstEntitySelected).style.display = "inline";
    document.getElementById(lblAvailableData).style.display = "inline";
    document.getElementById(blSeletcedData).style.display = "inline";

    //Hide
    document.getElementById(LstEmployee).style.display = "none";
    document.getElementById(LstCompany).style.display = "none";
    document.getElementById(LstLocation).style.display = "none";
    document.getElementById(LstDepartment).style.display = "none";

    if (LstCategory != "")
        document.getElementById(LstCategory).style.display = "none";


    document.getElementById(LstEntitySelected).options.length = 0;
    document.getElementById('detail').style.display = "inline";
    document.getElementById(txtSearchEmp).style.display = "none";
    document.getElementById(txtCompany).style.display = "none";
    document.getElementById(txtDivision).style.display = "inline";

    if (txtShift != "")
        document.getElementById(txtShift).style.display = "none";

    document.getElementById(txtDepartment).style.display = "none";
    document.getElementById(txtLocation).style.display = "none";


}

function showDepartment(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData) {


    if ($('#' + RbdDivision + ' input:checked').val() == '1') {

        if (document.getElementById(strdivision).value == "") {
            document.getElementById(LstDivision).style.display = "none";
            document.getElementById(RbdDivision + '_0').checked = true;
        }
    }

    //Show
    document.getElementById(btnOK).style.display = "inline";
    document.getElementById(Button4).style.display = "inline";
    document.getElementById(LstDepartment).style.display = "inline";
    document.getElementById(Btntd).style.display = "inline";
    document.getElementById(LstEntitySelected).style.display = "inline";
    document.getElementById(lblAvailableData).style.display = "inline";
    document.getElementById(blSeletcedData).style.display = "inline";

    //Employee
    if (LstCategory != "")
        document.getElementById(LstCategory).style.display = "none";
    document.getElementById(LstEmployee).style.display = "none";
    document.getElementById(LstCompany).style.display = "none";
    document.getElementById(LstLocation).style.display = "none";
    document.getElementById(LstDivision).style.display = "none";

    document.getElementById(LstEntitySelected).options.length = 0;
    document.getElementById('detail').style.display = "inline";
    document.getElementById(txtSearchEmp).style.display = "none";
    document.getElementById(txtCompany).style.display = "none";
    document.getElementById(txtDivision).style.display = "none";
    if (txtShift != "")
        document.getElementById(txtShift).style.display = "none";
    document.getElementById(txtDepartment).style.display = "inline";
    document.getElementById(txtLocation).style.display = "none";



}

function showCategory(strVal, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData) {



    if ($('#' + RbdDepartment + ' input:checked').val() == '1') {
        if (document.getElementById(strdepartment).value == "") {
            document.getElementById(LstDepartment).style.display = "none";
            document.getElementById(RbdDepartment + '_0').checked = true;
        }
    }

    //Show
    document.getElementById(btnOK).style.display = "inline";
    document.getElementById(Button4).style.display = "inline";
    document.getElementById(LstCategory).style.display = "inline";
    document.getElementById(Btntd).style.display = "inline";
    document.getElementById(LstEntitySelected).style.display = "inline";
    document.getElementById(lblAvailableData).style.display = "inline";
    document.getElementById(blSeletcedData).style.display = "inline";

    //hide
    document.getElementById(LstEmployee).style.display = "none";
    document.getElementById(LstCompany).style.display = "none";
    document.getElementById(LstLocation).style.display = "none";
    document.getElementById(LstDivision).style.display = "none";
    document.getElementById(LstDepartment).style.display = "none";
    document.getElementById(LstEntitySelected).options.length = 0;
    document.getElementById('detail').style.display = "inline";
    document.getElementById(txtSearchEmp).style.display = "none";
    document.getElementById(txtCompany).style.display = "none";
    document.getElementById(txtDivision).style.display = "none";
    document.getElementById(txtShift).style.display = "inline";
    document.getElementById(txtDepartment).style.display = "none";
    document.getElementById(txtLocation).style.display = "none";
}


//Okclick

function okclick(strEmployee, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData, btnSave) {
    var first = "";
    var strVal = "";
    var count = 0;
    OkFlag = "True";
    if ((document.getElementById(RbdEmp + '_1').checked == true) && document.getElementById(LstEmployee).style.display == "inline") {
        var list1 = document.getElementById(LstEntitySelected);

        for (var i = 0; i < list1.options.length; i++) {

            if (first == "")
                first = first + "('" + list1.options[i].value + "'";
            else

                first = first + ",'" + list1.options[i].value + "'";
            count = count + 1;

        }

        if (first == "") {
            alert("select atleast one item");
            return false;
        }
        else {
            document.getElementById(strEmployee).value = first + ")";
            document.getElementById(LstEntitySelected).style.display = "none";
            document.getElementById(LstEmployee).style.display = "none";
            document.getElementById(btnOK).style.display = "none";
            document.getElementById(Button4).style.display = "none";
            document.getElementById(Btntd).style.display = "none";
        }


    }

    if ((document.getElementById(RbdCompany + '_1').checked == true) && document.getElementById(LstCompany).style.display == "inline") {
        var list1 = document.getElementById(LstEntitySelected);

        for (var i = 0; i < list1.options.length; i++) {

            if (first == "")
                first = first + "('" + list1.options[i].value + "'";
            else
                first = first + ",'" + list1.options[i].value + "'";

            count = count + 1;

        }

        if (first == "") {
            alert("select atleast one item");
            return false;
        }
        else {
            document.getElementById(strcomany).value = first + ")";

            document.getElementById(LstCompany).style.display = "none";
            document.getElementById(LstEntitySelected).style.display = "none";
            document.getElementById(btnOK).style.display = "none";
            document.getElementById(Button4).style.display = "none";
            document.getElementById(Btntd).style.display = "none";
        }


    }

    if ((document.getElementById(RbdLocation + '_1').checked == true) && document.getElementById(LstLocation).style.display == "inline") {
        var list1 = document.getElementById(LstEntitySelected);

        for (var i = 0; i < list1.options.length; i++) {

            if (first == "")
                first = first + "('" + list1.options[i].value + "'";
            else
                first = first + ",'" + list1.options[i].value + "'";

            count = count + 1;

        }

        if (first == "") {
            alert("select atleast one item");
            return false;
        }
        else {

            document.getElementById(strlocation).value = first + ")";
            document.getElementById(LstLocation).style.display = "none";
            document.getElementById(LstEntitySelected).style.display = "none";
            document.getElementById(btnOK).style.display = "none";
            document.getElementById(Button4).style.display = "none";
            document.getElementById(Btntd).style.display = "none";
        }

    }

    if ((document.getElementById(RbdDivision + '_1').checked == true) && document.getElementById(LstDivision).style.display == "inline") {
        var list1 = document.getElementById(LstEntitySelected);

        for (var i = 0; i < list1.options.length; i++) {

            if (first == "")
                first = first + "('" + list1.options[i].value + "'";
            else
                first = first + ",'" + list1.options[i].value + "'";

            count = count + 1;

        }

        if (first == "") {
            alert("select atleast one item");
            return false;
        }
        else {

            document.getElementById(strdivision).value = first + ")";
            document.getElementById(LstDivision).style.display = "none";
            document.getElementById(LstEntitySelected).style.display = "none";
            document.getElementById(btnOK).style.display = "none";
            document.getElementById(Button4).style.display = "none";
            document.getElementById(Btntd).style.display = "none";
        }

        document.getElementById(txtSearchEmp).style.display = "none";
        document.getElementById(txtSearchEmp).value = "";

    }
    if ((document.getElementById(RbdDepartment + '_1').checked == true) && document.getElementById(LstDepartment).style.display == "inline") {
        var list1 = document.getElementById(LstEntitySelected);

        for (var i = 0; i < list1.options.length; i++) {

            if (first == "") {

                first = first + "('" + list1.options[i].value + "'";
            }
            else {

                first = first + ",'" + list1.options[i].value + "'";
            }
            count = count + 1;

        }

        if (first == "") {
            alert("select atleast one item");
            return false;
        }
        else {

            document.getElementById(strdepartment).value = first + ")";
            document.getElementById(LstDepartment).style.display = "none";
            document.getElementById(LstEntitySelected).style.display = "none";
            document.getElementById(btnOK).style.display = "none";
            document.getElementById(Button4).style.display = "none";
            document.getElementById(Btntd).style.display = "none";
        }



    }

    if (RbdCategory != "") {
        if ((document.getElementById(RbdCategory + '_1').checked == true) && document.getElementById(LstCategory).style.display == "inline") {
            var list1 = document.getElementById(LstEntitySelected);

            for (var i = 0; i < list1.options.length; i++) {

                if (first == "") {

                    first = first + "('" + list1.options[i].value + "'";
                }
                else {

                    first = first + ",'" + list1.options[i].value + "'";
                }
                count = count + 1;

            }

            if (first == "") {
                alert("select atleast one item");
                return false;
            }
            else {

                document.getElementById(strcategory).value = first + ")";
                document.getElementById(LstCategory).style.display = "none";
                document.getElementById(LstEntitySelected).style.display = "none";
                document.getElementById(btnOK).style.display = "none";
                document.getElementById(Button4).style.display = "none";
                document.getElementById(Btntd).style.display = "none";
            }
        }
    }



    document.getElementById(txtSearchEmp).style.display = "none";
    document.getElementById(txtSearchEmp).value = "";
    document.getElementById(txtCompany).style.display = "none";
    document.getElementById(txtCompany).value = "";
    document.getElementById(txtDivision).style.display = "none";
    document.getElementById(txtDivision).value = "";

    if (txtShift != "") {
        document.getElementById(txtShift).style.display = "none";
        document.getElementById(txtShift).value = "";
    }
    document.getElementById(txtDepartment).style.display = "none";
    document.getElementById(txtDepartment).value = "";
    document.getElementById(txtLocation).style.display = "none";
    document.getElementById(txtLocation).value = "";
    document.getElementById(lblAvailableData).style.display = "none";
    document.getElementById(blSeletcedData).style.display = "none";
    document.getElementById(btnSave).disabled = false;
    return true;

}


//Close Click

function closeclick(strEmployee, strcomany, strlocation, strdivision, strdepartment, strcategory, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, lblAvailableData, blSeletcedData, btnSave) {

    document.getElementById(btnOK).style.display = "none";
    document.getElementById(Button4).style.display = "none";
    document.getElementById(Btntd).style.display = "none";

    removeEntitySelectedListBox(LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee);

    if (document.getElementById(strEmployee).value == "") {
        document.getElementById(LstEmployee).style.display = "none";
        document.getElementById(RbdEmp + '_0').checked = true;
    }


    if (document.getElementById(strcomany).value == "") {
        document.getElementById(LstCompany).style.display = "none";
        document.getElementById(RbdCompany + '_0').checked = true;
    }

    if (document.getElementById(strlocation).value == "") {
        document.getElementById(LstLocation).style.display = "none";
        document.getElementById(RbdLocation + '_0').checked = true;
    }

    if (document.getElementById(strdivision).value == "") {
        document.getElementById(LstDivision).style.display = "none";
        document.getElementById(RbdDivision + '_0').checked = true;
    }

    if (document.getElementById(strdepartment).value == "") {
        document.getElementById(LstDepartment).style.display = "none";
        document.getElementById(RbdDepartment + '_0').checked = true;
    }

    if (LstCategory != "") {
        if (document.getElementById(strcategory).value == "") {
            document.getElementById(LstCategory).style.display = "none";
            document.getElementById(RbdCategory + '_0').checked = true;
        }
    }
    document.getElementById(LstEmployee).style.display = "none";
    document.getElementById(LstEntitySelected).style.display = "none";
    document.getElementById(txtSearchEmp).style.display = "none";
    document.getElementById(txtSearchEmp).value = "";
    document.getElementById(txtCompany).style.display = "none";
    document.getElementById(txtCompany).value = "";
    document.getElementById(txtDivision).style.display = "none";
    document.getElementById(txtDivision).value = "";
    if (txtShift != "") {
        document.getElementById(txtShift).style.display = "none";
        document.getElementById(txtShift).value = "";
    }
    document.getElementById(txtDepartment).style.display = "none";
    document.getElementById(txtDepartment).value = "";
    document.getElementById(txtLocation).style.display = "none";
    document.getElementById(txtLocation).value = "";
    document.getElementById(lblAvailableData).style.display = "none";
    document.getElementById(blSeletcedData).style.display = "none";
    document.getElementById('Btntd').style.display = "none";
    document.getElementById(btnSave).disabled = false;
}


///
var myValsEmp = new Array();
var myValsComp = new Array();
var myValsLoc = new Array();
var myValsDiv = new Array();
var myValsDep = new Array();
var myValsCat = new Array();

// All Restore

function removeEntitySelectedListBox(LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee) {

    if (document.getElementById(LstEmployee).style.display == "inline") {
        var Employee = document.getElementById(LstEmployee);
        var EntitySelected = document.getElementById(LstEntitySelected);

    }
    if (document.getElementById(LstCompany).style.display == "inline") {


        var Employee = document.getElementById(LstCompany);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstLocation).style.display == "inline") {

        var Employee = document.getElementById(LstLocation);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstDivision).style.display == "inline") {

        var Employee = document.getElementById(LstDivision);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstDepartment).style.display == "inline") {

        var Employee = document.getElementById(LstDepartment);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }
    if (LstCategory != "") {

        if (document.getElementById(LstCategory).style.display == "inline") {

            var Employee = document.getElementById(LstCategory);
            var EntitySelected = document.getElementById(LstEntitySelected);
        }
    }

    for (var i = 0; i <= EntitySelected.options.length - 1; i++) {
        var newOption = window.document.createElement('OPTION');
        newOption.text = EntitySelected.options[i].text;
        newOption.value = EntitySelected.options[i].value;
        Employee.options.add(newOption);

        if (document.getElementById(LstEmployee).style.display == "inline") {
            myValsEmp.push(EntitySelected.options[i].text)
        }
        if (document.getElementById(LstCompany).style.display == "inline") {

            myValsComp.push(EntitySelected.options[i].text)
        }

        if (document.getElementById(LstLocation).style.display == "inline") {
            myValsLoc.push(EntitySelected.options[i].text)
        }

        if (document.getElementById(LstDivision).style.display == "inline") {

            myValsDiv.push(EntitySelected.options[i].text)
        }

        if (document.getElementById(LstDepartment).style.display == "inline") {

            myValsDep.push(EntitySelected.options[i].text)
        }

        if (LstCategory != "") {
            if (document.getElementById(LstCategory).style.display == "inline") {
                myValsCat.push(EntitySelected.options[i].text)

            }
        }

    }

    EntitySelected.options.length = 0;
}

//// //All Selection

function AllSelectedListBox(LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee) {

    if (document.getElementById(LstEmployee).style.display == "inline") {
        var Employee = document.getElementById(LstEmployee);
        var EntitySelected = document.getElementById(LstEntitySelected);


    }
    if (document.getElementById(LstCompany).style.display == "inline") {


        var Employee = document.getElementById(LstCompany);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstLocation).style.display == "inline") {

        var Employee = document.getElementById(LstLocation);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstDivision).style.display == "inline") {

        var Employee = document.getElementById(LstDivision);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstDepartment).style.display == "inline") {

        var Employee = document.getElementById(LstDepartment);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstCategory).style.display == "inline") {

        var Employee = document.getElementById(LstCategory);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }


    for (var i = 0; i <= Employee.options.length - 1; i++) {
        var newOption = window.document.createElement('OPTION');
        newOption.text = Employee.options[i].text;
        newOption.value = Employee.options[i].value;

        if (document.getElementById(LstCompany).style.display == "inline") {
            myValsComp.length = 0;
        }

        if (document.getElementById(LstLocation).style.display == "inline") {
            myValsLoc.length = 0;
        }

        if (document.getElementById(LstDivision).style.display == "inline") {
            myValsDiv.length = 0;
        }

        if (document.getElementById(LstDepartment).style.display == "inline") {
            myValsDep.length = 0;
        }

        if (document.getElementById(LstCategory).style.display == "inline") {
            myValsCat.length = 0;
        }

        EntitySelected.options.add(newOption);


    }

    Employee.options.length = 0;
}


/////Some Selection
function FillEntitySeletedListBox(LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee) {

    if (document.getElementById(LstEmployee).style.display == "inline") {
        var Employee = document.getElementById(LstEmployee);
        var EntitySelected = document.getElementById(LstEntitySelected);


    }
    if (document.getElementById(LstCompany).style.display == "inline") {


        var Employee = document.getElementById(LstCompany);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstLocation).style.display == "inline") {


        var Employee = document.getElementById(LstLocation);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstDivision).style.display == "inline") {


        var Employee = document.getElementById(LstDivision);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstDepartment).style.display == "inline") {


        var Employee = document.getElementById(LstDepartment);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstCategory).style.display == "inline") {

        var Employee = document.getElementById(LstCategory);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    for (var i = Employee.options.length - 1; i >= 0; i--) {
        if (Employee.options[i].selected) {

            var newOption = window.document.createElement('OPTION');

            newOption.text = Employee.options[i].text;
            newOption.value = Employee.options[i].value;

            if (document.getElementById(LstEmployee).style.display == "inline") {
                index = FindIndexInArray(myValsEmp, Employee.options[i].text);

                if (index > -1) {
                    myValsEmp.splice(index, 1);
                }

            }
            if (document.getElementById(LstCompany).style.display == "inline") {

                index = FindIndexInArray(myValsComp, Employee.options[i].text);
                if (index > -1) {
                    myValsComp.splice(index, 1);
                }

            }

            if (document.getElementById(LstLocation).style.display == "inline") {

                index = FindIndexInArray(myValsLoc, Employee.options[i].text);

                if (index > -1) {
                    myValsLoc.splice(index, 1);
                }
            }

            if (document.getElementById(LstDivision).style.display == "inline") {
                index = FindIndexInArray(myValsDiv, Employee.options[i].text);

                if (index > -1) {
                    myValsDiv.splice(index, 1);
                }

            }

            if (document.getElementById(LstDepartment).style.display == "inline") {

                index = FindIndexInArray(myValsDep, Employee.options[i].text);

                if (index > -1) {
                    myValsDep.splice(index, 1);
                }

            }

            if (document.getElementById(LstCategory).style.display == "inline") {
                index = FindIndexInArray(myValsCat, Employee.options[i].text);

                if (index > -1) {
                    myValsCat.splice(index, 1);
                }

            }

            Employee.remove(i);
            EntitySelected.options.add(newOption);


        }
    }
}

/////Some Restore

function ReturnFillEntityAvailable(LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdCategory, btnOK, Btntd, Button4, LstEntitySelected, LstEmployee) {

    if (document.getElementById(LstEmployee).style.display == "inline") {
        var Employee = document.getElementById(LstEmployee);
        var EntitySelected = document.getElementById(LstEntitySelected);

    }
    if (document.getElementById(LstCompany).style.display == "inline") {


        var Employee = document.getElementById(LstCompany);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstLocation).style.display == "inline") {

        var Employee = document.getElementById(LstLocation);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstDivision).style.display == "inline") {

        var Employee = document.getElementById(LstDivision);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstDepartment).style.display == "inline") {

        var Employee = document.getElementById(LstDepartment);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }

    if (document.getElementById(LstCategory).style.display == "inline") {

        var Employee = document.getElementById(LstCategory);
        var EntitySelected = document.getElementById(LstEntitySelected);
    }


    for (var i = EntitySelected.options.length - 1; i >= 0; i--) {

        if (EntitySelected.options[i].selected) {
            var newOption = window.document.createElement('OPTION');
            newOption.text = EntitySelected.options[i].text;
            newOption.value = EntitySelected.options[i].value;

            /*Add Item to ArrayList*/
            if (document.getElementById(LstEmployee).style.display == "inline") {
                myValsEmp.push(EntitySelected.options[i].text)
            }

            if (document.getElementById(LstCompany).style.display == "inline") {
                myValsComp.push(EntitySelected.options[i].text)
            }

            if (document.getElementById(LstLocation).style.display == "inline") {
                myValsLoc.push(EntitySelected.options[i].text)
            }

            if (document.getElementById(LstDivision).style.display == "inline") {
                myValsDiv.push(EntitySelected.options[i].text)
            }

            if (document.getElementById(LstDepartment).style.display == "inline") {
                myValsDep.push(EntitySelected.options[i].text)
            }

            if (document.getElementById(LstCategory).style.display == "inline") {
                myValsCat.push(EntitySelected.options[i].text)
            }

            /*Add Item to ArrayList*/
            EntitySelected.options.remove(i);
            Employee.options.add(newOption);

        }

    }

}


function FindIndexInArray(xArrray, xSelectedValue) {
    var ArrayValue;
    var len = xArrray.length;
    var index = -1;
    var i;


    for (i = 0; i < len; i++) {
        ArrayValue = xArrray[i];

        if (ArrayValue == xSelectedValue) {
            index = i;
            return index;
        }
    }
}


//Reports

function FilterListBox(txtSearch, LstEntity, LstDummy) {
    var list = document.getElementById(LstEntity);
    var list1 = document.getElementById(LstDummy);
    var textEmpToSearch = document.getElementById(txtSearch);

    var items = 0;

    for (items = list1.options.length - 1; items >= 0; items--) {
        list.options[items] = null;
    }

    var i = 0, j = 0;
    var strList;

    var strTxt = textEmpToSearch.value.toLowerCase().replace(/\s/g, '');
    var strLength = textEmpToSearch.value.length;

    var iSearchPos;
    for (i = 0; i <= list1.options.length - 1; i++) {

        iSearchPos = list1.options[i].text.toLowerCase().replace(/\s/g, '').search(strTxt);

        if (iSearchPos >= 0) {

            list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
            j++;
        }

    }
}

function FilterListBoxS(txtSearch, LstEntity, LstDummy, lstSelected) {
    var list = document.getElementById(LstEntity);
    var list1 = document.getElementById(LstDummy);
    var textEmpToSearch = document.getElementById(txtSearch);

    var items = 0;

    for (items = list1.options.length - 1; items >= 0; items--) {
        list.options[items] = null;
    }

    var i = 0, j = 0;
    var strList;

    var strTxt = textEmpToSearch.value.toLowerCase().replace(/\s/g, '');
    var strLength = textEmpToSearch.value.length;

    var iSearchPos;
    for (i = 0; i <= list1.options.length - 1; i++) {

        iSearchPos = list1.options[i].text.toLowerCase().replace(/\s/g, '').search(strTxt);
        var ele = $("#" + lstSelected + " option:contains('" + list1.options[i].text + "')");
        if (iSearchPos >= 0 && ele.length == 0) {

            list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
            j++;
        }

    }
}


//Reports Okclick
function RptOkClick(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSec, strGrade,strDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstShift, LstSection, LstGrade,LstDesignation, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade,txtDesignation, Panel1, txtCalendarFrom, btnView, btnReset) {
    var first = "";
    var count = 0;
    var list1;
    var HiddenVal;
    var entity;
    var strSearchBox;
    if (document.getElementById(LstEmployee).style.display == "inline") {
        list1 = document.getElementById(LstEmployee);
        HiddenVal = strEmployee;
        entity = "Employees";
        strSearchBox = txtSearchEmp;
    }

    if (document.getElementById(LstCompany).style.display == "inline") {
        list1 = document.getElementById(LstCompany);
        HiddenVal = strcomany;
        entity = "Companies";
        strSearchBox = strcomany;
    }

    if (document.getElementById(LstLocation).style.display == "inline") {
        list1 = document.getElementById(LstLocation);
        HiddenVal = strlocation;
        entity = "Locations";
        strSearchBox = txtLocation;
    }

    if (document.getElementById(LstDivision).style.display == "inline") {
        list1 = document.getElementById(LstDivision);
        HiddenVal = strdivision;
        entity = "Divisions";
        strSearchBox = txtDivision;
    }

    if (document.getElementById(LstDepartment).style.display == "inline") {
        list1 = document.getElementById(LstDepartment);
        HiddenVal = strdepartment;
        entity = "Departments";
        strSearchBox = txtDepartment;
    }

    if (document.getElementById(LstShift).style.display == "inline") {
        list1 = document.getElementById(LstShift);
        HiddenVal = strShift;
        entity = "Shifts";
        strSearchBox = txtShift;
    }
    if (LstSection != "") {

        if (document.getElementById(LstSection).style.display == "inline") {
            list1 = document.getElementById(LstSection);
            HiddenVal = strSec;
            entity = "Groups";
            strSearchBox = txtSection;
        }
    }

    if (LstGrade != "") {

        if (document.getElementById(LstGrade).style.display == "inline") {
            list1 = document.getElementById(LstGrade);
            HiddenVal = strGrade;
            entity = "Grades";
            strSearchBox = txtGrade;
        }
    }

    if (LstDesignation != "") {
        if (document.getElementById(LstDesignation).style.display == "inline") {
            list1 = document.getElementById(LstDesignation);
            HiddenVal = strDesignation;
            entity = "Designations";
            strSearchBox = txtDesignation;
        }
    }

    ///
    for (var i = 0; i < list1.options.length; i++) {
        if (list1.options[i].selected) {
            if (first == "")
                first = first + "('" + list1.options[i].value + "'";
            else
                first = first + ",'" + list1.options[i].value + "'";
            count = count + 1;
        }
    }
    if (count > 1000) {
        alert("Can not select more than 1000" + entity + "\n  Total Selected Records :" + count);
        return false;
    }
    if (first == "") {
        alert("select atleast one item");
        return false;
    }
    else
        document.getElementById(HiddenVal).value = first + ")";

    ////
    document.getElementById(txtCalendarFrom).disabled = false;
    document.getElementById(btnView).disabled = false;
    document.getElementById(btnReset).disabled = false;
    list1.style.display = "none";
    document.getElementById(btnOK).style.display = "none";
    document.getElementById(btnCancel).style.display = "none";
    document.getElementById(strSearchBox).style.display = "none";
    document.getElementById(Panel1).style.display = "none";
}


//a
function RptOkClickbkp(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSec, strGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstShift, LstSection, LstGrade, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, Panel1, txtCalendarFrom, btnView, btnReset) {
    var first = "";
    var count = 0;
    var list1;
    var HiddenVal;
    var entity;
    var strSearchBox;
    if (document.getElementById(LstEmployee).style.display == "inline") {
        list1 = document.getElementById(LstEmployee);
        HiddenVal = strEmployee;
        entity = "Employees";
        strSearchBox = txtSearchEmp;
    }

    if (document.getElementById(LstCompany).style.display == "inline") {
        list1 = document.getElementById(LstCompany);
        HiddenVal = strcomany;
        entity = "Companies";
        strSearchBox = strcomany;
    }

    if (document.getElementById(LstLocation).style.display == "inline") {
        list1 = document.getElementById(LstLocation);
        HiddenVal = strlocation;
        entity = "Locations";
        strSearchBox = txtLocation;
    }

    if (document.getElementById(LstDivision).style.display == "inline") {
        list1 = document.getElementById(LstDivision);
        HiddenVal = strdivision;
        entity = "Divisions";
        strSearchBox = txtDivision;
    }

    if (document.getElementById(LstDepartment).style.display == "inline") {
        list1 = document.getElementById(LstDepartment);
        HiddenVal = strdepartment;
        entity = "Departments";
        strSearchBox = txtDepartment;
    }

    if (document.getElementById(LstShift).style.display == "inline") {
        list1 = document.getElementById(LstShift);
        HiddenVal = strShift;
        entity = "Shifts";
        strSearchBox = txtShift;
    }
    if (LstSection != "") {

        if (document.getElementById(LstSection).style.display == "inline") {
            list1 = document.getElementById(LstSection);
            HiddenVal = strSec;
            entity = "Groups";
            strSearchBox = txtSection;
        }
    }

    if (LstGrade != "") {

        if (document.getElementById(LstGrade).style.display == "inline") {
            list1 = document.getElementById(LstGrade);
            HiddenVal = strGrade;
            entity = "Grades";
            strSearchBox = txtGrade;
        }
    }

    ///
    for (var i = 0; i < list1.options.length; i++) {
        if (list1.options[i].selected) {
            if (first == "")
                first = first + "('" + list1.options[i].value + "'";
            else
                first = first + ",'" + list1.options[i].value + "'";
            count = count + 1;
        }
    }
    if (count > 1000) {
        alert("Can not select more than 1000" + entity + "\n  Total Selected Records :" + count);
        return false;
    }
    if (first == "") {
        alert("select atleast one item");
        return false;
    }
    else
        document.getElementById(HiddenVal).value = first + ")";

    ////
    document.getElementById(txtCalendarFrom).disabled = false;
    document.getElementById(btnView).disabled = false;
    document.getElementById(btnReset).disabled = false;
    list1.style.display = "none";
    document.getElementById(btnOK).style.display = "none";
    document.getElementById(btnCancel).style.display = "none";
    document.getElementById(strSearchBox).style.display = "none";
    document.getElementById(Panel1).style.display = "none";
}

//a


//ReportCode Added by aniket
function RptOkClick1bkp(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSec, strGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstShift, LstSection, LstGrade, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, Panel1, txtCalendarFrom, btnView, btnReset, lblCompanyList, lblProfitCenterList, lblCostCenterList, lblGradeList, lblEmployeeList, lblCompanyName, lblProfitCenterName, lblCostCenterName, lblGradeName, lblEmployeeName) {
    debugger;
  
    var first = "";
    var firstlist = "";
    var count = 0;
    var list1;
    var HiddenVal;
    var entity;
    var strSearchBox;


    if (LstEmployee != "") {
        if (document.getElementById(LstEmployee).style.display == "inline") {
            list1 = document.getElementById(LstEmployee);
            HiddenVal = strEmployee;
            entity = "Employees";
            strSearchBox = txtSearchEmp;
        }
    }


    if (LstCompany != "") {
        if (document.getElementById(LstCompany).style.display == "inline") {
            list1 = document.getElementById(LstCompany);
            HiddenVal = strcomany;
            entity = "Companies";
            strSearchBox = strcomany;
        }
    }


    if (LstLocation != "") {
        if (document.getElementById(LstLocation).style.display == "inline") {
            list1 = document.getElementById(LstLocation);
            HiddenVal = strlocation;
            entity = "Locations";
            strSearchBox = txtLocation;
        }
    }


    if (LstDivision != "") {
        if (document.getElementById(LstDivision).style.display == "inline") {
            list1 = document.getElementById(LstDivision);
            HiddenVal = strdivision;
            entity = "Divisions";
            strSearchBox = txtDivision;
        }
    }


    if (LstDepartment != "") {
        if (document.getElementById(LstDepartment).style.display == "inline") {
            list1 = document.getElementById(LstDepartment);
            HiddenVal = strdepartment;
            entity = "Departments";
            strSearchBox = txtDepartment;
        }
    }


    if (LstShift != "") {
        if (document.getElementById(LstShift).style.display == "inline") {
            list1 = document.getElementById(LstShift);
            HiddenVal = strShift;
            entity = "Shifts";
            strSearchBox = txtShift;
        }
    }


    if (LstSection != "") {
        if (document.getElementById(LstSection).style.display == "inline") {
            list1 = document.getElementById(LstSection);
            HiddenVal = strSec;
            entity = "Groups";
            strSearchBox = txtSection;
        }
    }



    if (LstGrade != "") {
        if (document.getElementById(LstGrade).style.display == "inline") {
            list1 = document.getElementById(LstGrade);
            HiddenVal = strGrade;
            entity = "Grades";
            strSearchBox = txtGrade;
        }
    }
    

    ///
    for (var i = 0; i < list1.options.length; i++) {
        if (list1.options[i].selected) {
            if (first == "")
                first = first + "('" + list1.options[i].value + "'";
            else
                first = first + ",'" + list1.options[i].value + "'";
            count = count + 1;
        }
    }

    
    if (count > 1000) {
        alert("Can not select more than 1000" + entity + "\n  Total Selected Records :" + count);
        return false;
    }
    if (first == "") {
        alert("select atleast one item");
        return false;
    }
    else
        if (LstCompany != "") {

            var LstCompanylist1 = document.getElementById(LstCompany);
            var Companylist = "";
            var strLstCompanylist1="";
            var strLstCompanylist1_array="";
            var strLstCompanylist="";
            for (var i = 0; i < LstCompanylist1.options.length; i++) {
                if (LstCompanylist1.options[i].selected) {
                    strLstCompanylist1 = LstCompanylist1.options[i].text;
                    strLstCompanylist1.trim();
                    strLstCompanylist1_array = strLstCompanylist1.split('|');
                    strLstCompanylist = strLstCompanylist1_array[0].trim();
                       
                    if (Companylist == "") {
                        Companylist = Companylist + "(" + strLstCompanylist + "";
                    }
                    else {
                        Companylist = Companylist + ", " + strLstCompanylist + "";
                        count = count + 1;
                    }
                }
            }


            var Company = document.getElementById(LstCompany);
            if (Company.selectedIndex != -1) {
                document.getElementById(lblCompanyList).innerText = Companylist + ")";
                document.getElementById(lblCompanyList).style.display = 'block';
                document.getElementById(lblCompanyName).style.display = 'block'; 
            }

        }
        if (LstDivision != "") {

            var LstDivision1 = document.getElementById(LstDivision);
            var Divisionlist = "";
            var strLstDivisionlist1 = "";
            var strLstDivisionlist1_array = "";
            var strLstDivisionlist = "";
            for (var i = 0; i < LstDivision1.options.length; i++) {
                if (LstDivision1.options[i].selected) {
                    strLstDivisionlist1 = LstDivision1.options[i].text;
                    strLstDivisionlist1.trim();
                    strLstDivisionlist1_array = strLstDivisionlist1.split('|');
                    strLstDivisionlist = strLstDivisionlist1_array[0].trim();
                    if (Divisionlist == "")
                        Divisionlist = Divisionlist + "(" + strLstDivisionlist + "";
                    else
                        Divisionlist = Divisionlist + ", " + strLstDivisionlist + "";
                    count = count + 1;
                }
            }



            var ProfitCenter = document.getElementById(LstDivision);
            if (ProfitCenter.selectedIndex != -1) {
                document.getElementById(lblProfitCenterList).innerText = Divisionlist + ")";
                document.getElementById(lblProfitCenterList).style.display = 'block';
                document.getElementById(lblProfitCenterName).style.display = 'block';
         }

        } if (LstLocation != "") {

            var LstLocation1 = document.getElementById(LstLocation);
            var Locationlist = "";
            var strLstLocationlist1 = "";
            var strLstLocationlist1_array = "";
            var strLstLocationlist = "";
            for (var i = 0; i < LstLocation1.options.length; i++) {
                if (LstLocation1.options[i].selected) {
                    strLstLocationlist1 = LstLocation1.options[i].text;
                    strLstLocationlist1.trim();
                    strLstLocationlist1_array = strLstLocationlist1.split('|');
                    strLstLocationlist = strLstLocationlist1_array[0].trim();
                    if (Locationlist == "")
                        Locationlist = Locationlist + "(" + strLstLocationlist + "";
                    else
                        Locationlist = Locationlist + ", " + strLstLocationlist + "";
                    count = count + 1;
                }
            }



         var CostCenter = document.getElementById(LstLocation);
         if (CostCenter.selectedIndex != -1) {
             document.getElementById(lblCostCenterList).innerText = Locationlist + ")";
             document.getElementById(lblCostCenterList).style.display = 'block';
             document.getElementById(lblCostCenterName).style.display = 'block';
        }

     } if (LstGrade != "") {

         var LstGrade1 = document.getElementById(LstGrade);
         var Gradelist = "";
         var strLstGradelist1 = "";
         var strLstGradelist1_array = "";
         var strLstGradelist = "";
         for (var i = 0; i < LstGrade1.options.length; i++) {
             if (LstGrade1.options[i].selected) {
                 strLstGradelist1 = LstGrade1.options[i].text;
                 strLstGradelist1.trim();
                 strLstGradelist1_array = strLstGradelist1.split('|');
                 strLstGradelist = strLstGradelist1_array[0].trim();
                 if (Gradelist == "")
                     Gradelist = Gradelist + "(" + strLstGradelist + "";
                 else
                     Gradelist = Gradelist + ", " + strLstGradelist + "";
                 count = count + 1;
             }
         }




        var Grade = document.getElementById(LstGrade);
        if (Grade.selectedIndex != -1) {
            document.getElementById(lblGradeList).innerText = Gradelist + ")";
            document.getElementById(lblGradeList).style.display = 'block';
            document.getElementById(lblGradeName).style.display = 'block';
        }

    } if (LstEmployee != "") {

        var LstEmployee1 = document.getElementById(LstEmployee);
        var Employeelist = "";
        var strLstEmployeelist1 = "";
        var strLstEmployeelist1_array = "";
        var strLstEmployeelist = "";
        for (var i = 0; i < LstEmployee1.options.length; i++) {
            if (LstEmployee1.options[i].selected) {

                strLstEmployeelist1 = LstEmployee1.options[i].text;
                strLstEmployeelist1.trim();
                strLstEmployeelist1_array = strLstEmployeelist1.split('|');
                strLstEmployeelist = strLstEmployeelist1_array[0].trim();

                if (Employeelist == "")
                    Employeelist = Employeelist + "(" + strLstEmployeelist + "";
                else
                    Employeelist = Employeelist + ", " + strLstEmployeelist + "";
                count = count + 1;
            }
        }


        var emp = document.getElementById(LstEmployee);
        if (emp.selectedIndex != -1) {
            document.getElementById(lblEmployeeList).innerText = Employeelist + ")";
            document.getElementById(lblEmployeeList).style.display = 'block';
            document.getElementById(lblEmployeeName).style.display = 'block';
        }

        }
        document.getElementById(HiddenVal).value = first + ")";

    ////
    document.getElementById(txtCalendarFrom).disabled = false;
    document.getElementById(btnView).disabled = false;
    document.getElementById(btnReset).disabled = false;
    list1.style.display = "none";
    document.getElementById(btnOK).style.display = "none";
    document.getElementById(btnCancel).style.display = "none";
    document.getElementById(strSearchBox).style.display = "none";
    document.getElementById(Panel1).style.display = "none";
}

///

function RptOkClick1(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSec, strGrade, strDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstShift, LstSection, LstGrade, LstDesignation, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, Panel1, txtCalendarFrom, btnView, btnReset, lblCompanyList, lblProfitCenterList, lblCostCenterList, lblGradeList, lblEmployeeList, lblDesignationList, lblCompanyName, lblProfitCenterName, lblCostCenterName, lblGradeName, lblEmployeeName, lblDesignationName) {
    debugger;

    var first = "";
    var firstlist = "";
    var count = 0;
    var list1;
    var HiddenVal;
    var entity;
    var strSearchBox;


    if (LstEmployee != "") {
        if (document.getElementById(LstEmployee).style.display == "inline") {
            list1 = document.getElementById(LstEmployee);
            HiddenVal = strEmployee;
            entity = "Employees";
            strSearchBox = txtSearchEmp;
        }
    }


    if (LstCompany != "") {
        if (document.getElementById(LstCompany).style.display == "inline") {
            list1 = document.getElementById(LstCompany);
            HiddenVal = strcomany;
            entity = "Companies";
            strSearchBox = strcomany;
        }
    }


    if (LstLocation != "") {
        if (document.getElementById(LstLocation).style.display == "inline") {
            list1 = document.getElementById(LstLocation);
            HiddenVal = strlocation;
            entity = "Locations";
            strSearchBox = txtLocation;
        }
    }


    if (LstDivision != "") {
        if (document.getElementById(LstDivision).style.display == "inline") {
            list1 = document.getElementById(LstDivision);
            HiddenVal = strdivision;
            entity = "Divisions";
            strSearchBox = txtDivision;
        }
    }


    if (LstDepartment != "") {
        if (document.getElementById(LstDepartment).style.display == "inline") {
            list1 = document.getElementById(LstDepartment);
            HiddenVal = strdepartment;
            entity = "Departments";
            strSearchBox = txtDepartment;
        }
    }


    if (LstShift != "") {
        if (document.getElementById(LstShift).style.display == "inline") {
            list1 = document.getElementById(LstShift);
            HiddenVal = strShift;
            entity = "Shifts";
            strSearchBox = txtShift;
        }
    }


    if (LstSection != "") {
        if (document.getElementById(LstSection).style.display == "inline") {
            list1 = document.getElementById(LstSection);
            HiddenVal = strSec;
            entity = "Groups";
            strSearchBox = txtSection;
        }
    }



    if (LstGrade != "") {
        if (document.getElementById(LstGrade).style.display == "inline") {
            list1 = document.getElementById(LstGrade);
            HiddenVal = strGrade;
            entity = "Grades";
            strSearchBox = txtGrade;
        }
    }

    if (LstDesignation != "") {
        if (document.getElementById(LstDesignation).style.display == "inline") {
            list1 = document.getElementById(LstDesignation);
            HiddenVal = strDesignation;
            entity = "Designations";
            strSearchBox = txtDesignation;
        }
    }



    ///
    for (var i = 0; i < list1.options.length; i++) {
        if (list1.options[i].selected) {
            if (first == "")
                first = first + "('" + list1.options[i].value + "'";
            else
                first = first + ",'" + list1.options[i].value + "'";
            count = count + 1;
        }
    }


    if (count > 1000) {
        alert("Can not select more than 1000" + entity + "\n  Total Selected Records :" + count);
        return false;
    }
    if (first == "") {
        alert("select atleast one item");
        return false;
    }
    else
        if (LstCompany != "") {

            var LstCompanylist1 = document.getElementById(LstCompany);
            var Companylist = "";
            var strLstCompanylist1 = "";
            var strLstCompanylist1_array = "";
            var strLstCompanylist = "";
            for (var i = 0; i < LstCompanylist1.options.length; i++) {
                if (LstCompanylist1.options[i].selected) {
                    strLstCompanylist1 = LstCompanylist1.options[i].text;
                    strLstCompanylist1.trim();
                    strLstCompanylist1_array = strLstCompanylist1.split('|');
                    strLstCompanylist = strLstCompanylist1_array[0].trim();

                    if (Companylist == "") {
                        Companylist = Companylist + "(" + strLstCompanylist + "";
                    }
                    else {
                        Companylist = Companylist + ", " + strLstCompanylist + "";
                        count = count + 1;
                    }
                }
            }


            var Company = document.getElementById(LstCompany);
            if (Company.selectedIndex != -1) {
                document.getElementById(lblCompanyList).innerText = Companylist + ")";
                document.getElementById(lblCompanyList).style.display = 'block';
                document.getElementById(lblCompanyName).style.display = 'block';
            }

        }
    if (LstDivision != "") {

        var LstDivision1 = document.getElementById(LstDivision);
        var Divisionlist = "";
        var strLstDivisionlist1 = "";
        var strLstDivisionlist1_array = "";
        var strLstDivisionlist = "";
        for (var i = 0; i < LstDivision1.options.length; i++) {
            if (LstDivision1.options[i].selected) {
                strLstDivisionlist1 = LstDivision1.options[i].text;
                strLstDivisionlist1.trim();
                strLstDivisionlist1_array = strLstDivisionlist1.split('|');
                strLstDivisionlist = strLstDivisionlist1_array[0].trim();
                if (Divisionlist == "")
                    Divisionlist = Divisionlist + "(" + strLstDivisionlist + "";
                else
                    Divisionlist = Divisionlist + ", " + strLstDivisionlist + "";
                count = count + 1;
            }
        }



        var ProfitCenter = document.getElementById(LstDivision);
        if (ProfitCenter.selectedIndex != -1) {
            document.getElementById(lblProfitCenterList).innerText = Divisionlist + ")";
            document.getElementById(lblProfitCenterList).style.display = 'block';
            document.getElementById(lblProfitCenterName).style.display = 'block';
        }

    } if (LstLocation != "") {

        var LstLocation1 = document.getElementById(LstLocation);
        var Locationlist = "";
        var strLstLocationlist1 = "";
        var strLstLocationlist1_array = "";
        var strLstLocationlist = "";
        for (var i = 0; i < LstLocation1.options.length; i++) {
            if (LstLocation1.options[i].selected) {
                strLstLocationlist1 = LstLocation1.options[i].text;
                strLstLocationlist1.trim();
                strLstLocationlist1_array = strLstLocationlist1.split('|');
                strLstLocationlist = strLstLocationlist1_array[0].trim();
                if (Locationlist == "")
                    Locationlist = Locationlist + "(" + strLstLocationlist + "";
                else
                    Locationlist = Locationlist + ", " + strLstLocationlist + "";
                count = count + 1;
            }
        }



        var CostCenter = document.getElementById(LstLocation);
        if (CostCenter.selectedIndex != -1) {
            document.getElementById(lblCostCenterList).innerText = Locationlist + ")";
            document.getElementById(lblCostCenterList).style.display = 'block';
            document.getElementById(lblCostCenterName).style.display = 'block';
        }

    } if (LstGrade != "") {

        var LstGrade1 = document.getElementById(LstGrade);
        var Gradelist = "";
        var strLstGradelist1 = "";
        var strLstGradelist1_array = "";
        var strLstGradelist = "";
        for (var i = 0; i < LstGrade1.options.length; i++) {
            if (LstGrade1.options[i].selected) {
                strLstGradelist1 = LstGrade1.options[i].text;
                strLstGradelist1.trim();
                strLstGradelist1_array = strLstGradelist1.split('|');
                strLstGradelist = strLstGradelist1_array[0].trim();
                if (Gradelist == "")
                    Gradelist = Gradelist + "(" + strLstGradelist + "";
                else
                    Gradelist = Gradelist + ", " + strLstGradelist + "";
                count = count + 1;
            }
        }




        var Grade = document.getElementById(LstGrade);
        if (Grade.selectedIndex != -1) {
            document.getElementById(lblGradeList).innerText = Gradelist + ")";
            document.getElementById(lblGradeList).style.display = 'block';
            document.getElementById(lblGradeName).style.display = 'block';
        }

    } if (LstDesignation != "") {

        var LstDesignation1 = document.getElementById(LstDesignation);
        var LstDesignationlist = "";
        var strLstDesignationlist1 = "";
        var strLstDesignationlist1_array = "";
        var strLstDesignationlist = "";
        for (var i = 0; i < LstDesignation1.options.length; i++) {
            if (LstDesignation1.options[i].selected) {
                strLstDesignationlist1 = LstDesignation1.options[i].text;
                strLstDesignationlist1.trim();
                strLstDesignationlist1_array = strLstDesignationlist1.split('|');
                strLstDesignationlist = strLstDesignationlist1_array[0].trim();
                if (LstDesignationlist == "")
                    LstDesignationlist = LstDesignationlist + "(" + strLstDesignationlist + "";
                else
                    LstDesignationlist = LstDesignationlist + ", " + strLstDesignationlist + "";
                count = count + 1;
            }
        }




        var Designation = document.getElementById(LstDesignation);
        if (Designation.selectedIndex != -1) {
            document.getElementById(lblDesignationList).innerText = LstDesignationlist + ")";
            document.getElementById(lblDesignationList).style.display = 'block';
            document.getElementById(lblDesignationName).style.display = 'block';
        }

    } if (LstEmployee != "") {

        var LstEmployee1 = document.getElementById(LstEmployee);
        var Employeelist = "";
        var strLstEmployeelist1 = "";
        var strLstEmployeelist1_array = "";
        var strLstEmployeelist = "";
        for (var i = 0; i < LstEmployee1.options.length; i++) {
            if (LstEmployee1.options[i].selected) {

                strLstEmployeelist1 = LstEmployee1.options[i].text;
                strLstEmployeelist1.trim();
                strLstEmployeelist1_array = strLstEmployeelist1.split('|');
                strLstEmployeelist = strLstEmployeelist1_array[0].trim();

                if (Employeelist == "")
                    Employeelist = Employeelist + "(" + strLstEmployeelist + "";
                else
                    Employeelist = Employeelist + ", " + strLstEmployeelist + "";
                count = count + 1;
            }
        }


        var emp = document.getElementById(LstEmployee);
        if (emp.selectedIndex != -1) {
            document.getElementById(lblEmployeeList).innerText = Employeelist + ")";
            document.getElementById(lblEmployeeList).style.display = 'block';
            document.getElementById(lblEmployeeName).style.display = 'block';
        }

    }
    document.getElementById(HiddenVal).value = first + ")";

    ////
    document.getElementById(txtCalendarFrom).disabled = false;
    document.getElementById(btnView).disabled = false;
    document.getElementById(btnReset).disabled = false;
    list1.style.display = "none";
    document.getElementById(btnOK).style.display = "none";
    document.getElementById(btnCancel).style.display = "none";
    document.getElementById(strSearchBox).style.display = "none";
    document.getElementById(Panel1).style.display = "none";
}
///



function RptCloseClick(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSec, strGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstShift, LstSection, LstGrade, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, Panel1, txtCalendarFrom, btnView, btnReset) {
    debugger;
    if (document.getElementById(strEmployee).value == "") {
        document.getElementById(LstEmployee).style.display = "none";
        document.getElementById(RbdEmp + '_0').checked = true;
    }

    if (document.getElementById(strcomany).value == "") {
        document.getElementById(LstCompany).style.display = "none";
        document.getElementById(RbdCompany + '_0').checked = true;
    }

    if (document.getElementById(strlocation).value == "") {
        document.getElementById(LstLocation).style.display = "none";
        document.getElementById(RbdLocation + '_0').checked = true;
    }

    if (document.getElementById(strdivision).value == "") {
        document.getElementById(LstDivision).style.display = "none";
        document.getElementById(RbdDivision + '_0').checked = true;
    }

    if (document.getElementById(strdepartment).value == "") {
        document.getElementById(LstDepartment).style.display = "none";
        document.getElementById(RbdDepartment + '_0').checked = true;
    }

    if (document.getElementById(strShift).value == "") {
        document.getElementById(LstShift).style.display = "none";
        document.getElementById(RbdShift + '_0').checked = true;
    }
    if (strSec != "") {
        $('#' + [txtSearchEmp, txtCompany, txtLocation, txtDivision, txtDepartment, txtShift, txtSection].join(', #')).find('input').prop('value', "");
        if (document.getElementById(strSec).value == "") {
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(RbdSection + '_0').checked = true;
        }

    }
    else {
        $('#' + [txtSearchEmp, txtCompany, txtLocation, txtDivision, txtDepartment, txtShift].join(', #')).find('input').prop('value', "");
    }

    if (strGrade != "") {

        if (document.getElementById(strGrade).value == "") {
            document.getElementById(LstGrade).style.display = "none";
            document.getElementById(RbdGrade + '_0').checked = true;
        }

    }



    document.getElementById(txtCalendarFrom).disabled = false;
    document.getElementById(btnOK).style.display = "none";
    document.getElementById(btnCancel).style.display = "none";
    document.getElementById(Panel1).style.display = "none";
    document.getElementById(btnView).disabled = false;
    document.getElementById(btnReset).disabled = false;
}

function RptEntitySpecificDetails(strVal, strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSection, strGrade,strDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade,LstDesignation, DEmployee, DCompany, DLocation, DDivision, DDepartment, DCategory, DSection, DGrade,DDesignation, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade,RbdDesignation, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade,txtDesignation, Panel1, txtCalenderform, btnView, btnReset, HeadLbl, txtReader, ddlPersonalType) {

    debugger;

    var LoginUser = document.getElementById('lblUserName').innerHTML;
    LoginUser = LoginUser.replace("(", "").replace(")", "").replace(" ", "");


    var strcom, strloc, strdiv, strdept, strcat, Shift, strSec, strGr,strDesg,strchecked = "";
    var lBox, DummylBox;
    var myData = [];

    strcom = document.getElementById(strcomany).value;
    strloc = document.getElementById(strlocation).value;
    strdiv = document.getElementById(strdivision).value;
    strdept = document.getElementById(strdepartment).value;
    if (strShift != "")
        Shift = document.getElementById(strShift).value;
    strcat = "";
    if (strSection != "")
        strSec = document.getElementById(strSection).value;
    else
        strSec = "";
    if (strGrade != "")
        strGr = document.getElementById(strGrade).value;
    else
        strGr = "";
    if (strDesignation != "")
        strDesg = document.getElementById(strDesignation).value;
    else
        strDesg = "";

    strchecked = strVal;
    if (strcom == '')
        strcom = "'N'";
    if (strloc == '')
        strloc = "'N'";
    if (strdiv == '')
        strdiv = "'N'";
    if (strdept == '')
        strdept = "'N'";
    if (strcat == '')
        strcat = "'N'";
    if (Shift == '')
        Shift = "'N'";
    if (strSec == '')
        strSec = "'N'";
    if (strGr == '')
        strGr = "'N'";
    if (strDesg == '')
        strDesg = "'N'";


    if (ddlPersonalType != "") {

        if (document.getElementById(strEmployee).value != "" || document.getElementById(strcomany).value != "" || document.getElementById(strlocation).value != "" || document.getElementById(strdivision).value != "" || document.getElementById(strdepartment).value != "") {
            document.getElementById(ddlPersonalType).disabled = true;
        }

        if (strSec != "") {
            if (document.getElementById(strSection).value != "")
                document.getElementById(ddlPersonalType).disabled = true;

        }


        if (strGrade != "") {
            if (document.getElementById(strGrade).value != "")
                document.getElementById(ddlPersonalType).disabled = true;

        }
        if (strDesignation != "") {
            if (document.getElementById(strDesignation).value != "")
                document.getElementById(ddlPersonalType).disabled = true;

        }
        if (strShift != "") {
            if (document.getElementById(strShift).value != "")
                document.getElementById(ddlPersonalType).disabled = true;
        }
    }

    //Employee
    if (strchecked == 'EMP') {

        if ($('#' + RbdEmp + ' input:checked').val() == '1') {

            document.getElementById(HeadLbl).innerHTML = "Select Employees";
            if (RbdSection != "" && RbdGrade != "") {
                //$('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
            }
            else {
                //$('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }



            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);

            document.getElementById(txtReader).style.display = "none";
            document.getElementById(LstEmployee).style.display = "inline";
            document.getElementById(txtSearchEmp).style.display = "inline";
            document.getElementById(btnOK).style.display = "inline";
            document.getElementById(btnCancel).style.display = "inline";
            document.getElementById(Panel1).style.display = "inline";
            document.getElementById(HeadLbl).style.display = "inline";
            document.getElementById(txtSearchEmp).value = "";
            document.getElementById(btnView).disabled = true;
            document.getElementById(btnReset).disabled = true;
            $('#' + LstEmployee).empty();
            var list = document.getElementById(LstEmployee);
            var list1 = document.getElementById(DEmployee);
            var j = 0;
            for (i = 0; i <= list1.options.length - 1; i++) {
                list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                j++;
            }

        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
        return true;
    }
    document.getElementById(btnOK).style.display = "inline";
    document.getElementById(btnCancel).style.display = "inline";
    document.getElementById(Panel1).style.display = "inline";
    document.getElementById(btnView).disabled = true;
    document.getElementById(btnReset).disabled = true;
    document.getElementById(HeadLbl).style.display = "inline";

    //Company
    if (strchecked == 'COM') {
        if ($('#' + RbdCompany + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Companies";

            if (RbdSection != "") {
                //$('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                    $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
            }

            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(txtReader).style.display = "none";
            document.getElementById(LstCompany).style.display = "inline";
            document.getElementById(txtCompany).style.display = "inline";
            document.getElementById(txtCompany).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstCompany);
                DummylBox = $('#' + DCompany);
                $('#' + LstCompany).empty();
                $('#' + DCompany).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }

    //a
    //Designation
    if (strchecked == 'DES') {
        if ($('#' + RbdDesignation + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Designation";

            if (RbdSection != "") {
                //$('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                    $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
            }

            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(txtReader).style.display = "none";
            document.getElementById(LstDesignation).style.display = "inline";
            document.getElementById(txtDesignation).style.display = "inline";
            document.getElementById(txtDesignation).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstDesignation);
                DummylBox = $('#' + DDesignation);
                $('#' + LstDesignation).empty();
                $('#' + DDesignation).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //a
    //Location
    if (strchecked == 'LOC') {
        if ($('#' + RbdLocation + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Locations";
            if (RbdSection != "") {
                //                    $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                    $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
            }



            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(txtReader).style.display = "none";
            document.getElementById(LstLocation).style.display = "inline";
            document.getElementById(txtDivision).style.display = "inline";
            document.getElementById(txtDivision).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstLocation);
                DummylBox = $('#' + DLocation);
                $("#" + LstLocation).empty();
                $("#" + DLocation).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Division
    else if (strchecked == 'DIV') {
        if ($('#' + RbdDivision + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Divisions";

            if (RbdSection != "") {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                //$('#' + [RbdEmp, RbdCompany, RbdLocation, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                    $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', false);
            }

            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(txtReader).style.display = "none";
            document.getElementById(LstDivision).style.display = "inline";
            document.getElementById(txtLocation).style.display = "inline";
            document.getElementById(txtLocation).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstDivision);
                DummylBox = $('#' + DDivision);
                $("#" + LstDivision).empty();
                $("#" + DDivision).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Department
    else if (strchecked == 'DEP') {
        if ($('#' + RbdDepartment + ' input:checked').val() == '1') {

            document.getElementById(HeadLbl).innerHTML = "Select Departments";
            if (RbdSection != "") {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                //                $('#' + [RbdShift, RbdGrade].join(', #')).find('input').prop('disabled', false);
                $('#' + [RbdGrade].join(', #')).find('input').prop('disabled', false);
            }
            else {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                // $('#' + [RbdShift].join(', #')).find('input').prop('disabled', false);
            }

            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(txtReader).style.display = "none";
            document.getElementById(LstDepartment).style.display = "inline";
            document.getElementById(txtDepartment).style.display = "inline";
            document.getElementById(txtDepartment).value = "";

            if (LoginUser == 'admin') {
                lBox = $('#' + LstDepartment);
                DummylBox = $('#' + DDepartment);
                $("#" + LstDepartment).empty();
                $("#" + DDepartment).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Shift
    else if (strchecked == 'SFT') {
        if ($('#' + RbdShift + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Shifts";

            if (RbdSection != "") {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdGrade, RbdSection].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdSection].join(', #')).find('input').prop('disabled', false);
            }
            else {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }


            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(txtReader).style.display = "none";
            document.getElementById(LstCategory).style.display = "inline";
            document.getElementById(txtShift).style.display = "inline";
            document.getElementById(txtShift).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstCategory);
                DummylBox = $('#' + DCategory);
                $("#" + LstCategory).empty();
                $("#" + DCategory).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }

    }


    //Grade
    else if (strchecked == 'GRD') {
        if ($('#' + RbdGrade + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Grades";

            if (RbdSection != "" && RbdGrade != "") {
                //                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdSection].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }


            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(txtReader).style.display = "none";
            document.getElementById(LstGrade).style.display = "inline";
            document.getElementById(txtGrade).style.display = "inline";
            document.getElementById(txtGrade).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstGrade);
                DummylBox = $('#' + DGrade);
                $("#" + LstGrade).empty();
                $("#" + DGrade).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }

    //Section(group)
    else if (strchecked == 'GRP') {
        if ($('#' + RbdSection + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Groups";

            if (RbdSection != "") {
                //                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdGrade].join(', #')).find('input').prop('disabled', true);


            }
            else {
                //$('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }


            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(txtReader).style.display = "none";
            document.getElementById(LstSection).style.display = "inline";
            document.getElementById(txtSection).style.display = "inline";
            document.getElementById(txtSection).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstSection);
                DummylBox = $('#' + DSection);
                $("#" + LstSection).empty();
                $("#" + DSection).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }

    else if (strchecked == 'RED') {
        if ($('#' + rblReader + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Reader";

            if (RbdSection != "" && RbdGrade != "") {
                //                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdSection].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }


            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(lstReader).style.display = "inline";
            document.getElementById(txtReader).style.display = "inline";
            document.getElementById(txtReader).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + lstReader);
                DummylBox = $('#' + lstReaderDummy);
                $("#" + lstReader).empty();
                $("#" + lstReaderDummy).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }


    if (LoginUser == 'admin') {

        $.ajax({
            url: "Filter/Filter.asmx/FillEntitySpecificDetails",
            type: "POST",
            dataType: "json",
            timeout: 10000,
            data: "{'strcom':'" + escape(strcom) + "','strloc':'" + escape(strloc) + "','strdiv':'" + escape(strdiv) + "','strdept':'" + escape(strdept) + "','strcat':'" + escape(strcat) + "','strchecked':'" + escape(strchecked) + "','strshift':'" + escape(Shift) + "','strSec':'" + escape(strSec) + "','strGRD':'" + escape(strGr) + "','strDesg':'" + escape(strDesg) + "'}",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (msg) {

                myData = $.parseJSON(msg.d);
                if (myData.length > 0 && myData.length != null) {
                    var listItems = [];
                    for (var i = 0; i < myData.length; i++) {
                        listItems.push('<option value="' + myData[i].ID + '">' + myData[i].Value + '</option>');
                    }
                }
                $(lBox).append(listItems.join(''));
                $(DummylBox).append(listItems.join(''));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

                alert("Error...\n" + textStatus + "\n" + errorThrown);
            }
        });
    }
    else {

        //alert('not admin');

    }
}

function RptEntitySpecificDetailsbkp(strVal, strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSection, strGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, DEmployee, DCompany, DLocation, DDivision, DDepartment, DCategory, DSection, DGrade, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, Panel1, txtCalenderform, btnView, btnReset, HeadLbl, txtReader, ddlPersonalType) {

    //debugger;

    var LoginUser = document.getElementById('lblUserName').innerHTML;
    LoginUser = LoginUser.replace("(", "").replace(")", "").replace(" ", "");


    var strcom, strloc, strdiv, strdept, strcat, Shift, strSec, strGr, strchecked = "";
    var lBox, DummylBox;
    var myData = [];

    strcom = document.getElementById(strcomany).value;
    strloc = document.getElementById(strlocation).value;
    strdiv = document.getElementById(strdivision).value;
    strdept = document.getElementById(strdepartment).value;
    if (strShift != "")
        Shift = document.getElementById(strShift).value;
    strcat = "";
    if (strSection != "")
        strSec = document.getElementById(strSection).value;
    else
        strSec = "";
    if (strGrade != "")
        strGr = document.getElementById(strGrade).value;
    else
        strGr = "";

    strchecked = strVal;
    if (strcom == '')
        strcom = "'N'";
    if (strloc == '')
        strloc = "'N'";
    if (strdiv == '')
        strdiv = "'N'";
    if (strdept == '')
        strdept = "'N'";
    if (strcat == '')
        strcat = "'N'";
    if (Shift == '')
        Shift = "'N'";
    if (strSec == '')
        strSec = "'N'";
    if (strGr == '')
        strGr = "'N'";


    if (ddlPersonalType != "") {

        if (document.getElementById(strEmployee).value != "" || document.getElementById(strcomany).value != "" || document.getElementById(strlocation).value != "" || document.getElementById(strdivision).value != "" || document.getElementById(strdepartment).value != "") {
            document.getElementById(ddlPersonalType).disabled = true;
        }

        if (strSec != "") {
            if (document.getElementById(strSection).value != "")
                document.getElementById(ddlPersonalType).disabled = true;

        }


        if (strGrade != "") {
            if (document.getElementById(strGrade).value != "")
                document.getElementById(ddlPersonalType).disabled = true;

        }
        if (strShift != "") {
            if (document.getElementById(strShift).value != "")
                document.getElementById(ddlPersonalType).disabled = true;
        }
    }

    //Employee
    if (strchecked == 'EMP') {

        if ($('#' + RbdEmp + ' input:checked').val() == '1') {

            document.getElementById(HeadLbl).innerHTML = "Select Employees";
            if (RbdSection != "" && RbdGrade != "") {
                //$('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
            }
            else {
                //$('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }



            RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType);

            document.getElementById(LstEmployee).style.display = "inline";
            document.getElementById(txtSearchEmp).style.display = "inline";
            document.getElementById(btnOK).style.display = "inline";
            document.getElementById(btnCancel).style.display = "inline";
            document.getElementById(Panel1).style.display = "inline";
            document.getElementById(HeadLbl).style.display = "inline";
            document.getElementById(txtSearchEmp).value = "";
            document.getElementById(btnView).disabled = true;
            document.getElementById(btnReset).disabled = true;
            $('#' + LstEmployee).empty();
            var list = document.getElementById(LstEmployee);
            var list1 = document.getElementById(DEmployee);
            var j = 0;
            for (i = 0; i <= list1.options.length - 1; i++) {
                list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                j++;
            }

        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
        return true;
    }
    document.getElementById(btnOK).style.display = "inline";
    document.getElementById(btnCancel).style.display = "inline";
    document.getElementById(Panel1).style.display = "inline";
    document.getElementById(btnView).disabled = true;
    document.getElementById(btnReset).disabled = true;
    document.getElementById(HeadLbl).style.display = "inline";

    //Company
    if (strchecked == 'COM') {
        if ($('#' + RbdCompany + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Companies";

            if (RbdSection != "") {
                //$('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                    $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
            }

            RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType);
            document.getElementById(LstCompany).style.display = "inline";
            document.getElementById(txtCompany).style.display = "inline";
            document.getElementById(txtCompany).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstCompany);
                DummylBox = $('#' + DCompany);
                $('#' + LstCompany).empty();
                $('#' + DCompany).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Location
    if (strchecked == 'LOC') {
        if ($('#' + RbdLocation + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Locations";
            if (RbdSection != "") {
                //                    $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                    $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
            }



            RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType);
            document.getElementById(LstLocation).style.display = "inline";
            document.getElementById(txtDivision).style.display = "inline";
            document.getElementById(txtDivision).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstLocation);
                DummylBox = $('#' + DLocation);
                $("#" + LstLocation).empty();
                $("#" + DLocation).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Division
    else if (strchecked == 'DIV') {
        if ($('#' + RbdDivision + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Divisions";

            if (RbdSection != "") {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                //$('#' + [RbdEmp, RbdCompany, RbdLocation, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                    $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', false);
            }

            RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType);
            document.getElementById(LstDivision).style.display = "inline";
            document.getElementById(txtLocation).style.display = "inline";
            document.getElementById(txtLocation).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstDivision);
                DummylBox = $('#' + DDivision);
                $("#" + LstDivision).empty();
                $("#" + DDivision).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Department
    else if (strchecked == 'DEP') {
        if ($('#' + RbdDepartment + ' input:checked').val() == '1') {

            document.getElementById(HeadLbl).innerHTML = "Select Departments";
            if (RbdSection != "") {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                //                $('#' + [RbdShift, RbdGrade].join(', #')).find('input').prop('disabled', false);
                $('#' + [RbdGrade].join(', #')).find('input').prop('disabled', false);
            }
            else {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                // $('#' + [RbdShift].join(', #')).find('input').prop('disabled', false);
            }

            RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType);
            document.getElementById(LstDepartment).style.display = "inline";
            document.getElementById(txtDepartment).style.display = "inline";
            document.getElementById(txtDepartment).value = "";

            if (LoginUser == 'admin') {
                lBox = $('#' + LstDepartment);
                DummylBox = $('#' + DDepartment);
                $("#" + LstDepartment).empty();
                $("#" + DDepartment).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Shift
    else if (strchecked == 'SFT') {
        if ($('#' + RbdShift + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Shifts";

            if (RbdSection != "") {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdGrade, RbdSection].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdSection].join(', #')).find('input').prop('disabled', false);
            }
            else {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }


            RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType);
            document.getElementById(LstCategory).style.display = "inline";
            document.getElementById(txtShift).style.display = "inline";
            document.getElementById(txtShift).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstCategory);
                DummylBox = $('#' + DCategory);
                $("#" + LstCategory).empty();
                $("#" + DCategory).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }

    }


    //Grade
    else if (strchecked == 'GRD') {
        if ($('#' + RbdGrade + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Grades";

            if (RbdSection != "" && RbdGrade != "") {
                //                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdSection].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }


            RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType);
            document.getElementById(LstGrade).style.display = "inline";
            document.getElementById(txtGrade).style.display = "inline";
            document.getElementById(txtGrade).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstGrade);
                DummylBox = $('#' + DGrade);
                $("#" + LstGrade).empty();
                $("#" + DGrade).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }

    //Section(group)
    else if (strchecked == 'GRP') {
        if ($('#' + RbdSection + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Groups";

            if (RbdSection != "") {
                //                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdGrade].join(', #')).find('input').prop('disabled', true);


            }
            else {
                //$('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }


            RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType);
            document.getElementById(LstSection).style.display = "inline";
            document.getElementById(txtSection).style.display = "inline";
            document.getElementById(txtSection).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstSection);
                DummylBox = $('#' + DSection);
                $("#" + LstSection).empty();
                $("#" + DSection).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }

   
   

    if (LoginUser =='admin') {
       
        $.ajax({
            url: "Filter/Filter.asmx/FillEntitySpecificDetails",
            type: "POST",
            dataType: "json",
            timeout: 10000,
            data: "{'strcom':'" + escape(strcom) + "','strloc':'" + escape(strloc) + "','strdiv':'" + escape(strdiv) + "','strdept':'" + escape(strdept) + "','strcat':'" + escape(strcat) + "','strchecked':'" + escape(strchecked) + "','strshift':'" + escape(Shift) + "','strSec':'" + escape(strSec) + "','strGRD':'" + escape(strGr) + "'}",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (msg) {

                myData = $.parseJSON(msg.d);
                if (myData.length > 0 && myData.length != null) {
                    var listItems = [];
                    for (var i = 0; i < myData.length; i++) {
                        listItems.push('<option value="' + myData[i].ID + '">' + myData[i].Value + '</option>');
                    }
                }
                $(lBox).append(listItems.join(''));
                $(DummylBox).append(listItems.join(''));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

                alert("Error...\n" + textStatus + "\n" + errorThrown);
            }
        });
    }
    else {

        //alert('not admin');
        
    }
}


//more
function RptEntitySpecificDetails2(strVal, strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSection, strGrade,strDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade,LstDesignation, DEmployee, DCompany, DLocation, DDivision, DDepartment, DCategory, DSection, DGrade,DDesignation, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade,RbdDesignation, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade,txtDesignation, Panel1, txtCalenderform, btnView, btnReset, HeadLbl, txtReader, ddlPersonalType) {

    debugger;

    var LoginUser = document.getElementById('lblUserName').innerHTML;
    LoginUser = LoginUser.replace("(", "").replace(")", "").replace(" ", "");


    var strcom, strloc, strdiv, strdept, strcat, Shift, strSec, strGr,strDesg, strchecked = "";
    var lBox, DummylBox;
    var myData = [];

    strcom = document.getElementById(strcomany).value;
    strloc = document.getElementById(strlocation).value;
    strdiv = document.getElementById(strdivision).value;
    strdept = document.getElementById(strdepartment).value;
    if (strShift != "")
        Shift = document.getElementById(strShift).value;
    strcat = "";
    if (strSection != "")
        strSec = document.getElementById(strSection).value;
    else
        strSec = "";
    if (strGrade != "")
        strGr = document.getElementById(strGrade).value;
    else
        strGr = "";
    if (strDesignation != "")
        strDesg = document.getElementById(strDesignation).value;
    else
        strDesg = "";

    strchecked = strVal;
    if (strcom == '')
        strcom = "'N'";
    if (strloc == '')
        strloc = "'N'";
    if (strdiv == '')
        strdiv = "'N'";
    if (strdept == '')
        strdept = "'N'";
    if (strcat == '')
        strcat = "'N'";
    if (Shift == '')
        Shift = "'N'";
    if (strSec == '')
        strSec = "'N'";
    if (strGr == '')
        strGr = "'N'";
    if (strDesg == '')
        strDesg = "'N'";


    if (ddlPersonalType != "") {

        if (document.getElementById(strEmployee).value != "" || document.getElementById(strcomany).value != "" || document.getElementById(strlocation).value != "" || document.getElementById(strdivision).value != "" || document.getElementById(strdepartment).value != "") {
            document.getElementById(ddlPersonalType).disabled = true;
        }

        if (strSec != "") {
            if (document.getElementById(strSection).value != "")
                document.getElementById(ddlPersonalType).disabled = true;

        }


        if (strGrade != "") {
            if (document.getElementById(strGrade).value != "")
                document.getElementById(ddlPersonalType).disabled = true;

        }
        if (strDesignation != "") {
            if (document.getElementById(strDesignation).value != "")
                document.getElementById(ddlPersonalType).disabled = true;

        }
        if (strShift != "") {
            if (document.getElementById(strShift).value != "")
                document.getElementById(ddlPersonalType).disabled = true;
        }
    }

    //Employee
    if (strchecked == 'EMP') {

        if ($('#' + RbdEmp + ' input:checked').val() == '1') {

            document.getElementById(HeadLbl).innerHTML = "Select Employees";
            if (RbdSection != "" && RbdGrade != "") {
                //$('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
            }
            else {
                //$('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }



            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);

            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";
            document.getElementById(LstEmployee).style.display = "inline";
            document.getElementById(txtSearchEmp).style.display = "inline";
            document.getElementById(btnOK).style.display = "inline";
            document.getElementById(btnCancel).style.display = "inline";
            document.getElementById(Panel1).style.display = "inline";
            document.getElementById(HeadLbl).style.display = "inline";
            document.getElementById(txtSearchEmp).value = "";
            document.getElementById(btnView).disabled = true;
            document.getElementById(btnReset).disabled = true;
            $('#' + LstEmployee).empty();
            var list = document.getElementById(LstEmployee);
            var list1 = document.getElementById(DEmployee);
            var j = 0;
            for (i = 0; i <= list1.options.length - 1; i++) {
                list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                j++;
            }

        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
        return true;
    }
    document.getElementById(btnOK).style.display = "inline";
    document.getElementById(btnCancel).style.display = "inline";
    document.getElementById(Panel1).style.display = "inline";
    document.getElementById(btnView).disabled = true;
    document.getElementById(btnReset).disabled = true;
    document.getElementById(HeadLbl).style.display = "inline";

    //Company
    if (strchecked == 'COM') {
        if ($('#' + RbdCompany + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Manager";

            if (RbdSection != "") {
                //$('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                    $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
            }

            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstCompany).style.display = "inline";
            document.getElementById(txtCompany).style.display = "inline";
            document.getElementById(txtCompany).value = "";
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";
            if (LoginUser == 'admin') {
//                lBox = $('#' + LstCompany);
//                DummylBox = $('#' + DCompany);
//                $('#' + LstCompany).empty();
//                $('#' + DCompany).empty();
                //
                lBox = $('#' + LstCompany);
                DummylBox = $('#' + DCompany);
                $("#" + LstCompany).empty();
                var list = document.getElementById(LstCompany);
                var list1 = document.getElementById(DCompany);
                var j = 0;
                for (i = 0; i <= list1.options.length - 1; i++) {
                    list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                    j++;
                }
                //


            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }

    //a
    //Designation
    if (strchecked == 'DES') {
        if ($('#' + RbdDesignation + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Designation";

            if (RbdSection != "") {
                //$('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                    $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
            }

            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstDesignation).style.display = "inline";
            document.getElementById(txtDesignation).style.display = "inline";
            document.getElementById(txtDesignation).value = "";
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";
            if (LoginUser == 'admin') {
                //                lBox = $('#' + LstCompany);
                //                DummylBox = $('#' + DCompany);
                //                $('#' + LstCompany).empty();
                //                $('#' + DCompany).empty();
                //
                lBox = $('#' + LstDesignation);
                DummylBox = $('#' + DDesignation);
                $("#" + LstDesignation).empty();
                var list = document.getElementById(LstDesignation);
                var list1 = document.getElementById(DDesignation);
                var j = 0;
                for (i = 0; i <= list1.options.length - 1; i++) {
                    list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                    j++;
                }
                //


            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }

    //a


    //Location
    if (strchecked == 'LOC') {
        if ($('#' + RbdLocation + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Controller";
            if (RbdSection != "") {
                //                    $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                    $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
            }



            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstLocation).style.display = "inline";
            document.getElementById(txtDivision).style.display = "inline";
            document.getElementById(txtDivision).value = "";
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstLocation);
                DummylBox = $('#' + DLocation);
                $("#" + LstLocation).empty();
                var list = document.getElementById(LstLocation);
                var list1 = document.getElementById(DLocation);
                var j = 0;
                for (i = 0; i <= list1.options.length - 1; i++) {
                    list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                    j++;
                }
               // $("#" + DLocation).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Division
    else if (strchecked == 'DIV') {
        if ($('#' + RbdDivision + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Divisions";

            if (RbdSection != "") {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                //$('#' + [RbdEmp, RbdCompany, RbdLocation, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                    $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', false);
            }

            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstDivision).style.display = "inline";
            document.getElementById(txtLocation).style.display = "inline";
            document.getElementById(txtLocation).value = "";
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstDivision);
                DummylBox = $('#' + DDivision);
                $("#" + LstDivision).empty();
                $("#" + DDivision).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Department
    else if (strchecked == 'DEP') {
        if ($('#' + RbdDepartment + ' input:checked').val() == '1') {

            document.getElementById(HeadLbl).innerHTML = "Select Departments";
            if (RbdSection != "") {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                //                $('#' + [RbdShift, RbdGrade].join(', #')).find('input').prop('disabled', false);
                $('#' + [RbdGrade].join(', #')).find('input').prop('disabled', false);
            }
            else {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                // $('#' + [RbdShift].join(', #')).find('input').prop('disabled', false);
            }

            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstDepartment).style.display = "inline";
            document.getElementById(txtDepartment).style.display = "inline";
            document.getElementById(txtDepartment).value = "";
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";

            if (LoginUser == 'admin') {
                lBox = $('#' + LstDepartment);
                DummylBox = $('#' + DDepartment);
                $("#" + LstDepartment).empty();
                $("#" + DDepartment).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Shift
    else if (strchecked == 'SFT') {
        if ($('#' + RbdShift + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Shifts";

            if (RbdSection != "") {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdGrade, RbdSection].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdSection].join(', #')).find('input').prop('disabled', false);
            }
            else {
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }


            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstCategory).style.display = "inline";
            document.getElementById(txtShift).style.display = "inline";
            document.getElementById(txtShift).value = "";
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstCategory);
                DummylBox = $('#' + DCategory);
                $("#" + LstCategory).empty();
                $("#" + DCategory).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }

    }


    //Grade
    else if (strchecked == 'GRD') {
        if ($('#' + RbdGrade + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Grades";

            if (RbdSection != "" && RbdGrade != "") {
                //                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdSection].join(', #')).find('input').prop('disabled', false);
            }
            else {
                //                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }


            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstGrade).style.display = "inline";
            document.getElementById(txtGrade).style.display = "inline";
            document.getElementById(txtGrade).value = "";
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstGrade);
                DummylBox = $('#' + DGrade);
                $("#" + LstGrade).empty();
                $("#" + DGrade).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }

    //Section(group)
    else if (strchecked == 'GRP') {
        if ($('#' + RbdSection + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Groups";

            if (RbdSection != "") {
                //                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdGrade].join(', #')).find('input').prop('disabled', true);


            }
            else {
                //$('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment].join(', #')).find('input').prop('disabled', true);
            }


            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstSection).style.display = "inline";
            document.getElementById(txtSection).style.display = "inline";
            document.getElementById(txtSection).value = "";
            if (LoginUser == 'admin') {
                lBox = $('#' + LstSection);
                DummylBox = $('#' + DSection);
                $("#" + LstSection).empty();
                $("#" + DSection).empty();
            }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
}
//more

//aniket
function RptEntitySpecificDetails1(strVal, strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSection, strGrade,strDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade,LstDesignation, DEmployee, DCompany, DLocation, DDivision, DDepartment, DCategory, DSection, DGrade,DDesignation, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade,RbdDesignation, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade,txtDesignation, Panel1, txtCalenderform, btnView, btnReset, HeadLbl, txtReader, ddlPersonalType) {

    debugger;

    var LoginUser = document.getElementById('lblUserName').innerHTML;
    LoginUser = LoginUser.replace("(", "").replace(")", "").replace(" ", "");


    var strcom, strloc, strdiv, strdept, strcat, Shift, strSec, strGr,strDesg, strchecked = "";
    var lBox, DummylBox;
    var myData = [];

    strcom = document.getElementById(strcomany).value;
    strloc = document.getElementById(strlocation).value;
    strdiv = document.getElementById(strdivision).value;
    strdept = document.getElementById(strdepartment).value;
    if (strShift != "")
        Shift = document.getElementById(strShift).value;
    strcat = "";
    if (strSection != "")
        strSec = document.getElementById(strSection).value;
    else
        strSec = "";
    if (strGrade != "")
        strGr = document.getElementById(strGrade).value;
    else
        strGr = "";
    if (strDesignation != "")
        strDesg = document.getElementById(strDesignation).value;
    else
        strDesg = "";

    strchecked = strVal;
    if (strcom == '')
        strcom = "'N'";
    if (strloc == '')
        strloc = "'N'";
    if (strdiv == '')
        strdiv = "'N'";
    if (strdept == '')
        strdept = "'N'";
    if (strcat == '')
        strcat = "'N'";
    if (Shift == '')
        Shift = "'N'";
    if (strSec == '')
        strSec = "'N'";
    if (strGr == '')
        strGr = "'N'";
    if (strDesg == '')
        strDesg = "'N'";


    if (ddlPersonalType != "") {

        if (document.getElementById(strEmployee).value != "" || document.getElementById(strcomany).value != "" || document.getElementById(strlocation).value != "" || document.getElementById(strdivision).value != "" || document.getElementById(strdepartment).value != "") {
            document.getElementById(ddlPersonalType).disabled = true;
        }

        if (strSec != "") {
            if (document.getElementById(strSection).value != "")
                document.getElementById(ddlPersonalType).disabled = true;

        }


        if (strGrade != "") {
            if (document.getElementById(strGrade).value != "")
                document.getElementById(ddlPersonalType).disabled = true;

        }
        if (strDesignation != "") {
            if (document.getElementById(strDesignation).value != "")
                document.getElementById(ddlPersonalType).disabled = true;

        }
        if (strShift != "") {
            if (document.getElementById(strShift).value != "")
                document.getElementById(ddlPersonalType).disabled = true;
        }
    }

    //Employee
    if (strchecked == 'EMP') {
        debugger;
        if ($('#' + RbdEmp + ' input:checked').val() == '1') {

            document.getElementById(HeadLbl).innerHTML = "Select Employees";
            {
                //$('#' + [RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                //$('#' + [RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdCompany].join(', #')).find('input').prop('disabled', false);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdSection].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdGrade].join(', #')).find('input').prop('disabled', true);
         

            }



            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";
            document.getElementById(LstEmployee).style.display = "inline";
            document.getElementById(txtSearchEmp).style.display = "inline";
            document.getElementById(btnOK).style.display = "inline";
            document.getElementById(btnCancel).style.display = "inline";
            document.getElementById(Panel1).style.display = "inline";
            document.getElementById(HeadLbl).style.display = "inline";
            document.getElementById(txtSearchEmp).value = "";
            document.getElementById(btnView).disabled = true;
            document.getElementById(btnReset).disabled = true;
            $('#' + LstEmployee).empty();
            lBox = $('#' + LstEmployee);
            DummylBox = $('#' + DEmployee);
            var list = document.getElementById(LstEmployee);
            var list1 = document.getElementById(DEmployee);
            var j = 0;
            for (i = 0; i <= list1.options.length - 1; i++) {
                list.options[j] = new Option(list1.options[i].text, list1.options[i].value);
                j++;
            }

        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
        //return true;
    }
    document.getElementById(btnOK).style.display = "inline";
    document.getElementById(btnCancel).style.display = "inline";
    document.getElementById(Panel1).style.display = "inline";
    document.getElementById(btnView).disabled = true;
    document.getElementById(btnReset).disabled = true;
    document.getElementById(HeadLbl).style.display = "inline";

    //Company
    if (strchecked == 'COM') {
        if ($('#' + RbdCompany + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Companies";

            
             {
                //                    $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                // $('#' + [RbdEmp, RbdLocation, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
                 $('#' + [RbdEmp].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdShift].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdSection].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdGrade].join(', #')).find('input').prop('disabled', true);
            
                
            }

             RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstCompany).style.display = "inline";
            document.getElementById(txtCompany).style.display = "inline";
            document.getElementById(txtCompany).value = "";
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";
            //if (LoginUser == 'admin') {
                lBox = $('#' + LstCompany);
                DummylBox = $('#' + DCompany);
                $('#' + LstCompany).empty();
                $('#' + DCompany).empty();
           // }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Location//cost center
    if (strchecked == 'LOC') {
        if ($('#' + RbdLocation + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Cost Center";
            
             {
                //                    $('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                 //$('#' + [RbdEmp, RbdCompany, RbdDivision, RbdDepartment, RbdShift, RbdSection].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdDesignation].join(', #')).find('input').prop('disabled', false);
                 $('#' + [RbdGrade].join(', #')).find('input').prop('disabled', false);
                 $('#' + [RbdEmp].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdCompany].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdShift].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdSection].join(', #')).find('input').prop('disabled', true);
            }



            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstLocation).style.display = "inline";
            document.getElementById(txtDivision).style.display = "inline";
            document.getElementById(txtDivision).value = "";
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";
            //if (LoginUser == 'admin') {
                lBox = $('#' + LstLocation);
                DummylBox = $('#' + DLocation);
                $("#" + LstLocation).empty();
                $("#" + DLocation).empty();
            //}
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Division
     if (strchecked == 'DIV') {
        if ($('#' + RbdDivision + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Profit Center";

            
             {
                //                    $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdShift].join(', #')).find('input').prop('disabled', true);
                 //$('#' + [RbdEmp, RbdCompany, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', false);
                 $('#' + [RbdEmp].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdCompany].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdShift].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdSection].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdGrade].join(', #')).find('input').prop('disabled', true);

            }

            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstDivision).style.display = "inline";
            document.getElementById(txtLocation).style.display = "inline";
            document.getElementById(txtLocation).value = "";
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";
            //if (LoginUser == 'admin') {
                lBox = $('#' + LstDivision);
                DummylBox = $('#' + DDivision);
                $("#" + LstDivision).empty();
                $("#" + DDivision).empty();
            //}
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    

    //Grade
     if (strchecked == 'GRD') {
        if ($('#' + RbdGrade + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Grades";

             {
                //                $('#' + [RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                 //$('#' + [ RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdEmp].join(', #')).find('input').prop('disabled', false);
                 $('#' + [RbdCompany].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdShift].join(', #')).find('input').prop('disabled', true);
                 $('#' + [RbdSection].join(', #')).find('input').prop('disabled', true);
              
            }


            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstGrade).style.display = "inline";
            document.getElementById(txtGrade).style.display = "inline";
            document.getElementById(txtGrade).value = "";
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";
            //if (LoginUser == 'admin') {
                lBox = $('#' + LstGrade);
                DummylBox = $('#' + DGrade);
                $("#" + LstGrade).empty();
                $("#" + DGrade).empty();
            //}
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    //Designation
    if (strchecked == 'DES') {
        if ($('#' + RbdDesignation + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Designation";


            {
                //                    $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                // $('#' + [RbdEmp, RbdLocation, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
                $('#' + [RbdEmp].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdSection].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdGrade].join(', #')).find('input').prop('disabled', true);
            }

            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstDesignation).style.display = "inline";
            document.getElementById(txtDesignation).style.display = "inline";
            document.getElementById(txtDesignation).value = "";
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(txtSection).style.display = "none";
            document.getElementById(txtSection).value = "";

            //if (LoginUser == 'admin') {
            lBox = $('#' + LstDesignation);
            DummylBox = $('#' + DDesignation);
            $('#' + LstDesignation).empty();
            $('#' + DDesignation).empty();
            // }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }

    //Designation
    if (strchecked == 'GRP') {
        if ($('#' + RbdSection + ' input:checked').val() == '1') {
            document.getElementById(HeadLbl).innerHTML = "Select Group";


            {
                //                    $('#' + [RbdEmp, RbdDivision, RbdDepartment, RbdShift].join(', #')).find('input').prop('disabled', true);
                // $('#' + [RbdEmp, RbdLocation, RbdDepartment, RbdShift, RbdSection, RbdGrade].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDivision].join(', #')).find('input').prop('disabled', false);
                $('#' + [RbdEmp].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdLocation].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdDepartment].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdShift].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdSection].join(', #')).find('input').prop('disabled', true);
                $('#' + [RbdGrade].join(', #')).find('input').prop('disabled', true);
            }

            RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, LstDesignation, txtReader, ddlPersonalType);
            document.getElementById(LstSection).style.display = "inline";
            document.getElementById(txtSection).style.display = "inline";
            document.getElementById(txtSection).value = "";
            //if (LoginUser == 'admin') {
            lBox = $('#' + LstSection);
            DummylBox = $('#' + DSection);
            $('#' + LstSection).empty();
            $('#' + DSection).empty();
            // }
        }
        else {
            document.getElementById(Panel1).style.display = "none";
            return true;
        }
    }
    
    //if (LoginUser == 'admin') {

        $.ajax({
            url: "Filter/Filter.asmx/FillEntitySpecificDetails",
            type: "POST",
            dataType: "json",
            timeout: 10000,
            data: "{'strcom':'" + escape(strcom) + "','strloc':'" + escape(strloc) + "','strdiv':'" + escape(strdiv) + "','strdept':'" + escape(strdept) + "','strcat':'" + escape(strcat) + "','strchecked':'" + escape(strchecked) + "','strshift':'" + escape(Shift) + "','strSec':'" + escape(strSec) + "','strGRD':'" + escape(strGr) + "','strDesg':'" + escape(strDesg) + "'}",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (msg) {

                myData = $.parseJSON(msg.d);
                if (myData.length > 0 && myData.length != null) {
                    var listItems = [];
                    for (var i = 0; i < myData.length; i++) {
                        listItems.push('<option value="' + myData[i].ID + '">' + myData[i].Value + '</option>');
                    }
                }
                $(lBox).append(listItems.join(''));
                $(DummylBox).append(listItems.join(''));
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

                alert("Error...\n" + textStatus + "\n" + errorThrown);
            }
        });
    //}
    //else {

        //alert('not admin');

   // }
}



function RptDisable(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade, txtReader, ddlPersonalType) {
    if (txtSection != "") {
        document.getElementById(txtSection).style.display = "none";
        document.getElementById(LstSection).style.display = "none";
    }
    if (txtGrade != "") {
        document.getElementById(txtGrade).style.display = "none";
        document.getElementById(LstGrade).style.display = "none";
    }

    if (txtReader != "") {
        document.getElementById(txtReader).style.display = "none";
    }

    document.getElementById(txtSearchEmp).style.display = "none";
    document.getElementById(txtCompany).style.display = "none";
    document.getElementById(txtDivision).style.display = "none";
    document.getElementById(txtLocation).style.display = "none";
    document.getElementById(txtDepartment).style.display = "none";
    document.getElementById(txtShift).style.display = "none";
    document.getElementById(LstEmployee).style.display = "none";
    document.getElementById(LstCompany).style.display = "none";
    document.getElementById(LstLocation).style.display = "none";
    document.getElementById(LstDivision).style.display = "none";
    document.getElementById(LstDepartment).style.display = "none";
    document.getElementById(LstCategory).style.display = "none";

}

function RptDisable1(txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade,txtDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstCategory, LstSection, LstGrade,LstDesignation, txtReader, ddlPersonalType) {
    if (txtSection != "") {
        document.getElementById(txtSection).style.display = "none";
        document.getElementById(LstSection).style.display = "none";
    }
    if (txtGrade != "") {
        document.getElementById(txtGrade).style.display = "none";
        document.getElementById(LstGrade).style.display = "none";
    }

    if (txtDesignation != "") {
        document.getElementById(txtDesignation).style.display = "none";
        document.getElementById(LstDesignation).style.display = "none";
    }

    if (txtReader != "") {
        document.getElementById(txtReader).style.display = "none";
    }

    document.getElementById(txtSearchEmp).style.display = "none";
    document.getElementById(txtCompany).style.display = "none";
    document.getElementById(txtDivision).style.display = "none";
    document.getElementById(txtLocation).style.display = "none";
    document.getElementById(txtDepartment).style.display = "none";
    document.getElementById(txtShift).style.display = "none";
    document.getElementById(LstEmployee).style.display = "none";
    document.getElementById(LstCompany).style.display = "none";
    document.getElementById(LstLocation).style.display = "none";
    document.getElementById(LstDivision).style.display = "none";
    document.getElementById(LstDepartment).style.display = "none";
    document.getElementById(LstCategory).style.display = "none";

}



//Reports Okclick
function ReaderOkClick(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSec, strGrade,strDesignation, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstShift, LstSection, LstGrade,LstDesignation, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade,txtDesignation, Panel1, txtCalendarFrom, btnView, btnReset, lstReader, Readerhdn, txtReader) {
    var first = "";
    var count = 0;
    var list1;
    var HiddenVal;
    var entity;
    var strSearchBox;
    if (document.getElementById(LstEmployee).style.display == "inline") {
        list1 = document.getElementById(LstEmployee);
        HiddenVal = strEmployee;
        entity = "Employees";
        strSearchBox = txtSearchEmp;
    }

    if (document.getElementById(LstCompany).style.display == "inline") {
        list1 = document.getElementById(LstCompany);
        HiddenVal = strcomany;
        entity = "Companies";
        strSearchBox = strcomany;
    }

    if (document.getElementById(LstLocation).style.display == "inline") {
        list1 = document.getElementById(LstLocation);
        HiddenVal = strlocation;
        entity = "Locations";
        strSearchBox = txtLocation;
    }

    if (document.getElementById(LstDivision).style.display == "inline") {
        list1 = document.getElementById(LstDivision);
        HiddenVal = strdivision;
        entity = "Divisions";
        strSearchBox = txtDivision;
    }

    if (document.getElementById(LstDepartment).style.display == "inline") {
        list1 = document.getElementById(LstDepartment);
        HiddenVal = strdepartment;
        entity = "Departments";
        strSearchBox = txtDepartment;
    }

    if (document.getElementById(LstShift).style.display == "inline") {
        list1 = document.getElementById(LstShift);
        HiddenVal = strShift;
        entity = "Shifts";
        strSearchBox = txtShift;
    }
    if (LstSection != "") {

        if (document.getElementById(LstSection).style.display == "inline") {
            list1 = document.getElementById(LstSection);
            HiddenVal = strSec;
            entity = "Sections";
            strSearchBox = txtSection;
        }
    }

    if (LstGrade != "") {

        if (document.getElementById(LstGrade).style.display == "inline") {
            list1 = document.getElementById(LstGrade);
            HiddenVal = strGrade;
            entity = "Divisions";
            strSearchBox = txtGrade;
        }
    }

    if (document.getElementById(lstReader).style.display == "inline") {
        list1 = document.getElementById(lstReader);
        HiddenVal = Readerhdn;
        entity = "Reader";
        strSearchBox = txtReader;
    }


    if (LstDesignation != "") {
        if (document.getElementById(LstDesignation).style.display == "inline") {
            list1 = document.getElementById(LstDesignation);
            HiddenVal = strDesignation;
            entity = "Designations";
            strSearchBox = txtDesignation;
        }
    }




    ///
    for (var i = 0; i < list1.options.length; i++) {
        if (list1.options[i].selected) {
            if (first == "")
                first = first + "('" + list1.options[i].value + "'";
            else
                first = first + ",'" + list1.options[i].value + "'";
            count = count + 1;
        }
    }
    if (count > 1000) {
        alert("Can not select more than 1000" + entity + "\n  Total Selected Records :" + count);
        return false;
    }
    if (first == "") {
        alert("select atleast one item");
        return false;
    }
    else
        document.getElementById(HiddenVal).value = first + ")";

    ////
    document.getElementById(txtCalendarFrom).disabled = false;
    document.getElementById(btnView).disabled = false;
    document.getElementById(btnReset).disabled = false;
    list1.style.display = "none";
    document.getElementById(btnOK).style.display = "none";
    document.getElementById(btnCancel).style.display = "none";
    document.getElementById(strSearchBox).style.display = "none";
    document.getElementById(Panel1).style.display = "none";
}




//aa
function ReaderOkClickbkp(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSec, strGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstShift, LstSection, LstGrade, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, Panel1, txtCalendarFrom, btnView, btnReset, lstReader, Readerhdn, txtReader) {
    var first = "";
    var count = 0;
    var list1;
    var HiddenVal;
    var entity;
    var strSearchBox;
    if (document.getElementById(LstEmployee).style.display == "inline") {
        list1 = document.getElementById(LstEmployee);
        HiddenVal = strEmployee;
        entity = "Employees";
        strSearchBox = txtSearchEmp;
    }

    if (document.getElementById(LstCompany).style.display == "inline") {
        list1 = document.getElementById(LstCompany);
        HiddenVal = strcomany;
        entity = "Companies";
        strSearchBox = strcomany;
    }

    if (document.getElementById(LstLocation).style.display == "inline") {
        list1 = document.getElementById(LstLocation);
        HiddenVal = strlocation;
        entity = "Locations";
        strSearchBox = txtLocation;
    }

    if (document.getElementById(LstDivision).style.display == "inline") {
        list1 = document.getElementById(LstDivision);
        HiddenVal = strdivision;
        entity = "Divisions";
        strSearchBox = txtDivision;
    }

    if (document.getElementById(LstDepartment).style.display == "inline") {
        list1 = document.getElementById(LstDepartment);
        HiddenVal = strdepartment;
        entity = "Departments";
        strSearchBox = txtDepartment;
    }

    if (document.getElementById(LstShift).style.display == "inline") {
        list1 = document.getElementById(LstShift);
        HiddenVal = strShift;
        entity = "Shifts";
        strSearchBox = txtShift;
    }
    if (LstSection != "") {

        if (document.getElementById(LstSection).style.display == "inline") {
            list1 = document.getElementById(LstSection);
            HiddenVal = strSec;
            entity = "Sections";
            strSearchBox = txtSection;
        }
    }

    if (LstGrade != "") {

        if (document.getElementById(LstGrade).style.display == "inline") {
            list1 = document.getElementById(LstGrade);
            HiddenVal = strGrade;
            entity = "Divisions";
            strSearchBox = txtGrade;
        }
    }

    if (document.getElementById(lstReader).style.display == "inline") {
        list1 = document.getElementById(lstReader);
        HiddenVal = Readerhdn;
        entity = "Reader";
        strSearchBox = txtReader;
    }

    ///
    for (var i = 0; i < list1.options.length; i++) {
        if (list1.options[i].selected) {
            if (first == "")
                first = first + "('" + list1.options[i].value + "'";
            else
                first = first + ",'" + list1.options[i].value + "'";
            count = count + 1;
        }
    }
    if (count > 1000) {
        alert("Can not select more than 1000" + entity + "\n  Total Selected Records :" + count);
        return false;
    }
    if (first == "") {
        alert("select atleast one item");
        return false;
    }
    else
        document.getElementById(HiddenVal).value = first + ")";

    ////
    document.getElementById(txtCalendarFrom).disabled = false;
    document.getElementById(btnView).disabled = false;
    document.getElementById(btnReset).disabled = false;
    list1.style.display = "none";
    document.getElementById(btnOK).style.display = "none";
    document.getElementById(btnCancel).style.display = "none";
    document.getElementById(strSearchBox).style.display = "none";
    document.getElementById(Panel1).style.display = "none";
}


//aa

function ReaderCancelClick(strEmployee, strcomany, strlocation, strdivision, strdepartment, strShift, strSec, strGrade, LstEmployee, LstCompany, LstLocation, LstDivision, LstDepartment, LstShift, LstSection, LstGrade, RbdEmp, RbdCompany, RbdLocation, RbdDivision, RbdDepartment, RbdShift, RbdSection, RbdGrade, btnOK, btnCancel, txtSearchEmp, txtCompany, txtDivision, txtLocation, txtDepartment, txtShift, txtSection, txtGrade, Panel1, txtCalendarFrom, btnView, btnReset, lstReader, rblReader, ReaderHdn) {
    if (document.getElementById(strEmployee).value == "") {
        document.getElementById(LstEmployee).style.display = "none";
        document.getElementById(RbdEmp + '_0').checked = true;
    }

    if (document.getElementById(strcomany).value == "") {
        document.getElementById(LstCompany).style.display = "none";
        document.getElementById(RbdCompany + '_0').checked = true;
    }

    if (document.getElementById(strlocation).value == "") {
        document.getElementById(LstLocation).style.display = "none";
        document.getElementById(RbdLocation + '_0').checked = true;
    }

    if (document.getElementById(strdivision).value == "") {
        document.getElementById(LstDivision).style.display = "none";
        document.getElementById(RbdDivision + '_0').checked = true;
    }

    if (document.getElementById(strdepartment).value == "") {
        document.getElementById(LstDepartment).style.display = "none";
        document.getElementById(RbdDepartment + '_0').checked = true;
    }

    if (document.getElementById(strShift).value == "") {
        document.getElementById(LstShift).style.display = "none";
        document.getElementById(RbdShift + '_0').checked = true;
    }
    if (strSec != "") {
        $('#' + [txtSearchEmp, txtCompany, txtLocation, txtDivision, txtDepartment, txtShift, txtSection].join(', #')).find('input').prop('value', "");
        if (document.getElementById(strSec).value == "") {
            document.getElementById(LstSection).style.display = "none";
            document.getElementById(RbdSection + '_0').checked = true;
        }

    }
    else {
        $('#' + [txtSearchEmp, txtCompany, txtLocation, txtDivision, txtDepartment, txtShift].join(', #')).find('input').prop('value', "");
    }

    if (strGrade != "") {

        if (document.getElementById(strGrade).value == "") {
            document.getElementById(LstGrade).style.display = "none";
            document.getElementById(RbdGrade + '_0').checked = true;
        }

    }
    if (document.getElementById(ReaderHdn).value == "") {
        document.getElementById(lstReader).style.display = "none";
        document.getElementById(rblReader + '_0').checked = true;
    }



    document.getElementById(txtCalendarFrom).disabled = false;
    document.getElementById(btnOK).style.display = "none";
    document.getElementById(btnCancel).style.display = "none";
    document.getElementById(Panel1).style.display = "none";
    document.getElementById(btnView).disabled = false;
    document.getElementById(btnReset).disabled = false;
}



//CallManagement

function FillFaultBasedOnCategory(ddlComplaintCategory, ddlCallType) {
    var myData = [];

    var Category = document.getElementById(ddlComplaintCategory).value;

    lBox = $('#' + ddlCallType);
    $('#' + ddlCallType).empty();

    $.ajax({
        url: "Filter/Filter.asmx/FillFaultType",
        type: "POST",
        dataType: "json",
        timeout: 10000,
        data: "{'Category':'" + Category + "'}",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (msg) {

            myData = $.parseJSON(msg.d);
            if (myData.length > 0 && myData.length != null) {
                var listItems = [];
                var strAll = "All";
                if (Category == "All")
                    listItems.push('<option value="All">' + strAll + '</option>');
                for (var i = 0; i < myData.length; i++) {
                    listItems.push('<option value="' + myData[i].ID + '">' + myData[i].Value + '</option>');
                }
            }
            $(lBox).append(listItems.join(''));


        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

            //  alert("Error...\n" + textStatus + "\n" + errorThrown);
        }
    });
    return false;


}


function ValidateForm(FromDate, ToDate) {
    if (!CompareDates(document.getElementById(FromDate), document.getElementById(ToDate))) {
        alert("To Date should not be less than From date");
        return false;
    }
    return true;
}


function lpad(number, length) {

    var str = '' + number;
    while (str.length < length) {
        str = '0' + str;
    }

    return str;

}