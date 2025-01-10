document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('uploadForm');
    const selectedDateInput = document.getElementById('selectedDate');
    const buttonTextInput = document.getElementById('buttonText');

    // Function to handle button clicks
    function handleButtonClick(button) {
        if (!selectedDateInput.value) {
            alert("Please select a date before submitting.");
            return;
        }

        // Set the button text in the hidden input
        buttonTextInput.value = button.textContent.trim();

        // Submit the form
        form.submit();
    }

    // Attach click event listeners to buttons
    document.getElementById('btnUploadKPI').addEventListener('click', function () {
        handleButtonClick(this);
    });

    document.getElementById('btnUploadOther').addEventListener('click', function () {
        handleButtonClick(this);
    });
});
