function ShowSuccessMessage(message) {
    $.toast({
        heading: 'Success',
        text: message,
        position: 'bottom-right',
        icon: 'success',
        loader: true,        // Change it to false to disable loader
        loaderBg: '#08966e'  // To change the background
    });
}

function ShowErrorMessage(message) {
    $.toast({
        heading: 'Error',
        text: message,
        position: 'bottom-right',
        icon: 'error',
        loader: true,        // Change it to false to disable loader
        loaderBg: '#e61a4b'  // To change the background
    });
}

function ShowWarningMessage(message) {
    $.toast({
        heading: 'Warning',
        text: message,
        position: 'bottom-right',
        icon: 'warning',
        loader: true,        // Change it to false to disable loader
        loaderBg: '#c18a00'  // To change the background
    });
}

function ShowInfoMessage(message) {
    $.toast({
        heading: 'Info',
        text: message,
        position: 'bottom-right',
        icon: 'info',
        loader: true,        // Change it to false to disable loader
        loaderBg: '#1b859e'  // To change the background
    });
}

//------------------------
var app = new function () {

    //function reset() {
    //    alertify.set({
    //        labels: {
    //            ok: "OK",
    //            cancel: "Cancel"
    //        },
    //        delay: 5000,
    //        buttonReverse: false,
    //        buttonFocus: "ok"
    //    });
    //}

    this.Confirm = function (message, callback, yes, cancel) {
        yes = yes ? yes : "OK";
        cancel = cancel ? cancel : "Cancel";  
        alertify.set({ labels: { ok: yes, cancel: cancel } });
        alertify.confirm(message, callback);
    }
    this.Alert = function (message, ok) {
        this.ShowDialog(message, ok);
    }
    this.ShowDialog = function (message, ok) {
        ok = ok ? ok : "OK";
        alertify.set({ labels: { ok: ok, cancel: "Cancel" } });
        alertify.alert(message);
    }

    this.ShowSuccessMessage = function (message) {
        $.toast({
            heading: 'Success',
            text: message,
            position: 'bottom-right',
            icon: 'success',
            loader: true,        // Change it to false to disable loader
            loaderBg: '#08966e'  // To change the background
        });
    }

    this.ShowErrorMessage = function (message) {
        return $.toast({
            heading: 'Error',
            text: message,
            position: 'bottom-right',
            icon: 'error',
            loader: true,        // Change it to false to disable loader
            loaderBg: '#e61a4b'  // To change the background
        });
    }

    this.ShowWarningMessage = function (message) {
        $.toast({
            heading: 'Warning',
            text: message,
            position: 'bottom-right',
            icon: 'warning',
            loader: true,        // Change it to false to disable loader
            loaderBg: '#c18a00'  // To change the background
        });
    }
    this.ShowNotification = function (message) {
        $.toast({
            heading: '',
            text: message,
            position: 'top-right',
            icon: 'warning',
            loader: true,        // Change it to false to disable loader
            loaderBg: '#c18a00'  // To change the background
        });
    }

    this.ShowInfoMessage = function (message) {
        $.toast({
            heading: 'Info',
            text: message,
            position: 'bottom-right',
            icon: 'info',
            loader: true,        // Change it to false to disable loader
            loaderBg: '#1b859e'  // To change the background
        });
    }
}