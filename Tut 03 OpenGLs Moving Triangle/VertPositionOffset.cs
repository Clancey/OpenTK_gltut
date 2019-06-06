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
    class VertPositionOffset : GameWindow
    {
        public VertPositionOffset()
            : base(800, 600, GraphicsMode.Default, "Shader Calc Offset")
        {
            VSync = VSyncMode.On;
        }

        int theProgram;
        int elapsedTimeUniform;


        void InitializeProgram()
        {
            var shaders = new List<int>
            {
                Framework.LoadShader(ShaderType.VertexShader,"calcOffset.vert"),
                Framework.LoadShader(ShaderType.FragmentShader,"standard.frag"),
            };

            theProgram = Framework.CreateProgram(shaders);
            elapsedTimeUniform = GL.GetUniformLocation(theProgram, "time");

            var loopDurationUnf = GL.GetUniformLocation(theProgram, "loopDuration");
            GL.UseProgram(theProgram);
            GL.Uniform1(loopDurationUnf, 5f);
            GL.UseProgram(0);
        }

        static readonly float[] vertextPositions =
        {
            0.25f, 0.25f, 0.0f, 1.0f,
            0.25f, -0.25f, 0.0f, 1.0f,
            -0.25f, -0.25f, 0.0f, 1.0f,
        };


        uint positionBufferObject;
        uint vao;


        void InitializeVertexBuffer()
        {

            GL.GenBuffers(1, out positionBufferObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, positionBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertextPositions.GetSize(), vertextPositions, BufferUsageHint.StreamDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }


        void init()
        {
            InitializeProgram();
            InitializeVertexBuffer();

            GL.GenVertexArrays(1, out vao);
            GL.BindVertexArray(vao);
        }



        double elapsedTime;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";

            elapsedTime += e.Time;



            var backColor = new Color4(0f, 0, 0, 0);
            GL.ClearColor(backColor);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(theProgram);

            GL.Uniform1(elapsedTimeUniform, (float)elapsedTime);


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

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Exit();
            }
            base.OnKeyUp(e);
        }


    }


    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new VertPositionOffset().Run(60);
        }
    }
}
