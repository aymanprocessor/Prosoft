class NavbarManager {
    constructor(navbarContainerId) {
        this.navbarContainer = $(`#${navbarContainerId}`);
        if (this.navbarContainer.length === 0) {
            throw new Error('Invalid container element ID');
        }
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
                    height: '50px',
                    color: '#000',
                    textDecoration: 'none',
                    fontSize: '1.15em',
                    transition: '0.3s ease'
                })
                .hover(
                    function () {
                        $(this).css({ background: 'rgba(0,0,0,0.7)', color: '#fff' });
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
                    right: '250px',
                    top: '0',
                    display: 'none',
                   // background: 'rgba(230,230,230,1)',
                    boxShadow: '2px 2px 10px rgba(0,0,0,.1)'
                });
                li.append(subUl);

                li.hover(
                    function () {
                        subUl.css('display', 'block');
                    },
                    function () {
                        subUl.css('display', 'none');
                    }
                );
            }

            li.append(a);
            ul.append(li);
        });

        return ul;
    }
}