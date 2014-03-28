/*
* javascript file to the Minate web application
*
* abandon all hope ye' who enter here! just 'cause you won't understand the brilliance of the code,
* you're about to read!
* go and grab one of those eclipse glasses. don't ever say I didn't warn you...
* 
* ready?! behold....
* 
*/

/*
*
*/
var Urls = {
    /*  */
    cache: {},
    /*  */
    Register: function (name, func) {
        if (!Urls.cache[name])
            Urls.cache[name] = func;
    },
    /*  */
    Invoke: function (name) {
        func = Urls.cache[name];
        return func.apply(null, Array.prototype.slice.call(arguments, 1));
    }
};

/*
*
*/
function MinateModel(_playerId, _totalBombs) {

    /*  */
    if (!(this instanceof arguments.callee))
        return new MinateModel(_player);
        
    /*  */
    var clientId = _playerId;
    /*  */
    var totalBombs = _totalBombs;
    /*  */
    var gameFinished;

    /*
    *
    */
    this.Initiate = function () {
        gameFinished = false;
        
        return this;
    }

    /*
    *
    */
    this.GetCellsToOpen = function (_x, _y, callback) {
        $.post(Urls.Invoke("GetCellsToOpen", _x, _y), {}, callback, "json");
    }

    /*
    *
    */
    this.GetCurrentPlayer = function (callback) {
        $.post(Urls.Invoke("GetCurrentPlayer"), {}, callback, "json");
    }

    /*
    *
    */
    this.ClientID = function () {
        return clientId;
    }
    
    /*
    *
    */
    this.TotalBombs = function () {
        return totalBombs;
    }

    /*
    *
    */
    this.GameFinished = function (state) {
        return gameFinished = gameFinished || !!state;
    }
}