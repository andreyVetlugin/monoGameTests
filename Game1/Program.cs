
using Game1;
using Game1.Views;
using Microsoft.Xna.Framework;

//using var game = new Game1.Game1();
using var tetrisGame = new TetrisDrawer(new InGameDisplayInfo { BorderCellColor = Color.Red, CellThickness = 1, CellSize = 15, FigureBlockColor = Color.Black}, new Point(10, 20), new Point(200, 300));
tetrisGame.Run();
//game.Run();
