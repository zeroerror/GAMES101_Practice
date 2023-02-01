using UnityEngine;

public class OrthographicProjection : MonoBehaviour
{
    public float width = 800;
    public float height = 600;
    public float near = 0.1f;
    public float far = 1000;

    public float width_half => width / 2f;
    public float height_half => height / 2f;

    Vector3 camPos => transform.position;
    Quaternion camRot => transform.rotation;

    public BoxCollider box;
    public Vector3 boxPos => box.transform.position;

    void Start()
    {
        GetScreenPos(boxPos);
    }

    void Update()
    {
        GetScreenPos(boxPos);
    }

    void OnDrawGizmos()
    {
        DrawCamera();
        Draw2DScreenView();
    }

    void Draw2DScreenView()
    {
        var boxPos = box.transform.position;
        var scale = box.transform.localScale;
        var scaleX_half = scale.x / 2f;
        var scaleY_half = scale.y / 2f;
        var scaleZ_half = scale.z / 2f;

        var p1_w = new Vector3(scaleX_half, scaleY_half, scaleZ_half);
        var p2_w = new Vector3(-scaleX_half, scaleY_half, scaleZ_half);
        var p3_w = new Vector3(scaleX_half, -scaleY_half, scaleZ_half);
        var p4_w = new Vector3(-scaleX_half, -scaleY_half, scaleZ_half);

        var p5_w = new Vector3(scaleX_half, scaleY_half, -scaleZ_half);
        var p6_w = new Vector3(-scaleX_half, scaleY_half, -scaleZ_half);
        var p7_w = new Vector3(scaleX_half, -scaleY_half, -scaleZ_half);
        var p8_w = new Vector3(-scaleX_half, -scaleY_half, -scaleZ_half);

        var rot = box.transform.rotation;
        p1_w = rot * p1_w;
        p2_w = rot * p2_w;
        p3_w = rot * p3_w;
        p4_w = rot * p4_w;
        p5_w = rot * p5_w;
        p6_w = rot * p6_w;
        p7_w = rot * p7_w;
        p8_w = rot * p8_w;

        p1_w = boxPos + p1_w;
        p2_w = boxPos + p2_w;
        p3_w = boxPos + p3_w;
        p4_w = boxPos + p4_w;
        p5_w = boxPos + p5_w;
        p6_w = boxPos + p6_w;
        p7_w = boxPos + p7_w;
        p8_w = boxPos + p8_w;

        var p1_s = GetScreenPos(p1_w);
        var p2_s = GetScreenPos(p2_w);
        var p3_s = GetScreenPos(p3_w);
        var p4_s = GetScreenPos(p4_w);
        var p5_s = GetScreenPos(p5_w);
        var p6_s = GetScreenPos(p6_w);
        var p7_s = GetScreenPos(p7_w);
        var p8_s = GetScreenPos(p8_w);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(p1_s, p5_s);
        Gizmos.DrawLine(p2_s, p6_s);
        Gizmos.DrawLine(p3_s, p7_s);
        Gizmos.DrawLine(p4_s, p8_s);
        Gizmos.DrawLine(p4_s, p8_s);

        Gizmos.DrawLine(p1_s, p2_s);
        Gizmos.DrawLine(p1_s, p3_s);
        Gizmos.DrawLine(p2_s, p4_s);
        Gizmos.DrawLine(p3_s, p4_s);

        Gizmos.DrawLine(p5_s, p6_s);
        Gizmos.DrawLine(p5_s, p7_s);
        Gizmos.DrawLine(p6_s, p8_s);
        Gizmos.DrawLine(p7_s, p8_s);
    }

    void DrawCamera()
    {
        Gizmos.color = Color.yellow;
        var p1 = camPos + new Vector3(width_half, height_half, 0);
        var p2 = camPos + new Vector3(-width_half, height_half, 0);
        var p3 = camPos + new Vector3(width_half, -height_half, 0);
        var p4 = camPos + new Vector3(-width_half, -height_half, 0);
        var p5 = p1 + new Vector3(0, 0, far);
        var p6 = p2 + new Vector3(0, 0, far);
        var p7 = p3 + new Vector3(0, 0, far);
        var p8 = p4 + new Vector3(0, 0, far);
        Gizmos.DrawLine(p1, p5);
        Gizmos.DrawLine(p2, p6);
        Gizmos.DrawLine(p3, p7);
        Gizmos.DrawLine(p4, p8);
        Gizmos.DrawLine(p4, p8);

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p1, p3);
        Gizmos.DrawLine(p2, p4);
        Gizmos.DrawLine(p3, p4);

        Gizmos.DrawLine(p5, p6);
        Gizmos.DrawLine(p5, p7);
        Gizmos.DrawLine(p6, p8);
        Gizmos.DrawLine(p7, p8);
    }

    Vector3 GetScreenPos(Vector3 worldPos)
    {
        // 正交投影矩阵
        Matrix4x4 projection = Utils.CreateOrthographicProjection(
         -(width_half), +(width_half),
         -(height_half), +(height_half),
         near, far);

        // 将投影矩阵和视图矩阵相乘
        Matrix4x4 viewMatrix = Matrix4x4.TRS(-camPos, camRot, Vector3.one);
        Matrix4x4 worldToViewMatrix = viewMatrix * Matrix4x4.TRS(worldPos, Quaternion.identity, Vector3.one);
        Matrix4x4 result = projection * worldToViewMatrix;
        Vector3 screenPos = new Vector3(result[0, 3] * width_half, result[1, 3] * height_half, result[2, 3]);
        return screenPos;
    }



}
