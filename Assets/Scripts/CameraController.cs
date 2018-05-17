using UnityEngine;

public class CameraController : MonoBehaviour {

    public float minZoom, maxZoom, scrollSpeed, mouseSensibility;

    public Vector3 BoardCenter { get; set; }
    private float zoomAmount, rotX, rotY;
    private Vector3 positionOffset;
    private Camera mainCam;

    void Start () {
        mainCam = Camera.main;
        positionOffset = transform.position;
	}
	
	void Update () {
        CameraZoom();
        CameraRotation();
    }

    void CameraRotation()
    {
        if(Input.GetMouseButton(0))
        {
            rotX -= Input.GetAxis("Mouse Y") * mouseSensibility;
            rotY += Input.GetAxis("Mouse X") * mouseSensibility;

            rotX = Mathf.Clamp(rotX, -20, 45);
            transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        }
    }

    void CameraZoom()
    {
        zoomAmount += Input.GetAxis("Mouse ScrollWheel") * -1000;
        zoomAmount = Mathf.Clamp(zoomAmount, minZoom, maxZoom);

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, zoomAmount, scrollSpeed * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        transform.position = BoardCenter + positionOffset;
    }
}
