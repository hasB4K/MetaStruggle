﻿using System.Collections.Generic;
using GameClient.Global;
using GameClient.Renderable.GUI;

namespace GameClient.Renderable.Layout
{
    public class LayoutStack<T> : Stack<T>
    {
        new public void Pop()
        {
            var e = Peek();
            base.Pop();
            if (!(e is Menu) && Peek() is Menu)
                GameEngine.SoundCenter.Play("Main Title");
        }

        new public void Push(T e)
        {
            if (!(e is Menu) && Count > 0 && Peek() is Menu)
                GameEngine.SoundCenter.Stop("Main Title");
            base.Push(e);
        }
    }
}
