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
* Once upon a time there was a Player object, and I'm sure it was created with a call to this function!
*/
function Player(name, id, color) 
{
    if (!(this instanceof arguments.callee))
        return new Player(name, id);

    this.name = name;
    this.mines = 0;
    this.nextPlayer = null;
    this.index = id;
    this.color = color;
}

/*
* Even if I'm only half sure with the Player, with the Cell I'm more than sure that it was created with this function.
*/
function Cell(x, y) 
{
    if (!(this instanceof arguments.callee))
        return new Cell(x, y);

    this.isBomb = false;
    this.isExposed = false;
    this.isFlagged = false;
    this.isMarked = false;
    this.neighborBombs = 0;
    this.xPos = x;
    this.yPos = y;
}

/*
* Then there is this MarkedCell that only exists when we want to open a cell that wasn't opened.
*/
function MarkedCell() 
{
    if (!(this instanceof arguments.callee))
        return new MarkedCell();
    this.x = -1;
    this.y = -1;
}

/*
*
*
*/
var MinateModel = new (function () {

    var totalBombs;         //
    var flaggedBombs;       //
    var maxX;               //
    var maxY;               //
    var markedCount;        //
    var markedArray;        //
    var nCells;             //
    var cells;              //
    var players;            //
    var currentPlayer;      //

    /*
    *
    *
    */
    this.Init = function (szX, szY, bombs) {
        totalBombs = bombs;
        flaggedBombs = 0;

        maxX = szX;
        maxY = szY;

        markedCount = -1;
        markedArray = new Array();

        nCells = ((maxX + 1) * (maxY + 1));
        cells = new Array(nCells);

        players = new Array();
        players[0] = new Player("Player 1", 1, "Red");
        players[1] = new Player("Player 2", 2, "Blue");

        players[0].nextPlayer = players[1];
        players[1].nextPlayer = players[0];

        currentPlayer = players[0];
    }

    /*
    *
    *
    */
    this.CheckBounds = function (x, y) {
        return ((0 <= x) && (x <= maxX) && (0 <= y) && (y <= maxY));
    }

    /*
    * This is just a helper function for our Controller know where to touch in the Model cell array. He wouldn't know where to
    * touch if it wasn't for this function.
    *
    */
    this.ArrayIndexOf = function (x, y) {
        return x + y * (maxX + 1);
    }

    /*
    *
    *
    */
    this.MakeBoard = function () {
        for (l = 0; l <= maxX; l++) {
            for (k = 0; k <= maxY; k++) {

                cells[this.ArrayIndexOf(l, k)] = new Cell(l, k);
            }
        }

        this.PopulateWithBombs();
    }

    /*
    *
    *
    */
    this.PopulateWithBombs = function () {
        bombsToPlace = totalBombs;
        while (bombsToPlace != 0) {
            this.PlaceBombRandomLoc();
            bombsToPlace--;
        }
    }

    /*
    *
    *
    */
    this.ClearBoardModel = function () {
        for (i = 0; i <= maxX; i++) {
            for (j = 0; j <= maxY; j++) {
                with (cells[this.ArrayIndexOf(i, j)]) {
                    isExposed = false;
                    isBomb = false;
                    isFlagged = false;
                    isMarked = false;
                    neighborBombs = 0;
                }
            }
        }
    }

    /*
    *
    *
    */
    this.AddNeighbor = function (x, y) {
        if (this.CheckBounds(x, y)) {
            with (cells[this.ArrayIndexOf(x, y)]) {
                ++neighborBombs;
            }
        }
    }

    /*
    *
    *
    */
    this.PlaceBombRandomLoc = function () {
        bombPlaced = false;

        while (!bombPlaced) {
            with (Math) {
                i = floor(random() * (maxX + 1));
                j = floor(random() * (maxY + 1));
            }

            bombPlaced = this.PlaceBomb(i, j);
        }
    }

    /*
    *
    *
    */
    this.PlaceBomb = function (x, y) {
        with (cells[this.ArrayIndexOf(x, y)]) {
            if ((!isBomb) && (!isExposed)) {
                isBomb = true;
                for (i = x - 1; i <= x + 1; i++) {
                    for (j = y - 1; j <= y + 1; j++) {
                        this.AddNeighbor(i, j);
                    }
                }
                return true;
            }
            else
                return false;
        }
    }

    /*
    *
    *
    */
    this.GetTotalBombs = function () {
        return totalBombs;
    }

    /*
    *
    *
    */
    this.GetFlaggedCount = function () {
        return flaggedBombs;
    }

    /*
    *
    *
    */
    this.GetCellsToOpen = function (x, y) {
        cellsToOpen = new Array();

        with (cells[this.ArrayIndexOf(x, y)]) {
            if (!isFlagged && !isExposed) {
                this.MarkCellToOpen(x, y, cellsToOpen);
            }
        }

        return cellsToOpen;
    }

    /*
    *
    *
    */
    this.MarkCellToOpen = function (x, y, cellsToOpen) {
        ++markedCount;

        markedArray[markedCount] = new MarkedCell()
        cellsToOpen[cellsToOpen.length] = cells[this.ArrayIndexOf(x, y)];

        markedArray[markedCount].x = x;
        markedArray[markedCount].y = y;
        cells[this.ArrayIndexOf(x, y)].isMarked = true;
    }

    /*
    *
    *
    */
    this.MarkMatrixToOpen = function (x, y, cellsToOpen) {
        for (a = x - 1; a <= x + 1; a++) {
            for (b = y - 1; b <= y + 1; b++) {
                if (this.CheckBounds(a, b)) {
                    with (cells[this.ArrayIndexOf(a, b)]) {
                        if ((!isExposed) && (!isMarked) && (!isFlagged)) {
                            this.MarkCellToOpen(xPos, yPos, cellsToOpen);
                        }
                    }
                }
            }
        }
    }

    /*
    *
    *
    */
    this.OpenAllMarkedCells = function () {
        while (markedCount >= 0) {
            markedCount--;
            with (markedArray[markedCount + 1]) {
                this.OpenCell(x, y);
            }
        }
    }

    /*
    *
    *
    */
    this.OpenCell = function (x, y) {
        with (cells[this.ArrayIndexOf(x, y)]) {
            if (isBomb) {
                currentPlayer.mines++;
                isFlagged = true;
            }
            else {
                isExposed = true;
                isMarked = false;
            }

            with (cells[this.ArrayIndexOf(x, y)]) {
                if (neighborBombs == 0) {
                    this.MarkMatrixToOpen(x, y, cellsToOpen);
                }
            }
        }
    }

    /*
    *
    *
    */
    this.GetCurrentPlayer = function () {
        return currentPlayer;
    }

    /*
    *
    *
    */
    this.NextPlayerTurn = function () {
        currentPlayer = currentPlayer.nextPlayer;
    }

    /*
    *
    *
    */
    this.ResetPlayers = function () {
        for (i = 0; i < players.length; ++i) {
            players[i].mines = 0;
        }

        return players.length;
    }

    /*
    *
    *
    */
    this.AddNewPlayer = function () {
    
    }
})();