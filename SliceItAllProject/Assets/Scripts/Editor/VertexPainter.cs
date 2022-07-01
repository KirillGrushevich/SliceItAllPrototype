using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class VertexPainter : EditorWindow
    {
        private Color upColor = Color.white;
        private Color downColor = Color.black;

        [MenuItem("Tools/Vertex Painter")]
        private static void ShowWindow()
        {
            var window = GetWindow<VertexPainter>();
            window.titleContent = new GUIContent("VertexPainter");
            window.Show();
        }

        private void OnGUI()
        {
            upColor = EditorGUILayout.ColorField("Up Color", upColor);
            downColor = EditorGUILayout.ColorField("Down Color", downColor);

            if (Selection.activeObject != null && GUILayout.Button("Save"))
            {
                Save(false);
            }
            
            if (Selection.activeObject != null && GUILayout.Button("Save invert"))
            {
                Save(true);
            }
        }

        private void Save(bool invert)
        {
            foreach (var selectedObj in Selection.objects)
            {
                var obj = selectedObj as GameObject;
                if (obj == null)
                {
                    continue;
                }

                var meshFilter = obj.GetComponent<MeshFilter>();
                if (meshFilter == null)
                {
                    continue;
                }

                var mesh = Instantiate(meshFilter.sharedMesh);

                var minY = 0f;
                var maxY = 0f;

                foreach (var vertex in mesh.vertices)
                {
                    if (vertex.y < minY)
                    {
                        minY = vertex.y;
                        continue;
                    }

                    if (vertex.y > maxY)
                    {
                        maxY = vertex.y;
                    }
                }

                maxY -= minY;

                mesh.name = $"Mesh_{downColor}";
                var colors = new Color[mesh.vertexCount];
                for (var i = 0; i < colors.Length; i++)
                {
                    var t = (mesh.vertices[i].y + minY) / maxY;
                    if (invert)
                    {
                        t = 1f - t;
                    }
                    colors[i] = Color.Lerp(downColor, upColor, t);
                }

                mesh.colors = colors;
                meshFilter.sharedMesh = mesh;
                    
                EditorUtility.SetDirty(meshFilter);
            }
        }
    }
}