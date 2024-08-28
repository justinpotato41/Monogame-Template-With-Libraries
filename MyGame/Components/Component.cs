using MyGame.CODE;

namespace MyGame.Components;

public abstract class Component
{
    /// <summary>
    /// The entity the component is on.
    /// </summary>
    public Entity entity;
    public bool enabled = true;
    
    public Component()
    {
        
    }
    
    public virtual void Start()
    {
        
    }
    
    public virtual void Update(float deltaTime)
    {
        
    }
    
    public virtual void Draw()
    {
        
    }
}