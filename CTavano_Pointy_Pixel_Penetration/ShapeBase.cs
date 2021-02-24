using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CTavano_Pointy_Pixel_Penetration
{
    public abstract class ShapeBase{
        protected float m_fRot;                         //Rotation
        protected float m_fRotInc;                      //Rotation increment?
        protected float m_fxSpeed;                      //X speed
        protected float m_fySpeed;                      //Y speed
        public static Random s_rng = new Random();      //Random number gen
        public const float TILESIZE = 50;               //Constant size for shapes
        public bool IsMarkedForDeath { get; set; }      //Bool for if a shape is marked for dead
        public PointF Position { get; protected set; }  //Position of the shape on the screen

        public ShapeBase(PointF pointF) {
            m_fRot = 0;                                         //Set the rotation
            m_fRotInc = (float)(s_rng.NextDouble() * 6 - 3);    //Set the rotation increment
            m_fxSpeed = (float)(s_rng.NextDouble() * 5 - 2.5);  //Set the x speed
            m_fySpeed = (float)(s_rng.NextDouble() * 5 - 2.5);  //Set the y speed
            Position = pointF;                                  //Set the Position of the shape
        }

        static protected GraphicsPath GenModel(int vertices, double variance) {
            GraphicsPath Shape = new GraphicsPath();                    //To hold our Shape
            PointF[] points = new PointF[vertices];                     //Array for the points of our shape
            double theta = (2 * Math.PI) / vertices;                    //Get our angle in Rad
            double delta = theta;                                       //To hold our total angle
            int min = (int)(TILESIZE - (TILESIZE * (variance / 100)));  //Our min value for random variance generation

            for (int i = 0; i < vertices; i++){
                PointF tempP = new PointF();

                //Create points based on if there is variance or not
                if (variance == 0){
                    tempP.X = (float)(Math.Cos(delta) * TILESIZE);
                    tempP.Y = (float)(Math.Sin(delta) * TILESIZE);
                }
                else {
                    tempP.X = (float)(Math.Cos(delta) * s_rng.Next(min ,(int)TILESIZE + 1));
                    tempP.Y = (float)(Math.Sin(delta) * s_rng.Next(min, (int)TILESIZE + 1));
                }
                
                points[i] = tempP;      //Add the point to the array                
                delta += theta;         //Increase the angle by theta
            }
            
            Shape.AddPolygon(points);   //Add the points to create a polygon

            return Shape;    
        }

        //Abstract method for derived classes to transform based on shape type
        public abstract GraphicsPath GetPath();

        //base class Render method can rely on the polymorphic GetPath method to produce the graphics path for rendering.
        //Render and Tick will be the same for all shapes, so the concrete implementations should exist in the base class.
        //The Render method will simply fill the GetPath return value with a provided colour.
        public void Render(Graphics bg, Color fillColor) {
            //bg.DrawPath(new Pen(fillColor), GetPath());
            bg.FillPath(new SolidBrush(fillColor), GetPath());
        }

        //The Tick method will accept a Size and will move the shape according to the current speed values. 
        //For any violation of the bounds as specified in the Size parameter, the appropriate speed value will flip sign.
        public void Tick(Size Window) {
            m_fRot += m_fRotInc;

            //Check to see if the shape is going to be out of bounds\
            if (Position.X + m_fxSpeed <= 0 || Position.X + m_fxSpeed >= Window.Width - (TILESIZE / 2))
                m_fxSpeed *= -1;

            else
                Position = new PointF(Position.X + m_fxSpeed, Position.Y);

            if (Position.Y + m_fySpeed <= 0 || Position.Y + m_fySpeed >= Window.Height - TILESIZE)
                m_fySpeed *= -1;

            else
                Position = new PointF(Position.X, Position.Y + m_fySpeed);
        }

        //Use the distance formula to get the distance between the two shapes d=√((x_2-x_1)²+(y_2-y_1)²)
        public double GetDistance(ShapeBase s2) => Math.Sqrt(Math.Pow(s2.Position.X - Position.X, 2) + Math.Pow(s2.Position.Y - Position.Y, 2));

        //Draw a circle around the shape to mimic a shield
        public void SheildsUp(Graphics bg) {
            bg.DrawEllipse(new Pen(Color.LightBlue, 2), Position.X - TILESIZE , Position.Y - TILESIZE, TILESIZE * 2, TILESIZE * 2);
        }
    }

    /// <summary>
    /// Triangle class
    /// </summary>
    class Triangle : ShapeBase {
        static readonly GraphicsPath s_model;

        //Static constructor that will generate the triangle model based on our criteria.
        static Triangle() => s_model = GenModel(3, 0);

        //Constructor that accepts a PointF. Set the Location to the provided PointF
        public Triangle(PointF pF) : base(pF) { }

        //Add a GetPath method override that will return a GraphicsPath.This method will return a GraphicsPath that is a fully transformed clone 
        //of the triangle model.
        public override GraphicsPath GetPath(){
            Matrix matrix = new Matrix();
            matrix.Rotate(m_fRot);
            matrix.Translate(Position.X, Position.Y, MatrixOrder.Append);

            GraphicsPath clone = (GraphicsPath)s_model.Clone();
            clone.Transform(matrix);

            return clone;
        }
    }

    /// <summary>
    /// Rock class
    /// </summary>
    class Rock : ShapeBase { 
        readonly GraphicsPath _model;

        //Constructor that accepts a PointF and generates a random rock shape
        public Rock(PointF pF) : base(pF) {
            _model = GenModel(s_rng.Next(4,13), s_rng.NextDouble() * 100);    
        }

        //GetPath method override that will return a GraphicsPath. It will return a fully transformed clone of a rock model.
        public override GraphicsPath GetPath(){
            Matrix matrix = new Matrix();
            matrix.Rotate(m_fRot);
            matrix.Translate(Position.X, Position.Y, MatrixOrder.Append);

            GraphicsPath clone = (GraphicsPath)_model.Clone();
            clone.Transform(matrix);

            return clone;
        }
    }
}
