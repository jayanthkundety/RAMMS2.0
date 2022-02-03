var jsSection = new function () {
    this.Master = {};
    this.timer = 0;
    this.SectionaName = "";
    this.Init = function (sec, isInit) {
        this.SectionaName = sec;
        var par = $("#roadmapcode");
        if (isInit) {
            par.find("[roadSearch]").on("keydown", function () {
                if (jsSection.timer > 0) { clearTimeout(jsSection.timer); }
                var _txt = this;
                jsSection.timer = setTimeout(function () {                    
                    var val = _txt.value;
                    var tbdy = $("#roadmapcode table tbody");
                    if (val != "") {     
                        val = val.toLowerCase();
                        tbdy.find("tr").hide();
                        tbdy.find("tr[rname*='" + val + "']").show();
                    }
                    else {
                        tbdy.find("tr").show();
                    }
                }, 400);
            });
        }
        switch (sec) {
            case "Bario":
                sec = "Mulu / Bario";
                break;
            case "Bakong":
                sec = "Beluru / Bakong";
                break;
            case "BATUNIAH":
                sec = "Batu Niah";
                break;
        }
        var tbl = par.find("table");
        var tbody = tbl.find("tbody");
        tbody.empty();
        var data = this.Master[sec.toLowerCase()];
        var _tr = $("<tr/>");
        var _td = $("<td/>");
        for (var i = 0; i < data.length; i++) {
            var tr = _tr.clone();
            tr.attr("rname", (data[i].RdmRdName + " " + data[i].RdmRdCode).toLowerCase());            
            var td1 = _td.clone();            
            td1.text(data[i].RdmRdName);
            var td2 = _td.clone();
            td2.text(data[i].RdmRdCode);
            tr.append(td1).append(td2);
            tbody.append(tr);
        }
    }
}