function HandleValidationErrorSummaryIfAny(response, formId) {
    var isValidForm = true;
    if (response.status == 400) {
        //var validationSummary = $('#ValidationSummary div.validation-summary-valid ul');
        //validationSummary.empty();
        var validationElements = "";
        $.each(response.responseJSON, function (index, jsonObject) {
            $.each(jsonObject, function (key, val) {
                console.log("key : " + key + " ; value : " + val);
                //validationSummary.append('<li>' + val + '</li>');
                validationElements = validationElements + '<li><label class="error">' + val + '</label></li>';
            });
            isValidForm = false;
        });

        if (!isValidForm) {
            var modalElement = '<div class="modal fade" id="validationModal" aria-hidden="true" role="dialog" aria-labelledby="AssetDataDownloadCenterTitle">';
            modalElement = modalElement + '<div class="modal-dialog modal-dialog-centered modal-xl" role="document" style="width:30%">';
            modalElement = modalElement + '<div class="modal-content">';
            modalElement = modalElement + '<div class="modal-header"> <h5 class="modal-title" id="validationModalCenterTitle"> ';
            modalElement = modalElement + 'Validation Messages </h5>';
            modalElement = modalElement + '<button type="button" class="close" data-dismiss="modal" aria-label="Close"> <span aria-hidden="true">&times;</span> </button> </div> <div class="modal-body"> <div id="ValidationSummary">';
            modalElement = modalElement + '<ul> ' + validationElements + '</ul>';
            modalElement = modalElement + '</div> </div> </div> </div> </div>';

            $("#validationModal").remove();
            $('body').append(modalElement);

            $('#validationModal').modal('show');
            //this.validationModal.modal('show');

            //$.notify("<div id='divVMSGCtrl' class='msgul'>Application Validation Messages </div><ul class='msgul'>" + validationElements + "</ul>",
            //    {
            //        autoHide: false,
            //        className: "warn",
            //        globalPosition: 'top center',
            //        IsHTML: true
            //    });

            var valSummaryDisplayer = $("#" + formId).find("#val-summary-displayer");
            if (valSummaryDisplayer.length) {
                valSummaryDisplayer.html('<button id="btnShowValidationMessageBox" type="button" class="show-validation-message-box btn btn-sm btn-themebtn" title="Click to View Validation Messages"><span class="validation-icon"></span></button>');

                $(".show-validation-message-box").bind('click', function () {
                    if ($("#validationModal").length) {
                        $('#validationModal').modal('show');
                    }
                    else {
                        alert('No Validation Messages to show');
                    }

                });
            }

        }
        return isValidForm;
    }
}

//$(document).on("click", "#btnShowValidationMessageBox", function () {
//    $('#validationModal').modal('show');
//});
