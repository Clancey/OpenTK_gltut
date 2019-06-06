using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public static class Framework
{
    public static string LocalDir = "data";
    public static int LoadShader(ShaderType shaderType, string shaderFile)
    {
        var filePath = Path.Combine(LocalDir, shaderFile);
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Could not find the Shader",shaderFile);
        var data = File.ReadAllText(filePath);
        var shader = GL.CreateShader(shaderType);

        GL.ShaderSource(shader, data);
        GL.CompileShader(shader);

        GL.GetShader(shader, ShaderParameter.CompileStatus, out var status);

        if (status == 0)
        {
            var log = GL.GetShaderInfoLog(shader);
            throw new Exception(log);
        }
        return shader;

    }

}
