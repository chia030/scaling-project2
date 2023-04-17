// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let ViewModel = function () {

    let me = this;

    me.searchTerms = ko.observable();
    me.hits = ko.observable();
    me.results = ko.observableArray();
    me.timeUsed = ko.observable();
    me.url = ko.observable();
    // me.documents = ko.observable();

    me.search = function () {
        console.log("calling search function")
        $.ajax({
            // url: "http://localhost:5191/SearchQuery?query=" + me.searchTerms() + "&maxAmount=10", //could create and input field for maxAmount
            // url: "http://localhost:9000/Search?terms=" + me.searchTerms() + "&numberOfResults=10",
            url: "http://localhost:9000/LB/SearchQuery?query=" + me.searchTerms() + "&maxAmount=10",
            success: function (data) {
                //var dataParse = JSON.parse(data);
                var dataParse = $.parseJSON(data);
                console.log(dataParse);
                //   me.hits(data.Hits);
                me.timeUsed(dataParse.ElapsedMilliseconds);
                console.log(dataParse.ElapsedMilliseconds);
                 console.log(dataParse.Documents[0].Path);
                 me.results.removeAll();
                 ko.utils.arrayPushAll(me.results, dataParse.Documents);
                console.log(me.results);
                //   me.results.removeAll();
                //   data.documents.forEach(function (path) {
                //      me.url.push(path);
                //  });
            }
        });
    }

};

ko.applyBindings(new ViewModel());
