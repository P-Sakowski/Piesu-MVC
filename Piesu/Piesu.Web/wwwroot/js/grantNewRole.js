(function () {
    const alertSuccessElement = document.getElementById("success-alert");
    const alertFailureElement = document.getElementById("failure-alert");
    const buttons = document.getElementsByClassName("grantRole");

    const grantNewRole = async (clickedBtn) => {
        const requestData = {
            id: clickedBtn.dataset.id
        };

        const response = await fetch('/api/RoleApi', {
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

    window.addEventListener("DOMContentLoaded", () => {
        for (let index = 0; index < buttons.length; index++) {
            buttons[index].addEventListener("click", event => {
                event.preventDefault();
                grantNewRole(event.target).then(() => console.log("role granted successfully"));
            });
        }
    });
})(); 