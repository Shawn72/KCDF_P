//Function to allow only numbers to textbox
function validate4N(key) {
    //getting key code of pressed key
    var keycode = (key.which) ? key.which : key.keyCode;
    var phn = document.getElementById('txtPhoneNo');
    //comparing pressed keycodes
    if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
        return false;
    }
    else {
        //Condition to check textbox contains ten numbers or not
        if (phn.value.length < 13) {
            return true;
        }
        else {
            return false;
        }
    }
}

function validateID(key) {
    //getting key code of pressed key
    var keycode = (key.which) ? key.which : key.keyCode;
    var id = document.getElementById('txtIDNo');
    //comparing pressed keycodes
    if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
        return false;
    }
    else {
        //Condition to check textbox contains ten numbers or not
        if (id.value.length < 15) {
            return true;
        }
        else {
            return false;
        }
    }
}

function validateGuard4N(key) {
    //getting key code of pressed key
    var keycode = (key.which) ? key.which : key.keyCode;
    var phn = document.getElementById('txtGuardianPhone');
    //comparing pressed keycodes
    if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
        return false;
    }
    else {
        //Condition to check textbox contains ten numbers or not
        if (phn.value.length < 13) {
            return true;
        }
        else {
            return false;
        }
    }
}

function allowOnlyNumber(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}
       