﻿using Jimgine.Core.Manager;
using Jimgine.Core.Models.Commands;
using Jimgine.Core.Models.Graphics.UI;
using Jimgine.Core.Models.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Jimgine.Test
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        GameManager gameManager;
        IUIComponent health;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            gameManager = new GameManager(graphics, GraphicsDevice, Exit, Content);
            gameManager.InitialiseFromConfig(@"Base\game.json");

            //Testing stuff
            var lowerPlayersHealth = new Action(delegate ()
            {
                gameManager.StateManager.Player.AddHealth(-5);
            });

            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.Escape, new ActionCommand(Exit)));

            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.D, new ActionCommand(lowerPlayersHealth)));

            health = gameManager.GraphicsService.UIComponentFactory.CreateText(new Vector2(0, 0), 5, gameManager.StateManager.Player.Health.ToString(), Color.Black, "default");
            gameManager.StateManager.Player.HealthChanges += Player_HealthChanges;

            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.Left, new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingLeft(true); }), new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingLeft(false); })));
            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.Right, new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingRight(true); }), new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingRight(false); })));
            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.Up, new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingUp(true); }), new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingUp(false); })));
            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.Down, new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingDown(true); }), new ActionCommand(delegate () { gameManager.StateManager.Player.SetMovingDown(false); })));
            gameManager.InputService.AddInput(new KeyboardInputContainer(Keys.None, new ActionCommand(delegate () { gameManager.StateManager.Player.SetNotMoving(); })));

        }

        protected override void LoadContent()
        {
        }


        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            gameManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        private void Player_HealthChanges(object sender, EventArgs e)
        {
            health.SetValue<string>(e.ToString());
        }
    }
}
