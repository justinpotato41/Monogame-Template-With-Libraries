using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.CODE;

/// <summary>
/// Globals class. Static class that can be accessed anywhere.
/// All of them are setup in the Game1 class
/// </summary>
public static class Globals
{
    public static SpriteBatch batch;
    public static ContentManager content;
    public static GraphicsDevice graphicsDevice;
    public static GameWindow window;
}