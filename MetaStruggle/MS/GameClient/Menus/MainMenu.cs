﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameClient.Global;
using GameClient.Language;
using GameClient.Renderable.GUI;
using GameClient.Renderable.GUI.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameClient.Menus
{
    class MainMenu
    {
        private readonly SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;

        public MainMenu(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            _spriteBatch = spriteBatch;
            _graphics = graphics;
        }

        public Menu CreateMainMenu()
        {
            //Menu1 test = new Menu1(RessourceProvider.MenuBackgrounds["MainMenu"]);
            //test.Add(new Textbox("test", new Rectangle(0, 0, 400, 50), RessourceProvider.Buttons["Textbox"], RessourceProvider.Fonts["HUD"], Color.White));
            //test.Add(new ButtonSelector("test", new Rectangle(0, 0, 400, 50), RessourceProvider.Buttons["TextboxMulti"], RessourceProvider.Fonts["HUD"], Color.White, Color.DarkRed));
            //test.Add(new Button(new Rectangle(50, 10, 50, 50),"test", RessourceProvider.Fonts["HUD"], Color.White, Color.Black, () => GameEngine.DisplayStack.Pop()));
            var buttons = new List<MenuButton1>
                {
                    new MenuButton1("play", Play),
                    new MenuButton1("option", () => GameEngine.DisplayStack.Push(new SettingsMenu(_spriteBatch,_graphics).MenuSettings())),
                    new MenuButton1("Multi", () => GameEngine.DisplayStack.Push(new CharacterSelector(_spriteBatch,_graphics, true).Create())),
                    new MenuButton1("quit", () => Environment.Exit(0)),
                    new MenuButton1("Test", () => GameEngine.DisplayStack.Push(new TestMenu(_spriteBatch,_graphics, true).Create())),
                };

            var main = new Menu("MainMenu", buttons, RessourceProvider.MenuBackgrounds["MainMenu"],
                                new Point((int)((GameEngine.Config.ResolutionWidth / 2) - RessourceProvider.Fonts["Menu"].MeasureString(buttons[0].DisplayedName).X / 2),
                                          (int)(GameEngine.Config.ResolutionHeight) / 2));

            return main;
        }

         public void Play()
        {
            if (GameEngine.SceneManager == null)
                GameEngine.SceneManager = Renderable.Environments.Environment1.GetScene(_spriteBatch);

            GameEngine.SoundCenter.PlayWithStatus("music1");
            GameEngine.DisplayStack.Push(GameEngine.SceneManager);
        }
    }
}
