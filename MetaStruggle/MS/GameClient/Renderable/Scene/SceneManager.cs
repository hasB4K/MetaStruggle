﻿using System;
using System.Collections.Generic;
using System.Linq;
using DPSF;
using GameClient.Characters;
using GameClient.Global;
using GameClient.Menus;
using GameClient.Renderable.GUI;
using GameClient.Renderable.Particle;
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
        public string MapName { get; set; }
        Dictionary<string, ParticleSystem> ParticlesMap { get; set; }
        bool ActivatePause { get; set; }
        int NumberOfLives { get; set; }
        Stack<Character> StatusCharacter { get; set; }

        public SceneManager(Camera3D camera, SpriteBatch spriteBatch, string mapName = null, bool activatePause = false, int numberOfLives = 5)
        {
            Camera = camera;
            SpriteBatch = spriteBatch;
            Items = new List<I3DElement>();
            Hud = new HUD();
            NumberOfLives = numberOfLives;
            StatusCharacter = new Stack<Character>();
            if (mapName != null)
                AddMap(mapName);
            ActivatePause = activatePause;
        }

        public void AddMap(string mapName)
        {
            Skybox = new Skybox(RessourceProvider.Skyboxes[mapName]);
            Items.Add(new Model3D(this, RessourceProvider.StaticModels[mapName], new Vector3(10, 0, 0),
                          new Vector3(1f, 1f, 0.8f)));

            if (!GameEngine.ParticleEngine.Particles.ContainsKey(mapName)) return;

            MapName = mapName;
            ParticlesMap = GameEngine.ParticleEngine.Particles[mapName];
            GameEngine.ParticleEngine.AddParticles(ParticlesMap);
            InitializeParticleEngine();
        }

        public void InitializeParticleEngine()
        {
            foreach (var kvp in ParticlesMap)
                kvp.Value.ActivateParticleSystem = true;
        }

        public void AddElement(I3DElement element)
        {
            Items.Add(element);
            if (element is Character)
            {
                Hud.AddCharacter(element as Character);
                (element as Character).NumberMaxOfLives = NumberOfLives;
            }
        }

        public static SceneManager CreateScene(Vector3 cameraPosition, Vector3 cameraTarget, SpriteBatch spriteBatch, string mapName = null, bool activatePause = false)
        {
            return new SceneManager(new Camera3D(cameraPosition, cameraTarget), spriteBatch, mapName, activatePause);
        }

        public void Update(GameTime gameTime)
        {
            if (ActivatePause && GameEngine.KeyboardState.IsKeyDown(Keys.Escape))
            {
                System.Threading.Thread.Sleep(400);
                GameEngine.SoundCenter.PlayWithStatus();
                GameEngine.DisplayStack.Push(new PauseMenu().Create());
                return;
            }
            if (GameManger(gameTime))
            {
                GameEngine.SoundCenter.PlayWithStatus();
                GameEngine.DisplayStack.Push(new MenuGameOver().Create(StatusCharacter));
            }

            if (Skybox != null)
                Skybox.Update();
            foreach (var element in Items)
                element.Update(gameTime);
            GameEngine.ParticleEngine.Update(gameTime, Camera);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Skybox != null)
                Skybox.Draw(spriteBatch);
            foreach (var element in Items)
                element.Draw(gameTime, spriteBatch);
            Camera.FollowsCharacters(Camera, Items.FindAll(e => e is Character));
            Hud.DrawHUD(spriteBatch);
            GameEngine.ParticleEngine.Draw(spriteBatch, Camera);
        }

        public void ResetAll()
        {
            GameEngine.ParticleEngine.DestroyAll();
            GameEngine.SoundCenter.Stop(MapName);
        }

        bool GameManger(GameTime gameTime)
        {
            var characters = Items.OfType<Character>().ToList();
            foreach (var character in characters.Where(c => c.IsDead).Where(character => !StatusCharacter.Contains(character)
                && character.NumberMaxOfLives - character.NumberOfDeath <= 0))
                StatusCharacter.Push(character);
            if (characters.Count() == StatusCharacter.Count + 1)
            {
                foreach (var c in characters.Where(c => !StatusCharacter.Contains(c)))
                    StatusCharacter.Push(c);
                return true;
            }
            return false;
        }
    }
}
