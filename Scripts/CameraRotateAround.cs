using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraRotateAround : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float sensitivity = 3; // чувствительность мышки
    public float limit = 80; // ограничение вращения по Y
    public float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
    public float zoomMax = 10; // макс. увеличение
    public float zoomMin = 3; // мин. увеличение
    public bool FreeMode;
    public Toggle[] m_Toggle;
    private float X;

    bool IsDestroy;
    public MeshFilter go;

    Vector3 EndPosition;

    void Start()
    {
        limit = Mathf.Abs(limit);
        if (limit > 90) limit = 90;
        offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
        transform.position = target.position + offset;
    }

    void Update()
    {
        if (FreeMode)
        {
            offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

            X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            transform.localEulerAngles = new Vector3(0, X, 0);
            transform.position = transform.localRotation * offset + target.position;  //Камера смотрит на таргет
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    //Не реализованно
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    EndPosition = hit.point;
                    if (m_Toggle[0].isOn)
                    {
                       //Не реализованно
                    }
                    if (m_Toggle[2].isOn)
                    {
                        if (hit.rigidbody != null)
                        {
                            hit.rigidbody.velocity = transform.forward * 40;
                        }
                    } //Destroy
                    else if (m_Toggle[1].isOn && !IsDestroy)
                    {
                        target.GetComponent<DivideCube>().DivideIntoCuboids();
                        IsDestroy = true;
                    }//Brick
                    Debug.Log(EndPosition);
                }
            }
        }
    }

    public void SetFreeModeBoolButton()
    {
        FreeMode = !FreeMode;
    } //Кнопка свободного перемещения камеры
    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    } //Перезапуск сцены


}