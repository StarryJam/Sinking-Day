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
    public List<GameObject> menus;
    public float menuRotate;

    private Vector3[,] objPos = new Vector3[10, 10];
    private Quaternion[] menuRotation = new Quaternion[10];

    private void Start()
    {
        defaultPointer = (Texture2D)Resources.Load("Image/Pointer/Pointer");
        Cursor.SetCursor(defaultPointer, Vector2.zero, CursorMode.Auto);
        for (int i = 0; i < menus.Count; i++)
        {
            menuRotation[i] = menus[i].transform.rotation;
        }
        for (int i = 0; i < layers.Count; i++)
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
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i].transform.rotation = menuRotation[i];
            menus[i].transform.Rotate(mouseOffset.y * menuRotate, mouseOffset.x * menuRotate, 0);
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

