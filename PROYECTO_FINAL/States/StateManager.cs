using System;
using SFML.System;
using SFML.Graphics;
using SFML.Audio;

namespace Game
{
    static public class StateManager
    {
        static private GameLoop gameLoop;
        static private MainMenu menu;
        static private CreditsMenu credits;

        static private List<LoopState> scenes = new List<LoopState>();
        static private LoopState currentScene = null;

        static private SoundEffect selectSound;

        public enum SceneType
        {
            MainMenu, //0
            Game, //1
            Credits //2
        }

        static public void Initialize(RenderWindow window)
        {
            gameLoop = new GameLoop(window);
            menu = new MainMenu(window);
            credits = new CreditsMenu(window);
            scenes.Add(menu);
            scenes.Add(gameLoop);
            scenes.Add(credits);
        }

        static public void LoadScene(SceneType sceneType)
        {
            selectSound = new SoundEffect("Resources/Sounds/collect.wav");
            selectSound.SetVolume(10f);

            if (currentScene != null)
            {
                currentScene.Stop();
            }

            selectSound.Play();

            currentScene = scenes[Convert.ToInt32(sceneType)];
            currentScene.Play();
        }

        static public LoopState GetScene(SceneType sceneType)
        {
            return scenes[Convert.ToInt32(sceneType)];
        }

        static public void ExitGame()
        {
            foreach (LoopState scene in scenes)
            {
                scene.Stop();
            }
            return;
        }
    }
}