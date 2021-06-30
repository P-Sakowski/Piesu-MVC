(function () {
    const alertSuccessElement = document.getElementById("success-alert");
    const alertFailureElement = document.getElementById("failure-alert");
    const formElement = document.forms[1];

    const addNewDog = async () => {
        const requestData = {
            title: formElement.elements.namedItem('Title').value,
            description: formElement.elements.namedItem('Description').value,
            dog: formElement.elements.namedItem('Dog').value,
        };

        const response = await fetch('/api/AdvertApi', {
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
