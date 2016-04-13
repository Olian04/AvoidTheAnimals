using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace TheOlian.Collision
{
    /**
     * AABB = Axis Alined Bounding Box
     * (Rectangle hitbox)
     */
    class AABB : RectCollisionCalc, BoundingBox
    {
        private static boundingType TYPE = boundingType.AABB;

        private Rectangle boundingBox;
        private Vector2 centerPosition;
        private Vector2 size;


        public AABB(Vector2 _centerPosition, Vector2 _size) : base() {
            base.Init(this);
            centerPosition = _centerPosition;
            size = _size;
            changeBoundingBox();
        }
        public AABB(Rectangle rect) {
            base.Init(this);
            centerPosition = rect.Center.ToVector2();
            size = rect.Size.ToVector2();
            changeBoundingBox();
        }
        private void changeBoundingBox() {
            boundingBox = new Rectangle(new Point((int)(centerPosition.X - (size.X / 2)), (int)(centerPosition.Y - (size.Y / 2)) ), size.ToPoint());
        }


        public void setPosition(Vector2 newValue) {
            centerPosition = newValue;
            changeBoundingBox();
        }
        public void alterPositionAdition(Vector2 adition) {
            centerPosition += adition;
            changeBoundingBox();
        }

        public Rectangle getBoundingBox() {
            return boundingBox;
        }
        public boundingType getBoundinType()
        {
            return TYPE;
        }
        public Vector2 getCenter()
        {
            return centerPosition;
        }
        public Vector2 getSize()
        {
            return size;
        }
    }
}
