using System.Collections.Generic;
using System.Linq;
using Apos.Camera;
using Humper;
using Microsoft.Xna.Framework;
using MyGame.Components;

namespace MyGame.CODE;

public class Scene
{
    public List<Entity> entities;
    List<Entity> sortedEntities;
    public bool IsYsort = false;
    public SceneManager sceneManager;
    
    /// <summary>
    /// The Physics world that handles the boxes collisions
    /// </summary>
    public World world;
    
    public Camera camera;
    
    /// <summary>
    /// Makes a new Scene.
    /// </summary>
    /// <param name="IsYsort">If true, draws the world by the Y position of entities</param>
    /// <param name="worldCellsWidth">Width of cells in the world</param>
    /// <param name="worldCellsHeight">Height of cells in the world</param>
    /// <param name="cellSize">The cell size. By default 64 for faster collision calculation, 
    /// but you can also change this parameter at instanciation.</param>
    public Scene(bool IsYsort = false, float worldCellsWidth = 500, float worldCellsHeight = 300, float cellSize = 64)
    {
        entities = new();
        sortedEntities = new();
        this.IsYsort = IsYsort;
        
        world = new World(worldCellsWidth, worldCellsHeight, cellSize);
        
        IVirtualViewport defaultViewport = new DefaultViewport(Globals.graphicsDevice, Globals.window);
        camera = new Camera(defaultViewport);
    }
    
    public virtual void Start()
    {
        foreach(var entity in entities)
        {
            if(entity.isActive)
                entity.Start();
        }
    }
    
    public virtual void Update(float deltaTime)
    {
        if(!IsYsort)
        {
            sortedEntities = entities;
        }
        
        for(int i = 0; i<entities.Count; i++)
        {
            if(entities[i].isActive)
                entities[i].Update(deltaTime);
        }
    }
    
    public virtual void Draw()
    {
        if(IsYsort)
        {
            sortedEntities = entities.OrderBy(e => e.position.Y).ToList();
        }
        
        foreach(var entity in sortedEntities)
        {
            if(entity.isActive)
                entity.Draw();
        }
    }
    
    public void AddEntity(Entity entity)
    {
        entity.scene = this;
        entities.Add(entity);
    }
    
    public void Destroy(Entity entity)
    {
        entities.Remove(entity);
    }
    
    /// <summary>
    /// Setup for the camera with a start target and the scale
    /// </summary>
    /// <param name="startTarget"></param>
    /// <param name="xScale"></param>
    /// <param name="yScale"></param>
    public void SetupCamera(Entity startTarget, float xScale, float yScale)
    {
        camera.XY = startTarget.position + new Vector2(startTarget.GetComponent<SpriteRenderer>().region.Width/2, startTarget.GetComponent<SpriteRenderer>().region.Height/2);
        camera.Scale = new Vector2(xScale, yScale);
    }
    
    /// <summary>
    /// Makes the camera follow a target. Should be put in the "Update" loop of a scene
    /// </summary>
    /// <param name="target">The target</param>
    /// <param name="IsSmooth">activates/deactivates smoothnes. Lerps the camera position by the smootSpeed</param>
    /// <param name="smootSpeed">lerping speed from 0 to 1</param>
    public void CameraFollow(Entity target, bool IsSmooth = false, float smootSpeed = 0.2f)
    {
        if(IsSmooth)
            camera.XY = Vector2.Lerp(camera.XY, target.position + new Vector2(target.GetComponent<SpriteRenderer>().region.Width/2, target.GetComponent<SpriteRenderer>().region.Height/2), smootSpeed);
        else
            camera.XY = target.position + new Vector2(target.GetComponent<SpriteRenderer>().region.Width/2, target.GetComponent<SpriteRenderer>().region.Height/2);
    }
}