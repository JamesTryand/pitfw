registerNamespace("Pit.Test.FSStringsTest");
registerNamespace("Pit.Test.RegexTest");
registerNamespace("Pit.Test.MathTest");
registerNamespace("Pit.Test.StringMapTest");
registerNamespace("Pit.Test.JsStringTest");
registerNamespace("Pit.Test.JsArrayTest");
registerNamespace("Pit.Test.DateTest");
Pit.Test.FSStringsTest.Collect = function() {
    var spaceOut = function(input) {
            return Pit.FSharp.Core.StringModule.Collect(function(c) {
                return (c.ToString() + " ");
            })(input);
        };
    var res = spaceOut("Hi");
    return Assert.AreEqual("StringModule Collect", "H i ", res);
};
Pit.Test.FSStringsTest.Exists = function() {
    var containsUpperCase = function(str) {
            return (function(thisObject) {
                if (Pit.FSharp.Core.StringModule.Exists(function(c) {
                    return c == "e";
                })(str)) {
                    return true;
                } else {
                    return false;
                }
            })(this);
        };
    var res = containsUpperCase("Hello");
    return Assert.AreEqual("StringModule Exists", true, res);
};
Pit.Test.FSStringsTest.ForAll = function() {
    var isAllS = function(str) {
            return (function(thisObject) {
                if (Pit.FSharp.Core.StringModule.ForAll(function(c) {
                    return c == "s";
                })(str)) {
                    return true;
                } else {
                    return false;
                }
            })(this);
        };
    var res1 = isAllS("ssss");
    var res2 = isAllS("hhis");
    Assert.AreEqual("StringModule Forall", true, res1);
    return Assert.AreEqual("StringModule Forall", false, res2);
};
Pit.Test.FSStringsTest.Init = function() {
    var res = Pit.FSharp.Core.StringModule.Initialize(5)(function(i) {
        return i.ToString();
    });
    return Assert.AreEqual("StringModule Init", "01234", res);
};
Pit.Test.FSStringsTest.Iterate = function() {
    var str = "HE";
    var ch = "H";
    return Pit.FSharp.Core.Operators.op_PipeRight(str)(function(str1) {
        return Pit.FSharp.Core.StringModule.Iterate(function(c) {
            Assert.AreEqual("StringModule Iterate", ch, c);
            return ch = "E";
        })(str1);
    });
};
Pit.Test.FSStringsTest.IterateIndex = function() {
    var str = "HE";
    return Pit.FSharp.Core.Operators.op_PipeRight(str)(function(str1) {
        return Pit.FSharp.Core.StringModule.IterateIndexed(function(i) {
            return function(c) {
                return Assert.AreEqual("StringModule IterateIndex", c.ToString(), Pit.FSharp.Core.LanguagePrimitives.IntrinsicFunctions.GetString(str1)(i));
            };
        })(str1);
    });
};
Pit.Test.FSStringsTest.Length = function() {
    var str = Pit.FSharp.Core.Operators.op_PipeRight("HELLO")(function(str) {
        return Pit.FSharp.Core.StringModule.Length(str);
    });
    return Assert.AreEqual("StringModule length", 5, str);
};
Pit.Test.FSStringsTest.Map = function() {
    var str = "hello";
    var res = Pit.FSharp.Core.Operators.op_PipeRight(str)(function(str1) {
        return Pit.FSharp.Core.StringModule.Map(function(c) {
            return Pit.FSharp.Core.Operators.op_PipeRight((Pit.FSharp.Core.Operators.ToInt(c) + 5))(function(value) {
                return Pit.FSharp.Core.Operators.ToChar(value);
            });
        })(str1);
    });
    return Assert.AreEqual("StringModule Map", "mjqqt", res);
};
Pit.Test.FSStringsTest.Replicate = function() {
    var str = "XO";
    var res = Pit.FSharp.Core.Operators.op_PipeRight(str)(function(str1) {
        return Pit.FSharp.Core.StringModule.Replicate(5)(str1);
    });
    return Assert.AreEqual("StringModule Replicate", "XOXOXOXOXO", res);
};
Pit.Test.RegexTest.Create = function() {
    var str = "Every man in the world! Every woman on earth!";
    var r = new RegExp("man", "g");
    var jsStr = str;
    var str2 = jsStr.replace(r, "person");
    return Assert.AreEqual("Regex Create", str2, "Every person in the world! Every woperson on earth!");
};
Pit.Test.MathTest.PI = function() {
    var p1 = Math.PI;
    return Assert.IsNotNull("PI", p1);
};
Pit.Test.MathTest.abs = function() {
    var a = Math.abs(-30);
    return Assert.AreEqual("Abs", 30, 30);
};
Pit.Test.MathTest.TestAll = function() {
    Pit.Test.MathTest.PI();
    return Pit.Test.MathTest.abs();
};
Pit.Test.StringMapTest.Length = function() {
    var s = "Hello World";
    var len = s.get_Length();
    return Assert.AreEqual("String Map length", len, 11);
};
Pit.Test.StringMapTest.GetCharAt = function() {
    var s = "Hello World";
    var c = Pit.FSharp.Core.LanguagePrimitives.IntrinsicFunctions.GetString(s)(3);
    return Assert.AreEqual("String Map GetCharAt", "l", c);
};
Pit.Test.StringMapTest.Substring1 = function() {
    var s = "Hello World";
    var s1 = s.Substring(1, 4);
    return Assert.AreEqual("String Map Substring1", "ello", s1);
};
Pit.Test.StringMapTest.Substring2 = function() {
    var s = "Hello World";
    var s1 = s.Substring(4);
    return Assert.AreEqual("String Map Substring2", "o World", s1);
};
Pit.Test.StringMapTest.ContainsPass = function() {
    var s = "Hello";
    var r = s.Contains("e");
    return Assert.AreEqual("String Map Contains Pass", true, r);
};
Pit.Test.StringMapTest.ContainsFail = function() {
    var s = "Hello";
    var r = s.Contains("r");
    return Assert.AreEqual("String Map Contains Fail", false, r);
};
Pit.Test.StringMapTest.EndsWith = function() {
    var s = "Hello";
    var r = s.EndsWith("o");
    return Assert.AreEqual("String Map EndsWith", true, r);
};
Pit.Test.StringMapTest.Equals = function() {
    var s = "Hello";
    var s1 = "Hello";
    var r = s.Equals(s1);
    return Assert.AreEqual("String Map Equals", true, r);
};
Pit.Test.StringMapTest.IndexOf1 = function() {
    var s = "Hello";
    var i = s.IndexOf("l");
    return Assert.AreEqual("String Map IndexOf", 2, i);
};
Pit.Test.StringMapTest.IndexOf2 = function() {
    var s = "Hello";
    var i = s.IndexOf("o", 3);
    return Assert.AreEqual("String Map IndexOf", 4, i);
};
Pit.Test.StringMapTest.LastIndexOf = function() {
    var s = "Hello";
    var i = s.LastIndexOf("l");
    return Assert.AreEqual("String Map LastIndexOf", 3, i);
};
Pit.Test.StringMapTest.Replace = function() {
    var s = "Hello";
    var s1 = s.Replace("l", "w");
    return Assert.AreEqual("String Map Replace", "Hewwo", s1);
};
Pit.Test.StringMapTest.Split = function() {
    var s = "Hello World";
    var s1 = s.Split([" "]);
    return Assert.AreEqual("String Map Split", 2, s1.get_Length());
};
Pit.Test.StringMapTest.ToLower = function() {
    var s = "Hello World";
    var r = s.ToLower();
    return Assert.AreEqual("String Map ToLower", "hello world", r);
};
Pit.Test.StringMapTest.ToUpper = function() {
    var s = "Hello World";
    var r = s.ToUpper();
    return Assert.AreEqual("String Map ToUpper", "HELLO WORLD", r);
};
Pit.Test.JsStringTest.Create = function() {
    return "Hello";
};
Pit.Test.JsStringTest.Length = function() {
    var s = Pit.Test.JsStringTest.Create();
    return Assert.AreEqual("String Length", s.length, 5);
};
Pit.Test.JsStringTest.CharAt = function() {
    var s = Pit.Test.JsStringTest.Create();
    var c = s.charAt(1);
    return Assert.AreEqual("String CharAt", "e", c);
};
Pit.Test.JsStringTest.CharCodeAt = function() {
    var s = Pit.Test.JsStringTest.Create();
    var cc = s.charCodeAt(1);
    return Assert.AreEqual("String CharCodeAt", 101, cc);
};
Pit.Test.JsStringTest.Concat = function() {
    var s = Pit.Test.JsStringTest.Create();
    var js = s.concat(" World");
    return Assert.AreEqual("String Concat", "Hello World", js.ToString());
};
Pit.Test.JsStringTest.IndexOf = function() {
    var s = Pit.Test.JsStringTest.Create();
    var i = s.indexOf("o");
    Assert.AreEqual("String IndexOf", 4, i);
    var i1 = s.indexOf("HELLO");
    return Assert.AreEqual("String IndexOf", -1, i1);
};
Pit.Test.JsStringTest.LastIndexOf = function() {
    var s = Pit.Test.JsStringTest.Create();
    var i = s.lastIndexOf("l");
    return Assert.AreEqual("String LastIndexOf", 3, i);
};
Pit.Test.JsStringTest.Match = function() {
    var s = "The rain in SPAIN stays mainly in the plain";
    var m = s.match(new RegExp("ain", "gi"));
    return Assert.AreEqual("String Match", 4, m.length);
};
Pit.Test.JsStringTest.Replace = function() {
    var s = Pit.Test.JsStringTest.Create();
    var s1 = s.replace("e", "w");
    return Assert.AreEqual("String Replace", "Hwllo", s1);
};
Pit.Test.JsStringTest.Search = function() {
    var s = Pit.Test.JsStringTest.Create();
    var s1 = s.search("l");
    return Assert.AreEqual("String Search", s1, 2);
};
Pit.Test.JsStringTest.Slice = function() {
    var s = Pit.Test.JsStringTest.Create();
    var s1 = s.slice(1);
    return Assert.AreEqual("String slice", 4, s1.get_Length());
};
Pit.Test.JsStringTest.Split = function() {
    var s = "Hello World";
    var s1 = s.split(" ");
    return Assert.AreEqual("String Split", 2, s1.length);
};
Pit.Test.JsStringTest.Substring1 = function() {
    var s = Pit.Test.JsStringTest.Create();
    var s1 = s.substring(1);
    return Assert.AreEqual("String substring", s1.get_Length(), 4);
};
Pit.Test.JsStringTest.Substring2 = function() {
    var s = Pit.Test.JsStringTest.Create();
    var s1 = s.substr(1, 1);
    return Assert.AreEqual("String substring", s1.get_Length(), 1);
};
Pit.Test.JsStringTest.ToLower = function() {
    var s = Pit.Test.JsStringTest.Create();
    var s1 = s.toLower();
    return Assert.AreEqual("String ToLower", "hello", s1);
};
Pit.Test.JsStringTest.ToUpper = function() {
    var s = Pit.Test.JsStringTest.Create();
    var s1 = s.toUpper();
    return Assert.AreEqual("String ToUpper", "HELLO", s1);
};
Pit.Test.DateTest.Create = function() {
    var d = new Date();
    var m = d.getDate();
    Assert.IsNotNull("Date Create Month", m);
    var d1 = d.getDay();
    Assert.IsNotNull("Date Create Day", d1);
    var mn = d.getMonth();
    Assert.IsNotNull("Date Create Month", mn);
    var y = d.getFullYear();
    Assert.IsNotNull("Date Create Year", y);
    var h = d.getHours();
    Assert.IsNotNull("Date Create Hours", h);
    var mi = d.getMinutes();
    Assert.IsNotNull("Date Create Minutes", mi);
    var sec = d.getSeconds();
    Assert.IsNotNull("Date Create Seconds", sec);
    var time = d.getTime();
    Assert.IsNotNull("Date Create Time", time);
    var gmt = (-d.getTimezoneOffset() / 60);
    Assert.IsNotNull("Date Timezone offset", gmt);
    var ud = d.getUTCDate();
    Assert.IsNotNull("UTC Create Date", ud);
    var udy = d.getUTCDay();
    Assert.IsNotNull("UTC Create Day", udy);
    var udm = d.getUTCMonth();
    Assert.IsNotNull("UTC Create Month", udm);
    var udy1 = d.getUTCFullYear();
    Assert.IsNotNull("UTC Create Year", udy1);
    var udh = d.getUTCHours();
    Assert.IsNotNull("UTC Create Hours", udh);
    var udmn = d.getUTCMinutes();
    Assert.IsNotNull("UTC Create Minutes", udmn);
    var udsec = d.getUTCSeconds();
    Assert.IsNotNull("UTC Create Seconds", udsec);
    var udmil = d.getUTCMilliseconds();
    return Assert.IsNotNull("UTC Create Milliseconds", udmil);
};
Pit.Test.DateTest.Parse = function() {
    var d = Date.parse("Jul 8, 2005");
    return Assert.AreEqual("Date Parse", d, 1120761000000);
};
Pit.Test.DateTest.UTC = function() {
    var d = Date.UTC();
    return Assert.IsNotNull("Date UTC", d);
};
Pit.Test.DateTest.TestAll = function() {
    Pit.Test.DateTest.Create();
    Pit.Test.DateTest.Parse();
    return Pit.Test.DateTest.UTC();
};
