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
*
*/
var MinateView = new (function () {

    var xSz;                    //
    var ySz;                    //

    var mineboardPlaceholder;   //
    var winningDialog;          //
    var players;                //

    /*
    *
    *
    */
    this.Init = function (_x, _y) {
        mineboardPlaceholder = $(".boardPane");
        players = $(".status");
        winningDialog = $("#dialog");

        xSz = _x;
        ySz = _y;

        this.Design();
        this.InstantiateWinningDialog();
    }

    /*
    *
    *
    */
    this.Design = function () {
        mineboardPlaceholder.append("<div class=\"mineboard\">");

        for (i = 0; i <= ySz; ++i) {
            $(".mineboard").append("<div class=\"mineboard-row\">");

            for (j = 0; j <= xSz; ++j) {
                $("div.mineboard-row:nth-child(" + (i + 1) + ")").append("<div class=\"mineboard-cell\" onclick=\"MinateController.CellClick(" + j + ", " + i + ");\"/>");
            }
        }
    }

    /*
    *
    *
    */
    this.GetCellAt = function (x, y) {
        return $("div.mineboard-row:nth-child(" + (y + 1) + ")").children("div.mineboard-cell:nth-child(" + (x + 1) + ")");
    }

    /*
    *
    *
    */
    this.ClearBoardView = function () {
        for (i = 0; i <= xSz; i++) {
            for (j = 0; j <= ySz; j++) {
                this.GetCellAt(i, j).css("background-position", "0px 0px");
            }
        }
    }

    /*
    *
    *
    */
    this.OpenCells = function (cellsToOpen, currPlayer) {
        for (i = 0; i < cellsToOpen.length; ++i) {
            with (cellsToOpen[i]) {

                if (isBomb) {
                    this.OpenCellWithBomb(xPos, yPos, currPlayer);
                } else if (neighborBombs > 0) {
                    this.OpenCellWithNeighbors(xPos, yPos, neighborBombs);
                } else {
                    this.GetCellAt(xPos, yPos).css("background-position", "-25px 0px");
                }
            }
        }
    }

    /*
    *
    *
    */
    this.OpenCellWithBomb = function (x, y, p) {
        this.GetCellAt(x, y).hide("explode", { pieces: 8 }, 750);
        this.GetCellAt(x, y).show("explode", { pieces: 8 }, 100);
        this.GetCellAt(x, y).css("background-position", "-" + ((p.index * 25) + 225) + "px 0px");
    }

    /*
    * 
    *
    */
    this.OpenCellWithNeighbors = function (x, y, n) {
        this.GetCellAt(x, y).css("background-position", "-" + ((n + 1) * 25) + "px 0px");
    }

    /*
    *
    *
    */
    this.ChangePlayerMines = function (player) {
        $("div.status:nth-child(" + player.index + ")").children("p.mines").text(player.mines);
    }

    /*
    *
    *
    */
    this.ClearPlayerScores = function (n) {
        for (i = 1; i <= n; ++i) {
            $("div.status:nth-child(" + i + ")").children("p.mines").text("0");
        }
    }

    /*
    *
    *
    */
    this.ChangeActivePlayer = function (fromPlayer, toPlayer) {
        $("div.status:nth-child(" + fromPlayer.index + ")").toggleClass("active");
        $("div.status:nth-child(" + toPlayer.index + ")").toggleClass("active");


        $("div.mineboard-cell").hover(function () {
            $(this).css("border", "solid 1.5px " + toPlayer.color);
        }, function () {
            $(this).css("border", "solid 1.5px #000000");
        });
    }

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
    *
    */
    this.ShowWinningDialog = function (winner) {
        winningDialog.dialog("option", "title", winner.name + " won this game!");
        winningDialog.show();
        winningDialog.dialog("open");
    }
})();