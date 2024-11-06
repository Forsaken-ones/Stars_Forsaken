using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera
{
    public Vector2 Position { get; set; }
    public float Zoom { get; set; }
    public float Rotation { get; set; }

    public Camera()
    {
        this.Zoom = 4.0f; // 4.0f
        this.Rotation = 0.0f; //0.0f
        this.Position = Vector2.Zero; //Vector2.Zero
    }

    public Camera(Vector2 position, float zoom, float rotation)
    {
        this.Zoom = zoom;
        this.Rotation = rotation;
        this.Position = position;
    }

    public Matrix GetTransformation(GraphicsDevice graphicsDevice)
    {
        return Matrix.CreateTranslation(new Vector3(-Position, 0)) *
               Matrix.CreateRotationZ(Rotation) *
               Matrix.CreateScale(Zoom, Zoom, 1) *
               Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
    }
}