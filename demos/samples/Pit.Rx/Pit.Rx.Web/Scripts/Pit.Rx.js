registerNamespace("Pit.Rx.App");
registerNamespace("Pit.Rx.RxSample");
registerNamespace("Pit.Rx.Observable");
DOM.domReady(function () {
    var divEl = document.getElementById("rx");
    return Pit.Rx.RxSample.trackMouse("Time Flies")(divEl);
});
Pit.Rx.RxSample.wireup = function (i) {
    return function (el) {
        return function (mouseMove) {
            return Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(mouseMove)(function (source) {
                return Pit.FSharp.Control.ObservableModule.Map(function (e) {
                    return {
                        Item1: e.clientX,
                        Item2: e.clientY
                    };
                })(source);
            }))(function (w) {
                return Pit.Rx.Observable.delay((i * 100))(w);
            }))(function (source) {
                return Pit.FSharp.Control.ObservableModule.Subscribe(function (tupledArg) {
                    var x = tupledArg.Item1;
                    var y = tupledArg.Item2;
                    el.style.left = (x.ToString() + "px");
                    return el.style.top = (y.ToString() + "px");
                })(source);
            }))(function (value) {
                return Pit.FSharp.Core.Operators.Ignore(value);
            });
        };
    };
};
Pit.Rx.RxSample.trackMouse = function (str) {
    return function (div) {
        div.innerHTML = '';
        var msg = str;
        var mouseMove = Pit.FSharp.Core.Operators.op_PipeRight(div)(function (el) {
            return Pit.Dom.Event.mousemove(el);
        });
        for (var i = 0; i <= (msg.length - 1); i++) {
            (function (thisObject, i) {
                var c = msg.charAt(i);
                var closure = (i + 1);
                var el = document.createElement("span");
                el.innerHTML = c;
                el.style.position = "absolute";
                div.appendChild(el);
                Pit.Rx.RxSample.wireup(i)(el)(mouseMove);
            })(this, i);
        };
    };
};
registerNamespace("Pit.Rx");
Pit.Rx.Observable.BasicObserver1 = (function () {
    function BasicObserver1(next, error, completed) {
        this.IObserver1 = __createIObserver1(this);
        this.next = next;
        this.error = error;
        this.completed = completed;
        this.stopped = false;
    }
    function __createIObserver1(thisObject) {
        var iobserver1 = (function () {
            function IObserver1(thisObject) {
                this.x = thisObject;
            }
            IObserver1.prototype.OnNext = function (args) {
                return this.x.next(args);
            };
            IObserver1.prototype.OnError = function (e) {
                return this.x.error(e);
            };
            IObserver1.prototype.OnCompleted = function () {
                return this.x.completed();
            };
            return IObserver1;
        })();
        return new iobserver1(thisObject);
    }
    BasicObserver1.prototype.__getIObserver1 = function () {
        return this.IObserver1;
    };
    BasicObserver1.prototype.IObserver1_OnNext = function (args) {
        return this.IObserver1.OnNext(args);
    };
    BasicObserver1.prototype.IObserver1_OnError = function (e) {
        return this.IObserver1.OnError(e);
    };
    BasicObserver1.prototype.IObserver1_OnCompleted = function () {
        return this.IObserver1.OnCompleted();
    };
    return BasicObserver1;
})();
Pit.Rx.Observable.BasicObservable1 = (function () {
    function BasicObservable1(f) {
        this.IObservable1 = __createIObservable1(this);
        this.f = f;
    }
    function __createIObservable1(thisObject) {
        var iobservable1 = (function () {
            function IObservable1(thisObject) {
                this.x = thisObject;
            }
            IObservable1.prototype.Subscribe = function (observer) {
                return this.x.f(observer);
            };
            return IObservable1;
        })();
        return new iobservable1(thisObject);
    }
    BasicObservable1.prototype.__getIObservable1 = function () {
        return this.IObservable1;
    };
    BasicObservable1.prototype.IObservable1_Subscribe = function (observer) {
        return this.IObservable1.Subscribe(observer);
    };
    return BasicObservable1;
})();
Pit.Rx.Observable.mkObservable = function (f) {
    return new Pit.Rx.Observable.BasicObservable1(f);
};
Pit.Rx.Observable.mkObserver = function (n) {
    return function (e) {
        return function (c) {
            return new Pit.Rx.Observable.BasicObserver1(n, e, c);
        };
    };
};
Pit.Rx.Observable.invoke = function (f) {
    return function (w) {
        return Pit.Rx.Observable.mkObservable(function (observer) {
            var obs = Pit.Rx.Observable.mkObserver(function (v) {
                return f(function () {
                    return observer.IObserver1_OnNext(v);
                });
            })(function (e) {
                return f(function () {
                    return observer.IObserver1_OnError(e);
                });
            })(function () {
                return f(function () {
                    return observer.IObserver1_OnCompleted();
                });
            });
            return w.IObservable1_Subscribe(obs);
        });
    };
};
Pit.Rx.Observable.delay = function (milliseconds) {
    return function (w) {
        var f = function (g) {
            return Pit.FSharp.Core.Operators.op_PipeRight(window.setTimeout(function () {
                return g();
            },
            milliseconds))(function (value) {
                return Pit.FSharp.Core.Operators.Ignore(value);
            });
        };
        return Pit.Rx.Observable.invoke(f)(w);
    };
};