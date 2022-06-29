using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCell
{
    public int X; //√оризонтальна€ координата €чейки лабиринта
    public int Y; //¬ертикальна€ координата €чейки лабиринта

    public bool LeftWall = true; //‘лаг, показывающий существование левой стены
    public bool RightWall = true; //‘лаг, показывающий существование правой стены
    public bool UpperWall = true; //‘лаг, показывающий существование верхней стены
    public bool BottomWall = true; //‘лаг, показывающий существование нижней стены

    public bool IsVisited = false; //‘лаг, показывающий была ли посещена €чейка
}
public class LabyrinthGenerator
{
    public int Width { get; private set; } //Ўирина лабиринта
    public int Height { get; private set; } //¬ысота лабиринта

    public LabyrinthGenerator(int width, int height)
    {
        Width = width;
        Height = height;
    }


    public LabyrinthCell[,] GenerateLabyrinth()
    {
        LabyrinthCell[,] labyrinthCells = new LabyrinthCell[Width, Height]; //ячейки лабиринта

        //ƒл€ всех €чеек лабиринта
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                labyrinthCells[x, y] = new LabyrinthCell { X = x, Y = y }; //«адать соответствующие координаты
            }
        }

        RemoveWallsBacktracking(labyrinthCells, labyrinthCells[0, 0]); //”далить стены

        return labyrinthCells; //¬ернуть €чейки лабиринта
    }

    private void RemoveWallsBacktracking(LabyrinthCell[,] labyrinth, LabyrinthCell curCell)
    {
        curCell.IsVisited = true; //—читать текущую вершину посещЄнной

        List<LabyrinthCell> unvisited = new List<LabyrinthCell>(); //—писок непосещЄнных соседних вершин

        int x = curCell.X; //√оризонтальна€ координата текущей вершины
        int y = curCell.Y; //¬ертикальна€ координата текущей вершины

        //ƒобавить все непосещЄнные соседние вершины в соответствующий список...
        if (x > 0 && !labyrinth[x - 1, y].IsVisited)
            unvisited.Add(labyrinth[x - 1, y]); //...если не посещена €чейка слева от текущей 
        if (x < Width - 2 && !labyrinth[x + 1, y].IsVisited)
            unvisited.Add(labyrinth[x + 1, y]); //...если не посещена €чейка справа от текущей 
        if (y > 0 && !labyrinth[x, y - 1].IsVisited)
            unvisited.Add(labyrinth[x, y - 1]); //...если не посещена €чейка сверху от текущей 
        if (y < Height - 2 && !labyrinth[x, y + 1].IsVisited)
            unvisited.Add(labyrinth[x, y + 1]); //...если не посещена €чейка снизу от текущей 

        if (unvisited.Count > 0)
        { //≈сли есть непосещенные соседи
            for (int i=0; i < unvisited.Count - 1; i++)
            {
                LabyrinthCell nextCell = unvisited[Random.Range(0, unvisited.Count)];
                RemoveWall(curCell, nextCell);
                RemoveWallsBacktracking(labyrinth, nextCell);
            }
        }
    }

    private void RemoveWall(LabyrinthCell first, LabyrinthCell second)
    {
        if (first.X == second.X)
        { //≈сли перва€ и втора€ €чейки на одной линии по вертикали
            if (first.Y < second.Y)
            { //≈сли перва€ €чейка ниже второй
                first.UpperWall = false; //”брать верхнюю стену первой €чейки
                second.BottomWall = false; //”брать нижнюю стену второй €чейки
            }
            else
            { //»наче
                first.BottomWall = false; //”брать нижнюю стену первой €чейки
                second.UpperWall = false; //”брать верхнюю стену второй €чейки
            }
        }
        else if (first.Y == second.Y)
        {//»наче≈сли перва€ и втора€ €чейки на одной линии по горизонтали
            if (first.X < second.X)
            {//≈сли перва€ €чейка левее второй
                first.RightWall = false; //”брать правую стену первой €чейки
                second.LeftWall = false; //”брать левую стену второй €чейки
            }
            else
            { //»наче
                first.LeftWall = false; //”брать левую стену первой €чейки
                second.RightWall = false; //”брать правую стену второй €чейки
            }
        }
    }
}
