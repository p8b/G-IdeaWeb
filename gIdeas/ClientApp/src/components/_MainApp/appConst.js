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