﻿using System;
using System.Collections.Generic;
using DPSF;
using GameClient.Characters;
using GameClient.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameClient.Renderable._3D;
using Microsoft.Xna.Framework.Input;

namespace GameClient.Renderable.Scene
{
    public class SceneManager : Layout.IBasicLayout
    {
        public Camera3D Camera { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public List<I3DElement> Items { get; private set; }
        public Skybox Skybox { get; set; }
        public HUD Hud { get; set; }

        public SceneManager(Camera3D camera, SpriteBatch spriteBatch)
        {
            Camera = camera;
            SpriteBatch = spriteBatch;
            Items = new List<I3DElement>();
            Hud = new HUD();
        }

        public void AddElement(I3DElement element)
        {
            Items.Add(element);
            if(element is Character)
                Hud.AddCharacter(element as Character);                
        }

        public static SceneManager CreateScene(Vector3 cameraPosition, Vector3 cameraTarget, SpriteBatch spriteBatch)
        {
            return new SceneManager(new Camera3D(cameraPosition, cameraTarget), spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            if (GameEngine.KeyboardState.IsKeyDown(Keys.Escape))
            {
                GameEngine.SoundCenter.PlayWithStatus();
                GameEngine.DisplayStack.Pop();
                System.Threading.Thread.Sleep(200);
                return;
            }
            if (Skybox != null)
                Skybox.Update();
            foreach (var element in Items)
                element.Update(gameTime);

            //
            GameEngine.ParticleSystemManager.SetCameraPositionForAllParticleSystems(Camera.Position);
            GameEngine.ParticleSystemManager.UpdateAllParticleSystems((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GameEngine.ParticleSystemManager.SetWorldViewProjectionMatricesForAllParticleSystems(Matrix.Identity, Camera.ViewMatrix, Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), spriteBatch.GraphicsDevice.DisplayMode.AspectRatio, 1f, 100f));
            GameEngine.ParticleSystemManager.DrawAllParticleSystems();
            //
            if(Skybox != null)
                Skybox.Draw(spriteBatch);
            foreach (var element in Items)
                element.Draw(gameTime, spriteBatch);
            Camera.FollowsCharacters(Camera, Items.FindAll(e => e is Character));
            Hud.DrawHUD(spriteBatch);
            
        }
    }
}
