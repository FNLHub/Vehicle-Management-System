//Cory's Example
$(document).ready(function () {

    //Array of div id's
    let divStepId = [];

    //Hide Submit Button
    let submitButton = $("#btnSubmit");
    submitButton.hide();

    for (let i = 0; i < 6; i++) {

        //Populate Array
        divStepId[i] = $("#step" + (i+1));
        //Hide all step Divs
        if (i > 0) {
            divStepId[i].hide();

            //Previous Button Pressed
            divStepId[i].find("#btnStep" + (i+1) + "Prev").click(function () {

                divStepId[i - 1].slideDown(300);
                //location.hash = "step2";
                $("html,body").animate({
                    scrollTop: divStepId[i - 1].offset().top
                }, 1000);

                divStepId[i].slideUp(300);

                //show current next step and prev step
                $("#btnStepGroup" + (i)).show();
            });
        }
        if (i < 5) {
            
            //Next Button Pressed
            divStepId[i].find("#btnStep"+(i+1)+"Next").click(function () {

                divStepId[i+1].slideDown(300);
                //location.hash = "step2";
                $("html,body").animate({
                    scrollTop: divStepId[i+1].offset().top
                }, 1000);

                //hide current next step and prev step
                $("#btnStepGroup" + (i + 1)).hide();

            });
        }  
    };
});

/*          Alex's Old Way of Button Functions'
function ShowHide(NextStepId, PrevBtnId, CurrentStepId) {
    //show next step
    let x = document.getElementById(NextStepId);
    if (x.style.display === "") {
        x.style.display = 'block';
    }
    //hide previous button
    let whatever = document.getElementsByClassName(PrevBtnId);
    for (let i = 0; i < whatever.length; i++) {
        whatever[i].style.display = 'none';
    }
    //disable elements in previous id
    $('#' + CurrentStepId + " *").attr("disabled", "disabled");

}

function GoBack(stepId, PrevBtnId, PrevStepId) {
    //hide current step
    let x = document.getElementById(stepId);
    if (x.style.display === 'block') {
        x.style.display = "";

        window.location.hash = '#' + PrevStepId;
    }
    //show previous button
    let whatever = document.getElementsByClassName(PrevBtnId);
    for (let i = 0; i < whatever.length; i++) {
        whatever[i].style.display = 'inline-block';
    }
    //re-enable elements in previous id
    $('#' + PrevStepId + " *").removeAttr("disabled");
}
*/

function EnableDisableSubmit(SubBtnId, Status) {
    let s = document.getElementById(SubBtnId)
    //make submit button visible
    if (Status === "enabled") {
        s.style.display = 'inline-block';
    }
    //make submit button invisible
    else if (Satus === "disabled") {
        s.style.display = 'none';
    }
}

function ValidateInformation(CurrentStepId, NextStepId, PrevBtnId) {
    //get all the data fields from the current step
    let allFieldsDone = 0;
    let Fields = $('#' + CurrentStepId + " input");

    Fields.each(function (index, element) {
        console.dir(element);
        //do nothing if any data field is left blank
        if (element.value === "") {
            alert("Please Fill In the " + element.localName + " Form");
        }
        else {
            if (true) {

                allFieldsDone++;
            }
            else {
                alert("Invalid Input");
            }
        }
    });
}



//Deleting a row
function deleteRow(num) {
    console.log(num);
    $("#row-" + num).remove();
};

$("#create-drivers").on("click", function () {
    $.ajax({
        async: false,
        url: "/Transportation/CreateDriver"
    }).success(function (pview) {
        $("#add-driver").append(pview);
    });
});