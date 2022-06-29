using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCell
{
    public int X; //�������������� ���������� ������ ���������
    public int Y; //������������ ���������� ������ ���������

    public bool LeftWall = true; //����, ������������ ������������� ����� �����
    public bool RightWall = true; //����, ������������ ������������� ������ �����
    public bool UpperWall = true; //����, ������������ ������������� ������� �����
    public bool BottomWall = true; //����, ������������ ������������� ������ �����

    public bool IsVisited = false; //����, ������������ ���� �� �������� ������
}
public class LabyrinthGenerator
{
    public int Width { get; private set; } //������ ���������
    public int Height { get; private set; } //������ ���������

    public LabyrinthGenerator(int width, int height)
    {
        Width = width;
        Height = height;
    }


    public LabyrinthCell[,] GenerateLabyrinth()
    {
        LabyrinthCell[,] labyrinthCells = new LabyrinthCell[Width, Height]; //������ ���������

        //��� ���� ����� ���������
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                labyrinthCells[x, y] = new LabyrinthCell { X = x, Y = y }; //������ ��������������� ����������
            }
        }

        RemoveWallsBacktracking(labyrinthCells, labyrinthCells[0, 0]); //������� �����

        return labyrinthCells; //������� ������ ���������
    }

    private void RemoveWallsBacktracking(LabyrinthCell[,] labyrinth, LabyrinthCell curCell)
    {
        curCell.IsVisited = true; //������� ������� ������� ����������

        List<LabyrinthCell> unvisited = GetUnvisited(labyrinth,curCell); //������ ������������ �������� ������

        while (unvisited.Count>0)
        { //���� ���� ������������ �������� �������
            LabyrinthCell nextCell = unvisited[Random.Range(0, unvisited.Count)]; //��������� �������� ������������ �������
            RemoveWall(curCell, nextCell); //������� ����� ����� ������� � ��������� ��������
            RemoveWallsBacktracking(labyrinth, nextCell); //������� �����
            unvisited = GetUnvisited(labyrinth, curCell); //������� ������ ����������� �������� ������
        }
        
    }

    private List<LabyrinthCell> GetUnvisited(LabyrinthCell[,] labyrinth, LabyrinthCell curCell)
    {

        List<LabyrinthCell> unvisited = new List<LabyrinthCell>(); //������ ������������ �������� ������
        int x = curCell.X; //�������������� ���������� ������� �������
        int y = curCell.Y; //������������ ���������� ������� �������

        //�������� ��� ������������ �������� ������� � ��������������� ������...
        if (x > 0 && !labyrinth[x - 1, y].IsVisited)
            unvisited.Add(labyrinth[x - 1, y]); //...���� �� �������� ������ ����� �� ������� 
        if (x < Width - 1 && !labyrinth[x + 1, y].IsVisited)
            unvisited.Add(labyrinth[x + 1, y]); //...���� �� �������� ������ ������ �� ������� 
        if (y > 0 && !labyrinth[x, y - 1].IsVisited)
            unvisited.Add(labyrinth[x, y - 1]); //...���� �� �������� ������ ������ �� ������� 
        if (y < Height - 1 && !labyrinth[x, y + 1].IsVisited)
            unvisited.Add(labyrinth[x, y + 1]); //...���� �� �������� ������ ����� �� ������� 
        return unvisited;
    }
    private void RemoveWall(LabyrinthCell first, LabyrinthCell second)
    {
        if (first.X == second.X)
        { //���� ������ � ������ ������ �� ����� ����� �� ���������
            if (first.Y < second.Y)
            { //���� ������ ������ ���� ������
                first.UpperWall = false; //������ ������� ����� ������ ������
                second.BottomWall = false; //������ ������ ����� ������ ������
            }
            else
            { //�����
                first.BottomWall = false; //������ ������ ����� ������ ������
                second.UpperWall = false; //������ ������� ����� ������ ������
            }
        }
        else if (first.Y == second.Y)
        {//��������� ������ � ������ ������ �� ����� ����� �� �����������
            if (first.X < second.X)
            {//���� ������ ������ ����� ������
                first.RightWall = false; //������ ������ ����� ������ ������
                second.LeftWall = false; //������ ����� ����� ������ ������
            }
            else
            { //�����
                first.LeftWall = false; //������ ����� ����� ������ ������
                second.RightWall = false; //������ ������ ����� ������ ������
            }
        }
    }
}
