using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.GraphicalEditor.Model
{
    public class RoomPosition : ValueObject
    {
        public double DimensionX { get; private set; }
        public double DimensionY { get; private  set; }
        public double Width { get; private set; }
        public double Height { get; private set; }

        public RoomPosition(double dimensionX, double dimensionY, double width, double height)
        {
            DimensionX = dimensionX;
            DimensionY = dimensionY;
            Width = width;
            Height = height;
            this.Validate();
        }

        private void Validate()
        {
            if (this.Width < 0 || this.Height < 0 || DimensionX < 0 || DimensionY < 0)
                throw new ArgumentException("Dimensions must be larger than 0!");
        }


        public RoomPosition AddWidth(double width) {
            double newWidth = this.Width + width;
            return new RoomPosition(this.DimensionX, this.DimensionY, newWidth, this.Height);
        }

        public RoomPosition AddHeight(double height)
        {
            double newHeight = this.Height + height;
            return new RoomPosition(this.DimensionX, this.DimensionY, this.Width, newHeight);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DimensionX;
            yield return DimensionY;
            yield return Width;
            yield return Height;
        }
    }
}
