﻿using GameClient.Global;
using GameClient.Renderable.GUI;
using GameClient.Renderable.GUI.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameClient.Menus
{
    class CharacterSelector
    {
        private Menu Menu;
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        private bool OnMulti { get; set; }

        public CharacterSelector(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, bool onMulti)
        {
            _spriteBatch = spriteBatch;
            _graphics = graphics;
            OnMulti = onMulti;
        }

        public Menu Create()
        {
            Menu = new Menu(RessourceProvider.MenuBackgrounds["MainMenu"]);

            Menu.Add("CharacterSelector.Text", new SimpleText(() => GameEngine.LangCenter.GetString("Text.SelectPlayer"), new Point(15, 5),
                Item.PosOnScreen.TopLeft, RessourceProvider.Fonts["Menu"],Color.White ));
            Menu.Add("CharacterSelector.Item", new ListImageButtons(new Rectangle(15, 15, 70, 45), RessourceProvider.CharacterFaces, "UglyTestTheme",
                RessourceProvider.Fonts["HUDlittle"], Color.White, Color.DarkOrange));
            Menu.Add("PlayerName.Text", new SimpleText(() => GameEngine.LangCenter.GetString("Text.TextboxPlayer"), new Point(15, 60),
                Item.PosOnScreen.TopLeft, RessourceProvider.Fonts["Menu"], Color.White));
            Menu.Add("PlayerName.Item", new Textbox("",new Rectangle(15,70,200,0), "UglyTestTheme",
                RessourceProvider.Fonts["Menu"], Color.White));
            Menu.Add("NextButton.Item", new MenuButton("Menu.Next", new Vector2(70, 70), RessourceProvider.Fonts["Menu"], Color.White,
                Color.DarkOrange, NextButton));

            return Menu;
        }

        void NextButton()
        {
            Textbox playerNameTextbox = Menu.Items["PlayerName.Item"] as Textbox;
            ListImageButtons characterSelector = Menu.Items["CharacterSelector.Item"] as ListImageButtons;

            if (playerNameTextbox.Text == "" || characterSelector.NameSelected == "")
                return;

            System.Threading.Thread.Sleep(200);

            if (OnMulti)
                GameEngine.DisplayStack.Push( new ServerSelector(_spriteBatch, _graphics, characterSelector.NameSelected,
                    playerNameTextbox.Text).Create());
            else
                GameEngine.DisplayStack.Pop();
        }
    }
}
