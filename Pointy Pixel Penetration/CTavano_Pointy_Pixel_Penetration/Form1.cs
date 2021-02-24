//////////////////////////////////////////////////////////////////
///Christofer Tavano - Nov/10/2020
///Pointy Pixel Penetration
//////////////////////////////////////////////////////////////////
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTavano_Pointy_Pixel_Penetration
{
    public partial class Form1 : Form
    {
        List<ShapeBase> LShapes = new List<ShapeBase>();
        LinkedList<RegionNode> LLRegions = new LinkedList<RegionNode>();
        //Stopwatch Stopwatch = new Stopwatch();

        /// <summary>
        /// Constructor for each node that will go into our regions linked list
        /// </summary>
        private struct RegionNode {
            public Region Region;
            public Stopwatch Time;
            //public long Time;
        }

        public Form1(){
            InitializeComponent();
            timer1.Tick += Timer1_Tick;
            MouseDown += Form1_MouseDown;
            timer1.Interval = 25;
            timer1.Start();
            //Stopwatch.Start();
        }

        /// <summary>
        /// Mouse down event handler for button clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e){
            PointF temp = new PointF();

            switch (e.Button){
                case MouseButtons.Left:
                    if (ModifierKeys == Keys.Shift) {
                        //add a rock
                        Console.WriteLine("Shift left click");
                        LShapes.Add(new Rock(e.Location));
                        break;
                    }

                    //add a triangle
                    Console.WriteLine("left click");
                    LShapes.Add( new Triangle(e.Location));
                    break;

                case MouseButtons.Right:
                    if (ModifierKeys == Keys.Shift){
                        //add 1000 rocks
                        Console.WriteLine("Shift right click");
                        for (int i = 0; i < 1001; i++){
                            temp.X = ShapeBase.s_rng.Next((int)ShapeBase.TILESIZE , Width - (int)ShapeBase.TILESIZE);
                            temp.Y = ShapeBase.s_rng.Next((int)ShapeBase.TILESIZE , Height - (int)ShapeBase.TILESIZE);
                            LShapes.Add(new Rock(temp));
                        }
                        break;
                    }

                    //add 1000 triangles
                    Console.WriteLine("right click");
                    for (int i = 0; i < 1001; i++){
                        temp.X = ShapeBase.s_rng.Next((int)ShapeBase.TILESIZE, Width - (int)ShapeBase.TILESIZE);
                        temp.Y = ShapeBase.s_rng.Next((int)ShapeBase.TILESIZE, Height - (int)ShapeBase.TILESIZE);
                        LShapes.Add(new Triangle(temp));
                    }
                    break;
                default:
                    break;
            }
        }
        
        /// <summary>
        /// Tick handler for continuously iterating through our animations and physics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e){
            //Create back-buffer objects
            using (BufferedGraphicsContext bgc = new BufferedGraphicsContext()){
                //DisplayRectangle represents the virtual display of the control
                using (BufferedGraphics bg = bgc.Allocate(CreateGraphics(), DisplayRectangle)){
                   
                    bg.Graphics.Clear(Color.Black);

                    //Check to see how long our hits have been active, deactivate them if the have been up for too long
                    while (LLRegions.First != null && LLRegions.First.Value.Time.ElapsedMilliseconds > 5000)
                        LLRegions.RemoveFirst();

                    //Iterate through our regions and color them
                    foreach (RegionNode rnode in LLRegions) {
                        bg.Graphics.FillRegion(new SolidBrush(Color.DarkBlue), rnode.Region);
                    }

                    //Iterate through our shapes and color them appropriately 
                    foreach (ShapeBase shape in LShapes){
                        shape.Tick(Size);

                        if (shape is Triangle)
                            shape.Render(bg.Graphics, Color.Green);
                        else
                            shape.Render(bg.Graphics, Color.DarkGray);
                    }

                    //Test each shape location for potential hit detection
                    foreach (ShapeBase shape in LShapes){
                        foreach (ShapeBase testShape in LShapes){
                            if (!shape.Equals(testShape) && !shape.IsMarkedForDeath && !testShape.IsMarkedForDeath){
                                if (shape.GetDistance(testShape) < 3 * ShapeBase.TILESIZE){
                                    //SHEILDS UP!
                                    shape.SheildsUp(bg.Graphics);
                                    testShape.SheildsUp(bg.Graphics);
                                    
                                    //Create regions of each shape
                                    Region RegA = new Region(shape.GetPath());
                                    Region RegB = new Region(testShape.GetPath());
                                    Region RegT = RegA.Clone();
                                    
                                    //Check if the regions intersect
                                    RegT.Intersect(RegB);

                                    //If there regions to intersect, then mark the shapes for death 
                                    //and add them to our LL
                                    if (!RegT.IsEmpty(bg.Graphics)) {
                                        shape.IsMarkedForDeath = true;
                                        testShape.IsMarkedForDeath = true;

                                        RegionNode temp = new RegionNode {
                                            Region = RegT,
                                            //Time = Stopwatch.ElapsedMilliseconds + 5000
                                            Time = Stopwatch.StartNew()
                                        };

                                        LLRegions.AddLast(temp);
                                    }
                                }
                            }
                        }
                    }

                    //Remove all the shapes that were marked for death
                    LShapes.RemoveAll(x => x.IsMarkedForDeath == true);

                    //Anti-aliasing
                    bg.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    //Flip back-buffer to front buffer
                    bg.Render();
                }
            }
        }
    }
}
