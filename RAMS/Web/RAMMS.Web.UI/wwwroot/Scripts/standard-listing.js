$(document).ready(function () {
    BindClickToSortingPaging('#content', '#contentPager a,#main-table > thead > tr > th a');
});

$("#btnSearch").click(function () {
    //alert(encodeURIComponent($("#txtSearch").val()));
    var OptionList = [{ Name: 'Keyword', Value: GetEncodedKeyword() }];
    //alert(OptionList);
    LoadList(OptionList);
});
$('#txtSearch').on("keypress", function (e) {
    if (e.keyCode == 13) {
        var OptionList = [{ Name: 'Keyword', Value: GetEncodedKeyword() }];
        LoadList(OptionList);
        return false; // prevent the button click from happening
    }
});
function LoadList(OptionList) {
    $('#main-message-placeholder').html('');
    if (arguments.length == 0) {
        // alert('ajax call back');
        var OptionList = [
                { Name: 'PageNumber', Value: $("#PageNumber").val() },
                { Name: 'SortOrder', Value: $("#SortOrder").val() },
                { Name: 'SortDirection', Value: $("#SortDirection").val() },
                { Name: 'DoSorting', Value: 'false' },
                { Name: 'Keyword', Value: GetEncodedKeyword() }
        ];
        LoadMyList(OptionList, '#content', '#main-message-placeholder');
    } else if (arguments.length == 1) {
        //alert('search');
        //alert('normal paging/sorting click');
        // alert($("#txtSearch").val());
        LoadMyList(OptionList, '#content', '#main-message-placeholder');
    }
}
function GetEncodedKeyword() {
    return encodeURIComponent($("#txtSearch").val());
}