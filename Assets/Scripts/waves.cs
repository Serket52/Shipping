using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class waves : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;

    public float radius = 1.0f;
    public float wavelength = 3.0f;
    private float a = 1/3;
    public float frequency = 2.0f;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        a = 1 / wavelength;
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = radius * Mathf.Sin(a * (vertices[i].x - radius * Mathf.Cos(a * vertices[i].x - frequency * t)) - frequency * t) ;
            vertices[i].y += (float)(0.4*(0.5 - Mathf.PerlinNoise(1000000 * (vertices[i].x)+t, 1000000 * (vertices[i].z)+t)));
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }
}
