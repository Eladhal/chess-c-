using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            string move, result = "game";
            int[] moveArr;
            Chess chess = new Chess();
            Console.WriteLine(chess);
            Console.WriteLine("Let's start a game :)");
            Console.WriteLine("Movement instruction:");
            Console.WriteLine("If you want to move a tool from line 1, column a to line 2 ,column b:");
            Console.WriteLine("Press 1a2b");
            Console.WriteLine();
            Console.WriteLine("The player with the white tools start playing");
            Console.WriteLine("Go ahead and Enter the first game move:");
            move = Console.ReadLine();
            Console.WriteLine();
            while (result == "game")
            {
                if (move.Length == 4)// Check if input is 4 letters
                {
                    bool typeRightOrder = ((move[0] >= '0') && (move[0] <= '9') && (move[1] >= 'a') && (move[1] <= 'z') && (move[2] >= '0') && (move[2] <= '9') && (move[3] >= 'a') && (move[3] <= 'z'));
                    if (typeRightOrder)
                    { // Check the right order (number,letter,number,letter)
                        moveArr = ConvertToIntArray(move);
                        if (insideBoardLines(moveArr))
                        {
                            result = chess.Movemant(moveArr[0], moveArr[1], moveArr[2], moveArr[3]);
                            Console.WriteLine(chess);
                            if (result == "checkmate")
                                break;
                            if (result == "stalemate")
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Typing in the wrong order!");
                        Console.WriteLine("The right order is: first line(1-8), first column(a-h),second line(1-8), second column(a-h)");
                        Console.WriteLine();
                    }
                }
                else // if Incomplete direction
                    Console.WriteLine("Invalid direction, please enter 4 letters!");
                Console.WriteLine();
                Console.WriteLine("Enter your next move please and press enter:");
                move = Console.ReadLine();

            }

        }
        static int[] ConvertToIntArray(string str)
        {
            int[] moveArr = new int[4];
            moveArr[0] = Math.Abs(9 - (int.Parse("" + str[0])));
            moveArr[1] = (int)(str[1]) - 96;
            moveArr[2] = Math.Abs(9 - (int.Parse("" + str[2])));
            moveArr[3] = (int)(str[3]) - 96;
            return moveArr;
        }
        static bool insideBoardLines(int[] moveArr)
        {
            for (int i = 0; i < 4; i++)
                if ((moveArr[i] < 1) || (moveArr[i] > 8))// Check if is its inside boundries
                {
                    Console.WriteLine("You are trying to move a tool outside the game boundry!");
                    Console.WriteLine();
                    return false;
                }
            return true;
        }
    }
    class Chess
    {
        ChessSquare[,] chessBoard;
        ChessSquare switchSquare;
        int numberOfGameMoves;
        string chess = "";
        public Chess()
        {
            chessBoard = new ChessSquare[10, 10];
            chessBoard[0, 0] = new ChessSquare(" ", " ");
            chessBoard[9, 0] = new ChessSquare(" ", " ");
            chessBoard[0, 9] = new ChessSquare(" ", " ");
            chessBoard[9, 9] = new ChessSquare(" ", " ");
            for (int i = 1; i < 9; i++)
            {
                chessBoard[i, 0] = new ChessSquare("", "" + Math.Abs(9 - i));
                chessBoard[i, 9] = new ChessSquare("", "" + Math.Abs(9 - i));
                chessBoard[0, i] = new ChessSquare(" ", "" + (Char)(96 + i));
                chessBoard[9, i] = new ChessSquare(" ", "" + (Char)(96 + i));
            }
            for (int i = 1; i < 9; i++)
                for (int j = 1; j < 9; j++)
                {
                    if (j == 2)
                        chessBoard[j, i] = new Pawn("b", "p", j, i, "blackThreat");
                    if ((j >= 4) && (j <= 5))
                        chessBoard[j, i] = new EmptySquare(j, i, "noThreat");
                    if (j == 3)
                        chessBoard[j, i] = new EmptySquare("e", "e", j, i, "blackThreat");
                    if (j == 6)
                        chessBoard[j, i] = new EmptySquare("e", "e", j, i, "whiteThreat");
                    if (j == 7)
                        chessBoard[j, i] = new Pawn("w", "p", j, i, "whiteThreat");
                    if (j == 1)
                        switch (i)
                        {
                            case 1: chessBoard[j, i] = new Rook("b", "r", j, i, "noThreat");
                                break;
                            case 2: chessBoard[j, i] = new Knight("b", "n", j, i, "noThreat");
                                break;
                            case 3: chessBoard[j, i] = new Bishop("b", "b", j, i, "noThreat");
                                break;
                            case 4: chessBoard[j, i] = new Queen("b", "q", j, i, "noThreat");
                                break;
                            case 5: chessBoard[j, i] = new King("b", "k", j, i, "noThreat");
                                break;
                            case 6: chessBoard[j, i] = new Bishop("b", "b", j, i, "noThreat");
                                break;
                            case 7: chessBoard[j, i] = new Knight("b", "n", j, i, "noThreat");
                                break;
                            case 8: chessBoard[j, i] = new Rook("b", "r", j, i, "noThreat");
                                break;
                        }
                    if (j == 8)
                        switch (i)
                        {
                            case 1: chessBoard[j, i] = new Rook("w", "r", j, i, "noThreat");
                                break;
                            case 2: chessBoard[j, i] = new Knight("w", "n", j, i, "noThreat");
                                break;
                            case 3: chessBoard[j, i] = new Bishop("w", "b", j, i, "noThreat");
                                break;
                            case 4: chessBoard[j, i] = new Queen("w", "q", j, i, "noThreat");
                                break;
                            case 5: chessBoard[j, i] = new King("w", "k", j, i, "noThreat");
                                break;
                            case 6: chessBoard[j, i] = new Bishop("w", "b", j, i, "noThreat");
                                break;
                            case 7: chessBoard[j, i] = new Knight("w", "n", j, i, "noThreat");
                                break;
                            case 8: chessBoard[j, i] = new Rook("w", "r", j, i, "noThreat");
                                break;
                        }
                }


        }
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < chessBoard.GetLength(0); i++)
            {
                for (int j = 0; j < chessBoard.GetLength(1); j++)
                    result += chessBoard[i, j] + " ";
                result += "\n";
            }
            return result;

        }
        public string Movemant(int row1, int column1, int row2, int column2)
        {
            int i, j;
            bool colorWhite = chessBoard[row1, column1].GetColor() == "w";
            bool whiteTurn = numberOfGameMoves % 2 == 0;
            if ((row1 == row2) && (column1 == column2))
            {
                Console.WriteLine("You aren't moving the tool, it's in the same place! Try again");
                Console.WriteLine();
                return "game";
            }
            if (chessBoard[row1, column1] is EmptySquare)
            {
                Console.WriteLine("There is no tool in that squre , it's empty! Try again");
                Console.WriteLine();
                return "game";
            }
            else // Movig the tool
            {
                if (((colorWhite) && (whiteTurn)) || ((!colorWhite) && (!whiteTurn))) // Check if it is the right turn
                {
                    if (chessBoard[row1, column1].validMove(chessBoard[row2, column2], chessBoard, numberOfGameMoves))
                    {
                        switchSquare = chessBoard[row2, column2];
                        chessBoard[row2, column2] = chessBoard[row1, column1];
                        chessBoard[row2, column2].SetLine(row2);
                        chessBoard[row2, column2].SetColumn(column2);
                        chessBoard[row1, column1] = new EmptySquare(row1, column1, "");
                        if (whiteTurn)
                        {
                            for (i = 1; i <= 8; i++)
                                for (j = 1; j <= 8; j++)
                                    if ((chessBoard[i, j] is King) && (chessBoard[i, j].GetColor() == "w"))
                                    {
                                        chess = ((King)chessBoard[i, j]).CheckIfInChessThreat(chessBoard, i, j);
                                        if (chess == "chess")
                                        {
                                            chessBoard[row1, column1] = chessBoard[row2, column2];
                                            chessBoard[row1, column1].SetLine(row1);
                                            chessBoard[row1, column1].SetColumn(column1);
                                            chessBoard[row2, column2] = switchSquare;
                                            chess = "";
                                            Console.WriteLine("Invalid move! your king will be on chess threat");
                                            Console.WriteLine("Try again");
                                            Console.WriteLine();
                                            return "game";
                                        }
                                        else
                                        {
                                            numberOfGameMoves++;
                                            if (chessBoard[row2, column2] is Rook)
                                                ((Rook)chessBoard[row2, column2]).IncreaseNumberOfMovements();
                                            if (chessBoard[row2, column2] is King)
                                                ((King)chessBoard[row2, column2]).IncreaseNumberOfMovements();
                                            if (chessBoard[row2, column2] is Pawn)
                                            {
                                                ((Pawn)chessBoard[row2, column2]).IncreaseNumberOfMovements();
                                                ((Pawn)chessBoard[row2, column2]).checkIfPromtion(row2, column2, chessBoard);
                                                if (row1 - row2 == 2)
                                                    ((Pawn)chessBoard[row2, column2]).setMoveTwoSteps(numberOfGameMoves);
                                            }
                                            ClearSquaresThreats(chessBoard);
                                            SetSquaresThreats(chessBoard);
                                            for (i = 1; i <= 8; i++)
                                                for (j = 1; j <= 8; j++)
                                                    if ((chessBoard[i, j] is King) && (chessBoard[i, j].GetColor() == "b"))
                                                    {
                                                        chess = ((King)chessBoard[i, j]).CheckIfInChessThreat(chessBoard, i, j);
                                                        if (chess == "checkmate")
                                                            if (whiteTurn)
                                                            {

                                                                Console.WriteLine("checkmate!");
                                                                Console.WriteLine("congrats :)  the white player won");
                                                                return "checkmate";
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("checkmate!");
                                                                Console.WriteLine("congrats :)  the black player won");
                                                                return "checkmate";
                                                            }
                                                        if (chess == "")
                                                            if (((King)chessBoard[i, j]).CheckIfStalemate(chessBoard, i, j))
                                                            {
                                                                Console.WriteLine("Stalemate! the game is over");
                                                                return "stalemate";
                                                            }
                                                    }
                                        }
                                    }
                        }
                        else // black turn
                        {
                            for (i = 1; i <= 8; i++)
                                for (j = 1; j <= 8; j++)
                                    if ((chessBoard[i, j] is King) && (chessBoard[i, j].GetColor() == "b"))
                                    {
                                        chess = ((King)chessBoard[i, j]).CheckIfInChessThreat(chessBoard, i, j);
                                        if (chess == "chess")
                                        {
                                            chessBoard[row1, column1] = chessBoard[row2, column2];
                                            chessBoard[row1, column1].SetLine(row1);
                                            chessBoard[row1, column1].SetColumn(column1);
                                            chessBoard[row2, column2] = new EmptySquare(row2, column2, "");
                                            chess = "";
                                            Console.WriteLine("Invalid move! your king will be on chess threat");
                                            Console.WriteLine("Try again");
                                            Console.WriteLine();
                                            return "game";
                                        }
                                        else
                                        {
                                            numberOfGameMoves++;
                                            if (chessBoard[row2, column2] is Rook)
                                                ((Rook)chessBoard[row2, column2]).IncreaseNumberOfMovements();
                                            if (chessBoard[row2, column2] is King)
                                                ((King)chessBoard[row2, column2]).IncreaseNumberOfMovements();
                                            if (chessBoard[row2, column2] is Pawn)
                                            {
                                                ((Pawn)chessBoard[row2, column2]).IncreaseNumberOfMovements();
                                                ((Pawn)chessBoard[row2, column2]).checkIfPromtion(row2, column2, chessBoard);
                                                if (row2 - row1 == 2)
                                                    ((Pawn)chessBoard[row2, column2]).setMoveTwoSteps(numberOfGameMoves);
                                            }
                                            ClearSquaresThreats(chessBoard);
                                            SetSquaresThreats(chessBoard);
                                            for (i = 1; i <= 8; i++)
                                                for (j = 1; j <= 8; j++)
                                                    if ((chessBoard[i, j] is King) && (chessBoard[i, j].GetColor() == "w"))
                                                    {
                                                        chess = ((King)chessBoard[i, j]).CheckIfInChessThreat(chessBoard, i, j);
                                                        if (chess == "checkmate")
                                                            if (whiteTurn)
                                                            {

                                                                Console.WriteLine("checkmate!");
                                                                Console.WriteLine("congrats :)  the white player won");
                                                                return "checkmate";
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("checkmate!");
                                                                Console.WriteLine("congrats :)  the black player won");
                                                                return "checkmate";
                                                            }
                                                        if (chess == "")
                                                            if (((King)chessBoard[i, j]).CheckIfStalemate(chessBoard, i, j))
                                                            {
                                                                Console.WriteLine("Stalemate! the game is over");
                                                                return "stalemate";
                                                            }
                                                    }
                                        }
                                    }
                        }
                    }
                    return "game";
                }
                else
                {
                    Console.WriteLine("It's the opponent turn! Try again");
                    Console.WriteLine();
                    return "game";
                }

            }
        }
        public void ClearSquaresThreats(ChessSquare[,] chessBoard)
        {
            for (int i = 1; i <= 8; i++)
                for (int j = 1; j <= 8; j++)
                    (chessBoard[i, j]).SetChessThreat("NoThreat");
        }
        public void SetSquaresThreats(ChessSquare[,] chessBoard)
        {
            for (int i = 1; i <= 8; i++)
                for (int j = 1; j <= 8; j++)
                    chessBoard[i, j].setChessSquaresInThreat(chessBoard, i, j);
        }
    }
    class ChessSquare
    {
        int line, column;
        string name;
        string color;
        string chessThreat;
        public ChessSquare(string color, string name)
        {
            this.name = name;
            this.color = color;
        }
        public ChessSquare(string color, string name, int line, int column, string chessThreat)
        {
            this.line = line;
            this.column = column;
            this.name = name;
            this.color = color;
            this.chessThreat = chessThreat;
        }
        public string GetColor()
        {
            return color;
        }
        public string GetName()
        {
            return name;
        }
        public int GetLine()
        {
            return line;
        }
        public void SetLine(int line)
        {
            this.line = line;
        }
        public void SetColumn(int column)
        {
            this.column = column;
        }
        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetColor(string color)
        {
            this.color = color;
        }
        public int GetColumn()
        {
            return column;
        }
        public void SetChessThreat(string str)
        {
            chessThreat = str;
        }
        public string GetChessThreat()
        {
            return chessThreat;
        }
        public override string ToString()
        {
            return "" + name + color;
        }
        public virtual bool validMove(ChessSquare chesssquare, ChessSquare[,] chessBoard, int numberOfGameMoves)
        {
            return false;
        }
        public virtual bool validMoveUnderChessThreat(ChessSquare chesssquare, ChessSquare[,] chessBoard)
        {
            return false;
        }
        public virtual void setChessSquaresInThreat(ChessSquare[,] chessBoard, int row, int column)
        {
            return;
        }

    }
    class EmptySquare : ChessSquare
    {
        public EmptySquare(string color, string name, int line, int column, string chessThreat)
            : base(color, name, line, column, chessThreat) { }
        public EmptySquare(int line, int column, string chessThreat)
            : this("e", "e", line, column, chessThreat)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public override void setChessSquaresInThreat(ChessSquare[,] chessBoard, int row, int column)
        {
            base.setChessSquaresInThreat(chessBoard, row, column);
        }
        public override bool validMove(ChessSquare chesssquare, ChessSquare[,] chessBoard, int numberOfGameMoves)
        {
            return false;
        }
        public override bool validMoveUnderChessThreat(ChessSquare chesssquare, ChessSquare[,] chessBoard)
        {
            return false;
        }
    }
    class Pawn : ChessSquare
    {
        int numberOfMovements, moveTwoSteps;
        public Pawn(string color, string name, int line, int column, string chessThreat) : base(color, name, line, column, chessThreat) { }
        public override string ToString()
        {
            return base.ToString();
        }
        public int GetNumberOfMovements()
        {
            return numberOfMovements;
        }
        public override bool validMove(ChessSquare chesssquare, ChessSquare[,] chessBoard, int numberOfGameMoves)
        {
            bool firstMove = numberOfMovements == 0;
            bool ColorIsWhite = this.GetColor() == "w";
            bool squareToGoIsEmpty = chesssquare.GetName() == "e";
            bool whiteValidPawnFirstMove = ((chesssquare.GetLine() - this.GetLine() == -1) || (chesssquare.GetLine() - this.GetLine() == -2)) && (chesssquare.GetColumn() == this.GetColumn()) && (chesssquare.GetName() == "e");
            bool whiteValidPawnMove = (chesssquare.GetLine() - this.GetLine() == -1) && (chesssquare.GetColumn() == this.GetColumn()) && (chesssquare.GetName() == "e");
            bool blackValidPawnFirstMove = ((chesssquare.GetLine() - this.GetLine() == 1) || (chesssquare.GetLine() - this.GetLine() == 2)) && (chesssquare.GetColumn() == this.GetColumn()) && (chesssquare.GetName() == "e");
            bool blackValidPawnMove = (chesssquare.GetLine() - this.GetLine() == 1) && (chesssquare.GetColumn() == this.GetColumn()) && (chesssquare.GetName() == "e");
            bool captureEnemyWhite = (chesssquare.GetLine() - this.GetLine() == -1) && (Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 1) && (chesssquare.GetColor() == "b");
            bool captureEnemyBlack = (chesssquare.GetLine() - this.GetLine() == 1) && (Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 1) && (chesssquare.GetColor() == "w");

            if (ColorIsWhite)
                if (squareToGoIsEmpty)
                {
                    if (this.checkIfInPassing(chesssquare, chessBoard, numberOfGameMoves))
                        return true;
                    if (firstMove)
                        if (whiteValidPawnFirstMove)
                            return true;
                        else// validNumberOfStepsFirstMove
                        {
                            Console.WriteLine("Invalid move! Try again");
                            Console.WriteLine();
                            return false;
                        }

                    else // not first Move (white)
                    {
                        if (whiteValidPawnMove)
                            return true;
                        else
                        {
                            Console.WriteLine("Invalid move! Try again");
                            Console.WriteLine();
                            return false;
                        }
                    }
                }
                else//square is not empty
                {
                    if (captureEnemyWhite)
                        return true;
                    else
                    {
                        Console.WriteLine("The path isn't clear! Try again");
                        Console.WriteLine();
                        return false;
                    }
                }
            else// color is balck
            {
                if (squareToGoIsEmpty)
                {
                    if (this.checkIfInPassing(chesssquare, chessBoard, numberOfGameMoves))
                        return true;
                    if (firstMove)
                        if (blackValidPawnFirstMove)
                            return true;
                        else
                        {
                            Console.WriteLine("Invalid move! Try again");
                            Console.WriteLine();
                            return false;
                        }
                    else// not first move (black)
                    {
                        if (blackValidPawnMove)
                            return true;
                        else
                        {
                            Console.WriteLine("Invalid move! Try again");
                            Console.WriteLine();
                            return false;
                        }
                    }
                }
                else// squre is not empty
                {
                    if (captureEnemyBlack)
                        return true;
                    else
                    {
                        Console.WriteLine("The path isn't clear! Try again");
                        Console.WriteLine();
                        return false;
                    }

                }
            }

        }
        public override bool validMoveUnderChessThreat(ChessSquare chesssquare, ChessSquare[,] chessBoard)
        {
            bool firstMove = numberOfMovements == 0;
            bool ColorIsWhite = this.GetColor() == "w";
            bool squareToGoIsEmpty = chesssquare.GetName() == "e";
            bool whiteValidPawnFirstMove = ((chesssquare.GetLine() - this.GetLine() == -1) || (chesssquare.GetLine() - this.GetLine() == -2)) && (chesssquare.GetColumn() == this.GetColumn()) && (chesssquare.GetName() == "e");
            bool whiteValidPawnMove = (chesssquare.GetLine() - this.GetLine() == -1) && (chesssquare.GetColumn() == this.GetColumn()) && (chesssquare.GetName() == "e");
            bool blackValidPawnFirstMove = ((chesssquare.GetLine() - this.GetLine() == 1) || (chesssquare.GetLine() - this.GetLine() == 2)) && (chesssquare.GetColumn() == this.GetColumn()) && (chesssquare.GetName() == "e");
            bool blackValidPawnMove = (chesssquare.GetLine() - this.GetLine() == 1) && (chesssquare.GetColumn() == this.GetColumn()) && (chesssquare.GetName() == "e");
            bool captureEnemyWhite = (chesssquare.GetLine() - this.GetLine() == -1) && (Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 1) && (chesssquare.GetColor() == "b");
            bool captureEnemyBlack = (chesssquare.GetLine() - this.GetLine() == 1) && (Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 1) && (chesssquare.GetColor() == "w");

            if (ColorIsWhite)
                if (squareToGoIsEmpty)
                    if (firstMove)
                        if (whiteValidPawnFirstMove)
                            return true;
                        else// validNumberOfStepsFirstMove
                            return false;
                    else // not first Move (white)
                        if (whiteValidPawnMove)
                            return true;
                        else
                            return false;
                else//square is not empty
                    if (captureEnemyWhite)
                        return true;
                    else
                        return false;
            else// color is balck
                if (squareToGoIsEmpty)
                    if (firstMove)
                        if (blackValidPawnFirstMove)
                            return true;
                        else
                            return false;
                    else// not first move (black)
                        if (blackValidPawnMove)
                            return true;
                        else
                            return false;
                else// squre is not empty
                    if (captureEnemyBlack)
                        return true;
                    else
                        return false;

        }
        public bool checkIfInPassing(ChessSquare chesssquare, ChessSquare[,] chessBoard, int numberOfGameMoves)
        {
            if (this.GetColor() == "w")
            {
                if ((chesssquare.GetLine() - this.GetLine() == -1) && (Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 1))
                    if ((chessBoard[chesssquare.GetLine() + 1, chesssquare.GetColumn()] is Pawn) && (chessBoard[chesssquare.GetLine() + 1, chesssquare.GetColumn()].GetColor() == "b"))
                        if (numberOfGameMoves == ((Pawn)chessBoard[chesssquare.GetLine() + 1, chesssquare.GetColumn()]).moveTwoSteps)
                        {
                            chessBoard[chesssquare.GetLine() + 1, chesssquare.GetColumn()] = new EmptySquare(chesssquare.GetLine() + 1, chesssquare.GetColumn(), "noThreat");
                            return true;
                        }
                return false;
            }
            else// color is black
            {
                if ((chesssquare.GetLine() - this.GetLine() == 1) && (Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 1))
                    if ((chessBoard[chesssquare.GetLine() - 1, chesssquare.GetColumn()] is Pawn) && (chessBoard[chesssquare.GetLine() - 1, chesssquare.GetColumn()].GetColor() == "w"))
                        if (numberOfGameMoves == ((Pawn)chessBoard[chesssquare.GetLine() - 1, chesssquare.GetColumn()]).moveTwoSteps)
                        {
                            chessBoard[chesssquare.GetLine() - 1, chesssquare.GetColumn()] = new EmptySquare(chesssquare.GetLine() - 1, chesssquare.GetColumn(), "noThreat");
                            return true;
                        }
                return false;
            }


        }
        public void checkIfPromtion(int row, int column, ChessSquare[,] chessBoard)
        {
            int number;
            if (this.GetColor() == "w")
            {
                if (row == 1)
                {
                    Console.WriteLine("Your pawn has reached to the last row! you can make a promotion");
                    Console.WriteLine("Choose the promotion you like and press Enter:");
                    Console.WriteLine("Press 1 for Rook");
                    Console.WriteLine("Press 2 for Knight");
                    Console.WriteLine("Press 3 for Bishop");
                    Console.WriteLine("Press 4 for Queen");
                    number = int.Parse(Console.ReadLine());
                    switch (number)
                    {
                        case 1: chessBoard[1, column] = new Rook("w", "r", 1, column, "noThreat");
                            break;
                        case 2: chessBoard[1, column] = new Knight("w", "k", 1, column, "noThreat");
                            break;
                        case 3: chessBoard[1, column] = new Bishop("w", "b", 1, column, "noThreat");
                            break;
                        case 4: chessBoard[1, column] = new Queen("w", "q", 1, column, "noThreat");
                            break;
                    }
                }
            }
            else // color is black
            {
                if (row == 8)
                {
                    Console.WriteLine("Your pawn has reached to the last row! you can make a promotion");
                    Console.WriteLine("Choose the promotion you like and press Enter:");
                    Console.WriteLine("Press 1 for Rook");
                    Console.WriteLine("Press 2 for Knight");
                    Console.WriteLine("Press 3 for Bishop");
                    Console.WriteLine("Press 4 for Queen");
                    number = int.Parse(Console.ReadLine());
                    switch (number)
                    {
                        case 1: chessBoard[8, column] = new Rook("b", "r", 8, column, "noThreat");
                            break;
                        case 2: chessBoard[8, column] = new Knight("b", "k", 8, column, "noThreat");
                            break;
                        case 3: chessBoard[8, column] = new Bishop("b", "b", 8, column, "noThreat");
                            break;
                        case 4: chessBoard[8, column] = new Queen("b", "q", 8, column, "noThreat");
                            break;
                    }
                }
            }

        }
        public override void setChessSquaresInThreat(ChessSquare[,] chessBoard, int row, int column)
        {
            if (this.GetColor() == "w")
            {
                if (((chessBoard[row - 1, column - 1]).GetChessThreat() == "blackThreat") || ((chessBoard[row - 1, column - 1]).GetChessThreat() == "white&blackThreat"))
                    (chessBoard[row - 1, column - 1]).SetChessThreat("white&blackThreat");
                else
                    (chessBoard[row - 1, column - 1]).SetChessThreat("whiteThreat");

                if (((chessBoard[row - 1, column + 1]).GetChessThreat() == "blackThreat") || ((chessBoard[row - 1, column + 1]).GetChessThreat() == "white&blackThreat"))
                    (chessBoard[row - 1, column + 1]).SetChessThreat("white&blackThreat");
                else
                    (chessBoard[row - 1, column + 1]).SetChessThreat("whiteThreat");
            }
            else
            {
                if (((chessBoard[row + 1, column - 1]).GetChessThreat() == "whiteThreat") || ((chessBoard[row + 1, column - 1]).GetChessThreat() == "white&blackThreat"))
                    (chessBoard[row + 1, column - 1]).SetChessThreat("white&blackThreat");
                else
                    (chessBoard[row + 1, column - 1]).SetChessThreat("blackThreat");

                if (((chessBoard[row + 1, column + 1]).GetChessThreat() == "whiteThreat") || ((chessBoard[row + 1, column + 1]).GetChessThreat() == "white&blackThreat"))
                    (chessBoard[row + 1, column + 1]).SetChessThreat("white&blackThreat");
                else
                    (chessBoard[row + 1, column + 1]).SetChessThreat("blackThreat");
            }
        }
        public void IncreaseNumberOfMovements()
        {
            numberOfMovements++;
        }
        public void setMoveTwoSteps(int movement)
        {
            moveTwoSteps = movement;
        }

    }
    class Rook : ChessSquare
    {
        int numberOfMovements;
        public Rook(string color, string name, int line, int column, string chessThreat) : base(color, name, line, column, chessThreat) { }
        public override string ToString()
        {
            return base.ToString();
        }
        public override bool validMove(ChessSquare chesssquare, ChessSquare[,] chessBoard, int numberOfGameMoves)
        {
            bool validRookMove = (chesssquare.GetLine() == this.GetLine()) || (chesssquare.GetColumn() == this.GetColumn());
            if (validRookMove)
                if (chesssquare.GetLine() == this.GetLine())
                    if (chesssquare.GetColumn() > this.GetColumn())
                    {
                        for (int i = this.GetColumn() + 1; i < chesssquare.GetColumn(); i++)
                            if (chessBoard[this.GetLine(), i].GetName() != "e")
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                        {
                            Console.WriteLine("The path isn't clear! Try again");
                            Console.WriteLine();
                            return false;
                        }
                    }
                    else // (chesssquare.GetColumn() < this.GetColumn())
                    {
                        for (int i = this.GetColumn() - 1; i > chesssquare.GetColumn(); i--)
                            if (chessBoard[this.GetLine(), i].GetName() != "e")
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                        {
                            Console.WriteLine("The path isn't clear! Try again");
                            Console.WriteLine();
                            return false;
                        }
                    }

                else // (chesssquare.GetColumn() == this.GetColumn())  
                {
                    if (chesssquare.GetLine() > this.GetLine())
                    {
                        for (int i = this.GetLine() + 1; i < chesssquare.GetLine(); i++)
                            if (chessBoard[i, this.GetColumn()].GetName() != "e")
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                return false;
                            }
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                        {
                            Console.WriteLine("The path isn't clear! Try again");
                            Console.WriteLine();
                            return false;
                        }
                    }
                    else // (chesssquare.GetLine() < this.GetLine())
                    {
                        for (int i = this.GetLine() - 1; i > chesssquare.GetLine(); i--)
                            if (chessBoard[i, this.GetColumn()].GetName() != "e")
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                        {
                            Console.WriteLine("The path isn't clear! Try again");
                            Console.WriteLine();
                            return false;
                        }

                    }
                } // else (chesssquare.GetColumn() == this.GetColumn())  
            else // !(validRookMove)
            {
                Console.WriteLine("Invalid move! Try again");
                Console.WriteLine();
                return false;
            }
        }
        public override bool validMoveUnderChessThreat(ChessSquare chesssquare, ChessSquare[,] chessBoard)
        {
            bool validRookMove = (chesssquare.GetLine() == this.GetLine()) || (chesssquare.GetColumn() == this.GetColumn());
            if (validRookMove)
                if (chesssquare.GetLine() == this.GetLine())
                    if (chesssquare.GetColumn() > this.GetColumn())
                    {
                        for (int i = this.GetColumn() + 1; i < chesssquare.GetColumn(); i++)
                            if (chessBoard[this.GetLine(), i].GetName() != "e")
                                return false;
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                            return false;
                    }
                    else // (chesssquare.GetColumn() < this.GetColumn())
                    {
                        for (int i = this.GetColumn() - 1; i > chesssquare.GetColumn(); i--)
                            if (chessBoard[this.GetLine(), i].GetName() != "e")
                                return false;
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                            return false;
                    }

                else // (chesssquare.GetColumn() == this.GetColumn())  
                {
                    if (chesssquare.GetLine() > this.GetLine())
                    {
                        for (int i = this.GetLine() + 1; i < chesssquare.GetLine(); i++)
                            if (chessBoard[i, this.GetColumn()].GetName() != "e")
                                return false;
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                            return false;
                    }
                    else // (chesssquare.GetLine() < this.GetLine())
                    {
                        for (int i = this.GetLine() - 1; i > chesssquare.GetLine(); i--)
                            if (chessBoard[i, this.GetColumn()].GetName() != "e")
                                return false;
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                            return false;
                    }
                } // else (chesssquare.GetColumn() == this.GetColumn())  
            else // !(validRookMove)
                return false;
        }
        public override void setChessSquaresInThreat(ChessSquare[,] chessBoard, int row, int column)
        {
            for (int i = row + 1; i <= 8; i++)
            {
                if (chessBoard[i, column] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[i, column]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[i, column]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[i, column]).GetChessThreat() == "blackThreat") || ((chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[i, column]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[i, column]).GetChessThreat() == "whiteThreat") || ((chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[i, column]).SetChessThreat("blackThreat");
                    break;
                }
            }
            for (int i = row - 1; i >= 1; i--)
            {
                if (chessBoard[i, column] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[i, column]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[i, column]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[i, column]).GetChessThreat() == "blackThreat") || ((chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[i, column]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[i, column]).GetChessThreat() == "whiteThreat") || ((chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[i, column]).SetChessThreat("blackThreat");
                    break;
                }
            }
            for (int i = column + 1; i <= 8; i++)
            {
                if (chessBoard[row, i] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[row, i]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[row, i]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row, i]).GetChessThreat() == "blackThreat") || ((chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row, i]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row, i]).GetChessThreat() == "whiteThreat") || ((chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row, i]).SetChessThreat("blackThreat");
                    break;
                }
            }
            for (int i = column - 1; i >= 1; i--)
            {
                if (chessBoard[row, i] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[row, i]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[row, i]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row, i]).GetChessThreat() == "blackThreat") || ((chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row, i]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row, i]).GetChessThreat() == "whiteThreat") || ((chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row, i]).SetChessThreat("blackThreat");
                    break;
                }
            }

        }
        public void IncreaseNumberOfMovements()
        {
            numberOfMovements++;
        }
        public int GetNumberOfMovements()
        {
            return numberOfMovements;
        }
    }
    class Knight : ChessSquare
    {
        public Knight(string color, string name, int line, int column, string chessThreat) : base(color, name, line, column, chessThreat) { }
        public override string ToString()
        {
            return base.ToString();
        }
        public override bool validMove(ChessSquare chesssquare, ChessSquare[,] chessBoard, int numberOfGameMoves)
        {
            bool validKnightMove = ((Math.Abs(chesssquare.GetLine() - this.GetLine()) == 2) && (Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 1)) || ((Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 2) && (Math.Abs(chesssquare.GetLine() - this.GetLine()) == 1));
            if (validKnightMove)
                if (this.GetColor() != chesssquare.GetColor())
                    return true;
                else
                {
                    Console.WriteLine("The path isn't clear! Try again");
                    Console.WriteLine();
                    return false;
                }
            else
            {
                Console.WriteLine("Invalid move! Try again");
                Console.WriteLine();
                return false;
            }

        }
        public override bool validMoveUnderChessThreat(ChessSquare chesssquare, ChessSquare[,] chessBoard)
        {
            bool validKnightMove = ((Math.Abs(chesssquare.GetLine() - this.GetLine()) == 2) && (Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 1)) || ((Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 2) && (Math.Abs(chesssquare.GetLine() - this.GetLine()) == 1));
            if (validKnightMove)
                if (this.GetColor() != chesssquare.GetColor())
                    return true;
                else
                    return false;
            else
                return false;
        }
        public override void setChessSquaresInThreat(ChessSquare[,] chessBoard, int row, int column)
        {
            if ((row > 1) && (row < 8) && (column > 1) && (column < 8))
            {
                int[] movesArray1 = { 1, 1, 2, 2, -1, -1, -2, -2 };
                int[] movesArray2 = { 2, -2, 1, -1, 2, -2, 1, -1 };
                for (int i = 0; i < 8; i++)
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "blackThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "whiteThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("blackThreat");
                }
            }
            if ((row == 1) && (column == 1))
            {
                int[] movesArray1 = { 1, 2 };
                int[] movesArray2 = { 2, 1 };
                for (int i = 0; i < 2; i++)
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "blackThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "whiteThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("blackThreat");
                }
            }
            if ((row == 1) && (column > 1) && (column < 8))
            {
                int[] movesArray1 = { 1, 1, 2, 2 };
                int[] movesArray2 = { 2, -2, 1, -1 };
                for (int i = 0; i < 4; i++)
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "blackThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "whiteThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("blackThreat");
                }
            }
            if ((row == 1) && (column == 8))
            {
                int[] movesArray1 = { 1, 2 };
                int[] movesArray2 = { -2, -1 };
                for (int i = 0; i < 2; i++)
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "blackThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "whiteThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("blackThreat");
                }
            }
            if ((column == 8) && (row > 1) && (row < 8))
            {

                int[] movesArray1 = { 1, 2, -1, -2 };
                int[] movesArray2 = { -2, -1, -2, -1 };
                for (int i = 0; i < 4; i++)
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "blackThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "whiteThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("blackThreat");
                }
            }
            if ((row == 8) && (column == 8))
            {

                int[] movesArray1 = { -1, -2 };
                int[] movesArray2 = { -2, -1 };
                for (int i = 0; i < 2; i++)
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "blackThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "whiteThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("blackThreat");
                }
            }
            if ((row == 8) && (column > 1) && (column < 8))
            {

                int[] movesArray1 = { -1, -1, -2, -2 };
                int[] movesArray2 = { 2, -2, 1, -1 };
                for (int i = 0; i < 4; i++)
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "blackThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "whiteThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("blackThreat");
                }
            }

            if ((row == 8) && (column == 1))
            {
                int[] movesArray1 = { -1, -2 };
                int[] movesArray2 = { 2, 1 };
                for (int i = 0; i < 2; i++)
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "blackThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "whiteThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("blackThreat");
                }
            }
            if ((row > 1) && (row < 8) && (column == 1))
            {

                int[] movesArray1 = { 1, 2, -1, -2 };
                int[] movesArray2 = { 2, 1, 2, 1 };
                for (int i = 0; i < 4; i++)
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "blackThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "whiteThreat") || ((chessBoard[row + movesArray1[i], column + movesArray2[i]]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + movesArray1[i], column + movesArray2[i]]).SetChessThreat("blackThreat");
                }
            }

        }
    }
    class Bishop : ChessSquare
    {
        public Bishop(string color, string name, int line, int column, string chessThreat) : base(color, name, line, column, chessThreat) { }
        public override string ToString()
        {
            return base.ToString();
        }
        public override bool validMove(ChessSquare chesssquare, ChessSquare[,] chessBoard, int numberOfGameMoves)
        {
            bool validBishopMove = Math.Abs(chesssquare.GetLine() - this.GetLine()) == Math.Abs(chesssquare.GetColumn() - this.GetColumn());
            if (validBishopMove)
            {
                if (chesssquare.GetColumn() > this.GetColumn())
                    if (chesssquare.GetLine() < this.GetLine())
                    {
                        for (int i = this.GetColumn() + 1, j = this.GetLine() - 1; (i < chesssquare.GetColumn()) && (j > chesssquare.GetLine()); i++, j--)
                            if (chessBoard[j, i].GetName() != "e")
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                        {
                            Console.WriteLine("The path isn't clear! Try again");
                            Console.WriteLine();
                            return false;
                        }
                    }
                    else // (chesssquare.GetLine() > this.GetLine())
                    {
                        for (int i = this.GetColumn() + 1, j = this.GetLine() + 1; (i < chesssquare.GetColumn()) && (j < chesssquare.GetLine()); i++, j++)
                            if (chessBoard[j, i].GetName() != "e")
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                        {
                            Console.WriteLine("The path isn't clear! Try again");
                            Console.WriteLine();
                            return false;
                        }
                    }
                else // (chesssquare.GetColumn()<this.GetColumn())
                    if (chesssquare.GetLine() < this.GetLine())
                    {
                        for (int i = this.GetColumn() - 1, j = this.GetLine() - 1; (i > chesssquare.GetColumn()) && (j > chesssquare.GetLine()); i--, j--)
                            if (chessBoard[j, i].GetName() != "e")
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                        {
                            Console.WriteLine("The path isn't clear! Try again");
                            Console.WriteLine();
                            return false;
                        }
                    }
                    else // (chesssquare.GetLine() > this.GetLine())
                    {
                        for (int i = this.GetColumn() - 1, j = this.GetLine() + 1; (i > chesssquare.GetColumn()) && (j < chesssquare.GetLine()); i--, j++)
                            if (chessBoard[j, i].GetName() != "e")
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                        {
                            Console.WriteLine("The path isn't clear! Try again");
                            Console.WriteLine();
                            return false;
                        }
                    }
            }
            else
            {
                Console.WriteLine("Invalid move! Try again");
                Console.WriteLine();
                return false;
            }
        }
        public override bool validMoveUnderChessThreat(ChessSquare chesssquare, ChessSquare[,] chessBoard)
        {
            bool validBishopMove = Math.Abs(chesssquare.GetLine() - this.GetLine()) == Math.Abs(chesssquare.GetColumn() - this.GetColumn());
            if (validBishopMove)
            {
                if (chesssquare.GetColumn() > this.GetColumn())
                    if (chesssquare.GetLine() < this.GetLine())
                    {
                        for (int i = this.GetColumn() + 1, j = this.GetLine() - 1; (i < chesssquare.GetColumn()) && (j > chesssquare.GetLine()); i++, j--)
                            if (chessBoard[j, i].GetName() != "e")
                                return false;
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                            return false;
                    }
                    else // (chesssquare.GetLine() > this.GetLine())
                    {
                        for (int i = this.GetColumn() + 1, j = this.GetLine() + 1; (i < chesssquare.GetColumn()) && (j < chesssquare.GetLine()); i++, j++)
                            if (chessBoard[j, i].GetName() != "e")
                                return false;
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                            return false;
                    }
                else // (chesssquare.GetColumn()<this.GetColumn())
                    if (chesssquare.GetLine() < this.GetLine())
                    {
                        for (int i = this.GetColumn() - 1, j = this.GetLine() - 1; (i > chesssquare.GetColumn()) && (j > chesssquare.GetLine()); i--, j--)
                            if (chessBoard[j, i].GetName() != "e")
                                return false;
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                            return false;
                    }
                    else // (chesssquare.GetLine() > this.GetLine())
                    {
                        for (int i = this.GetColumn() - 1, j = this.GetLine() + 1; (i > chesssquare.GetColumn()) && (j < chesssquare.GetLine()); i--, j++)
                            if (chessBoard[j, i].GetName() != "e")
                                return false;
                        if (chesssquare.GetColor() != this.GetColor())
                            return true;
                        else
                            return false;
                    }
            }
            else
                return false;
        }
        public override void setChessSquaresInThreat(ChessSquare[,] chessBoard, int row, int column)
        {
            for (int i = column + 1, j = row - 1; (i <= 8) && (j >= 1); i++, j--)
            {
                if (chessBoard[j, i] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[j, i]).GetChessThreat() == "blackThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[j, i]).GetChessThreat() == "whiteThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("blackThreat");
                    break;
                }

            }
            for (int i = column + 1, j = row + 1; (i <= 8) && (j <= 8); i++, j++)
            {

                if (chessBoard[j, i] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[j, i]).GetChessThreat() == "blackThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[j, i]).GetChessThreat() == "whiteThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("blackThreat");
                    break;
                }
            }
            for (int i = column - 1, j = row + 1; (i >= 1) && (j <= 8); i--, j++)
            {
                if (chessBoard[j, i] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[j, i]).GetChessThreat() == "blackThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[j, i]).GetChessThreat() == "whiteThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("blackThreat");
                    break;
                }
            }
            for (int i = column - 1, j = row - 1; (i >= 1) && (j >= 1); i--, j--)
            {
                if (chessBoard[j, i] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[j, i]).GetChessThreat() == "blackThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[j, i]).GetChessThreat() == "whiteThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("blackThreat");
                    break;
                }
            }
        }
    }
    class Queen : ChessSquare
    {
        public Queen(string color, string name, int line, int column, string chessThreat) : base(color, name, line, column, chessThreat) { }
        public override string ToString()
        {
            return base.ToString();
        }
        public override bool validMove(ChessSquare chesssquare, ChessSquare[,] chessBoard, int numberOfGameMoves)
        {
            bool validRookMove = (chesssquare.GetLine() == this.GetLine()) || (chesssquare.GetColumn() == this.GetColumn());
            bool validBishopMove = Math.Abs(chesssquare.GetLine() - this.GetLine()) == Math.Abs(chesssquare.GetColumn() - this.GetColumn());
            bool validQueenMove = validRookMove || validBishopMove;
            if (validQueenMove)
                if (validRookMove)
                    if (chesssquare.GetLine() == this.GetLine())
                        if (chesssquare.GetColumn() > this.GetColumn())
                        {
                            for (int i = this.GetColumn() + 1; i < chesssquare.GetColumn(); i++)
                                if (chessBoard[this.GetLine(), i].GetName() != "e")
                                {
                                    Console.WriteLine("The path isn't clear! Try again");
                                    Console.WriteLine();
                                    return false;
                                }
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        }
                        else // (chesssquare.GetColumn() < this.GetColumn())
                        {
                            for (int i = this.GetColumn() - 1; i > chesssquare.GetColumn(); i--)
                                if (chessBoard[this.GetLine(), i].GetName() != "e")
                                {
                                    Console.WriteLine("The path isn't clear! Try again");
                                    Console.WriteLine();
                                    return false;
                                }
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        }

                    else // (chesssquare.GetColumn() == this.GetColumn())  
                    {
                        if (chesssquare.GetLine() > this.GetLine())
                        {
                            for (int i = this.GetLine() + 1; i < chesssquare.GetLine(); i++)
                                if (chessBoard[i, this.GetColumn()].GetName() != "e")
                                {
                                    Console.WriteLine("The path isn't clear! Try again");
                                    return false;
                                }
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        }
                        else // (chesssquare.GetLine() < this.GetLine())
                        {
                            for (int i = this.GetLine() - 1; i > chesssquare.GetLine(); i--)
                                if (chessBoard[i, this.GetColumn()].GetName() != "e")
                                {
                                    Console.WriteLine("The path isn't clear! Try again");
                                    Console.WriteLine();
                                    return false;
                                }
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }

                        }
                    } // else (chesssquare.GetColumn() == this.GetColumn())  
                else // it is a validBishopMove
                    if (chesssquare.GetColumn() > this.GetColumn())
                        if (chesssquare.GetLine() < this.GetLine())
                        {
                            for (int i = this.GetColumn() + 1, j = this.GetLine() - 1; (i < chesssquare.GetColumn()) && (j > chesssquare.GetLine()); i++, j--)
                                if (chessBoard[j, i].GetName() != "e")
                                {
                                    Console.WriteLine("The path isn't clear! Try again");
                                    Console.WriteLine();
                                    return false;
                                }
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        }
                        else // (chesssquare.GetLine() > this.GetLine())
                        {
                            for (int i = this.GetColumn() + 1, j = this.GetLine() + 1; (i < chesssquare.GetColumn()) && (j < chesssquare.GetLine()); i++, j++)
                                if (chessBoard[j, i].GetName() != "e")
                                {
                                    Console.WriteLine("The path isn't clear! Try again");
                                    Console.WriteLine();
                                    return false;
                                }
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        }
                    else // (chesssquare.GetColumn()<this.GetColumn())
                        if (chesssquare.GetLine() < this.GetLine())
                        {
                            for (int i = this.GetColumn() - 1, j = this.GetLine() - 1; (i > chesssquare.GetColumn()) && (j > chesssquare.GetLine()); i--, j--)
                                if (chessBoard[j, i].GetName() != "e")
                                {
                                    Console.WriteLine("The path isn't clear! Try again");
                                    Console.WriteLine();
                                    return false;
                                }
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        }
                        else // (chesssquare.GetLine() > this.GetLine())
                        {
                            for (int i = this.GetColumn() - 1, j = this.GetLine() + 1; (i > chesssquare.GetColumn()) && (j < chesssquare.GetLine()); i--, j++)
                                if (chessBoard[j, i].GetName() != "e")
                                {
                                    Console.WriteLine("The path isn't clear! Try again");
                                    Console.WriteLine();
                                    return false;
                                }
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                            {
                                Console.WriteLine("The path isn't clear! Try again");
                                Console.WriteLine();
                                return false;
                            }
                        }
            else
            {
                Console.WriteLine("Invalid move! Try again");
                Console.WriteLine();
                return false;
            }
        }
        public override bool validMoveUnderChessThreat(ChessSquare chesssquare, ChessSquare[,] chessBoard)
        {
            bool validRookMove = (chesssquare.GetLine() == this.GetLine()) || (chesssquare.GetColumn() == this.GetColumn());
            bool validBishopMove = Math.Abs(chesssquare.GetLine() - this.GetLine()) == Math.Abs(chesssquare.GetColumn() - this.GetColumn());
            bool validQueenMove = validRookMove || validBishopMove;
            if (validQueenMove)
                if (validRookMove)
                    if (chesssquare.GetLine() == this.GetLine())
                        if (chesssquare.GetColumn() > this.GetColumn())
                        {
                            for (int i = this.GetColumn() + 1; i < chesssquare.GetColumn(); i++)
                                if (chessBoard[this.GetLine(), i].GetName() != "e")
                                    return false;
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                                return false;
                        }
                        else // (chesssquare.GetColumn() < this.GetColumn())
                        {
                            for (int i = this.GetColumn() - 1; i > chesssquare.GetColumn(); i--)
                                if (chessBoard[this.GetLine(), i].GetName() != "e")
                                    return false;
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                                return false;
                        }

                    else // (chesssquare.GetColumn() == this.GetColumn())  
                    {
                        if (chesssquare.GetLine() > this.GetLine())
                        {
                            for (int i = this.GetLine() + 1; i < chesssquare.GetLine(); i++)
                                if (chessBoard[i, this.GetColumn()].GetName() != "e")
                                    return false;
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                                return false;
                        }
                        else // (chesssquare.GetLine() < this.GetLine())
                        {
                            for (int i = this.GetLine() - 1; i > chesssquare.GetLine(); i--)
                                if (chessBoard[i, this.GetColumn()].GetName() != "e")
                                    return false;
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                                return false;
                        }
                    } // else (chesssquare.GetColumn() == this.GetColumn())  
                else // it is a validBishopMove
                    if (chesssquare.GetColumn() > this.GetColumn())
                        if (chesssquare.GetLine() < this.GetLine())
                        {
                            for (int i = this.GetColumn() + 1, j = this.GetLine() - 1; (i < chesssquare.GetColumn()) && (j > chesssquare.GetLine()); i++, j--)
                                if (chessBoard[j, i].GetName() != "e")
                                    return false;
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                                return false;
                        }
                        else // (chesssquare.GetLine() > this.GetLine())
                        {
                            for (int i = this.GetColumn() + 1, j = this.GetLine() + 1; (i < chesssquare.GetColumn()) && (j < chesssquare.GetLine()); i++, j++)
                                if (chessBoard[j, i].GetName() != "e")
                                    return false;
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                                return false;
                        }
                    else // (chesssquare.GetColumn()<this.GetColumn())
                        if (chesssquare.GetLine() < this.GetLine())
                        {
                            for (int i = this.GetColumn() - 1, j = this.GetLine() - 1; (i > chesssquare.GetColumn()) && (j > chesssquare.GetLine()); i--, j--)
                                if (chessBoard[j, i].GetName() != "e")
                                    return false;
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                                return false;
                        }
                        else // (chesssquare.GetLine() > this.GetLine())
                        {
                            for (int i = this.GetColumn() - 1, j = this.GetLine() + 1; (i > chesssquare.GetColumn()) && (j < chesssquare.GetLine()); i--, j++)
                                if (chessBoard[j, i].GetName() != "e")
                                    return false;
                            if (chesssquare.GetColor() != this.GetColor())
                                return true;
                            else
                                return false;
                        }
            else
                return false;
        }
        public override void setChessSquaresInThreat(ChessSquare[,] chessBoard, int row, int column)
        {
            for (int i = row + 1; i <= 8; i++)
            {
                if (chessBoard[i, column] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[i, column]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[i, column]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[i, column]).GetChessThreat() == "blackThreat") || ((chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[i, column]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[i, column]).GetChessThreat() == "whiteThreat") || ((chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[i, column]).SetChessThreat("blackThreat");
                    break;
                }
            }
            for (int i = row - 1; i >= 1; i--)
            {
                if (chessBoard[i, column] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[i, column]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[i, column]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[i, column]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[i, column]).GetChessThreat() == "blackThreat") || ((chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[i, column]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[i, column]).GetChessThreat() == "whiteThreat") || ((chessBoard[i, column]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[i, column]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[i, column]).SetChessThreat("blackThreat");
                    break;
                }
            }
            for (int i = column + 1; i <= 8; i++)
            {
                if (chessBoard[row, i] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[row, i]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[row, i]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row, i]).GetChessThreat() == "blackThreat") || ((chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row, i]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row, i]).GetChessThreat() == "whiteThreat") || ((chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row, i]).SetChessThreat("blackThreat");
                    break;
                }
            }
            for (int i = column - 1; i >= 1; i--)
            {
                if (chessBoard[row, i] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[row, i]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[row, i]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[row, i]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[row, i]).GetChessThreat() == "blackThreat") || ((chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row, i]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[row, i]).GetChessThreat() == "whiteThreat") || ((chessBoard[row, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row, i]).SetChessThreat("blackThreat");
                    break;
                }
            }
            for (int i = column + 1, j = row - 1; (i <= 8) && (j >= 1); i++, j--)
            {
                if (chessBoard[j, i] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[j, i]).GetChessThreat() == "blackThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[j, i]).GetChessThreat() == "whiteThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("blackThreat");
                    break;
                }
            }
            for (int i = column + 1, j = row + 1; (i <= 8) && (j <= 8); i++, j++)
            {

                if (chessBoard[j, i] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[j, i]).GetChessThreat() == "blackThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[j, i]).GetChessThreat() == "whiteThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("blackThreat");
                    break;
                }
            }
            for (int i = column - 1, j = row + 1; (i >= 1) && (j <= 8); i--, j++)
            {

                if (chessBoard[j, i] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[j, i]).GetChessThreat() == "blackThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[j, i]).GetChessThreat() == "whiteThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("blackThreat");
                    break;
                }
            }
            for (int i = column - 1, j = row - 1; (i >= 1) && (j >= 1); i--, j--)
            {

                if (chessBoard[j, i] is EmptySquare)
                    if (this.GetColor() == "w")
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "blackThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if ((((EmptySquare)chessBoard[j, i]).GetChessThreat() == "whiteThreat") || (((EmptySquare)chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            ((EmptySquare)chessBoard[j, i]).SetChessThreat("blackThreat");
                else
                {
                    if (this.GetColor() == "w")
                        if (((chessBoard[j, i]).GetChessThreat() == "blackThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("whiteThreat");
                    else
                        if (((chessBoard[j, i]).GetChessThreat() == "whiteThreat") || ((chessBoard[j, i]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[j, i]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[j, i]).SetChessThreat("blackThreat");
                    break;
                }
            }
        }
    }
    class King : ChessSquare
    {
        int numberOfMovements;
        bool underChessThreat;
        public King(string color, string name, int line, int column, string chessThreat) : base(color, name, line, column, chessThreat) { }
        public override string ToString()
        {
            return base.ToString();
        }
        public override bool validMove(ChessSquare chesssquare, ChessSquare[,] chessBoard, int numberOfGameMoves)
        {
            bool validKingMove = ((Math.Abs(chesssquare.GetLine() - this.GetLine()) == 1) && (this.GetColumn() == chesssquare.GetColumn())) || ((Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 1) && (chesssquare.GetLine() == this.GetLine())) || ((Math.Abs(chesssquare.GetLine() - this.GetLine()) == 1) && (Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 1));
            if (this.CheckIfCastling(chesssquare, chessBoard))
                return true;
            if (validKingMove)
                if (this.GetColor() == "w")
                    if ((chesssquare is EmptySquare) || (chesssquare.GetColor() == "b"))
                        if (((chesssquare).GetChessThreat() == "blackThreat") || ((chesssquare).GetChessThreat() == "white&blackThreat"))
                        {
                            Console.WriteLine("This square is on chess threat! you cannot move to it");
                            Console.WriteLine("Try again");
                            Console.WriteLine();
                            return false;
                        }
                        else
                            return true;
                    else
                    {
                        Console.WriteLine("The path isn't clear! Try again");
                        Console.WriteLine();
                        return false;
                    }
                else // color is black
                    if ((chesssquare is EmptySquare) || (chesssquare.GetColor() == "w"))
                        if (((chesssquare).GetChessThreat() == "whiteThreat") || ((chesssquare).GetChessThreat() == "white&blackThreat"))
                        {
                            Console.WriteLine("This square is on chess threat! you cannot move to it");
                            Console.WriteLine("Try again");
                            Console.WriteLine();
                            return false;
                        }
                        else
                            return true;
                    else
                    {
                        Console.WriteLine("The path isn't clear! Try again");
                        Console.WriteLine();
                        return false;
                    }
            else
            {
                Console.WriteLine("Invalid move! Try again");
                Console.WriteLine();
                return false;
            }
        }
        public override bool validMoveUnderChessThreat(ChessSquare chesssquare, ChessSquare[,] chessBoard)
        {
            bool validKingMove = ((Math.Abs(chesssquare.GetLine() - this.GetLine()) == 1) && (this.GetColumn() == chesssquare.GetColumn())) || ((Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 1) && (chesssquare.GetLine() == this.GetLine())) || ((Math.Abs(chesssquare.GetLine() - this.GetLine()) == 1) && (Math.Abs(chesssquare.GetColumn() - this.GetColumn()) == 1));
            if (validKingMove)
            {
                if (this.GetColor() == "w")
                    if ((chesssquare.GetColor() == "e") || (chesssquare.GetColor() == "b"))
                        if ((chesssquare.GetChessThreat() == "blackThreat") || (chesssquare.GetChessThreat() == "white&blackThreat"))
                            return false;
                        else
                            return true;
                    else
                        return false;
                else // color is black
                    if ((chesssquare.GetColor() == "e") || (chesssquare.GetColor() == "w"))
                        if ((chesssquare.GetChessThreat() == "whiteThreat") || (chesssquare.GetChessThreat() == "white&blackThreat"))
                            return false;
                        else
                            return true;
                    else
                        return false;
            }
            else
                return false;
        }
        public string CheckIfInChessThreat(ChessSquare[,] chessBoard, int row, int column)
        {
            if (((King)chessBoard[row, column]).CheckIfChessThreatFromPawn(chessBoard, row, column) == "chess")
            {
                underChessThreat = true;
                return "chess";
            }
            if (((King)chessBoard[row, column]).CheckIfChessThreatFromPawn(chessBoard, row, column) == "checkmate")
                return "checkmate";
            if (((King)chessBoard[row, column]).CheckIfChessThreatFromKnight(chessBoard, row, column) == "chess")
            {
                underChessThreat = true;
                return "chess";
            }
            if (((King)chessBoard[row, column]).CheckIfChessThreatFromKnight(chessBoard, row, column) == "checkmate")
                return "checkmate";
            if (((King)chessBoard[row, column]).CheckIfChessThreatFromRookOrQueen(chessBoard, row, column) == "chess")
            {
                underChessThreat = true;
                return "chess";
            }
            if (((King)chessBoard[row, column]).CheckIfChessThreatFromRookOrQueen(chessBoard, row, column) == "checkmate")
                return "checkmate";
            if (((King)chessBoard[row, column]).CheckIfChessThreatFromBishopOrQueen(chessBoard, row, column) == "chess")
            {
                underChessThreat = true;
                return "chess";
            }
            if (((King)chessBoard[row, column]).CheckIfChessThreatFromBishopOrQueen(chessBoard, row, column) == "checkmate")
                return "checkmate";
            underChessThreat = false;
            return "";
        }
        public string CheckIfChessThreatFromPawn(ChessSquare[,] chessBoard, int row, int column)
        {
            if ((chessBoard[row - 1, column - 1] is Pawn) && (chessBoard[row - 1, column - 1].GetColor() == "b") && (this.GetColor() == "w"))
                if (((King)chessBoard[row, column]).IsCheckMate(false, row - 1, column - 1, chessBoard))
                    return "checkmate";
                else
                {
                    Console.WriteLine("Chess for white King!");
                    Console.WriteLine();
                    return "chess";
                }
            if ((chessBoard[row - 1, column + 1] is Pawn) && (chessBoard[row - 1, column + 1].GetColor() == "b") && (this.GetColor() == "w"))
                if (((King)chessBoard[row, column]).IsCheckMate(false, row - 1, column + 1, chessBoard))
                    return "checkmate";
                else
                {
                    Console.WriteLine("Chess for white King!");
                    Console.WriteLine();
                    return "chess";
                }
            if ((chessBoard[row + 1, column - 1] is Pawn) && (chessBoard[row + 1, column - 1].GetColor() == "w") && (this.GetColor() == "b"))
                if (((King)chessBoard[row, column]).IsCheckMate(true, row + 1, column - 1, chessBoard))
                    return "checkmate";
                else
                {
                    Console.WriteLine("Chess for black King!");
                    Console.WriteLine();
                    return "chess";
                }
            if ((chessBoard[row + 1, column + 1] is Pawn) && (chessBoard[row + 1, column + 1].GetColor() == "w") && (this.GetColor() == "b"))
                if (((King)chessBoard[row, column]).IsCheckMate(true, row + 1, column + 1, chessBoard))
                    return "checkmate";
                else
                {
                    Console.WriteLine("Chess for black King!");
                    Console.WriteLine();
                    return "chess";
                }
            return "";

        }
        public string CheckIfChessThreatFromRookOrQueen(ChessSquare[,] chessBoard, int row, int column)
        {
            for (int i = row + 1; i <= 8; i++)
                if (chessBoard[i, column].GetName() != "e")
                {
                    if (((chessBoard[i, column] is Rook) || (chessBoard[i, column] is Queen)) && (this.GetColor() == "w") && (chessBoard[i, column].GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, i, column, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if (((chessBoard[i, column] is Rook) || (chessBoard[i, column] is Queen)) && (this.GetColor() == "b") && (chessBoard[i, column].GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, i, column, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    break;
                }
            for (int i = row - 1; i >= 1; i--)
                if (chessBoard[i, column].GetName() != "e")
                {
                    if (((chessBoard[i, column] is Rook) || (chessBoard[i, column] is Queen)) && (this.GetColor() == "w") && (chessBoard[i, column].GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, i, column, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if (((chessBoard[i, column] is Rook) || (chessBoard[i, column] is Queen)) && (this.GetColor() == "b") && (chessBoard[i, column].GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, i, column, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    break;
                }
            for (int i = column + 1; i <= 8; i++)
                if (chessBoard[row, i].GetName() != "e")
                {
                    if (((chessBoard[row, i] is Rook) || (chessBoard[row, i] is Queen)) && (this.GetColor() == "w") && (chessBoard[row, i].GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, row, i, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if (((chessBoard[row, i] is Rook) || (chessBoard[row, i] is Queen)) && (this.GetColor() == "b") && (chessBoard[row, i].GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, row, i, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    break;
                }
            for (int i = column - 1; i >= 1; i--)
                if (chessBoard[row, i].GetName() != "e")
                {
                    if (((chessBoard[row, i] is Rook) || (chessBoard[row, i] is Queen)) && (this.GetColor() == "w") && (chessBoard[row, i].GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, row, i, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if (((chessBoard[row, i] is Rook) || (chessBoard[row, i] is Queen)) && (this.GetColor() == "b") && (chessBoard[row, i].GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, row, i, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    break;
                }
            return "";
        }
        public string CheckIfChessThreatFromKnight(ChessSquare[,] chessBoard, int row, int column)
        {
            if ((row > 1) && (row < 8) && (column > 1) && (column < 8))
            {

                int[] movesArray1 = { 1, 1, 2, 2, -1, -1, -2, -2 };
                int[] movesArray2 = { 2, -2, 1, -1, 2, -2, 1, -1 };
                for (int i = 0; i < 8; i++)
                {
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "b") && (this.GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "w") && (this.GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                }
            }
            if ((row == 1) && (column == 1))
            {
                int[] movesArray1 = { 1, 2 };
                int[] movesArray2 = { 2, 1 };
                for (int i = 0; i < 2; i++)
                {
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "b") && (this.GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "w") && (this.GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                }
            }
            if ((row == 1) && (column > 1) && (column < 8))
            {
                int[] movesArray1 = { 1, 1, 2, 2 };
                int[] movesArray2 = { 2, -2, 1, -1 };
                for (int i = 0; i < 4; i++)
                {
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "b") && (this.GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "w") && (this.GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                }
            }
            if ((row == 1) && (column == 8))
            {
                int[] movesArray1 = { 1, 2 };
                int[] movesArray2 = { -2, -1 };
                for (int i = 0; i < 2; i++)
                {
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "b") && (this.GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "w") && (this.GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                }
            }
            if ((column == 8) && (row > 1) && (row < 8))
            {

                int[] movesArray1 = { 1, 2, -1, -2 };
                int[] movesArray2 = { -2, -1, -2, -1 };
                for (int i = 0; i < 4; i++)
                {
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "b") && (this.GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "w") && (this.GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                }
            }
            if ((row == 8) && (column == 8))
            {

                int[] movesArray1 = { -1, -2 };
                int[] movesArray2 = { -2, -1 };
                for (int i = 0; i < 2; i++)
                {
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "b") && (this.GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "w") && (this.GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                }
            }
            if ((row == 8) && (column > 1) && (column < 8))
            {

                int[] movesArray1 = { -1, -1, -2, -2 };
                int[] movesArray2 = { 2, -2, 1, -1 };
                for (int i = 0; i < 4; i++)
                {
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "b") && (this.GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "w") && (this.GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                }
            }

            if ((row == 8) && (column == 1))
            {
                int[] movesArray1 = { -1, -2 };
                int[] movesArray2 = { 2, 1 };
                for (int i = 0; i < 2; i++)
                {
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "b") && (this.GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "w") && (this.GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                }
            }
            if ((row > 1) && (row < 8) && (column == 1))
            {

                int[] movesArray1 = { 1, 2, -1, -2 };
                int[] movesArray2 = { 2, 1, 2, 1 };
                for (int i = 0; i < 4; i++)
                {
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "b") && (this.GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if ((chessBoard[row + movesArray1[i], column + movesArray2[i]] is Knight) && (chessBoard[row + movesArray1[i], column + movesArray2[i]].GetColor() == "w") && (this.GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, row + movesArray1[i], column + movesArray2[i], chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                }
            }
            return "";

        }
        public string CheckIfChessThreatFromBishopOrQueen(ChessSquare[,] chessBoard, int row, int column)
        {
            for (int i = column + 1, j = row - 1; (i <= 8) && (j >= 1); i++, j--)
                if (chessBoard[j, i].GetName() != "e")
                {
                    if (((chessBoard[j, i] is Bishop) || (chessBoard[j, i] is Queen)) && (this.GetColor() == "w") && (chessBoard[j, i].GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, j, i, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if (((chessBoard[j, i] is Bishop) || (chessBoard[j, i] is Queen)) && (this.GetColor() == "b") && (chessBoard[j, i].GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, j, i, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    break;
                }
            for (int i = column + 1, j = row + 1; (i <= 8) && (j <= 8); i++, j++)
                if (chessBoard[j, i].GetName() != "e")
                {
                    if (((chessBoard[j, i] is Bishop) || (chessBoard[j, i] is Queen)) && (this.GetColor() == "w") && (chessBoard[j, i].GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, j, i, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if (((chessBoard[j, i] is Bishop) || (chessBoard[j, i] is Queen)) && (this.GetColor() == "b") && (chessBoard[j, i].GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, j, i, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    break;
                }
            for (int i = column - 1, j = row + 1; (i >= 1) && (j <= 8); i--, j++)
                if (chessBoard[j, i].GetName() != "e")
                {
                    if (((chessBoard[j, i] is Bishop) || (chessBoard[j, i] is Queen)) && (this.GetColor() == "w") && (chessBoard[j, i].GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, j, i, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if (((chessBoard[j, i] is Bishop) || (chessBoard[j, i] is Queen)) && (this.GetColor() == "b") && (chessBoard[j, i].GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, j, i, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    break;
                }
            for (int i = column - 1, j = row - 1; (i >= 1) && (j >= 1); i--, j--)
                if (chessBoard[j, i].GetName() != "e")
                {
                    if (((chessBoard[j, i] is Bishop) || (chessBoard[j, i] is Queen)) && (this.GetColor() == "w") && (chessBoard[j, i].GetColor() == "b"))
                        if (((King)chessBoard[row, column]).IsCheckMate(false, j, i, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for white King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    if (((chessBoard[j, i] is Bishop) || (chessBoard[j, i] is Queen)) && (this.GetColor() == "b") && (chessBoard[j, i].GetColor() == "w"))
                        if (((King)chessBoard[row, column]).IsCheckMate(true, j, i, chessBoard))
                            return "checkmate";
                        else
                        {
                            Console.WriteLine("Chess for black King!");
                            Console.WriteLine();
                            return "chess";
                        }
                    break;
                }
            return "";

        }
        public override void setChessSquaresInThreat(ChessSquare[,] chessBoard, int row, int column)
        {

            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    if (this.GetColor() == "w")
                    {
                        if (((chessBoard[row + i, column + j]).GetChessThreat() == "blackThreat") || ((chessBoard[row + i, column + j]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + i, column + j]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + i, column + j]).SetChessThreat("whiteThreat");
                    }
                    else
                    {
                        if (((chessBoard[row + i, column + j]).GetChessThreat() == "whiteThreat") || ((chessBoard[row + i, column + j]).GetChessThreat() == "white&blackThreat"))
                            (chessBoard[row + i, column + j]).SetChessThreat("white&blackThreat");
                        else
                            (chessBoard[row + i, column + j]).SetChessThreat("blackThreat");
                    }


        }
        public bool IsCheckMate(bool colorIsWhite, int row, int column, ChessSquare[,] chessBoard)
        {
            int i, j, index, index1;
            for (i = -1; i <= 1; i++) // loop to check if king can move
                for (j = -1; j <= 1; j++)
                    if ((chessBoard[this.GetLine(), this.GetColumn()]).validMoveUnderChessThreat(chessBoard[this.GetLine() + i, this.GetColumn() + j], chessBoard))
                        return false;
            for (i = 1; i <= 8; i++) // check if can kill the Threat (white)
                for (j = 1; j <= 8; j++)
                    if (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[row, column], chessBoard))
                        return false;
            if (!((chessBoard[row, column] is Pawn) || (chessBoard[row, column] is Knight)))// check if can block the threat
            {
                if ((chessBoard[row, column] is Rook) || (chessBoard[row, column] is Queen))
                    if (colorIsWhite)
                    {
                        if (this.GetLine() == row)
                            if (column > this.GetColumn())
                            {
                                for (index = this.GetColumn() + 1; index < column; index++)
                                    for (i = 1; i <= 8; i++)
                                        for (j = 1; j <= 8; j++)
                                            if ((chessBoard[i, j].GetColor() == "b") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[row, index], chessBoard)))
                                                return false;
                            }
                            else
                            {
                                for (index = this.GetColumn() - 1; index > column; index--)
                                    for (i = 1; i <= 8; i++)
                                        for (j = 1; j <= 8; j++)
                                            if ((chessBoard[i, j].GetColor() == "b") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[row, index], chessBoard)))
                                                return false;
                            }
                        if (this.GetColumn() == column)
                            if (row > this.GetLine())
                            {
                                for (index = this.GetLine() + 1; index < row; index++)
                                    for (i = 1; i <= 8; i++)
                                        for (j = 1; j <= 8; j++)
                                            if ((chessBoard[i, j].GetColor() == "b") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[index, column], chessBoard)))
                                                return false;
                            }
                            else
                            {
                                for (index = this.GetLine() - 1; index > row; index--)
                                    for (i = 1; i <= 8; i++)
                                        for (j = 1; j <= 8; j++)
                                            if ((chessBoard[i, j].GetColor() == "b") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[index, column], chessBoard)))
                                                return false;
                            }
                        else // color is black
                        {
                            if (this.GetLine() == row)
                                if (column > this.GetColumn())
                                {
                                    for (index = this.GetColumn() + 1; index < column; index++)
                                        for (i = 1; i <= 8; i++)
                                            for (j = 1; j <= 8; j++)
                                                if ((chessBoard[i, j].GetColor() == "w") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[row, index], chessBoard)))
                                                    return false;
                                }
                                else
                                {
                                    for (index = this.GetColumn() - 1; index > column; index--)
                                        for (i = 1; i <= 8; i++)
                                            for (j = 1; j <= 8; j++)
                                                if ((chessBoard[i, j].GetColor() == "w") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[row, index], chessBoard)))
                                                    return false;
                                }
                            if (this.GetColumn() == column)
                                if (row > this.GetLine())
                                {
                                    for (index = this.GetLine() + 1; index < row; index++)
                                        for (i = 1; i <= 8; i++)
                                            for (j = 1; j <= 8; j++)
                                                if ((chessBoard[i, j].GetColor() == "w") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[index, column], chessBoard)))
                                                    return false;
                                }
                                else
                                {
                                    for (index = this.GetLine() - 1; index > row; index--)
                                        for (i = 1; i <= 8; i++)
                                            for (j = 1; j <= 8; j++)
                                                if ((chessBoard[i, j].GetColor() == "w") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[index, column], chessBoard)))
                                                    return false;
                                }
                        }
                    }
                if ((chessBoard[row, column] is Bishop) || (chessBoard[row, column] is Queen))
                {
                    if (colorIsWhite)
                    {
                        if (column > this.GetColumn())
                            if (row < this.GetLine())
                            {
                                for (index = this.GetLine() - 1, index1 = this.GetColumn() + 1; (index > row) && (index1 < column); index--, index1++)
                                    for (i = 1; i <= 8; i++)
                                        for (j = 1; j <= 8; j++)
                                            if ((chessBoard[i, j].GetColor() == "b") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[index, index1], chessBoard)))
                                                return false;
                            }
                            else // row > this.GetLine()
                            {
                                for (index = this.GetLine() + 1, index1 = this.GetColumn() + 1; (index < row) && (index1 < column); index++, index1++)
                                    for (i = 1; i <= 8; i++)
                                        for (j = 1; j <= 8; j++)
                                            if ((chessBoard[i, j].GetColor() == "b") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[index, index1], chessBoard)))
                                                return false;
                            }
                        else // (column < this.GetColumn())
                            if (row < this.GetLine())
                            {
                                for (index = this.GetLine() - 1, index1 = this.GetColumn() - 1; (index > row) && (index1 > column); index--, index1--)
                                    for (i = 1; i <= 8; i++)
                                        for (j = 1; j <= 8; j++)
                                            if ((chessBoard[i, j].GetColor() == "b") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[index, index1], chessBoard)))
                                                return false;
                            }
                            else // row > this.GetLine()
                            {
                                for (index = this.GetLine() + 1, index1 = this.GetColumn() - 1; (index < row) && (index1 > column); index++, index1--)
                                    for (i = 1; i <= 8; i++)
                                        for (j = 1; j <= 8; j++)
                                            if ((chessBoard[i, j].GetColor() == "b") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[index, index1], chessBoard)))
                                                return false;
                            }
                    }
                    else // color is black
                    {
                        if (column > this.GetColumn())
                            if (row < this.GetLine())
                            {
                                for (index = this.GetLine() - 1, index1 = this.GetColumn() + 1; (index > row) && (index1 < column); index--, index1++)
                                    for (i = 1; i <= 8; i++)
                                        for (j = 1; j <= 8; j++)
                                            if ((chessBoard[i, j].GetColor() == "w") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[index, index1], chessBoard)))
                                                return false;
                            }
                            else // row > this.GetLine()
                            {
                                for (index = this.GetLine() + 1, index1 = this.GetColumn() + 1; (index < row) && (index1 < column); index++, index1++)
                                    for (i = 1; i <= 8; i++)
                                        for (j = 1; j <= 8; j++)
                                            if ((chessBoard[i, j].GetColor() == "w") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[index, index1], chessBoard)))
                                                return false;
                            }
                        else // (column < this.GetColumn())
                            if (row < this.GetLine())
                            {
                                for (index = this.GetLine() - 1, index1 = this.GetColumn() - 1; (index > row) && (index1 > column); index--, index1--)
                                    for (i = 1; i <= 8; i++)
                                        for (j = 1; j <= 8; j++)
                                            if ((chessBoard[i, j].GetColor() == "w") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[index, index1], chessBoard)))
                                                return false;
                            }
                            else // row > this.GetLine()
                            {
                                for (index = this.GetLine() + 1, index1 = this.GetColumn() - 1; (index < row) && (index1 > column); index++, index1--)
                                    for (i = 1; i <= 8; i++)
                                        for (j = 1; j <= 8; j++)
                                            if ((chessBoard[i, j].GetColor() == "w") && (chessBoard[i, j].validMoveUnderChessThreat(chessBoard[index, index1], chessBoard)))
                                                return false;
                            }
                    }
                }
            }
            return true;
        }
        public bool CheckIfCastling(ChessSquare chesssquare, ChessSquare[,] chessBoard)
        {
            if ((this.GetColor() == "w") && (this.GetLine() == 8) && (this.GetColumn() == 5) && (underChessThreat == false))
            {
                if ((chesssquare.GetLine() == 8) && (chesssquare.GetColumn() == 7) && (chessBoard[8, 8] is Rook) && (chessBoard[8, 8].GetColor() == "w"))
                    if ((this.GetNumberOfMovements() == 0) && (((Rook)chessBoard[8, 8]).GetNumberOfMovements() == 0))
                    {
                        for (int i = 6; i <= 7; i++)
                            if (chessBoard[8, i].GetColor() != "e")
                                return false;
                        for (int i = 5; i <= 7; i++)
                            if ((chessBoard[8, i].GetChessThreat() == ("white&blackThreat")) || (chessBoard[8, i].GetChessThreat() == ("blackThreat")))
                                return false;
                        chessBoard[8, 6] = chessBoard[8, 8];
                        chessBoard[8, 6].SetLine(8);
                        chessBoard[8, 6].SetColumn(6);
                        chessBoard[8, 8] = new EmptySquare(8, 8, "noThreat");
                        return true;
                    }
                if ((chesssquare.GetLine() == 8) && (chesssquare.GetColumn() == 3) && (chessBoard[8, 1] is Rook) && (chessBoard[8, 1].GetColor() == "w"))
                    if ((this.GetNumberOfMovements() == 0) && (((Rook)chessBoard[8, 1]).GetNumberOfMovements() == 0))
                    {
                        for (int i = 2; i <= 4; i++)
                            if (chessBoard[8, i].GetColor() != "e")
                                return false;
                        for (int i = 1; i <= 5; i++)
                            if ((chessBoard[8, i].GetChessThreat() == ("white&blackThreat")) || (chessBoard[8, i].GetChessThreat() == ("blackThreat")))
                                return false;
                        chessBoard[8, 4] = chessBoard[8, 1];
                        chessBoard[8, 4].SetLine(8);
                        chessBoard[8, 4].SetColumn(4);
                        chessBoard[8, 1] = new EmptySquare(8, 1, "noThreat");
                        return true;
                    }
                return false;
            }
            if ((this.GetColor() == "b") && (this.GetLine() == 1) && (this.GetColumn() == 5) && (underChessThreat == false))
            {
                if ((chesssquare.GetLine() == 1) && (chesssquare.GetColumn() == 7) && (chessBoard[1, 8] is Rook) && (chessBoard[1, 8].GetColor() == "b"))
                    if ((this.GetNumberOfMovements() == 0) && (((Rook)chessBoard[1, 8]).GetNumberOfMovements() == 0))
                    {
                        for (int i = 6; i <= 7; i++)
                            if (chessBoard[1, i].GetColor() != "e")
                                return false;
                        for (int i = 5; i <= 7; i++)
                            if ((chessBoard[1, i].GetChessThreat() == ("white&blackThreat")) || (chessBoard[1, i].GetChessThreat() == ("whiteThreat")))
                                return false;
                        chessBoard[1, 6] = chessBoard[1, 8];
                        chessBoard[1, 6].SetLine(1);
                        chessBoard[1, 6].SetColumn(6);
                        chessBoard[1, 8] = new EmptySquare(1, 8, "noThreat");
                        return true;
                    }
                if ((chesssquare.GetLine() == 1) && (chesssquare.GetColumn() == 3) && (chessBoard[1, 1] is Rook) && (chessBoard[1, 1].GetColor() == "b"))
                    if ((this.GetNumberOfMovements() == 0) && (((Rook)chessBoard[1, 1]).GetNumberOfMovements() == 0))
                    {
                        for (int i = 2; i <= 4; i++)
                            if (chessBoard[1, i].GetColor() != "e")
                                return false;
                        for (int i = 1; i <= 5; i++)
                            if ((chessBoard[1, i].GetChessThreat() == ("white&blackThreat")) || (chessBoard[1, i].GetChessThreat() == ("whiteThreat")))
                                return false;
                        chessBoard[1, 4] = chessBoard[1, 1];
                        chessBoard[1, 4].SetLine(1);
                        chessBoard[1, 4].SetColumn(4);
                        chessBoard[1, 1] = new EmptySquare(1, 1, "noThreat");
                        return true;
                    }
                return false;
            }
            return false;
        }
        public bool CheckIfStalemate(ChessSquare[,] chessBoard, int row, int column)
        {
            int i,j,sumOfWhiteTools=0,sumOfBlackTools=0,whiteKing=0,blackKing=0,whiteRook=0,blackRook=0,whiteBishop=0,blackBishop=0;
                for (i = 1; i <= 8; i++)
                    for (j = 1; j <= 8; j++)
                    {
                        if (chessBoard[i, j].GetColor() == "w")
                        {
                            sumOfWhiteTools++;
                            if (chessBoard[i, j].GetName() == "b")
                                whiteBishop++;
                            if (chessBoard[i, j].GetName() == "r")
                                whiteRook++;
                            if (chessBoard[i, j].GetName() == "k")
                                whiteKing++;
                        }
                        if (chessBoard[i, j].GetColor() == "b")
                        {
                            sumOfBlackTools++;
                            if (chessBoard[i, j].GetName() == "b")
                                blackBishop++;
                            if (chessBoard[i, j].GetName() == "r")
                                blackRook++;
                            if (chessBoard[i, j].GetName() == "k")
                                blackKing++;
                        }
                    }
            if (this.GetColor()=="w")
                if ((sumOfWhiteTools == 1)&&(whiteKing==1))
                {
                    for (i = -1; i <= 1; i++) // loop to check if king can move
                        for (j = -1; j <= 1; j++)
                            if (chessBoard[row, column].validMoveUnderChessThreat(chessBoard[row + i, this.GetColumn() + j], chessBoard))
                                return false;
                   return true;
                }
            if (this.GetColor()=="b")
                if ((sumOfBlackTools == 1)&&(blackKing==1))
                {
                    for (i = -1; i <= 1; i++) // loop to check if king can move
                        for (j = -1; j <= 1; j++)
                            if ((chessBoard[row, column]).validMoveUnderChessThreat(chessBoard[row + i, column + j], chessBoard))
                                return false;
                   return true;
                }
            if ((sumOfWhiteTools == 1) && (sumOfBlackTools == 1) && (whiteKing == 1) && (blackKing == 1))
                return true;
            if (((sumOfWhiteTools == 2) && (sumOfBlackTools == 1) && (whiteKing == 1) && (blackKing == 1)) && ((whiteBishop == 1) || (whiteRook == 1)))
                return true;
            if (((sumOfBlackTools == 2) && (sumOfWhiteTools == 1) && (whiteKing == 1) && (blackKing == 1)) && ((blackRook == 1) || (blackBishop == 1)))
                return true;
            return false;
        }
        public void IncreaseNumberOfMovements()
        {
            numberOfMovements++;
        }
        public int GetNumberOfMovements()
        {
            return numberOfMovements;
        }

    }
}







