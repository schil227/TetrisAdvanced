using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TetrisAdvanced.Data;
using TetrisAdvanced.Data.Enumerations;
using TetrisAdvanced.Interfaces;

namespace TetrisAdvanced.Services
{
    public class InputService : IInputService
    {
        public async Task HandleCommand(Engine engine)
        {
            ConsoleKey keyPressed;
            while (true)
            {
                keyPressed = Console.ReadKey(true).Key;

                if (keyPressed == ConsoleKey.Escape)
                {
                    engine.keyPressed = KeyInput.QUIT;
                    break;
                }
                else if (keyPressed == ConsoleKey.J)
                {
                    engine.keyPressed = KeyInput.MOVE_LEFT;
                }
                else if (keyPressed == ConsoleKey.L)
                {
                    engine.keyPressed = KeyInput.MOVE_RIGHT;
                }
                else if (keyPressed == ConsoleKey.K)
                {
                    engine.keyPressed = KeyInput.MOVE_DOWN;
                }
                else if (keyPressed == ConsoleKey.U)
                {
                    engine.keyPressed = KeyInput.ROTATE_LEFT;
                }
                else if (keyPressed == ConsoleKey.O)
                {
                    engine.keyPressed = KeyInput.ROTATE_RIGHT;
                }
            }
        }
    }
}
