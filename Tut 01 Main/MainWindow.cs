using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKSample
{
    public class MainWindow : GameWindow
    {
        public MainWindow()
            : base(800, 600, GraphicsMode.Default, "OpenTK Quick Start Sample")
        {
            VSync = VSyncMode.On;
        }

        int CreateShader(ShaderType shaderType, string shaderFile)
        {
            var shader = GL.CreateShader(shaderType);

            GL.ShaderSource(shader, shaderFile);
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out var status);

            if (status == 0)
            {
                var log = GL.GetShaderInfoLog(shader);
                Console.WriteLine($"Compile failure in {shader}: {log}");
            }
            return shader;
        }

       
        int CreateProgram(List<int> shaderList)
        {
            var program = GL.CreateProgram();
            shaderList.ForEach(x=> GL.AttachShader(program,x));

            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var status);
            //0 == false
            if(status == 0)
            {
                var log = GL.GetProgramInfoLog(program);
                Console.WriteLine($"Linker failure: {log}");
            }

            shaderList.ForEach(x => GL.DetachShader(program, x));
            return program;
        }
        int theProgram;

        const string vertexShader =
           @"#version 330
layout(location = 0) in vec4 position;
void main()
{
    gl_Position = position;
}";

        const string fragmentShader = 
            @"#version 330
out vec4 outputColor;
void main()
{
   outputColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);
};";

        void InitializeProgram()
        {
            var shaders = new List<int>
            {
                CreateShader(ShaderType.VertexShader,vertexShader),
                CreateShader(ShaderType.FragmentShader,fragmentShader),
            };
            theProgram = CreateProgram(shaders);
            shaders.ForEach(GL.DeleteShader);
        }

        static readonly float[] vertextPositions =
        {
            0.75f, 0.75f, 0.0f, 1.0f,
    0.75f, -0.75f, 0.0f, 1.0f,
    -0.75f, -0.75f, 0.0f, 1.0f,
        };


        uint positionBufferObject;
        uint vao;


        void InitializeVertexBuffer()
        {

            GL.GenBuffers(1, out positionBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, positionBufferObject);
            GL.BufferData(
               BufferTarget.ArrayBuffer,
               (IntPtr)(vertextPositions.Length * sizeof(float)),
               vertextPositions,
               BufferUsageHint.StaticDraw
               );
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }


        void init()
        {
            InitializeProgram();
            InitializeVertexBuffer();

            GL.GenVertexArrays(1, out vao);
            GL.BindVertexArray(vao);
        }



       
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";



            var backColor = new Color4(0f, 0, 0, 0);
            GL.ClearColor(backColor);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(theProgram);


            GL.BindBuffer(BufferTarget.ArrayBuffer, positionBufferObject);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 0, 0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DisableVertexAttribArray(0);
            GL.UseProgram(0);

            SwapBuffers();
        }


        
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            //Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.LoadMatrix(ref projection);
        }
        protected override void OnLoad(EventArgs e)
        {
            CursorVisible = true;
            init();
        }


    }
}
