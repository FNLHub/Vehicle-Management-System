//Cory's Example
$(document).ready(function () {

    //Array of div id's
    let divStepId = [];

    //Hide Submit Button
    let submitButton = $("#btnSubmit");
    submitButton.hide();

    for (let i = 0; i < 6; i++) {

        //Populate Array
        divStepId[i] = $("#step" + (i + 1));
        //Hide all step Divs
        if (i > 0) {
            divStepId[i].hide();

            //Previous Button Pressed
            divStepId[i].find("#btnStep" + (i + 1) + "Prev").click(function () {

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
            divStepId[i].find("#btnStep" + (i + 1) + "Next").click(function () {

                divStepId[i + 1].slideDown(300);
                //location.hash = "step2";
                $("html,body").animate({
                    scrollTop: divStepId[i + 1].offset().top
                }, 1000);

                //hide current next step and prev step
                $("#btnStepGroup" + (i + 1)).hide();

            });
        }
    };
});