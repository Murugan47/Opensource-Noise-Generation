using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class HexMeshCreator : MonoBehaviour
{

    public GameObject HexMesh;

    // Position offset (top-left corner)
    public float xMax = -5.1f;
    public float yMax = 4.6f;

    // Position tools
    public float xCoord;
    public float yCoord;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xCoord = xMax;
        yCoord = yMax;
        HexMeshColomn1(xCoord, yCoord);
        HexMeshBuilder(xCoord, yCoord);
    }

    public void CallMap()
    {
        xCoord = xMax;
        yCoord = yMax;

        GameObject[] hexes = GameObject.FindGameObjectsWithTag("NoiseMesh");

        foreach (GameObject hex in hexes)
        {
            Destroy(hex);
        }

        HexMeshColomn1(xCoord, yCoord);
        HexMeshBuilder(xCoord, yCoord);
    }

    public void HexMeshColomn1(float x, float y)
    {
        for (int i = 0; i < 23; i++)
        {
            Instantiate(HexMesh, new Vector3(x, y, 0), Quaternion.identity);
            y = y - 0.345f;
        }

    }

    public void HexMeshBuilder(float x, float y)
    {
        yCoord = y;
        for (int i = 0; i < 34; i++)
        {
            x += 0.3f;
            y = yCoord;
            if ((i % 2) == 0)
            {
                y = y + 0.1725f;
                for (int n = 0; n < 24; n++)
                {
                    Instantiate(HexMesh, new Vector3(x, y, 0), Quaternion.identity);
                    y = y - 0.345f;
                }
            }
            else
            {
                for (int n = 0; n < 24; n++)
                {
                    Instantiate(HexMesh, new Vector3(x, y, 0), Quaternion.identity);
                    y = y - 0.345f;
                }
            }

        }
    }
    
}
