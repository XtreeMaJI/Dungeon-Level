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
    private const float COLLIDER_HEIGHT = 0.25f;

    public float radius = 2f;
    public float borderWidth = 0.5f; //Значение от нуля до единицы. При единице весь шестиугольник будет зарисован

    public void UpdateMesh()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().sharedMesh = mesh;
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
        for (int pointIdx = 0; pointIdx < TRAPEZIES_COUNT; pointIdx++)
        {
            int nextPointIdx = (pointIdx + 1) % TRAPEZIES_COUNT;

            {
                List<int> firstTriangle = new List<int>();
                firstTriangle.Add(pointIdx);
                firstTriangle.Add(nextPointIdx);
                firstTriangle.Add(pointIdx + vertices.Count / 2);
                triangles.AddRange(firstTriangle);

                List<int> secondTriangle = new List<int>();
                secondTriangle.Add(nextPointIdx);
                secondTriangle.Add(nextPointIdx + vertices.Count / 2);
                secondTriangle.Add(pointIdx + vertices.Count / 2);
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
