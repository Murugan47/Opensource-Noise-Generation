using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
public class HexMesh : MonoBehaviour
{
    // Mesh properties
    Mesh mesh;

    // Hexagon properties
    private int hexagonSides = 6;
    public float hexagonRadius;

    // Position offset (top-left corner)
    public float xMax;
    public float yMax;

    // Lists for per-triangle vertices and colors
    List<Vector3> meshVertices;
    List<int> meshTriangles;
    List<Color> meshColors;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        DrawHexagonGrid(hexagonSides, hexagonRadius, xMax, yMax);
    }

    void DrawHexagonGrid(int sides, float radius, float xMax, float yMax)
    {
        // Get the circumference points (first point is center)
        List<Vector3> points = GetCircumference(sides, radius, xMax, yMax);

        // Initialize lists for the new mesh
        meshVertices = new List<Vector3>();
        meshTriangles = new List<int>();
        meshColors = new List<Color>();

        int pointCount = points.Count;

        // Loop over each triangle in the fan
        for (int i = 1; i < pointCount; i++)
        {
            Vector3 center = points[0];
            Vector3 current = points[i];
            Vector3 next = points[i + 1 < pointCount ? i + 1 : 1]; // wrap-around

            // Add vertices (duplicate per triangle)
            meshVertices.Add(center);
            meshVertices.Add(current);
            meshVertices.Add(next);

            // Add triangle indices
            int index = meshVertices.Count;
            meshTriangles.Add(index - 3);
            meshTriangles.Add(index - 2);
            meshTriangles.Add(index - 1);

            Color Color1 = RandomBWGray();
            Color Color2 = RandomBWGray();
            Color Color3 = RandomBWGray();
            Color mainColor;

            if (Color1 == Color.white && (Color2 == Color.white || Color3 == Color.white))
            {
                mainColor = Color.white;
            }
            else if (Color1 == Color.white && Color2 == Color.black && Color3 == Color.black)
            {
                mainColor = Color.black;
            }
            else if (Color1 == Color.black && (Color2 == Color.black || Color3 == Color.black))
            {
                mainColor = Color.black;
            }
            else if (Color1 == Color.black && Color2 == Color.white && Color3 == Color.white)
            {
                mainColor = Color.white;
            }
            else
            {
                mainColor = Color.white;
            }

            //meshColors.Add(mainColor);
            //meshColors.Add(mainColor);
            //meshColors.Add(mainColor);

            meshColors.Add(RandomBWGray());
            meshColors.Add(RandomBWGray());
            meshColors.Add(RandomBWGray());
            
        }

        // Assign to mesh
        mesh.Clear();
        mesh.vertices = meshVertices.ToArray();
        mesh.triangles = meshTriangles.ToArray();
        mesh.colors = meshColors.ToArray();
        mesh.RecalculateNormals();
    }

    // Generates the center + circumference points
    List<Vector3> GetCircumference(int sides, float radius, float xMax, float yMax)
    {
        List<Vector3> points = new List<Vector3>();
        HexMeshCreator hexmeshcreator = FindFirstObjectByType<HexMeshCreator>();
        //xMax = hexmeshcreator.xCoord;
        //yMax = hexmeshcreator.yCoord;
        // Add center point first
        points.Add(new Vector3(xMax, yMax, 0));

        // Step for each side
        float stepCount = 1f / sides;
        float TAU = Mathf.PI * 2f;
        float radianStep = stepCount * TAU;

        // Add circumference points
        for (int i = 0; i < sides; i++)
        {
            float rad = radianStep * i;
            float x = xMax - Mathf.Cos(rad) * radius;
            float y = yMax - Mathf.Sin(rad) * radius;
            points.Add(new Vector3(x, y, 0));
        }

        return points;
    }

    Color RandomBWGray()
    {
        int choice = Random.Range(0, 3); // 0, 1, or 2
        switch (choice)
        {
            case 0: return Color.white;
            case 1: return Color.black;  // middle between black and white
            default: return Color.gray;
        }
    }


}