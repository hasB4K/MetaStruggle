﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using XNAnimation;

namespace GameClient.Global
{
    public static class RessourceProvider
    {
        public static Dictionary<string, Texture2D> CharacterFaces = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Texture2D> MenuBackgrounds = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Texture2D> Skyboxes = new Dictionary<string, Texture2D>();

        public static Dictionary<string, SkinnedModel> AnimatedModels = new Dictionary<string, SkinnedModel>();
        public static Dictionary<string, Model> StaticModels = new Dictionary<string, Model>();

        public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

        public static Dictionary<string, Cursor> Cursors = new Dictionary<string, Cursor>();
        public static Dictionary<string, Dictionary<string, Texture2D>> Themes = new Dictionary<string, Dictionary<string, Texture2D>>();
        public static Dictionary<string, Video> Videos = new Dictionary<string, Video>();


        public static void Fill(ContentManager content)
        {
            LoadCharacterFaces(content);
            LoadMenuBackgrounds(content);
            LoadSkyboxes(content);

            LoadAnimatedModels(content);
            LoadStaticModels(content);

            LoadFonts(content);

            //LoadCursors(content);
            LoadThemes(content);
            LoadVideos(content);
        }

        static void LoadCharacterFaces(ContentManager content)
        {
            CharacterFaces.Add("Zeus", content.Load<Texture2D>("CharacterFaces\\Zeus"));
            CharacterFaces.Add("Spiderman", content.Load<Texture2D>("CharacterFaces\\Spiderman"));
            CharacterFaces.Add("Alex", content.Load<Texture2D>("CharacterFaces\\Alex"));
            CharacterFaces.Add("Ares", content.Load<Texture2D>("CharacterFaces\\Ares"));
            CharacterFaces.Add("Ironman", content.Load<Texture2D>("CharacterFaces\\Ironman"));
        }

        static void LoadMenuBackgrounds(ContentManager content)
        {
            MenuBackgrounds.Add("MainMenu", content.Load<Texture2D>("MenuBackgrounds\\MainMenu"));
        }

        static void LoadSkyboxes(ContentManager content)
        {
            Skyboxes.Add("Ocean", content.Load<Texture2D>("Skyboxes\\Ocean"));
        }

        static void LoadAnimatedModels(ContentManager content)
        {
            AnimatedModels.Add("Zeus", GetZeus(content));
            AnimatedModels.Add("Dwarf", GetDwarf(content));
            AnimatedModels.Add("Spiderman", GetSpiderman(content));
            AnimatedModels.Add("Alex", GetAlex(content));
            AnimatedModels.Add("Ares", GetAres(content));
            AnimatedModels.Add("Ironman", GetIronman(content));
        }

        static SkinnedModel GetZeus(ContentManager content)
        {
            var sm = content.Load<SkinnedModel>("AnimatedModels\\Zeus\\Model");
            var body = content.Load<Texture2D>("AnimatedModels\\Zeus\\textura cor-2");
            var thunder = content.Load<Texture2D>("AnimatedModels\\Zeus\\Cube_001");

            foreach (ModelMesh mesh in sm.Model.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    effect.Texture = body;

                    if (mesh.Name == "Cube_001")
                        effect.Texture = thunder;

                    effect.EnableDefaultLighting();

                    effect.SpecularColor = new Vector3(0.25f);
                    effect.SpecularPower = 16;
                }
            }

            return sm;
        }

        static SkinnedModel GetIronman(ContentManager content)
        {
            var sm = content.Load<SkinnedModel>("AnimatedModels\\Ironman\\Model");
            var body = content.Load<Texture2D>("AnimatedModels\\Ironman\\texture");

            foreach (ModelMesh mesh in sm.Model.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    effect.Texture = body;

                    effect.EnableDefaultLighting();

                    effect.SpecularColor = new Vector3(0.25f);
                    effect.SpecularPower = 16;
                }
            }

            return sm;
        }

        static SkinnedModel GetDwarf(ContentManager content)
        {
            var sm = content.Load<SkinnedModel>("AnimatedModels\\Dwarf\\Model");
            var body = content.Load<Texture2D>("AnimatedModels\\Dwarf\\Body");
            var axe = content.Load<Texture2D>("AnimatedModels\\Dwarf\\axe");

            foreach (ModelMesh mesh in sm.Model.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    effect.Texture = body;

                    if (mesh.Name == "axe")
                        effect.Texture = axe;

                    effect.EnableDefaultLighting();

                    effect.SpecularColor = new Vector3(0.25f);
                    effect.SpecularPower = 16;
                }
            }

            return sm;
        }
        static SkinnedModel GetSpiderman(ContentManager content)
        {
            var sm = content.Load<SkinnedModel>("AnimatedModels\\Spiderman\\Spiderman");
            var body = content.Load<Texture2D>("AnimatedModels\\Spiderman\\Spidermantex");

            foreach (ModelMesh mesh in sm.Model.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    effect.Texture = body;
                    effect.EnableDefaultLighting();

                    effect.SpecularColor = new Vector3(0.25f);
                    effect.SpecularPower = 16;
                }
            }

            return sm;
        }
        static SkinnedModel GetAlex(ContentManager content)
        {
            var sm = content.Load<SkinnedModel>("AnimatedModels\\Alex\\AlexMercer");
            var body = content.Load<Texture2D>("AnimatedModels\\Alex\\nis_alex_body_dm");
            var head = content.Load<Texture2D>("AnimatedModels\\Alex\\alex_head_shot_dm");
            var hand = content.Load<Texture2D>("AnimatedModels\\Alex\\AlexHand");

            foreach (ModelMesh mesh in sm.Model.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    if (mesh.Name == "model.005")
                        effect.Texture = body;
                    if (mesh.Name == "model.006")
                        effect.Texture = head;
                    if (mesh.Name == "model.004")
                        effect.Texture = hand;
                    effect.EnableDefaultLighting();

                    effect.SpecularColor = new Vector3(0.25f);
                    effect.SpecularPower = 30;
                }
            }

            return sm;
        }
        static SkinnedModel GetAres(ContentManager content)
        {
            var sm = content.Load<SkinnedModel>("AnimatedModels\\Ares\\Model");
            var body = content.Load<Texture2D>("AnimatedModels\\Ares\\textura cor-2");
            var casque = content.Load<Texture2D>("AnimatedModels\\Ares\\UVcasque");

            foreach (ModelMesh mesh in sm.Model.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    if (mesh.Name == "prometheu")
                        effect.Texture = body;

                    if (mesh.Name == "Hebras")
                        effect.Texture = casque;

                    effect.EnableDefaultLighting();

                    effect.SpecularColor = new Vector3(0.25f);
                    effect.SpecularPower = 16;
                }
            }
            return sm;
        }

        static void LoadStaticModels(ContentManager content)
        {
            StaticModels.Add("MapDesert", content.Load<Model>("StaticModels\\MapDesert"));
            StaticModels.Add("MapTardis", content.Load<Model>("StaticModels\\tardis map"));
            StaticModels.Add("MapMinecraft", content.Load<Model>("StaticModels\\tree map"));
        }

        static void LoadFonts(ContentManager content)
        {
            Fonts.Add("Menu", content.Load<SpriteFont>("Fonts\\Menu"));
            Fonts.Add("HUD", content.Load<SpriteFont>("Fonts\\HUD"));
            Fonts.Add("HUDlittle", content.Load<SpriteFont>("Fonts\\HUDlittle"));
        }

        static void LoadCursors(ContentManager content)
        {
            
        }

        static void LoadThemes(ContentManager content)
        {
            var testTheme = new Dictionary<string, Texture2D>
                {
                    {"ListLine.Top", content.Load<Texture2D>("Themes\\UglyTestTheme\\ListLine\\Top")},
                    {"ListLine.Down", content.Load<Texture2D>("Themes\\UglyTestTheme\\ListLine\\Down")},
                    {"ListLine.Separator", content.Load<Texture2D>("Themes\\UglyTestTheme\\ListLine\\Separator")},
                    {"ListLine.LeftSide", content.Load<Texture2D>("Themes\\UglyTestTheme\\ListLine\\LeftSide")},
                    {"ListLine.RightSide", content.Load<Texture2D>("Themes\\UglyTestTheme\\ListLine\\RightSide")},
                    {"ListLine.Background", content.Load<Texture2D>("Themes\\UglyTestTheme\\ListLine\\Background")},
                    {"ListLine.Scroll", content.Load<Texture2D>("Themes\\UglyTestTheme\\ListLine\\Scroll")},

                    {"Textbox.LeftSide", content.Load<Texture2D>("Themes\\UglyTestTheme\\Textbox\\LeftSide")},
                    {"Textbox.RightSide", content.Load<Texture2D>("Themes\\UglyTestTheme\\Textbox\\RightSide")},
                    {"Textbox.Separator", content.Load<Texture2D>("Themes\\UglyTestTheme\\Textbox\\Separator")},
                    {"Textbox.Background", content.Load<Texture2D>("Themes\\UglyTestTheme\\Textbox\\Background")}
                };

            Themes.Add("UglyTestTheme", testTheme);
        }

        static void LoadVideos(ContentManager content)
        {
            Videos.Add("Intro", content.Load<Video>("Videos\\Intro"));
        }
    }
}
