
var SystemSetting = {
    allowedImageFileExt: ['jpeg', 'jpg', 'gif', 'png'],
    allowedAttachmentFileExt: ['pdf', 'docx', 'doc', 'xls', "xlsx", "ppt", "pptx", "dwg", "txt", "zip", "rar", "mp4", "avi", "wmv"],
    allowAllAttachment: ["pdf", "docx", "doc", "xls", "xlsx", "ppt", "pptx", "dwg", "txt", "zip", "rar", "mp4", "avi", "wmv", "jpeg", "jpg", "gif", "png"]
}
//function BindModalFormClick(ClassID, MessagePlaceHolderID, ErrorMessagePlaceHolderID)
//{
//    //alert('c me?');
//    //$(ContentID).unbind('click').on("click", ElementID, function () {
//    $(ClassID).unbind('click').on("click", function () {
//        //$(ClassID).click(function () {

//        var ID = $(this).attr("data-ID");
//        var Param = $(this).attr("data-Param");
//        var str = this.href + "?ID=" + ID;

//        if (Param != null)
//            str = str + "&" + Param;
        
//         $('#simpleModal').load(str, function (response, status, xhr) {
//             //alert(status);
//             if (status == "error") {
//                 //var msg = "Sorry but there was an error: ";
//                 // $("#error").html(msg + xhr.status + " " + xhr.statusText);
//                 $(MessagePlaceHolderID).html(MessageBox(MessageType.Error, "Failed to process your request! Please refresh and try again. Error code: " + xhr.status + " - " + xhr.statusText));
//             } else {
//                 $('#simpleModal').modal('show');
//                 //bindForm(this, ID, '#main-message-placeholder', '#modal-message-placeholder');
//                 $(MessagePlaceHolderID).html('');
//                 $(ErrorMessagePlaceHolderID).html('');
//                 bindForm(this, ID, MessagePlaceHolderID, ErrorMessagePlaceHolderID);
//             }

//         });
         
//         return false;
//     });
//}
function BindModalFormClick(ClassID, MessagePlaceHolderID, ErrorMessagePlaceHolderID, RefreshCallback) {
    //$(ContentID).unbind('click').on("click", ElementID, function () {
   //alert('c me 2?');
    $(ClassID).unbind('click').on("click", function () {
        //$(ClassID).click(function () {

        var ID = $(this).attr("data-ID");
        var Param = $(this).attr("data-Param");
        var str = this.href + "?PK=" + ID;
        //alert(Param);
        if (Param != null)
            str = str + "&" + Param;

        $('#simpleModal').load(str, function (response, status, xhr) {
            //alert(status);
            if (status == "error") {
                //var msg = "Sorry but there was an error: ";
                // $("#error").html(msg + xhr.status + " " + xhr.statusText);
                $(MessagePlaceHolderID).html(MessageBox(MessageType.Error, "Failed to process your request! Please refresh and try again. Error code: " + xhr.status + " - " + xhr.statusText));
            } else {
                $('#simpleModal').modal('show');
                //bindForm(this, ID, '#main-message-placeholder', '#modal-message-placeholder');
                $(MessagePlaceHolderID).html('');
                $(ErrorMessagePlaceHolderID).html('');
                bindForm(this, ID, MessagePlaceHolderID, ErrorMessagePlaceHolderID, RefreshCallback);
            }

        });

        return false;
    });
}

//function bindForm(dialog, ID, MessagePlaceHolderID, ErrorMessagePlaceHolderID) {
    
//    //alert(ID);
//    //alert(this.method);
//   // alert('bindForm');
//    $('form', dialog).unbind('submit').bind('submit', function () {
//       // alert(this.action);
//        //$('form', dialog).submit(function () {
//        $.ajax({
//            cache: false,
//            url: this.action,
//            type: this.method,
//            data: $(this).serialize() + '&ID=' + ID,
//            modal: true,
//            success: function (result) {
//                if (result.success) {
//                    alert('thanks for submitting');
//                    $('#simpleModal').modal('hide');
//                    if (typeof LoadList !== 'undefined' && $.isFunction(LoadList)) {
//                        LoadList();
//                        if (result.message)
//                        {
//                            //alert(MessagePlaceHolderID);
//                            $(MessagePlaceHolderID).html(MessageBox(MessageType.OK, result.message));
//                        }
                        
                      
//                    }
//                } else {
//                    // $('#simpleModal').html(result);
//                    //alert(result.error);
//                    $(ErrorMessagePlaceHolderID).html(result.error);
//                   // bindForm();
//                    //if (typeof LoadList !== 'undefined' && $.isFunction(LoadList)) {
//                    //    LoadList();
//                    //}
//                }
//            },
//            error: function (xhr, status, error) {
//                //  alert("Error!" + error);
//                $(ErrorMessagePlaceHolderID).html(MessageBox(MessageType.Error, "Failed to process your request! Please refresh and try again. Error code: " + xhr.status + " - " + error));
//            }
//        });
//        return false;
//    });
//}
 
function bindForm(dialog, ID, MessagePlaceHolderID, ErrorMessagePlaceHolderID, RefreshCallback) {
    //alert($.isFunction(RefreshCallback));
    $('form', dialog).unbind('submit').bind('submit', function () {
            // alert('bindform');
            //alert($(this).serialize());
            $.ajax({
                cache: false,
                url: this.action,
                type: this.method,
                //data: $(this).serialize() + '&ID=' + ID,
                data: $(this).serialize() + '&PK=' + ID,
                modal: true,
                success: function (result) {
                    //alert('method found');
                    if (result.success) {
                        $('#simpleModal').modal('hide');
                        
                        if (RefreshCallback !== 'undefined' && $.isFunction(RefreshCallback)) {
                            RefreshCallback();
                            if (result.message) {
                                $(MessagePlaceHolderID).html(MessageBox(MessageType.OK, result.message));
                            }


                        }
                        else if (typeof LoadList !== 'undefined' && $.isFunction(LoadList)) {
                            LoadList();
                            if (result.message) {
                                //alert(MessagePlaceHolderID);
                                $(MessagePlaceHolderID).html(MessageBox(MessageType.OK, result.message));
                            }


                        }
                    } else {
                        //alert('no method found');
                        $(ErrorMessagePlaceHolderID).html(result.error);
                    }
                },
                error: function (xhr, status, error) {
                   // alert('kao luck found');
                    $(ErrorMessagePlaceHolderID).html(MessageBox(MessageType.Error, "Failed to process your request! Please refresh and try again. Error code: " + xhr.status + " - " + error));
                }
            });
            return false;
        });
    }



//End Form Method

//Formatting Methods
function Format_Boolean(boolean, arr )
{
    if(boolean)
    {
        return arr[0];
    }
    else
    {
        return arr[1];
    }
}
//End Formatting

//Listing Methods
function LoadPagingIndicator(TableID,SortOrderID,SortDirectionID) {
        $( TableID + ' > thead > tr > th > a').each(function () {
            // var page = GetParameterByName($(this).attr("href"), "page");
            var SortOrder = GetParameterByName($(this).attr("href"), "SortOrder");
            var SortDirection = GetParameterByName($(this).attr("href"), "SortDirection");
            //var Keyword = GetParameterByName($(this).attr("href"), "Keyword");

            if ($(SortOrderID).val() == SortOrder) {
                //alert(SortDirection);
                if ($(SortDirectionID).val() == 'Asc') {
                    $(this).text($(this).text() + ' ▲')
                } else {
                    $(this).text($(this).text() + ' ▼')
                }
            }
        }); //END .each
   
}
//function BindClickToSortingPaging(ContentID, ElementID) {
//    $(ContentID).unbind('click').on("click", ElementID, function () {
//        var OptionList = [];
//        var sPageURL = $(this).attr("href").split('?');
//        if (sPageURL.length > 0) {

//            var sURLVariables = sPageURL[1].split('&');
//            for (var i = 0; i < sURLVariables.length; i++) {
//                var sParameterName = sURLVariables[i].split('=');
//                //alert(sParameterName);
//                var element = {};
//                element.Name = sParameterName[0];
//                element.Value = sParameterName[1];
//                OptionList.push(element);
//            }
//        }
//        LoadList(OptionList);
//        return false;
//    });
//}
function BindClickToSortingPaging(ContentID, ElementID, CallBack) {
    //alert('fff');
    $(ContentID).unbind('click').on("click", ElementID, function () {
        var OptionList = [];
        var sPageURL = $(this).attr("href").split('?');
        if (sPageURL.length > 0) {

            var sURLVariables = sPageURL[1].split('&');
            for (var i = 0; i < sURLVariables.length; i++) {
                var sParameterName = sURLVariables[i].split('=');
                //alert(sParameterName);
                var element = {};
                element.Name = sParameterName[0];
                element.Value = sParameterName[1];
                OptionList.push(element);
            }
        }

        if (typeof CallBack !== 'undefined' && $.isFunction(CallBack)) {
            CallBack(OptionList);
        }
        else
        {
            LoadList(OptionList);
        }
       
        return false;
    });
}
//function LoadMyList(SearchOption,ContentClassID,MessagePlaceHolderID) {
////function LoadMyList(keyword, PageNumber, SortOrder, SortDirection, DoSorting) {
    
//    //  alert($(this).serialize() + '&Keyword=' + keyword + '&page=' + PageNumber);
//    //alert(LoadMyListSettings.ActionURL);
//    var url = "";
   
///*
//    var OptionList = [
//    { Name: 'SortOrder', Value: '1' },
//   { Name: 'PageNumber', Value: '1' },
//{ Name: 'SortDirection', Value: 'Asc' }
//    ];
//    */
//    //var OptionList = [];
//    //var element = {};
//    //for (var key in SearchOption) {
//    //    //alert("key " + key + " has value " + SearchOption[key]);
//    //    // url += "&" + key + "=" + SearchOption[key];
//    //    var element = {};
//    //    element.Name = key;
//    //    element.Value = SearchOption[key];
//    //    OptionList.push(element);
//    //    //alert(element);
//    //    // OptionList.push({ Name: Key, Value: SearchOption[key] })

//    //}
   
//    OptionList = JSON.stringify({ 'OptionList': SearchOption });
//    //alert(OptionList);
//    //jQuery.ajaxSettings.traditional = true;
//    //alert(url);
//    $.ajax({
//        contentType: 'application/json; charset=utf-8',
//        url: LoadMyListSettings.ActionURL,
//        //data: $(this).serialize() + '&Keyword=' + keyword + '&page=' + PageNumber + '&SortOrder=' + SortOrder + '&SortDirection=' + SortDirection + '&DoSorting=' + DoSorting,
//        // data: $(this).serialize() +url,
//        data:OptionList,
//        type: 'POST',
//        cache: false,
//        success: function (result) {

//            $(ContentClassID).html(result);
//        },
//        error: function (xhr, status, error) {
//          //  alert("Error!" + error);
//            $(MessagePlaceHolderID).html(MessageBox(MessageType.Error, "Failed to load record! Please refresh and try again. Error code: " + xhr.status +" - " + error));
//        }
//    });
//}

function LoadMyList(SearchOption, ContentClassID, MessagePlaceHolderID,ActionURL) {

    var url = "";
    if (typeof ActionURL === "undefined") {
        // ...
        url = LoadMyListSettings.ActionURL;
    } else
    {
        url = ActionURL;
    }
    //alert(url);
    //alert('actionurl' + ActionURL);
    OptionList = JSON.stringify({ 'OptionList': SearchOption });
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        url: url,
        data: OptionList,
        type: 'POST',
        cache: false,
        success: function (result) {
            $(ContentClassID).html(result);
        },
        error: function (xhr, status, error) {
            $(MessagePlaceHolderID).html(MessageBox(MessageType.Error, "Failed to load record! Please refresh and try again. Error code: " + xhr.status + " - " + error));
        }
    });
}

//End Listing Methods
//Message Box Methods
var MessageType = {
    Error: 1,
    Information: 2,
    OK: 3,
    Warning: 4,
    
}
function MessageBox(MT,Message)
{
    //alert('g');
    var icon = "";
    var type = "";
    //alert(MessageType);
    //alert(MessageType.Error);
    switch (MT)
    {
        case MessageType.Error:
            icon = "glyphicon-remove-sign";
            type = "alert-danger";
            break;
        case MessageType.Information:
            icon = "glyphicon-info-sign";
            type = "alert-info";
            break;
        case MessageType.OK:
            icon = "glyphicon-ok-sign";
            type = "alert-success";
            break;
        case MessageType.Warning:
            icon = "glyphicon-exclamation-sign";
            type = "alert-warning";
            break;
    }
    var HTML = "<div class=\"alert " + type + " alert-dismissable\" role=\"alert\">";
    HTML += "<span class=\"close\" data-dismiss=\"alert\">&times;</span>";
    HTML += "<span class=\"glyphicon " + icon + "\" aria-hidden=\"true\"></span>";
    HTML += "<span class=\"sr-only\">Error:</span>";
    HTML += Message;
    HTML += "</div>"
   // alert(HTML);
    return HTML;
}
//End Message Box Methods

//Misc Methods


function GetParameterByName(url,name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(url);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function AppendMessageToTable(TableID,CssClass,Message)
{
    var tr = $("<tr></tr>");
    tr.append("<td class=\"" + CssClass + "\" colspan=\"" + $( TableID + " > thead").children('tr').children('th').length + "\">" + Message + "</td>");
    $( TableID + ' > tbody:last').append(tr);
}


//Auto Complete
function InitiateAutoComplete(TextBoxID, val_URL, hidID) {
    $(TextBoxID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: val_URL,
                //data: { QueryText: request.term, LawFirmID: val_LawFirmID },
                data: { QueryText: request.term },
                dataType: 'json',
                type: 'GET',
                success: function (data) {
                    $(hidID).val("");
                    response($.map(data, function (item) {
                        return {
                            label: item.ID,
                            value: item.PK
                        }
                    }));
                }
            })
        },
        focus: function (event, ui) {
            event.preventDefault();
            $(TextBoxID).val(ui.item.label);
        },
        select: function (event, ui) {
            $(TextBoxID).val(ui.item.label);
            $(hidID).val(ui.item.value);
            return false;
        },
        minLength: 3
    });
}
//Auto Complete End

function CalculateGST_OnAmount(Amount_ID, GSTRate_ID, GSTAmount_ID, TotalAmount_ID) {
    var objAmount = $(Amount_ID);
    var objGSTRate = $(GSTRate_ID);
    var objGST = $(GSTAmount_ID);

    var objTotal = $(TotalAmount_ID);

    if (parseFloat(objAmount.val()) > 0 && parseFloat(objGSTRate.val()) > 0) {
        var GSTAmount = (parseFloat(objAmount.val()) * (parseFloat(objGSTRate.val()) / 100.00));
        var TotalAmount = (parseFloat(objAmount.val()) + GSTAmount);

        objGST.val(parseFloat(GSTAmount).toFixed(2));
        objTotal.val(parseFloat(TotalAmount).toFixed(2));
    }
    else {
        objGST.val('0.00');
        objTotal.val('0.00');
    }
}

//End Misc Methods

$(document).ajaxSend(function (event, request, settings) {
    $('#loading-indicator').show();
});

$(document).ajaxComplete(function (event, request, settings) {
    $('#loading-indicator').hide();
});

function getLatitude(obj) {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            obj.val(position.coords.latitude);
        });

    } else {
        alert("Geolocation is not supported by this browser.");
    }
}

function getLongitude(obj) {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            obj.val(position.coords.longitude);
        });

    } else {
        alert("Geolocation is not supported by this browser.");
    }
}



