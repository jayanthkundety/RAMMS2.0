var mAMQA2 = new function () {
    this.ActionRender = function (data) {
        var title = "";
        var isModifyPerm = tblHeaderGrid.Base.IsModify ? "" : 'hidden';
        var isDeletePerm = tblHeaderGrid.Base.IsDelete ? "" : 'hidden';
        if (data.RefID != "" && data.RefID != null) {
            title = data.RefID;
        }
        if (data.SubmitSts) {
            var actionSection = "<div class='btn-group dropright' id='actiondropdown'> <button id='actionclick' type='button' class='btn btn-sm btn-themebtn dropdown-toggle' data-toggle='dropdown'> Click Me </button><div class='dropdown-menu'><button type='button' class='dropdown-item editdel-btns' data-backdrop='static' onclick='javascript:EditForm(" + data.RefNo + ",1 );'><span class='view-icon'></span> View</button><button type='button' class='dropdown-item editdel-btns'" + isDeletePerm+" onclick='javascript:DeleteHeaderRecord(" + data.RefNo + ");'><span class='del-icon'></span> Delete</button><button type='button' class='dropdown-item editdel-btns' id='btnAssetView' onclick='javascript:Qa2Print(" + data.RefNo + ");' href=''><span class='print-icon'></span> Print</button></div></div>";
            return actionSection;
        }
        else {
            var actionSection = "<div class='btn-group dropright' id='actiondropdown'> <button id='actionclick' type='button' class='btn btn-sm btn-themebtn dropdown-toggle' data-toggle='dropdown'> Click Me </button><div class='dropdown-menu'><button type='button' class='dropdown-item editdel-btns'" + isModifyPerm + " data-backdrop='static' onclick='javascript:EditForm(" + data.RefNo + ",0 );'><span class='edit-icon'></span> Edit</button><button type='button' class='dropdown-item editdel-btns'  data-backdrop='static' onclick='javascript:EditForm(" + data.RefNo + ",1 );'><span class='view-icon'></span> View</button><button type='button' class='dropdown-item editdel-btns'" + isDeletePerm +" onclick='javascript:DeleteHeaderRecord(" + data.RefNo + ");'><span class='del-icon'></span> Delete</button><button type='button' class='dropdown-item editdel-btns' id='btnAssetView' onclick='javascript:Qa2Print(" + data.RefNo + ");' href=''><span class='print-icon'></span> Print</button></div></div>";
            return actionSection;
        }
    }
}
$(document).ready(function () {
    document.getElementById("smartSearch").focus();
})
function EditForm(id, view) {
    window.location.href = "EditFormQa2?headerId=" + id + "&view=" + view;
}
function DeleteHeaderRecord(id) {
    var headerId = id;
    if (app.Confirm("Are you sure want to delete the record?", function (e) {
        if (e) {
            InitAjaxLoading();
            $.ajax({
                url: '/MAM/FormQA2HeaderListDelete',
                data: { headerId },
                type: 'POST',
                success: function (data) {
                    HideAjaxLoading();
                    if (data > 0) {
                        app.ShowSuccessMessage("Successfully Deleted"); 
                        headerGridRefresh(); 
                    }
                    else {
                        alert("Error in Deleted. Kindly retry later.");
                    }
                }
            });
        }
    }));
}

function Qa2Print(id) {
  
    window.location.href = '/MAM/Qa2Print?id=' + id;
    
}
$("#formQa2SrchRoadCode").on("change", function () {
    var rdCode = $(this).val()
    rdNameByRdCode(rdCode);
})
$("#formQa2SrchActCode").on("change", function () {
    var val = $("#formQa2SrchActCode").find(":selected").text();
    var ActName = val.split('-');
    $("#formQa2SrchActivityName").val(ActName[1]);
})
$("#FormQa2SrchRMU").on("change", function () {
    var obj = new Object();
    detailSearchDdList(obj);
})
function detailSearchDdList(obj) {
    var val = $("#FormQa2SrchRMU").find(":selected").text().split('-');
    obj.RMU = val[1]
    searchList(obj);
}
function searchList(obj) {
    $.ajax({
        url: '/Assets/detailSearchDdList',
        data: obj,
        type: 'Post',
        success: function (data) {
            if (obj.RdCode == "" || obj.RdCode == null || obj.RdCode == 0) {
                $("#formQa2SrchRoadCode option").remove();
                $("#formQa2SrchRoadCode").append($('<option>').val(null).text("Select Road Code"));
                $.each(data.rdCode, function (index, value) {
                    $("#formQa2SrchRoadCode").append($('<option>').val(value.value).html(value.text));
                    $("#formQa2SrchRoadCode").trigger("chosen:updated");
                })
                $("#formQa2SrchRoadName").val(null);
            }
        }
    })
}

function headerGridRefresh() {   
    $('[searchsectionbtn]').click();
}

function rdNameByRdCode(rdCode) {
    $.ajax({
        url: '/MAM/GetDivisionByRoadCode',
        dataType: 'JSON',
        data: { roadCode: rdCode },
        type: 'Post',
        success: function (data) {
            if (data != null) {
                if (data._RMAllData != undefined && data._RMAllData != null) {
                    $("#formQa2SrchRoadName").val(data._RMAllData.roadName);
                }
            }
        },
        error: function (data) {
            console.error(data);
        }
    });
}