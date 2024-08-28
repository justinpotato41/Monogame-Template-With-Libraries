using System;
using System.Collections.Generic;
using System.Linq;
using Humper;
using Humper.Responses;
using Microsoft.Xna.Framework;
using MyGame.Components;

namespace MyGame.CODE;

public class Entity
{
    public string Name;
    
    /// <summary>
    /// A Box-Entity dictionary to access the entity through its box
    /// </summary>
    public static Dictionary<IBox, Entity> BoxEntityMap = new Dictionary<IBox, Entity>();
    
    public List<Component> components;
    public Scene scene;
    public bool isActive = true;
    
    public Vector2 position;
    public float rotation = 0;
    public Vector2 scale = new Vector2(1, 1);
    
    public Vector2 velocity = Vector2.Zero;
    public Vector2 intendedPosition = Vector2.Zero;
    
    public IBox box;
    public float startBoxX;
    public float startBoxY;
    public IMovement result;
    public ICollision col;
    
    public Entity(string Name, float x, float y)
    {
        this.Name = Name;
        
        position = new Vector2(x, y);
        
        components = new();
    }
    
    /// <summary>
    /// Generates the AABB box for the entity (the box can't be rotated)
    /// </summary>
    /// <param name="world">The world that's gonna generate the box</param>
    /// <param name="x">The X position of the box. It starts from the entity position</param>
    /// <param name="y">The Y position of the box. It starts from the entity position</param>
    /// <param name="w">Width of the box in pixels</param>
    /// <param name="h">Height of the box in pixels</param>
    /// <param name="tag">The tag of the box. You can access the "Tags." for that</param>
    public void GenerateBoxCollider(World world, float x, float y, float w, float h, Enum tag)
    {
        box = world.Create(position.X + x, position.Y + y, w, h);
        box.AddTags(tag);
        startBoxX = x; startBoxY = y;
        
        // Store the mapping between the box and the entity
        BoxEntityMap[box] = this;
    }
    
    public virtual void Start()
    {
        foreach(var component in components)
        {
            if(component.enabled)
                component.Start();
        }
    }
    
    public virtual void Update(float deltaTime)
    {
        // First, it is calculated the intended new position
        intendedPosition = position + velocity * deltaTime;
        
        if(box != null)
        {
            // Now, the box is moved and handles collisions before finalizing the position
            result = box.Move(intendedPosition.X + startBoxX, intendedPosition.Y + startBoxY, (collision) =>
            {
                // if you give a the "Area" tag then other things will go through it but it can still detect collisions
                if(collision.Other.HasTag(Tags.Area) || collision.Box.HasTag(Tags.Area))
                {
                    //Console.WriteLine("entered an area");
                    return CollisionResponses.Cross;
                }
                // Adjust the velocity based on the collision
                if (collision.Hit.Normal.X != 0)
                    velocity.X = 0; // Stop horizontal movement
                if (collision.Hit.Normal.Y != 0)
                    velocity.Y = 0; // Stop vertical movement
                
                col = collision; // stores the collision in another variable to be used in other scripts

                return CollisionResponses.Slide;
            });
            
            // Finalize the position based on collision resolution
            position = new Vector2(box.Bounds.X - startBoxX, box.Bounds.Y - startBoxY);
        }
        else
        {
            // If no collision box, just update the position directly
            position = intendedPosition;
        }
        
        foreach(var component in components)
        {
            if(component.enabled)
                component.Update(deltaTime);
        }
    }
    
    public virtual void Draw()
    {
        foreach(var component in components)
        {
            if(component.enabled)
                component.Draw();
        }
    }
    
    public void AddComponent(Component component)
    {
        component.entity = this;
        components.Add(component);
    }
    
    public T GetComponent<T>() where T : Component
    {
        return components.OfType<T>().FirstOrDefault();
    }
    
    public Entity FindEntityByName(string Name)
    {
        return scene.entities.Find(e => e.Name == Name);
    }
    
    public void Destroy(Entity entity)
    {
        entity.scene.world.Remove(entity.box);
        scene.entities.Remove(entity);
    }
}