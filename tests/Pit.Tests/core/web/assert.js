registerNamespace('Pit');
Pit.Assert = (function () {
    function Assert() {
    }

    Assert.prototype.AreEqual = function (fnName, a1, a2) {
        var res = a1 === a2;
        if (res) {
            console.log("Function - " + fnName + " Result - " + res);
        }
        else {
            console.error("Function - " + fnName + " Failed");
        }
    };

    Assert.prototype.AreNotEqual = function (fnName, a1, a2) {
        var res = a1 !== a2;
        if (!res) {
            console.log("Function - " + fnName + " Result - " + res);
        }
        else {
            console.error("Function - " + fnName + " Failed");
        }
    };

    Assert.prototype.IsNull = function (fnName, a1) {
        var res = a1 === null;
        if (res) {
            console.log("Function - " + fnName + " Result - " + res);
        }
        else {
            console.error("Function - " + fnName + " Failed");
        }
    };

    Assert.prototype.IsNotNull = function (fnName, a1) {
        var res = a1 !== null;
        if (res) {
            console.log("Function - " + fnName + " Result - " + res);
        }
        else {
            console.error("Function - " + fnName + " Failed");
        }
    };

    return Assert;
})();
var Assert = new Pit.Assert();