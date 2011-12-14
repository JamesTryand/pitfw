#Pit - F# to JS Compiler
>Lets you code, debug and manage easily

Pit is F# to Javascript compiler that leverages the beauty of F# and also JavaScript.

##Roadmap
###Features
* F# Map Structure
* AJAX
    * Support for mapping response text as JSON
    * Support for reading response XML
* jQuery integration
    * Additionally implement extension points in the compiler to modify the AST using MEF.
* F# views for ASP.NET MVC (Tomas Petricek's project)
* HTML5 apis
    * WebWorkers
    * File API
    * Data attributes
* WinRT + VS2011 integration
    * F# 3.0 build targetting VS 2011
    * Metro-specific application templates
    * JavaScript bindings to WinJS
Note: From now on it will be an incremental release based on feature set completion, and no specific version based releases. This is because we have initial set of feature set ready with Pit. Any of these features could be contributed by the community.

Additionally, If someone is interested in creating component controls using Pit, below are the areas of focus,

* F# Component Controls (using reactive pattern aka Observables)
    * Editor controls like AutoComplete, MaskEdit, DateTime, Integer/Double/Percent edit, Popup edit, Slider, Range slider, Color picker, RadioListBox, CheckListBox etc.,
    * Basic Charting library (using SVG/HTML5 Canvas)
        * Sparkline charts
    * Basic diagram controls (using SVG/HTML5 Canvas) 
    * Basic DataGrid

##Release Notes
###Pit v0.2

####Features
* Array2D, F# Set, F# String extensions
* Operator overloading for types, records, unions
* AJAX / XMLHttpRequest both in debug/JS mode
* HTML5 DOM elements & SVG
* Custom Library project templates
* Mac MonoDevelop support
####Fixes
* Fix closure issue in for loops
* Fix mapping .NET string functions to JsString
* Fix tuple value code generation in function parameters
* Fix type extensions for DOM type classes
* Fix overloaded constructors issue
* Fix overloaded members
* Fix code generation issue with static property GETTER
* Fix missing method for Window.setInterval

###Pit v0.1

* Support all features of F# that can be translated using "ReflectedDefinitionAttribute"
* Clean JS code generation
* Support below major F# libraries
  * Seq
  * List
  * Array
  * Event
  * IObservable
  * Computation Expressions
* Full HTML DOM and HTML5 Canvas Support
* Visual Studio 2010 integration with Application Project Templates
  * Debugging support for F# code
  * Build support with Pit compiler
  * JavaScript error notification
     
##How to contribute?
If you would like to contribute to the Pit project you could take up any of the features that is available in the roadmap / any custom feature that you think would provide nice value. You can email mohamedsuhaiba@live.in for dicussing and getting started with it. Small note here, Pit team may be working on some of the features already, so it's at best to check once before you fork off and start on it.

And if you want to work on bug fixes, go ahead and fork it off no need to check with us! Send us couple of test cases along with it.

Note: All features should pass the existing tests found in the "tests" folder and also provide new tests for the feature, only then the feature will be accepted for inclusion in the main branch.

##Reporting issues
Bugs can be filed using the Issues tab in Github (https://github.com/fahadsuhaib/pitfw/issues). Make sure you have got the following things
    * Sample with clear reproducing steps
    * If possible, provide a screenshot and describe the error in a simple way
We will analyze the bug and if it's a good use-case then this will be added to the main test cases for future regression testing.

####Thanks
We would like to thank the F# community for providing valuable information in getting this work done. Also, we would like to thank projects like Tomas's F# WebTools (http://fswebtools.codeplex.com/), Jason Greene's F# to JavaScript (https://github.com/jgreene/FSharp.Javascript), Script# (https://github.com/nikhilk/scriptsharp) and the F# source compiler drops (http://fsharppowerpack.codeplex.com/) to help us understand and complete the implementation.