using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class waves : MonoBehaviour
{
    Vector3[][] vertices;
    Mesh[] planes;

    public float radius = 1.0f;
    public float wavelength = 3.0f;
    private float a = 1/3;
    public float frequency = 2.0f;

    public float radius2 = 0.2f;
    public float frequency2 = 4.0f;
    public float wavelength2 = 5.0f;
    private float a2 = 0.2f;
    

    private float t;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = -10; i <= 10; i++)
        {
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.position = new Vector3(10 * i, 0, 0);
            plane.transform.SetParent(gameObject.transform, false);
            planes[i + 10] = plane.GetComponent<MeshFilter>().mesh;
            vertices[i + 10] = planes[i + 10].vertices;
        }
        t = 0;
        a = 1 / wavelength;
        a2 = 1 / wavelength2;
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        for (int n = 0; n < planes.Length; n++)
        {
            for (int i = 0; i < vertices[n].Length; i++)
            {
                vertices[n][i].y = radius * Mathf.Sin(a * (vertices[n][i].x + gameObject.transform.position.x - radius * Mathf.Cos(a * vertices[n][i].x + gameObject.transform.position.x - frequency * t)) - frequency * t) + radius2 * Mathf.Sin(a2 * (vertices[n][i].z + gameObject.transform.position.z - radius2 * Mathf.Cos(a2 * vertices[n][i].z + gameObject.transform.position.z - frequency2 * t)) - frequency2 * t);
                vertices[n][i].y += (float)(0.4 * (0.5 - Mathf.PerlinNoise(1000000 * (vertices[n][i].x + gameObject.transform.position.x) + t, 1000000 * (vertices[n][i].z + gameObject.transform.position.z) + t)));
            }


            planes[n].vertices = vertices[n];
            planes[n].RecalculateBounds();
        }
    }
}
