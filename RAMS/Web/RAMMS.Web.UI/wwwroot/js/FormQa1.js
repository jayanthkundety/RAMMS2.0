$(document).ready(function () {

    $("#formQa1rmu").on("change", function () {
        var val = $(this).find(":selected").text();
        val = val.split("-").length > 0 ? val.split("-")[1] : val;
        $("#formQa1rmuDesc").val(val);
        var req = {};
        req.Section = '';
        req.RoadCode = '';
        req.RMU = $("#formQa1rmu option:selected").text().split("-")[1];
        $.ajax({
            url: '/MAM/RMUSecRoad',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                if (data != null) {
                    $("#formQa1SectionCode").empty();
                    $("#formQa1SectionCode").append($("<option></option>").val("").html("Select Section Code"));
                    $.each(data.section, function (index, v) {
                        $("#formQa1SectionCode").append($("<option></option>").val(v.value).html(v.text));
                    });
                    $("#formQa1SectionCode").trigger("change");
                    $('#formQa1SectionCode').trigger("chosen:updated");

                } else {

                }
            },
            error: function (data) {

                console.error(data);
            }
        });
    });

    $("#formQa1Crew").on("change", function () {
        var val = $("#formQa1Crew option:selected").text();
        var Name = val.split("-")[1];
        var ctrl = $(this);
        if (val != "99999999" && val != "") {
            $("#formQa1CrewName").val(Name);
        }
        else if (val == "99999999") {
            $("#txt CrewName").prop("disabled", false);
            $("#formQa1CrewName").val('');
        }
        else {
            $("#formQa1CrewName").prop("disabled", true);
            $("#formQa1CrewName").val('');
        }
        return false;
    });

    $("#formQa1SectionCode").on("change", function () {
        var ddldata = $(this).val();
        if (ddldata != "") {
            $("#formQa1SectionName").val(ddldata);
        }
        else {
            $("#formQa1SectionName").val("");
        }
        return false;
    });

    $("#formQa1ActivityCode").on("change", function () {
        var val = $(this).find(":selected").text();
        val = val.split("-").length > 0 ? val.split("-")[1] : val;
        $("#formQa1ActivityName").val(val);
    });

});