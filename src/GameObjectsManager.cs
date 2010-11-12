//07-08-2010
//Kamil Hawdziejuk

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XELibrary;
using Microsoft.Xna.Framework.Graphics;
using GameXna.GameObjects;
using GameXna.GameStates;

namespace GameXna
{
    public class GameObjectsManager : DrawableGameComponent
    {
        #region --- Private fields ---

        private SoundManager sound = null;
        private SphereCollisionDetector sphereCollisionDetector = new SphereCollisionDetector();
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> collidedObjects = new List<GameObject>();
        private GameObject activeObject = null;

        #endregion

        #region --- Creator & destroyers ---

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GameObjectsManager(Game game) : base(game)
        {
            this.sound = game.Components[2] as SoundManager;
        }

        #endregion

        #region --- Public properties --- 

        public GameObject ActiveObject
        {
            get
            {
                return this.activeObject;
            }
            set
            {
                this.activeObject = value;
            }
        }

        public List<GameObject> GameObjects
        {
            get
            {
                return this.gameObjects;
            }
        }

        #endregion

        #region --- Public methods ---

        public GameObject Get(string _name)
        {
            return this.gameObjects.Find(a => a.Name.Equals(_name));
        }
        
        public void Add(GameObject _obj)
        {
            if (!this.gameObjects.Contains(_obj))
            {
                this.gameObjects.Add(_obj);
            }
        }

        #endregion

        public override void Draw(GameTime gameTime)
        {
            foreach (GameObject obj in this.gameObjects)
            {
                obj.Draw();
            }

            base.Draw(gameTime);
        }


        public GameXna OurGame
        {
            get
            {
                return this.Game as GameXna;
            }
        }

        /// <summary>
        /// Aktualizuje stany obiektów, sprawdza zależności między nimi
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if ((this.Game as GameXna).GameStateManager.State is StartLevelState)
            {
                foreach (GameObject obj in this.gameObjects)
                {
                    //if (obj == this.activeObject)
                    {
                        obj.Update(gameTime);
                    }
                }

                //dla każdych wehikułów...
                foreach (GameVehicle vehicule in this.gameObjects.FindAll(a => a is GameVehicle))
                {
                    //i wystrzelonych z nich pocisków...
                    foreach (Bullet b in vehicule.Bullets.FindAll(a => a.State == BulletState.Running))
                    {
                        //sprawdz inne wehikuły...
                        foreach (GameVehicle vehicule2 in this.gameObjects.FindAll(a => a != b && a != vehicule))
                        {
                            //czy nie są w kolizji z tymi pociskami!
                            if (this.sphereCollisionDetector.DetectCollision(b, vehicule2, 0))
                            {
                                //zmień stan na "trafiony"
                                b.State = BulletState.Hit;
                                //i uruchom "dźwięk trafienia"
                                sound.StopAll();
                                sound.Play("explosion");

                                this.OurGame.WonGameState.Winner = vehicule;
                                this.OurGame.GameStateManager.PushState(this.OurGame.WonGameState.Value);
                            }
                        }
                    }
                }
                base.Update(gameTime);
            }
        }
    }
}
