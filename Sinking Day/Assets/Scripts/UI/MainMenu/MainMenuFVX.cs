using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFVX : MonoBehaviour {

    [System.Serializable]
    public class MainMenuLayer
    {
        public float layerOffset = -0.1f;
        public List<GameObject> Objs;
    }

    public static Texture2D defaultPointer;

    public List<MainMenuLayer> layers;
    Vector2 mousePosition;
    Vector2 mouseOffset;
    public GameObject menu;
    public float menuRotate;

    private Vector3[,] objPos = new Vector3[10, 10];
    private Quaternion menuRotation;

    private void Start()
    {
        defaultPointer = (Texture2D)Resources.Load("Image/Pointer/Pointer");
        Cursor.SetCursor(defaultPointer, Vector2.zero, CursorMode.Auto);
        if (menu != null)
        {
            menuRotation = menu.transform.rotation;
        }
        for(int i = 0; i < layers.Count; i++)
        {
            for(int j = 0; j < layers[i].Objs.Count; j++)
            {
                objPos[i, j] = layers[i].Objs[j].transform.position;
            }
        }
    }

    private void Update()
    {
        mousePosition = Input.mousePosition;
        mouseOffset = mousePosition - new Vector2(Screen.width / 2, Screen.height / 2);
        if (menu != null)
        {
            menu.transform.rotation = menuRotation;
            menu.transform.Rotate(mouseOffset.y * menuRotate, mouseOffset.x * menuRotate, 0);
        }
        for (int i = 0; i < layers.Count; i++)
        {
            for (int j = 0; j < layers[i].Objs.Count; j++)
            {
                layers[i].Objs[j].transform.position = objPos[i, j] - new Vector3(mouseOffset.x, mouseOffset.y, 0) * layers[i].layerOffset;
            }
        }
    }
}

