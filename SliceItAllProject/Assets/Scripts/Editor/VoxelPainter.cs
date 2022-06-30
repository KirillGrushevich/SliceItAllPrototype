using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class VoxelPainter : EditorWindow
    {
        private Color voxelColor = Color.white;
        
        [MenuItem("Tools/Voxel Painter")]
        private static void ShowWindow()
        {
            var window = GetWindow<VoxelPainter>();
            window.titleContent = new GUIContent("VoxelPainter");
            window.Show();
        }

        private void OnGUI()
        {
            voxelColor = EditorGUILayout.ColorField("VoxelColor", voxelColor);

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
                    mesh.name = $"Voxel_{voxelColor}";
                    var colors = new Color[mesh.vertexCount];
                    for (var i = 0; i < colors.Length; i++)
                    {
                        colors[i] = voxelColor;
                    }

                    mesh.colors = colors;
                    meshFilter.sharedMesh = mesh;
                    
                    EditorUtility.SetDirty(meshFilter);
                }
            }
        }
    }
}