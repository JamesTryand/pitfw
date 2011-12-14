registerNamespace("Pit.Test.JsArrayTest");
registerNamespace("Pit.Test.SetTests");
registerNamespace("Pit.Test.ObservableTests");
registerNamespace("Pit.Test.Event2Tests");
registerNamespace("Pit.Test.EventsTest");
registerNamespace("Pit.Test.ListTest");
registerNamespace("Pit.Test.SeqTest");
registerNamespace("Pit.Test.ArrayTest");
registerNamespace("Pit.Test.Array2DTest");
Pit.Test.SetTests.Create = function() {
    var s = Pit.FSharp.Collections.SetModule.Empty().Add(1).Add(2);
    var c = Pit.FSharp.Collections.SetModule.Count(s);
    return Assert.AreEqual("Set Create", 2, c);
};
Pit.Test.SetTests.Add = function() {
    var s = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.SetModule.Empty())(function(set) {
        return Pit.FSharp.Collections.SetModule.Add(1)(set);
    }))(function(set) {
        return Pit.FSharp.Collections.SetModule.Add(2)(set);
    });
    var c = Pit.FSharp.Collections.SetModule.Count(s);
    return Assert.AreEqual("Set Add", 2, c);
};
Pit.Test.SetTests.AddOp = function() {
    var s1 = Pit.FSharp.Collections.SetModule.OfArray([1, 2, 3]);
    var s2 = Pit.FSharp.Collections.SetModule.OfArray([4, 5]);
    var add = Pit.FSharp.Collections.FSharpSet1.op_Addition(s1, s2);
    var c = Pit.FSharp.Collections.SetModule.Count(add);
    return Assert.AreEqual("Set AddOp", 5, c);
};
Pit.Test.SetTests.Contains = function() {
    var s = Pit.FSharp.Collections.SetModule.Empty().Add(1).Add(2);
    var f = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(set) {
        return Pit.FSharp.Collections.SetModule.Contains(2)(set);
    });
    return Assert.AreEqual("Set Contains", true, f);
};
Pit.Test.SetTests.Exists = function() {
    var s = Pit.FSharp.Collections.SetModule.OfArray([1, 2, 3, 4, 5]);
    var f = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(set) {
        return Pit.FSharp.Collections.SetModule.Contains(3)(set);
    });
    return Assert.AreEqual("Set Exists", true, f);
};
Pit.Test.SetTests.Filter = function() {
    var s = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.SetModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10)))))(function(set) {
        return Pit.FSharp.Collections.SetModule.Filter(function(e) {
            return (e % 2) == 0;
        })(set);
    });
    var c = Pit.FSharp.Collections.SetModule.Count(s);
    return Assert.AreEqual("Set Filter", 5, c);
};
Pit.Test.SetTests.Difference = function() {
    var s1 = Pit.FSharp.Collections.SetModule.OfArray([1, 2, 3]);
    var s2 = Pit.FSharp.Collections.SetModule.OfArray([3, 4, 5]);
    var diff = Pit.FSharp.Collections.SetModule.Difference(s1)(s2);
    var diffc = Pit.FSharp.Collections.SetModule.Count(diff);
    return Assert.AreEqual("Set Difference", 2, diffc);
};
Pit.Test.SetTests.DifferenceOp = function() {
    var s1 = Pit.FSharp.Collections.SetModule.OfArray([1, 2, 3]);
    var s2 = Pit.FSharp.Collections.SetModule.OfArray([3, 4, 5]);
    var diff = Pit.FSharp.Collections.FSharpSet1.op_Subtraction(s1, s2);
    var diffc = Pit.FSharp.Collections.SetModule.Count(diff);
    return Assert.AreEqual("Set Difference", 2, diffc);
};
Pit.Test.SetTests.Fold = function() {
    var sumSet = function(set) {
            return Pit.FSharp.Collections.SetModule.Fold(function(acc) {
                return function(elem) {
                    return (acc + elem);
                };
            })(0)(set);
        };
    var s = Pit.FSharp.Collections.SetModule.OfArray([1, 2, 3]);
    var res = sumSet(s);
    return Assert.AreEqual("Set Fold", 6, res);
};
Pit.Test.SetTests.FoldBack = function() {
    var subSetBack = function(set) {
            return Pit.FSharp.Collections.SetModule.FoldBack(function(elem) {
                return function(acc) {
                    return (elem - acc);
                };
            })(set)(0);
        };
    var s = Pit.FSharp.Collections.SetModule.OfArray([1, 2, 3]);
    var res = subSetBack(s);
    return Assert.AreEqual("Set Foldback", 2, res);
};
Pit.Test.SetTests.ForAll = function() {
    var predicate = function(el) {
            return el >= 0;
        };
    var allPositive = function(set) {
            return Pit.FSharp.Collections.SetModule.ForAll(predicate)(set);
        };
    var s = Pit.FSharp.Collections.SetModule.OfArray([1, 2, 3]);
    var f = allPositive(s);
    return Assert.AreEqual("Set ForAll", true, f);
};
Pit.Test.SetTests.Intersect = function() {
    var set1 = Pit.FSharp.Collections.SetModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(3))));
    var set2 = Pit.FSharp.Collections.SetModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(2)(6))));
    var s = Pit.FSharp.Collections.SetModule.Intersect(set1)(set2);
    var c = Pit.FSharp.Collections.SetModule.Count(s);
    return Assert.AreEqual("Set Intersect", 2, c);
};
Pit.Test.SetTests.IntersectMany = function() {
    var sets = [Pit.FSharp.Collections.SetModule.OfArray(Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(9)))), Pit.FSharp.Collections.SetModule.OfArray(Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(5)(8))))];
    var setres = Pit.FSharp.Collections.SetModule.IntersectMany(sets);
    var c = Pit.FSharp.Collections.SetModule.Count(setres);
    return Assert.AreEqual("Set Intersect Many", 4, c);
};
Pit.Test.SetTests.IsEmpty = function() {
    var f = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.SetModule.Empty())(function(set) {
        return Pit.FSharp.Collections.SetModule.IsEmpty(set);
    });
    return Assert.AreEqual("Set IsEmpty", true, f);
};
Pit.Test.SetTests.IsProperSubset = function() {
    var s1 = Pit.FSharp.Collections.SetModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(6))));
    var s2 = Pit.FSharp.Collections.SetModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(4))));
    var f = Pit.FSharp.Collections.SetModule.IsProperSubset(s2)(s1);
    var f2 = Pit.FSharp.Collections.SetModule.IsSubset(s2)(s1);
    Assert.AreEqual("Set IsProperSubset", true, f);
    return Assert.AreEqual("Set IsSubset", true, f2);
};
Pit.Test.SetTests.IsProperSuperset = function() {
    var s1 = Pit.FSharp.Collections.SetModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(4))));
    var s2 = Pit.FSharp.Collections.SetModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(6))));
    var f = Pit.FSharp.Collections.SetModule.IsProperSuperset(s2)(s1);
    var f2 = Pit.FSharp.Collections.SetModule.IsSuperset(s2)(s1);
    Assert.AreEqual("Set IsProperSuperset", true, f);
    return Assert.AreEqual("Set IsSuperset", true, f2);
};
Pit.Test.SetTests.Iterate = function() {
    var s = Pit.FSharp.Collections.SetModule.Empty().Add(1).Add(2);
    var i = 1;
    return Pit.FSharp.Core.Operators.op_PipeRight(s)(function(set) {
        return Pit.FSharp.Collections.SetModule.Iterate(function(e) {
            Assert.AreEqual("Set Iterate", e, i);
            return i = 2;
        })(set);
    });
};
Pit.Test.SetTests.Map = function() {
    var s = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.SetModule.Empty().Add(1).Add(2))(function(set) {
        return Pit.FSharp.Collections.SetModule.Map(function(e) {
            return (e + 2);
        })(set);
    });
    var i = 3;
    return Pit.FSharp.Core.Operators.op_PipeRight(s)(function(set) {
        return Pit.FSharp.Collections.SetModule.Iterate(function(e) {
            Assert.AreEqual("Set Iterate", e, i);
            return i = 4;
        })(set);
    });
};
Pit.Test.SetTests.MaxElement = function() {
    var s = Pit.FSharp.Collections.SetModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10))));
    var m = Pit.FSharp.Collections.SetModule.MaxElement(s);
    return Assert.AreEqual("Set MaxElement", 10, m);
};
Pit.Test.SetTests.MinElement = function() {
    var s = Pit.FSharp.Collections.SetModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(5)(10))));
    var m = Pit.FSharp.Collections.SetModule.MinElement(s);
    return Assert.AreEqual("Set MinElement", 5, m);
};
Pit.Test.SetTests.OfArray = function() {
    var s = Pit.FSharp.Collections.SetModule.OfArray([1, 2, 3]);
    var c = Pit.FSharp.Collections.SetModule.Count(s);
    return Assert.AreEqual("Set OfArray", 3, c);
};
Pit.Test.SetTests.OfList = function() {
    var s = Pit.FSharp.Collections.SetModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(3))));
    var c = Pit.FSharp.Collections.SetModule.Count(s);
    return Assert.AreEqual("Set OfList", 3, c);
};
Pit.Test.SetTests.OfSeq = function() {
    var s = Pit.FSharp.Collections.SetModule.OfSeq(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(3)));
    var c = Pit.FSharp.Collections.SetModule.Count(s);
    return Assert.AreEqual("Set OfSeq", 3, c);
};
Pit.Test.SetTests.Partition = function() {
    var s = Pit.FSharp.Collections.SetModule.OfArray([-2, -1, 1, 2]);
    var patternInput = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(set) {
        return Pit.FSharp.Collections.SetModule.Partition(function(t) {
            return t < 0;
        })(set);
    });
    var p = patternInput.Item2;
    var n = patternInput.Item1;
    var nc = Pit.FSharp.Collections.SetModule.Count(n);
    return Assert.AreEqual("Set Partition", 2, nc);
};
Pit.Test.SetTests.Remove = function() {
    var s = Pit.FSharp.Collections.SetModule.Empty().Add(1).Add(2);
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(set) {
        return Pit.FSharp.Collections.SetModule.Remove(2)(set);
    });
    var c = Pit.FSharp.Core.Operators.op_PipeRight(r)(function(set) {
        return Pit.FSharp.Collections.SetModule.Count(set);
    });
    return Assert.AreEqual("Set Remove", 1, c);
};
Pit.Test.SetTests.Singleton = function() {
    var s = Pit.FSharp.Collections.SetModule.Singleton(1);
    var c = Pit.FSharp.Collections.SetModule.Count(s);
    return Assert.AreEqual("Set Singleton", 1, c);
};
Pit.Test.SetTests.Union = function() {
    var s1 = Pit.FSharp.Collections.SetModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_RangeStep(2)(2)(8))));
    var s2 = Pit.FSharp.Collections.SetModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_RangeStep(1)(2)(9))));
    var s3 = Pit.FSharp.Collections.SetModule.Union(s1)(s2);
    var c = Pit.FSharp.Collections.SetModule.Count(s3);
    return Assert.AreEqual("Set Union", 9, c);
};
Pit.Test.SetTests.UnionMany = function() {
    var sets = [Pit.FSharp.Collections.SetModule.OfArray(Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(9)))), Pit.FSharp.Collections.SetModule.OfArray(Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(5)(8))))];
    var setres = Pit.FSharp.Collections.SetModule.UnionMany(sets);
    var c = Pit.FSharp.Collections.SetModule.Count(setres);
    return Assert.AreEqual("Set Union Many", 9, c);
};
registerNamespace("Pit.Test");
Pit.Test.ObservableTests.EventTest = (function() {
    function EventTest() {
        this.evt = new Pit.FSharp.Control.FSharpEvent1();
        this.evt2 = new Pit.FSharp.Control.FSharpEvent1();
        this.i = 0;
        this.i2 = 0;
    }
    EventTest.prototype.FakeCall = function() {
        this.evt.Trigger(this.i);
        return this.i = (this.i + 1);
    };
    EventTest.prototype.FakeCall2 = function() {
        this.evt2.Trigger(this.i2);
        return this.i2 = (this.i2 + 1);
    };
    EventTest.prototype.get_Evt = function() {
        return this.evt.get_Publish();
    };
    EventTest.prototype.get_Evt2 = function() {
        return this.evt2.get_Publish();
    };
    return EventTest;
})();
Pit.Test.ObservableTests.Add = function() {
    var e = new Pit.Test.ObservableTests.EventTest();
    var n = 0;
    Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(source) {
        return Pit.FSharp.Control.ObservableModule.Add(function(i) {
            Assert.AreEqual("Observable Add Test", n, i);
            return n = (n + 1);
        })(source);
    });
    e.FakeCall();
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.ObservableTests.Map = function() {
    var e = new Pit.Test.ObservableTests.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(source) {
        return Pit.FSharp.Control.ObservableModule.Map(function(i) {
            return {
                Item1: i,
                Item2: (i + 1)
            };
        })(source);
    }))(function(source) {
        return Pit.FSharp.Control.ObservableModule.Add(function(tupledArg) {
            var k = tupledArg.Item1;
            var l = tupledArg.Item2;
            Assert.AreEqual("Observable Map Test", k, 0);
            return Assert.AreEqual("Observable Map Test", l, 1);
        })(source);
    });
    return e.FakeCall();
};
Pit.Test.ObservableTests.Choose = function() {
    var e = new Pit.Test.ObservableTests.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(source) {
        return Pit.FSharp.Control.ObservableModule.Choose(function(i) {
            return (function(thisObject) {
                if (i == 1) {
                    return new Pit.FSharp.Core.FSharpOption1.Some({
                        Item1: i,
                        Item2: (i + 1)
                    });
                } else {
                    return new Pit.FSharp.Core.FSharpOption1.None();
                }
            })(this);
        })(source);
    }))(function(source) {
        return Pit.FSharp.Control.ObservableModule.Add(function(tupledArg) {
            var k = tupledArg.Item1;
            var l = tupledArg.Item2;
            Assert.AreEqual("Observable Choose Test", k, 1);
            return Assert.AreEqual("Observable Choose Test", l, 2);
        })(source);
    });
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.ObservableTests.Filter = function() {
    var e = new Pit.Test.ObservableTests.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(source) {
        return Pit.FSharp.Control.ObservableModule.Filter(function(i) {
            return (function(thisObject) {
                if (i == 1) {
                    return true;
                } else {
                    return false;
                }
            })(this);
        })(source);
    }))(function(source) {
        return Pit.FSharp.Control.ObservableModule.Add(function(k) {
            return Assert.AreEqual("Observable Filter Test", k, 1);
        })(source);
    });
    e.FakeCall();
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.ObservableTests.Merge = function() {
    var e = new Pit.Test.ObservableTests.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(source2) {
        return Pit.FSharp.Control.ObservableModule.Merge(e.get_Evt2())(source2);
    }))(function(source) {
        return Pit.FSharp.Control.ObservableModule.Add(function(k) {
            return Assert.AreEqual("Event Merge test", k, 0);
        })(source);
    });
    e.FakeCall();
    return e.FakeCall2();
};
Pit.Test.ObservableTests.PairWise = function() {
    var e = new Pit.Test.ObservableTests.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(source) {
        return Pit.FSharp.Control.ObservableModule.Pairwise(source);
    }))(function(source) {
        return Pit.FSharp.Control.ObservableModule.Add(function(tupledArg) {
            var k = tupledArg.Item1;
            var l = tupledArg.Item2;
            Assert.AreEqual("Observable Pairwise Test", k, 0);
            return Assert.AreEqual("Observable Pairwise Test", l, 1);
        })(source);
    });
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.ObservableTests.Partition = function() {
    var e = new Pit.Test.ObservableTests.EventTest();
    var patternInput = Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(source) {
        return Pit.FSharp.Control.ObservableModule.Partition(function(l) {
            return l == 1;
        })(source);
    });
    var e2 = patternInput.Item2;
    var e1 = patternInput.Item1;
    Pit.FSharp.Core.Operators.op_PipeRight(e1)(function(source) {
        return Pit.FSharp.Control.ObservableModule.Add(function(l) {
            return Assert.AreEqual("Observable partition test", l, 1);
        })(source);
    });
    Pit.FSharp.Core.Operators.op_PipeRight(e2)(function(source) {
        return Pit.FSharp.Control.ObservableModule.Add(function(k) {
            return Assert.AreEqual("Observable parition test", k, 0);
        })(source);
    });
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.ObservableTests.Scan = function() {
    var initialState = 0;
    var i = 1;
    var e = new Pit.Test.ObservableTests.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(source) {
        return Pit.FSharp.Control.ObservableModule.Scan(function(state) {
            return function(_arg1) {
                return (state + 1);
            };
        })(initialState)(source);
    }))(function(source) {
        return Pit.FSharp.Control.ObservableModule.Add(function(l) {
            Assert.AreEqual("Event Scan Test", i, l);
            return i = (i + 1);
        })(source);
    });
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.ObservableTests.OddEven = function(i) {
    return (function(thisObject) {
        if ((i % 2) == 0) {
            return new Pit.FSharp.Core.FSharpChoice2.Choice1Of2(i);
        } else {
            return new Pit.FSharp.Core.FSharpChoice2.Choice2Of2(i);
        }
    })(this);
};
Pit.Test.ObservableTests.Split = function() {
    var initialState = 0;
    var e = new Pit.Test.ObservableTests.EventTest();
    var patternInput = Pit.FSharp.Control.ObservableModule.Split(function(i) {
        return Pit.Test.ObservableTests.OddEven(i);
    })(e.get_Evt());
    var OddResult = patternInput.Item1;
    var EvenResult = patternInput.Item2;
    Pit.FSharp.Core.Operators.op_PipeRight(OddResult)(function(source) {
        return Pit.FSharp.Control.ObservableModule.Add(function(k) {
            return Assert.AreEqual("Event Split test", k, 0);
        })(source);
    });
    Pit.FSharp.Core.Operators.op_PipeRight(EvenResult)(function(source) {
        return Pit.FSharp.Control.ObservableModule.Add(function(k) {
            return Assert.AreEqual("Event Split test", k, 1);
        })(source);
    });
    e.FakeCall();
    return e.FakeCall();
};
registerNamespace("Pit.Test");
Pit.Test.Event2Tests.Args = (function() {
    function Args(x) {
        this.x = x;
    }
    Args.prototype.get_XValue = function() {
        return this.x;
    };
    return Args;
})();
Pit.Test.Event2Tests.Event2Test = (function() {
    function Event2Test() {
        this.ev = new Pit.FSharp.Control.FSharpEvent2();
        this.ev2 = new Pit.FSharp.Control.FSharpEvent2();
        this.i = 0;
        this.i2 = 0;
    }
    Event2Test.prototype.FakeCall = function() {
        this.ev.Trigger(this, new Pit.Test.Event2Tests.Args(this.i));
        return this.i = (this.i + 1);
    };
    Event2Test.prototype.FakeCall2 = function() {
        this.ev2.Trigger(this, new Pit.Test.Event2Tests.Args(this.i2));
        return this.i2 = (this.i2 + 1);
    };
    Event2Test.prototype.get_Evt = function() {
        return this.ev.get_Publish();
    };
    Event2Test.prototype.get_Evt2 = function() {
        return this.ev2.get_Publish();
    };
    return Event2Test;
})();
Pit.Test.Event2Tests.Add = function() {
    var e = new Pit.Test.Event2Tests.Event2Test();
    Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(arg) {
            return Assert.AreEqual("Event2 add test", arg.get_XValue(), 0);
        })(sourceEvent);
    });
    return e.FakeCall();
};
Pit.Test.Event2Tests.Map = function() {
    var e = new Pit.Test.Event2Tests.Event2Test();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Map(function(a) {
            return {
                Item1: a.get_XValue(),
                Item2: (a.get_XValue() + 1)
            };
        })(sourceEvent);
    }))(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(tupledArg) {
            var k = tupledArg.Item1;
            var l = tupledArg.Item2;
            Assert.AreEqual("Event2 Map test", k, 0);
            return Assert.AreEqual("Event2 Map test", l, 1);
        })(sourceEvent);
    });
    return e.FakeCall();
};
Pit.Test.Event2Tests.Choose = function() {
    var e = new Pit.Test.Event2Tests.Event2Test();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Choose(function(arg) {
            return (function(thisObject) {
                if (arg.get_XValue() == 1) {
                    return new Pit.FSharp.Core.FSharpOption1.Some({
                        Item1: arg.get_XValue(),
                        Item2: (arg.get_XValue() + 1)
                    });
                } else {
                    return new Pit.FSharp.Core.FSharpOption1.None();
                }
            })(this);
        })(sourceEvent);
    }))(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(tupledArg) {
            var k = tupledArg.Item1;
            var l = tupledArg.Item2;
            Assert.AreEqual("Event2 Choose test", k, 1);
            return Assert.AreEqual("Event2 Choose test", l, 2);
        })(sourceEvent);
    });
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.Event2Tests.Filter = function() {
    var e = new Pit.Test.Event2Tests.Event2Test();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Filter(function(arg) {
            return (function(thisObject) {
                if (arg.get_XValue() == 1) {
                    return true;
                } else {
                    return false;
                }
            })(this);
        })(sourceEvent);
    }))(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(k) {
            return Assert.AreEqual("Event2 filter test", k.get_XValue(), 1);
        })(sourceEvent);
    });
    e.FakeCall();
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.Event2Tests.Merge = function() {
    var e = new Pit.Test.Event2Tests.Event2Test();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(event2) {
        return Pit.FSharp.Control.EventModule.Merge(e.get_Evt2())(event2);
    }))(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(arg) {
            return Assert.AreEqual("Event2 Merge test", arg.get_XValue(), 0);
        })(sourceEvent);
    });
    e.FakeCall();
    return e.FakeCall2();
};
Pit.Test.Event2Tests.PairWise = function() {
    var e = new Pit.Test.Event2Tests.Event2Test();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Pairwise(sourceEvent);
    }))(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(tupledArg) {
            var k = tupledArg.Item1;
            var l = tupledArg.Item2;
            Assert.AreEqual("Event2 Pairwise test", k.get_XValue(), 0);
            return Assert.AreEqual("Event2 Pairwise test", l.get_XValue(), 1);
        })(sourceEvent);
    });
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.Event2Tests.Partition = function() {
    var e = new Pit.Test.Event2Tests.Event2Test();
    var patternInput = Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Partition(function(l) {
            return l.get_XValue() == 1;
        })(sourceEvent);
    });
    var e2 = patternInput.Item2;
    var e1 = patternInput.Item1;
    Pit.FSharp.Core.Operators.op_PipeRight(e1)(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(l) {
            return Assert.AreEqual("Event2 Partition test", l.get_XValue(), 1);
        })(sourceEvent);
    });
    Pit.FSharp.Core.Operators.op_PipeRight(e2)(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(l) {
            return Assert.AreEqual("Event2 Partition test", l.get_XValue(), 0);
        })(sourceEvent);
    });
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.Event2Tests.Scan = function() {
    var initialState = 0;
    var i = 1;
    var e = new Pit.Test.Event2Tests.Event2Test();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Scan(function(state) {
            return function(_arg1) {
                return (state + 1);
            };
        })(initialState)(sourceEvent);
    }))(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(l) {
            Assert.AreEqual("Event2 Scan Test", i, l);
            return i = (i + 1);
        })(sourceEvent);
    });
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.Event2Tests.OddEven = function(i) {
    return (function(thisObject) {
        if ((i.get_XValue() % 2) == 0) {
            return new Pit.FSharp.Core.FSharpChoice2.Choice1Of2(i.get_XValue());
        } else {
            return new Pit.FSharp.Core.FSharpChoice2.Choice2Of2(i.get_XValue());
        }
    })(this);
};
Pit.Test.Event2Tests.Split = function() {
    var initialState = 0;
    var e = new Pit.Test.Event2Tests.Event2Test();
    var patternInput = Pit.FSharp.Control.EventModule.Split(function(i) {
        return Pit.Test.Event2Tests.OddEven(i);
    })(e.get_Evt());
    var OddResult = patternInput.Item1;
    var EvenResult = patternInput.Item2;
    Pit.FSharp.Core.Operators.op_PipeRight(OddResult)(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(k) {
            return Assert.AreEqual("Event2 Split test", k, 0);
        })(sourceEvent);
    });
    Pit.FSharp.Core.Operators.op_PipeRight(EvenResult)(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(k) {
            return Assert.AreEqual("Event2 Split test", k, 1);
        })(sourceEvent);
    });
    e.FakeCall();
    return e.FakeCall();
};
registerNamespace("Pit.Test");
Pit.Test.EventsTest.EventTest = (function() {
    function EventTest() {
        this.evt = new Pit.FSharp.Control.FSharpEvent1();
        this.evt2 = new Pit.FSharp.Control.FSharpEvent1();
        this.i = 0;
        this.i2 = 0;
    }
    EventTest.prototype.FakeCall = function() {
        this.evt.Trigger(this.i);
        return this.i = (this.i + 1);
    };
    EventTest.prototype.FakeCall2 = function() {
        this.evt2.Trigger(this.i2);
        return this.i2 = (this.i2 + 1);
    };
    EventTest.prototype.get_Evt = function() {
        return this.evt.get_Publish();
    };
    EventTest.prototype.get_Evt2 = function() {
        return this.evt2.get_Publish();
    };
    return EventTest;
})();
Pit.Test.EventsTest.Add = function() {
    var e = new Pit.Test.EventsTest.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(i) {
            return Assert.AreEqual("Event Add test", i, 0);
        })(sourceEvent);
    });
    return e.FakeCall();
};
Pit.Test.EventsTest.Map = function() {
    var e = new Pit.Test.EventsTest.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Map(function(i) {
            return {
                Item1: i,
                Item2: (i + 1)
            };
        })(sourceEvent);
    }))(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(tupledArg) {
            var k = tupledArg.Item1;
            var l = tupledArg.Item2;
            Assert.AreEqual("Event Map test", k, 0);
            return Assert.AreEqual("Event Map test", l, 1);
        })(sourceEvent);
    });
    return e.FakeCall();
};
Pit.Test.EventsTest.Choose = function() {
    var e = new Pit.Test.EventsTest.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Choose(function(i) {
            return (function(thisObject) {
                if (i == 1) {
                    return new Pit.FSharp.Core.FSharpOption1.Some({
                        Item1: i,
                        Item2: (i + 1)
                    });
                } else {
                    return new Pit.FSharp.Core.FSharpOption1.None();
                }
            })(this);
        })(sourceEvent);
    }))(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(tupledArg) {
            var k = tupledArg.Item1;
            var l = tupledArg.Item2;
            Assert.AreEqual("Event Choose test", k, 1);
            return Assert.AreEqual("Event Choose test", l, 2);
        })(sourceEvent);
    });
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.EventsTest.Filter = function() {
    var e = new Pit.Test.EventsTest.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Filter(function(i) {
            return (function(thisObject) {
                if (i == 1) {
                    return true;
                } else {
                    return false;
                }
            })(this);
        })(sourceEvent);
    }))(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(k) {
            return Assert.AreEqual("Event Filter test", k, 1);
        })(sourceEvent);
    });
    e.FakeCall();
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.EventsTest.Merge = function() {
    var e = new Pit.Test.EventsTest.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(event2) {
        return Pit.FSharp.Control.EventModule.Merge(e.get_Evt2())(event2);
    }))(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(k) {
            return Assert.AreEqual("Event Merge test", k, 0);
        })(sourceEvent);
    });
    e.FakeCall();
    return e.FakeCall2();
};
Pit.Test.EventsTest.PairWise = function() {
    var e = new Pit.Test.EventsTest.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Pairwise(sourceEvent);
    }))(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(tupledArg) {
            var k = tupledArg.Item1;
            var l = tupledArg.Item2;
            Assert.AreEqual("Event PairWise test", k, 0);
            return Assert.AreEqual("Event PairWise test", l, 1);
        })(sourceEvent);
    });
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.EventsTest.Partition = function() {
    var e = new Pit.Test.EventsTest.EventTest();
    var patternInput = Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Partition(function(l) {
            return l == 1;
        })(sourceEvent);
    });
    var e2 = patternInput.Item2;
    var e1 = patternInput.Item1;
    Pit.FSharp.Core.Operators.op_PipeRight(e1)(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(l) {
            return Assert.AreEqual("Event Partition test", l, 1);
        })(sourceEvent);
    });
    Pit.FSharp.Core.Operators.op_PipeRight(e2)(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(l) {
            return Assert.AreEqual("Event Partition test", l, 0);
        })(sourceEvent);
    });
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.EventsTest.Scan = function() {
    var initialState = 0;
    var i = 1;
    var e = new Pit.Test.EventsTest.EventTest();
    Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(e.get_Evt())(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Scan(function(state) {
            return function(_arg1) {
                return (state + 1);
            };
        })(initialState)(sourceEvent);
    }))(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(l) {
            Assert.AreEqual("Event Scan Test", i, l);
            return i = (i + 1);
        })(sourceEvent);
    });
    e.FakeCall();
    return e.FakeCall();
};
Pit.Test.EventsTest.OddEven = function(i) {
    return (function(thisObject) {
        if ((i % 2) == 0) {
            return new Pit.FSharp.Core.FSharpChoice2.Choice1Of2(i);
        } else {
            return new Pit.FSharp.Core.FSharpChoice2.Choice2Of2(i);
        }
    })(this);
};
Pit.Test.EventsTest.Split = function() {
    var initialState = 0;
    var e = new Pit.Test.EventsTest.EventTest();
    var patternInput = Pit.FSharp.Control.EventModule.Split(function(i) {
        return Pit.Test.EventsTest.OddEven(i);
    })(e.get_Evt());
    var OddResult = patternInput.Item1;
    var EvenResult = patternInput.Item2;
    Pit.FSharp.Core.Operators.op_PipeRight(OddResult)(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(k) {
            return Assert.AreEqual("Event Split test", k, 0);
        })(sourceEvent);
    });
    Pit.FSharp.Core.Operators.op_PipeRight(EvenResult)(function(sourceEvent) {
        return Pit.FSharp.Control.EventModule.Add(function(k) {
            return Assert.AreEqual("Event Split test", k, 1);
        })(sourceEvent);
    });
    e.FakeCall();
    return e.FakeCall();
};
registerNamespace("Pit.Test.ListTest.Transaction");
Pit.Test.ListTest.Transaction = function() {
    this.Tag = 0;
    this.IsWithdrawal = false;
    this.IsDeposit = false;
};
Pit.Test.ListTest.Transaction.Deposit = function() {};
Pit.Test.ListTest.Transaction.Deposit.prototype = new Pit.Test.ListTest.Transaction();
Pit.Test.ListTest.Transaction.Deposit.prototype.equality = function(compareTo) {
    var result = true;
    return result;
};
Pit.Test.ListTest.Transaction.Withdrawal = function() {};
Pit.Test.ListTest.Transaction.Withdrawal.prototype = new Pit.Test.ListTest.Transaction();
Pit.Test.ListTest.Transaction.Withdrawal.prototype.equality = function(compareTo) {
    var result = true;
    return result;
};
Pit.Test.ListTest.Transaction.prototype.get_Tag = function() {
    return this.Tag;
};
Pit.Test.ListTest.Transaction.prototype.get_IsWithdrawal = function() {
    return this instanceof Pit.Test.ListTest.Transaction.Withdrawal;
};
Pit.Test.ListTest.Transaction.prototype.get_IsDeposit = function() {
    return this instanceof Pit.Test.ListTest.Transaction.Deposit;
};
Pit.Test.ListTest.Declare1 = function() {
    var list123 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    return Assert.AreEqual("Declare List type 1:", list123.get_Head(), 1);
};
Pit.Test.ListTest.Declare2 = function() {
    var list123 = Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10)));
    return Assert.AreEqual("Declare List type 2:", list123.get_Head(), 1);
};
Pit.Test.ListTest.Declare3 = function() {
    var list123 = Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
        return Pit.FSharp.Collections.SeqModule.Map(function(i) {
            return (i * i);
        })(Pit.FSharp.Core.Operators.op_Range(1)(10));
    })));
    return Assert.AreEqual("Declare List type 3:", list123.get_Head(), 1);
};
Pit.Test.ListTest.AttachElements = function() {
    var list123 = Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
        return Pit.FSharp.Collections.SeqModule.Map(function(i) {
            return (i * i);
        })(Pit.FSharp.Core.Operators.op_Range(1)(10));
    })));
    var list2 = new Pit.FSharp.Collections.FSharpList1.Cons(100, list123);
    return Assert.AreEqual("Attach elements:", list2.get_Head(), 100);
};
Pit.Test.ListTest.ConcatenateElements = function() {
    var list1 = Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
        return Pit.FSharp.Collections.SeqModule.Map(function(i) {
            return (i * i);
        })(Pit.FSharp.Core.Operators.op_Range(1)(10));
    })));
    var list2 = new Pit.FSharp.Collections.FSharpList1.Cons(100, new Pit.FSharp.Collections.FSharpList1.Empty());
    var list3 = Pit.FSharp.Core.Operators.op_Append(list1)(list2);
    return Assert.AreEqual("Concatenate elements:", list3.get_Head(), 1);
};
Pit.Test.ListTest.Properties = function() {
    var list1 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    Assert.AreEqual("List Empty property:", list1.get_IsEmpty(), false);
    Assert.AreEqual("List Length property:", list1.get_Length(), 3);
    Assert.AreEqual("List Head property:", list1.get_Head(), 1);
    Assert.AreEqual("List Tail property", list1.get_Tail().get_Head(), 2);
    Assert.AreEqual("List.Tail.Tail.Head", list1.get_Tail().get_Tail().get_Head(), 3);
    return Assert.AreEqual("List.Item(1)", list1.get_Item(1), 2);
};
Pit.Test.ListTest.Recursion1 = function() {
    var sum = function(list) {
            var loop = function(list1) {
                    return function(acc) {
                        return (function(thisObject) {
                            if (list1 instanceof Pit.FSharp.Collections.FSharpList1.Empty) {
                                return acc;
                            } else {
                                var tail = list1.get_Tail();
                                var head = list1.get_Head();
                                return loop(tail)((acc + head));
                            }
                        })(this);
                    };
                };
            return loop(list)(0);
        };
    var list = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    return Assert.AreEqual("List Recursion 1", sum(list), 6);
};
Pit.Test.ListTest.Recursion2 = function() {
    var IsPrimeMultipleTest = function(n) {
            return function(x) {
                return (function(thisObject) {
                    if (x == n) {
                        return true;
                    } else {
                        return (x % n) != 0;
                    }
                })(this);
            };
        };
    var RemoveAllMultiples = function(listn) {
            return function(listx) {
                return (function(thisObject) {
                    if (listn instanceof Pit.FSharp.Collections.FSharpList1.Empty) {
                        return listx;
                    } else {
                        var tail = listn.get_Tail();
                        var head = listn.get_Head();
                        return RemoveAllMultiples(tail)(Pit.FSharp.Collections.ListModule.Filter(IsPrimeMultipleTest(head))(listx));
                    }
                })(this);
            };
        };
    var GetPrimesUpTo = function(n) {
            var max = Pit.FSharp.Core.Operators.ToInt(Pit.FSharp.Core.Operators.Sqrt(Pit.FSharp.Core.Operators.ToDouble(n)));
            return RemoveAllMultiples(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(2)(max))))(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(n))));
        };
    var primes = GetPrimesUpTo(100);
    Assert.AreEqual("List Recursion 2 - First element", Pit.FSharp.Collections.ListModule.Get(primes)(1), 2);
    return Assert.AreEqual("List Recursion 2 - 25th element", Pit.FSharp.Collections.ListModule.Get(primes)(25), 97);
};
Pit.Test.ListTest.containsNumber = function(number) {
    return function(list) {
        return Pit.FSharp.Collections.ListModule.Exists(function(elem) {
            return elem == number;
        })(list);
    };
};
Pit.Test.ListTest.BooleanOperation = function() {
    var list0to3 = Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(0)(3)));
    return Assert.AreEqual("Boolean Operation:", Pit.Test.ListTest.containsNumber(0)(list0to3), true);
};
Pit.Test.ListTest.isEqualElement = function(list1) {
    return function(list2) {
        return Pit.FSharp.Collections.ListModule.Exists2(function(elem1) {
            return function(elem2) {
                return elem1 == elem2;
            };
        })(list1)(list2);
    };
};
Pit.Test.ListTest.Exists2 = function() {
    var list1to5 = Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(5)));
    var list5to1 = Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_RangeStep(5)(-1)(1)));
    var result = Pit.Test.ListTest.isEqualElement(list1to5)(list5to1);
    return (function(thisObject) {
        if (result) {
            return Assert.AreEqual("List.exists2 function.", result, true);
        } else {
            return Assert.AreEqual("List.exists2 function.", result, false);
        }
    })(this);
};
Pit.Test.ListTest.ForAll = function() {
    var isAllZeroes = function(list) {
            return Pit.FSharp.Collections.ListModule.ForAll(function(elem) {
                return elem == 0;
            })(list);
        };
    Assert.AreEqual("List.forall function", isAllZeroes(new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Empty()))), true);
    return Assert.AreEqual("List.forall function", isAllZeroes(new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Empty()))), false);
};
Pit.Test.ListTest.listEqual = function(list1) {
    return function(list2) {
        return Pit.FSharp.Collections.ListModule.ForAll2(function(elem1) {
            return function(elem2) {
                return elem1 == elem2;
            };
        })(list1)(list2);
    };
};
Pit.Test.ListTest.ForAll2 = function() {
    Assert.AreEqual("List.forall2 function", Pit.Test.ListTest.listEqual(new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Empty()))))(new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Empty())))), true);
    return Assert.AreEqual("List.forall2 function", Pit.Test.ListTest.listEqual(new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Empty()))))(new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Empty())))), false);
};
Pit.Test.ListTest.Sort = function() {
    var sortedList1 = Pit.FSharp.Collections.ListModule.Sort(new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(4, new Pit.FSharp.Collections.FSharpList1.Cons(8, new Pit.FSharp.Collections.FSharpList1.Cons(-2, new Pit.FSharp.Collections.FSharpList1.Cons(5, new Pit.FSharp.Collections.FSharpList1.Empty()))))));
    return Assert.AreEqual("List.sort function", Pit.FSharp.Collections.ListModule.Get(sortedList1)(1), 1);
};
Pit.Test.ListTest.SortBy = function() {
    var sortedList2 = Pit.FSharp.Collections.ListModule.SortBy(function(elem) {
        return Pit.FSharp.Core.Operators.Abs(elem);
    })(new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(4, new Pit.FSharp.Collections.FSharpList1.Cons(8, new Pit.FSharp.Collections.FSharpList1.Cons(-2, new Pit.FSharp.Collections.FSharpList1.Cons(5, new Pit.FSharp.Collections.FSharpList1.Empty()))))));
    return Assert.AreEqual("List.sortBy function", Pit.FSharp.Collections.ListModule.Get(sortedList2)(1), -2);
};
Pit.Test.ListTest.Find = function() {
    var isDivisibleBy = function(number) {
            return function(elem) {
                return (elem % number) == 0;
            };
        };
    var result = Pit.FSharp.Collections.ListModule.Find(isDivisibleBy(5))(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(100))));
    return Assert.AreEqual("List.find function", result, 5);
};
Pit.Test.ListTest.Pick = function() {
    var valuesList = new Pit.FSharp.Collections.FSharpList1.Cons({
        Item1: "a",
        Item2: 1
    }, new Pit.FSharp.Collections.FSharpList1.Cons({
        Item1: "b",
        Item2: 2
    }, new Pit.FSharp.Collections.FSharpList1.Cons({
        Item1: "c",
        Item2: 3
    }, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var resultPick = Pit.FSharp.Collections.ListModule.Pick(function(elem) {
        return (function(thisObject) {
            if (elem.Item2 == 2) {
                var value = elem.Item1;
                return new Pit.FSharp.Core.FSharpOption1.Some(value);
            } else {
                return new Pit.FSharp.Core.FSharpOption1.None();
            }
        })(this);
    })(valuesList);
    return Assert.AreEqual("List.pick function", resultPick, "b");
};
Pit.Test.ListTest.TryFind = function() {
    var list1d = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Cons(7, new Pit.FSharp.Collections.FSharpList1.Cons(9, new Pit.FSharp.Collections.FSharpList1.Cons(11, new Pit.FSharp.Collections.FSharpList1.Cons(13, new Pit.FSharp.Collections.FSharpList1.Cons(15, new Pit.FSharp.Collections.FSharpList1.Cons(19, new Pit.FSharp.Collections.FSharpList1.Cons(22, new Pit.FSharp.Collections.FSharpList1.Cons(29, new Pit.FSharp.Collections.FSharpList1.Cons(36, new Pit.FSharp.Collections.FSharpList1.Empty())))))))))));
    var isEven = function(x) {
            return (x % 2) == 0;
        };
    var matchValue = Pit.FSharp.Collections.ListModule.TryFind(isEven)(list1d);
    (function(thisObject) {
        if (matchValue instanceof Pit.FSharp.Core.FSharpOption1.None) {
            return null;
        } else {
            var value = matchValue.get_Value();
            return Assert.AreEqual("List.tryFind function", value, 22);
        }
    })(this);
    var matchValue = Pit.FSharp.Collections.ListModule.TryFindIndex(isEven)(list1d);
    return (function(thisObject) {
        if (matchValue instanceof Pit.FSharp.Core.FSharpOption1.None) {
            return null;
        } else {
            var value = matchValue.get_Value();
            return Assert.AreEqual("List.tryFindIndex function", value, 8);
        }
    })(this);
};
Pit.Test.ListTest.ArithemeticOperations = function() {
    var sum1 = Pit.FSharp.Collections.ListModule.Sum(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10))));
    Assert.AreEqual("List.sum function", sum1, 55);
    var sum2 = Pit.FSharp.Collections.ListModule.SumBy(function(elem) {
        return (elem * elem);
    })(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10))));
    Assert.AreEqual("List.sumBy function", sum2, 385);
    var avg1 = Pit.FSharp.Collections.ListModule.Average(new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Empty())))));
    Assert.AreEqual("List.average function", avg1, 1);
    var avg2 = Pit.FSharp.Collections.ListModule.AverageBy(function(elem) {
        return Pit.FSharp.Core.Operators.ToDouble(elem);
    })(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10))));
    return Assert.AreEqual("List.averageBy function", avg2, 5.5);
};
Pit.Test.ListTest.Zip = function() {
    var list1 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var list2 = new Pit.FSharp.Collections.FSharpList1.Cons(-1, new Pit.FSharp.Collections.FSharpList1.Cons(-2, new Pit.FSharp.Collections.FSharpList1.Cons(-3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var listZip = Pit.FSharp.Collections.ListModule.Zip(list1)(list2);
    var f = Pit.FSharp.Core.Operators.Fst(listZip.get_Head());
    Assert.AreEqual("List.zip function", f, 1);
    return Assert.AreEqual("List.zip function", Pit.FSharp.Core.Operators.Snd(listZip.get_Head()), -1);
};
Pit.Test.ListTest.Zip3 = function() {
    var list1 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var list2 = new Pit.FSharp.Collections.FSharpList1.Cons(-1, new Pit.FSharp.Collections.FSharpList1.Cons(-2, new Pit.FSharp.Collections.FSharpList1.Cons(-3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var list3 = new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Cons(0, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var listZip3 = Pit.FSharp.Collections.ListModule.Zip3(list1)(list2)(list3);
    var patternInput = listZip3.get_Head();
    var f3 = patternInput.Item3;
    var f2 = patternInput.Item2;
    var f1 = patternInput.Item1;
    Assert.AreEqual("List.zip function", f1, 1);
    Assert.AreEqual("List.zip function", f2, -1);
    return Assert.AreEqual("List.zip function", f3, 0);
};
Pit.Test.ListTest.UnZip = function() {
    var lists = Pit.FSharp.Collections.ListModule.Unzip(new Pit.FSharp.Collections.FSharpList1.Cons({
        Item1: 1,
        Item2: 2
    }, new Pit.FSharp.Collections.FSharpList1.Cons({
        Item1: 3,
        Item2: 4
    }, new Pit.FSharp.Collections.FSharpList1.Empty())));
    Assert.AreEqual("List.unzip function", Pit.FSharp.Core.Operators.Fst(lists).get_Head(), 1);
    return Assert.AreEqual("List.unzip function", Pit.FSharp.Core.Operators.Snd(lists).get_Head(), 2);
};
Pit.Test.ListTest.UnZip3 = function() {
    var listsUnzip3 = Pit.FSharp.Collections.ListModule.Unzip3(new Pit.FSharp.Collections.FSharpList1.Cons({
        Item1: 1,
        Item2: 2,
        Item3: 3
    }, new Pit.FSharp.Collections.FSharpList1.Cons({
        Item1: 4,
        Item2: 5,
        Item3: 6
    }, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var i3 = listsUnzip3.Item3;
    var i2 = listsUnzip3.Item2;
    var i1 = listsUnzip3.Item1;
    Assert.AreEqual("List.unzip3 function", i1.get_Head(), 1);
    Assert.AreEqual("List.unzip3 function", i2.get_Head(), 2);
    return Assert.AreEqual("List.unzip3 function", i3.get_Head(), 3);
};
Pit.Test.ListTest.Map = function() {
    var list1 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var newList = Pit.FSharp.Collections.ListModule.Map(function(x) {
        return (x + 1);
    })(list1);
    return Assert.AreEqual("List.map function", newList.get_Head(), 2);
};
Pit.Test.ListTest.Map2 = function() {
    var list1 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var list2 = new Pit.FSharp.Collections.FSharpList1.Cons(4, new Pit.FSharp.Collections.FSharpList1.Cons(5, new Pit.FSharp.Collections.FSharpList1.Cons(6, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var sumList = Pit.FSharp.Collections.ListModule.Map2(function(x) {
        return function(y) {
            return (x + y);
        };
    })(list1)(list2);
    return Assert.AreEqual("List.map2 function", sumList.get_Head(), 5);
};
Pit.Test.ListTest.Map3 = function() {
    var list1 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var list2 = new Pit.FSharp.Collections.FSharpList1.Cons(4, new Pit.FSharp.Collections.FSharpList1.Cons(5, new Pit.FSharp.Collections.FSharpList1.Cons(6, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var newList2 = Pit.FSharp.Collections.ListModule.Map3(function(x) {
        return function(y) {
            return function(z) {
                return ((x + y) + z);
            };
        };
    })(list1)(list2)(new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Cons(4, new Pit.FSharp.Collections.FSharpList1.Empty()))));
    return Assert.AreEqual("List.map3 function", newList2.get_Head(), 7);
};
Pit.Test.ListTest.Mapi = function() {
    var list1 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var newListAddIndex = Pit.FSharp.Collections.ListModule.MapIndexed(function(i) {
        return function(x) {
            return (x + i);
        };
    })(list1);
    return Assert.AreEqual("List.mapi function", newListAddIndex.get_Head(), 1);
};
Pit.Test.ListTest.Mapi2 = function() {
    var list1 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var list2 = new Pit.FSharp.Collections.FSharpList1.Cons(4, new Pit.FSharp.Collections.FSharpList1.Cons(5, new Pit.FSharp.Collections.FSharpList1.Cons(6, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var listAddTimesIndex = Pit.FSharp.Collections.ListModule.MapIndexed2(function(i) {
        return function(x) {
            return function(y) {
                return ((x + y) * i);
            };
        };
    })(list1)(list2);
    return Assert.AreEqual("List.mapi2 function", listAddTimesIndex.get_Head(), 0);
};
Pit.Test.ListTest.Collect = function() {
    var list1 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var collectList = Pit.FSharp.Collections.ListModule.Collect(function(x) {
        return Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
            return Pit.FSharp.Collections.SeqModule.Map(function(i) {
                return (x * i);
            })(Pit.FSharp.Core.Operators.op_Range(1)(3));
        })));
    })(list1);
    return Assert.AreEqual("List.collect function", collectList.get_Head(), 1);
};
Pit.Test.ListTest.Filter = function() {
    var evenOnlyList = Pit.FSharp.Collections.ListModule.Filter(function(x) {
        return (x % 2) == 0;
    })(new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Cons(4, new Pit.FSharp.Collections.FSharpList1.Cons(5, new Pit.FSharp.Collections.FSharpList1.Cons(6, new Pit.FSharp.Collections.FSharpList1.Empty())))))));
    return Assert.AreEqual("List.filter function", Pit.FSharp.Core.Operators.op_PipeRight(evenOnlyList)(function(list) {
        return Pit.FSharp.Collections.ListModule.Length(list);
    }), 3);
};
Pit.Test.ListTest.Choose = function() {
    var k = Pit.FSharp.Collections.ListModule.Choose(function(elem) {
        return (function(thisObject) {
            if ((elem % 2) == 0) {
                return new Pit.FSharp.Core.FSharpOption1.Some(Pit.FSharp.Core.Operators.ToDouble(((elem * elem) - 1)));
            } else {
                return new Pit.FSharp.Core.FSharpOption1.None();
            }
        })(this);
    })(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10))));
    return Assert.AreEqual("List.choose function", k.get_Head(), 3);
};
Pit.Test.ListTest.Append = function() {
    var list1to10 = Pit.FSharp.Collections.ListModule.Append(new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty()))))(new Pit.FSharp.Collections.FSharpList1.Cons(4, new Pit.FSharp.Collections.FSharpList1.Cons(5, new Pit.FSharp.Collections.FSharpList1.Cons(6, new Pit.FSharp.Collections.FSharpList1.Cons(7, new Pit.FSharp.Collections.FSharpList1.Cons(8, new Pit.FSharp.Collections.FSharpList1.Cons(9, new Pit.FSharp.Collections.FSharpList1.Cons(10, new Pit.FSharp.Collections.FSharpList1.Empty()))))))));
    return Assert.AreEqual("List.append function", Pit.FSharp.Core.Operators.op_PipeRight(list1to10)(function(list) {
        return Pit.FSharp.Collections.ListModule.Length(list);
    }), 10);
};
Pit.Test.ListTest.Concat = function() {
    var listResult = Pit.FSharp.Collections.ListModule.Concat(new Pit.FSharp.Collections.FSharpList1.Cons(new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty()))), new Pit.FSharp.Collections.FSharpList1.Cons(new Pit.FSharp.Collections.FSharpList1.Cons(4, new Pit.FSharp.Collections.FSharpList1.Cons(5, new Pit.FSharp.Collections.FSharpList1.Cons(6, new Pit.FSharp.Collections.FSharpList1.Empty()))), new Pit.FSharp.Collections.FSharpList1.Cons(new Pit.FSharp.Collections.FSharpList1.Cons(7, new Pit.FSharp.Collections.FSharpList1.Cons(8, new Pit.FSharp.Collections.FSharpList1.Cons(9, new Pit.FSharp.Collections.FSharpList1.Empty()))), new Pit.FSharp.Collections.FSharpList1.Empty()))));
    return Assert.AreEqual("List.concat function", Pit.FSharp.Core.Operators.op_PipeRight(listResult)(function(list) {
        return Pit.FSharp.Collections.ListModule.Length(list);
    }), 9);
};
Pit.Test.ListTest.reverseList = function(list) {
    return Pit.FSharp.Collections.ListModule.Fold(function(acc) {
        return function(elem) {
            return new Pit.FSharp.Collections.FSharpList1.Cons(elem, acc);
        };
    })(new Pit.FSharp.Collections.FSharpList1.Empty())(list);
};
Pit.Test.ListTest.Fold = function() {
    var sumList = function(list) {
            return Pit.FSharp.Collections.ListModule.Fold(function(acc) {
                return function(elem) {
                    return (acc + elem);
                };
            })(0)(list);
        };
    return Assert.AreEqual("List.fold function: SumTest", sumList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(3)))), 6);
};
Pit.Test.ListTest.Fold2 = function() {
    var sumGreatest = function(list1) {
            return function(list2) {
                return Pit.FSharp.Collections.ListModule.Fold2(function(acc) {
                    return function(elem1) {
                        return function(elem2) {
                            return (acc + Pit.FSharp.Core.Operators.Max(elem1)(elem2));
                        };
                    };
                })(0)(list1)(list2);
            };
        };
    var sum = sumGreatest(new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty()))))(new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Empty()))));
    return Assert.AreEqual("List.fold2 function", sum, 8);
};
Pit.Test.ListTest.Fold2_2 = function() {
    var transactionTypes = new Pit.FSharp.Collections.FSharpList1.Cons(new Pit.Test.ListTest.Transaction.Deposit(), new Pit.FSharp.Collections.FSharpList1.Cons(new Pit.Test.ListTest.Transaction.Deposit(), new Pit.FSharp.Collections.FSharpList1.Cons(new Pit.Test.ListTest.Transaction.Withdrawal(), new Pit.FSharp.Collections.FSharpList1.Empty())));
    var transactionAmounts = new Pit.FSharp.Collections.FSharpList1.Cons(100, new Pit.FSharp.Collections.FSharpList1.Cons(1000, new Pit.FSharp.Collections.FSharpList1.Cons(95, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var initialBalance = 200;
    var endingBalance = Pit.FSharp.Collections.ListModule.Fold2(function(acc) {
        return function(elem1) {
            return function(elem2) {
                return (function(thisObject) {
                    if (elem1 instanceof Pit.Test.ListTest.Transaction.Withdrawal) {
                        return (acc - elem2);
                    } else {
                        return (acc + elem2);
                    }
                })(this);
            };
        };
    })(initialBalance)(transactionTypes)(transactionAmounts);
    return Assert.AreEqual("List.fold2 function", Pit.FSharp.Core.Operators.op_PipeRight(endingBalance)(function(value) {
        return Pit.FSharp.Core.Operators.ToInt(value);
    }), 1205);
};
Pit.Test.ListTest.FoldBack = function() {
    var sumListBack = function(list) {
            return Pit.FSharp.Collections.ListModule.FoldBack(function(acc) {
                return function(elem) {
                    return (acc + elem);
                };
            })(list)(0);
        };
    return Assert.AreEqual("List.foldBack function: Sum List", sumListBack(new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())))), 6);
};
Pit.Test.ListTest.FoldBack2 = function() {
    var subtractArrayBack = function(array1) {
            return function(array2) {
                return Pit.FSharp.Collections.ListModule.FoldBack2(function(elem) {
                    return function(acc1) {
                        return function(acc2) {
                            return (elem - (acc1 - acc2));
                        };
                    };
                })(array1)(array2)(0);
            };
        };
    var a1 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var a2 = new Pit.FSharp.Collections.FSharpList1.Cons(4, new Pit.FSharp.Collections.FSharpList1.Cons(5, new Pit.FSharp.Collections.FSharpList1.Cons(6, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var res = subtractArrayBack(a1)(a2);
    return Assert.AreEqual("List.fold2 function:", res, -9);
};
Pit.Test.ListTest.Reduce = function() {
    var sumAList = function(list) {
            return (function(thisObject) {
                try {
                    return Pit.FSharp.Collections.ListModule.Reduce(function(acc) {
                        return function(elem) {
                            return (acc + elem);
                        };
                    })(list);
                } catch (matchValue) {
                    (function(thisObject) {
                        if (matchValue instanceof Pit.ArgumentException) {
                            var exc = matchValue;
                            return 0;
                        } else {
                            return Pit.FSharp.Core.Operators.Reraise();
                        }
                    })(thisObject)
                }
            })(this);
        };
    var resultSum = sumAList(new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(4, new Pit.FSharp.Collections.FSharpList1.Cons(10, new Pit.FSharp.Collections.FSharpList1.Empty()))));
    return Assert.AreEqual("List.reduce function:", resultSum, 16);
};
Pit.Test.SeqTest.Declare = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var i = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(5)(source);
    });
    return Assert.AreEqual("Seq Declare", 6, i);
};
Pit.Test.SeqTest.Initialize = function() {
    var s = Pit.FSharp.Collections.SeqModule.Initialize(5)(function(i) {
        return i;
    });
    var i = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(2)(source);
    });
    return Assert.AreEqual("Seq Init", 2, i);
};
Pit.Test.SeqTest.InitializeInfinite = function() {
    var s = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.SeqModule.InitializeInfinite(function(i) {
        return (i + 1);
    }))(function(source) {
        return Pit.FSharp.Collections.SeqModule.Take(10)(source);
    });
    var len = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Length(source);
    });
    return Assert.AreEqual("Seq Infinite", 10, len);
};
Pit.Test.SeqTest.OfArray = function() {
    var array = [1, 2, 3];
    var s = Pit.FSharp.Collections.SeqModule.OfArray(array);
    var i = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(0)(source);
    });
    return Assert.AreEqual("Seq OfArray", 1, i);
};
Pit.Test.SeqTest.Iterate = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(2));
    var idx = 1;
    return Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Iterate(function(i) {
            Assert.AreEqual("Seq Iterate", i, idx);
            return idx = (idx + 1);
        })(source);
    });
};
Pit.Test.SeqTest.IterateIndexed = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(2));
    var r = 1;
    return Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.IterateIndexed(function(idx) {
            return function(i) {
                Assert.AreEqual("Seq IterateIndexed", i, r);
                return r = (r + 1);
            };
        })(source);
    });
};
Pit.Test.SeqTest.Exists = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(3));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Exists(function(i) {
            return i == 2;
        })(source);
    });
    return Assert.AreEqual("Seq Exists", true, r);
};
Pit.Test.SeqTest.ForAll = function() {
    var predicate = function(elem) {
            return elem >= 0;
        };
    var allPositive = function(source) {
            return Pit.FSharp.Collections.SeqModule.ForAll(predicate)(source);
        };
    var r = allPositive([0, 1, 2, 3]);
    return Assert.AreEqual("Seq ForAll", true, r);
};
Pit.Test.SeqTest.Iterate2 = function() {
    var s1 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(2));
    var s2 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(2));
    var r = 1;
    return Pit.FSharp.Collections.SeqModule.Iterate2(function(i1) {
        return function(i2) {
            Assert.AreEqual("Seq Iterate2", i1, r);
            Assert.AreEqual("Seq Iterate2", i2, r);
            return r = (r + 1);
        };
    })(s1)(s2);
};
Pit.Test.SeqTest.Filter = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Filter(function(i) {
            return i < 5;
        })(source);
    });
    return Assert.AreEqual("Seq Filter", 4, Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Length(source);
    }));
};
Pit.Test.SeqTest.Map = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(5));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Map(function(i) {
            return (i + i);
        })(source);
    });
    return Assert.AreEqual("Seq Map", 10, Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(4)(source);
    }));
};
Pit.Test.SeqTest.MapIndexed = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(5));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.MapIndexed(function(idx) {
            return function(i) {
                return (idx + i);
            };
        })(source);
    });
    return Assert.AreEqual("Seq MapIndexed", 9, Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(4)(source);
    }));
};
Pit.Test.SeqTest.Map2 = function() {
    var s1 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(5));
    var s2 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(6)(10));
    var r = Pit.FSharp.Collections.SeqModule.Map2(function(i1) {
        return function(i2) {
            return (i1 + i2);
        };
    })(s1)(s2);
    return Assert.AreEqual("Seq Map2", 11, Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(2)(source);
    }));
};
Pit.Test.SeqTest.Choose = function() {
    var numbers = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var evens = Pit.FSharp.Collections.SeqModule.Choose(function(x) {
        var x1 = x;
        return (function() {
            return (function(thisObject) {
                var x1 = x;
                if ((x1 % 2) == 0) {
                    return new Pit.FSharp.Core.FSharpOption1.Some(x1);
                } else {
                    return new Pit.FSharp.Core.FSharpOption1.None();
                };
            })(this);
        })();
    })(numbers);
    return Assert.AreEqual("Seq Choose", 4, Pit.FSharp.Core.Operators.op_PipeRight(evens)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(1)(source);
    }));
};
Pit.Test.SeqTest.Zip = function() {
    var s1 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(2));
    var s2 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(3)(4));
    var r = Pit.FSharp.Collections.SeqModule.Zip(s1)(s2);
    var patternInput = Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(0)(source);
    });
    var i2 = patternInput.Item2;
    var i1 = patternInput.Item1;
    Assert.AreEqual("Seq Zip", 1, i1);
    return Assert.AreEqual("Seq Zip", 3, i2);
};
Pit.Test.SeqTest.Zip3 = function() {
    var s1 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(2));
    var s2 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(3)(4));
    var s3 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(5)(6));
    var r = Pit.FSharp.Collections.SeqModule.Zip3(s1)(s2)(s3);
    var patternInput = Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(0)(source);
    });
    var i3 = patternInput.Item3;
    var i2 = patternInput.Item2;
    var i1 = patternInput.Item1;
    Assert.AreEqual("Seq Zip", 1, i1);
    Assert.AreEqual("Seq Zip", 3, i2);
    return Assert.AreEqual("Seq Zip", 5, i3);
};
Pit.Test.SeqTest.TryPick = function() {
    var values = [{
        Item1: "a",
        Item2: 1
    }, {
        Item1: "b",
        Item2: 2
    }, {
        Item1: "c",
        Item2: 3
    }];
    var resultPick = Pit.FSharp.Collections.SeqModule.TryPick(function(elem) {
        return (function(thisObject) {
            if (elem.Item2 == 2) {
                var value = elem.Item1;
                return new Pit.FSharp.Core.FSharpOption1.Some(value);
            } else {
                return new Pit.FSharp.Core.FSharpOption1.None();
            }
        })(this);
    })(values);
    return (function(thisObject) {
        if (resultPick instanceof Pit.FSharp.Core.FSharpOption1.None) {
            throw "Seq TryPick Failure"
        } else {
            var r = resultPick.get_Value();
            return Assert.AreEqual("Seq TryPick", "b", r);
        }
    })(this);
};
Pit.Test.SeqTest.Pick = function() {
    var values = [{
        Item1: "a",
        Item2: 1
    }, {
        Item1: "b",
        Item2: 2
    }, {
        Item1: "c",
        Item2: 3
    }];
    var resultPick = Pit.FSharp.Collections.SeqModule.Pick(function(elem) {
        return (function(thisObject) {
            if (elem.Item2 == 2) {
                var value = elem.Item1;
                return new Pit.FSharp.Core.FSharpOption1.Some(value);
            } else {
                return new Pit.FSharp.Core.FSharpOption1.None();
            }
        })(this);
    })(values);
    return Assert.AreEqual("Seq Pick", "b", resultPick);
};
Pit.Test.SeqTest.TryFind = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(5));
    var matchValue = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.TryFind(function(i) {
            return i == 2;
        })(source);
    });
    return (function(thisObject) {
        if (matchValue instanceof Pit.FSharp.Core.FSharpOption1.None) {
            throw "Seq TryFind Failure"
        } else {
            var t = matchValue.get_Value();
            return Assert.AreEqual("Seq TryFind", 2, t);
        }
    })(this);
};
Pit.Test.SeqTest.Find = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(5));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Find(function(i) {
            return i == 3;
        })(source);
    });
    return Assert.AreEqual("Seq Find", 3, r);
};
Pit.Test.SeqTest.Concat = function() {
    var s = Pit.FSharp.Collections.SeqModule.Concat([
        [1, 2, 3],
        [4, 5, 6],
        [7, 8, 9]
    ]);
    return Assert.AreEqual("Seq Concat", 9, Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Length(source);
    }));
};
Pit.Test.SeqTest.Length = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(5));
    return Assert.AreEqual("Seq Length", 5, Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Length(source);
    }));
};
Pit.Test.SeqTest.Fold = function() {
    var sumSeq = function(sequence1) {
            return Pit.FSharp.Collections.SeqModule.Fold(function(acc) {
                return function(elem) {
                    return (acc + elem);
                };
            })(0)(sequence1);
        };
    var sum = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.SeqModule.Initialize(10)(function(index) {
        return (index * index);
    }))(sumSeq);
    return Assert.AreEqual("Seq Sum", 285, sum);
};
Pit.Test.SeqTest.Reduce = function() {
    var names = ["A", "man", "landed", "on", "the", "moon"];
    var sentence = Pit.FSharp.Core.Operators.op_PipeRight(names)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Reduce(function(acc) {
            return function(item) {
                return ((acc + " ") + item);
            };
        })(source);
    });
    return Assert.AreEqual("Seq Reduce", sentence, "A man landed on the moon");
};
Pit.Test.SeqTest.Append = function() {
    var s1 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(5));
    var s2 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(6)(10));
    var r = Pit.FSharp.Collections.SeqModule.Append(s1)(s2);
    return Assert.AreEqual("Seq Append", 10, Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Length(source);
    }));
};
Pit.Test.SeqTest.Collect = function() {
    var k = Pit.FSharp.Collections.SeqModule.Collect(function(elem) {
        return Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(0)(elem)));
    })([1, 5, 10]);
    return Assert.AreEqual("Seq Collect", 19, Pit.FSharp.Core.Operators.op_PipeRight(k)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Length(source);
    }));
};
Pit.Test.SeqTest.CompareWith = function() {
    var s1 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var s2 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_RangeStep(10)(-1)(1));
    var comparer = function(elem1) {
            return function(elem2) {
                return (function(thisObject) {
                    if (elem1 > elem2) {
                        return 1;
                    } else {
                        return (function(thisObject) {
                            if (elem1 < elem2) {
                                return -1;
                            } else {
                                return 0;
                            }
                        })(thisObject);
                    }
                })(this);
            };
        };
    var compareSequences = function(source1) {
            return function(source2) {
                return Pit.FSharp.Collections.SeqModule.CompareWith(comparer)(source1)(source2);
            };
        };
    var compareResult1 = compareSequences(s1)(s2);
    var res = (function(thisObject) {
        if (compareResult1 == -1) {
            return "Sequence1 is less than sequence2.";
        } else {
            return (function(thisObject) {
                if (compareResult1 == 0) {
                    return "Sequence1 is equal to sequence2.";
                } else {
                    return (function(thisObject) {
                        if (compareResult1 == 1) {
                            return "Sequence1 is greater than sequence2.";
                        } else {
                            throw "Invalid comparison result."
                        }
                    })(thisObject);
                }
            })(thisObject);
        }
    })(this);
    return Assert.AreEqual("Seq CompareWith", "Sequence1 is less than sequence2.", res);
};
Pit.Test.SeqTest.Singleton = function() {
    var res1 = Pit.FSharp.Collections.SeqModule.Singleton(10);
    var i1 = Pit.FSharp.Core.Operators.op_PipeRight(res1)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(0)(source);
    });
    var i2 = Pit.FSharp.Core.Operators.op_PipeRight(res1)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(0)(source);
    });
    return Assert.AreEqual("Seq Singleton", i1, i2);
};
Pit.Test.SeqTest.Truncate = function() {
    var mySeq = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
        return Pit.FSharp.Collections.SeqModule.Map(function(i) {
            return (i * i);
        })(Pit.FSharp.Core.Operators.op_Range(1)(10));
    }));
    var truncatedSeq = Pit.FSharp.Collections.SeqModule.Truncate(5)(mySeq);
    return Assert.AreEqual("Seq Truncate", 1, Pit.FSharp.Core.Operators.op_PipeRight(truncatedSeq)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(0)(source);
    }));
};
Pit.Test.SeqTest.Take = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Take(5)(source);
    });
    return Assert.AreEqual("Seq Take", 5, Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(4)(source);
    }));
};
Pit.Test.SeqTest.TakeWhile = function() {
    var mySeq = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
        return Pit.FSharp.Collections.SeqModule.Map(function(i) {
            return (i * i);
        })(Pit.FSharp.Core.Operators.op_Range(1)(10));
    }));
    var res = Pit.FSharp.Collections.SeqModule.TakeWhile(function(elem) {
        return elem < 10;
    })(mySeq);
    return Assert.AreEqual("Seq TakeWhile", 9, Pit.FSharp.Core.Operators.op_PipeRight(res)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(2)(source);
    }));
};
Pit.Test.SeqTest.Skip = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Skip(4)(source);
    });
    return Assert.AreEqual("Seq Skip", 7, Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(2)(source);
    }));
};
Pit.Test.SeqTest.SkipWhile = function() {
    var mySeq = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
        return Pit.FSharp.Collections.SeqModule.Map(function(i) {
            return (i * i);
        })(Pit.FSharp.Core.Operators.op_Range(1)(10));
    }));
    var res = Pit.FSharp.Core.Operators.op_PipeRight(mySeq)(function(source) {
        return Pit.FSharp.Collections.SeqModule.SkipWhile(function(el) {
            return el < 10;
        })(source);
    });
    return Assert.AreEqual("Seq SkipWhile", 36, Pit.FSharp.Core.Operators.op_PipeRight(res)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(2)(source);
    }));
};
Pit.Test.SeqTest.PairWise = function() {
    var s = Pit.FSharp.Collections.SeqModule.Pairwise(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
        return Pit.FSharp.Collections.SeqModule.Map(function(i) {
            return (i * i);
        })(Pit.FSharp.Core.Operators.op_Range(1)(10));
    })));
    var patternInput = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(2)(source);
    });
    var i2 = patternInput.Item2;
    var i1 = patternInput.Item1;
    Assert.AreEqual("Seq PairWise", i1, 9);
    return Assert.AreEqual("Seq PairWise", i2, 16);
};
Pit.Test.SeqTest.Scan = function() {
    var sumSeq = function(sequence1) {
            return Pit.FSharp.Collections.SeqModule.Scan(function(acc) {
                return function(elem) {
                    return (acc + elem);
                };
            })(0)(sequence1);
        };
    var sum = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.SeqModule.Initialize(10)(function(index) {
        return (index * index);
    }))(sumSeq);
    return Assert.AreEqual("Seq Scan", 14, Pit.FSharp.Core.Operators.op_PipeRight(sum)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(4)(source);
    }));
};
Pit.Test.SeqTest.FindIndex = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var f = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.FindIndex(function(i) {
            return i == 5;
        })(source);
    });
    return Assert.AreEqual("Seq FindIndex", 4, f);
};
Pit.Test.SeqTest.TryFindIndex = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var matchValue = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.TryFindIndex(function(i) {
            return i == 5;
        })(source);
    });
    return (function(thisObject) {
        if (matchValue instanceof Pit.FSharp.Core.FSharpOption1.None) {
            throw "Seq TryFindIndex Failure"
        } else {
            var t = matchValue.get_Value();
            return Assert.AreEqual("Seq TryFindIndex", 4, t);
        }
    })(this);
};
Pit.Test.SeqTest.ToList = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.ToList(source);
    });
    return Assert.AreEqual("Seq ToList", 5, Pit.FSharp.Collections.ListModule.Get(r)(4));
};
Pit.Test.SeqTest.OfList = function() {
    var l = Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10)));
    var s = Pit.FSharp.Core.Operators.op_PipeRight(l)(function(source) {
        return Pit.FSharp.Collections.SeqModule.OfList(source);
    });
    return Assert.AreEqual("Seq OfList", 2, Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(1)(source);
    }));
};
Pit.Test.SeqTest.ToArray = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var a = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.ToArray(source);
    });
    return Assert.AreEqual("Seq ToArray", 2, a[1]);
};
Pit.Test.SeqTest.Sum = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Sum(source);
    });
    return Assert.AreEqual("Seq Sum", 55, r);
};
Pit.Test.SeqTest.SumBy = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.SumBy(function(x) {
            return (x * x);
        })(source);
    });
    return Assert.AreEqual("Seq SumBy", 385, r);
};
Pit.Test.SeqTest.Average = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Average(source);
    });
    return Assert.AreEqual("Seq Average", 5.5, r);
};
Pit.Test.SeqTest.AverageBy = function() {
    var avg2 = Pit.FSharp.Collections.SeqModule.AverageBy(function(el) {
        return Pit.FSharp.Core.Operators.ToDouble(el);
    })(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10)));
    return Assert.AreEqual("Seq Average", 5.5, avg2);
};
Pit.Test.SeqTest.Min = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Min(source);
    });
    return Assert.AreEqual("Seq Min", 1, r);
};
Pit.Test.SeqTest.MinBy = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(-10)(10));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.MinBy(function(x) {
            return ((x * x) - 1);
        })(source);
    });
    return Assert.AreEqual("Seq MinBy", 0, r);
};
Pit.Test.SeqTest.Max = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Max(source);
    });
    return Assert.AreEqual("Seq Max", 10, r);
};
Pit.Test.SeqTest.MaxBy = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(-10)(10));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.MaxBy(function(x) {
            return ((x * x) - 1);
        })(source);
    });
    return Assert.AreEqual("Seq MaxBy", -10, r);
};
Pit.Test.SeqTest.ForAll2 = function() {
    var predicate = function(elem1) {
            return function(elem2) {
                return elem1 == elem2;
            };
        };
    var allEqual = function(source1) {
            return function(source2) {
                return Pit.FSharp.Collections.SeqModule.ForAll2(predicate)(source1)(source2);
            };
        };
    var r1 = allEqual([1, 2])([1, 2]);
    var r2 = allEqual([2, 1])([1, 2]);
    Assert.AreEqual("Seq ForAll2", true, r1);
    return Assert.AreEqual("Seq ForAll2", false, r2);
};
Pit.Test.SeqTest.Exists2 = function() {
    var s1 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(5));
    var s2 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_RangeStep(5)(-1)(1));
    var r = Pit.FSharp.Collections.SeqModule.Exists2(function(i1) {
        return function(i2) {
            return i1 == i2;
        };
    })(s1)(s2);
    return Assert.AreEqual("Seq Exists", true, r);
};
Pit.Test.SeqTest.Head = function() {
    var s1 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(5));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s1)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Head(source);
    });
    return Assert.AreEqual("Seq Head", 1, r);
};
Pit.Test.SeqTest.GroupBy = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var g = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.GroupBy(function(i) {
            return (i % 2) == 0;
        })(source);
    });
    var patternInput = Pit.FSharp.Core.Operators.op_PipeRight(g)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(0)(source);
    });
    var r1 = patternInput.Item1;
    var i1 = patternInput.Item2;
    var patternInput1 = Pit.FSharp.Core.Operators.op_PipeRight(g)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(1)(source);
    });
    var r2 = patternInput1.Item1;
    var i2 = patternInput1.Item2;
    return Assert.AreEqual("Seq GroupBy", Pit.FSharp.Core.Operators.op_PipeRight(i1)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Length(source);
    }), Pit.FSharp.Core.Operators.op_PipeRight(i2)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Length(source);
    }));
};
Pit.Test.SeqTest.CountBy = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var g = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.CountBy(function(i) {
            return (i % 2) == 0;
        })(source);
    });
    var patternInput = Pit.FSharp.Core.Operators.op_PipeRight(g)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(0)(source);
    });
    var r1 = patternInput.Item1;
    var i1 = patternInput.Item2;
    var patternInput1 = Pit.FSharp.Core.Operators.op_PipeRight(g)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(1)(source);
    });
    var r2 = patternInput1.Item1;
    var i2 = patternInput1.Item2;
    return Assert.AreEqual("Seq CountBy", i1, i2);
};
Pit.Test.SeqTest.Distinct = function() {
    var s = [1, 1, 2, 2];
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Distinct(source);
    });
    return Assert.AreEqual("Seq Distinct", 2, Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Length(source);
    }));
};
Pit.Test.SeqTest.DistinctBy = function() {
    var s = Pit.FSharp.Core.Operators.op_Range(-5)(10);
    var r = Pit.FSharp.Collections.SeqModule.DistinctBy(function(elem) {
        return Pit.FSharp.Core.Operators.Abs(elem);
    })(s);
    return Assert.AreEqual("Seq DistinctBy", 0, Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(5)(source);
    }));
};
Pit.Test.SeqTest.Sort = function() {
    var s = [10, -2, 4, 9];
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Sort(source);
    });
    return Assert.AreEqual("Seq Sort", -2, Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(0)(source);
    }));
};
Pit.Test.SeqTest.SortBy = function() {
    var s = [10, -2, 4, 9];
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.SortBy(function(i) {
            return (i % 2) == 0;
        })(source);
    });
    return Assert.AreEqual("Seq SortBy", -2, Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Get(2)(source);
    }));
};
Pit.Test.SeqTest.Windowed = function() {
    var s = Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(9)));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Windowed(3)(source);
    });
    return Assert.AreEqual("Seq Windowed", 7, Pit.FSharp.Core.Operators.op_PipeRight(r)(function(source) {
        return Pit.FSharp.Collections.SeqModule.Length(source);
    }));
};
Pit.Test.SeqTest.ReadOnly = function() {
    var s = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10));
    var r = Pit.FSharp.Core.Operators.op_PipeRight(s)(function(source) {
        return Pit.FSharp.Collections.SeqModule.ReadOnly(source);
    });
    var e = r.IEnumerable1_GetEnumerator();
    return (function(thisObject) {
        try {
            var m = e.IEnumerator_MoveNext();
            return Assert.AreEqual("Seq ReadOnly", true, m);
        } finally {
            (function(thisObject) {
                if (e.containsInterface("__getIDisposable")) {
                    return e.IDisposable_Dispose();
                } else {
                    return null;
                }
            })(thisObject)
        }
    })(this);
};
Pit.Test.ArrayTest.Declare = function() {
    var arr1 = [1, 2, 3];
    return Assert.AreEqual("Declare Array", arr1[0], 1);
};
Pit.Test.ArrayTest.Length = function() {
    var array = [1, 2, 3, 4, 5];
    var len = Pit.FSharp.Collections.ArrayModule.Length(array);
    return Assert.AreEqual("Array Length", len, 5);
};
Pit.Test.ArrayTest.DeclareRange = function() {
    var array = Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
        return Pit.FSharp.Collections.SeqModule.Map(function(i) {
            return (i * i);
        })(Pit.FSharp.Core.Operators.op_Range(1)(10));
    })));
    return Assert.AreEqual("Declare Array Range", array[4], 25);
};
Pit.Test.ArrayTest.ZeroCreate = function() {
    var array = Pit.FSharp.Collections.ArrayModule.ZeroCreate(10);
    return Assert.AreEqual("Array Zero Create", array[0], 0);
};
Pit.Test.ArrayTest.CreateGetSet = function() {
    var array1 = Pit.FSharp.Collections.ArrayModule.Create(5)('');
    for (var i = 0; i <= (array1.get_Length() - 1); i++) {
        (function(thisObject, i) {
            Pit.FSharp.Collections.ArrayModule.Set(array1)(i)(i.ToString())
        })(this, i);
    };
    return Assert.AreEqual("Array Create/Get/Set", array1[0], Pit.FSharp.Collections.ArrayModule.Get(array1)(0));
};
Pit.Test.ArrayTest.Init = function() {
    var array = Pit.FSharp.Collections.ArrayModule.Initialize(5)(function(index) {
        return (index * index);
    });
    return Assert.AreEqual("Array Init", array[4], 16);
};
Pit.Test.ArrayTest.Copy = function() {
    var array1 = Pit.FSharp.Collections.ArrayModule.Initialize(5)(function(index) {
        return (index * index);
    });
    var array2 = Pit.FSharp.Collections.ArrayModule.Copy(array1);
    return Assert.AreEqual("Array Copy", array1[0], array2[0]);
};
Pit.Test.ArrayTest.Sub = function() {
    var a1 = Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10)));
    var a2 = Pit.FSharp.Collections.ArrayModule.GetSubArray(a1)(2)(6);
    return Assert.AreEqual("Array Sub", 6, a2.get_Length());
};
Pit.Test.ArrayTest.Append = function() {
    var m = Pit.FSharp.Collections.ArrayModule.Append([1, 2, 3])([4, 5, 6]);
    return Assert.AreEqual("Array append", 6, m.get_Length());
};
Pit.Test.ArrayTest.Choose = function() {
    var k = Pit.FSharp.Collections.ArrayModule.Choose(function(elem) {
        return (function(thisObject) {
            if ((elem % 2) == 0) {
                return new Pit.FSharp.Core.FSharpOption1.Some(Pit.FSharp.Core.Operators.ToDouble(((elem * elem) - 1)));
            } else {
                return new Pit.FSharp.Core.FSharpOption1.None();
            }
        })(this);
    })(Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10))));
    return Assert.AreEqual("Array Choose", k[0], 3);
};
Pit.Test.ArrayTest.Collect = function() {
    var k = Pit.FSharp.Collections.ArrayModule.Collect(function(elem) {
        return Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(0)(elem)));
    })([1, 5, 10]);
    return Assert.AreEqual("Array Collect", 19, k.get_Length());
};
Pit.Test.ArrayTest.Concat = function() {
    var multiplicationTable = function(max) {
            return Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
                return Pit.FSharp.Collections.SeqModule.Map(function(i) {
                    return Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
                        return Pit.FSharp.Collections.SeqModule.Map(function(j) {
                            return {
                                Item1: i,
                                Item2: j,
                                Item3: (i * j)
                            };
                        })(Pit.FSharp.Core.Operators.op_Range(1)(max));
                    })));
                })(Pit.FSharp.Core.Operators.op_Range(1)(max));
            }));
        };
    var array = Pit.FSharp.Collections.ArrayModule.Concat(multiplicationTable(3));
    var patternInput = array[3];
    var k = patternInput.Item3;
    var j = patternInput.Item2;
    var i = patternInput.Item1;
    Assert.AreEqual("Array Concat - i", i, 2);
    Assert.AreEqual("Array Concat - j", j, 1);
    return Assert.AreEqual("Array Concat - k", k, 2);
};
Pit.Test.ArrayTest.Filter = function() {
    var k = Pit.FSharp.Collections.ArrayModule.Filter(function(elem) {
        return (elem % 2) == 0;
    })(Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10))));
    return Assert.AreEqual("Array Filter", k[0], 2);
};
Pit.Test.ArrayTest.Rev = function() {
    var a = Pit.FSharp.Collections.ArrayModule.Reverse([3, 2, 1]);
    return Assert.AreEqual("Array Reverse", a[0], 1);
};
Pit.Test.ArrayTest.FilterChooseRev = function() {
    var a = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10))))(function(array) {
        return Pit.FSharp.Collections.ArrayModule.Filter(function(elem) {
            return (elem % 2) == 0;
        })(array);
    }))(function(array) {
        return Pit.FSharp.Collections.ArrayModule.Choose(function(elem) {
            return (function(thisObject) {
                if (elem != 8) {
                    return new Pit.FSharp.Core.FSharpOption1.Some((elem * elem));
                } else {
                    return new Pit.FSharp.Core.FSharpOption1.None();
                }
            })(this);
        })(array);
    }))(function(array) {
        return Pit.FSharp.Collections.ArrayModule.Reverse(array);
    });
    return Assert.AreEqual("Array Filter Choose Reverse", a[0], 100);
};
Pit.Test.ArrayTest.Exists1 = function() {
    var allNegative = Pit.FSharp.Core.Operators.op_ComposeRight(function(array) {
        return Pit.FSharp.Collections.ArrayModule.Exists(function(elem) {
            return Pit.FSharp.Core.Operators.Abs(elem) == elem;
        })(array);
    })(function(value) {
        return !value;
    });
    var res = allNegative([-1, -2, -3]);
    return Assert.AreEqual("Array Exists Equal", res, true);
};
Pit.Test.ArrayTest.Exists2 = function() {
    var allNegative = Pit.FSharp.Core.Operators.op_ComposeRight(function(array) {
        return Pit.FSharp.Collections.ArrayModule.Exists(function(elem) {
            return Pit.FSharp.Core.Operators.Abs(elem) == elem;
        })(array);
    })(function(value) {
        return !value;
    });
    var res = allNegative([-1, 2, -3]);
    return Assert.AreNotEqual("Array Exists Not Equal", res, false);
};
Pit.Test.ArrayTest.Exists2Function = function() {
    var predicate = function(elem1) {
            return function(elem2) {
                return elem1 == elem2;
            };
        };
    var haveEqualElement = function(array1) {
            return function(array2) {
                return Pit.FSharp.Collections.ArrayModule.Exists2(predicate)(array1)(array2);
            };
        };
    var res = haveEqualElement([1, 2, 3])([3, 2, 1]);
    return Assert.AreEqual("Array Exists2", res, true);
};
Pit.Test.ArrayTest.ForAll = function() {
    var predicate = function(elem) {
            return elem >= 0;
        };
    var allPositive = function(array) {
            return Pit.FSharp.Collections.ArrayModule.ForAll(predicate)(array);
        };
    var res = allPositive([0, 1, 2, 3]);
    return Assert.AreEqual("Array For All", res, true);
};
Pit.Test.ArrayTest.ForAll2 = function() {
    var predicate = function(elem1) {
            return function(elem2) {
                return elem1 == elem2;
            };
        };
    var allEqual = function(array1) {
            return function(array2) {
                return Pit.FSharp.Collections.ArrayModule.ForAll2(predicate)(array1)(array2);
            };
        };
    var res = allEqual([1, 2])([1, 2]);
    return Assert.AreEqual("Array ForAll2", res, true);
};
Pit.Test.ArrayTest.FindAndFindIndex = function() {
    var a1 = Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10)));
    var i = Pit.FSharp.Core.Operators.op_PipeRight(a1)(function(array) {
        return Pit.FSharp.Collections.ArrayModule.Find(function(a) {
            return a == 5;
        })(array);
    });
    Assert.AreEqual("Array Find", 5, i);
    var i2 = Pit.FSharp.Core.Operators.op_PipeRight(a1)(function(array) {
        return Pit.FSharp.Collections.ArrayModule.FindIndex(function(a) {
            return a == 5;
        })(array);
    });
    return Assert.AreEqual("Array Find", 4, i2);
};
Pit.Test.ArrayTest.TryFind = function() {
    var array = Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10)));
    var item = Pit.FSharp.Core.Operators.op_PipeRight(array)(function(array1) {
        return Pit.FSharp.Collections.ArrayModule.TryFind(function(i) {
            return i == 2;
        })(array1);
    });
    return (function(thisObject) {
        if (item instanceof Pit.FSharp.Core.FSharpOption1.None) {
            throw "Item Not Found"
        } else {
            var i = item.get_Value();
            return Assert.AreEqual("Array Try Find", i, 2);
        }
    })(this);
};
Pit.Test.ArrayTest.Fill = function() {
    var arrayFill1 = Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10)));
    Pit.FSharp.Collections.ArrayModule.Fill(arrayFill1)(3)(5)(0);
    return Assert.AreEqual("Array Fill", arrayFill1[3], 0);
};
Pit.Test.ArrayTest.Iterate = function() {
    var array = [1];
    return Pit.FSharp.Core.Operators.op_PipeRight(array)(function(array1) {
        return Pit.FSharp.Collections.ArrayModule.Iterate(function(i) {
            return Assert.AreEqual("Array Iterate", i, 1);
        })(array1);
    });
};
Pit.Test.ArrayTest.IterateIndexed = function() {
    var array = [1, 2];
    var array2 = [1, 2];
    return Pit.FSharp.Core.Operators.op_PipeRight(array)(function(array1) {
        return Pit.FSharp.Collections.ArrayModule.IterateIndexed(function(idx) {
            return function(i) {
                return Assert.AreEqual("Array Iterate Indexed", i, array2[idx]);
            };
        })(array1);
    });
};
Pit.Test.ArrayTest.IterateIndexed2 = function() {
    var array = [1];
    var array2 = [3];
    return Pit.FSharp.Core.Operators.op_PipeRight(array2)(function(array21) {
        return Pit.FSharp.Collections.ArrayModule.IterateIndexed2(function(idx) {
            return function(i1) {
                return function(i2) {
                    Assert.AreEqual("Array Iterate Indexed2", i1, 1);
                    return Assert.AreEqual("Array Iterate Indexed2", i2, 3);
                };
            };
        })(array)(array21);
    });
};
Pit.Test.ArrayTest.Map = function() {
    var array = [1, 2];
    var array2 = Pit.FSharp.Core.Operators.op_PipeRight(array)(function(array1) {
        return Pit.FSharp.Collections.ArrayModule.Map(function(i) {
            return (i + i);
        })(array1);
    });
    return Assert.AreEqual("Array Map", array2[1], 4);
};
Pit.Test.ArrayTest.MapIndexed = function() {
    var array = [1, 2];
    var array2 = Pit.FSharp.Core.Operators.op_PipeRight(array)(function(array1) {
        return Pit.FSharp.Collections.ArrayModule.MapIndexed(function(idx) {
            return function(i) {
                return (idx + i);
            };
        })(array1);
    });
    return Assert.AreEqual("Array MapIndexed", array2[1], 3);
};
Pit.Test.ArrayTest.Map2 = function() {
    var array = [1, 2];
    var array2 = [3, 4];
    var resultArray = Pit.FSharp.Core.Operators.op_PipeRight(array2)(function(array21) {
        return Pit.FSharp.Collections.ArrayModule.Map2(function(i1) {
            return function(i2) {
                return (i1 + i2);
            };
        })(array)(array21);
    });
    return Assert.AreEqual("Array Map2", resultArray[1], 6);
};
Pit.Test.ArrayTest.MapIndexed2 = function() {
    var array = [1, 2];
    var array2 = [3, 4];
    var resultArray = Pit.FSharp.Core.Operators.op_PipeRight(array2)(function(array21) {
        return Pit.FSharp.Collections.ArrayModule.MapIndexed2(function(idx) {
            return function(i1) {
                return function(i2) {
                    return ((idx + i1) + i2);
                };
            };
        })(array)(array21);
    });
    return Assert.AreEqual("Array MapIndexed2", resultArray[1], 7);
};
Pit.Test.ArrayTest.Pick = function() {
    var values = [{
        Item1: "a",
        Item2: 1
    }, {
        Item1: "b",
        Item2: 2
    }, {
        Item1: "c",
        Item2: 3
    }];
    var resultPick = Pit.FSharp.Collections.ArrayModule.Pick(function(elem) {
        return (function(thisObject) {
            if (elem.Item2 == 2) {
                var value = elem.Item1;
                return new Pit.FSharp.Core.FSharpOption1.Some(value);
            } else {
                return new Pit.FSharp.Core.FSharpOption1.None();
            }
        })(this);
    })(values);
    return Assert.AreEqual("Array Pick", "b", resultPick);
};
Pit.Test.ArrayTest.TryPick = function() {
    var values = [{
        Item1: "a",
        Item2: 1
    }, {
        Item1: "b",
        Item2: 2
    }, {
        Item1: "c",
        Item2: 3
    }];
    var resultPick = Pit.FSharp.Collections.ArrayModule.TryPick(function(elem) {
        return (function(thisObject) {
            if (elem.Item2 == 2) {
                var value = elem.Item1;
                return new Pit.FSharp.Core.FSharpOption1.Some(value);
            } else {
                return new Pit.FSharp.Core.FSharpOption1.None();
            }
        })(this);
    })(values);
    return (function(thisObject) {
        if (resultPick instanceof Pit.FSharp.Core.FSharpOption1.None) {
            throw "TryPick failed"
        } else {
            var t = resultPick.get_Value();
            return Assert.AreEqual("Array TryPick", "b", t);
        }
    })(this);
};
Pit.Test.ArrayTest.Partition = function() {
    var array = [-2, -1, 1, 2];
    var patternInput = Pit.FSharp.Core.Operators.op_PipeRight(array)(function(array1) {
        return Pit.FSharp.Collections.ArrayModule.Partition(function(t) {
            return t < 0;
        })(array1);
    });
    var p = patternInput.Item2;
    var n = patternInput.Item1;
    return Assert.AreEqual("Array Partition", 2, n.get_Length());
};
Pit.Test.ArrayTest.Zip = function() {
    var array1 = [1, 2, 3];
    var array2 = [-1, -2, -3];
    var arrayZip = Pit.FSharp.Collections.ArrayModule.Zip(array1)(array2);
    var patternInput = Pit.FSharp.Collections.ArrayModule.Get(arrayZip)(1);
    var item2 = patternInput.Item2;
    var item1 = patternInput.Item1;
    Assert.AreEqual("Array Zip", 2, item1);
    return Assert.AreEqual("Array Zip", -2, item2);
};
Pit.Test.ArrayTest.Zip3 = function() {
    var array1 = [1, 2, 3];
    var array2 = [-1, -2, -3];
    var array3 = [-1, -2, -3];
    var arrayZip = Pit.FSharp.Collections.ArrayModule.Zip3(array1)(array2)(array3);
    var patternInput = Pit.FSharp.Collections.ArrayModule.Get(arrayZip)(1);
    var item3 = patternInput.Item3;
    var item2 = patternInput.Item2;
    var item1 = patternInput.Item1;
    Assert.AreEqual("Array Zip3", 2, item1);
    Assert.AreEqual("Array Zip3", -2, item2);
    return Assert.AreEqual("Array Zip3", -2, item3);
};
Pit.Test.ArrayTest.Unzip = function() {
    var patternInput = Pit.FSharp.Collections.ArrayModule.Unzip([{
        Item1: 1,
        Item2: 2
    }, {
        Item1: 3,
        Item2: 4
    }]);
    var array2 = patternInput.Item2;
    var array1 = patternInput.Item1;
    Assert.AreEqual("Array Unzip", 2, array1.get_Length());
    return Assert.AreEqual("Array Unzip", 2, array2.get_Length());
};
Pit.Test.ArrayTest.Unzip3 = function() {
    var patternInput = Pit.FSharp.Collections.ArrayModule.Unzip3([{
        Item1: 1,
        Item2: 2,
        Item3: 3
    }, {
        Item1: 3,
        Item2: 4,
        Item3: 3
    }]);
    var array3 = patternInput.Item3;
    var array2 = patternInput.Item2;
    var array1 = patternInput.Item1;
    Assert.AreEqual("Array Unzip3", 2, array1.get_Length());
    Assert.AreEqual("Array Unzip3", 2, array2.get_Length());
    return Assert.AreEqual("Array Unzip3", 2, array3.get_Length());
};
Pit.Test.ArrayTest.Fold = function() {
    var sumArray = function(array) {
            return Pit.FSharp.Collections.ArrayModule.Fold(function(acc) {
                return function(elem) {
                    return (acc + elem);
                };
            })(0)(array);
        };
    var a = [1, 2, 3];
    var res = sumArray(a);
    return Assert.AreEqual("Array Fold", 6, res);
};
Pit.Test.ArrayTest.FoldBack = function() {
    var subtractArrayBack = function(array1) {
            return Pit.FSharp.Collections.ArrayModule.FoldBack(function(elem) {
                return function(acc) {
                    return (elem - acc);
                };
            })(array1)(0);
        };
    var a = [1, 2, 3];
    var res = subtractArrayBack(a);
    return Assert.AreEqual("Array FoldBack", 2, res);
};
Pit.Test.ArrayTest.Fold2 = function() {
    var sumGreatest = function(array1) {
            return function(array2) {
                return Pit.FSharp.Collections.ArrayModule.Fold2(function(acc) {
                    return function(elem1) {
                        return function(elem2) {
                            return (acc + Pit.FSharp.Core.Operators.Max(elem1)(elem2));
                        };
                    };
                })(0)(array1)(array2);
            };
        };
    var sum = sumGreatest([1, 2, 3])([3, 2, 1]);
    return Assert.AreEqual("Array Fold2", 8, sum);
};
Pit.Test.ArrayTest.FoldBack2 = function() {
    var subtractArrayBack = function(array1) {
            return function(array2) {
                return Pit.FSharp.Collections.ArrayModule.FoldBack2(function(elem) {
                    return function(acc1) {
                        return function(acc2) {
                            return (elem - (acc1 - acc2));
                        };
                    };
                })(array1)(array2)(0);
            };
        };
    var a1 = [1, 2, 3];
    var a2 = [4, 5, 6];
    var res = subtractArrayBack(a1)(a2);
    return Assert.AreEqual("Array FoldBack2", -9, res);
};
Pit.Test.ArrayTest.Scan = function() {
    var initialBalance = 1122.73;
    var transactions = [-100, 450.34, -62.34, -127, -13.5, -12.92];
    var balances = Pit.FSharp.Collections.ArrayModule.Scan(function(balance) {
        return function(transactionAmount) {
            return (balance + transactionAmount);
        };
    })(initialBalance)(transactions);
    return Assert.AreEqual("Array Scan", 1022.73, balances[1]);
};
Pit.Test.ArrayTest.ScanBack = function() {
    var subtractArrayBack = function(array1) {
            return Pit.FSharp.Collections.ArrayModule.ScanBack(function(elem) {
                return function(acc) {
                    return (elem - acc);
                };
            })(array1)(0);
        };
    var a = [1, 2, 3];
    var res = subtractArrayBack(a);
    return Assert.AreEqual("Array ScanBack", 2, res[0]);
};
Pit.Test.ArrayTest.Reduce = function() {
    var names = ["A", "man", "landed", "on", "the", "moon"];
    var sentence = Pit.FSharp.Core.Operators.op_PipeRight(names)(function(array) {
        return Pit.FSharp.Collections.ArrayModule.Reduce(function(acc) {
            return function(item) {
                return ((acc + " ") + item);
            };
        })(array);
    });
    return Assert.AreEqual("Array Reduce", sentence, "A man landed on the moon");
};
Pit.Test.ArrayTest.ReduceBack = function() {
    var res = Pit.FSharp.Collections.ArrayModule.ReduceBack(function(elem) {
        return function(acc) {
            return (elem - acc);
        };
    })([1, 2, 3, 4]);
    return Assert.AreEqual("Array Reduce Back", res, -2);
};
Pit.Test.ArrayTest.SortInPlace = function() {
    var array = [10, 2, 4, 1];
    Pit.FSharp.Collections.ArrayModule.SortInPlace(array);
    return Assert.AreEqual("Array SortInPlace", 1, array[0]);
};
Pit.Test.ArrayTest.SortInPlaceBy = function() {
    var array1 = [1, 4, 8, -2, 5];
    Pit.FSharp.Collections.ArrayModule.SortInPlaceBy(function(elem) {
        return Pit.FSharp.Core.Operators.Abs(elem);
    })(array1);
    return Assert.AreEqual("Array SortInPlaceBy", 1, array1[0]);
};
Pit.Test.ArrayTest.SortInPlaceWith = function() {
    var array1 = [1, 4, 8, -2, 5];
    Pit.FSharp.Collections.ArrayModule.SortInPlaceWith(function(e1) {
        return function(e2) {
            return (e1 - e2);
        };
    })(array1);
    return Assert.AreEqual("Array SortInPlaceWith", -2, array1[0]);
};
Pit.Test.ArrayTest.SortWith = function() {
    var array1 = [1, 4, 8, -2, 5];
    var array2 = Pit.FSharp.Collections.ArrayModule.SortWith(function(e1) {
        return function(e2) {
            return (e1 - e2);
        };
    })(array1);
    return Assert.AreEqual("Array SortWith", -2, array2[0]);
};
Pit.Test.ArrayTest.Sort = function() {
    var array1 = [1, 4, 8, -2, 5];
    var array2 = Pit.FSharp.Collections.ArrayModule.Sort(array1);
    return Assert.AreEqual("Array Sort", -2, array2[0]);
};
Pit.Test.ArrayTest.Sort2 = function() {
    var array1 = ["Womciw", "Beosudo", "Guyx", "Rouh", "Iibow", "Tae", "Ebiucly", "Gonumaf", "Hiowvivb"];
    var array2 = ["Pye", "Gyhsy", "Lhfi", "Ouqilfo", "Ymukoed", "Nhap", "Aguccet", "Hahd", "Debcok"];
    var names = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.ArrayModule.Zip(array1)(array2))(function(array) {
        return Pit.FSharp.Collections.ArrayModule.Map(function(tupledArg) {
            var f = tupledArg.Item1;
            var s = tupledArg.Item2;
            return ((f + " ") + s);
        })(array);
    }))(function(array) {
        return Pit.FSharp.Collections.ArrayModule.Sort(array);
    });
    return Assert.AreEqual("Array Sort2", "Iibow Ymukoed", names[5]);
};
Pit.Test.ArrayTest.Permute = function() {
    var array1 = [1, 2, 3, 4, 5];
    var n = array1.get_Length();
    var permute = Pit.FSharp.Collections.ArrayModule.Permute(function(idx) {
        return (idx % n);
    })(array1);
    return Assert.AreEqual("Array Permute", 1, permute[0]);
};
Pit.Test.ArrayTest.Sum = function() {
    var a = [1, 2, 3, 4, 5];
    var s = Pit.FSharp.Collections.ArrayModule.Sum(a);
    return Assert.AreEqual("Array Sum", s, 15);
};
Pit.Test.ArrayTest.SumBy = function() {
    var s = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10))))(function(array) {
        return Pit.FSharp.Collections.ArrayModule.SumBy(function(x) {
            return (x * x);
        })(array);
    });
    return Assert.AreEqual("Array Sumby", 385, s);
};
Pit.Test.ArrayTest.Min = function() {
    var a = [1, 2, 3, 4];
    var s = Pit.FSharp.Collections.ArrayModule.Min(a);
    return Assert.AreEqual("Array Min", 1, s);
};
Pit.Test.ArrayTest.Max = function() {
    var a = [1, 2, 3, 4];
    var s = Pit.FSharp.Collections.ArrayModule.Max(a);
    return Assert.AreEqual("Array Max", 4, s);
};
Pit.Test.ArrayTest.MinBy = function() {
    var r = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(-10)(10))))(function(array) {
        return Pit.FSharp.Collections.ArrayModule.MinBy(function(x) {
            return ((x * x) - 1);
        })(array);
    });
    return Assert.AreEqual("Array MinBy", 0, r);
};
Pit.Test.ArrayTest.MaxBy = function() {
    var r = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(-10)(10))))(function(array) {
        return Pit.FSharp.Collections.ArrayModule.MaxBy(function(x) {
            return ((x * x) - 1);
        })(array);
    });
    return Assert.AreEqual("Array MaxBy", -10, r);
};
Pit.Test.ArrayTest.Average = function() {
    var r = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10))))(function(array) {
        return Pit.FSharp.Collections.ArrayModule.Average(array);
    });
    return Assert.AreEqual("Array Average", 5.5, r);
};
Pit.Test.ArrayTest.AverageBy = function() {
    var avg2 = Pit.FSharp.Collections.ArrayModule.AverageBy(function(elem) {
        return Pit.FSharp.Core.Operators.ToDouble(elem);
    })(Pit.FSharp.Collections.SeqModule.ToArray(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(10))));
    return Assert.AreEqual("Array AverageBy", 5.5, avg2);
};
Pit.Test.ArrayTest.ToList = function() {
    var array = [1, 2, 3];
    var list = Pit.FSharp.Collections.ArrayModule.ToList(array);
    return Assert.AreEqual("Array ToList", 1, list.get_Head());
};
Pit.Test.ArrayTest.OfList = function() {
    var list = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Empty())));
    var a = Pit.FSharp.Collections.ArrayModule.OfList(list);
    return Assert.AreEqual("Array OfList", 1, a[0]);
};
Pit.Test.ArrayTest.ToSeq = function() {
    var a = [1, 2, 3];
    var sequence = Pit.FSharp.Collections.ArrayModule.ToSeq(a);
    var e = sequence.IEnumerable1_GetEnumerator();
    return (function(thisObject) {
        try {
            Pit.FSharp.Core.Operators.op_PipeRight(e.IEnumerator_MoveNext())(function(value) {
                return Pit.FSharp.Core.Operators.Ignore(value);
            });
            return Assert.AreEqual("Array ToSeq", 1, e.IEnumerator1_get_Current());
        } finally {
            (function(thisObject) {
                if (e.containsInterface("__getIDisposable")) {
                    return e.IDisposable_Dispose();
                } else {
                    return null;
                }
            })(thisObject)
        }
    })(this);
};
Pit.Test.ArrayTest.OfSeq = function() {
    var sequence = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(5));
    var array = Pit.FSharp.Collections.ArrayModule.OfSeq(sequence);
    return Assert.AreEqual("Array OfSeq", 1, array[0]);
};
Pit.Test.Array2DTest.Init = function() {
    var arr = Pit.FSharp.Collections.Array2DModule.Initialize(2)(2)(function(i) {
        return function(j) {
            return (i + j);
        };
    });
    var r = Pit.FSharp.Collections.Array2DModule.Get(arr)(1)(1);
    return Assert.AreEqual("Array2D Init", r, 2);
};
Pit.Test.Array2DTest.Length1 = function() {
    var arr = Pit.FSharp.Collections.Array2DModule.Initialize(2)(2)(function(i) {
        return function(j) {
            return (i + j);
        };
    });
    var len1 = Pit.FSharp.Core.Operators.op_PipeRight(arr)(function(array) {
        return Pit.FSharp.Collections.Array2DModule.Length1(array);
    });
    return Assert.AreEqual("Array2D Length1", len1, 2);
};
Pit.Test.Array2DTest.Length2 = function() {
    var arr = Pit.FSharp.Collections.Array2DModule.Initialize(2)(2)(function(i) {
        return function(j) {
            return (i + j);
        };
    });
    var len1 = Pit.FSharp.Core.Operators.op_PipeRight(arr)(function(array) {
        return Pit.FSharp.Collections.Array2DModule.Length2(array);
    });
    return Assert.AreEqual("Array2D Length2", len1, 2);
};
Pit.Test.Array2DTest.GetSet = function() {
    var arr = Pit.FSharp.Collections.Array2DModule.Initialize(2)(2)(function(i) {
        return function(j) {
            return (i + j);
        };
    });
    Pit.FSharp.Collections.Array2DModule.Set(arr)(1)(0)(3);
    var r = Pit.FSharp.Collections.Array2DModule.Get(arr)(1)(0);
    return Assert.AreEqual("Array2D GetSet", r, 3);
};
Pit.Test.Array2DTest.ZeroCreate = function() {
    var arr = Pit.FSharp.Collections.Array2DModule.ZeroCreate(2)(2);
    var r = Pit.FSharp.Collections.Array2DModule.Get(arr)(1)(1);
    return Assert.AreEqual("Array2D ZeroCreate", r, 0);
};
Pit.Test.Array2DTest.Iter = function() {
    var arr = Pit.FSharp.Collections.Array2DModule.ZeroCreate(2)(2);
    var r = Pit.FSharp.Collections.Array2DModule.Length1(arr);
    Assert.AreEqual("Array2D Iter", r, 2);
    return Pit.FSharp.Core.Operators.op_PipeRight(arr)(function(array) {
        return Pit.FSharp.Collections.Array2DModule.Iterate(function(i) {
            return Assert.AreEqual("Array2D Iter", i, 0);
        })(array);
    });
};
Pit.Test.Array2DTest.IterateIndex = function() {
    var arr = Pit.FSharp.Collections.Array2DModule.Initialize(2)(2)(function(i) {
        return function(j) {
            return (i + j);
        };
    });
    return Pit.FSharp.Core.Operators.op_PipeRight(arr)(function(array) {
        return Pit.FSharp.Collections.Array2DModule.IterateIndexed(function(i) {
            return function(j) {
                return function(x) {
                    return Assert.AreEqual("Array2D IterateIndex", (i + j), x);
                };
            };
        })(array);
    });
};
Pit.Test.Array2DTest.Map = function() {
    var arr = Pit.FSharp.Collections.Array2DModule.ZeroCreate(2)(2);
    return Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(arr)(function(array) {
        return Pit.FSharp.Collections.Array2DModule.Map(function(i) {
            return 1;
        })(array);
    }))(function(array) {
        return Pit.FSharp.Collections.Array2DModule.Iterate(function(i) {
            return Assert.AreEqual("Array2D Iter", i, 1);
        })(array);
    });
};
Pit.Test.Array2DTest.MapIndexed = function() {
    var arr = Pit.FSharp.Collections.Array2DModule.ZeroCreate(2)(2);
    return Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.op_PipeRight(arr)(function(array) {
        return Pit.FSharp.Collections.Array2DModule.MapIndexed(function(i) {
            return function(j) {
                return function(x) {
                    return (i + j);
                };
            };
        })(array);
    }))(function(array) {
        return Pit.FSharp.Collections.Array2DModule.IterateIndexed(function(i) {
            return function(j) {
                return function(x) {
                    return Assert.AreEqual("Array2D IterateIndex", (i + j), x);
                };
            };
        })(array);
    });
};
