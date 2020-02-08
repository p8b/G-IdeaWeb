import React from 'react';

export const DefaultNav = () => {
    return {
        type:'DEFAULT_NAV'
    }
}
export const CustomerNav = () => {
    return {
        type: 'CUSTOMER_NAV_MENU',
        payload: [
            {
                id: 0,
                path: "/Link1",
                displayName: "Link 1",
                displayOnRight: false

            },
            {
                id: 1,
                path: "/Link2",
                displayName: "Link 2",
                displayOnRight: true

            }
        ]
    };
}
export const ManagerNav = () => {
    return {
        type: 'MANAGER_NAV_MENU',
        payload: [
            {
                id: 0,
                path: "/Link1",
                displayName: <div>Link1</div>,
                displayOnRight: false
            },
            {
                id: 1,
                path: "/Link2",
                displayName: <div>Link2</div>,
                displayOnRight: true
            },
        ]
    };
}