const dataModule = (function() {
    const doAPICall = function(url){
        return fetch(url)
        .then(r => r.json())
        .then(data => data)
        .catch(error => console.error('An error occured: ', error));
    };
    return {
        doAPICall: doAPICall
    };
})();