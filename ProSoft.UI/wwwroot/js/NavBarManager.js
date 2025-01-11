class NavbarManager {
    constructor(navbarContainerId) {
        this.navbarContainer = $(`#${navbarContainerId}`);
        if (this.navbarContainer.length === 0) {
            throw new Error('Invalid container element ID');
        }

        // Add a click event listener to the document to handle clicks outside the submenu
        $(document).on('click', (e) => {
            if (!$(e.target).closest('.eis-dropdown').length) {
                // Hide all submenus if the click is outside any dropdown
                $('.eis-dropdown ul').css('display', 'none');
            }
        });
    }

    createNav(items) {
        // Clear existing navbar content
        this.navbarContainer.empty();

        // Create main nav element
        const nav = $('<nav></nav>');
        const ul = this.createMenu(items);
        nav.append(ul);
        this.navbarContainer.append(nav);
    }

    createMenu(items) {
        const ul = $('<ul></ul>').css({
            listStyle: 'none',
            padding: '10px 0'
        });

        items.forEach(item => {
            const li = $('<li></li>').addClass(item.subMenu ? 'eis-dropdown' : '');

            const a = $('<a></a>').attr('href', item.href || '#')
                .text(item.text)
                .css({
                    display: 'flex',
                    alignItems: 'center',
                    padding: '10px 15px',
                    marginBottom: '5px',
                    marginLeft: '5px',
                    marginRight: '5px',
                    height: '50px',
                    color: '#000',
                    textDecoration: 'none',
                    fontSize: '1.15em',
                    transition: '0.3s ease'
                })
                .hover(
                    function () {
                        $(this).css({ background: '#00796b', color: '#fff' });
                    },
                    function () {
                        $(this).css({ background: 'none', color: '#000' });
                    }
                );

            if (item.subMenu) {
                a.append($('<span>&rsaquo;</span>').css({
                    marginLeft: 'auto',
                    fontSize: '1.5em'
                }));

                const subUl = this.createMenu(item.subMenu)
                    .css({
                        position: 'absolute',
                        width: '250px',
                        right: '260px',
                        top: '0',
                        display: 'none',
                        background: '#f1fff5',
                        boxShadow: '2px 2px 10px rgba(0,0,0,.1)',
                        borderRadius: '10px'

                    });
                li.append(subUl);

                // Toggle submenu on click
                a.on('click', (e) => {
                    e.preventDefault(); // Prevent default link behavior
                    subUl.css('display', subUl.css('display') === 'none' ? 'block' : 'none');
                });
            }

            li.append(a);
            ul.append(li);
        });

        return ul;
    }
}
