using System;
using OpenTK;

namespace Graphics {
	public interface IWithin {
		bool PointWithinBounds(Vector2d point);
		bool PointWithinContent(Vector2d point);
		bool BoundsCollide(BoundingBox box);
		bool BoundsCollide(IBounded shape);
		bool ContentCollides(IPrimitiveShape shape);
		bool ContentCollides(GLImage image,byte alphaThreshold);
		bool ContentCollides(GLImage image);
		bool ContentCollides(BoundingBox box);
	}
}

