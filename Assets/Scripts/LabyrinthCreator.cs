using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCreator : MonoBehaviour
{
    [SerializeField] private GameObject Cell; //������ ���������
    [SerializeField] private int Width = 5; //������ ���������
    [SerializeField] private int Height = 5; //������ ���������
    private float sizeX; //������ ������ �� X
    private float sizeY; //������ ������ �� Y


    private void Start()
    {
        sizeX = Cell.GetComponent<Transform>().localScale.x;
        sizeY = Cell.GetComponent<Transform>().localScale.y;
        CreateLabyrinth(); //������� ��������
    }

    private void CreateLabyrinth()
    {
        GameObject Cells = new GameObject("Cells"); //������ ��� ���� �����
        LabyrinthGenerator generator = new LabyrinthGenerator(Width, Height); //��������� ��������� 
        LabyrinthCell[,] labyrinth = generator.GenerateLabyrinth(); //��������� ���������

        //����������� ��������� �� �����
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GameObject curObject = Instantiate(Cell, new Vector2(x*sizeX, y*sizeY), Quaternion.identity); //������� ������
                curObject.transform.SetParent(Cells.transform); //�������� ������� ������ �� ���� �������
                Cell cell = curObject.GetComponent<Cell>();
                cell.SetWallsActive(labyrinth[x, y].LeftWall, labyrinth[x, y].RightWall, labyrinth[x, y].UpperWall, labyrinth[x, y].BottomWall); //�������� ��������� ���� � ������
            }
        }
    }
}
