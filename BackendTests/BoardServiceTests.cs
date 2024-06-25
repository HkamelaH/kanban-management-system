using System;
using Backend.ServiceLayer;

namespace BackendTests
{
    class BoardServiceTests
    {
        static void Main(string[] args)
        {
            BoardService boardService = new BoardService();
            TestCreateBoard(boardService);
            TestAddTask(boardService);
            TestMoveTask(boardService);

            Console.WriteLine("All tests passed.");
        }

        static void TestCreateBoard(BoardService boardService)
        {
            var result = boardService.CreateBoard("MyBoard");
            Console.WriteLine(result);
        }

        static void TestAddTask(BoardService boardService)
        {
            var result = boardService.AddTaskToBoard("MyBoard", "Task 1", "Description of task 1", DateTime.Now.AddDays(7));
            Console.WriteLine(result);
        }

        static void TestMoveTask(BoardService boardService)
        {
            var result = boardService.MoveTaskInBoard("MyBoard", 1, "In Progress");
            Console.WriteLine(result);

            result = boardService.MoveTaskInBoard("MyBoard", 1, "Done");
            Console.WriteLine(result);
        }
    }
}
