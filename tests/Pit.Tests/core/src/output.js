registerNamespace("Pit.Test.OverloadedCtorsTests");
registerNamespace("Pit.Test");
Pit.Test.OverloadedCtorsTests.t = (function() {
    function t() {
        this.x = 0;
        this.msg = '';
    }
    t.prototype.get_x = function() {
        return this.x;
    };
    t.prototype.get_msg = function() {
        return this.msg;
    };
    return t;
})();
Pit.Test.OverloadedCtorsTests.t.__init__ = function(idx, lambda) {
    if (typeof this['ctors'] == 'undefined') {
        this['ctors'] = [];
    }
    var ctors = this['ctors'];
    ctors[idx] = lambda;
}
Pit.Test.OverloadedCtorsTests.t.__init__(0, function(x) {
    var t = new Pit.Test.OverloadedCtorsTests.t();
    t.x = x;
    t.msg = '';
    return t;
});
Pit.Test.OverloadedCtorsTests.t.__init__(1, function(x, msg) {
    var t = new Pit.Test.OverloadedCtorsTests.t();
    t.x = x;
    t.msg = msg;
    return t;
});
