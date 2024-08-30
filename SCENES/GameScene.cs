/*
    THIS IS THE GAME SCENE
    
    its the next scene after the START SCENE.
    
    
    
    You can change scenes this way here: "sceneManager.ChangeScene(typeof(GameScene));"
*/

using Microsoft.Xna.Framework;
using MyGame.CODE;
using MyGame.Components;

namespace MyGame.SCENES;

public class GameScene : Scene
{
    // DECLARE ENTITIES and others
    public Entity player;
    public Entity enemy;
    public Entity zone;
    
    public override void Start()
    {
        // INITIALIZE ENTITIES ------------------------------------------------------------------------------------------------------
        
        player = new Entity("player", 0, 0);
        player.AddComponent(new SpriteRenderer("ART/test_sprite", new Rectangle(0, 0, 22, 31), 0));
        player.AddComponent(new PlayerMovement(100));
        player.GenerateBoxCollider(world, 6, 22, 11, 10, Tags.Player);
        AddEntity(player);
        
        enemy = new Entity("enemy", 100, 100);
        enemy.AddComponent(new SpriteRenderer("ART/test_sprite", new Rectangle(0, 0, 22, 31), 0));
        enemy.GenerateBoxCollider(world, 6, 22, 11, 10, Tags.Enemy);
        AddEntity(enemy);
        
        zone = new Entity("zone", 150, 100);
        zone.GenerateBoxCollider(world, 0, 0, 80, 80, Tags.Area);
        AddEntity(zone);
        
        // INITIALIZE ENTITIES ------------------------------------------------------------------------------------------------------
        
        // setup for the camera. First the start position and then the zoom on x and zoom on y
        SetupCamera(player, 3, 3);
        
        base.Start();
    }

    public override void Update(float deltaTime)
    {
        // Makes the camera follow an entity. Here, smoothness value is 0.2f by default, it can be any value from 0 to 1
        CameraFollow(player, true);
        
        base.Update(deltaTime);
    }

    public override void Draw()
    {
        // DRAW HERE WHATEVER YOU NEED
        
        base.Draw();
    }
}