
const vehicleTypeFunc = function (element) {

    let target = document.getElementsByClassName('vihtype'); 

    for (let i = 0; i < target.length; i++) {

        console.log(target[i]); 
        
        if (target[i].value === element) {

            target[i].setAttribute('selected', true);
            window.location.href = '/Vehicles/GetVehicleType?selectedValue=' + element;

        }
        
    }
}













