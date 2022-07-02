using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float LeftBound;
    private float RightBound;
    private float UpperBound;
    private float BottomBound;

    public void SetCameraBounds(float leftBound, float rightBound, float upperBound, float bottomBound)
    {
        float vertExtent = gameObject.GetComponent<Camera>().orthographicSize;
        float horExtent = gameObject.GetComponent<Camera>().aspect * vertExtent;
        LeftBound = leftBound + horExtent - 0.5f;
        RightBound = rightBound - horExtent + 0.5f;
        UpperBound = upperBound - vertExtent + 0.5f;
        BottomBound = bottomBound + vertExtent - 0.5f;
    }
    private void Update()
    {
        CameraClamp();
    }

    private void FixedUpdate()
    {
        if (transform.localPosition != new Vector3(0, 0, transform.localPosition.z))
            transform.localPosition = new Vector3(0 * Time.fixedDeltaTime, 0 * Time.fixedDeltaTime, transform.localPosition.z);
    }


    private void CameraClamp()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, LeftBound, RightBound), Mathf.Clamp(transform.position.y, BottomBound, UpperBound),
            transform.position.z);
    }

}