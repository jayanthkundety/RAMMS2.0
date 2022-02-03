var notification = new function () {
    this.TopCount = 10;
    this.UpTo = null;
    this.Template = "<a href='' class='dropdown-item notify-item'><div class='notify-icon'><span class='bell-icon'></span></div><p class='notify-details'><span msg></span><small class='text-muted'></small></p></a>";
    this.Data = [];
    this.ShowNotification = false;
    this.init = function () {
        var self = this;
        var post = {};
        post.Count = this.TopCount;
        post.from = this.UpTo;
        PostData("Process/GetNotification", post, function (data) {
            if (data && data.NotifyList && data.NotifyList.length > 0) {
                self.UpTo = data.NotifyList[0].Dt;
                self.Data = data.NotifyList.concat(self.Data);
                if (self.ShowNotification) { app.ShowNotification(data.NotifyList.length + " New Notification"); }
                var li = $("#liNotification").show();
                if (li.find("[notificationMenu]:visible").length > 0) {
                    li.find("[notificationMenu]:visible").hide();
                    self.Show();
                }
            }
            setTimeout(function () { notification.init(); }, 30 * 1000);
            self.ShowNotification = true;
        }, "", false);
    }
    this.BindData = function (item, obj, isAppend) {
        var self = this;
        var temp = $(self.Template);
        temp.attr("href", obj.URL);
        var not = temp.find(".notify-details");
        not.find("[msg]").text(obj.Msg);
        not.find("small").text(moment(obj.Dt).fromNow());
        if (isAppend) { item.append(temp); } else { item.prepend(temp); }
    }
    this.Show = function () {
        var self = this;
        var li = $("#liNotification");
        var menu = li.find("[notificationMenu]:visible");
        if (menu.length == 0) {
            if (this.Data && this.Data.length > 0) {
                var item = li.find("[notificationItem]");
                item.empty();
                $.each(this.Data, function (idx, obj) {
                    self.BindData(item, obj, true);
                });
                li.find("[notificationMenu]").slideDown('slow');
            }
        }
        else {
            menu.slideUp('slow');
        }
    }
    this.event = function () {
        var self = this;
        $("#liNotification [notification]").on("click", function () { self.Show(); });
    }
}
$(document).click(function (e) {
    $clicked = $(e.target);
    if ($clicked.closest('.dropdown').length === 0 && $("#liNotification [notificationMenu]:visible").length > 0) {
        $("#liNotification [notificationMenu]:visible").hide();
    }
});
