/*
    This is an example of player movement script. It is a component that can be attached to any entity.
    You can make more of this and call them whatever you want.
    
    The constructor is only for being able to change the values when adding this component.
    
    
    
    You can change scenes this way here: "entity.scene.sceneManager.ChangeScene(typeof(GameScene));"
    (notice its accessing the sceneManager from the component, then through the entity)
*/

using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using MyGame.CODE;

namespace MyGame.Components;

public class PlayerMovement : Component
{
    public float speed;
    public float acc;
    Vector2 dir = Vector2.Zero;
    
    // EXPORT VARIABLES (optional)
    public PlayerMovement(float speed = 50, float acc = 0.2f)
    {
        this.speed = speed;
        this.acc = acc;
    }
    // EXPORT VARIABLES
    
    public override void Update(float deltaTime)
    {
        // using KeyboardExtended to check the direction you're pressing on the arrow keys
        dir = Vector2.Zero;
        if(KeyboardExtended.GetState().IsKeyDown(Keys.Right))
        {
            dir.X = 1;
        }
        else if(KeyboardExtended.GetState().IsKeyDown(Keys.Left))
        {
            dir.X = -1;
        }
        if(KeyboardExtended.GetState().IsKeyDown(Keys.Down))
        {
        dir.Y = 1;
        }
        else if(KeyboardExtended.GetState().IsKeyDown(Keys.Up))
        {
            dir.Y = -1;
        }
        
        Move();
        
        // EXAMPLE OF DETECTING COLLISION
        if(entity.result.Hits.Any((c) => c.Box.HasTag(Tags.Enemy)))
        {
            // destroys the entity the "Other" box is on. This is done with the help pf the BoxEntityMap dictionary
            entity.Destroy(Entity.BoxEntityMap[entity.col.Other]);
        }
        
        if(entity.result.Hits.Any((c) => c.Box.HasTag(Tags.Area)))
        {
            entity.scene.camera.Z = MathHelper.Lerp(entity.scene.camera.Z, 3.5f, 0.1f);
        }
        else
        {
            entity.scene.camera.Z = MathHelper.Lerp(entity.scene.camera.Z, 1f, 0.1f);
        }
        
        base.Update(deltaTime);
    }
    
    // movement function for top down games with acceleration
    public void Move()
    {
        if(dir != Vector2.Zero)
        {
            entity.velocity = Vector2.Lerp(entity.velocity, speed * dir, 0.2f);
        }
        else
        {
            entity.velocity = Vector2.Lerp(entity.velocity, Vector2.Zero, 0.2f);
        }
    }
}