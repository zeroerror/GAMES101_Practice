using UnityEngine;

public static class Utils
{

    public static Matrix4x4 CreateOrthographicProjection(float left, float right, float bottom, float top, float near, float far)
    {
        Matrix4x4 result = new Matrix4x4();

        result[0, 0] = 2.0f / (right - left);
        result[1, 1] = 2.0f / (top - bottom);
        result[2, 2] = 2.0f / (near - far);
        result[0, 3] = (left + right) / (left - right);
        result[1, 3] = (bottom + top) / (bottom - top);
        result[2, 3] = (near + far) / (near - far);
        result[3, 3] = 1.0f;

        return result;
    }

    public static Matrix4x4 CreatePerspectiveProjection(float near, float far)
    {
        Matrix4x4 result = new Matrix4x4();

        result[0, 0] = -near;
        result[1, 1] = -near;
        result[2, 2] = -near + far;
        result[2, 3] = -near * far;
        result[3, 2] = 1;

        return result;
    }

}