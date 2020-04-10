import { gRole } from "./Models";

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
export const AllRoles = [
    new gRole({ id: 1, name: "Admin" }),
    new gRole({ id: 2, name: "Staff" }),
    new gRole({ id: 3, name: "QA Coordinator" }),
    new gRole({ id: 4, name: "QA Mananger" }),
]
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

/// Code used from https://www.isummation.com/blog/convert-arraybuffer-to-base64-string-and-vice-versa/
export const ArrayBufferToBase64 = (buffer) => {
    console.log(buffer);
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}

export const Base64ToArrayBuffer = (base64) => {
    var binary_string = window.atob(base64);
    var len = binary_string.length;
    var bytes = new Uint8Array(len);
    for (var i = 0; i < len; i++) {
        bytes[i] = binary_string.charCodeAt(i);
    }
    return bytes.buffer;
}

export const StringBase64ToBlob = (base64) => {
    var byteCharacters = window.atob(base64);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    return new Blob([byteArray]);
}