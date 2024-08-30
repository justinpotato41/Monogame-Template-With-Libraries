using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.CODE;

namespace MyGame.Components;

public class SpriteRenderer : Component
{
    public Texture2D texture;
    public Rectangle region = Rectangle.Empty;
    public float layerDepth;
    
    public Color color;
    
    public SpriteRenderer(string texturepath, float layerDepth)
    {
        texture = Globals.content.Load<Texture2D>(texturepath);
        this.region = Rectangle.Empty;
        this.layerDepth = layerDepth;
        color = Color.White;
    }
    
    public SpriteRenderer(string texturepath, Rectangle region, float layerDepth)
    {
        texture = Globals.content.Load<Texture2D>(texturepath);
        if(region != Rectangle.Empty) this.region = region;
        this.layerDepth = layerDepth;
        color = Color.White;
    }
    
    public override void Draw()
    {
        if(region != Rectangle.Empty)
            Globals.batch.Draw(texture, entity.position, region, color, entity.rotation, Vector2.Zero, entity.scale, SpriteEffects.None, layerDepth);
        else
            Globals.batch.Draw(texture, entity.position, null, color, entity.rotation, Vector2.Zero, entity.scale, SpriteEffects.None, layerDepth);
        
        base.Draw();
    }
}