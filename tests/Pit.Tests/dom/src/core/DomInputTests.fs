namespace Pit.Dom.Tests
    open Pit
    open Pit.Dom

    module DomInputTests =

        let Setup(t:string) (inputid:string) (value:string) =
            let div = document.GetElementById("check")
            div.InnerHTML <- "<input type='"+ t + "' name='"+inputid+"' value='" + value + "' id='"+inputid+"' />"

        let TestButton() =
            Setup "button" "btn" "Click"
            let btn = document.GetElementById("btn") |> DomInputButton.Of
            Assert.AreEqual "Button name" btn.Name "btn"
            Assert.AreEqual "Button type" btn.Type "button"
            btn
            |> Event.click
            |> Event.add(fun _ ->
                alert("Button Click")
            )

        let TestCheckbox() =
            Setup "checkbox" "check1" ""
            let chk = document.GetElementById("check1") |> DomInputCheckbox.Of
            chk.Checked <- true
            Assert.AreEqual "Checkbox Checked" chk.Checked true

        let TestRadio() =
            Setup "radio" "rad" ""
            let rad = document.GetElementById("rad") |> DomInputRadioButton.Of
            rad.Checked <- true
            Assert.AreEqual "Radio Checked" rad.Checked true

        let TestPassword() =
            Setup "password" "pwd" "hello"
            let pwd = document.GetElementById("pwd") |> DomInputPassword.Of
            Assert.AreEqual "Password Def value" pwd.DefaultValue "hello"
            pwd.Size <- 40
            Assert.AreEqual "Password Size" pwd.Size 40
            pwd.MaxLength <- 10
            Assert.AreEqual "Password Maxlength" pwd.MaxLength 10

        let TestText() =
            Setup "text" "t1" "Hello"
            let t1 = document.GetElementById("t1") |> DomInputText.Of
            Assert.AreEqual "Text Def Value" t1.DefaultValue "Hello"
            Assert.IsNotNull "Text Form" t1.Form
            t1.Value <- "Hello World"
            Assert.AreEqual "Text Value" t1.Value "Hello World"