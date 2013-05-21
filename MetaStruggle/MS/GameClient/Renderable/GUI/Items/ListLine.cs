﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameClient.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameClient.Renderable.GUI.Items
{
    public class ListLine : Item
    {
        private Dictionary<string, Texture2D> Theme { get; set; }
        private Rectangle InternalRectangle;
        float[] FieldsWidthAbstract { get; set; }
        int[] RealFieldsWidth { get; set; }
        Line Fields { get; set; }
        List<Line> Elements { get; set; }
        Line LineSelected { get; set; }
        public string[] Selected { get { return (LineSelected != null) ? LineSelected.Elements : null; } }

        int MaxLine { get; set; }
        private int StartPos;
        private int EndPos { get { return StartPos + MaxLine; } set { StartPos = value - MaxLine; } }
        private int _oldWheelValue;
        int HeightLine { get; set; }
        float Ratio { get; set; }

        public ListLine(Dictionary<string, float> fields, List<string[]> elements, Rectangle abstractRectangle, string theme, SpriteFont font, Color normalColor, Color selectedColor)
            : base(abstractRectangle, true)
        {
            Theme = RessourceProvider.Themes[theme];
            Elements = new List<Line>();
            FieldsWidthAbstract = fields.Values.ToArray();
            RealFieldsWidth = new int[fields.Values.Count];
            HeightLine = GetLineHeight(font);
            _oldWheelValue = GameEngine.MouseState.ScrollWheelValue;
            InternalRectangle = new Rectangle(RealRectangle.X + Theme["ListLine.LeftSide"].Width, RealRectangle.Y + HeightLine,
                RealRectangle.Width - (Theme["ListLine.LeftSide"].Width + Theme["ListLine.RightSide"].Width), RealRectangle.Height - (Theme["ListLine.Top"].Height + Theme["ListLine.Down"].Height));

            for (int i = 0; i < RealFieldsWidth.Length; i++)
                RealFieldsWidth[i] = (int)((FieldsWidthAbstract[i] / 100f) * InternalRectangle.Width);

            Fields = new Line(new Rectangle(InternalRectangle.X, RealRectangle.Y, InternalRectangle.Width, HeightLine), fields.Keys.ToArray(), RealFieldsWidth, font, normalColor, selectedColor, true);

            int heigth = InternalRectangle.Y;
            int newHeigth = HeightLine;
            MaxLine = 0;
            for (int i = 0; i < elements.Count; heigth += HeightLine, i++)
                if ((heigth + HeightLine) <= InternalRectangle.Height + InternalRectangle.Y)
                {
                    Elements.Add(new Line(new Rectangle(InternalRectangle.X, heigth, InternalRectangle.Width, HeightLine), elements[i], RealFieldsWidth, font, normalColor, selectedColor, true));
                    newHeigth += HeightLine;
                    MaxLine++;
                }
                else
                    Elements.Add(new Line(new Rectangle(InternalRectangle.X, heigth, InternalRectangle.Width, HeightLine), elements[i], RealFieldsWidth, font, normalColor, selectedColor, false));
            if (newHeigth > InternalRectangle.Height)
            {
                InternalRectangle.Height = newHeigth - HeightLine;
                Ratio = (Elements.Count*HeightLine)/(float) InternalRectangle.Height;
            }
            else
                Ratio = 1;
        }

        public override void DrawItem(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Theme["ListLine.Background"], InternalRectangle, Color.White);

            spriteBatch.Draw(Theme["ListLine.LeftSide"], new Rectangle(RealRectangle.X, InternalRectangle.Y,
                Theme["ListLine.LeftSide"].Width, InternalRectangle.Height), Color.White);

            spriteBatch.Draw(Theme["ListLine.RightSide"], new Rectangle(InternalRectangle.X + InternalRectangle.Width, InternalRectangle.Y,
                Theme["ListLine.RightSide"].Width, InternalRectangle.Height), Color.White);

            spriteBatch.Draw(Theme["ListLine.Top"], new Rectangle(RealRectangle.X, RealRectangle.Y, RealRectangle.Width, HeightLine), Color.White);

            spriteBatch.Draw(Theme["ListLine.Down"], new Rectangle(RealRectangle.X, InternalRectangle.Y + InternalRectangle.Height,
                RealRectangle.Width, Theme["ListLine.Down"].Height), Color.White);

            spriteBatch.Draw(Theme["ListLine.Scroll"], new Rectangle(InternalRectangle.X + InternalRectangle.Width,
                InternalRectangle.Y + (int)(((StartPos) * HeightLine) / (Ratio)),
                Theme["ListLine.RightSide"].Width, (int)(InternalRectangle.Height / Ratio)), Color.White);

            for (int i = 0, width = InternalRectangle.X + RealFieldsWidth[i]; i < RealFieldsWidth.Length - 1; i++, width += RealFieldsWidth[i])
                spriteBatch.Draw(Theme["ListLine.Separator"],
                                 new Rectangle(width - 3, InternalRectangle.Y,
                                               Theme["ListLine.Separator"].Width, InternalRectangle.Height), Color.White);

            Fields.DrawItem(gameTime, spriteBatch);
            foreach (Line element in Elements.Where(element => element.IsDrawable))
                element.DrawItem(gameTime, spriteBatch);

            
        }

        public override void UpdateItem(GameTime gameTime)
        {
            foreach (Line element in Elements.Where(element => element.IsDrawable))
            {
                element.UpdateItem(gameTime);
                if (!element.IsSelect || element.Equals(LineSelected))
                    continue;

                if (LineSelected != null)
                    LineSelected.IsSelect = false;
                LineSelected = element;
            }
            if (StartPos > 0 && GameEngine.MouseState.ScrollWheelValue > _oldWheelValue)
            {
                StartPos--;
                for (int index = 0; index < Elements.Count; index++)
                {
                    Elements[index].IsDrawable = (index >= StartPos && index < EndPos);
                    Elements[index].UpdatePosition(HeightLine);
                }
            }
            else if (EndPos < Elements.Count && GameEngine.MouseState.ScrollWheelValue < _oldWheelValue)
            {
                StartPos++;
                for (int index = 0; index < Elements.Count; index++)
                {
                    Elements[index].IsDrawable = (index >= StartPos && index < EndPos);
                    Elements[index].UpdatePosition(-HeightLine);
                }
            }
            _oldWheelValue = GameEngine.MouseState.ScrollWheelValue;
            //base.UpdateItem(gameTime); //mettre UpadateResolution en Virtual
        }
    }
}
