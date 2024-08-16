using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;

namespace Game
{
    public class Program
    {
        private static void Main()
        {
            VideoMode videoMode = new VideoMode(500, 250);
            string title = "RUN JOEY RUN";

            RenderWindow renderWindow = new RenderWindow(videoMode, title);

            StateManager.Initialize(renderWindow);
            StateManager.LoadScene(StateManager.SceneType.MainMenu);
        }
    }
}