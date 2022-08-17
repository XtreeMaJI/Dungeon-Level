using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс, создающий шестиугольный меш из трапеций 
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexCellConstructor : MonoBehaviour
{
    private const int TRAPEZIES_COUNT = 6;
    private const int OUTER_VERTICES_COUNT = 6;
    private const int ANGLE_BETWEEN_VERTICES = 60;

    public float radius = 2f;
    public float borderWidth = 0.5f; //Значение от нуля до единицы. При единице весь шестиугольник будет зарисован

    private void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        mesh.Clear();

        //Вычисляем координаты вершин шестиугольника и добавляем их в меш
        List<Vector3> outerVertices = GetOuterVertices();
        List<Vector3> innerVertices = GetInnerVertices(outerVertices);
        List<Vector3> vertices = new List<Vector3>();
        vertices.AddRange(outerVertices);
        vertices.AddRange(innerVertices);
        mesh.vertices = vertices.ToArray();

        //Добавляем индексы вершин в массив треугольников, чтобы пара треугольников образовывала трапецию
        List<int> triangles = new List<int>();
        for (int i = 0; i < TRAPEZIES_COUNT; i++)
        {
            if (i + 1 != vertices.Count / 2)
            {
                List<int> firstTriangle = new List<int>();
                firstTriangle.Add(i);
                firstTriangle.Add(i + 1);
                firstTriangle.Add(i + vertices.Count / 2);
                triangles.AddRange(firstTriangle);

                List<int> secondTriangle = new List<int>();
                secondTriangle.Add(i + 1);
                secondTriangle.Add(i + 1 + vertices.Count / 2);
                secondTriangle.Add(i + vertices.Count / 2);
                triangles.AddRange(secondTriangle);
            }
            else
            {
                //Последняя трапеция
                List<int> firstTriangle = new List<int>();
                firstTriangle.Add(i);
                firstTriangle.Add(0);
                firstTriangle.Add(i + vertices.Count / 2);
                triangles.AddRange(firstTriangle);

                List<int> secondTriangle = new List<int>();
                secondTriangle.Add(0);
                secondTriangle.Add(vertices.Count / 2);
                secondTriangle.Add(i + vertices.Count / 2);
                triangles.AddRange(secondTriangle);
            }
        }

        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    List<Vector3> GetOuterVertices()
    {
        List<Vector3> vertices = new List<Vector3>();

        //Верхняя точка шестиугольника
        Vector3 topVert = Vector3.forward * radius;

        for(int i = 0; i < OUTER_VERTICES_COUNT; i++)
        {
            Vector3 newVert = Quaternion.AngleAxis(i * ANGLE_BETWEEN_VERTICES, Vector3.up) * topVert;
            vertices.Add(newVert);
        }

        return vertices;
    }

    List<Vector3> GetInnerVertices(List<Vector3> outerVertices)
    {
        List<Vector3> vertices = new List<Vector3>();

        foreach(Vector3 vert in outerVertices)
        {
            vertices.Add(vert * (1 - borderWidth));
        }

        return vertices;
    }
}
