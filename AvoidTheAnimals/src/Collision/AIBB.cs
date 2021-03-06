﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheOlian.Collision
{
    /**
     * AIDBC = Axis Independent Bounding Box
     */
    class AIBB : RectCollisionCalc, BoundingBox
    {
        private static boundingType TYPE = boundingType.AIDBC;

        private Rectangle boundingBox;
        private Vector2 centerPosition;
        private float radious;


        public AIBB(Vector2 _centerPosition, float _radious) : base()
        {
            base.Init(this);
            centerPosition = _centerPosition;
            radious = _radious;
            changeBoundingBox();
        }
        private void changeBoundingBox()
        {
            boundingBox = new Rectangle((int)(centerPosition.X - radious), (int)(centerPosition.Y - radious), (int)radious*2, (int)radious*2);
        }


        public void setPosition(Vector2 newValue)
        {
            centerPosition = newValue;
            changeBoundingBox();
        }
        public void alterPositionAdition(Vector2 adition)
        {
            centerPosition += adition;
            changeBoundingBox();
        }


        public Rectangle getBoundingBox()
        {
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
            return new Vector2(radious*2);
        }
    }
}
