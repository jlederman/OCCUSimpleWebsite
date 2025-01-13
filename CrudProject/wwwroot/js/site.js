// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', () => {
    const rows = document.querySelectorAll('tbody tr');

    rows.forEach(row => {
        const propertyCell = row.querySelector('td:first-child');
        const propertyName = propertyCell?.textContent.trim();

        if (propertyName === 'Name' || propertyName === 'Last Edited') {
            return;
        }

        const cells = row.querySelectorAll('td:not(:first-child)');

        if (cells.length === 2) {
            const [cell1, cell2] = cells;
            if (cell1.textContent.trim() !== cell2.textContent.trim()) {
                cell1.classList.add('mismatch');
                cell2.classList.add('mismatch');
            }
        }
    });
});
