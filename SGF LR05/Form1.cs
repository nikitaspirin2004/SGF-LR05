using System;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SGF_LR05
{
    public partial class Form1 : Form
    {
        private GLControl glControl1;
        float dx = 0, dy = 0, clouddx = 0, roaddx = 0;

        float sx = 0.004f;
        float directX = 1, cloudDirectX = -1, roadDirectX = -1;
        public Form1()
        {
            InitializeComponent();
            glControl1 = new GLControl();
            glControl1.Resize += GLControl_Resize;
            glControl1.Load += GLControl_Load;
            glControl1.Paint += GLControl_Paint; 
            glControl1.Dock = DockStyle.Fill;
            pictureBox1.Controls.Add(glControl1);
        }

        private void GLControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
        }

        private void GLControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(0.5f, 1.0f, 1.0f, 1.0f); // цвет фона
        }

        private void GLControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            float ratio = (float)(glControl1.Width / (float)glControl1.Height);
            if (ratio > 1)
                GL.Ortho(0.0, 30.0 * ratio, 0.0, 30.0, -10, 10);
            else
                GL.Ortho(0.0, 30.0 / ratio, 0.0, 30.0, -10, 10);

            DrawRoad();
            DrawRoadLine();
            DrawCar();
            DrawCloud();

            glControl1.SwapBuffers();
            glControl1.Invalidate();
        }

        private void DrawCloud()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(clouddx, 0, 0);


            //
            GL.PushMatrix();

            GL.Color3(1f, 1f, 1f);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(45, 28);
            GL.Vertex2(50, 28);
            GL.Vertex2(50, 25);
            GL.Vertex2(45, 25);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(0 + 60, 24);
            GL.Vertex2(12 + 60, 24);
            GL.Vertex2(12 + 60, 19);
            GL.Vertex2(0 + 60, 19);
            GL.End();

            GL.PopMatrix();

            if (clouddx <= -80) clouddx = 10;
            clouddx += cloudDirectX * sx;
        }

        private void DrawRoad()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.PushMatrix();

            GL.Color3(0.1f, 0.1f, 0.1f);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(0, 0);
            GL.Vertex2(80, 0);
            GL.Vertex2(80, 6.5);
            GL.Vertex2(0, 6.5);
            GL.End();

            GL.PopMatrix();

        }

        private void DrawRoadLine()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(roaddx, 0, 0);

            GL.PushMatrix();

            GL.Color3(1f, 1f, 1f);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(36, 1);
            GL.Vertex2(46, 1);
            GL.Vertex2(46, 2);
            GL.Vertex2(36, 2);
            GL.End();

            GL.PopMatrix();

            if (roaddx <= -60) roaddx = 20;
            roaddx += roadDirectX * sx * 8;
        }
        private void DrawCar()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();


            //
            GL.PushMatrix();

            // Рисуем горизонтальную часть машины
            GL.Color3(0f, 0f, 0f); 
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(6, 5);
            GL.Vertex2(30, 5);
            GL.Vertex2(30, 10);
            GL.Vertex2(6, 10);
            GL.End();


            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(12, 10);
            GL.Vertex2(23, 10);
            GL.Vertex2(23, 15);
            GL.Vertex2(12, 15);
            GL.End();



            GL.Color4(0.5f, 1.0f, 1.0f, 1.0f);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(13, 10);
            GL.Vertex2(17, 10);
            GL.Vertex2(17, 14);
            GL.Vertex2(13, 14);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(18, 10);
            GL.Vertex2(22, 10);
            GL.Vertex2(22, 14);
            GL.Vertex2(18, 14);
            GL.End();

            double xc = 12, yc = 5, r = 2;
            double x, y;
            GL.Color3(0.3f, 0.3f, 0.3f);
            GL.Begin(PrimitiveType.TriangleFan); // веер треугольников
            GL.Vertex2(xc + 0.8f, yc + 0.3);
            GL.Vertex2(xc + 1, yc);
            for (int i = 0; i <= 30; i++)
            {
                GL.Color3(0.3f, 0.3f, 0.3f);
                x = xc + r * Math.Sin(i * Math.PI / 15);
                y = yc + r * Math.Cos(i * Math.PI / 15);
                GL.Vertex2(x, y);
            }
            GL.End();

            double xc1 = 23, yc1 = 5, r1 = 2;
            GL.Begin(PrimitiveType.TriangleFan); // веер треугольников
            GL.Vertex2(xc1 + 0.8f, yc1 + 0.3);
            GL.Vertex2(xc1 + 1, yc1);
            for (int i = 0; i <= 30; i++)
            {
                GL.Color3(0.3f, 0.3f, 0.3f);
                x = xc1 + r * Math.Sin(i * Math.PI / 15);
                y = yc1 + r * Math.Cos(i * Math.PI / 15);
                GL.Vertex2(x, y);
            }
            GL.End();



            GL.PopMatrix();
        }
    }
}