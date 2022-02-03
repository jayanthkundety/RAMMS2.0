var formJS = new function () {
    this.ActionRender = function (data) {
        //debugger;
        var title = "";
        if (data.RefID != "" && data.RefID != null) {
            title = data.RefID;
        }
        if (data.SubmitSts) {
            var actionSection = "<div class='btn-group dropright' id='actiondropdown'> <button id='actionclick' type='button' class='btn btn-sm btn-themebtn dropdown-toggle' data-toggle='dropdown'> Click Me </button><div class='dropdown-menu'><button type='button' class='dropdown-item editdel-btns' data-toggle='modal' data-target='#FormAAddModal'  data-backdrop='static' onclick='javascript:ViewDetailHeader(" + data.RefNo + ",&#39;" + title + "&#39;);'><span class='view-icon'></span> View</button><button type='button' class='dropdown-item editdel-btns' onclick='javascript:DeleteHeaderRecord(" + data.RefNo + ");'><span class='del-icon'></span> Delete</button><button type='button' class='dropdown-item editdel-btns' id='btnAssetView' onclick='javascript:PrintForm(" + data.RefNo + ");' href=''><span class='print-icon'></span> Print</button></div></div>";
            return actionSection;
        }
        else {
            var actionSection = "<div class='btn-group dropright' id='actiondropdown'> <button id='actionclick' type='button' class='btn btn-sm btn-themebtn dropdown-toggle' data-toggle='dropdown'> Click Me </button><div class='dropdown-menu'><button type='button' class='dropdown-item editdel-btns' data-toggle='modal' data-target='#FormAAddModal' data-backdrop='static' id='formAHeaderEdit' onclick='javascript:openFormAModel(" + data.RefNo + ",&#39;" + title + "&#39;);'><span class='edit-icon'></span> Edit</button><button type='button' class='dropdown-item editdel-btns' data-toggle='modal' data-target='#FormAAddModal' data-backdrop='static' onclick='javascript:ViewDetailHeader(" + data.RefNo + ",&#39;" + title + "&#39;);'><span class='view-icon'></span> View</button><button type='button' class='dropdown-item editdel-btns' onclick='javascript:DeleteHeaderRecord(" + data.RefNo + ");'><span class='del-icon'></span> Delete</button><button type='button' class='dropdown-item editdel-btns' id='btnAssetView' onclick='javascript:PrintForm(" + data.RefNo + ");' href=''><span class='print-icon'></span> Print</button></div></div>";
            return actionSection;
        }
    }
}
new dropDownInject("#formADetSrchRMU", "#formADetSrchSec", false);

