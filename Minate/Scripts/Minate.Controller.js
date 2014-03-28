/// <reference path="jquery.polling.js" />
/// <reference path="Minate.View.js" />
/// <reference path="Minate.Model.js" />

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
function MinateController(_maxX, _maxY, _totalBombs, _playerId) {

    /*  */
    if (!(this instanceof arguments.callee))
        return new MinateController(_maxX, _maxY, _totalBombs, _playerId);

    /*  */
    var minateModel = new MinateModel(_playerId, _totalBombs).Initiate();
    /*  */
    var minateView = new MinateView(_maxX, _maxY).Initiate();
    /* POLLING ONLY! Saves the active poll identifier, so we can cancel the active polling. */
    var playersPoll;
    var playsPoll;
    var messagesPoll;

    var lastPlay = -1;
    var lastPlayer = -1;

    /*
    *
    */
    this.CellClick = function (x, y, e) {
        if (!minateModel.GameFinished()) {
            minateModel.GetCurrentPlayer(function (currentPlayer, status) {

                if (currentPlayer.Identifier == minateModel.ClientID()) {
                    minateModel.GetCellsToOpen(x, y, function (cellsToOpen, status) {
                        if (cellsToOpen.length > 0) {
                            minateView.OpenCells(cellsToOpen, currentPlayer);

                            lastPlay++;
                        }
                    });
                }
            });
        }
    }

    /*
    *
    *
    */
    this.SendMessage = function (player, message) {
        $.post(Urls.Invoke("SendChatMessage"), { Id: minateModel.ClientID(), Text: message },
                function (data, state) {
                    if (data.state == "success")
                        minateView.AddMessage(data.message)
                }, "json");
    }

    /*
    *
    */
    this.PollForChanges = function () {
        ctrl = this;

        options = {
            callback: function (plays, status) {
                $.polling(playsPoll, { cancel: true });

                var play;

                for (var i = 0; i < plays.length; ++i) {
                    play = plays[i];

                    minateView.OpenCells(play.Cells, play.Owner);

                    if (play.Cells.length == 1 && play.Cells[0].Bomb)
                        minateView.ChangePlayerMines(play.Owner.Identifier);
                    else
                        minateView.ChangeActivePlayer();
                }

                if (play) {
                    minateModel.GetCurrentPlayer(function (currentPlayer, status) {
                        if (currentPlayer.Winner) {
                            $.polling(playsPoll, { cancel: true });
                            
                            minateModel.GameFinished(true);
                            minateView.ShowWinningDialog(currentPlayer.Winner.Identifier == minateModel.ClientID());
                        }
                    });

                    lastPlay = play.Identifier;
                }
                
                playsPoll = $.polling(Urls.Invoke("GetPlaysSince", lastPlay), options);
            },
            timeout: 1000
        };

        playsPoll = $.polling(Urls.Invoke("GetPlaysSince", lastPlay), options);
    }

    /*
    * 
    */

    this.PollForPlayers = function () {
        ctrl = this;

        options = {
            callback: function (players, status) {
                $.polling(playersPoll, { cancel: true });

                if (players.state == "full" && players.list.length == 0) {
                    ctrl.PollForChanges();
                    minateView.ChangeActivePlayer();
                }
                else {
                    var player;

                    if (players.list)
                        players = players.list;

                    for (var i = 0; i < players.length; ++i) {
                        player = players[i];
                        minateView.DrawPlayer(player);
                    }

                    if (player) {
                        lastPlayer = player.Identifier;
                    }

                    playersPoll = $.polling(Urls.Invoke("GetPlayersInGame", lastPlayer), options);
                }
            },
            timeout: 2000
        };

        playersPoll = $.polling(Urls.Invoke("GetPlayersInGame", lastPlayer), options);
    }

    this.PollForMessages = function () {
        ctrl = this;

        options = {
            callback: function (messages, status) {
                var message;

                for (var i = 0; i < messages.length; ++i) {
                    message = messages[i];
                    minateView.AddMessage(message);
                }
            },
            timeout: 5000
        };

        messagesPoll = $.polling(Urls.Invoke("GetChatMessages", ctrl.lastMessage), options);
    }

    this.ReplayPlayers = function () {
        ctrl = this;

        options = {
            callback: function (players, status) {
                $.polling(playersPoll, { cancel: true });

                var player;

                if (players.list)
                    players = players.list;

                for (var i = 0; i < players.length; ++i) {
                    player = players[i];
                    minateView.DrawPlayer(player);
                }
                
                minateView.ChangeActivePlayer();
            }
        };

        playersPoll = $.polling(Urls.Invoke("GetPlayersInGame", -1), options);
    }

    this.ReplayForward = function () {
        ctrl = this;
        lastPlay = lastPlay + 1;

        options = {
            callback: function (play, status) {
                $.polling(playsPoll, { cancel: true });

                if (play) {
                    minateView.OpenCells(play.Cells, play.Owner);

                    if (play.Cells.length == 1 && play.Cells[0].Bomb)
                        minateView.ChangePlayerMines(play.Owner.Identifier);
                    else
                        minateView.ChangeActivePlayer();
                }
            }, timeout: 500
        };

        playsPoll = $.polling(Urls.Invoke("GetSpecificPlay", lastPlay), options);
    }
}