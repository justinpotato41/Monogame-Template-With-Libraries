using System;
using System.Collections.Generic;

namespace MyGame.CODE;

public class SceneManager
{
    public List<Scene> scenes;
    
    public SceneManager()
    {
        scenes = new();
    }
    
    /// <summary>
    /// Gets the first scene. The current scene will always be the first scene in a list.
    /// </summary>
    /// <returns>The first scene of a list</returns>
    public Scene GetCurrentScene()
    {
        return scenes[0];
    }
    
    /// <summary>
    /// Changes the scene. It switches the first scene (always the one that runs) with the scene you specified
    /// </summary>
    /// <param name="sceneType"> Use typeof() for this</param>
    public void ChangeScene(Type sceneType)
    {
        for(int i = 0; i<scenes.Count; i++)
        {
            if(scenes[i].GetType() == sceneType)
            {
                scenes[i].Start();
                (scenes[i], scenes[0]) = (scenes[0], scenes[i]);
            }
        }
    }
    
    public void AddScene(Scene scene)
    {
        scene.sceneManager = this;
        scenes.Add(scene);
    }
    
    public void RemoveScene(Scene scene)
    {
        scenes.Remove(scene);
    }
}