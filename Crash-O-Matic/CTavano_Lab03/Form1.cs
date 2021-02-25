/////////////////////////////////////////////////////////////////////////////////////////////////
///Chris Tavano
///
///Crash-O-Matic
///Get the car to the other side of the screen. Earn points if it makes it to the end,
///lose points if it crashes.
/////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;
using Chris_Tavano_Drawers;
using System.Diagnostics;

namespace CTavano_Lab03{
    public partial class CarGame : Form{
        readonly DPictureColor _Canvas = new DPictureColor(Properties.Resources.Crash) {ContinuousUpdate = false};
        readonly Timer Timer, CarSpawnTimer;
        List<Car> _Cars = new List<Car>();
        List<Car> _TempCars = new List<Car>();
        int _TotalScore;

        public CarGame() {
            InitializeComponent();
            Car.Pcanvas = _Canvas;
            Shown += SnapForm;
            Timer = new Timer(){ Interval = 50 };
            Timer.Start();
            Timer.Tick += Tick;
            CarSpawnTimer = new Timer{ Interval = 2000 };
            CarSpawnTimer.Start();
            CarSpawnTimer.Tick += CarSpawnTimer_Tick;
            _Canvas.MouseLeftClick += MouseLeftClick;
        }

        /// <summary>
        /// Timer that we will use to spawn vehicles to the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarSpawnTimer_Tick(object sender, EventArgs e){
            switch (Car.Rng.Next(0, 8)){
                case 0:
                    _Cars.Add(new VSedan(Car.Rng.Next(5,10)));
                    break;
                case 1:
                    _Cars.Add(new HAmbulance(Car.Rng.Next(7, 11)));
                    break;
                case 2:
                    _Cars.Add(new ETSBus(Car.Rng.Next(7, 11)));
                    break;
                case 3:
                    _Cars.Add(new CyberTruck(Car.Rng.Next(7,11)));
                    break;
                case 4:
                    _Cars.Add(new VSedan(Car.Rng.Next(-10, -4)));
                   break;
                case 5:
                    _Cars.Add(new HAmbulance(Car.Rng.Next(-10, -4)));
                    break;
                case 6:
                    _Cars.Add(new ETSBus(Car.Rng.Next(-10, -4)));
                    break;
                case 7:
                    _Cars.Add(new CyberTruck(Car.Rng.Next(-10,-4)));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Main timer that will detect vehicle collision, safe travel, and update scoring.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick(object sender, EventArgs e) {
            if (_Canvas == null || _Cars == null) return;               //check to see if the canvas or the list of cars are null
            else {
                _Canvas.Clear();                                        //clear the canvas
                _Cars.ForEach(car => car.Move());                       //move all the vehicles
                _Cars.Where(car => car is IAnimateable).ToList().ForEach(car => (car as IAnimateable)?.Animate());  //animate all the ones that support it
                _Cars.ForEach(car => car.Move());                       //move all the vehicles again

                _TempCars = _Cars.ToList();                             //put the list of vehicles into a temp list

                Car car1;                                               //car 1 to compare
                Car car2;                                               //car 2 to compare
                Rectangle car1Rect;                                     //area of car1


                for (int i = 0; i < _TempCars.Count - 1; i++) {         //itterate through the list
                    car1 = _TempCars[i];                                //get the first car from the list
                    car1Rect = car1.GetRect();                          //and get its area to compare with
                    for (int j = i + 1; j < _TempCars.Count; j++){      //iterate through the list starting at the next car
                        car2 = _TempCars[j];                            //get the second car to compare    
                        if (car1Rect.IntersectsWith(car2.GetRect())){   //check to see if they hit eachother
                            _Cars.Remove(car1);                         //remove both of them if they did
                            _TotalScore -= car1.GetHitScore();
                            _Cars.Remove(car2);
                            _TotalScore -= car2.GetHitScore();
                        }
                    }

                    if (Car.OutOfBounds(car1)) {                        //if the vehicle made it across safely
                        _Cars.Remove(car1);                             //remove the vehicle
                        _TotalScore += car1.GetSafeScore();             //add the score to the total score
                    }
                }

                if(_TotalScore < 0) { 
                    Timer.Stop();                                       //stop the main form timer
                    CarSpawnTimer.Stop();                               //stop spawning cars timer
                    _Canvas.AddText("Game Over", 40, (_Canvas.ScaledWidth / 4) + 50, (_Canvas.ScaledHeight / 4) + 65, 300, 100, Color.Yellow);
                }

                //show the score to the window
                _Canvas.AddText(_TotalScore.ToString(), 20, (_Canvas.ScaledWidth / 4) + 50, 65, 300, 100, Color.Yellow);

                _Cars.ForEach(car => car.ShowCar());                    //print all the cars to the screen
                _Canvas.Render();                                       //render the canvas
                
            }
        }

        /// <summary>
        /// Form event handler to snap the canvas window to the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnapForm(object sender, EventArgs e) => _Canvas.Position = new Point(Location.X + Width - 17, Location.Y);

        /// <summary>
        /// MouseLeftClick event handler for the drawer
        /// </summary>
        /// <param name="pos">position of the left click</param>
        /// <param name="dr"> the drawer window</param>
        private void MouseLeftClick(Point pos, CDrawer dr) => _Cars.Where(car => car.PointOnCar(pos)).ToList().ForEach(car => car.ToggleSpeed());

    }
}

/*
//CAR
Pcanvas.AddRectangle(263, 172, Width, Height, Color, 0, null);          //car
Pcanvas.AddRectangle(267 + 4, 172 - 1, 7, 2, Color.Red, 0, null);       //l/f light
Pcanvas.AddRectangle(292 + 29, 172 - 1, 7, 2, Color.Red, 0, null);      //r/f light
Pcanvas.AddRectangle(267 + 4, 172 + 17, 32, 10, Color.Gray, 0, null);   //f window
Pcanvas.AddRectangle(267 + 4, 172 + 48, 32, 10, Color.Gray, 0, null);   //r window
Pcanvas.AddRectangle(267 + 4, 172 + 74, 7, 2, Color.Yellow, 0, null);   //l/r light
Pcanvas.AddRectangle(292 + 29, 172 + 74, 7, 2, Color.Yellow, 0, null);  //r/r light

_Canvas.AddRectangle(263, Vcar, 40, 75, Color.Orange, 0, null);     //car
_Canvas.AddRectangle(267, Vcar - 1, 7, 2, Color.Yellow, 0, null);   //l/f light
_Canvas.AddRectangle(292, Vcar - 1, 7, 2, Color.Yellow, 0, null);   //r/f light
_Canvas.AddRectangle(267, Vcar + 17, 32, 10, Color.Gray, 0, null);  //f window
_Canvas.AddRectangle(267, Vcar + 48, 32, 10, Color.Gray, 0, null);  //r window
_Canvas.AddRectangle(267, Vcar + 74, 7, 2, Color.Red, 0, null);     //l/r light
_Canvas.AddRectangle(292, Vcar + 74, 7, 2, Color.Red, 0, null);     //r/r light

//HAMBULANCE
_Canvas.AddRectangle(rectangle, 167 - 1, 100, 40, Color.WhiteSmoke, 0, null);         //HAmbulance
_Canvas.AddRectangle(rectangle - 1, 167 - 1 + 3, 2, 9, Color.Yellow, 1, Color.Gray);  //top/light light
_Canvas.AddRectangle(rectangle - 1, 167 - 1 + 28, 2, 9, Color.Yellow, 1, Color.Gray); //bottom/light light
_Canvas.AddRectangle(rectangle + 17, 167 - 1 + 4, 14, 32, Color.LightGray, 0, null);  //front window
_Canvas.AddRectangle(rectangle + 38, 167 - 1 + 4, 6, 16, Color.White, 1, Color.Gray); //top light
_Canvas.AddRectangle(rectangle + 38, 167 - 1 + 20, 6, 16, Color.Red, 1, Color.Gray);  //bottom light
_Canvas.AddRectangle(rectangle + 70, 167 - 1 + 5, 10, 30, Color.Red, 0, null);        //cross
_Canvas.AddRectangle(rectangle + 60, 167 - 1 + 15, 30, 10, Color.Red, 0, null);       //cross
_Canvas.AddRectangle(rectangle + 99, 167 - 1 + 3, 2, 9, Color.Red, 1, Color.Gray);    //top/right light
_Canvas.AddRectangle(rectangle + 99, 167 - 1 + 28, 2, 9, Color.Red, 1, Color.Gray);   //bottom/right light

_Canvas.AddRectangle(rectangle, 167, 100, 40, Color.WhiteSmoke, 0, null);         //HAmbulance
_Canvas.AddRectangle(rectangle - 1, 167 + 3, 2, 9, Color.Red, 1, Color.Gray);     //top/light light
_Canvas.AddRectangle(rectangle - 1, 167 + 28, 2, 9, Color.Red, 1, Color.Gray);    //bottom/light light
_Canvas.AddRectangle(rectangle + 68, 167 + 4, 14, 32, Color.LightGray, 0, null);  //front window
_Canvas.AddRectangle(rectangle + 54, 167 + 4, 6, 16, Color.White, 1, Color.Gray); //top light
_Canvas.AddRectangle(rectangle + 54, 167 + 20, 6, 16, Color.Red, 1, Color.Gray);  //bottom light
_Canvas.AddRectangle(rectangle + 20, 167 + 5, 10, 30, Color.Red, 0, null);        //cross
_Canvas.AddRectangle(rectangle + 10, 167 + 15, 30, 10, Color.Red, 0, null);       //cross
_Canvas.AddRectangle(rectangle + 99, 167 + 3, 2, 9, Color.Yellow, 1, Color.Gray); //top/right light
_Canvas.AddRectangle(rectangle + 99, 167 + 28, 2, 9, Color.Yellow, 1, Color.Gray);//bottom/right light

//BUS
_Canvas.AddRectangle(rectangle, 167, 180, 40, Color.MediumBlue, 0, null);           //ETS Top/Left
_Canvas.AddRectangle(rectangle - 1, 167 + 2, 4, 34, Color.Black, 0, null);          //top/light light
_Canvas.AddRectangle(rectangle - 1, 167 + 4, 1, 30, Color.Orange, 0, null);         //bottom/light light
_Canvas.AddRectangle(rectangle + 5, 167 + 2, 4, 2, Color.DarkOrange, 0, null);      //top lights
_Canvas.AddRectangle(rectangle + 5, 167 + 13, 4, 2, Color.DarkOrange, 0, null);     //top lights
_Canvas.AddRectangle(rectangle + 5, 167 + 18, 4, 2, Color.DarkOrange, 0, null);     //top lights
_Canvas.AddRectangle(rectangle + 5, 167 + 23, 4, 2, Color.DarkOrange, 0, null);     //top lights
_Canvas.AddRectangle(rectangle + 5, 167 + 33, 4, 2, Color.DarkOrange, 0, null);     //top lights
_Canvas.AddRectangle(rectangle + 10, 167 - 2, 25, 2, Color.Black, 0, null);         //front door
_Canvas.AddRectangle(rectangle + 80, 167 - 2, 40, 2, Color.Black, 0, null);         //bottom light
_Canvas.AddRectangle(rectangle + 160, 167 + 29, 6, 3, Color.Gray, 0, null);         //exhaust
_Canvas.AddText("ETS",14,rectangle + 60, 167 + 9, 40, 20,Color.Gray);               //ETS text
_Canvas.AddRectangle(rectangle + 179, 167 + 2, 2, 9, Color.Red, 1, Color.Gray);     //top/right light
_Canvas.AddRectangle(rectangle + 179, 167 + 27, 2, 9, Color.Red, 1, Color.Gray);    //bottom/right light

_Canvas.AddRectangle(rectangle, 256 + 3, 180, 40, Color.MediumBlue, 0, null);       //ETS Bottom/Right
_Canvas.AddRectangle(rectangle + 177, 256 + 6, 4, 34, Color.Black, 0, null);        //top/light light
_Canvas.AddRectangle(rectangle + 180, 256 + 8, 1, 30, Color.Orange, 0, null);       //bottom/light light
_Canvas.AddRectangle(rectangle + 170, 256 + 6, 4, 2, Color.DarkOrange, 0, null);    //top lights
_Canvas.AddRectangle(rectangle + 170, 256 + 17, 4, 2, Color.DarkOrange, 0, null);   //top lights
_Canvas.AddRectangle(rectangle + 170, 256 + 22, 4, 2, Color.DarkOrange, 0, null);   //top lights
_Canvas.AddRectangle(rectangle + 170, 256 + 27, 4, 2, Color.DarkOrange, 0, null);   //top lights
_Canvas.AddRectangle(rectangle + 170, 256 + 37, 4, 2, Color.DarkOrange, 0, null);   //top lights
_Canvas.AddRectangle(rectangle + 145, 256 + 42, 25, 2, Color.Black, 0, null);       //front door
_Canvas.AddRectangle(rectangle + 60, 256 + 42, 40, 2, Color.Black, 0, null);        //bottom light
_Canvas.AddRectangle(rectangle + 15, 256 + 14, 6, 3, Color.Gray, 0, null);          //exhaust
_Canvas.AddText("ETS", 14, rectangle + 80, 256 + 13, 40, 20, Color.Gray);           //ETS text
_Canvas.AddRectangle(rectangle - 1, 256 + 6, 2, 9, Color.Red, 1, Color.Gray);       //top/right light
_Canvas.AddRectangle(rectangle - 1, 256 + 31, 2, 9, Color.Red, 1, Color.Gray);      //bottom/right light

//CyberTruck going down
_Canvas.AddRectangle(172, 200, 40, 90, Color.Silver, 1, Color.Black);
_Canvas.AddRectangle(172 + 4, 200 - 1, 7, 2, Color.Red, 0, null);               // l/f light
_Canvas.AddRectangle(172 + 29, 200 - 1, 7, 2, Color.Red, 0, null);              // r/f light
_Canvas.AddRectangle(172 + 4, 200 + 40, 32, 15, Color.Gray, 0, null);           // f window
_Canvas.AddRectangle(172 + 4, 200 + 60, 32, 15, Color.Gray, 0, null);           // r window
_Canvas.AddRectangle(172 + 4, 200 + 5, 31, 28, Color.Silver, 1, Color.Black);   //trunk
_Canvas.AddLine(172 + 4, 200 + 9, 172 + 35, 200 + 9, Color.Black, 1);           //trunk lines
_Canvas.AddLine(172 + 4, 200 + 13, 172 + 35, 200 + 13, Color.Black, 1);         //trunk lines
_Canvas.AddLine(172 + 4, 200 + 17, 172 + 35, 200 + 17, Color.Black, 1);         //trunk lines
_Canvas.AddLine(172 + 4, 200 + 21, 172 + 35, 200 + 21, Color.Black, 1);         //trunk lines
_Canvas.AddLine(172 + 4, 200 + 25, 172 + 35, 200 + 25, Color.Black, 1);         //trunk lines
_Canvas.AddLine(172 + 4, 200 + 29, 172 + 35, 200 + 29, Color.Black, 1);         //trunk lines
_Canvas.AddRectangle(172 + 4, 200 + 89, 7, 2, Color.Yellow, 0, null);           // l/r light
_Canvas.AddRectangle(172 + 29, 200 + 89, 7, 2, Color.Yellow, 0, null);          // r/r light

//CyberTruck going up
_Canvas.AddRectangle(263, 200, 40, 90, Color.Silver, 1, Color.Black);
_Canvas.AddRectangle(263 + 4, 200 - 1, 7, 2, Color.Yellow, 0, null);            // l/f light
_Canvas.AddRectangle(263 + 29, 200 - 1, 7, 2, Color.Yellow, 0, null);           // r/f light
_Canvas.AddRectangle(263 + 4, 200 + 15, 32, 15, Color.Gray, 0, null);           // f window
_Canvas.AddRectangle(263 + 4, 200 + 35, 32, 15, Color.Gray, 0, null);           // r window
_Canvas.AddRectangle(263 + 4, 200 + 57, 31, 28, Color.Silver, 1, Color.Black);  //trunk
_Canvas.AddLine(263 + 4, 200 + 61, 263 + 35, 200 + 61, Color.Black, 1);         //trunk lines
_Canvas.AddLine(263 + 4, 200 + 65, 263 + 35, 200 + 65, Color.Black, 1);         //trunk lines
_Canvas.AddLine(263 + 4, 200 + 69, 263 + 35, 200 + 69, Color.Black, 1);         //trunk lines
_Canvas.AddLine(263 + 4, 200 + 73, 263 + 35, 200 + 73, Color.Black, 1);         //trunk lines
_Canvas.AddLine(263 + 4, 200 + 77, 263 + 35, 200 + 77, Color.Black, 1);         //trunk lines
_Canvas.AddLine(263 + 4, 200 + 81, 263 + 35, 200 + 81, Color.Black, 1);         //trunk lines
_Canvas.AddRectangle(263 + 4, 200 + 89, 7, 2, Color.Red, 0, null);              // l/r light
_Canvas.AddRectangle(263 + 29, 200 + 89, 7, 2, Color.Red, 0, null);             // r/r light
*/
