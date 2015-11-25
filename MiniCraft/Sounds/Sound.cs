using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace MiniCraft.Sounds
{
    public class Sound
    {
        public static Sound playerHurt { get; private set; }
        public static Sound playerDeath { get; private set; }
        public static Sound monsterHurt { get; private set; }
        public static Sound test { get; private set; }
        public static Sound pickup { get; private set; }
        public static Sound bossdeath { get; private set; }
        public static Sound craft { get; private set; }

        public static void Initialize(ContentManager content)
        {
            playerHurt = new Sound(content, "playerhurt");
            playerDeath = new Sound(content, "death");
            monsterHurt = new Sound(content, "monsterhurt");
            test = new Sound(content, "test");
            pickup = new Sound(content, "pickup");
            bossdeath = new Sound(content, "bossdeath");
            craft = new Sound(content, "craft");
        }

        private SoundEffect soundEffect;

        private Sound(ContentManager content, string path)
        {
            soundEffect = content.Load<SoundEffect>("Sounds/" + path);
        }

        public void play()
        {
            soundEffect.Play();
        }
    }
}
