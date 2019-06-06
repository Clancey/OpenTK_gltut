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
    class AspectRatio : GameWindow
    {
        public AspectRatio()
            : base(800, 800, GraphicsMode.Default, "Aspect Ratio")
        {
            VSync = VSyncMode.On;
        }

        int theProgram;
        int offsetUniform;
        int perspectiveMatrixUnif;


        float[] perspectiveMatrix;
        const float fFrustumScale = 1.0f;


        void InitializeProgram()
        {
            var shaders = new List<int>
            {
                Framework.LoadShader(ShaderType.VertexShader,"MatrixPerspective.vert"),
                Framework.LoadShader(ShaderType.FragmentShader,"StandardColors.frag"),
            };

            theProgram = Framework.CreateProgram(shaders);

            offsetUniform = GL.GetUniformLocation(theProgram, "offset");

            perspectiveMatrixUnif = GL.GetUniformLocation(theProgram, "perspectiveMatrix");

            float fzNear = 0.5f; float fzFar = 3.0f;

            perspectiveMatrix = new float[16];
            perspectiveMatrix[0] = fFrustumScale;
            perspectiveMatrix[5] = fFrustumScale;
            perspectiveMatrix[10] = (fzFar + fzNear) / (fzNear - fzFar);
            perspectiveMatrix[14] = (2 * fzFar * fzNear) / (fzNear - fzFar);
            perspectiveMatrix[11] = -1.0f;




            GL.UseProgram(theProgram);
            GL.UniformMatrix4(perspectiveMatrixUnif, 1, false, perspectiveMatrix);
            GL.UseProgram(0);
        }

        static readonly float[] vertexData = {
     0.25f,  0.25f, -1.25f, 1.0f,
     0.25f, -0.25f, -1.25f, 1.0f,
    -0.25f,  0.25f, -1.25f, 1.0f,

     0.25f, -0.25f, -1.25f, 1.0f,
    -0.25f, -0.25f, -1.25f, 1.0f,
    -0.25f,  0.25f, -1.25f, 1.0f,

     0.25f,  0.25f, -2.75f, 1.0f,
    -0.25f,  0.25f, -2.75f, 1.0f,
     0.25f, -0.25f, -2.75f, 1.0f,

     0.25f, -0.25f, -2.75f, 1.0f,
    -0.25f,  0.25f, -2.75f, 1.0f,
    -0.25f, -0.25f, -2.75f, 1.0f,

    -0.25f,  0.25f, -1.25f, 1.0f,
    -0.25f, -0.25f, -1.25f, 1.0f,
    -0.25f, -0.25f, -2.75f, 1.0f,

    -0.25f,  0.25f, -1.25f, 1.0f,
    -0.25f, -0.25f, -2.75f, 1.0f,
    -0.25f,  0.25f, -2.75f, 1.0f,

     0.25f,  0.25f, -1.25f, 1.0f,
     0.25f, -0.25f, -2.75f, 1.0f,
     0.25f, -0.25f, -1.25f, 1.0f,

     0.25f,  0.25f, -1.25f, 1.0f,
     0.25f,  0.25f, -2.75f, 1.0f,
     0.25f, -0.25f, -2.75f, 1.0f,

     0.25f,  0.25f, -2.75f, 1.0f,
     0.25f,  0.25f, -1.25f, 1.0f,
    -0.25f,  0.25f, -1.25f, 1.0f,

     0.25f,  0.25f, -2.75f, 1.0f,
    -0.25f,  0.25f, -1.25f, 1.0f,
    -0.25f,  0.25f, -2.75f, 1.0f,

     0.25f, -0.25f, -2.75f, 1.0f,
    -0.25f, -0.25f, -1.25f, 1.0f,
     0.25f, -0.25f, -1.25f, 1.0f,

     0.25f, -0.25f, -2.75f, 1.0f,
    -0.25f, -0.25f, -2.75f, 1.0f,
    -0.25f, -0.25f, -1.25f, 1.0f,




    0.0f, 0.0f, 1.0f, 1.0f,
    0.0f, 0.0f, 1.0f, 1.0f,
    0.0f, 0.0f, 1.0f, 1.0f,

    0.0f, 0.0f, 1.0f, 1.0f,
    0.0f, 0.0f, 1.0f, 1.0f,
    0.0f, 0.0f, 1.0f, 1.0f,

    0.8f, 0.8f, 0.8f, 1.0f,
    0.8f, 0.8f, 0.8f, 1.0f,
    0.8f, 0.8f, 0.8f, 1.0f,

    0.8f, 0.8f, 0.8f, 1.0f,
    0.8f, 0.8f, 0.8f, 1.0f,
    0.8f, 0.8f, 0.8f, 1.0f,

    0.0f, 1.0f, 0.0f, 1.0f,
    0.0f, 1.0f, 0.0f, 1.0f,
    0.0f, 1.0f, 0.0f, 1.0f,

    0.0f, 1.0f, 0.0f, 1.0f,
    0.0f, 1.0f, 0.0f, 1.0f,
    0.0f, 1.0f, 0.0f, 1.0f,

    0.5f, 0.5f, 0.0f, 1.0f,
    0.5f, 0.5f, 0.0f, 1.0f,
    0.5f, 0.5f, 0.0f, 1.0f,

    0.5f, 0.5f, 0.0f, 1.0f,
    0.5f, 0.5f, 0.0f, 1.0f,
    0.5f, 0.5f, 0.0f, 1.0f,

    1.0f, 0.0f, 0.0f, 1.0f,
    1.0f, 0.0f, 0.0f, 1.0f,
    1.0f, 0.0f, 0.0f, 1.0f,

    1.0f, 0.0f, 0.0f, 1.0f,
    1.0f, 0.0f, 0.0f, 1.0f,
    1.0f, 0.0f, 0.0f, 1.0f,

    0.0f, 1.0f, 1.0f, 1.0f,
    0.0f, 1.0f, 1.0f, 1.0f,
    0.0f, 1.0f, 1.0f, 1.0f,

    0.0f, 1.0f, 1.0f, 1.0f,
    0.0f, 1.0f, 1.0f, 1.0f,
    0.0f, 1.0f, 1.0f, 1.0f,

};


        uint vertexBufferObject;
        uint vao;


        void InitializeVertexBuffer()
        {

            GL.GenBuffers(1, out vertexBufferObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertexData.GetSize(), vertexData,
                BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }


        void init()
        {
            InitializeProgram();
            InitializeVertexBuffer();

            GL.GenVertexArrays(1, out vao);
            GL.BindVertexArray(vao);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Cw);
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

            GL.Uniform2(offsetUniform, .5f, .5f);

            var colorData = vertexData.GetSize(2);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 0, 0);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 0, colorData);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);


            GL.UseProgram(0);

            SwapBuffers();

        }



        protected override void OnResize(EventArgs e)
        {

            perspectiveMatrix[0] = fFrustumScale / ((float)Width / (float)Height);
            perspectiveMatrix[5] = fFrustumScale;



            GL.UseProgram(theProgram);
            GL.UniformMatrix4(perspectiveMatrixUnif, 1, false, perspectiveMatrix);
            GL.UseProgram(0);

            GL.Viewport(0, 0, Width, Height);


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
            new AspectRatio().Run(60);
        }
    }
}
