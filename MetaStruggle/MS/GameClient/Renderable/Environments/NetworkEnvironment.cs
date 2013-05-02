﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameClient.Characters;
using GameClient.Renderable.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Network;
using Network.Packet.Packets.DatasTypes;

namespace GameClient.Renderable.Environments
{
    public class NetworkEnvironment
    {
        private SceneManager sm;
        public string CurrentCharacterName { get; set; }

        public NetworkEnvironment(string currentCharName)
        {
            CurrentCharacterName = currentCharName;
        }

        public NetworkEnvironment(SpriteBatch spriteBatch)
        {
            sm = SceneManager.CreateScene(
                new Vector3(-5, 5, -30), //Position initiale de la caméra
                new Vector3(0, 0, 0), //Point visé par la caméra
                spriteBatch); //SpriteBatch



            sm.Camera.FollowsCharacters(sm.Camera, sm.Items.FindAll(e => e is Character));
        }

        public SceneManager GetScene(SpriteBatch spriteBatch)
        {
            return sm;
        }

        void RegisterEvents()
        {
            Global.GameEngine.EventManager.Register("Network.Game.GameStart", GameStart);
            Global.GameEngine.EventManager.Register("Network.Game.SetCharacterPosition", SetCharacterPosition);
        }

        void SetCharacterPosition(object data)
        {
            var cp = (CharacterPositionDatas) data;

            var c = (Character) sm.Items.Where(e => e is Character).First(e => (e as Character).ID == cp.ID);
            c.Position = new Vector3(cp.X, cp.Y, -17);
        }

        void GameStart(object data)
        {
            var gs = (GameStartDatas) data;

            foreach (var p in gs.Players)
                sm.AddElement(new Character(p.Name, p.ModelType, sm, new Vector3(0,0,-17), new Vector3(1), (p.ModelType == "Spiderman" || p.ModelType == "Alex") ? 1.6f : 1) {ID = p.ID, Playing = p.Name == CurrentCharacterName});
        }
    }
}
