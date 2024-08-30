using Humper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using MyGame.CODE;
using MyGame.SCENES;

namespace MyGame;

public class Game1 : Game
{
    // SETUP
    private GraphicsDeviceManager _graphics;
    private Texture2D pixelTexture;
    private SpriteFont font;
    
    // SCENE MANAGER
    SceneManager sceneManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        // Windows title
        Window.Title = "MyGame";
        
        // Change the "GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/Height" with the window width/height
        _graphics.PreferredBackBufferWidth = 1024;
        _graphics.PreferredBackBufferHeight = 576;
        
        Window.AllowUserResizing = true;
        
        // Is fullscreen or nah???
        _graphics.IsFullScreen = false;
    }

    protected override void Initialize()
    {
        Globals.content = Content;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        #region SETUP
        Globals.window = Window;
        Globals.batch = new SpriteBatch(GraphicsDevice);
        Globals.graphicsDevice = GraphicsDevice;
        pixelTexture = Content.Load<Texture2D>("ART/pixel");
        font = Content.Load<SpriteFont>("ART/font");
        sceneManager = new SceneManager();
        #endregion
        
        
        
        
        
        
        
        
        
        
        // ADD YOUR SCENES FOR THEM TO BE USED
        sceneManager.AddScene(new StartScene());
        sceneManager.AddScene(new GameScene());
        
        
        
        
        
        
        
        
        
        
        sceneManager.GetCurrentScene().Start();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        KeyboardExtended.Update();
        
        // UPDATE
        sceneManager.GetCurrentScene().Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        sceneManager.GetCurrentScene().camera.SetViewport();
        Globals.batch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: sceneManager.GetCurrentScene().camera.View, sortMode: SpriteSortMode.FrontToBack);
        
        sceneManager.GetCurrentScene().Draw();
        // Comment the NEXT line to disable drawing the collision boxes:
        sceneManager.GetCurrentScene().world.DrawDebug((int)sceneManager.GetCurrentScene().world.Bounds.X, (int)sceneManager.GetCurrentScene().world.Bounds.Y, (int)sceneManager.GetCurrentScene().world.Bounds.Width, (int)sceneManager.GetCurrentScene().world.Bounds.Height, DrawCell, DrawBox, DrawString);
        
        Globals.batch.End();
        sceneManager.GetCurrentScene().camera.ResetViewport();
        
        base.Draw(gameTime);
    }
    
    #region DebugDraw functions
    // private void DrawCell(int x, int y, int w, int h, float alpha)
    // {
    //     spriteBatch.Draw(pixelTexture, pixelTexture.Bounds, new Rectangle(x,y,w,h), new Color(Color.White,alpha));
    // }
    private void DrawCell(int x, int y, int w, int h, float alpha)
    {
        Globals.batch.DrawRectangle(new RectangleF(x, y, w, h), new Color(Color.White, alpha));
    }

    // private void DrawBox(IBox box)
    // {
    //     spriteBatch.Draw(pixelTexture, pixelTexture.Bounds, box.Bounds.GetBoundingRectangle(), Color.Green);
    // }
    private void DrawBox(IBox box)
    {
        Globals.batch.DrawRectangle(new RectangleF(box.X, box.Y, box.Width, box.Height), Color.Green);
    }

    // private void DrawString(string message, int x, int y, float alpha)
    // {
    //     var size = this.font.MeasureString(message);
    //     spriteBatch.DrawString(this.font, message, new Vector2( x - size.X / 2, y - size.Y / 2), new Color(Color.White, alpha));
    // }
    private void DrawString(string message, int x, int y, float alpha)
    {
        var size = this.font.MeasureString(message);
        Globals.batch.DrawString(this.font, message, new Vector2( x - size.X / 2, y - size.Y / 2), new Color(Color.White, alpha));
    }
    #endregion
}
