(function () {
    const alertSuccessElement = document.getElementById("success-alert");
    const alertFailureElement = document.getElementById("failure-alert");
    const formElement = document.forms[1];

    const addNewDog = async () => {
        const requestData = {
            name: formElement.elements.namedItem('Name').value,
            description: formElement.elements.namedItem('Description').value,
            breed: formElement.elements.namedItem('Breed').value,
        };

        const response = await fetch('/api/DogApi', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestData)
        });
        const responseJson = await response.json();

        if (responseJson.isSuccessful) {
            alertSuccessElement.style.display = 'block';
            alertFailureElement.style.display = 'none';
        } else {
            alertSuccessElement.style.display = 'none';
            alertFailureElement.style.display = 'block';
        }
    };

    window.addEventListener("load", () => {
        formElement.addEventListener("submit", event => {
            event.preventDefault();
            addNewDog().then(() => console.log("added successfully"));
        });
    });
})(); 