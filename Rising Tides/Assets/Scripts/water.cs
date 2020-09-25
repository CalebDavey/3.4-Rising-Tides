using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    List<Vector3> verts = new List<Vector3>();
    List<int> tris = new List<int>();

    public float waterSizeX = 20;
    public float waterSizeZ = 20;

    Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        formMesh(Vector3.zero, waterSizeX, waterSizeZ);
        displayMesh();
    }

    void formMesh(Vector3 startPos, float width, float height)
    {
        float y = startPos.y;
        int t = 0;

        for(int x = 0; x < width; x++) { 
            for(int z = 0; z < height; z++)
            {
                float xPos = x - width;
                float zPos = z - height;

                verts.Add(new Vector3(xPos, y, zPos));
                verts.Add(new Vector3(xPos + width, y, zPos));
                verts.Add(new Vector3(xPos + width, y, zPos + height));
                verts.Add(new Vector3(xPos, y, zPos + height));


                tris.Add(t);
                tris.Add(t + 3);
                tris.Add(t + 2);

                tris.Add(t);
                tris.Add(t + 2);
                tris.Add(t + 1);

                t += 2;
            }
        }

    }
    
    void displayMesh()
    {
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();

        mesh.RecalculateNormals();
    }
}
