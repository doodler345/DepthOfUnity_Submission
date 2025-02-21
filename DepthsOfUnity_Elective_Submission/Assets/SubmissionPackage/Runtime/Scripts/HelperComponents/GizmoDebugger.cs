using UnityEngine;

namespace GizmoDebugger
{
    public class GizmoDebugger : MonoBehaviour
    {
        public enum GizmoTypes
        {
            Cube,
            Sphere,
            Line,
            Ray,
            WireCube,
            WireSphere
        }

        public GizmoTypes GizmoType { get => gizmoType; }
        [SerializeField] private GizmoTypes gizmoType = GizmoTypes.WireSphere;
        public bool OnlyDrawWhenSelected { get => onlyDrawWhenSelected; }
        [SerializeField] private bool onlyDrawWhenSelected = true;

        public Color gizmoColor = Color.white;
        public Vector3 originPositionOffset = Vector3.zero;
        public Vector3 endPositionOffset = Vector3.up;
        public Vector3 direction = Vector3.up;
        public Vector3 size = Vector3.one;
        public float radius = 0.5f;

    #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (onlyDrawWhenSelected) return;
            DrawGizmo();
        }

        private void OnDrawGizmosSelected()
        {
            if (!onlyDrawWhenSelected) return;
            DrawGizmo();
        }

        private void DrawGizmo()
        {
            Gizmos.color = gizmoColor;
            Vector3 origin = transform.position + transform.right * originPositionOffset.x + transform.up * originPositionOffset.y + transform.forward * originPositionOffset.z;

            switch (gizmoType)
            {
                case GizmoTypes.Cube:
                    Gizmos.DrawCube(origin, size);
                    break;
                case GizmoTypes.Sphere:
                    Gizmos.DrawSphere(origin, radius);
                    break;
                case GizmoTypes.Line:
                    Vector3 endPosition = transform.position + transform.right * endPositionOffset.x + transform.up * endPositionOffset.y + transform.forward * endPositionOffset.z;
                    Gizmos.DrawLine(origin, endPosition);
                    break;
                case GizmoTypes.Ray:
                    Gizmos.DrawRay(origin, direction);
                    break;
                case GizmoTypes.WireCube:
                    Gizmos.DrawWireCube(origin, size);
                    break;
                case GizmoTypes.WireSphere:
                    Gizmos.DrawWireSphere(origin, radius);
                    break;
            }
        }
    #endif
    }
}
