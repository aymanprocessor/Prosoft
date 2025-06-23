// Patient Search Functionality
//document.getElementById('searchInput').addEventListener('input', function () {
//    filterTable();
//});

function filterTable() {
    var input = document.getElementById('searchInput');
    var filter = input.value.toUpperCase();
    var table = document.getElementById('myTable');
    var rows = table.getElementsByTagName('tr');

    for (var i = 1; i < rows.length; i++) { // Start from 1 to skip the header row
        var cells = rows[i].getElementsByTagName('td');
        var found = false;

        for (var j = 0; j < cells.length; j++) {
            var cell = cells[j];
            if (cell) {
                var textValue = cell.textContent || cell.innerText;
                if (textValue.toUpperCase().indexOf(filter) > -1) {
                    found = true;
                    break;
                }
            }
        }

        if (found) {
            rows[i].style.display = '';
        } else {
            rows[i].style.display = 'none';
        }
    }
}