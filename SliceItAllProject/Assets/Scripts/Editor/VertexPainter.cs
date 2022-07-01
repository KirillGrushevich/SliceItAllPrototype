using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class VertexPainter : EditorWindow
    {
        private Color color = Color.white;
        
        [MenuItem("Tools/Vertex Painter")]
        private static void ShowWindow()
        {
            var window = GetWindow<VertexPainter>();
            window.titleContent = new GUIContent("VertexPainter");
            window.Show();
        }

        private void OnGUI()
        {
            color = EditorGUILayout.ColorField("Color", color);

            if (Selection.activeObject != null && GUILayout.Button("Save"))
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
                    mesh.name = $"Mesh_{color}";
                    var colors = new Color[mesh.vertexCount];
                    for (var i = 0; i < colors.Length; i++)
                    {
                        colors[i] = color;
                    }

                    mesh.colors = colors;
                    meshFilter.sharedMesh = mesh;
                    
                    EditorUtility.SetDirty(meshFilter);
                }
            }
        }
    }
}