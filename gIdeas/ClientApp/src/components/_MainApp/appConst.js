/// This method is used to get the value of the a cookie by passing it the cookie's name
export const getCookieValue = (name) => {
    try {
        /// Get the cookie values from the document and split them
        var cookies = document.cookie.split(';');
        var cookieValue = ' ';
        /// Loop through the available cookies
        for (var i = 0; i < cookies.length; i++) {
            /// if the cookie name match is found
            if (~cookies[i].indexOf(name + '=')) 
            {
                /// get the value of the cookie by simply removing the name and "=" 
                cookieValue = cookies[i].replace(name + '=', '');
            }
        }
        /// return the value of the cookie
        return cookieValue.trim();
    } catch (err) {
        return '';
    }
}

export const AccessClaims = {
    Admin: "Admin",
    QAMananger: "QAManager",
    QACoordinator: "QACoordinator",
    Staff: "Staff"
}

export const IdeaStatus = {
    Pending: "Pending",
    FirstClosure: "FirstClosure",
    Closed: "Closed",
    Blocked: "Blocked",
    All: ["Pending", "FirstClosure", "Closed", "Blocked"]
}

export const AllRecords = "***GET-ALL***";


const Base_Api_URL = "https://localhost:44382/api/";
// const Base_Api_URL = "https://192.168.1.6:44382/api/";
export class apiCaller {


    static async postFormData(apiUrl, bodyObject) {
        return fetch(`${Base_Api_URL}${apiUrl}`, {
            method: "POST",
            body: bodyObject,
        }).catch(err => console.log(err));
    }

    static async post(apiUrl, bodyObject = "") {
        return fetch(`${Base_Api_URL}${apiUrl}`, {
            method: "POST",
            headers: {
                Accept: 'application/json',
                'content-type': 'application/json',
                'X-AntiForgery-TOKEN': getCookieValue("AntiForgery-TOKEN"),
            },
            body: JSON.stringify(bodyObject),
        }).catch(err => console.log(err));
    }
    static async put(apiUrl, bodyObject = "") {
        return fetch(`${Base_Api_URL}${apiUrl}`, {
            method: "PUT",
            headers: {
                Accept: 'application/json',
                'content-type': 'application/json',
                'X-AntiForgery-TOKEN': getCookieValue("AntiForgery-TOKEN"),
            },
            body: JSON.stringify(bodyObject),
        }).catch(err => console.log(err));
    }
    static async delete(apiUrl, bodyObject = "") {
        return fetch(`${Base_Api_URL}${apiUrl}`, {
            method: "DELETE",
            headers: {
                Accept: 'application/json',
                'content-type': 'application/json',
                'X-AntiForgery-TOKEN': getCookieValue("AntiForgery-TOKEN"),
            },
            body: JSON.stringify(bodyObject),
        }).catch(err => console.log(err));
    }
    static async get(apiUrl) {
        return fetch(`${Base_Api_URL}${apiUrl}`, {
            method: "GET",
        }).catch(err => console.log(err));
    }
}
