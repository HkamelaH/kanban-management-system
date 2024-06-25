using IntroSE.Forum.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BussinessLayer.Board;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Linq;


namespace IntroSE.Kanban.Backend.ServiceLayer

public class Tasks
{
    private readonly Boardfacad boardFacad;
    //private readonly UserController userController;
    public Tasks(Boardfacad BoardFacad)
    {
        this.Boardfacad = boardFacad;

    }




    public string PlayingTasks(string email)
    {
        /// we have to implement response 

        try
        {
            List<Task> inprogressTasks = Boardfacad.InProgessTasks(email);
            Task[] tasksArray = inprogressTasks.ToArray();
            /// response = ...
            return JsonSerializer.Serialize(response);
        }
        catch (Exception e)
        {
            /// response = .. 
            return JsonSerializer.Serialize(response);
        }

    }

    public string ChangeTaskPlace(string email, string boardName, int columnOrdinal, int taskId)
    {
        /// same as above 
        try
        {
            Boardfacad.ChangeTaskPlace(email, boardName, columnOrdinal, taskId);
            ////
            return JsonSerializer.Serialize(response);
        }
        catch (Exception e)
        {
            ///  
            return JsonSerializer.Serialize(response);
        }

    }


    public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
    {
        ///
        try
        {
            Boardfacad.AddTask(email, boardName, title, description, dueDate);
            ///
            return JsonSerializer.Serialize(response);
        }
        catch (Exception e)
        {
            ///
            return JsonSerializer.Serialize(response);
        }
        public string UpdateTaskDateDue(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            ///
            try
            {
                Boardfacad.UpdateTaskDueDate(email, boardName, columnOrdinal, taskId, dueDate);
                ///
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                ///
                return JsonSerializer.Serialize(response);
            }

        }

        public string ChangeTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            ///
            try
            {
                Boardfacad.UpdateTaskTitle(email, boardName, columnOrdinal, taskId, title);
                ///
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                ///
                return JsonSerializer.Serialize(response);
            }
        }
        public string ChangeTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            ///
            try
            {
                Boardfacad.UpdateTaskTitle(email, boardName, columnOrdinal, taskId, title);
                ///
                return JsonSerializer.Serialize(response);
            }
            catch (Exception e)
            {
                ///
                return JsonSerializer.Serialize(response);
            }
        }



    }







}
