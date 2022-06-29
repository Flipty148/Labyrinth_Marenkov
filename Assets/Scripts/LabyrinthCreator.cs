using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCreator : MonoBehaviour
{
    [SerializeField] private GameObject Cell; //Ячейка лабиринта
    [SerializeField] private int Width = 5; //Ширина лабиринта
    [SerializeField] private int Height = 5; //Высота лабиринта


    private void Start()
    {
        CreateLabyrinth(); //Создать лабиринт
    }

    private void CreateLabyrinth()
    {
        GameObject Cells = new GameObject("Cells"); //Объект для всех ячеек
        LabyrinthGenerator generator = new LabyrinthGenerator(Width, Height); //Генератор лабиринта 
        LabyrinthCell[,] labyrinth = generator.GenerateLabyrinth(); //Генерация лабиринта

        //Отображение лабиринта на сцене
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GameObject curObject = Instantiate(Cell, new Vector2(x, y), Quaternion.identity); //Создать ячейку
                curObject.transform.SetParent(Cells.transform); //Добавить текущую ячейку ко всем ячейкам
                Cell cell = curObject.GetComponent<Cell>();
                cell.SetWallsActive(labyrinth[x, y].LeftWall, labyrinth[x, y].RightWall, labyrinth[x, y].UpperWall, labyrinth[x, y].BottomWall); //Изменить видимость стен в ячейке
            }
        }
    }
}
