using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKSample
{
    public class VertexColors : GameWindow
    {
        public VertexColors()
            : base(800, 600, GraphicsMode.Default, "OpenTK Quick Start Sample")
        {
            VSync = VSyncMode.On;
        }


        int CreateProgram(List<int> shaderList)
        {
            var program = GL.CreateProgram();
            shaderList.ForEach(x => GL.AttachShader(program, x));

            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var status);
            //0 == false
            if (status == 0)
            {
                var log = GL.GetProgramInfoLog(program);
                Console.WriteLine($"Linker failure: {log}");
            }

            shaderList.ForEach(x => GL.DetachShader(program, x));
            return program;
        }
        int theProgram;


        void InitializeProgram()
        {
            var shaders = new List<int>
            {
                Framework.LoadShader(ShaderType.VertexShader,"VertexColors.vert"),
                Framework.LoadShader(ShaderType.FragmentShader,"VertexColors.frag"),
            };

            theProgram = CreateProgram(shaders);
            shaders.ForEach(GL.DeleteShader);
        }

        static readonly float[] vertexData ={
             0.0f,    0.5f, 0.0f, 1.0f,
             0.5f, -0.366f, 0.0f, 1.0f,
            -0.5f, -0.366f, 0.0f, 1.0f,
             1.0f,    0.0f, 0.0f, 1.0f,
             0.0f,    1.0f, 0.0f, 1.0f,
             0.0f,    0.0f, 1.0f, 1.0f,
        };


        uint vertexBufferObject;
        uint vao;


        void InitializeVertexBuffer()
        {
            GL.GenBuffers(1, out vertexBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertexData.GetSize(), vertexData, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }


        void init()
        {
            InitializeProgram();
            InitializeVertexBuffer();

            GL.GenVertexArrays(1, out vao);
            GL.BindVertexArray(vao);
        }



        // Colors start on the third index, but we need to account for the size of the Data 
        static int colorOffset = Vector4.SizeInBytes * 3;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";



            var backColor = new Color4(0f, 0, 0, 0);
            GL.ClearColor(backColor);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(theProgram);


            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 0, 0);

            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 0, colorOffset);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
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

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Exit();
            }
            base.OnKeyUp(e);
        }


    }
}
