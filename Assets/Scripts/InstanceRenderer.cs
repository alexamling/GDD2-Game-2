using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Using a static class for future modification, is nessicary
/// </summary>
public static class InstanceRenderer
{
    public static void Render(Mesh mesh, Material material, Matrix4x4[] transforms, int count)
    {
        Graphics.DrawMeshInstanced(mesh, 0, material, transforms, count);
    }
}
