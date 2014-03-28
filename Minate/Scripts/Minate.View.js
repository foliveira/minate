/// <reference path="jquery-1.3.2.js" />

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
function MinateView(_x, _y) {

    /*  */
    if (!(this instanceof arguments.callee))
        return new MinateView(_x, _y);
        
    /*  */
    var xSz = _x;
    /*  */
    var ySz = _y;
    /*  */
    var board;
    /*  */
    var winningDialog;
    /*  */
    var players;
    /*  */
    var tmplCache;
    /*  */
    var cellCache;
    /*  */
    var playersCache;
    /*  */
    var activePlayer;

    /*
    *
    */
    this.Initiate = function () {
        tmplCache = new Array();
        cellCache = new Array();
        playersCache = new Array();
        activePlayer = -1;

        board = $(".mineboard");
        players = $(".players");
        winningDialog = $("#dialog");

        this.ShowPlayer = this.Template("draw_status");
        this.ShowDialog = this.Template("end_game");

        this.Design();
        this.InstantiateWinningDialog();

        return this;
    }

    /*
    *
    */
    this.Design = function () {
        for (i = 0; i <= ySz; ++i) {
            board.append("<div class=\"mineboard-row\">");

            for (j = 0; j <= xSz; ++j) {
                row = $("div.mineboard-row:eq(" + i + ")").append("<div class=\"mineboard-cell\" onclick=\"minateController.CellClick(" + i + ", " + j + ");\"/>");
                cellCache.push(row.children("div.mineboard-cell:eq(" + j + ")"));
            }
        }
    }

    /*
    *
    */
    this.InstantiateWinningDialog = function () {
        winningDialog.dialog({
            bgiframe: true,
            modal: true,
            reziable: false,
            draggable: false,
            autoOpen: false,
            show: "puff",
            buttons: {
                Ok: function () {
                    $(this).dialog('close');
                }
            }
        });
    }

    /*
    *
    */
    this.GetCellAt = function (x, y) {
        return cellCache[y + x * (xSz + 1)];
    }

    /*
    *
    */
    this.OpenCells = function (cellsToOpen, currPlayer) {
        for (i = 0; i < cellsToOpen.length; ++i) {
            with (cellsToOpen[i]) {
                if (Bomb) {
                    this.OpenCellWithBomb(X, Y, currPlayer);
                } else if (Neighbors > 0) {
                    this.OpenCellWithNeighbors(X, Y, Neighbors);
                } else {
                    this.GetCellAt(X, Y).css("background-position", "-25px 0px");
                }
            }
        }
    }

    this.CloseCells = function (cellsToClose) {
        for (i = 0; i < cellsToClose.length; ++i) {
            with (cellsToClose[i]) {
                this.GetCellAt(X, Y).css("background-position", "0px 0px");
            }
        }
    }

    /*
    *
    */
    this.OpenCellWithBomb = function (x, y, p) {
        //this.GetCellAt(x, y).hide("explode", { pieces: 8 }, 750);
        //this.GetCellAt(x, y).show("explode", { pieces: 8 }, 100);
        this.GetCellAt(x, y).css("background-position", "-" + ((p.Identifier * 25) + 250) + "px 0px");
    }

    /*
    *
    */
    this.OpenCellWithNeighbors = function (x, y, n) {
        this.GetCellAt(x, y).css("background-position", "-" + ((n + 1) * 25) + "px 0px");
    }

    /*
    *
    */
    this.ChangePlayerMines = function (playerId) {
        var elem = playersCache[playerId].children("p.mines");
        var mines = elem.data("mines") || 0;

        elem.text(++mines);

        elem.data("mines", mines);
    }

    /*
    *
    */
    this.ChangeActivePlayer = function () {
        var idx = activePlayer;

        if (idx >= 0) playersCache[idx].removeAttr("id");
        idx = (idx > 0) ? idx % (playersCache.length - 1) : idx + 1;
        playersCache[idx].attr("id", "active").effect("highlight", {}, 2000);

        activePlayer = idx;
    }

    /*
    *
    */
    this.ShowWinningDialog = function (winner) {
        winningDialog.html(this.ShowDialog({ status: winner }));

        winningDialog.dialog("option", "title", winner ? "You won!" : "You lost!");
        winningDialog.show();
        winningDialog.dialog("open");
    }

    /*
    *
    */
    this.DrawPlayer = function (_player) {
        players.append($.trim(this.ShowPlayer({ player: _player })));
        playersCache.push(players.children().eq(_player.Identifier));
    }

    this.AddMessage = function (message) {
        alert("NOT IMPLEMENTED YET");
    }
    
    /*
    *
    */
    this.Template = function (str, data) {
        // Figure out if we're getting a template, or if we need to
        // load the template - and be sure to cache the result.
        var fn = !/\W/.test(str) ?
      tmplCache[str] = tmplCache[str] ||
        this.Template(document.getElementById(str).innerHTML) :

        // Generate a reusable function that will serve as a template
        // generator (and which will be cached).
      new Function("obj",
        "var p=[],print=function(){p.push.apply(p,arguments);};" +

        // Introduce the data as local variables using with(){}
        "with(obj){p.push('" +

        // Convert the template into pure JavaScript
        str
          .replace(/[\r\t\n]/g, " ")
          .split("<!").join("\t")
          .replace(/((^|!>)[^\t]*)'/g, "$1\r")
          .replace(/\t=(.*?)!>/g, "',$1,'")
          .split("\t").join("');")
          .split("!>").join("p.push('")
          .split("\r").join("\\'") +
       "');}return p.join('');");
       
        // Provide some basic currying to the user
        return data ? fn(data) : fn;
    }
}