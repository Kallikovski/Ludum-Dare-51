using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMouseCursor : MonoBehaviour
{

    [SerializeField] private Texture2D cursorArrow;

    void Start()
    {
        // Cursor.visible = false;
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

}
