using Microsoft.Xna.Framework;

namespace TheOlian.Collision
{
    enum boundingType {AABB, AIDBC}
    interface BoundingBox
    {
        boundingType getBoundinType();

        void setPosition(Vector2 newValue);
        void alterPositionAdition(Vector2 adition);
        Rectangle getBoundingBox();
        Vector2 getCenter();
        Vector2 getSize();

        bool Intersects(BoundingBox target);
        bool isLeft(BoundingBox target);
        bool isRight(BoundingBox target);
        bool isAbove(BoundingBox target);
        bool isBelow(BoundingBox target);
        double degreeTo(BoundingBox target);
        double radiantTo(BoundingBox target);

    }
}
