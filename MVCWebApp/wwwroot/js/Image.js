function previewImage(event) {
    let input = event.target;
    var imagePreview = document.getElementById('renderizar');

    imagePreview.src = '';

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            imagePreview.src = e.target.result;
        }
        reader.readAsDataURL(input.files[0]);
    }
}