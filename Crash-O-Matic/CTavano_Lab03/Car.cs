using Chris_Tavano_Drawers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using GDIDrawer;
using System.Windows.Forms.VisualStyles;

namespace CTavano_Lab03 {
    public abstract class Car {
        private static DPictureColor _Pcanvas;                          // Static PicDrawer reference with Manual property
        public static DPictureColor Pcanvas { get => _Pcanvas; set { _Pcanvas?.Close(); _Pcanvas = value; } }
        public static Random Rng { get; set; } = new Random();          // random object as an Automatic property
        protected static List<int> Downs = new List<int>() { 172, 494 };// Down
        protected static List<int> Ups = new List<int>() { 263, 585 };  // Up
        protected static List<int> Left = new List<int>() { 167 };      // Left
        protected static List<int> Right = new List<int>() { 259 };     // Right
        protected int X, Y, Width, Height, Speed;                       // Int members for X,Y coordinates, width, height, speed
        protected bool MaxSpeed;                                        // Bool representing maxspeed and halfspeed

        //CTOR accepting speed, width, height of car
        public Car(int speed, int width, int height) {
            Speed = speed;
            Width = width;
            Height = height;
        }

        public abstract Rectangle GetRect();

        //Use NVI pattern to create a public ShowCar() and protected abstract VShowCar()
        public void ShowCar() => VShowCar();
        protected abstract void VShowCar();


        //Use NVI pattern to create a public Move() and protected abstract Move()
        public void Move() => VMove();
        protected abstract void VMove();


        //override Equals() return true if GetRect() are overlapping
        public override bool Equals(object obj) {
            if (!(obj is Car car)) return false;
            return GetRect().IntersectsWith(car.GetRect());
        }

        //override GetHashCode() return 0
        public override int GetHashCode() => 0;

        //Add PointOnCar(), accepts Point returns true if point is within the car's rectangle
        public bool PointOnCar(Point point) => GetRect().Contains(point);

        //Add Static helper predicate OutOfBounds(), return true if the car's rectangle is outisde the drawer
        public static bool OutOfBounds(Car car) {
            Rectangle window = new Rectangle(0, 0, Pcanvas.ScaledWidth, Pcanvas.ScaledHeight);
            //return rectangle.Contains(car.GetRect());
            return !window.IntersectsWith(car.GetRect()); //invert the output because it shouldn't contain it
        }

        //Add ToggleSpeed(), uses MaxSpeed bool member to toggle between half and full speed
        public void ToggleSpeed() => MaxSpeed = MaxSpeed ? false : true;

        public abstract int GetSafeScore();

        public abstract int GetHitScore();

    }

    abstract class HorizontalCar : Car {

        /// <summary>
        /// Horizontal Car Class Derived from Car
        /// </summary>
        /// <param name="speed">Speed of car</param>
        /// <param name="width">Width of car</param>
        /// <param name="height">Height of car</param>
        public HorizontalCar(int speed, int width, int height) : base(speed, width, height) {
            Speed = speed;
            Width = width;
            Height = height;

            X = Speed > 0 ? -Width : Pcanvas.ScaledWidth;
            Y = Speed > 0 ? Right[0] : Left[0];
        }

        //override VMove(), since we are traveling horizontally, increment X by our Speed
        protected override void VMove() => X += MaxSpeed ? Speed : Speed / 2;
    }

    abstract class VerticalCar : Car {

        /// <summary>
        /// Vertical Car Class Derived from Car
        /// </summary>
        /// <param name="speed">Speed of car</param>
        /// <param name="width">Width of car</param>
        /// <param name="height">Height of car</param>
        public VerticalCar(int speed, int width, int height) : base(speed, width, height) {
            Speed = speed;
            Width = width;
            Height = height;

            Y = Speed > 0 ? -Height : Pcanvas.ScaledHeight;
            X = Speed > 0 ? Downs[Rng.Next(0, 2)] : Ups[Rng.Next(0,2)];
        }

        //Override VMove(), since we are traveling vertically, increment Y by our Speed
        protected override void VMove() => Y += MaxSpeed ? Speed : Speed / 2;
    }

    class VSedan : VerticalCar {
        protected Color Color = RandColor.GetKnownColor();

        /// <summary>
        /// Represents a simple Vertically traveling Car
        /// </summary>
        /// <param name="speed">Speed of car</param>
        /// <param name="width">Width of car: default of 40</param>
        /// <param name="height">Height of car: default of 70</param>
        public VSedan(int speed, int width = 40, int height = 75) : base(speed, width, height) { }

        //Override GetRect(), Based on your size, generate your bounding Rectangle and return it
        public override Rectangle GetRect() => new Rectangle(X, Y, Width, Height);

        //Override VShowCar(), Using your GetRect() and perhaps some other decorations ( tires, etc ), draw your sedan.
        protected override void VShowCar() {
            Rectangle rectangle = GetRect();

            if (Speed > 0) {
                //car going down
                Pcanvas.AddRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, Color, 0, null);
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y - 1, 7, 2, Color.Red, 0, null);       // l/f light
                Pcanvas.AddRectangle(rectangle.X + 29, rectangle.Y - 1, 7, 2, Color.Red, 0, null);      // r/f light
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 17, 32, 10, Color.Gray, 0, null);   // f window
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 48, 32, 10, Color.Gray, 0, null);   // r window
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 74, 7, 2, Color.Yellow, 0, null);   // l/r light
                Pcanvas.AddRectangle(rectangle.X + 29, rectangle.Y + 74, 7, 2, Color.Yellow, 0, null);  // r/r light
            } else {
                //car going up
                Pcanvas.AddRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, Color, 0, null);
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y - 1, 7, 2, Color.Yellow, 0, null);    // l/f light
                Pcanvas.AddRectangle(rectangle.X + 29, rectangle.Y - 1, 7, 2, Color.Yellow, 0, null);   // r/f light
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 17, 32, 10, Color.Gray, 0, null);   // f window
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 48, 32, 10, Color.Gray, 0, null);   // r window
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 74, 7, 2, Color.Red, 0, null);      // l/r light
                Pcanvas.AddRectangle(rectangle.X + 29, rectangle.Y + 74, 7, 2, Color.Red, 0, null);     // r/r light
            }
        }

        //return the score of the vehicle making it safely across
        public override int GetSafeScore() => Width * Height / 2;

        //return the score if the vehicles hit
        public override int GetHitScore() => Width * Height;
    }

    //Interface for vehicle animation
    interface IAnimateable{
        //Gives the illusion of animation
        void Animate();
    }

    class HAmbulance : HorizontalCar, IAnimateable {
        //int red = 9;              //make the red siren slightly larger
        //int blue = 8;             //make the blue siren slightly smaller
        Color left = Color.Red;     //left light
        Color right = Color.Blue;   //right light

        /// <summary>
        /// Represents a HAmbulace traveling horizontally
        /// </summary>
        /// <param name="speed">Speed of car</param>
        /// <param name="width">Width of car: default of 40</param>
        /// <param name="height">Height of car: default of 70</param>
        public HAmbulance(int speed = 7, int width = 100, int height = 40) : base(speed, width, height) {}

        //IAnimateable support for VShowCar()
        public void Animate() {
            //red = red == 8 ? 7 : 8;
            //blue = blue == 7 ? 9 : 7;
            left = left == Color.White ? Color.Red : Color.White;
            right = right == Color.Red ? Color.White : Color.Red;
        }

        //Override GetRect(), Based on your size, generate your bounding Rectangle and return it
        public override Rectangle GetRect() => new Rectangle(X, Y, Width, Height);

        //Override VShowCar(), Using your GetRect() and perhaps some other decorations ( tires, etc ), draw your HAmbulance.
        protected override void VShowCar() {
            Rectangle rectangle = GetRect();

            if (Speed > 0) {
                Pcanvas.AddRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, Color.WhiteSmoke, 0, null);         //HAmbulance going right
                Pcanvas.AddRectangle(rectangle.X - 1, rectangle.Y + 3, 2, 9, Color.Red, 1, Color.Gray);     //top/light light
                Pcanvas.AddRectangle(rectangle.X - 1, rectangle.Y + 28, 2, 9, Color.Red, 1, Color.Gray);    //bottom/light light
                Pcanvas.AddRectangle(rectangle.X + 68, rectangle.Y + 4, 14, 32, Color.LightGray, 0, null);  //front window
                Pcanvas.AddRectangle(rectangle.X + 54, rectangle.Y + 4, 6, 16, left, 1, Color.Gray);        //top light
                Pcanvas.AddRectangle(rectangle.X + 54, rectangle.Y + 20, 6, 16, right, 1, Color.Gray);      //bottom light
                Pcanvas.AddRectangle(rectangle.X + 20, rectangle.Y + 5, 10, 30, Color.Red, 0, null);        //cross
                Pcanvas.AddRectangle(rectangle.X + 10, rectangle.Y + 15, 30, 10, Color.Red, 0, null);       //cross
                Pcanvas.AddRectangle(rectangle.X + 99, rectangle.Y + 3, 2, 9, Color.Yellow, 1, Color.Gray); //top/right light
                Pcanvas.AddRectangle(rectangle.X + 99, rectangle.Y + 28, 2, 9, Color.Yellow, 1, Color.Gray);//bottom/right light
            } else {
                Pcanvas.AddRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, Color.WhiteSmoke, 0, null);         //HAmbulance going left
                Pcanvas.AddRectangle(rectangle.X - 1, rectangle.Y + 3, 2, 9, Color.Yellow, 1, Color.Gray);  //top/light light
                Pcanvas.AddRectangle(rectangle.X - 1, rectangle.Y + 28, 2, 9, Color.Yellow, 1, Color.Gray); //bottom/light light
                Pcanvas.AddRectangle(rectangle.X + 17, rectangle.Y + 4, 14, 32, Color.LightGray, 0, null);  //front window
                Pcanvas.AddRectangle(rectangle.X + 38, rectangle.Y + 4, 6, 16, left, 1, Color.Gray);        //top light
                Pcanvas.AddRectangle(rectangle.X + 38, rectangle.Y + 20, 6, 16, right, 1, Color.Gray);      //bottom light
                Pcanvas.AddRectangle(rectangle.X + 65, rectangle.Y + 5, 10, 30, Color.Red, 0, null);        //cross
                Pcanvas.AddRectangle(rectangle.X + 55, rectangle.Y + 15, 30, 10, Color.Red, 0, null);       //cross
                Pcanvas.AddRectangle(rectangle.X + 99, rectangle.Y + 3, 2, 9, Color.Red, 1, Color.Gray);    //top/right light
                Pcanvas.AddRectangle(rectangle.X + 99, rectangle.Y + 28, 2, 9, Color.Red, 1, Color.Gray);   //bottom/right light
            }
        }
        //return the score of the vehicle making it safely across
        public override int GetSafeScore() => Width * Height / 2;

        //return the score if the vehicles hit
        public override int GetHitScore() => Width * Height;
    }

    class ETSBus : HorizontalCar,IAnimateable {
        Color destinationLight = Color.Orange;

        /// <summary>
        /// Represents an ETSBus traveling horizontally
        /// </summary>
        /// <param name="speed">Speed of the vehicle</param>
        /// <param name="width">Widths of the vehicle</param>
        /// <param name="height">Height of the vehicle</param>
        public ETSBus(int speed = 7, int width = 180, int height = 40) : base(speed, width, height) {}

        /// <summary>
        /// IAnimateable method for random flickering the front display of the bus.
        /// </summary>
        public void Animate(){
            switch (Rng.Next(0, 5)) {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    destinationLight = destinationLight == Color.Orange ? Color.Yellow : Color.Orange;
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }

        //Override GetRect(), Based on your size, generate your bounding Rectangle and return it
        public override Rectangle GetRect() => new Rectangle(X, Y, Width, Height);

        //Override VShowCar(), Using your GetRect() and perhaps some other decorations ( tires, etc ), draw your HAmbulance.
        protected override void VShowCar() {
            Rectangle rectangle = GetRect();

            if (Speed > 0) {
                Pcanvas.AddRectangle(rectangle.X, 256 + 3, 180, 40, Color.MediumBlue, 0, null);       //ETS going Right
                Pcanvas.AddRectangle(rectangle.X + 177, 256 + 6, 4, 34, Color.Black, 0, null);        //top/light light
                Pcanvas.AddRectangle(rectangle.X + 180, 256 + 8, 1, 30, destinationLight, 0, null);   //bottom/light light
                Pcanvas.AddRectangle(rectangle.X + 170, 256 + 6, 4, 2, Color.DarkOrange, 0, null);    //top lights
                Pcanvas.AddRectangle(rectangle.X + 170, 256 + 17, 4, 2, Color.DarkOrange, 0, null);   //top lights
                Pcanvas.AddRectangle(rectangle.X + 170, 256 + 22, 4, 2, Color.DarkOrange, 0, null);   //top lights
                Pcanvas.AddRectangle(rectangle.X + 170, 256 + 27, 4, 2, Color.DarkOrange, 0, null);   //top lights
                Pcanvas.AddRectangle(rectangle.X + 170, 256 + 37, 4, 2, Color.DarkOrange, 0, null);   //top lights
                Pcanvas.AddRectangle(rectangle.X + 145, 256 + 42, 25, 2, Color.Black, 0, null);       //front door
                Pcanvas.AddRectangle(rectangle.X + 60, 256 + 42, 40, 2, Color.Black, 0, null);        //bottom light
                Pcanvas.AddRectangle(rectangle.X + 15, 256 + 14, 6, 3, Color.Gray, 0, null);          //exhaust
                Pcanvas.AddText("ETS", 14, rectangle.X + 70, 256 + 13, 40, 20, Color.Gray);           //ETS text
                Pcanvas.AddRectangle(rectangle.X - 1, 256 + 6, 2, 9, Color.Red, 1, Color.Gray);       //top/right light
                Pcanvas.AddRectangle(rectangle.X - 1, 256 + 31, 2, 9, Color.Red, 1, Color.Gray);      //bottom/right light
            } else {
                Pcanvas.AddRectangle(rectangle.X, 167, 180, 40, Color.MediumBlue, 0, null);           //ETS going Left
                Pcanvas.AddRectangle(rectangle.X - 1, 167 + 2, 4, 34, Color.Black, 0, null);          //top/light light
                Pcanvas.AddRectangle(rectangle.X - 1, 167 + 4, 1, 30, destinationLight, 0, null);     //bottom/light light
                Pcanvas.AddRectangle(rectangle.X + 5, 167 + 2, 4, 2, Color.DarkOrange, 0, null);      //top lights
                Pcanvas.AddRectangle(rectangle.X + 5, 167 + 13, 4, 2, Color.DarkOrange, 0, null);     //top lights
                Pcanvas.AddRectangle(rectangle.X + 5, 167 + 18, 4, 2, Color.DarkOrange, 0, null);     //top lights
                Pcanvas.AddRectangle(rectangle.X + 5, 167 + 23, 4, 2, Color.DarkOrange, 0, null);     //top lights
                Pcanvas.AddRectangle(rectangle.X + 5, 167 + 33, 4, 2, Color.DarkOrange, 0, null);     //top lights
                Pcanvas.AddRectangle(rectangle.X + 10, 167 - 2, 25, 2, Color.Black, 0, null);         //front door
                Pcanvas.AddRectangle(rectangle.X + 80, 167 - 2, 40, 2, Color.Black, 0, null);         //bottom light
                Pcanvas.AddRectangle(rectangle.X + 160, 167 + 29, 6, 3, Color.Gray, 0, null);         //exhaust
                Pcanvas.AddText("ETS", 14, rectangle.X + 70, 167 + 9, 40, 20, Color.Gray);            //ETS text
                Pcanvas.AddRectangle(rectangle.X + 179, 167 + 2, 2, 9, Color.Red, 1, Color.Gray);     //top/right light
                Pcanvas.AddRectangle(rectangle.X + 179, 167 + 27, 2, 9, Color.Red, 1, Color.Gray);    //bottom/right light
            }
        }

        //return the score of the vehicle making it safely across
        public override int GetSafeScore() => Width * Height / 2;

        //return the score if the vehicles hit
        public override int GetHitScore() => Width * Height;
    }

    class CyberTruck : VerticalCar, IAnimateable{
        Color FontLights = Color.Yellow;

        /// <summary>
        /// CTOR for creating a Cybertruck
        /// </summary>
        /// <param name="speed">Speed of the vehicle</param>
        /// <param name="width">Width of the vehicle</param>
        /// <param name="height">Height of the vehicle</param>
        public CyberTruck(int speed = 10, int width = 40, int height = 90) : base(speed, width, height) {}

        //Override GetRect(), Based on your size, generate your bounding Rectangle and return it
        public override Rectangle GetRect() => new Rectangle(X, Y, Width, Height);

        //Override VShowCar(), to render the cybertruck
        protected override void VShowCar() {
            Rectangle rectangle = GetRect();

            if (Speed > 0) {
                //CyberTruck going down
                Pcanvas.AddRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, Color.Silver, 1, Color.Black);
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y - 1, 7, 2, Color.Red, 0, null);                               //l/f light
                Pcanvas.AddRectangle(rectangle.X + 29, rectangle.Y - 1, 7, 2, Color.Red, 0, null);                              //r/f light
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 40, 32, 15, Color.Gray, 0, null);                           //f window
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 60, 32, 15, Color.Gray, 0, null);                           //r window
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 5, 31, 28, Color.Silver, 1, Color.Black);                   //trunk
                Pcanvas.AddLine(rectangle.X + 4, rectangle.Y + 9, rectangle.X + 35, rectangle.Y + 9, Color.Black, 1);           //trunk lines
                Pcanvas.AddLine(rectangle.X + 4, rectangle.Y + 13, rectangle.X + 35, rectangle.Y + 13, Color.Black, 1);         //trunk lines
                Pcanvas.AddLine(rectangle.X + 4, rectangle.Y + 17, rectangle.X + 35, rectangle.Y + 17, Color.Black, 1);         //trunk lines
                Pcanvas.AddLine(rectangle.X + 4, rectangle.Y + 21, rectangle.X + 35, rectangle.Y + 21, Color.Black, 1);         //trunk lines
                Pcanvas.AddLine(rectangle.X + 4, rectangle.Y + 25, rectangle.X + 35, rectangle.Y + 25, Color.Black, 1);         //trunk lines
                Pcanvas.AddLine(rectangle.X + 4, rectangle.Y + 29, rectangle.X + 35, rectangle.Y + 29, Color.Black, 1);         //trunk lines
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 89, 7, 2, FontLights, 0, null);                             //l/r light
                Pcanvas.AddRectangle(rectangle.X + 29, rectangle.Y + 89, 7, 2, FontLights, 0, null);                            //r/r light
            } else {
                //CyberTruck going up
                Pcanvas.AddRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, Color.Silver, 1, Color.Black);
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y - 1, 7, 2, FontLights, 0, null);                              //l/f light
                Pcanvas.AddRectangle(rectangle.X + 29, rectangle.Y - 1, 7, 2, FontLights, 0, null);                             //r/f light
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 15, 32, 15, Color.Gray, 0, null);                           //f window
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 35, 32, 15, Color.Gray, 0, null);                           //r window
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 57, 31, 28, Color.Silver, 1, Color.Black);                  //trunk
                Pcanvas.AddLine(rectangle.X + 4, rectangle.Y + 61, rectangle.X + 35, rectangle.Y + 61, Color.Black, 1);         //trunk lines
                Pcanvas.AddLine(rectangle.X + 4, rectangle.Y + 65, rectangle.X + 35, rectangle.Y + 65, Color.Black, 1);         //trunk lines
                Pcanvas.AddLine(rectangle.X + 4, rectangle.Y + 69, rectangle.X + 35, rectangle.Y + 69, Color.Black, 1);         //trunk lines
                Pcanvas.AddLine(rectangle.X + 4, rectangle.Y + 73, rectangle.X + 35, rectangle.Y + 73, Color.Black, 1);         //trunk lines
                Pcanvas.AddLine(rectangle.X + 4, rectangle.Y + 77, rectangle.X + 35, rectangle.Y + 77, Color.Black, 1);         //trunk lines
                Pcanvas.AddLine(rectangle.X + 4, rectangle.Y + 81, rectangle.X + 35, rectangle.Y + 81, Color.Black, 1);         //trunk lines
                Pcanvas.AddRectangle(rectangle.X + 4, rectangle.Y + 89, 7, 2, Color.Red, 0, null);                              //l/r light
                Pcanvas.AddRectangle(rectangle.X + 29, rectangle.Y + 89, 7, 2, Color.Red, 0, null);                             //r/r light
            }
        }

        //return the score of the vehicle making it safely across
        public override int GetSafeScore() => Width * Height / 2;

        //return the score if the vehicles hit
        public override int GetHitScore() => Width * Height;

        /// <summary>
        /// IAnimateable method for random flickering the front lights of the cybertruck
        /// </summary>
        public void Animate(){
            switch (Rng.Next(0, 5)){
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    FontLights = FontLights == Color.Yellow ? Color.White : Color.Yellow;
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
    }
}