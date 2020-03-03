/// Navigation menu items for different users
export const DefaultNav = () => {
    return {
        type: 'DEFAULT_NAV',
        payload: [
            {
                id: 0,
                path: "/",
                displayName: "",

            }
        ]
    }
}
export const AdminNav = () => {
    return {
        type: 'ADMIN_NAV',
        payload: [
            {
                id: 0,
                path: "/",
                displayName: "Home",

            }
        ]
    };
}