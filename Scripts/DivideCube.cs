using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivideCube : MonoBehaviour
{
    public GameObject TargetCube;
    Vector3 SelectionCount;
    public Material SubMaterial;
    public CameraRotateAround cameraRotateAround;
    public Scrollbar[] AllScrollBars;

    private Vector3 SizeOfOriginalCube, SelectionSize, FillStartPos;
    Transform ParentTransform;
    GameObject SubCube;

    void Start()
    {
        if (TargetCube == null)
        {
            TargetCube = gameObject;
        }
        SizeOfOriginalCube = TargetCube.transform.lossyScale;

        FillStartPos = TargetCube.transform.TransformPoint(new Vector3(-0.5f, -0.3f, -0.5f))
            + TargetCube.transform.TransformDirection(new Vector3(SelectionSize.x, -SelectionSize.y, SelectionSize.z) / 2); 
        ParentTransform = new GameObject(TargetCube.name + "CubeParent").transform; //Создание пустого родителя
        ParentTransform.position = TargetCube.transform.position;
    }
    public void DivideIntoCuboids()
    {
        SelectionCount = new Vector3(
            ((float)System.Math.Round(AllScrollBars[0].value, 1) * 10),
            ((float)System.Math.Round(AllScrollBars[1].value, 1) * 10),
            ((float)System.Math.Round(AllScrollBars[2].value, 1) * 10)
        ); //Считаем размер сетки из ScrollBar
        SelectionSize = new Vector3(
            SizeOfOriginalCube.x / SelectionCount.x,
            SizeOfOriginalCube.y / SelectionCount.y,
            SizeOfOriginalCube.z / SelectionCount.z
            ); //Размер дочерних кубов
        int SellCount = 0;//Кол-во дочерних кубов
        for (int x = 0; x < SelectionCount.x; x++)
        {
            for (int y = 0; y < SelectionCount.y; y++)
            {
                for (int z = 0; z < SelectionCount.z; z++)
                {
                    SubCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    SubCube.transform.localScale = SelectionSize;
                    SubCube.transform.position = FillStartPos + TargetCube.transform.TransformDirection(new Vector3((SelectionSize.x) * x, (SelectionSize.y) * y, (SelectionSize.z) * z));
                    SubCube.transform.rotation = TargetCube.transform.rotation;

                    SubCube.transform.SetParent(ParentTransform);
                    SubCube.GetComponent<MeshRenderer>().material = SubMaterial;
                    SellCount++;
                    SubCube.AddComponent<Rigidbody>();  //Добавление физики
                }
            }
        }
        Debug.Log(SellCount);
        Destroy(TargetCube); //Уничтожить таргет для камеры и назначить новый
        cameraRotateAround.target = ParentTransform;
    }
}
