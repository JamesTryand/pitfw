registerNamespace("Pit.MonadsTest");
registerNamespace("Pit.Monads");
registerNamespace("Pit.Test.ActivePatternsTest");
registerNamespace("Pit.Test.RangeEnumeratorTests");
registerNamespace("Pit.Test.UOMTest");
registerNamespace("Pit.Test.OperatorOverloadTests");
registerNamespace("Pit.Test.OverloadedCtorsTests");
registerNamespace("Pit.Test.TryWithTests");
registerNamespace("Pit.Test.WhileTests");
registerNamespace("Pit.Test.UnionTests");
registerNamespace("Pit.Test.TupleTests");
registerNamespace("Pit.Test.RecordsTests");
registerNamespace("Pit.Test.PatternTests");
registerNamespace("Pit.Test.LetTests");
registerNamespace("Pit.Test.ForTests");
registerNamespace("Pit.Test.DelegateTests");
Pit.MonadsTest.failIfBig = function(n) {
    return function(builder) {
        return builder.Delay(function() {
            return (function(thisObject) {
                if (n > 1000) {
                    return builder.ReturnFrom(new Pit.FSharp.Core.FSharpOption1.None());
                } else {
                    return builder.Return(n);
                }
            })(this);
        });
    }(Pit.Monads.get_maybe);
};
Pit.MonadsTest.safesum = function(tupledArg) {
    var inp1 = tupledArg.Item1;
    var inp2 = tupledArg.Item2;
    return function(builder) {
        return builder.Delay(function() {
            return builder.Bind({
                Item1: Pit.MonadsTest.failIfBig(inp1),
                Item2: function(_arg2) {
                    var n1 = _arg2;
                    return builder.Bind({
                        Item1: Pit.MonadsTest.failIfBig(inp2),
                        Item2: function(_arg1) {
                            var n2 = _arg1;
                            var sum = (n1 + n2);
                            return builder.Return(sum);
                        }
                    });
                }
            });
        });
    }(Pit.Monads.get_maybe);
};
Pit.MonadsTest.result1 = function() {
    return Pit.MonadsTest.safesum({
        Item1: 999,
        Item2: 1000
    });
};
Pit.MonadsTest.result2 = function() {
    return Pit.MonadsTest.safesum({
        Item1: 1000,
        Item2: 1001
    });
};
registerNamespace("Pit");
Pit.Monads.MaybeBuilder = (function() {
    function MaybeBuilder() {}
    MaybeBuilder.prototype.Return = function(x) {
        return Pit.Monads.succeed(x);
    };
    MaybeBuilder.prototype.Bind = function(tupledArg) {
        var p = tupledArg.Item1;
        var rest = tupledArg.Item2;
        return Pit.Monads.bind(p)(rest);
    };
    MaybeBuilder.prototype.Delay = function(f) {
        return Pit.Monads.delay(f);
    };
    MaybeBuilder.prototype.Let = function(tupledArg) {
        var p = tupledArg.Item1;
        var rest = tupledArg.Item2;
        return rest(p);
    };
    MaybeBuilder.prototype.ReturnFrom = function(x) {
        return x;
    };
    return MaybeBuilder;
})();
Pit.Monads.succeed = function(x) {
    return new Pit.FSharp.Core.FSharpOption1.Some(x);
};
Pit.Monads.bind = function(p) {
    return function(rest) {
        return (function(thisObject) {
            if (p instanceof Pit.FSharp.Core.FSharpOption1.Some) {
                var r = p.get_Value();
                return rest(r);
            } else {
                return new Pit.FSharp.Core.FSharpOption1.None();
            }
        })(this);
    };
};
Pit.Monads.delay = function(f) {
    return f();
};
Pit.Monads.get_maybe = new Pit.Monads.MaybeBuilder();
Pit.Test.ActivePatternsTest.EvenOdd = function(input) {
    return (function(thisObject) {
        if ((input % 2) == 0) {
            return new Pit.FSharp.Core.FSharpChoice2.Choice1Of2(null);
        } else {
            return new Pit.FSharp.Core.FSharpChoice2.Choice2Of2(null);
        }
    })(this);
};
Pit.Test.ActivePatternsTest.isEven = function(input) {
    var activePatternResult = Pit.Test.ActivePatternsTest.EvenOdd(input);
    return (function(thisObject) {
        if (activePatternResult instanceof Pit.FSharp.Core.FSharpChoice2.Choice2Of2) {
            return false;
        } else {
            return true;
        }
    })(this);
};
Pit.Test.ActivePatternsTest.ActivePatterns = function() {
    var res = Pit.Test.ActivePatternsTest.isEven(2);
    return Assert.AreEqual("Active Pattern", true, res);
};
Pit.Test.ActivePatternsTest.get_err = 1E-10;
Pit.Test.ActivePatternsTest.floatequal = function(x) {
    return function(y) {
        return Pit.FSharp.Core.Operators.Abs((x - y)) < Pit.Test.ActivePatternsTest.get_err;
    };
};
Pit.Test.ActivePatternsTest.Square_ = function(x) {
    return (function(thisObject) {
        if (Pit.Test.ActivePatternsTest.floatequal(Pit.FSharp.Core.Operators.Sqrt(Pit.FSharp.Core.Operators.ToDouble(x)))(Pit.FSharp.Core.Operators.Round(Pit.FSharp.Core.Operators.Sqrt(Pit.FSharp.Core.Operators.ToDouble(x))))) {
            return new Pit.FSharp.Core.FSharpOption1.Some(x);
        } else {
            return new Pit.FSharp.Core.FSharpOption1.None();
        }
    })(this);
};
Pit.Test.ActivePatternsTest.Cube_ = function(x) {
    return (function(thisObject) {
        if (Pit.Test.ActivePatternsTest.floatequal(Pit.FSharp.Core.Operators.op_Exponentiation(Pit.FSharp.Core.Operators.ToDouble(x))((1 / 3)))(Pit.FSharp.Core.Operators.Round(Pit.FSharp.Core.Operators.op_Exponentiation(Pit.FSharp.Core.Operators.ToDouble(x))((1 / 3))))) {
            return new Pit.FSharp.Core.FSharpOption1.Some(x);
        } else {
            return new Pit.FSharp.Core.FSharpOption1.None();
        }
    })(this);
};
Pit.Test.ActivePatternsTest.findSquareCubes = function(x) {
    var activePatternResult = Pit.Test.ActivePatternsTest.Cube_(x);
    return (function() {
        if ((function() {
            return (function(thisObject) {
                var activePatternResult = Pit.Test.ActivePatternsTest.Square_(x);
                if ((function(thisObject) {
                    if (activePatternResult instanceof Pit.FSharp.Core.FSharpOption1.Some) {
                        var x1 = activePatternResult.get_Value();
                        return true;
                    } else {
                        return false;
                    }
                })(this)) {
                    return (function(thisObject) {
                        if (activePatternResult instanceof Pit.FSharp.Core.FSharpOption1.Some) {
                            var x1 = activePatternResult.get_Value();
                            return true;
                        } else {
                            return false;
                        }
                    })(this);
                } else {
                    return false;
                };
            })(this);
        })()) {
            return Assert.AreEqual("Partial Active Patterns", 64, x);
        } else {
            return null;
        };
    })();
};
Pit.Test.ActivePatternsTest.PartialActivePatterns = function() {
    return Pit.Test.ActivePatternsTest.findSquareCubes(64);
};
Pit.Test.RangeEnumeratorTests.Increment = function() {
    var s = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_Range(1)(5)))(function(source) {
        return Pit.FSharp.Collections.ArrayModule.OfSeq(source);
    });
    return Assert.AreEqual("Range Increment", 5, s[4]);
};
Pit.Test.RangeEnumeratorTests.Decrement = function() {
    var s = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_RangeStep(5)(-1)(1)))(function(source) {
        return Pit.FSharp.Collections.ArrayModule.OfSeq(source);
    });
    return Assert.AreEqual("Range Decrement", 1, s[4]);
};
Pit.Test.RangeEnumeratorTests.IncrementTwoWay = function() {
    var s = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_RangeStep(2)(2)(5)))(function(source) {
        return Pit.FSharp.Collections.ArrayModule.OfSeq(source);
    });
    return Assert.AreEqual("Range Increment 2 Way", 4, s[1]);
};
Pit.Test.RangeEnumeratorTests.NestedRanges = function() {
    var s = Pit.FSharp.Core.Operators.op_PipeRight(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
        return Pit.FSharp.Collections.SeqModule.Map(function(i) {
            return Pit.FSharp.Collections.ArrayModule.OfList(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Core.Operators.op_RangeStep(i)(i)(5))));
        })(Pit.FSharp.Core.Operators.op_Range(2)(5));
    })))(function(source) {
        return Pit.FSharp.Collections.SeqModule.ToArray(source);
    });
    var len = s.get_Length();
    Assert.AreEqual("Range Nested Length", 4, len);
    return Assert.AreEqual("Range Nested Value", 5, s[3][0]);
};
registerNamespace("Pit.Test");
Pit.Test.UOMTest.C = (function() {
    function C() {}
    return C;
})();
Pit.Test.UOMTest.F = (function() {
    function F() {}
    return F;
})();
Pit.Test.UOMTest.m = (function() {
    function m() {}
    return m;
})();
Pit.Test.UOMTest.kg = (function() {
    function kg() {}
    return kg;
})();
Pit.Test.UOMTest.to_farenheit = function(x) {
    return ((x * (9 / 5)) + 32);
};
Pit.Test.UOMTest.to_celsius = function(x) {
    return ((x - 32) * (5 / 9));
};
Pit.Test.UOMTest.UOMeasure1 = function() {
    var f = Pit.Test.UOMTest.to_farenheit(20);
    return Assert.AreEqual("Units Of Measure To Farenheit", 68, f);
};
Pit.Test.UOMTest.get_vanillaFloats = new Pit.FSharp.Collections.FSharpList1.Cons(10, new Pit.FSharp.Collections.FSharpList1.Cons(15.5, new Pit.FSharp.Collections.FSharpList1.Cons(17, new Pit.FSharp.Collections.FSharpList1.Empty())));
Pit.Test.UOMTest.get_lengths = Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
    return Pit.FSharp.Collections.SeqModule.Map(function(a) {
        return (a * 1);
    })(new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(7, new Pit.FSharp.Collections.FSharpList1.Cons(14, new Pit.FSharp.Collections.FSharpList1.Cons(5, new Pit.FSharp.Collections.FSharpList1.Empty())))));
})));
Pit.Test.UOMTest.get_masses = Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
    return Pit.FSharp.Collections.SeqModule.Map(function(a) {
        return (a * 1);
    })(new Pit.FSharp.Collections.FSharpList1.Cons(155.54, new Pit.FSharp.Collections.FSharpList1.Cons(179.01, new Pit.FSharp.Collections.FSharpList1.Cons(135.9, new Pit.FSharp.Collections.FSharpList1.Empty()))));
})));
Pit.Test.UOMTest.get_densities = Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
    return Pit.FSharp.Collections.SeqModule.Map(function(a) {
        return (a * 1);
    })(new Pit.FSharp.Collections.FSharpList1.Cons(0.54, new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(1.1, new Pit.FSharp.Collections.FSharpList1.Cons(0.25, new Pit.FSharp.Collections.FSharpList1.Cons(0.7, new Pit.FSharp.Collections.FSharpList1.Empty()))))));
})));
Pit.Test.UOMTest.average = function(l) {
    var patternInput = Pit.FSharp.Core.Operators.op_PipeRight(l)(function(list) {
        return Pit.FSharp.Collections.ListModule.Fold(function(tupledArg) {
            var sum = tupledArg.Item1;
            var count = tupledArg.Item2;
            return function(x) {
                return {
                    Item1: (sum + x),
                    Item2: (count + 1)
                };
            };
        })({
            Item1: 0,
            Item2: 0
        })(list);
    });
    var sum = patternInput.Item1;
    var count = patternInput.Item2;
    return (sum / count);
};
Pit.Test.UOMTest.UOMeasure2 = function() {
    var patternInput = {
        Item1: Pit.Test.UOMTest.average(Pit.Test.UOMTest.get_vanillaFloats),
        Item2: Pit.Test.UOMTest.average(Pit.Test.UOMTest.get_lengths),
        Item3: Pit.Test.UOMTest.average(Pit.Test.UOMTest.get_masses),
        Item4: Pit.Test.UOMTest.average(Pit.Test.UOMTest.get_densities)
    };
    var m = patternInput.Item3;
    var l = patternInput.Item2;
    var f = patternInput.Item1;
    var d = patternInput.Item4;
    Assert.AreEqual("UOM Floats", Pit.FSharp.Core.Operators.op_PipeRight(f)(function(v) {
        return parseInt(v);
    }), 14);
    Assert.AreEqual("UOM Lengths", l, 7);
    Assert.AreEqual("UOM Masses", Pit.FSharp.Core.Operators.op_PipeRight(m)(function(v) {
        return parseInt(v);
    }), 156);
    return Assert.AreEqual("UOM Densities", d, 0.718);
};
registerNamespace("Pit.Test");
registerNamespace("Pit.Test.OperatorOverloadTests.Expression");
Pit.Test.OperatorOverloadTests.t = (function() {
    function t(x, y) {
        this.x = x;
        this.y = y;
    }
    t.prototype.get_x = function() {
        return this.x;
    };
    t.prototype.get_y = function() {
        return this.y;
    };
    return t;
})();
Pit.Test.OperatorOverloadTests.t.op_Addition = function(tupledArg) {
    var t1 = tupledArg.Item1;
    var t2 = tupledArg.Item2;
    return new Pit.Test.OperatorOverloadTests.t((t1.get_x() + t2.get_x()), (t1.get_y() + t2.get_y()));
};
Pit.Test.OperatorOverloadTests.t.op_Subtraction = function(tupledArg) {
    var t1 = tupledArg.Item1;
    var t2 = tupledArg.Item2;
    return new Pit.Test.OperatorOverloadTests.t((t1.get_x() - t2.get_x()), (t1.get_y() - t2.get_y()));
};
Pit.Test.OperatorOverloadTests.t.op_Multiply = function(tupledArg) {
    var t1 = tupledArg.Item1;
    var v = tupledArg.Item2;
    return new Pit.Test.OperatorOverloadTests.t((t1.get_x() * v), (t1.get_y() * v));
};
Pit.Test.OperatorOverloadTests.Expression = function() {
    this.Tag = 0;
    this.IsConstant = false;
    this.IsAdd = false;
};
Pit.Test.OperatorOverloadTests.Expression.Add = function(item1, item2) {
    this.Item1 = item1;
    this.Item2 = item2;
};
Pit.Test.OperatorOverloadTests.Expression.Add.prototype = new Pit.Test.OperatorOverloadTests.Expression();
Pit.Test.OperatorOverloadTests.Expression.Add.prototype.equality = function(compareTo) {
    var result = true;
    result = result && this.get_Item1() == compareTo.get_Item1();
    result = result && this.get_Item2() == compareTo.get_Item2();
    return result;
};
Pit.Test.OperatorOverloadTests.Expression.Add.prototype.get_Item1 = function() {
    return this.Item1;
};
Pit.Test.OperatorOverloadTests.Expression.Add.prototype.get_Item2 = function() {
    return this.Item2;
};
Pit.Test.OperatorOverloadTests.Expression.Constant = function(item) {
    this.Item = item;
};
Pit.Test.OperatorOverloadTests.Expression.Constant.prototype = new Pit.Test.OperatorOverloadTests.Expression();
Pit.Test.OperatorOverloadTests.Expression.Constant.prototype.equality = function(compareTo) {
    var result = true;
    result = result && this.get_Item() == compareTo.get_Item();
    return result;
};
Pit.Test.OperatorOverloadTests.Expression.Constant.prototype.get_Item = function() {
    return this.Item;
};
Pit.Test.OperatorOverloadTests.Expression.prototype.get_Tag = function() {
    return this.Tag;
};
Pit.Test.OperatorOverloadTests.Expression.prototype.get_IsConstant = function() {
    return this instanceof Pit.Test.OperatorOverloadTests.Expression.Constant;
};
Pit.Test.OperatorOverloadTests.Expression.prototype.get_IsAdd = function() {
    return this instanceof Pit.Test.OperatorOverloadTests.Expression.Add;
};
Pit.Test.OperatorOverloadTests.Expression.op_Addition = function(tupledArg) {
    var x = tupledArg.Item1;
    var y = tupledArg.Item2;
    return new Pit.Test.OperatorOverloadTests.Expression.Add(x, y);
};
Pit.Test.OperatorOverloadTests.OpOverload1 = function() {
    var t1 = new Pit.Test.OperatorOverloadTests.t(10, 10);
    var t2 = new Pit.Test.OperatorOverloadTests.t(20, 20);
    var t3 = Pit.Test.OperatorOverloadTests.t.op_Addition({
        Item1: t1,
        Item2: t2
    });
    var t4 = Pit.Test.OperatorOverloadTests.t.op_Subtraction({
        Item1: t1,
        Item2: t2
    });
    var t5 = Pit.Test.OperatorOverloadTests.t.op_Multiply({
        Item1: t1,
        Item2: 10
    });
    Assert.AreEqual("Op Overload 1", t3.get_x(), 30);
    Assert.AreEqual("Op Overload 2", t4.get_x(), -10);
    return Assert.AreEqual("Op Overload 3", t5.get_x(), 100);
};
Pit.Test.OperatorOverloadTests.OpOverload2 = function() {
    var a = new Pit.Test.OperatorOverloadTests.Expression.Constant(10);
    var b = new Pit.Test.OperatorOverloadTests.Expression.Constant(20);
    var c = Pit.Test.OperatorOverloadTests.Expression.op_Addition({
        Item1: a,
        Item2: b
    });
    var res = (function(thisObject) {
        if (c instanceof Pit.Test.OperatorOverloadTests.Expression.Add) {
            return true;
        } else {
            return false;
        }
    })(this);
    return Assert.AreEqual("Union case overload", true, res);
};
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
registerNamespace("Pit.Test");
Pit.Test.TryWithTests.Error1 = (function() {
    __extends(Error1, Pit.Exception);

    function Error1(data0) {
        Error1.__super__.constructor.apply(this, arguments);
        this.Data0 = data0;
    }
    Error1.prototype.get_Data0 = function() {
        return this.Data0;
    };
    return Error1;
})();
Pit.Test.TryWithTests.Error1.__init__ = function(idx, lambda) {
    if (typeof this['ctors'] == 'undefined') {
        this['ctors'] = [];
    }
    var ctors = this['ctors'];
    ctors[idx] = lambda;
}
Pit.Test.TryWithTests.Error2 = (function() {
    __extends(Error2, Pit.Exception);

    function Error2(data0, data1) {
        Error2.__super__.constructor.apply(this, arguments);
        this.Data0 = data0;
        this.Data1 = data1;
    }
    Error2.prototype.get_Data0 = function() {
        return this.Data0;
    };
    Error2.prototype.get_Data1 = function() {
        return this.Data1;
    };
    return Error2;
})();
Pit.Test.TryWithTests.Error2.__init__ = function(idx, lambda) {
    if (typeof this['ctors'] == 'undefined') {
        this['ctors'] = [];
    }
    var ctors = this['ctors'];
    ctors[idx] = lambda;
}
Pit.Test.TryWithTests.TryWith1 = function() {
    var function1 = function(x) {
            return function(y) {
                return (function(thisObject) {
                    try {
                        return (function(thisObject) {
                            if (x == y) {
                                throw new Pit.Test.TryWithTests.Error1("x")
                            } else {
                                throw new Pit.Test.TryWithTests.Error2("x", 10)
                            }
                        })(thisObject);
                    } catch (matchValue) {
                        (function(thisObject) {
                            if (matchValue instanceof Pit.Test.TryWithTests.Error1) {
                                var str = matchValue.get_Data0();
                                return Assert.AreEqual("TryWith Error1", "x", str);
                            } else {
                                return (function(thisObject) {
                                    if (matchValue instanceof Pit.Test.TryWithTests.Error2) {
                                        var i = matchValue.get_Data1();
                                        var str = matchValue.get_Data0();
                                        return Assert.AreEqual("TryWith Error2", 10, i);
                                    } else {
                                        return Pit.FSharp.Core.Operators.Reraise();
                                    }
                                })(thisObject);
                            }
                        })(thisObject)
                    }
                })(this);
            };
        };
    function1(10)(10);
    return function1(10)(20);
};
Pit.Test.WhileTests.WhileDeclare = function() {
    var lookForValue = function(value) {
            return function(maxValue) {
                var continueLooping = true;
                var acc = 0;
                while (continueLooping) {
                    acc = (acc + 1);
                    (function(thisObject) {
                        if (acc < maxValue) {
                            return (function(thisObject) {
                                if (acc == value) {
                                    continueLooping = false;
                                    return Assert.AreEqual("While Loop", acc == value, true);
                                } else {
                                    return null;
                                }
                            })(thisObject);
                        } else {
                            return continueLooping = false;
                        }
                    })(this);
                };
            };
        };
    lookForValue(10)(20);
    return lookForValue(22)(20);
};
registerNamespace("Pit.Test.UnionTests.Shape");
Pit.Test.UnionTests.Shape = function() {
    this.Tag = 0;
    this.IsRectangle = false;
    this.IsSquare = false;
    this.IsEquilateralTriangle = false;
    this.IsCircle = false;
};
Pit.Test.UnionTests.Shape.Circle = function(item) {
    this.Item = item;
};
Pit.Test.UnionTests.Shape.Circle.prototype = new Pit.Test.UnionTests.Shape();
Pit.Test.UnionTests.Shape.Circle.prototype.equality = function(compareTo) {
    var result = true;
    result = result && this.get_Item() == compareTo.get_Item();
    return result;
};
Pit.Test.UnionTests.Shape.Circle.prototype.get_Item = function() {
    return this.Item;
};
Pit.Test.UnionTests.Shape.EquilateralTriangle = function(item) {
    this.Item = item;
};
Pit.Test.UnionTests.Shape.EquilateralTriangle.prototype = new Pit.Test.UnionTests.Shape();
Pit.Test.UnionTests.Shape.EquilateralTriangle.prototype.equality = function(compareTo) {
    var result = true;
    result = result && this.get_Item() == compareTo.get_Item();
    return result;
};
Pit.Test.UnionTests.Shape.EquilateralTriangle.prototype.get_Item = function() {
    return this.Item;
};
Pit.Test.UnionTests.Shape.Square = function(item) {
    this.Item = item;
};
Pit.Test.UnionTests.Shape.Square.prototype = new Pit.Test.UnionTests.Shape();
Pit.Test.UnionTests.Shape.Square.prototype.equality = function(compareTo) {
    var result = true;
    result = result && this.get_Item() == compareTo.get_Item();
    return result;
};
Pit.Test.UnionTests.Shape.Square.prototype.get_Item = function() {
    return this.Item;
};
Pit.Test.UnionTests.Shape.Rectangle = function(item1, item2) {
    this.Item1 = item1;
    this.Item2 = item2;
};
Pit.Test.UnionTests.Shape.Rectangle.prototype = new Pit.Test.UnionTests.Shape();
Pit.Test.UnionTests.Shape.Rectangle.prototype.equality = function(compareTo) {
    var result = true;
    result = result && this.get_Item1() == compareTo.get_Item1();
    result = result && this.get_Item2() == compareTo.get_Item2();
    return result;
};
Pit.Test.UnionTests.Shape.Rectangle.prototype.get_Item1 = function() {
    return this.Item1;
};
Pit.Test.UnionTests.Shape.Rectangle.prototype.get_Item2 = function() {
    return this.Item2;
};
Pit.Test.UnionTests.Shape.prototype.get_Tag = function() {
    return this.Tag;
};
Pit.Test.UnionTests.Shape.prototype.get_IsRectangle = function() {
    return this instanceof Pit.Test.UnionTests.Shape.Rectangle;
};
Pit.Test.UnionTests.Shape.prototype.get_IsSquare = function() {
    return this instanceof Pit.Test.UnionTests.Shape.Square;
};
Pit.Test.UnionTests.Shape.prototype.get_IsEquilateralTriangle = function() {
    return this instanceof Pit.Test.UnionTests.Shape.EquilateralTriangle;
};
Pit.Test.UnionTests.Shape.prototype.get_IsCircle = function() {
    return this instanceof Pit.Test.UnionTests.Shape.Circle;
};
Pit.Test.UnionTests.UnionDeclare = function() {
    var pi = 3.141592654;
    var area = function(myShape) {
            return (function(thisObject) {
                if (myShape instanceof Pit.Test.UnionTests.Shape.EquilateralTriangle) {
                    var s = myShape.get_Item();
                    return (((Pit.FSharp.Core.Operators.Sqrt(3) / 4) * s) * s);
                } else {
                    return (function(thisObject) {
                        if (myShape instanceof Pit.Test.UnionTests.Shape.Square) {
                            var s = myShape.get_Item();
                            return (s * s);
                        } else {
                            return (function(thisObject) {
                                if (myShape instanceof Pit.Test.UnionTests.Shape.Rectangle) {
                                    var w = myShape.get_Item2();
                                    var h = myShape.get_Item1();
                                    return (h * w);
                                } else {
                                    var radius = myShape.get_Item();
                                    return ((pi * radius) * radius);
                                }
                            })(thisObject);
                        }
                    })(thisObject);
                }
            })(this);
        };
    var radius = 15;
    var myCircle = new Pit.Test.UnionTests.Shape.Circle(radius);
    Assert.AreEqual("Union Declare", Pit.FSharp.Core.Operators.op_PipeRight(area(myCircle))(function(value) {
        return Pit.FSharp.Core.Operators.ToInt(value);
    }), 706);
    var squareSide = 10;
    var mySquare = new Pit.Test.UnionTests.Shape.Square(squareSide);
    Assert.AreEqual("Union Declare", area(mySquare), 100);
    var patternInput = {
        Item1: 5,
        Item2: 10
    };
    var width = patternInput.Item2;
    var height = patternInput.Item1;
    var myRectangle = new Pit.Test.UnionTests.Shape.Rectangle(height, width);
    return Assert.AreEqual("Union Declare", area(myRectangle), 50);
};
registerNamespace("Pit.Test");
Pit.Test.TupleTests.tRec = (function() {
    function tRec(p1, p2) {
        this.p1 = p1;
        this.p2 = p2;
    }
    tRec.prototype.get_p1 = function() {
        return this.p1;
    };
    tRec.prototype.get_p2 = function() {
        return this.p2;
    };
    return tRec;
})();
Pit.Test.TupleTests.TupleIgnore = (function() {
    function TupleIgnore() {}
    TupleIgnore.prototype.CallTuple2 = function(s1, s2) {
        return (s1 + s2);
    };
    return TupleIgnore;
})();
Pit.Test.TupleTests.TupleDecalre = function() {
    var patternInput = {
        Item1: 1,
        Item2: 2,
        Item3: 3
    };
    var c = patternInput.Item3;
    var b = patternInput.Item2;
    var a = patternInput.Item1;
    return Assert.AreEqual("Tuple Decalre", a, 1);
};
Pit.Test.TupleTests.TupleFst = function() {
    var r = Pit.FSharp.Core.Operators.Fst({
        Item1: 1,
        Item2: 2
    });
    return Assert.AreEqual("Tuple First(fst)", r, 1);
};
Pit.Test.TupleTests.TupleSnd = function() {
    var r = Pit.FSharp.Core.Operators.Snd({
        Item1: 1,
        Item2: 2
    });
    return Assert.AreEqual("Tuple Second(fst)", r, 2);
};
Pit.Test.TupleTests.MixedTuple = function() {
    var mixedTuple = {
        Item1: 1,
        Item2: "two",
        Item3: 3.3
    };
    var r = mixedTuple.Item2;
    return Assert.AreEqual("Mixed Tuple", r, "two");
};
Pit.Test.TupleTests.TupleFunctions = function() {
    var avg = function(tupledArg) {
            var a = tupledArg.Item1;
            var b = tupledArg.Item2;
            return ((a + b) / 2);
        };
    var r = avg({
        Item1: 5,
        Item2: 5
    });
    return Assert.AreEqual("Functions with Tuple arguements", r, 5);
};
Pit.Test.TupleTests.TupleFunctions2 = function() {
    var scalarMultiply = function(s) {
            return function(tupledArg) {
                var a = tupledArg.Item1;
                var b = tupledArg.Item2;
                return {
                    Item1: (a * s),
                    Item2: (b * s)
                };
            };
        };
    var r = Pit.FSharp.Core.Operators.Fst(scalarMultiply(5)({
        Item1: 1,
        Item2: 2
    }));
    return Assert.AreEqual("Functions with Tuple arguements 2", r, 5);
};
Pit.Test.TupleTests.t1 = function(x) {
    return {
        Item1: x,
        Item2: function(x1) {
            return (x1 + 1);
        }
    };
};
Pit.Test.TupleTests.TupleFunctions3 = function() {
    var r = Pit.FSharp.Core.Operators.Snd(Pit.Test.TupleTests.t1(3))(3);
    return Assert.AreEqual("Tuple with Functions arguements 2", r, 4);
};
Pit.Test.TupleTests.TupleArrays = function() {
    var a = {
        Item1: [1, 2, 3],
        Item2: [4, 5, 6]
    };
    return Assert.AreEqual("Tuple of Arrays", Pit.FSharp.Core.Operators.Fst(a)[0], 1);
};
Pit.Test.TupleTests.TupleRecords = function() {
    var j = new Pit.Test.TupleTests.tRec(5, 5);
    var k = new Pit.Test.TupleTests.tRec(5, 5);
    var tupRec = function(tupledArg) {
            var a = tupledArg.Item1;
            var b = tupledArg.Item2;
            return (((a.get_p1() + a.get_p2()) + b.get_p1()) + b.get_p2());
        };
    var r = tupRec({
        Item1: j,
        Item2: k
    });
    return Assert.AreEqual("Tuple with records", r, 20);
};
Pit.Test.TupleTests.TupleCallAsNormalFunction = function() {
    var a = new Pit.Test.TupleTests.TupleIgnore();
    var s = a.CallTuple2(1, 1);
    return Assert.AreEqual("Tuple Call as Normal Function IgnoreTuple=true", 2, s);
};
registerNamespace("Pit.Test");
Pit.Test.RecordsTests.MyRecord = (function() {
    function MyRecord(x, y, z) {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }
    MyRecord.prototype.get_X = function() {
        return this.X;
    };
    MyRecord.prototype.get_Y = function() {
        return this.Y;
    };
    MyRecord.prototype.get_Z = function() {
        return this.Z;
    };
    return MyRecord;
})();
Pit.Test.RecordsTests.Car = (function() {
    function Car(make, model, odometer) {
        this.Make = make;
        this.Model = model;
        this.Odometer = odometer;
    }
    Car.prototype.set_Odometer = function(x) {
        this.Odometer = x;
    };
    Car.prototype.get_Make = function() {
        return this.Make;
    };
    Car.prototype.get_Model = function() {
        return this.Model;
    };
    Car.prototype.get_Odometer = function() {
        return this.Odometer;
    };
    return Car;
})();
Pit.Test.RecordsTests.Point3D = (function() {
    function Point3D(x, y, z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    Point3D.prototype.get_x = function() {
        return this.x;
    };
    Point3D.prototype.get_y = function() {
        return this.y;
    };
    Point3D.prototype.get_z = function() {
        return this.z;
    };
    return Point3D;
})();
Pit.Test.RecordsTests.Address1 = (function() {
    function Address1(street, pincode) {
        this.street = street;
        this.pincode = pincode;
    }
    Address1.prototype.get_street = function() {
        return this.street;
    };
    Address1.prototype.get_pincode = function() {
        return this.pincode;
    };
    return Address1;
})();
Pit.Test.RecordsTests.Employee2 = (function() {
    function Employee2(id, name, address) {
        this.id = id;
        this.name = name;
        this.address = address;
    }
    Employee2.prototype.get_id = function() {
        return this.id;
    };
    Employee2.prototype.get_name = function() {
        return this.name;
    };
    Employee2.prototype.get_address = function() {
        return this.address;
    };
    return Employee2;
})();
Pit.Test.RecordsTests.CustomPoint = (function() {
    function CustomPoint(x, y) {
        this.x = x;
        this.y = y;
    }
    CustomPoint.prototype.get_x = function() {
        return this.x;
    };
    CustomPoint.prototype.get_y = function() {
        return this.y;
    };
    CustomPoint.prototype.get_xy = function() {
        return (this.get_x() * this.get_y());
    };
    return CustomPoint;
})();
Pit.Test.RecordsTests.RecordDeclare = function() {
    var myRecord1 = new Pit.Test.RecordsTests.MyRecord(1, 2, 3);
    Assert.AreEqual("Record Declare 1", myRecord1.get_X(), 1);
    return Assert.AreEqual("Record Declare 1", myRecord1.get_Y(), 2);
};
Pit.Test.RecordsTests.RecordDeclare2 = function() {
    var myRecord2 = new Pit.Test.RecordsTests.MyRecord(1, 2, 3);
    return Assert.AreEqual("Record Declare 2", myRecord2.get_Y(), 2);
};
Pit.Test.RecordsTests.RecordDeclare3 = function() {
    var myRecord2 = new Pit.Test.RecordsTests.MyRecord(1, 2, 3);
    var myRecord3 = new Pit.Test.RecordsTests.MyRecord(myRecord2.get_X(), 100, 2);
    return Assert.AreEqual("Record Declare 3", myRecord3.get_Y(), 100);
};
Pit.Test.RecordsTests.RecordDeclare4 = function() {
    var myCar = new Pit.Test.RecordsTests.Car("Fabrikam", "Coupe", 108112);
    myCar.set_Odometer((myCar.get_Odometer() + 21));
    return Assert.AreEqual("Record Declare 4", myCar.get_Odometer(), (108112 + 21));
};
Pit.Test.RecordsTests.RecordPatternMatching = function() {
    var evaluatePoint = function(point) {
            return (function(thisObject) {
                if (point.get_x() == 0) {
                    return (function(thisObject) {
                        if (point.get_y() == 0) {
                            return (function(thisObject) {
                                if (point.get_z() == 0) {
                                    return "Point is at the origin.";
                                } else {
                                    return "Point at other location";
                                }
                            })(thisObject);
                        } else {
                            return "Point at other location";
                        }
                    })(thisObject);
                } else {
                    return "Point at other location";
                }
            })(this);
        };
    var r = evaluatePoint(new Pit.Test.RecordsTests.Point3D(0, 0, 0));
    Assert.AreEqual("Record Pattern Matching", r, "Point is at the origin.");
    var r1 = evaluatePoint(new Pit.Test.RecordsTests.Point3D(10, 0, -1));
    return Assert.AreEqual("Record Pattern Matching", r1, "Point at other location");
};
Pit.Test.RecordsTests.RecordJsObjectTest = function() {
    var emp = {
        id: 0,
        name: "Robert"
    };
    Assert.AreEqual("RecordJsObject Test", 0, emp.id);
    return Assert.AreEqual("RecordJsObject Test", "Robert", emp.name);
};
Pit.Test.RecordsTests.RecordJsObjectEqualityTest = function() {
    var emp = {
        id: 1,
        name: "Robert"
    };
    var isEqual = (function(thisObject) {
        if (emp.id == 1) {
            return (function(thisObject) {
                if (emp.name == "Robert") {
                    return true;
                } else {
                    return false;
                }
            })(thisObject);
        } else {
            return false;
        }
    })(this);
    return Assert.AreEqual("Record Equality Test", true, isEqual);
};
Pit.Test.RecordsTests.RecordJsObjectEqualityTest2 = function() {
    var emp1 = {
        id: 1,
        name: "Robert"
    };
    var emp2 = {
        id: 1,
        name: "Robert"
    };
    var res = emp1.equality(emp2);
    return Assert.AreEqual("Record Equality Test2", true, res);
};
Pit.Test.RecordsTests.NestedRecord = function() {
    var man = {
        employee: {
            id: 0,
            name: "Robert",
            address: {
                street: "Green street",
                pincode: 420
            }
        },
        division: "HR"
    };
    var man1 = {
        employee: {
            id: man.employee.id,
            name: man.employee.name,
            address: {
                street: "Red street",
                pincode: 428
            }
        },
        division: man.division
    };
    var man2 = {
        employee: {
            id: 1,
            name: "Dilbert",
            address: man.employee.address
        },
        division: man1.division
    };
    Assert.AreEqual("Manager1 name", man1.employee.name, "Robert");
    Assert.AreEqual("Manager1 address", man1.employee.address.street, "Red street");
    Assert.AreEqual("Manager2 name", man2.employee.name, "Dilbert");
    return Assert.AreEqual("Manager2 address", man2.employee.address.street, "Green street");
};
Pit.Test.RecordsTests.NestedRecordEquality = function() {
    var man1 = {
        employee: {
            id: 0,
            name: "Robert",
            address: {
                street: "Green street",
                pincode: 420
            }
        },
        division: "HR"
    };
    var man2 = {
        employee: {
            id: 0,
            name: "Robert",
            address: {
                street: "Green street",
                pincode: 420
            }
        },
        division: "HR"
    };
    var res = man1.equality(man2);
    return Assert.AreEqual("Nested Record Equality", true, res);
};
Pit.Test.RecordsTests.NestedRecord2 = function() {
    var man = {
        employee: new Pit.Test.RecordsTests.Employee2(0, "Robert", new Pit.Test.RecordsTests.Address1("Green street", 420)),
        division: "HR"
    };
    var man1 = {
        employee: new Pit.Test.RecordsTests.Employee2(man.employee.get_id(), man.employee.get_name(), new Pit.Test.RecordsTests.Address1("Red street", 428)),
        division: man.division
    };
    var man2 = {
        employee: new Pit.Test.RecordsTests.Employee2(1, "Dilbert", man.employee.get_address()),
        division: man1.division
    };
    Assert.AreEqual("Manager1 name", man1.employee.get_name(), "Robert");
    Assert.AreEqual("Manager1 address", man1.employee.get_address().get_street(), "Red street");
    Assert.AreEqual("Manager2 name", man2.employee.get_name(), "Dilbert");
    return Assert.AreEqual("Manager2 address", man2.employee.get_address().get_street(), "Green street");
};
Pit.Test.RecordsTests.RecordExtendedTypeTest = function() {
    var p = new Pit.Test.RecordsTests.CustomPoint(10, 20);
    var xy = p.get_xy();
    return Assert.AreEqual("Record Member XY", xy, 200);
};
registerNamespace("Pit.Test");
registerNamespace("Pit.Test.PatternTests.PersonName");
var Color = {
    Red: {},
    Green: {},
    Blue: {}
};
Pit.Test.PatternTests.PersonName = function() {
    this.Tag = 0;
    this.IsFirstLast = false;
    this.IsLastOnly = false;
    this.IsFirstOnly = false;
};
Pit.Test.PatternTests.PersonName.FirstOnly = function(item) {
    this.Item = item;
};
Pit.Test.PatternTests.PersonName.FirstOnly.prototype = new Pit.Test.PatternTests.PersonName();
Pit.Test.PatternTests.PersonName.FirstOnly.prototype.equality = function(compareTo) {
    var result = true;
    result = result && this.get_Item() == compareTo.get_Item();
    return result;
};
Pit.Test.PatternTests.PersonName.FirstOnly.prototype.get_Item = function() {
    return this.Item;
};
Pit.Test.PatternTests.PersonName.LastOnly = function(item) {
    this.Item = item;
};
Pit.Test.PatternTests.PersonName.LastOnly.prototype = new Pit.Test.PatternTests.PersonName();
Pit.Test.PatternTests.PersonName.LastOnly.prototype.equality = function(compareTo) {
    var result = true;
    result = result && this.get_Item() == compareTo.get_Item();
    return result;
};
Pit.Test.PatternTests.PersonName.LastOnly.prototype.get_Item = function() {
    return this.Item;
};
Pit.Test.PatternTests.PersonName.FirstLast = function(item1, item2) {
    this.Item1 = item1;
    this.Item2 = item2;
};
Pit.Test.PatternTests.PersonName.FirstLast.prototype = new Pit.Test.PatternTests.PersonName();
Pit.Test.PatternTests.PersonName.FirstLast.prototype.equality = function(compareTo) {
    var result = true;
    result = result && this.get_Item1() == compareTo.get_Item1();
    result = result && this.get_Item2() == compareTo.get_Item2();
    return result;
};
Pit.Test.PatternTests.PersonName.FirstLast.prototype.get_Item1 = function() {
    return this.Item1;
};
Pit.Test.PatternTests.PersonName.FirstLast.prototype.get_Item2 = function() {
    return this.Item2;
};
Pit.Test.PatternTests.PersonName.prototype.get_Tag = function() {
    return this.Tag;
};
Pit.Test.PatternTests.PersonName.prototype.get_IsFirstLast = function() {
    return this instanceof Pit.Test.PatternTests.PersonName.FirstLast;
};
Pit.Test.PatternTests.PersonName.prototype.get_IsLastOnly = function() {
    return this instanceof Pit.Test.PatternTests.PersonName.LastOnly;
};
Pit.Test.PatternTests.PersonName.prototype.get_IsFirstOnly = function() {
    return this instanceof Pit.Test.PatternTests.PersonName.FirstOnly;
};
Pit.Test.PatternTests.ConstantMatchTest = function() {
    var filter123 = function(x) {
            return (function(thisObject) {
                if (x == 1) {
                    return Assert.AreEqual("Constant Match Test", true, (function(thisObject) {
                        if (x < 4) {
                            return x > 0;
                        } else {
                            return false;
                        }
                    })(thisObject));
                } else {
                    return (function(thisObject) {
                        if (x == 2) {
                            return Assert.AreEqual("Constant Match Test", true, (function(thisObject) {
                                if (x < 4) {
                                    return x > 0;
                                } else {
                                    return false;
                                }
                            })(thisObject));
                        } else {
                            return (function(thisObject) {
                                if (x == 3) {
                                    return Assert.AreEqual("Constant Match Test", true, (function(thisObject) {
                                        if (x < 4) {
                                            return x > 0;
                                        } else {
                                            return false;
                                        }
                                    })(thisObject));
                                } else {
                                    return null;
                                }
                            })(thisObject);
                        }
                    })(thisObject);
                }
            })(this);
        };
    for (var x = 1; x <= 10; x++) {
        (function(thisObject, x) {
            filter123(x)
        })(this, x);
    };
};
Pit.Test.PatternTests.ConstantMatchTest2 = function() {
    var printColorName = function(color) {
            return (function(thisObject) {
                if (color == Color.Red) {
                    return Assert.AreEqual("Constant Match Test 2", color, Color.Red);
                } else {
                    return (function(thisObject) {
                        if (color == Color.Green) {
                            return Assert.AreEqual("Constant Match Test 2", color, Color.Green);
                        } else {
                            return (function(thisObject) {
                                if (color == Color.Blue) {
                                    return Assert.AreEqual("Constant Match Test 2", color, Color.Blue);
                                } else {
                                    return null;
                                }
                            })(thisObject);
                        }
                    })(thisObject);
                }
            })(this);
        };
    printColorName(Color.Red);
    printColorName(Color.Green);
    return printColorName(Color.Blue);
};
Pit.Test.PatternTests.IdentifierPattern = function() {
    var constructQuery = function(personName) {
            return (function(thisObject) {
                if (personName instanceof Pit.Test.PatternTests.PersonName.LastOnly) {
                    var lastName = personName.get_Item();
                    return "last";
                } else {
                    return (function(thisObject) {
                        if (personName instanceof Pit.Test.PatternTests.PersonName.FirstLast) {
                            var lastName = personName.get_Item2();
                            var firstName = personName.get_Item1();
                            return "firstlast";
                        } else {
                            var firstName = personName.get_Item();
                            return "first";
                        }
                    })(thisObject);
                }
            })(this);
        };
    var r = constructQuery(new Pit.Test.PatternTests.PersonName.FirstOnly("Steve"));
    Assert.AreEqual("Identifier Pattern Test", r, "first");
    var r1 = constructQuery(new Pit.Test.PatternTests.PersonName.LastOnly("Jobs"));
    Assert.AreEqual("Identifier Pattern Test", r1, "last");
    var r2 = constructQuery(new Pit.Test.PatternTests.PersonName.FirstLast("John", "Jobs"));
    return Assert.AreEqual("Identifier Pattern Test", r2, "firstlast");
};
Pit.Test.PatternTests.function1 = function(x) {
    var var1 = x.Item1;
    var var2 = x.Item2;
    return (function() {
        return (function(thisObject) {
            var var2 = x.Item2;
            var var1 = x.Item1;
            if (var1 > var2) {
                return var2;
            } else {
                var var1 = x.Item1;
                var var2 = x.Item2;
                return (function() {
                    return (function(thisObject) {
                        var var2 = x.Item2;
                        var var1 = x.Item1;
                        if (var1 < var2) {
                            return var2;
                        } else {
                            var var2 = x.Item2;
                            var var1 = x.Item1;
                            return var1;
                        };
                    })(thisObject);
                })();
            };
        })(this);
    })();
};
Pit.Test.PatternTests.VariablePatternTest = function() {
    var r1 = Pit.Test.PatternTests.function1({
        Item1: 1,
        Item2: 2
    });
    Assert.AreEqual("Variable Pattern Test", r1, 2);
    var r2 = Pit.Test.PatternTests.function1({
        Item1: 2,
        Item2: 1
    });
    Assert.AreEqual("Identifier Pattern Test", r2, 1);
    var r3 = Pit.Test.PatternTests.function1({
        Item1: 0,
        Item2: 0
    });
    return Assert.AreEqual("Identifier Pattern Test", r3, 0);
};
Pit.Test.PatternTests.VariablePatternTest2 = function() {
    var sliceMiddle = 0.4;
    var x = sliceMiddle;
    return (function() {
        return (function(thisObject) {
            var x = sliceMiddle;
            if (x <= 0.25) {
                return null;
            } else {
                var x = sliceMiddle;
                return (function() {
                    return (function(thisObject) {
                        var x = sliceMiddle;
                        if ((function(thisObject) {
                            if (x > 0.25) {
                                return x <= 0.5;
                            } else {
                                return false;
                            }
                        })(thisObject)) {
                            return Assert.AreEqual("SliceMiddle", 0.4, sliceMiddle);
                        } else {
                            return null;
                        };
                    })(thisObject);
                })();
            };
        })(this);
    })();
};
Pit.Test.PatternTests.AsPatternTest = function() {
    var tuple1 = {
        Item1: 1,
        Item2: 2
    };
    var var2 = tuple1.Item2;
    var var1 = tuple1.Item1;
    Assert.AreEqual("As Pattern Test", var1, 1);
    return Assert.AreEqual("As Pattern Test", var2, 2);
};
Pit.Test.PatternTests.OrPatternTest = function() {
    var detectZeroOR = function(point) {
            return (function(thisObject) {
                if (point.Item1 == 0) {
                    return (function(thisObject) {
                        if (point.Item2 == 0) {
                            return "Zero found.";
                        } else {
                            return "Zero found.";
                        }
                    })(thisObject);
                } else {
                    return (function(thisObject) {
                        if (point.Item2 == 0) {
                            return "Zero found.";
                        } else {
                            return "Both nonzero.";
                        }
                    })(thisObject);
                }
            })(this);
        };
    var r1 = detectZeroOR({
        Item1: 0,
        Item2: 0
    });
    Assert.AreEqual("Or Pattern Test", r1, "Zero found.");
    var r2 = detectZeroOR({
        Item1: 1,
        Item2: 0
    });
    Assert.AreEqual("Or Pattern Test", r2, "Zero found.");
    var r3 = detectZeroOR({
        Item1: 0,
        Item2: 10
    });
    Assert.AreEqual("Or Pattern Test", r3, "Zero found.");
    var r4 = detectZeroOR({
        Item1: 10,
        Item2: 15
    });
    return Assert.AreEqual("Or Pattern Test", r4, "Both nonzero.");
};
Pit.Test.PatternTests.AndPatternTest = function() {
    var detectZeroAND = function(point) {
            return (function(thisObject) {
                if (point.Item1 == 0) {
                    return (function(thisObject) {
                        if (point.Item2 == 0) {
                            return "Both values zero.";
                        } else {
                            return (function(thisObject) {
                                if (point.Item1 == 0) {
                                    var var1 = point.Item1;
                                    var var2 = point.Item2;
                                    return ("nonzero " + var2.ToString());
                                } else {
                                    return (function(thisObject) {
                                        if (point.Item2 == 0) {
                                            var var1 = point.Item1;
                                            var var2 = point.Item2;
                                            return ("nonzero " + var1.ToString());
                                        } else {
                                            return "Both nonzero.";
                                        }
                                    })(thisObject);
                                }
                            })(thisObject);
                        }
                    })(thisObject);
                } else {
                    return (function(thisObject) {
                        if (point.Item1 == 0) {
                            var var1 = point.Item1;
                            var var2 = point.Item2;
                            return ("nonzero " + var2.ToString());
                        } else {
                            return (function(thisObject) {
                                if (point.Item2 == 0) {
                                    var var1 = point.Item1;
                                    var var2 = point.Item2;
                                    return ("nonzero " + var1.ToString());
                                } else {
                                    return "Both nonzero.";
                                }
                            })(thisObject);
                        }
                    })(thisObject);
                }
            })(this);
        };
    var r1 = detectZeroAND({
        Item1: 0,
        Item2: 0
    });
    Assert.AreEqual("And Pattern Test", r1, "Both values zero.");
    var r2 = detectZeroAND({
        Item1: 1,
        Item2: 0
    });
    Assert.AreEqual("And Pattern Test", r2, "nonzero 1");
    var r3 = detectZeroAND({
        Item1: 0,
        Item2: 10
    });
    Assert.AreEqual("And Pattern Test", r3, "nonzero 10");
    var r4 = detectZeroAND({
        Item1: 10,
        Item2: 15
    });
    return Assert.AreEqual("And Pattern Test", r4, "Both nonzero.");
};
Pit.Test.PatternTests.ConsPatternTest = function() {
    var list1 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(2, new Pit.FSharp.Collections.FSharpList1.Cons(3, new Pit.FSharp.Collections.FSharpList1.Cons(4, new Pit.FSharp.Collections.FSharpList1.Empty()))));
    var printList = function(l) {
            return (function(thisObject) {
                if (l instanceof Pit.FSharp.Collections.FSharpList1.Empty) {
                    return null;
                } else {
                    var tail = l.get_Tail();
                    var head = l.get_Head();
                    Assert.AreEqual("Cons Pattern test", true, head <= 4);
                    return printList(tail);
                }
            })(this);
        };
    return printList(list1);
};
Pit.Test.PatternTests.listLength = function(list) {
    return (function(thisObject) {
        if (list instanceof Pit.FSharp.Collections.FSharpList1.Cons) {
            return (function(thisObject) {
                if (list.get_Tail() instanceof Pit.FSharp.Collections.FSharpList1.Cons) {
                    return (function(thisObject) {
                        if (list.get_Tail().get_Tail() instanceof Pit.FSharp.Collections.FSharpList1.Cons) {
                            return (function(thisObject) {
                                if (list.get_Tail().get_Tail().get_Tail() instanceof Pit.FSharp.Collections.FSharpList1.Empty) {
                                    return 3;
                                } else {
                                    return Pit.FSharp.Collections.ListModule.Length(list);
                                }
                            })(thisObject);
                        } else {
                            return 2;
                        }
                    })(thisObject);
                } else {
                    return 1;
                }
            })(thisObject);
        } else {
            return 0;
        }
    })(this);
};
Pit.Test.PatternTests.ListPatternTest = function() {
    Assert.AreEqual("List Pattern test 1", Pit.Test.PatternTests.listLength(new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Empty())), 1);
    Assert.AreEqual("List Pattern test 2", Pit.Test.PatternTests.listLength(new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Empty()))), 2);
    return Assert.AreEqual("List Pattern test 3", Pit.Test.PatternTests.listLength(new Pit.FSharp.Collections.FSharpList1.Empty()), 0);
};
Pit.Test.PatternTests.countValues = function(list) {
    return function(value) {
        var checkList = function(list1) {
                return function(acc) {
                    return (function(thisObject) {
                        if (list1 instanceof Pit.FSharp.Collections.FSharpList1.Cons) {
                            var elem1 = list1.get_Head();
                            var head = list1.get_Head();
                            var tail = list1.get_Tail();
                            return (function() {
                                return (function(thisObject) {
                                    var elem1 = list1.get_Head();
                                    var head = list1.get_Head();
                                    var tail = list1.get_Tail();
                                    if (elem1 == value) {
                                        return checkList(tail)((acc + 1));
                                    } else {
                                        return (function(thisObject) {
                                            if (list1 instanceof Pit.FSharp.Collections.FSharpList1.Empty) {
                                                return acc;
                                            } else {
                                                var tail = list1.get_Tail();
                                                var head = list1.get_Head();
                                                return checkList(tail)(acc);
                                            }
                                        })(thisObject);
                                    };
                                })(thisObject);
                            })();
                        } else {
                            return (function(thisObject) {
                                if (list1 instanceof Pit.FSharp.Collections.FSharpList1.Empty) {
                                    return acc;
                                } else {
                                    var tail = list1.get_Tail();
                                    var head = list1.get_Head();
                                    return checkList(tail)(acc);
                                }
                            })(thisObject);
                        }
                    })(this);
                };
            };
        return checkList(list)(0);
    };
};
Pit.Test.PatternTests.ParanthesizedPatternTest = function() {
    var result = Pit.Test.PatternTests.countValues(Pit.FSharp.Collections.SeqModule.ToList(Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
        return Pit.FSharp.Collections.SeqModule.Map(function(x) {
            return ((x * x) - 4);
        })(Pit.FSharp.Core.Operators.op_Range(-10)(10));
    }))))(0);
    return Assert.AreEqual("Array Pattern test", result, 2);
};
Pit.Test.PatternTests.TuplePatternTest = function() {
    var detectZeroTuple = function(point) {
            return (function(thisObject) {
                if (point.Item1 == 0) {
                    return (function(thisObject) {
                        if (point.Item2 == 0) {
                            return "Both values zero.";
                        } else {
                            var var2 = point.Item2;
                            return "First value zero";
                        }
                    })(thisObject);
                } else {
                    return (function(thisObject) {
                        if (point.Item2 == 0) {
                            var var1 = point.Item1;
                            return "Second value zero";
                        } else {
                            return "Both nonzero.";
                        }
                    })(thisObject);
                }
            })(this);
        };
    var r1 = detectZeroTuple({
        Item1: 0,
        Item2: 0
    });
    Assert.AreEqual("Tuple Pattern test", r1, "Both values zero.");
    var r2 = detectZeroTuple({
        Item1: 1,
        Item2: 0
    });
    Assert.AreEqual("Tuple Pattern test", r2, "Second value zero");
    var r3 = detectZeroTuple({
        Item1: 0,
        Item2: 10
    });
    Assert.AreEqual("Tuple Pattern test", r3, "First value zero");
    var r4 = detectZeroTuple({
        Item1: 10,
        Item2: 15
    });
    return Assert.AreEqual("Tuple Pattern test", r4, "Both nonzero.");
};
Pit.Test.PatternTests.WildCardPatternTest = function() {
    var detect1 = function(x) {
            return (function(thisObject) {
                if (x == 1) {
                    return "Found";
                } else {
                    var var1 = x;
                    return "NotFound";
                }
            })(this);
        };
    var r1 = detect1(0);
    Assert.AreEqual("WildCard Pattern test", r1, "NotFound");
    var r2 = detect1(1);
    return Assert.AreEqual("WildCard Pattern test", r2, "Found");
};
Pit.Test.PatternTests.MultiplePatternTest = function() {
    var function1 = function(x) {
            return function(y) {
                var var1 = x.Item1;
                var var2 = x.Item2;
                return (function() {
                    return (function(thisObject) {
                        var var2 = x.Item2;
                        var var1 = x.Item1;
                        var var12 = y.Item1;
                        var var21 = y.Item2;
                        if (var1 > var2) {
                            return (function() {
                                return (function(thisObject) {
                                    var var21 = y.Item2;
                                    var var12 = y.Item1;
                                    if (var12 < var21) {
                                        return true;
                                    } else {
                                        return false;
                                    };
                                })(this);
                            })();
                        } else {
                            return false;
                        };
                    })(this);
                })();
            };
        };
    var r = function1({
        Item1: 3,
        Item2: 2
    })({
        Item1: 3,
        Item2: 5
    });
    return Assert.AreEqual("Multiple Patterns Test", true, r);
};
registerNamespace("Pit.Test");
registerNamespace("Pit.Test.LetTests.Test");
Pit.Test.LetTests.t = (function() {
    function t(left) {
        this.left = left;
    }
    t.prototype.set_left = function(x) {
        this.left = x;
    };
    t.prototype.get_left = function() {
        return this.left;
    };
    return t;
})();
Pit.Test.LetTests.LetDeclare = function() {
    var x = 1;
    return Assert.AreEqual("Let Declare 1", x, 1);
};
Pit.Test.LetTests.Let = function() {
    var f = function(x) {
            return (x + 1);
        };
    return Assert.AreEqual("Let Declare 2", f(1), 2);
};
Pit.Test.LetTests.Let3 = function() {
    var cylinderVolume = function(radius) {
            return function(length) {
                var pi = 3.14159;
                return (((length * pi) * radius) * radius);
            };
        };
    var vol = cylinderVolume(2)(3);
    return Assert.AreEqual("Let Declare 3", Pit.FSharp.Core.Operators.op_PipeRight(vol)(function(value) {
        return Pit.FSharp.Core.Operators.ToInt(value);
    }), 37);
};
Pit.Test.LetTests.LetRecursive = function() {
    var fib = function(n) {
            return (function(thisObject) {
                if (n < 2) {
                    return 1;
                } else {
                    return (fib((n - 1)) + fib((n - 2)));
                }
            })(this);
        };
    return Assert.AreEqual("Let Recursive 1", fib(10), 89);
};
Pit.Test.LetTests.LetRecursive2 = function() {
    var Even = function(x) {
            return (function(thisObject) {
                if (x == 0) {
                    return true;
                } else {
                    return Odd((x - 1));
                }
            })(this);
        };
    var Odd = function(x) {
            return (function(thisObject) {
                if (x == 1) {
                    return true;
                } else {
                    return Even((x - 1));
                }
            })(this);
        };
    var e = Even(2);
    Assert.AreEqual("Let Recursive 2", e, true);
    var o = Odd(3);
    return Assert.AreEqual("Let Recursive 2", o, true);
};
Pit.Test.LetTests.LetFunctionValues = function() {
    var apply1 = function(transform) {
            return function(y) {
                return transform(y);
            };
        };
    var increment = function(x) {
            return (x + 1);
        };
    var result1 = apply1(increment)(100);
    return Assert.AreEqual("Let Function Values", result1, 101);
};
Pit.Test.LetTests.LetLambdaFunctions = function() {
    var apply1 = function(transform) {
            return function(y) {
                return transform(y);
            };
        };
    var result3 = apply1(function(x) {
        return (x + 1);
    })(100);
    return Assert.AreEqual("Let Lambda Fucntions", result3, 101);
};
Pit.Test.LetTests.LetFunctionComposition = function() {
    var function1 = function(x) {
            return (x + 1);
        };
    var function2 = function(x) {
            return (x * 2);
        };
    var h = Pit.FSharp.Core.Operators.op_ComposeRight(function1)(function2);
    var result5 = h(100);
    return Assert.AreEqual("Let Function Composition", result5, 202);
};
Pit.Test.LetTests.LetTuple = function() {
    var patternInput = {
        Item1: 1,
        Item2: 2,
        Item3: 3
    };
    var k = patternInput.Item3;
    var j = patternInput.Item2;
    var i = patternInput.Item1;
    Assert.AreEqual("Let Tuple 1", i, 1);
    Assert.AreEqual("Let Tuple 1", j, 2);
    return Assert.AreEqual("Let Tuple 1", k, 3);
};
Pit.Test.LetTests.LetTuple2 = function() {
    var function2 = function(tupledArg) {
            var a = tupledArg.Item1;
            var b = tupledArg.Item2;
            return (a + b);
        };
    var f = function2({
        Item1: 10,
        Item2: 10
    });
    return Assert.AreEqual("Let Tuple 2", f, 20);
};
Pit.Test.LetTests.LetMutable = function() {
    var x = 0;
    Assert.AreEqual("Let Mutable", x, 0);
    x = (x + 1);
    return Assert.AreEqual("Let Mutable", x, 1);
};
Pit.Test.LetTests.LetMutableSetter = function() {
    var t = new Pit.Test.LetTests.t("10");
    var x = 20;
    t.set_left((function() {
        var copyOfStruct = Pit.FSharp.Core.Operators.ToDouble(x);
        return copyOfStruct.ToString();
    })());
    return Assert.AreEqual("Let Mutable Setter", t.get_left(), "20");
};
Pit.Test.LetTests.LetMutableSetterInModule = function() {
    Pit.Test.LetTests.Test.get_v = 10;
    return Assert.AreEqual("Let Mutable Setter in Module", Pit.Test.LetTests.Test.get_v, 10);
};
Pit.Test.ForTests.ForSimple = function() {
    var acc = 0;
    for (var i = 1; i <= 3; i++) {
        (function(thisObject, i) {
            acc = (acc + 1);
            Assert.AreEqual("For Simple", acc, i);
        })(this, i);
    };
};
Pit.Test.ForTests.ForFunctions = function() {
    var beginning = function(x) {
            return function(y) {
                return (x - (2 * y));
            };
        };
    var ending = function(x) {
            return function(y) {
                return (x + (2 * y));
            };
        };
    var function3 = function(x) {
            return function(y) {
                var acc = 1;
                for (var i = beginning(x)(y); i <= ending(x)(y); i++) {
                    (function(thisObject, i) {
                        acc = (acc + 1);
                        Assert.AreEqual("For Functions", acc, i);
                    })(this, i);
                };
            };
        };
    return function3(10)(4);
};
Pit.Test.ForTests.ForInDeclare = function() {
    var count = 0;
    var list1 = new Pit.FSharp.Collections.FSharpList1.Cons(1, new Pit.FSharp.Collections.FSharpList1.Cons(5, new Pit.FSharp.Collections.FSharpList1.Cons(100, new Pit.FSharp.Collections.FSharpList1.Cons(450, new Pit.FSharp.Collections.FSharpList1.Cons(788, new Pit.FSharp.Collections.FSharpList1.Empty())))));
    var enumerator = list1.IEnumerable1_GetEnumerator();
    (function(thisObject) {
        try {
            while (enumerator.IEnumerator_MoveNext()) {
                var forLoopVar = enumerator.IEnumerator1_get_Current();
                count = (count + 1);
            }
        } finally {
            (function(thisObject) {
                if (enumerator.containsInterface("__getIDisposable")) {
                    return enumerator.IDisposable_Dispose();
                } else {
                    return null;
                }
            })(thisObject)
        }
    })(this);
    return Assert.AreEqual("For In Declare 1", list1.get_Length(), count);
};
Pit.Test.ForTests.ForInDeclare2 = function() {
    var seq1 = Pit.FSharp.Core.Operators.CreateSequence(Pit.FSharp.Collections.SeqModule.Delay(function() {
        return Pit.FSharp.Collections.SeqModule.Map(function(i) {
            return {
                Item1: i,
                Item2: (i * i)
            };
        })(Pit.FSharp.Core.Operators.op_Range(1)(10));
    }));
    var enumerator = seq1.IEnumerable1_GetEnumerator();
    return (function(thisObject) {
        try {
            while (enumerator.IEnumerator_MoveNext()) {
                var forLoopVar = enumerator.IEnumerator1_get_Current();
                var asqr = forLoopVar.Item2;
                var a = forLoopVar.Item1;
                Assert.AreEqual("For In Declare 2", (a * a), asqr);
            }
        } finally {
            (function(thisObject) {
                if (enumerator.containsInterface("__getIDisposable")) {
                    return enumerator.IDisposable_Dispose();
                } else {
                    return null;
                }
            })(thisObject)
        }
    })(this);
};
Pit.Test.DelegateTests.Declare1 = function() {
    var f1 = function(tupledArg) {
            var a = tupledArg.Item1;
            var b = tupledArg.Item2;
            return (a + b);
        };
    var r = f1({
        Item1: 1,
        Item2: 2
    });
    return Assert.AreEqual("Delegate Declare1 Test", r, 3);
};
Pit.Test.DelegateTests.Declare2 = function() {
    var f1 = function(a, b) {
            return (a + b);
        };
    var r = f1(1, 2);
    return Assert.AreEqual("Delegate Declare2 Test", r, 3);
};
