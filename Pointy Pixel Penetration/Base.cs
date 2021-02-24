using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTavano_Pointy_Pixel_Penetration
{
    public abstract class Base{
        protected int m_fRot;
        protected int m_fRotInc;
        protected double m_fxSpeed;
        protected double m_fySpeed;
        protected static Random Rng = new Random();
        protected static bool IsMarkedForDeath { get; set; }
        protected static PointF Pos { get; set; }

        public Base(PointF pointF) {
            m_fRot = 0;
            m_fRotInc = Rng.Next();
            m_fxSpeed = Rng.NextDouble();
            m_fySpeed = Rng.NextDouble();
            Pos = pointF;
        }

        //Use NVI pattern to create a public ShowCar() and protected abstract VShowCar()
        public void Render() => VRender();
        protected abstract void VRender();
    }
}
