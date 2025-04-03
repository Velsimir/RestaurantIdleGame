using MainGame.Scripts.Logic;
using UnityEditor;
using UnityEngine;

namespace MainGame.Scripts.Editor
{
    [CustomEditor(typeof(NpcSpawner))]
    public class NpcSpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(NpcSpawner spawner, GizmoType gizmo)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(spawner.transform.position, 0.5f);
        }
    }
}