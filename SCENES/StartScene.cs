/*
    EXAMPLE START SCENE
    
    This is an example start scene for a top down game with player movement and sprite renderer component.
    
    
    Start runs once at the start of the game
    
    Update runs once / frame
    
    Draw runs once / frame but its for drawing only (it can skip some frames).
    
    
    
    You can change scenes this way here: "sceneManager.ChangeScene(typeof(GameScene));"
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using MyGame.CODE;
using MyGame.Components;

namespace MyGame.SCENES;

public class StartScene : Scene
{
    // DECLARE ENTITIES
    public Entity player;
    public Entity enemy;
    
    public override void Start()
    {
        // INITIALIZE ENTITIES ------------------------------------------------------------------------------------------------------
        
        player = new Entity("player", 0, 0);
        player.AddComponent(new SpriteRenderer("art/test_sprite", new Rectangle(0, 0, 22, 31), 0));
        player.AddComponent(new PlayerMovement(100));
        player.GenerateBoxCollider(world, 6, 22, 11, 10, Tags.Player);
        AddEntity(player);
        
        enemy = new Entity("enemy", 100, 100);
        enemy.AddComponent(new SpriteRenderer("art/test_sprite", new Rectangle(0, 0, 22, 31), 0));
        enemy.GenerateBoxCollider(world, 6, 22, 100, 100, Tags.Enemy);
        AddEntity(enemy);
        
        // INITIALIZE ENTITIES ------------------------------------------------------------------------------------------------------
        
        
        
        // setup for the camera. First the start position and then the zoom on x and zoom on y
        SetupCamera(player, 3, 3);
        
        base.Start();
    }

    public override void Update(float deltaTime)
    {
        // Makes the camera follow an entity. the 0.08f is the smoothness value, it can be any value from 0 to 1. 
        // I recommend a bit of smoothness :))
        CameraFollow(player, true, 0.08f);
        
        if(KeyboardExtended.GetState().WasKeyPressed(Keys.Enter))
        {
            sceneManager.ChangeScene(typeof(GameScene)); // You can change scenes this way :D
        }
        
        base.Update(deltaTime);
    }

    public override void Draw()
    {
        // DRAW HERE WHATEVER YOU NEED
        
        base.Draw();
    }
}