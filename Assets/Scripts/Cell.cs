using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject LeftWall;
    [SerializeField] private GameObject RightWall;
    [SerializeField] private GameObject UpperWall;
    [SerializeField] private GameObject BottomWall;

    public void SetWallsActive(bool LeftActive, bool RightActive, bool UpperActive, bool BottomActive)
    {
        LeftWall.SetActive(LeftActive);
        RightWall.SetActive(RightActive);
        UpperWall.SetActive(UpperActive);
        BottomWall.SetActive(BottomActive);
    }
}
