using UnityEngine;
using System.Collections;
using System.Collections.Generic;


enum DrawMode
{
    DrawLine,
    DrawRect,
    DrawCircle,

}

public class DrawRectTest : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 end;
    bool canDrawRect = false;
    public Material mat;

    private DrawMode drawMode;

    private ArrayList linePoints;

    void Start()
    {
    
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "drawLine"))
        {
            drawMode = DrawMode.DrawLine;
        }

        if (GUI.Button(new Rect(110, 0, 100, 50), "drawRect"))
        {
            drawMode = DrawMode.DrawRect;
        }
        if (GUI.Button(new Rect(220, 0, 100, 50), "drawCircle"))
        {
            drawMode = DrawMode.DrawLine;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            origin = Input.mousePosition;
            canDrawRect = true;
        }



        if (Input.GetMouseButtonUp(0))
        {
            end = Input.mousePosition;
            canDrawRect = false;
        }
    }

    private void OnPostRender()
    {
        if (canDrawRect)
        {
            DrawRect(origin, Input.mousePosition, mat);
        }
        //canDrawRect = false;
    }


    private void DrawRect(Vector3 origin, Vector3 end, Material mat)
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadPixelMatrix();
        GL.Color(Color.red);
        GL.Begin(GL.QUADS);

        GL.Vertex3(end.x, end.y, 0);
        GL.Vertex3(end.x, origin.y, 0);
        GL.Vertex3(origin.x, origin.y, 0);
        GL.Vertex3(origin.x, end.y, 0);

        GL.End();
        GL.PopMatrix();
    }
}