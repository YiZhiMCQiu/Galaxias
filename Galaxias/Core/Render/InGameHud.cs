﻿using Galasias.Core.Render;
using Galaxias.Core.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxias.Core.Render;
public class InGameHud
{
    private readonly string _heartTexture = "Assets/Textures/Gui/heart";
    private readonly string _heartHalfTexture = "Assets/Textures/Gui/heart_half";
    private SpriteFont _font;
    private GalaxiasClient _client;
    private FrameCounter _frameCounter = new();
    public InGameHud(GalaxiasClient galaxias) {
        _client = galaxias;
    }
    public void LoadContent()
    {
        _font = GalaxiasClient.GetInstance().Content.Load<SpriteFont>("Assets/Fonts/defaultFont");
    }
    public void Render(IntegrationRenderer renderer,float dTime)
    {
        _frameCounter.Update(dTime);
        RenderString(renderer, "X:" + Math.Round(_client.GetPlayer().x, 1), 0, 0);
        RenderString(renderer, "Y:" + Math.Round(_client.GetPlayer().y, 1), 0, _font.LineSpacing * 1f);
        RenderString(renderer, "FPS:" + Math.Round(_frameCounter.AverageFramesPerSecond, 1).ToString(), 0f, _font.LineSpacing * 2f);
        RenderString(renderer, "Speed:" + Math.Round(Math.Sqrt(_client.GetPlayer().vx * _client.GetPlayer().vx + _client.GetPlayer().vy * _client.GetPlayer().vy), 1),0, _font.LineSpacing * 3f);
        //MouseState state = Mouse.GetState();
        //Point pos = state.Position;
        //Vector2 p = camera.ScreenToWorldSpace(pos);
        //RenderString("Mouse Pos:" + p / 8f, new Vector2(0, 24));
        float w = 300;
        int health = (int)Math.Round(_client.GetPlayer().health / 10);
        for (int i = 4;i >= 0;i--)
        {
            if (health > 0)
            {
                health -= 2;
                if (health < 0)
                {
                    renderer.Draw(_heartHalfTexture, w - i * 8, 1, Color.White);
                }
                else
                {
                    renderer.Draw(_heartTexture, w - i * 8, 1, Color.White);
                }
            } else
            {
                break;
            }
        }
    }
    internal void RenderString(IntegrationRenderer renderer,string s, float x, float y, float scale = 1)
    {

        renderer.DrawString(_font, s, x, y, Color.White, Color.Black);
    }

}
