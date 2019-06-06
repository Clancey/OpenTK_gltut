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
    class OrthoCube : GameWindow
    {
        public OrthoCube()
            : base(800, 600, GraphicsMode.Default, "Ortho Cube")
        {
            VSync = VSyncMode.On;
        }

        int theProgram;
        int offsetUniform;


        void InitializeProgram()
        {
            var shaders = new List<int>
            {
                Framework.LoadShader(ShaderType.VertexShader,"OrthoWithOffset.vert"),
                Framework.LoadShader(ShaderType.FragmentShader,"StandardColors.frag"),
            };

            theProgram = Framework.CreateProgram(shaders);
            offsetUniform = GL.GetUniformLocation(theProgram, "offset");
        }

        static readonly float[] vertexData = {
         0.25f,  0.25f, 0.75f, 1.0f,
	     0.25f, -0.25f, 0.75f, 1.0f,
	    -0.25f,  0.25f, 0.75f, 1.0f,

	     0.25f, -0.25f, 0.75f, 1.0f,
	    -0.25f, -0.25f, 0.75f, 1.0f,
	    -0.25f,  0.25f, 0.75f, 1.0f,

	     0.25f,  0.25f, -0.75f, 1.0f,
	    -0.25f,  0.25f, -0.75f, 1.0f,
	     0.25f, -0.25f, -0.75f, 1.0f,

	     0.25f, -0.25f, -0.75f, 1.0f,
	    -0.25f,  0.25f, -0.75f, 1.0f,
	    -0.25f, -0.25f, -0.75f, 1.0f,

	    -0.25f,  0.25f,  0.75f, 1.0f,
	    -0.25f, -0.25f,  0.75f, 1.0f,
	    -0.25f, -0.25f, -0.75f, 1.0f,

	    -0.25f,  0.25f,  0.75f, 1.0f,
	    -0.25f, -0.25f, -0.75f, 1.0f,
	    -0.25f,  0.25f, -0.75f, 1.0f,

	     0.25f,  0.25f,  0.75f, 1.0f,
	     0.25f, -0.25f, -0.75f, 1.0f,
	     0.25f, -0.25f,  0.75f, 1.0f,

	     0.25f,  0.25f,  0.75f, 1.0f,
	     0.25f,  0.25f, -0.75f, 1.0f,
	     0.25f, -0.25f, -0.75f, 1.0f,

	     0.25f,  0.25f, -0.75f, 1.0f,
	     0.25f,  0.25f,  0.75f, 1.0f,
	    -0.25f,  0.25f,  0.75f, 1.0f,

	     0.25f,  0.25f, -0.75f, 1.0f,
	    -0.25f,  0.25f,  0.75f, 1.0f,
	    -0.25f,  0.25f, -0.75f, 1.0f,

	     0.25f, -0.25f, -0.75f, 1.0f,
	    -0.25f, -0.25f,  0.75f, 1.0f,
	     0.25f, -0.25f,  0.75f, 1.0f,

	     0.25f, -0.25f, -0.75f, 1.0f,
	    -0.25f, -0.25f, -0.75f, 1.0f,
	    -0.25f, -0.25f,  0.75f, 1.0f,




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

            GL.Uniform2(offsetUniform, .5f,.25f);

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
            new OrthoCube().Run(60);
        }
    }
}
