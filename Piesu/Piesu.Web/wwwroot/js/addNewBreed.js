(function () {
    const alertSuccessElement = document.getElementById("success-alert-add");
    const alertFailureElement = document.getElementById("failure-alert");
    const formElement = document.forms[1];

    const addNewBreed = async () => {
        const requestData = {
            name: formElement.elements.namedItem('Name').value
        };

        const response = await fetch('/api/BreedApi', {
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
            addNewBreed().then(() => console.log("added successfully"));
        });
    });
})(); 