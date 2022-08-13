
// Script handles fetch and update of the DOM


const addUrl = location.protocol + '//' + window.location.host + '/Vehicles/SetVehicleType?vehicleType=';

const vehiElement = document.querySelector('#vehicletypeid')
const formElement = document.querySelector('#formid');

const getOptions = {
    'method': 'GET',
    'Content-type': 'application/json'
};

const postOptions = {
    'method': 'POST',
    'Content-type': 'application/json'
};


// Selected vehicle type is passed from view by onchange event.
// The call is done to the controller action which returns a json to this promise
// With insertAdjacentHTML data i add directly on element.This also means that only the data
// is refreshed for on the page and not the whole page. 
const fetchData = function (vehicleValue) {

    let url = addUrl + vehicleValue;

    console.log('Allan ' + url);

    try {
        const response = fetch(url, getOptions)
            .then(res => {
                if (!res.ok) {
                    console.log('Not sucessful');
                } else {
                    console.log('SUCCESS');
                    return res.json();
                }
            })
            .then((data) => {
                console.log('Fetch - Data ' + data);
                vehiElement.innerHTML = ""; 
                addItemToDom(data);
            })

            .catch((error) => {
                console.log(error);
            });

        console.log('response ' + response);
        return response;

    } catch (e) {
        console.error('Exception is thrown ' + e.message);
    }
}

//Add result to the DOM
const addItemToDom = function (_vehicleData) {

    let _item = new Item02(_vehicleData);
    vehiElement.insertAdjacentHTML('beforebegin',
        `<input hidden class="form-control" name="VehicleType" value="${_item.vehicleType}"/>`);
}


// Class that reflects parts of the model in in the view.
class Item02 {
    constructor(vehicleType) {
        this.vehicleType = vehicleType;
    }
}

