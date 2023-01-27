using System;
using MarsGame.Loader;
using MarsGame.Entity;
using MarsGame.Texture;
using MarsGame.Model;

namespace MarsGame.Controls
{
    public abstract class GameControl
    {
        public IMapLoader Loader { get; set; }

        protected Map Map { get; private set; }
        protected Player Player { get; private set; }
        protected Hunter Hunter { get; private set; }
        protected ITextureContainer Textures { get; private set; }

        public GameControl()
        {
        }

        public GameControl(IMapLoader loader)
        {
            Loader = loader;
        }

        public virtual void Init()
        {
            LoadMap();
            LoadPlayer();
            LoadHunter();

            Textures = new TestTextureContainer();
        }

        public abstract void Start();

        protected virtual void LoadMap()
        {
            Map = Loader.Load();
        }

        protected virtual void LoadPlayer()
        {
            Cell startPos = Map.FindByCondition((cell) => cell.IsStart);

            if (startPos == null)
            {
                throw new NullReferenceException("Start position for player not found");
            }

            Player = new Player();
            Player.MoveTo(startPos.Position.X, startPos.Position.Y);
        }

        protected virtual void LoadHunter()
        {
            Hunter = new Hunter(Map);
        }
    }
}
